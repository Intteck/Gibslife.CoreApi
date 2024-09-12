using System;
using System.Diagnostics;
using GibsPro.Entities;
using Microsoft.EntityFrameworkCore;

namespace GibsPro.DataAccess
{
    //public class BizLogic
    //{
    //    private readonly GibsContext db;

    //    public enum LoginResultEnum
    //    {
    //        SUCCESS,
    //        DENIED_USERID,
    //        DENIED_PASSWORD,
    //        DENIED_LOCKED
    //    }

    //    public BizLogic(GibsContext context)
    //    {
    //        db = context;
    //    }

    //    public User Login(string userId, string password, string ipAddress)
    //    {
    //        var user = db.Users.Find(userId);

    //        if (user != null)
    //        {
    //            if (user.Password != password)
    //            {
    //                AppendLoginAuditLog(LoginResultEnum.DENIED_PASSWORD, userId, ipAddress);
    //                throw new Exception("Your Password is incorrect");
    //            }
    //            else if (user.Active == false)
    //            {
    //                AppendLoginAuditLog(LoginResultEnum.DENIED_LOCKED, userId, ipAddress);
    //                throw new Exception("Your Account is locked");
    //            }
    //            else
    //            {
    //                AppendLoginAuditLog(LoginResultEnum.SUCCESS, userId, ipAddress);
    //                return user;
    //            }
    //        }
    //        else
    //        {
    //            AppendLoginAuditLog(LoginResultEnum.DENIED_USERID, userId, ipAddress);
    //            throw new Exception("This User does not exist");
    //        }
    //    }

    //    private void AppendLoginAuditLog(LoginResultEnum loginResult, string userId, string ipAddress)
    //    {
    //        AuditLog LogItem = new AuditLog(userId, "Users", loginResult.ToString(), AuditActionEnum.LOGIN, "~", ipAddress);
    //        db.AuditLogs.Add(LogItem);
    //    }

    //    public SettingObject Settings
    //    {
    //        get
    //        {
    //            return new SettingObject(db.Settings);
    //        }
    //    }
    //}

    //public class SettingObject
    //{
    //    public SmsSetting SMS { get; set; } = new SmsSetting();
    //    public EmailSetting Email { get; set; } = new EmailSetting();
    //    public AccountsSetting Accounts { get; set; } = new AccountsSetting();
    //    public SecuritySetting Security { get; set; } = new SecuritySetting();

    //    public string Abbreviation { get; set; } = "GEC";
    //    public string Company { get; set; } = "Gibs Example Company";
    //    public string Address { get; set; } = "5 Gibs Example Address Street, Ikoyi, Lagos";
    //    public string Website { get; set; } = "home.example.com";

    //    private SettingObject()
    //    {
    //    }

    //    public SettingObject(DbSet<Setting> source)
    //    {
    //        LoadProperties(null, this, source);
    //    }

    //    public void SaveTo(DbSet<Setting> destination)
    //    {
    //        SaveProperties(null, this, destination);
    //    }

    //    public class AccountsSetting
    //    {
    //        public static DateTime LastReconciliationDate { get; set; } = DateTime.MinValue;
    //        public static bool ClosePeriodPermanently { get; set; } = false;
    //        public static bool LockLedgerTransSuppress { get; set; } = false;
    //    }

    //    public class SecuritySetting
    //    {
    //        public static int MaxLoginAttempts { get; set; } = 5;
    //        public static int MinPasswordLength { get; set; } = 6;
    //        public static int InitialPasswordDays { get; set; } = 2;
    //        public static int ChangePasswordDays { get; set; } = 60;
    //        public static int DisableDays { get; set; } = 0; // 0 = infinite
    //    }

    //    public class EmailSetting
    //    {
    //        public enum EmailProtocolEnum : byte
    //        {
    //            NONE = 0,
    //            SMTP = 1
    //        }

    //        public class SmtpSetting
    //        {
    //            public static string Username { get; set; } = "";
    //            public static string Password { get; set; } = "";
    //            public static string ServerIp { get; set; } = "127.0.0.1";
    //            public static int ServerPort { get; set; } = 25;
    //            public static string FriendlyName { get; set; } = "Gibs Example";
    //        }

    //        public static EmailProtocolEnum Protocol { get; set; } = EmailProtocolEnum.SMTP;
    //        public static SmtpSetting Smtp { get; set; } = new SmtpSetting();
    //    }

    //    public class SmsSetting
    //    {
    //        public enum SmsProtocolEnum : byte
    //        {
    //            NONE = 0,
    //            HTTP = 1,
    //            SMPP = 2
    //        }

    //        public class HttpSetting
    //        {
    //            public static string ApiPath { get; set; } = "http://api.smslive247.com/http/index.aspx";
    //            public static string ApiAuthParam { get; set; } = "apikey=[key-here]"; // or "username=[]&password=[]"
    //            public static string ApiParams { get; set; } = "destination={DEST}&message={MESSAGE}&senderid={SENDER}";
    //        }

    //        public class SmppSetting
    //        {
    //            public static string SystemId { get; set; } = "";
    //            public static string Password { get; set; } = "";
    //            public static string SytemType { get; set; } = "";
    //            public static string ServerIp { get; set; } = "127.0.0.1";
    //            public static int ServerPort { get; set; } = 8888;
    //        }

    //        public static string SenderId { get; set; } = "GibsExample";
    //        public static SmsProtocolEnum Protocol { get; set; } = SmsProtocolEnum.HTTP;
    //        public static HttpSetting Http { get; set; } = new HttpSetting();
    //        public static SmppSetting Smpp { get; set; } = new SmppSetting();
    //    }

    //    private void LoadProperties(string parentName, object obj, DbSet<Setting> table)
    //    {
    //        var objType = obj.GetType();
    //        var properties = objType.GetProperties();

    //        foreach (var prop in properties)
    //        {
    //            var settingName = string.IsNullOrEmpty(parentName) ? prop.Name : parentName + "." + prop.Name;
    //            var value = prop.GetValue(obj);

    //            if (prop.PropertyType.Assembly == objType.Assembly)
    //                LoadProperties(settingName, value, table);
    //            else
    //            {
    //                var row = table.Find(settingName);

    //                if (row != null)
    //                    prop.SetValue(obj, Convert.ChangeType(row.Value, prop.PropertyType));
    //            }
    //        }
    //    }

    //    private void SaveProperties(string parentName, object obj, DbSet<Setting> table)
    //    {
    //        var objType = obj.GetType();
    //        var properties = objType.GetProperties();

    //        foreach (var prop in properties)
    //        {
    //            var settingName = string.IsNullOrEmpty(parentName) ? prop.Name : parentName + "." + prop.Name;
    //            var value = prop.GetValue(obj);

    //            if (prop.PropertyType.Assembly == objType.Assembly)
    //                SaveProperties(settingName, value, table);
    //            else
    //            {
    //                var row = table.Find(settingName);

    //                if (row == null)
    //                    table.Add(new Setting(settingName, prop.Name.ToSentenceCase(), value.ToString()));
    //                else
    //                    row.UpdateValue(value.ToString(), prop.Name.ToSentenceCase());
    //            }
    //        }
    //    }

    //    public static SettingObject FactoryCreate()
    //    {
    //        return new SettingObject();
    //    }
    //}
}