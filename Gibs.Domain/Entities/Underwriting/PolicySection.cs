using System.Reflection;
using System.ComponentModel.DataAnnotations;

namespace Gibs.Domain.Entities
{
    public class PolicySection : AuditRecord
    {
        public record struct ComputeData(
            decimal FxRate, 
            decimal OurShareRate, 
            string EndorsementNo, 
            string EndorsementType, 
            string PartyID, 
            string PartyName);

        #pragma warning disable CS8618
        private PolicySection() /*EfCore*/
        {
            SMIs = new ReadOnlyList<PolicySMI>();
            ExFields = new ExtendedFields();
        }
        #pragma warning restore CS8618

        public PolicySection(
            ICodeNumberFactory codeFactory,
            PolicyHistory history,
            SectionRecord request)
        {
            ArgumentNullException.ThrowIfNull(history);

            ArgumentNullException.ThrowIfNull(request);

            if (history.Policy.Product == null)
                throw new ArgumentNullException(nameof(history.Policy.Product));

            var policy = history.Policy;
            var product = policy.Product;

            DeclareNo = history.DeclareNo;
            ProductId = product.Id;
            SectionId = request.SectionId;

            ItemSumInsured = request.SumInsured;
            ItemPremium = request.Premium;

            History = history;
            SMIs = new ReadOnlyList<PolicySMI>();
            ExFields = new ExtendedFields();


            //PolicyNo = history.PolicyNo;
            //StartDate = history.Business.StartDate;
            //EndDate = history.Business.EndDate;
            //ClassID = product.ClassID;


            if ("V,M,".Contains(product.ClassId))
            {
                string? suppliedCertNo = null; //ExFields.FAKE_CertificateNo ??
                CertificateNo = suppliedCertNo ?? codeFactory.CreateCodeNumber(CodeTypeEnum.CERTNO);
            }


            ////if V, M sectionID = CoverType
            //if ("V,M,".Contains(ClassID))
            //{
            //    //var coverType = request.Fields.FirstOrDefault(x => x.Code.ToLower() == "covertype");
            //    var coverType = request.Fields.FirstOrDefault(x => x.Code.EqualsIgnoreCase("coverType"));

            //    if (coverType != null)
            //        SectionID = coverType.Value;
            //}

            //UpdateCreatedBy("E-CHANNEL");

            var data = new ComputeData
            {
                FxRate = history.Business.FxRate,
                OurShareRate = history.Business.OurShareRate,
                EndorsementNo = history.EndorsementNo,
                EndorsementType = history.Endorsement.ToString(),
                PartyID = policy.Members.PartyId,
                PartyName = policy.Members.PartyName,
            };

            var fields = policy.Product.GetAllFields();
            MapRequestFieldsToPolicySection(this, request, fields, data);

            ////map to hack_fields, this must be done before SMIs
            //{
            //    ExFields.OtherSum = request.SumInsured;
            //    ExFields.EndorsementType = history.Endorsement;

            //    //Marine hacks
            //    SectionID     = ExFields.FAKE_SubjectMatter ?? SectionID;
            //    CertificateNo = ExFields.FAKE_CertificateNo ?? codeFactory.CreateCodeNumber(CodeTypeEnum.CERTNO);
            //}

            // if there are sections for this product, do SMI
            if (policy.Product.Sections.Count != 0)
            {
                var sectionID = request.SectionId;

                //if product has only 1 section, then ignore the one sent
                if (product.Sections.Count == 1)
                    sectionID = product.Sections.First().SectionId;

                var productSection = product.Sections.FirstOrDefault(x => x.SectionId == sectionID);

                if (productSection == null)
                {
                    var validSectionIDs = string.Join(",", product.Sections.Select(x => x.SectionId));
                    throw new ArgumentOutOfRangeException(nameof(request.SectionId),
                        $"Invalid SectionID [{sectionID}]. Please use [{validSectionIDs}]");
                }

                SectionId = productSection.SectionId;
                MapRequestSMIsToPolicySMIs(this, request, productSection);
            }
        }

