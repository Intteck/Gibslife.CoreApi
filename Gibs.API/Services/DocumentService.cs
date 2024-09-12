//using System;
//using System.IO;
//using System.Linq;
//using System.Text.RegularExpressions;
//using Gibs.Domain.Entities;

//namespace Gibs.Api.Services
//{
//    public partial class DocumentService 
//    {
//        public (string Filename, Stream Stream)? FetchCertificate(Policy policy, string? documentNo = null)
//        {
//            var fullPath = GetTemplateFilePath(policy.Product);

//            foreach (var history in policy.GetActiveHistories())
//            {
//                foreach (var section in history.Sections)
//                {
//                    if (documentNo == null || section.CertificateNo == documentNo)
//                    {
//                        var cert = CreateCertificateFromTemplate(section, fullPath);
//                        var filename = GenerateCertificateFilename(section);
//                        return (filename, cert);
//                    }
//                }
//            }
//            return null;
//        }

//        private static string GenerateCertificateFilename(PolicySection section)
//        {
//            return $"CIP-{section.DeclarationNo}-{section.CertificateNo}.pdf";
//        }

//        private static string GetTemplateFilePath(Product product)
//        {
//            var templateFile = $@"Templates\Certificates\{product.ProductID}.docx";
//            var fullPath = Path.Combine(AppContext.BaseDirectory, templateFile);
//            if (File.Exists(fullPath)) return fullPath;

//            templateFile = $@"Templates\Certificates\{product.MidClassID}.docx";
//            fullPath = Path.Combine(AppContext.BaseDirectory, templateFile);
//            if (File.Exists(fullPath)) return fullPath;

//            templateFile = $@"Templates\Certificates\{product.ClassID}.docx";
//            fullPath = Path.Combine(AppContext.BaseDirectory, templateFile);
//            if (File.Exists(fullPath)) return fullPath;

//            throw new Exception($"No Certificate for Policy Type [{product.ClassID}]");
//        }

//        [GeneratedRegex(@"<([\w\s\-\[\]]+)>")] private static partial Regex MyRegexPattern();
//        private static MemoryStream CreateCertificateFromTemplate(PolicySection sec, string fullPath)
//        {
//            var ph = sec.History;
//            var docx = new Spire.Doc.Document(fullPath);

//            var pattern = MyRegexPattern();
//            var matches = docx.FindAllPattern(pattern)
//                              .Select(x => x.SelectedText)
//                              .Distinct().ToList();

//            foreach (var match in matches)
//            {
//                var (field, format) = GetFieldFormat(match);
//                var objValue = GetValue(field, sec);
//                var strValue = GetFormattedValue(objValue, format);

//                docx.Replace(match, strValue, false, true);
//            }

//            var pdfParams = new Spire.Doc.ToPdfParameterList
//            {
//                PdfConformanceLevel = Spire.Doc.PdfConformanceLevel.Pdf_A2A
//            };

//            var stream = new MemoryStream();
//            docx.SaveToStream(stream, pdfParams);
//            docx.Close();

//            stream.Seek(0, SeekOrigin.Begin);
//            return stream;
//        }

//        private static (string field, string format) GetFieldFormat(string match)
//        {
//            var p = match.IndexOf('[');
//            var q = match.IndexOf(']');

//            //if ((q < p) || (p == 0))
//            //    continue;
//            //    //throw new Exception("Invalid text Field pattern. Use <EndDate[yyyy-MMM-dd]> or simply <EndDate>");

//            if (p < 0)
//                return (match[1..^1], string.Empty);

//            return (match[1..p], match[(p + 1)..q]);
//        }

//        private static object? GetValue(string fieldName, PolicySection sec)
//        {
//            var ph = sec.History;

//            return fieldName switch
//            {
//                "PolicyNo" => ph.PolicyNo,
//                "NaicomUID" => ph.NaicomID,
//                "CertificateNo" => sec.CertificateNo,
//                "PolicyHolder" => ph.Policy.Customer.FullName,
//                "StartDate" => ph.Business.StartDate,
//                "EndDate" => ph.Business.EndDate,
//                "SumInsured" => ph.Premium.SumInsured,
//                "Premium" => ph.Premium.GrossPremium,
//                "PremiumRate" => ph.Business.FlatPremiumRate,
//                "FxRate" => ph.Business.FxRate,

//                //get from mapped Fields in DB
//                //TODO: for enumType, can we return the description if available?
//                //maybe using format like [EnumDesc]/[DisplayText]
//                _ => sec.GetFieldValue(fieldName) 
//            };
//        }

//        private static string? GetFormattedValue(object? value, string format)
//        {
//            if (value is null)
//                return string.Empty;

//            return value switch
//            {
//                string x => x,
//                decimal x => x.ToString(format),
//                long x => x.ToString(format),
//                int x => x.ToString(format),
//                DateTime x => x.ToString(format),
//                _ => value.ToString(),
//            };
//        }
//    }
//}