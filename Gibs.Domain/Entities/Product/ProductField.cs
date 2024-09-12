using System.Diagnostics.CodeAnalysis;

namespace Gibs.Domain.Entities
{
	public class ProductField : AuditRecord
	{
		#pragma warning disable CS8618 
        private ProductField() { }
		#pragma warning restore CS8618 

        public ProductField(object owner, string fieldCode, FieldTypeEnum fieldType, string dbSectionField, string? dbHistoryField, bool isParent, string description, Dictionary<string, string>? valuePairs)
			: this(fieldCode, fieldType, dbSectionField, dbHistoryField, isParent, description, valuePairs)
		{
            ArgumentNullException.ThrowIfNull(owner);

            switch (owner)
			{
				case Class c:
					CodeId = c.Id;
					CodeType = CodeTypeEnum.RISKID;
					break;

				case MidClass m:
					CodeId = m.Id;
					CodeType = CodeTypeEnum.MIDRISKID;
					break;

				case Product p:
					CodeId = p.Id;
					CodeType = CodeTypeEnum.SUBRISKID;
					break;

				default:
					throw new InvalidOperationException();
			};
		}

		public ProductField(Product p, string fieldCode, FieldTypeEnum fieldType, string dbSectionField, string? dbHistoryField, bool isParent, string description, Dictionary<string, string>? valuePairs)
			: this(fieldCode, fieldType, dbSectionField, dbHistoryField, isParent, description, valuePairs)
		{
            ArgumentNullException.ThrowIfNull(p);

            CodeId = p.Id;
			CodeType = CodeTypeEnum.SUBRISKID;
		}

		public ProductField(MidClass m, string fieldCode, FieldTypeEnum fieldType, string dbSectionField, string? dbHistoryField, bool isParent, string description, Dictionary<string, string>? valuePairs)
			: this(fieldCode, fieldType, dbSectionField, dbHistoryField, isParent, description, valuePairs)
		{
            ArgumentNullException.ThrowIfNull(m);

            CodeId = m.Id;
			CodeType = CodeTypeEnum.MIDRISKID;
		}

		public ProductField(Class c, string fieldCode, FieldTypeEnum fieldType, string dbSectionField, string? dbHistoryField, bool isParent, string description, Dictionary<string, string>? valuePairs)
			: this(fieldCode, fieldType, dbSectionField, dbHistoryField, isParent, description, valuePairs)
		{
            ArgumentNullException.ThrowIfNull(c);

            CodeId = c.Id;
			CodeType = CodeTypeEnum.RISKID;
		}

		private ProductField(string fieldKey, FieldTypeEnum fieldType, string dbSectionField, string? dbHistoryField, bool isParent, string description, Dictionary<string, string>? valuePairs)
		{
			if (string.IsNullOrWhiteSpace(fieldKey))
				throw new ArgumentNullException(nameof(fieldKey));

            UpdateDbMapFields(dbSectionField, dbHistoryField);

			if (fieldType == FieldTypeEnum.ENUM)
				UpdateEnumFieldType(valuePairs);

			if (fieldType != FieldTypeEnum.ENUM)
				UpdateOtherFieldType(fieldType);

			CodeId = Guid.NewGuid().ToString();
			IsParent = isParent;
			FieldId = fieldKey;
			//FieldType = fieldType;
			Description = description;
			//IsRequired = false;
			SerialNo = 0;
		}

		public void UpdateOtherFieldType(FieldTypeEnum fieldType)
		{
			if (fieldType == FieldTypeEnum.ENUM)
				throw new ArgumentException("Enum Type is not allowed on this method", nameof(fieldType));

			FieldType = fieldType;
		}

		public void UpdateEnumFieldType(Dictionary<string, string>? valuePairs)
		{
			if (valuePairs == null || valuePairs.Count < 1)
				throw new ArgumentException("Enum options are required", nameof(valuePairs));

			FieldType = FieldTypeEnum.ENUM;

			MinimumValue = string.Join(",", valuePairs.Select(x => x.Key).ToList());
			MaximumValue = string.Join(",", valuePairs.Select(x => x.Value).ToList());
		}