        private static void MapRequestFieldsToPolicySection(
            PolicySection section, SectionRecord request, 
            IEnumerable<ProductField> fields, ComputeData data)
        {
            // update Fields1-50, A1-50 from NameValueFields
            foreach (var field in fields)
            {
                //var value = request.Fields.FirstOrDefault(x => x.Code.ToLower() == field.FieldKey.ToLower())?.Value;
                var value = request.Fields.FirstOrDefault(x => x.Code.EqualsIgnoreCase(field.FieldId))?.Value;

                // nothing was submitted, for a REQUIRED field
                if (string.IsNullOrWhiteSpace(value) && field.IsRequired && !field.IsHidden)
                    throw new ArgumentNullException(nameof(field), $"SectionField [{field.FieldId}] is required");

                // nothing was submitted, save default value
                if (string.IsNullOrWhiteSpace(value))
                    value = field.DefaultValue;

                // nothing was submitted, no default, and we don't care
                if (string.IsNullOrWhiteSpace(value) && !field.IsRequired)
                    continue;

                switch (field.FieldType)
                {
                    case ProductField.FieldTypeEnum.STRING:
                        {
                            value = value is null ? string.Empty : value;
                            SetStringProperty(section, field, value);
                            break;
                        }
                    case ProductField.FieldTypeEnum.ENUM:
                        {
                            if (!field.TryParseStringEnum(value, out var stringEnumPair))
                                throw new ArgumentOutOfRangeException(field.FieldId,
                                    $"Enum value expected for [{field.FieldId}]. Use [{field.MinimumValue}]");

                            SetStringProperty(section, field, stringEnumPair.Value);
                            break;
                        }
                    case ProductField.FieldTypeEnum.INT:
                        {
                            if (!int.TryParse(value, out var intValue))
                                throw new ArgumentOutOfRangeException(field.FieldId,
                                    $"Integer value expected for [{field.FieldId}]");

                            SetOtherProperty(section, field, intValue);
                            break;
                        }
                    case ProductField.FieldTypeEnum.DECIMAL:
                        {
                            if (!decimal.TryParse(value, out var decValue))
                                throw new ArgumentOutOfRangeException(field.FieldId,
                                    $"Decimal value expected for [{field.FieldId}]");

                            SetOtherProperty(section, field, decValue);
                            break;
                        }
                    case ProductField.FieldTypeEnum.DATE:
                        {
                            if (!ProductField.TryParseDate(value, out var dateTimeValue))
                                throw new ArgumentOutOfRangeException(field.FieldId,
                                    $"DateTime[{ProductField.DATE_FORMAT}] value expected for [{field.FieldId}]");

                            SetOtherProperty(section, field, dateTimeValue);
                            break;
                        }
                    case ProductField.FieldTypeEnum.COMPUTE:
                        {
                            //variables:
                            //SumInsured, Premium, FxRate, OurShareRate

                            if (string.IsNullOrWhiteSpace(field.DefaultValue))
                                break; //no formula. skip

                            string formula = field.DefaultValue;
                            formula = formula.Replace("{SumInsured}", request.SumInsured.ToString());
                            formula = formula.Replace("{Premium}", request.Premium.ToString());
                            formula = formula.Replace("{FxRate}", data.FxRate.ToString());
                            formula = formula.Replace("{OurShareRate}", data.OurShareRate.ToString());

                            decimal compValue = (decimal)Compute(formula).Result;
                            SetOtherProperty(section, field, compValue);
                            break;
                        }
                    case ProductField.FieldTypeEnum.INTERPOLATE:
                        {
                            //variables:
                            //EndorsementNo, EndorsementType, PartyID, PartyName 

                            if (string.IsNullOrWhiteSpace(field.DefaultValue))
                                break; //no format. skip

                            string format = field.DefaultValue;
                            format = format.Replace("{EndorsementType}", data.EndorsementType);
                            format = format.Replace("{EndorsementNo}", data.EndorsementNo);
                            format = format.Replace("{PartyName}", data.PartyName);
                            format = format.Replace("{PartyID}", data.PartyID);

                            SetStringProperty(section, field, format);
                            break;
                        }
                    default:
                        break;
                }
            }

            // local functions
            static void SetStringProperty(PolicySection section, ProductField dtoField, string value)
            {
                if (dtoField.DbSectionField.Length > 2)
                {
                    var dbField = GetClassProperty(section.ExFields, dtoField.DbSectionField);
                    dbField.SetValue(section.ExFields, value);
                }

                if (dtoField.DbHistoryField.Length > 2)
                {
                    //var dbField = GetClassProperty(section.History.ExFields, dtoField.DbHistoryField);
                    //dbField.SetValue(section.History.ExFields, value);
                }
            }

            static void SetOtherProperty<T>(PolicySection section, ProductField dtoField, T value)
                where T : struct
            {
                ThrowIfInvalidRange(dtoField, value);

                if (dtoField.DbSectionField.Length > 2)
                {
                    var dbField = GetClassProperty(section.ExFields, dtoField.DbSectionField);
                    var dbFieldType = dbField.PropertyType;

                    if (dbFieldType.IsGenericType && dbFieldType.GetGenericTypeDefinition() == typeof(Nullable<>))
                        dbFieldType = Nullable.GetUnderlyingType(dbFieldType)!;

                    var castValue = GetTypedValue(dbFieldType, dtoField, value);
                    dbField.SetValue(section.ExFields, castValue);
                }

                if (dtoField.DbHistoryField.Length > 2)
                {
                    //var dbField = GetClassProperty(section.History.ExFields, dtoField.DbHistoryField);
                    //var dbFieldType = dbField.PropertyType;

                    //if (dbFieldType.IsGenericType && dbFieldType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    //    dbFieldType = Nullable.GetUnderlyingType(dbFieldType)!;

                    //var castValue = GetTypedValue(dbFieldType, dtoField, value);
                    //dbField.SetValue(section.History.ExFields, castValue);
                }
            }

            static void ThrowIfInvalidRange<T>(ProductField dtoField, T value)
                where T : struct
            {
                var outOfRangeException = new ArgumentOutOfRangeException(dtoField.FieldId,
                    $"[{dtoField.FieldId}] should be {dtoField.MinimumValue} to {dtoField.MaximumValue}");

                if (!string.IsNullOrWhiteSpace(dtoField.MaximumValue))
                {
                    switch (value)
                    {
                        case int intValue:
                            if (intValue > int.Parse(dtoField.MaximumValue))
                                throw outOfRangeException;
                            break;

                        case decimal decValue:
                            if (decValue > decimal.Parse(dtoField.MaximumValue))
                                throw outOfRangeException;
                            break;

                        case DateTime dateValue:
                            if (dateValue > ProductField.ParseDate(dtoField.MaximumValue))
                                throw outOfRangeException;
                            break;
                    }
                }

                if (!string.IsNullOrWhiteSpace(dtoField.MinimumValue))
                {
                    switch (value)
                    {
                        case int intValue:
                            if (intValue < int.Parse(dtoField.MinimumValue))
                                throw outOfRangeException;
                            break;

                        case decimal decValue:
                            if (decValue < decimal.Parse(dtoField.MinimumValue))
                                throw outOfRangeException;
                            break;

                        case DateTime dateValue:
                            if (dateValue < ProductField.ParseDate(dtoField.MinimumValue))
                                throw outOfRangeException;
                            break;
                    }
                }
            }

            static object GetTypedValue<T>(Type dbFieldType, ProductField dtoField, T value)
                where T : struct
            {
                if (dbFieldType == value.GetType())
                    return value;

                if (dbFieldType == typeof(string))
                    switch (value)
                    {
                        //string/enum, decimal/computed, int, date
                        case DateTime date:
                            return date.ToString(ProductField.DATE_FORMAT);

                        case decimal dec:
                            return dec.ToString("G");

                        case int integer:
                            return integer.ToString("G");
                    }

                if (dbFieldType == typeof(decimal))
                    switch (value)
                    {
                        case int integer:
                            return Convert.ToDecimal(integer);
                    }

                throw new Exception($"Cannot map Field [{dtoField.FieldId} ({dtoField.FieldType})] " +
                                    $"to DB Column [{dtoField.DbSectionField} ({dbFieldType})]");
            }

            static Task<double> Compute(string formula)
            {
                return Microsoft.CodeAnalysis.CSharp.Scripting
                    .CSharpScript.EvaluateAsync<double>(formula);
            }
        }

