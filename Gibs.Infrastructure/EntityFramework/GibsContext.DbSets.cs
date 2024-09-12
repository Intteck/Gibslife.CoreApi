using Gibs.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gibs.Infrastructure
{
    public partial class GibsContext : DbContext
    {
        public DbSet<Region> Regions { get; set; }
        //public DbSet<Branch> Branches { get; set; }
        public DbSet<SalesChannel> SalesChannels { get; set; }
        public DbSet<SalesSubChannel> SalesSubChannels { get; set; }
        public DbSet<Marketer> Marketers { get; set; }
        public DbSet<Setting> Settings { get; set; }

        // Security
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Approval> Approvals { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Signature> Signatures { get; set; }

        // Product
        public DbSet<Class> Classes { get; set; }
        //public DbSet<MidClass> MidClasses { get; set; } 
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductField> ProductFields { get; set; }
        public DbSet<ProductSMI> ProductSMIs { get; set; }
        public DbSet<MemoClause> MemoClauses { get; set; }
        public DbSet<MarineClause> MarineClauses { get; set; }
        public DbSet<MotorClause> MotorClauses { get; set; }


        public DbSet<TemplateDoc> Templates { get; set; }


        //Agency
        public DbSet<Party> Parties { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CommRate> PartyRates { get; set; }
        public DbSet<PartyType> PartyTypes { get; set; }


        //Accounts
        public DbSet<Account> Accounts { get; set; }
        public DbSet<FxCurrency> Currencies { get; set; }

        //---------------------------------------------------------------------------
        public DbSet<DebitNote> DebitNotes { get; set; }
        //public DbSet<Receipt> Receipts { get; set; }  

        // Policy
        public DbSet<Policy> Policies { get; set; }


        //public DbSet<CodeNumber> CodeNumbers { get; set; }
        public DbSet<ClaimNotify> Claims { get; set; }


        //hacks for old db
        public DbSet<AutoNumber> AutoNumbers { get; set; }              //remove later, use CodeNumber entity
        public DbSet<PolicyAutoNumber> PolicyAutoNumbers { get; set; }  //remove later, use CodeNumber entity
    }
}