		[MemberNotNull(nameof(DbSectionField), nameof(DbHistoryField))]
		public void UpdateDbMapFields(string dbSectionField, string? dbHistoryField)
		{
			if (string.IsNullOrWhiteSpace(dbSectionField))
				dbSectionField = "-";

			if (string.IsNullOrWhiteSpace(dbHistoryField))
				dbHistoryField = "-";

			DbSectionField = dbSectionField;
			DbHistoryField = dbHistoryField;
		}

		public void UpdateDefault(object defaultValue)
		{
			DefaultValue = defaultValue.ToString();

		}

		public void UpdateMinMax(object min, object max)
		{
			switch (FieldType)
			{
				case FieldTypeEnum.INT:
					break;
				case FieldTypeEnum.DATE:
					break;
				case FieldTypeEnum.DECIMAL:
					break;

				case FieldTypeEnum.ENUM:
				case FieldTypeEnum.STRING:
				case FieldTypeEnum.COMPUTE:
				case FieldTypeEnum.INTERPOLATE:
				default:
					throw new InvalidOperationException($"Max & Min not applicable to {FieldType}");
			}
		}


		public static Func<ProductField, Class, bool> WhereClass { get; } = (x, c) =>
		x.CodeId == c.Id && x.CodeType == CodeTypeEnum.RISKID;

		public static Func<ProductField, MidClass, bool> WhereMidClass { get; } = (x, m) =>
		(x.CodeId == m.Id && x.CodeType == CodeTypeEnum.MIDRISKID) ||
		(x.CodeId == m.ClassId    && x.CodeType == CodeTypeEnum.RISKID     && x.IsParent == true);

		public static Func<ProductField, Product, bool> WhereProduct { get; } = (x, p) =>
		(x.CodeId == p.Id  && x.CodeType == CodeTypeEnum.SUBRISKID) ||
		(x.CodeId == p.MidClassId && x.CodeType == CodeTypeEnum.MIDRISKID) ||
		(x.CodeId == p.ClassId    && x.CodeType == CodeTypeEnum.RISKID     && x.IsParent == true);

		public static Func<ProductField, ProductSection, bool> WhereSection { get; } = (x, s) =>
		(x.CodeId == s.ProductId + "/" + s.SectionId && x.CodeType == CodeTypeEnum.SECTIONID) ||
		(x.CodeId == s.ProductId					 && x.CodeType == CodeTypeEnum.SUBRISKID);// ||
		//(x.CodeID == s.Product.MidClassID && x.CodeType == CodeTypeEnum.MIDRISKID) ||
		//(x.CodeID == s.Product.ClassID && x.CodeType == CodeTypeEnum.RISKID && x.IsParent == true);


		#region Date Functions
		//2012-04-23T18:25:43.511Z
		public const string DATE_FORMAT = "yyyy-MM-dd";

		public static bool TryParseDate(string? value, out DateTime dateTimeValue)
		{
			//return DateTime.TryParseExact(value, DATE_FORMAT,
			//				System.Globalization.CultureInfo.InvariantCulture,
			//				System.Globalization.DateTimeStyles.None,
			//				out dateTimeValue);
			return DateTime.TryParse(value, out dateTimeValue);
		}

		public static DateTime ParseDate(string value)
		{
			//return DateTime.ParseExact(value, DATE_FORMAT,
			//				System.Globalization.CultureInfo.InvariantCulture,
			//				System.Globalization.DateTimeStyles.None);
			return DateTime.Parse(value);
		}
        #endregion