        private static void MapRequestSMIsToPolicySMIs(
            PolicySection section, SectionRecord request, 
            ProductSection productSection)
        {
            if (request.Smis == null || !request.Smis.Any())
                return;

            if (request.Smis.Sum(x => x.SumInsured) != request.SumInsured)
                throw new ArgumentOutOfRangeException(nameof(request.SumInsured),
                    $"Total Sum of SMI SumInsured [{request.Smis.Sum(x => x.SumInsured)}] must equal Section SumInsured [{request.SumInsured}]");

            if (request.Smis.Sum(x => x.Premium) != request.Premium)
                throw new ArgumentOutOfRangeException(nameof(request.Premium),
                    $"Total Sum of SMI Premium [{request.Smis.Sum(x => x.Premium)}] must equal Section Premium [{request.Premium}]");

            foreach (var requestSMI in request.Smis)
            {
                var productSMI = productSection.SMIs.FirstOrDefault(x => x.SmiId == requestSMI.SmiId);

                if (productSMI == null)
                {
                    var validSMIs = string.Join(",", productSection.SMIs.Select(x => x.SmiId));
                    throw new ArgumentOutOfRangeException(nameof(requestSMI.SmiId),
                        $"Invalid SMI Code [{requestSMI.SmiId}] supplied. Please select from [{validSMIs}]");
                }

                var smi = new PolicySMI(productSMI, section, requestSMI);               
                ((ICollection<PolicySMI>)section.SMIs).Add(smi);
            }
        }

