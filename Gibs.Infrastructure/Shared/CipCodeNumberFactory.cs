using Gibs.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gibs.Infrastructure
{
    public class CipCodeNumberFactory : ICodeNumberFactory
    {
        private readonly Branch _branch;
        private readonly Product? _product;
        private readonly GibsContext _context;

        internal CipCodeNumberFactory(GibsContext context, Branch branch, Product? product)
        {
            _context = context;
            _product = product;
            _branch = branch;
        }

        public string CreateCodeNumber(CodeTypeEnum numType, string? bizSource = null)
        {
            switch (numType)
            {
                case CodeTypeEnum.POLICY:
                    return GetNextPolicyAutoNumber(_branch, _product, bizSource).Result;

                case CodeTypeEnum.CLAIM:
                    return GetNextSubriskClaimAutoNumber(_branch, _product);

                case CodeTypeEnum.CLAIM_NOTIFY:
                    return GetNextSubriskNotifyAutoNumber(_branch, _product);

                default:
                    return GetNextAutoNumber(numType, _branch, _product, bizSource).Result;
            }
        }


        #region Private Functions
        // cls_PolicyAutoNumber.GetSerialNoFormatSubClass
        private async Task<string> GetNextPolicyAutoNumber(Branch branch, Product? product, string? bizSource)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            var policyAutoNumber = await _context.PolicyAutoNumbers
                                            .Where(x => x.RiskID == product.Id &&
                                                        x.BranchID == branch.Id)
                                            .FirstOrDefaultAsync();
            if (policyAutoNumber is null)
                throw new InvalidOperationException($"Missing [policyAutoNumber] for " +
                    $"branch:{branch.Id}, product:{product.Id}");

            var nextValue = policyAutoNumber.NextValue;
            long serialNo = (nextValue.HasValue && nextValue.Value > 0 ? nextValue.Value : 1);

            //TODO here
            string year = DateTime.Now.Year.ToString();
            string riskID = policyAutoNumber.RiskID;
            string serial = serialNo.ToString("00000");
            string prefix = GetPrefix(bizSource, product.MidClassId);

            string result = $"{prefix}/{branch.Id}/{riskID}/{year}/{serial}";

            //save the next value
            policyAutoNumber.NextValue = serialNo + 1;
            return result;

            static string GetPrefix(string? bizSource, string midRiskID)
            {
                if (bizSource == "ACCEPTED")
                    return "IN";

                if (midRiskID == "401")
                    return "OC";

                return "P";
            }
        }

        // cls_SubRisk.GetSerialNoFormatSubClass -> CLAIM (subRisk.Deleted)
        private static string GetNextSubriskClaimAutoNumber(Branch branch, Product? product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            long serialNo = 1;

            //if (product.AutoNumNextClaimNo.HasValue && product.AutoNumNextClaimNo.Value > 0)
            //    serialNo = product.AutoNumNextClaimNo.Value;

            if (product.AutoNumNextClaimNo > 0)
                serialNo = product.AutoNumNextClaimNo;

            var year = DateTime.Today.Year;
            var serial = serialNo.ToString("00000");

            string str = $"C/{branch.Id}/{product.Id}/{year}/{serial}";

            //save the next value
            product.SetNextClaimNo(serialNo + 1);
            return str;
        }

        // cls_SubRisk.GetSerialNoFormatSubClass -> NOTIFY (subRisk.Active)
        private static string GetNextSubriskNotifyAutoNumber(Branch branch, Product? product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            long serialNo = 1;

            //if (product.AutoNumNextNotifyNo.HasValue && product.AutoNumNextNotifyNo.Value > 0)
            //    serialNo = product.AutoNumNextNotifyNo.Value;

            if (product.AutoNumNextNotifyNo > 0)
                serialNo = product.AutoNumNextNotifyNo;

            var year = DateTime.Today.Year;
            var serial = serialNo.ToString("00000");

            string str = $"N/{branch.Id}/{product.Id}/{year}/{serial}";

            //save the next value
            product.SetNextNotifyNo(serialNo + 1);
            return str;
        }

        private async Task<string> GetNextAutoNumber(CodeTypeEnum numType, Branch branch, Product? product, string? endorseType = null, DateTime thisDate = default)
        {
            //'$00000$ - Serial Number
            //'$SR$ - Sub Risk Code
            //'$RC$ - Risk Code
            //'$YR$ - Current Year (2 digits)
            //'$MO$ - Current Month (2 Digits)
            //'$BR$ - Branch Code
            //'$BR2$ - Branch Code 2

            //'$P$ - Endorsement something

            static string GetEndorsementCode(string? endorseType)
            {
                switch (endorseType)
                {
                    case "DELETE":
                    case "DELETED":
                    case "REVERSAL":
                    case "RETURN":
                        return "C";

                    case "RENEW":
                    case "RENEWAL":
                        return "R";

                    case "ADDITIONAL":
                        return "E";

                    case "NIL":
                    case "REDO":
                        return "N";

                    default:
                        return "A";
                }
            }

            if (thisDate == default)
                thisDate = DateTime.Today;

            try
            {
                string? format = await GetSerialNoFormat(numType, branch, null);//product

                if (format != null && format.Contains("$0"))
                {
                    long nextNum = await GetNextSerialNo(numType, branch, null);//product

                    if (nextNum > 0)
                    {
                        int len = format.IndexOf("0$") - format.IndexOf("$0");
                        if (len < 3) len = 3;

                        string zeros = new('0', len);
                        format = format.Replace($"${zeros}$", nextNum.ToString(zeros)); //formation of number
                    }
                    else
                    {
                        throw new Exception("Invalid format in $0000$");
                    }
                }
                else
                {
                    throw new Exception("Missing format $0000$");
                }

                format = format.Replace("$YR$", thisDate.ToString("yy"));
                format = format.Replace("$MO$", thisDate.ToString("MM"));

                if (format.Contains("$SR$") || format.Contains("$RC$"))
                {
                    if (product is null)
                        throw new ArgumentNullException(nameof(product),
                            $"AutoNumber Format [{format}] requires SubRisk");
                    format = format.Replace("$SR$", product.Id);
                    format = format.Replace("$RC$", product.ClassId);
                }

                if (format.Contains("$BR$") || format.Contains("$BR2$"))
                {
                    if (branch is null)
                        throw new ArgumentNullException(nameof(branch),
                            $"AutoNumber Format [{format}] requires Branch");
                    format = format.Replace("$BR$", branch.Id);
                    format = format.Replace("$BR2$", branch.AltName);
                }

                if (format.Contains("$P$"))
                {
                    if (endorseType is null)
                        throw new ArgumentNullException(nameof(endorseType),
                            $"AutoNumber Format [{format}] requires EndorseType");
                    format = format.Replace("$P$", GetEndorsementCode(endorseType));
                }

                return format.ToUpper();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error in GetNextAutoNumber()\n\r" + ex.ToString());
            }
        }

        private async Task<long> GetNextSerialNo(CodeTypeEnum numType, Branch branch, Product? product)
        {
            try
            {
                var query = _context.AutoNumbers.Where(x => x.NumType == numType.ToString());
                var result = await query.Where(x => x.BranchID == "ALL").ToListAsync();

                if (result.Count > 0)
                    return ResultOf(result, 1);

                query = query.Where(x => x.BranchID == branch.Id);

                if (product is null)
                    return ResultOf(await query.ToListAsync(), -1);

                if (numType.ToString() == "CLAIM")
                    return ResultOf(await query.ToListAsync(), -1);

                return ResultOf(await query.Where(x => x.RiskID == product.ClassId).ToListAsync(), -1);
            }
            catch
            {
                return -2;
            }

            static long ResultOf(List<AutoNumber> list, long errorNo)
            {
                if (list.Count > 0)
                {
                    long result = 0;
                    if (list[0].NextValue.HasValue)
                        result = list[0].NextValue!.Value;
                    else
                        result = 1;

                    // then increment and update
                    list[0].NextValue += 1;
                    return result;
                }
                return errorNo;
            }
        }

        private async Task<string?> GetSerialNoFormat(CodeTypeEnum numType, Branch branch, Product? product)
        {
            try
            {
                var query = _context.AutoNumbers.Where(x => x.NumType == numType.ToString());
                var result = await query.Where(x => x.BranchID == "ALL").ToListAsync();

                if (result.Count > 0)
                    return result[0].Format;

                query = query.Where(x => x.BranchID == branch.Id);

                if (product is null)
                    return ResultOf(await query.ToListAsync());

                if (numType.ToString() == "CLAIM")
                    return ResultOf(await query.ToListAsync());

                return ResultOf(await query.Where(x => x.RiskID == product.ClassId).ToListAsync());
            }
            catch
            {
                throw new InvalidOperationException("Error in GetSerialNoFormat()");
            }

            static string? ResultOf(List<AutoNumber> list)
            {
                if (list.Count > 0)
                    return list[0].Format;

                //'no number setup
                return string.Empty;
            }
        }
        #endregion
    }
}