        #region StringEnum Functions
        public bool TryParseStringEnum(string? word, out KeyValuePair<string, string> stringEnumPair)
        {
			stringEnumPair = default;

            if (string.IsNullOrWhiteSpace(word))
                return false;

            if (string.IsNullOrWhiteSpace(MinimumValue))
                return false;

			//------------------------------------------------------------------
			// consider moving this split() code to init area eg. constructor
			// 
            if (string.IsNullOrWhiteSpace(MaximumValue))
                MaximumValue = string.Empty;

            var options = StringSplitOptions.RemoveEmptyEntries
                        | StringSplitOptions.TrimEntries;

            var values = MaximumValue.Split(',', options).ToList();
            var keys = MinimumValue.Split(',', options).ToList();

            static bool compareIgnoreCase(string x, string y) => 
				string.Equals(x, y, StringComparison.OrdinalIgnoreCase);

			var notFound = -1;
            //------------------------------------------------------------------

            //check in enum keys/names
            int index = keys.FindIndex(x => compareIgnoreCase(x, word));

            if (index == notFound)
            {
                //check value in enum values/description
                index = values.FindIndex(x => compareIgnoreCase(x, word));

                if (index == notFound)
                    return false;

				// found in values
				stringEnumPair = new KeyValuePair<string, string>(keys[index], values[index]);
				return true;
            }

			//if there is a corresponding value for this key
            if (index < values.Count)
			{
                stringEnumPair = new KeyValuePair<string, string>(keys[index], values[index]);
                return true;
            }

            stringEnumPair = new KeyValuePair<string, string>(keys[index], keys[index]);
            return true;
        }


        public string? VerifyStringEnumValue(string? key)
		{
			if (string.IsNullOrWhiteSpace(key))
				return null;

			if (string.IsNullOrWhiteSpace(MinimumValue))
				return null;

			if (string.IsNullOrWhiteSpace(MaximumValue))
				MaximumValue = string.Empty;

			var options = StringSplitOptions.RemoveEmptyEntries
				        | StringSplitOptions.TrimEntries;

			var values = MaximumValue.Split(',', options).ToList();
			var keys = MinimumValue.Split(',', options).ToList();

			//check value in enum names
			int index = keys.FindIndex(str => str.EqualsIgnoreCase(key));

			if (index == -1)
            {
				//check value in enum description
				index = values.FindIndex(str => str.EqualsIgnoreCase(key));

                if (index == -1)
					return null;

				return values[index];
			}

			if (index < values.Count)
				return values[index];

			return keys[index];
		}

		public Dictionary<string, string> GetStringEnumValues()
		{
			var keyValuePairs = new Dictionary<string, string>();

			if (MinimumValue != null &&
				FieldType == FieldTypeEnum.ENUM)
			{
				var options = StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries;
				string[] keys = MinimumValue.Split(',', options);
				string[] values = Array.Empty<string>();

				if (MaximumValue != null)
					values = MaximumValue.Split(',', options);

				for (int i = 0; i < keys.Length; i++)
				{
					var value = string.Empty;
					if (values.Length - 1 >= i) value = values[i];

					keyValuePairs.Add(keys[i], value);
				}
			}
			return keyValuePairs;
		}

        #endregion

        #region Public Properties
		public string CodeId { get; private set; }
		public CodeTypeEnum CodeType { get; private set; }
		public string FieldId { get; private set; }
		public FieldTypeEnum FieldType { get; private set; }
		public string DbSectionField { get; private set; }
		public string DbHistoryField { get; private set; }

		public string? Description { get; set; }

		public bool IsParent { get; set; }
		public bool IsRequired { get; set; }
		public bool IsHidden => FieldId.StartsWith("_");

		public string? DefaultValue { get; private set; }
		public string? MinimumValue { get; private set; }
		public string? MaximumValue { get; private set; }

		public string? GroupName { get; private set; }
		public long SerialNo { get; set; }

		public string FieldTypeDescription
		{
			get
			{
				if (FieldType == FieldTypeEnum.ENUM)
					return $"enum ({MinimumValue})";

				if (FieldType == FieldTypeEnum.DATE)
					return $"date ({DATE_FORMAT})";

				var typeString = FieldType.ToString().ToLower();

				if (!string.IsNullOrWhiteSpace(MinimumValue) &&
					!string.IsNullOrWhiteSpace(MaximumValue))
					return $"{typeString} ({MinimumValue} to {MaximumValue})";

				return typeString;
			}
		}

		#endregion

		public enum CodeTypeEnum
		{
			RISKID,
			MIDRISKID,
			SUBRISKID,
			SECTIONID,
		}

		public enum FieldTypeEnum
		{
			INT,
			DATE,
			ENUM, //string enums
			STRING,
			DECIMAL,
			COMPUTE,
			INTERPOLATE,
		}
	}
}