        //private static bool StringEquals(string? str1, string? str2)
        //{
        //    return string.Equals(str1, str2, StringComparison.OrdinalIgnoreCase);
        //}

        public T GetFieldValue<T>(string fieldKey, T defaultValue) where T : struct
        {
            var product = History.Policy.Product;
            var field = product.GetAllFields()
                               .FirstOrDefault(x => x.FieldId == fieldKey);
            if (field == null)
                return defaultValue;

            object? result = GetClassPropertyValue(this, field);

            if (result is T value)
                return value;

            return defaultValue;
        }

        public string? GetFieldValue(string fieldKey, string defaultValue = "")
        {
            var product = History.Policy.Product;
            var field = product.GetAllFields()
                               .FirstOrDefault(x => x.FieldId == fieldKey);
            if (field == null)
                return "*ERROR*";

            var result = GetClassPropertyValue(this, field);

            if (result == null)
                return defaultValue;

            return result.ToString();
        }

        private static object? GetClassPropertyValue(PolicySection section, ProductField dtoField)
        {
            if (dtoField.DbSectionField.Length > 2)
                return GetClassProperty(section.ExFields, dtoField.DbSectionField)
                    .GetValue(section.ExFields);

            //if (dtoField.DbHistoryField.Length > 2)
            //    return GetClassProperty(section.History.ExFields, dtoField.DbHistoryField)
            //        .GetValue(section.History.ExFields);

            return null;
        }

        private static PropertyInfo GetClassProperty(ExtendedFields obj, string dbMapField)
        {
            var property = obj.GetType().GetProperty($"{dbMapField}",
                BindingFlags.NonPublic | BindingFlags.Public |
                BindingFlags.Instance  | BindingFlags.IgnoreCase);

            if (property == null)
                throw new Exception($"DB Column [{dbMapField}] does not exist in database");

            return property;
        }

        public IEnumerable<FieldRecord> GetFieldRecords(bool includeHidden = false)
        {
            var p = History.Policy.Product;
            var fields = includeHidden ? p.GetAllFields() : p.GetVisibleFields();

            foreach (ProductField field in fields)
            {
                var oValue = GetClassPropertyValue(this, field);
                var sValue = oValue == null ? string.Empty : oValue.ToString() + "";

                //how can we get numerics/dates/enums etc
                //to return correctly for older data?
                switch (field.FieldType)
                {
                    case ProductField.FieldTypeEnum.INT:
                        if (!int.TryParse(sValue, out _))
                        {
                            sValue = "0";
                        }
                        break;

                    case ProductField.FieldTypeEnum.DECIMAL:
                    case ProductField.FieldTypeEnum.COMPUTE:
                        if (!decimal.TryParse(sValue, out _))
                        {
                            sValue = "0.0";
                        }
                        break;

                    case ProductField.FieldTypeEnum.DATE:
                        if (!ProductField.TryParseDate(sValue, out _))
                        {
                            sValue = "";
                        }
                        break;

                    case ProductField.FieldTypeEnum.ENUM:
                        if (field.TryParseStringEnum(sValue, out var stringEnumPair))
                        {
                            sValue = stringEnumPair.Key;
                        }
                        break;

                    case ProductField.FieldTypeEnum.STRING:
                    case ProductField.FieldTypeEnum.INTERPOLATE:
                    default:
                        //sValue ??= string.Empty;
                        break;
                }

                yield return new FieldRecord(field.FieldId, sValue);
            }
        }

        public IEnumerable<RateRecord> GetRateRecords()
        {
            return new List<RateRecord>();
        }

        public IEnumerable<SmiRecord> GetSMIRecords()
        {
            foreach (PolicySMI smi in SMIs)
            {
                yield return new SmiRecord(smi.SmiId, smi.TotalSumInsured,
                    smi.TotalPremium, smi.PremiumRate, smi.Description);               
            }
        }

        #region Public Properties
        public long SerialNo { get; }
        //public string PolicyNo { get; protected set; }
        //public string ClassID { get; protected set; }
        public string DeclareNo { get; protected set; }
        public string ProductId { get; private set; }
        public string SectionId { get; protected set; }
        public string? CertificateNo { get; protected set; }

