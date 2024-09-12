using Gibs.Infrastructure;
using Gibs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net.Mime;

namespace Gibs.Migrations
{
    public class Program
    {
        private const string docxContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
        public static async Task Main()
        {
            var context = GibsContextFactory.CreateDbContext();

            //await RunMigrationsSQL(context);
            //await context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE [policy].[Templates]");

            //await CopyTemplatePolicyDocs(context);
            //await CopyTemplateEndorsementDocs(context);
        }

        private static async Task RunMigrationsSQL(GibsContext context)
        {
            var dir = AppDomain.CurrentDomain.BaseDirectory;
            var filePaths = Directory.GetFiles(@$"{dir}Migration-SQL", "*.sql");

            foreach (var filePath in filePaths)
            {
                var fileName = Path.GetFileName(filePath);
                var fileSql = File.ReadAllText(filePath);

                try
                {
                    Console.WriteLine($"Processing --> {fileName}");
                    var rowsAffected = await context.Database.ExecuteSqlRawAsync(fileSql);
                    Console.WriteLine($"{rowsAffected} rows affected");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static async Task CopyTemplatePolicyDocs(GibsContext context)
        {
            var dir = AppDomain.CurrentDomain.BaseDirectory;
            var filePaths = Directory.GetFiles(@$"{dir}Template-Files\Policy", "*.docx");

            foreach (var filePath in filePaths)
            {
                var fileName = Path.GetFileName(filePath);
                var fileData = File.ReadAllBytes(filePath);

                Console.WriteLine($"Uploading --> {fileName}");
                var productId = fileName.Replace(".docx", "");

                var id = TemplateID.Create(productId);
                var doc = new TemplateDoc(id, fileData, docxContentType, fileName);
                context.Templates.Add(doc);
                await context.SaveChangesAsync();

                Console.WriteLine($"Uploaded --> {fileName}");
            }
        }

        private static async Task CopyTemplateEndorsementDocs(GibsContext context)
        {
            var dir = AppDomain.CurrentDomain.BaseDirectory;
            var filePaths = Directory.GetFiles(@$"{dir}Template-Files\Endorsement", "*.docx");

            foreach (var filePath in filePaths)
            {
                var fileName = Path.GetFileName(filePath);
                var fileData = File.ReadAllBytes(filePath);

                Console.WriteLine($"Uploading --> {fileName}");

                var caption = fileName.Replace("Endorse.docx", "");
                var collective = false;

                if (caption.Contains("COLLECTIVE"))
                {
                    collective = true;
                    caption = caption.Replace("COLLECTIVE_", "");
                }

                var q = caption.Split('_'); string endorseId = q[0], riskId = q[1];

                var id = TemplateID.Create(endorseId, riskId, collective);
                var doc = new TemplateDoc(id, fileData, docxContentType, fileName);
                context.Templates.Add(doc);
                await context.SaveChangesAsync();

                Console.WriteLine($"Uploaded --> {fileName}");
            }
        }
    }
}

//private const string _conn = "Data Source=tcp:mssql-124479-0.cloudclusters.net,10018;Initial Catalog=gibs-core-db;User ID=idevworks;Password=1N$pirion;TrustServerCertificate=true";

//public static async Task Main(string[] args)
//{
//    var services = new ServiceCollection();

//    services.AddDbContext<GibsContext>(options =>
//    {
//        options.UseSqlServer(_conn);
//        options.EnableSensitiveDataLogging(true);
//    });

//    var serviceProvider = services.BuildServiceProvider();

//    var dbContext = serviceProvider.GetService<GibsContext>()
//        ?? throw new Exception("No context found");

//    //assign the GetLogonUser() delegate, needed by SaveChangesAsync()
//    //dbContext.GetLogonUser = () => GibsContext.SystemAccount;

//    await CopyOldGibsData(dbContext);
//    //await CopyTemplatePolicy(dbContext);
//}