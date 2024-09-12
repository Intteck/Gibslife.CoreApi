using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gibs7.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class abc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "account");

            migrationBuilder.EnsureSchema(
                name: "security");

            migrationBuilder.EnsureSchema(
                name: "common");

            migrationBuilder.EnsureSchema(
                name: "agency");

            migrationBuilder.EnsureSchema(
                name: "policy");

            migrationBuilder.EnsureSchema(
                name: "product");

            migrationBuilder.EnsureSchema(
                name: "claim");

            migrationBuilder.CreateTable(
                name: "AuditLog",
                schema: "security",
                columns: table => new
                {
                    AuditLogID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModuleID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedIP = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLog", x => x.AuditLogID);
                });

            migrationBuilder.CreateTable(
                name: "AutoNumbers",
                columns: table => new
                {
                    NumType = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BranchID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RiskID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NextValue = table.Column<long>(type: "bigint", nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Format = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sample = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutoNumbers", x => new { x.NumType, x.BranchID });
                });

            migrationBuilder.CreateTable(
                name: "Blobs",
                schema: "common",
                columns: table => new
                {
                    BlobID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Data = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blobs", x => x.BlobID);
                });

            migrationBuilder.CreateTable(
                name: "ControlAccounts",
                schema: "account",
                columns: table => new
                {
                    ControlID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ControlName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntryType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControlAccounts", x => x.ControlID);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                schema: "agency",
                columns: table => new
                {
                    CustomerID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherNames = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Industry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RiskProfileID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerTypeID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneAlt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityLGA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactPerson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KycNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KycTypeID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KycIssueDate = table.Column<DateOnly>(type: "date", nullable: true),
                    KycExpiryDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "Fields",
                schema: "product",
                columns: table => new
                {
                    CodeID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CodeTypeID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FieldID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DataTypeID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DbSectionField = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DbHistoryField = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsParent = table.Column<byte>(type: "tinyint", nullable: false),
                    IsRequired = table.Column<byte>(type: "tinyint", nullable: false),
                    DefValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Group = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Serial = table.Column<short>(type: "smallint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fields", x => new { x.CodeTypeID, x.CodeID, x.FieldID });
                });

            migrationBuilder.CreateTable(
                name: "FxCurrencies",
                schema: "account",
                columns: table => new
                {
                    FxCurrencyID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FxCurrencyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FxCurrencies", x => x.FxCurrencyID);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                schema: "security",
                columns: table => new
                {
                    GroupID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.GroupID);
                });

            migrationBuilder.CreateTable(
                name: "MarineClauses",
                columns: table => new
                {
                    ClauseId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarineClauses", x => x.ClauseId);
                });

            migrationBuilder.CreateTable(
                name: "Marketers",
                schema: "agency",
                columns: table => new
                {
                    MarketerID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ChannelID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubChannelID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marketers", x => x.MarketerID);
                });

            migrationBuilder.CreateTable(
                name: "Master",
                schema: "policy",
                columns: table => new
                {
                    PolicyNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SN = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneAlt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NaicomUID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartyID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MarketerID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MarketerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChannelID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChannelName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubChannelID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubChannelName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CoPolicyNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BizTypeID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SrcTypeID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    StdCoverDays = table.Column<int>(type: "int", nullable: false, defaultValue: 365),
                    CurrencyID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrencyRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OurShareRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoverDays = table.Column<int>(type: "int", nullable: false),
                    ActTypeID = table.Column<byte>(type: "tinyint", nullable: false),
                    GrossPremiumFx = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GrossPremium = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WholePremium = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SharePremium = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProrataPremium = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetPremium = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SumInsuredFx = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SumInsured = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShareSumInsured = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WholeSumInsured = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Master", x => x.PolicyNo);
                });

            migrationBuilder.CreateTable(
                name: "MemoClauses",
                columns: table => new
                {
                    ClauseId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HeaderText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemoClauses", x => x.ClauseId);
                });

            migrationBuilder.CreateTable(
                name: "MotorClauses",
                columns: table => new
                {
                    ClauseId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotorClauses", x => x.ClauseId);
                });

            migrationBuilder.CreateTable(
                name: "PartyTypes",
                schema: "agency",
                columns: table => new
                {
                    PartyTypeID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PartyTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartyTypes", x => x.PartyTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                schema: "security",
                columns: table => new
                {
                    PermissionID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PermissionName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.PermissionID);
                });

            migrationBuilder.CreateTable(
                name: "PolicyAutoNumbers",
                columns: table => new
                {
                    NumType = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BranchID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RiskID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NextValue = table.Column<long>(type: "bigint", nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Format = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sample = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyAutoNumbers", x => new { x.NumType, x.RiskID, x.BranchID });
                });

            migrationBuilder.CreateTable(
                name: "Rates",
                schema: "agency",
                columns: table => new
                {
                    RiskOptionID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PartyOptionID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CommRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VatRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rates", x => new { x.RiskOptionID, x.PartyOptionID });
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                schema: "common",
                columns: table => new
                {
                    RegionID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RegionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AltName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.RegionID);
                });

            migrationBuilder.CreateTable(
                name: "Risks",
                schema: "product",
                columns: table => new
                {
                    RiskID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RiskName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Risks", x => x.RiskID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "security",
                columns: table => new
                {
                    RoleID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                schema: "common",
                columns: table => new
                {
                    SettingID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SettingName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataTypeID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DefValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.SettingID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "security",
                columns: table => new
                {
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StaffNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AvatarUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PwdExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastLoginDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    ApiKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Templates",
                schema: "policy",
                columns: table => new
                {
                    TemplateID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TypeID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BlobID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Templates", x => x.TemplateID);
                    table.ForeignKey(
                        name: "FK_Templates_Blobs_BlobID",
                        column: x => x.BlobID,
                        principalSchema: "common",
                        principalTable: "Blobs",
                        principalColumn: "BlobID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                schema: "account",
                columns: table => new
                {
                    AccountID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ControlID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountID);
                    table.ForeignKey(
                        name: "FK_Accounts_ControlAccounts_ControlID",
                        column: x => x.ControlID,
                        principalSchema: "account",
                        principalTable: "ControlAccounts",
                        principalColumn: "ControlID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FxRates",
                schema: "account",
                columns: table => new
                {
                    FxRateID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrencyID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FxRates", x => x.FxRateID);
                    table.ForeignKey(
                        name: "FK_FxRates_FxCurrencies_CurrencyID",
                        column: x => x.CurrencyID,
                        principalSchema: "account",
                        principalTable: "FxCurrencies",
                        principalColumn: "FxCurrencyID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                schema: "agency",
                columns: table => new
                {
                    NoteNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DeclarationNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoteTypeID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApprovedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovedUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Approval = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PolicyNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Narration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartyID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FxCurrencyID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FxRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Z_NAICOM_UID = table.Column<string>(type: "varchar(200)", nullable: true),
                    Z_NAICOM_SENT_ON = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Z_NAICOM_STATUS = table.Column<string>(type: "varchar(20)", nullable: true),
                    Z_NAICOM_ERROR = table.Column<string>(type: "varchar(max)", nullable: true),
                    Z_NAICOM_JSON = table.Column<string>(type: "varchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.NoteNo);
                    table.ForeignKey(
                        name: "FK_Notes_Master_PolicyNo",
                        column: x => x.PolicyNo,
                        principalSchema: "policy",
                        principalTable: "Master",
                        principalColumn: "PolicyNo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Parties",
                schema: "agency",
                columns: table => new
                {
                    PartyID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PartyTypeID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CommTypeID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PartyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneAlt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityLGA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactPerson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KycNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KycTypeID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KycIssueDate = table.Column<DateOnly>(type: "date", nullable: true),
                    KycExpiryDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parties", x => x.PartyID);
                    table.ForeignKey(
                        name: "FK_Parties_PartyTypes_PartyTypeID",
                        column: x => x.PartyTypeID,
                        principalSchema: "agency",
                        principalTable: "PartyTypes",
                        principalColumn: "PartyTypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Branches",
                schema: "common",
                columns: table => new
                {
                    BranchID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RegionID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    StateID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AltName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.BranchID);
                    table.ForeignKey(
                        name: "FK_Branches_Regions_RegionID",
                        column: x => x.RegionID,
                        principalSchema: "common",
                        principalTable: "Regions",
                        principalColumn: "RegionID");
                });

            migrationBuilder.CreateTable(
                name: "MidRisks",
                schema: "product",
                columns: table => new
                {
                    MidRiskID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MidRiskName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RiskID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MidRisks", x => x.MidRiskID);
                    table.ForeignKey(
                        name: "FK_MidRisks_Risks_RiskID",
                        column: x => x.RiskID,
                        principalSchema: "product",
                        principalTable: "Risks",
                        principalColumn: "RiskID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                schema: "security",
                columns: table => new
                {
                    RoleID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PermissionID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => new { x.RoleID, x.PermissionID });
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_PermissionID",
                        column: x => x.PermissionID,
                        principalSchema: "security",
                        principalTable: "Permissions",
                        principalColumn: "PermissionID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_RoleID",
                        column: x => x.RoleID,
                        principalSchema: "security",
                        principalTable: "Roles",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Approvals",
                schema: "security",
                columns: table => new
                {
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApprovalID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MinValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Approvals", x => new { x.UserID, x.ApprovalID });
                    table.ForeignKey(
                        name: "FK_Approvals_Users_UserID",
                        column: x => x.UserID,
                        principalSchema: "security",
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GroupUsers",
                schema: "security",
                columns: table => new
                {
                    GroupID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupUsers", x => new { x.GroupID, x.UserID });
                    table.ForeignKey(
                        name: "FK_GroupUsers_Groups_GroupID",
                        column: x => x.GroupID,
                        principalSchema: "security",
                        principalTable: "Groups",
                        principalColumn: "GroupID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupUsers_Users_UserID",
                        column: x => x.UserID,
                        principalSchema: "security",
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Signatures",
                schema: "security",
                columns: table => new
                {
                    SignatureID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BlobID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Signatures", x => x.SignatureID);
                    table.ForeignKey(
                        name: "FK_Signatures_Blobs_BlobID",
                        column: x => x.BlobID,
                        principalSchema: "common",
                        principalTable: "Blobs",
                        principalColumn: "BlobID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Signatures_Users_UserID",
                        column: x => x.UserID,
                        principalSchema: "security",
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Details",
                schema: "policy",
                columns: table => new
                {
                    DeclareNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SN = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PolicyNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EndorseNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndorseTypeID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartyID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MarketerID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MarketerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChannelID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChannelName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubChannelID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubChannelName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CoPolicyNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BizTypeID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SrcTypeID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    StdCoverDays = table.Column<int>(type: "int", nullable: false, defaultValue: 365),
                    CurrencyID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrencyRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OurShareRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoverDays = table.Column<int>(type: "int", nullable: false),
                    ActTypeID = table.Column<byte>(type: "tinyint", nullable: false),
                    GrossPremiumFx = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GrossPremium = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WholePremium = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SharePremium = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProrataPremium = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetPremium = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SumInsuredFx = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SumInsured = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShareSumInsured = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WholeSumInsured = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DebitNoteNo = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    NaicomUID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprovedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovedUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Approval = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Details", x => x.DeclareNo);
                    table.ForeignKey(
                        name: "FK_Details_Master_PolicyNo",
                        column: x => x.PolicyNo,
                        principalSchema: "policy",
                        principalTable: "Master",
                        principalColumn: "PolicyNo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Details_Notes_DebitNoteNo",
                        column: x => x.DebitNoteNo,
                        principalSchema: "agency",
                        principalTable: "Notes",
                        principalColumn: "NoteNo");
                });

            migrationBuilder.CreateTable(
                name: "Master",
                schema: "claim",
                columns: table => new
                {
                    NotifyNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PolicyNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    refDNCNNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UndYear = table.Column<int>(type: "int", nullable: false),
                    NotifyDate = table.Column<DateOnly>(type: "date", nullable: false),
                    LossDate = table.Column<DateOnly>(type: "date", nullable: false),
                    LossDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SumInsured = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Premium = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Outstanding = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RefReceiptNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DebitNoteNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartyID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Members_MarketerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Members_MarketerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    A1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Business_AccountingType = table.Column<byte>(type: "tinyint", nullable: false),
                    SubmittedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubmittedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprovedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovedUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Approval = table.Column<byte>(type: "tinyint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Master", x => x.NotifyNo);
                    table.ForeignKey(
                        name: "FK_Master_Notes_DebitNoteNo",
                        column: x => x.DebitNoteNo,
                        principalSchema: "agency",
                        principalTable: "Notes",
                        principalColumn: "NoteNo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Channels",
                schema: "common",
                columns: table => new
                {
                    ChannelID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BranchID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ChannelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AltName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Channels", x => x.ChannelID);
                    table.ForeignKey(
                        name: "FK_Channels_Branches_BranchID",
                        column: x => x.BranchID,
                        principalSchema: "common",
                        principalTable: "Branches",
                        principalColumn: "BranchID");
                });

            migrationBuilder.CreateTable(
                name: "Master",
                schema: "product",
                columns: table => new
                {
                    ProductID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MidRiskID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RiskID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AltName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NaicomCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AutoNumNextClaimNo = table.Column<long>(type: "bigint", nullable: false),
                    AutoNumNextNotifyNo = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Master", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_Master_MidRisks_MidRiskID",
                        column: x => x.MidRiskID,
                        principalSchema: "product",
                        principalTable: "MidRisks",
                        principalColumn: "MidRiskID");
                    table.ForeignKey(
                        name: "FK_Master_Risks_RiskID",
                        column: x => x.RiskID,
                        principalSchema: "product",
                        principalTable: "Risks",
                        principalColumn: "RiskID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubChannels",
                schema: "common",
                columns: table => new
                {
                    SubChannelID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ChannelID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SubChannelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AltName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubChannels", x => x.SubChannelID);
                    table.ForeignKey(
                        name: "FK_SubChannels_Channels_ChannelID",
                        column: x => x.ChannelID,
                        principalSchema: "common",
                        principalTable: "Channels",
                        principalColumn: "ChannelID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sections",
                schema: "product",
                columns: table => new
                {
                    ProductID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SectionID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SectionName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => new { x.ProductID, x.SectionID });
                    table.ForeignKey(
                        name: "FK_Sections_Master_ProductID",
                        column: x => x.ProductID,
                        principalSchema: "product",
                        principalTable: "Master",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sections",
                schema: "policy",
                columns: table => new
                {
                    DeclareNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SectionID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SN = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CertificateNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SumInsured = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Premium = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    A1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    A2 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    A3 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    A4 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    A5 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    A6 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    A7 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    A8 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    A9 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    A10 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    A11 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    A12 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    A13 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    A14 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    A15 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    A16 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    A17 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    A18 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    A19 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    A20 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    A21 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    A22 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    A23 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    A24 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    A25 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    A26 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    A27 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    A28 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    A29 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    A30 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    A31 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    A32 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    A33 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    A34 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    A35 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    A36 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    A37 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    A38 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    A39 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    A40 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    A41 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    A42 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    A43 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    A44 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    A45 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    A46 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    A47 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    A48 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    A49 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    A50 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Field1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Field2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Field3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Field4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Field5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Field6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Field7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Field8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Field9 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Field10 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Field11 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Field12 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Field13 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Field14 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Field15 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Field16 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Field17 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Field18 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Field19 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Field20 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Field21 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Field22 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Field23 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Field24 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Field25 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Field26 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Field27 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Field28 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Field29 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Field30 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => new { x.DeclareNo, x.SectionID });
                    table.ForeignKey(
                        name: "FK_Sections_Details_DeclareNo",
                        column: x => x.DeclareNo,
                        principalSchema: "policy",
                        principalTable: "Details",
                        principalColumn: "DeclareNo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sections_Sections_ProductID_SectionID",
                        columns: x => new { x.ProductID, x.SectionID },
                        principalSchema: "product",
                        principalTable: "Sections",
                        principalColumns: new[] { "ProductID", "SectionID" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SMIs",
                schema: "product",
                columns: table => new
                {
                    ProductID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SectionID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SmiID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SmiName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMIs", x => new { x.ProductID, x.SectionID, x.SmiID });
                    table.ForeignKey(
                        name: "FK_SMIs_Sections_ProductID_SectionID",
                        columns: x => new { x.ProductID, x.SectionID },
                        principalSchema: "product",
                        principalTable: "Sections",
                        principalColumns: new[] { "ProductID", "SectionID" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SMIs",
                schema: "policy",
                columns: table => new
                {
                    SectionID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SMIID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DeclareNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SN = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SMIName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PremiumRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalSumInsured = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPremium = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShareSumInsured = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SharePremium = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PolicyNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMIs", x => new { x.DeclareNo, x.SectionID, x.SMIID });
                    table.ForeignKey(
                        name: "FK_SMIs_SMIs_ProductID_SectionID_SMIID",
                        columns: x => new { x.ProductID, x.SectionID, x.SMIID },
                        principalSchema: "product",
                        principalTable: "SMIs",
                        principalColumns: new[] { "ProductID", "SectionID", "SmiID" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SMIs_Sections_DeclareNo_SectionID",
                        columns: x => new { x.DeclareNo, x.SectionID },
                        principalSchema: "policy",
                        principalTable: "Sections",
                        principalColumns: new[] { "DeclareNo", "SectionID" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_ControlID",
                schema: "account",
                table: "Accounts",
                column: "ControlID");

            migrationBuilder.CreateIndex(
                name: "IX_Branches_RegionID",
                schema: "common",
                table: "Branches",
                column: "RegionID");

            migrationBuilder.CreateIndex(
                name: "IX_Channels_BranchID",
                schema: "common",
                table: "Channels",
                column: "BranchID");

            migrationBuilder.CreateIndex(
                name: "IX_Details_DebitNoteNo",
                schema: "policy",
                table: "Details",
                column: "DebitNoteNo",
                unique: true,
                filter: "[DebitNoteNo] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Details_PolicyNo",
                schema: "policy",
                table: "Details",
                column: "PolicyNo");

            migrationBuilder.CreateIndex(
                name: "IX_FxRates_CurrencyID",
                schema: "account",
                table: "FxRates",
                column: "CurrencyID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupUsers_UserID",
                schema: "security",
                table: "GroupUsers",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Master_DebitNoteNo",
                schema: "claim",
                table: "Master",
                column: "DebitNoteNo");

            migrationBuilder.CreateIndex(
                name: "IX_Master_MidRiskID",
                schema: "product",
                table: "Master",
                column: "MidRiskID");

            migrationBuilder.CreateIndex(
                name: "IX_Master_RiskID",
                schema: "product",
                table: "Master",
                column: "RiskID");

            migrationBuilder.CreateIndex(
                name: "IX_MidRisks_RiskID",
                schema: "product",
                table: "MidRisks",
                column: "RiskID");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_PolicyNo",
                schema: "agency",
                table: "Notes",
                column: "PolicyNo");

            migrationBuilder.CreateIndex(
                name: "IX_Parties_PartyTypeID",
                schema: "agency",
                table: "Parties",
                column: "PartyTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionID",
                schema: "security",
                table: "RolePermissions",
                column: "PermissionID");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_ProductID_SectionID",
                schema: "policy",
                table: "Sections",
                columns: new[] { "ProductID", "SectionID" });

            migrationBuilder.CreateIndex(
                name: "IX_Signatures_BlobID",
                schema: "security",
                table: "Signatures",
                column: "BlobID");

            migrationBuilder.CreateIndex(
                name: "IX_Signatures_UserID",
                schema: "security",
                table: "Signatures",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_SMIs_ProductID_SectionID_SMIID",
                schema: "policy",
                table: "SMIs",
                columns: new[] { "ProductID", "SectionID", "SMIID" });

            migrationBuilder.CreateIndex(
                name: "IX_SubChannels_ChannelID",
                schema: "common",
                table: "SubChannels",
                column: "ChannelID");

            migrationBuilder.CreateIndex(
                name: "IX_Templates_BlobID",
                schema: "policy",
                table: "Templates",
                column: "BlobID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts",
                schema: "account");

            migrationBuilder.DropTable(
                name: "Approvals",
                schema: "security");

            migrationBuilder.DropTable(
                name: "AuditLog",
                schema: "security");

            migrationBuilder.DropTable(
                name: "AutoNumbers");

            migrationBuilder.DropTable(
                name: "Customers",
                schema: "agency");

            migrationBuilder.DropTable(
                name: "Fields",
                schema: "product");

            migrationBuilder.DropTable(
                name: "FxRates",
                schema: "account");

            migrationBuilder.DropTable(
                name: "GroupUsers",
                schema: "security");

            migrationBuilder.DropTable(
                name: "MarineClauses");

            migrationBuilder.DropTable(
                name: "Marketers",
                schema: "agency");

            migrationBuilder.DropTable(
                name: "Master",
                schema: "claim");

            migrationBuilder.DropTable(
                name: "MemoClauses");

            migrationBuilder.DropTable(
                name: "MotorClauses");

            migrationBuilder.DropTable(
                name: "Parties",
                schema: "agency");

            migrationBuilder.DropTable(
                name: "PolicyAutoNumbers");

            migrationBuilder.DropTable(
                name: "Rates",
                schema: "agency");

            migrationBuilder.DropTable(
                name: "RolePermissions",
                schema: "security");

            migrationBuilder.DropTable(
                name: "Settings",
                schema: "common");

            migrationBuilder.DropTable(
                name: "Signatures",
                schema: "security");

            migrationBuilder.DropTable(
                name: "SMIs",
                schema: "policy");

            migrationBuilder.DropTable(
                name: "SubChannels",
                schema: "common");

            migrationBuilder.DropTable(
                name: "Templates",
                schema: "policy");

            migrationBuilder.DropTable(
                name: "ControlAccounts",
                schema: "account");

            migrationBuilder.DropTable(
                name: "FxCurrencies",
                schema: "account");

            migrationBuilder.DropTable(
                name: "Groups",
                schema: "security");

            migrationBuilder.DropTable(
                name: "PartyTypes",
                schema: "agency");

            migrationBuilder.DropTable(
                name: "Permissions",
                schema: "security");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "security");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "security");

            migrationBuilder.DropTable(
                name: "SMIs",
                schema: "product");

            migrationBuilder.DropTable(
                name: "Sections",
                schema: "policy");

            migrationBuilder.DropTable(
                name: "Channels",
                schema: "common");

            migrationBuilder.DropTable(
                name: "Blobs",
                schema: "common");

            migrationBuilder.DropTable(
                name: "Details",
                schema: "policy");

            migrationBuilder.DropTable(
                name: "Sections",
                schema: "product");

            migrationBuilder.DropTable(
                name: "Branches",
                schema: "common");

            migrationBuilder.DropTable(
                name: "Notes",
                schema: "agency");

            migrationBuilder.DropTable(
                name: "Master",
                schema: "product");

            migrationBuilder.DropTable(
                name: "Regions",
                schema: "common");

            migrationBuilder.DropTable(
                name: "Master",
                schema: "policy");

            migrationBuilder.DropTable(
                name: "MidRisks",
                schema: "product");

            migrationBuilder.DropTable(
                name: "Risks",
                schema: "product");
        }
    }
}