        public decimal ItemSumInsured { get; protected set; }
        public decimal ItemPremium { get; protected set; }


        //public DateOnly StartDate { get; protected set; }
        //public DateOnly EndDate { get; protected set; }

        public ExtendedFields ExFields { get; }

        //Navigation Properties
        //public virtual ProductSection ProductSection { get; protected set; }
        public virtual PolicyHistory History { get; protected set; }
        public ReadOnlyList<PolicySMI> SMIs { get; protected set; }
        #endregion
    }

    public class ExtendedFields
    {
        public decimal A1 { get; set; }
        public decimal A2 { get; set; }
        public decimal A3 { get; set; }
        public decimal A4 { get; set; }
        public decimal A5 { get; set; }
        public decimal A6 { get; set; }
        public decimal A7 { get; set; }
        public decimal A8 { get; set; }
        public decimal A9 { get; set; }
        public decimal A10 { get; set; }
        public decimal A11 { get; set; }
        public decimal A12 { get; set; }
        public decimal A13 { get; set; }
        public decimal A14 { get; set; }
        public decimal A15 { get; set; }
        public decimal A16 { get; set; }
        public decimal A17 { get; set; }
        public decimal A18 { get; set; }
        public decimal A19 { get; set; }
        public decimal A20 { get; set; }
        public decimal A21 { get; set; }
        public decimal A22 { get; set; }
        public decimal A23 { get; set; }
        public decimal A24 { get; set; }
        public decimal A25 { get; set; }
        public decimal A26 { get; set; }
        public decimal A27 { get; set; }
        public decimal A28 { get; set; }
        public decimal A29 { get; set; }
        public decimal A30 { get; set; }
        public decimal A31 { get; set; }
        public decimal A32 { get; set; }
        public decimal A33 { get; set; }
        public decimal A34 { get; set; }
        public decimal A35 { get; set; }
        public decimal A36 { get; set; }
        public decimal A37 { get; set; }
        public decimal A38 { get; set; }
        public decimal A39 { get; set; }
        public decimal A40 { get; set; }
        public decimal A41 { get; set; }
        public decimal A42 { get; set; }
        public decimal A43 { get; set; }
        public decimal A44 { get; set; }
        public decimal A45 { get; set; }
        public decimal A46 { get; set; }
        public decimal A47 { get; set; }
        public decimal A48 { get; set; }
        public decimal A49 { get; set; }
        public decimal A50 { get; set; }

        public string? Field1 { get; set; }
        public string? Field2 { get; set; }
        public string? Field3 { get; set; }
        public string? Field4 { get; set; }
        public string? Field5 { get; set; }
        public string? Field6 { get; set; }
        public string? Field7 { get; set; }
        public string? Field8 { get; set; }
        public string? Field9 { get; set; }
        public string? Field10 { get; set; }
        public string? Field11 { get; set; }
        public string? Field12 { get; set; }
        public string? Field13 { get; set; }
        public string? Field14 { get; set; }
        public string? Field15 { get; set; }
        public string? Field16 { get; set; }
        public string? Field17 { get; set; }
        public string? Field18 { get; set; }
        public string? Field19 { get; set; }
        public string? Field20 { get; set; }
        public string? Field21 { get; set; }
        public string? Field22 { get; set; }
        public string? Field23 { get; set; }
        public string? Field24 { get; set; }
        public string? Field25 { get; set; }
        public string? Field26 { get; set; }
        public string? Field27 { get; set; }
        public string? Field28 { get; set; }
        public string? Field29 { get; set; }
        public string? Field30 { get; set; }
        public string? Field31 { get; set; }
        public string? Field32 { get; set; }
        public string? Field33 { get; set; }
        public string? Field34 { get; set; }
        public string? Field35 { get; set; }
        public string? Field36 { get; set; }
        public string? Field37 { get; set; }
        public string? Field38 { get; set; }
        public string? Field39 { get; set; }
        public string? Field40 { get; set; }
        public string? Field41 { get; set; }
        public string? Field42 { get; set; }
        public string? Field43 { get; set; }
        public string? Field44 { get; set; }
        public string? Field45 { get; set; }
        public string? Field46 { get; set; }
        public string? Field47 { get; set; } //NAICOMID
        public string? Field48 { get; set; }
        public string? Field49 { get; set; }
        public string? Field50 { get; set; } // = "UNCOMPLETED";
    }
}
