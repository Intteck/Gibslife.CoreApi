//using System;
//using System.Diagnostics;
//using GibsPro.Entities;
//using Microsoft.EntityFrameworkCore;

//namespace GibsPro.DataAccess
//{
//    public class SettingRepository : ReadOnlyRepository<Setting>
//    {
//        public SettingRepository(GibsContext context) : base(context)
//        {
//        }

//        public SettingObject AsObject()
//        {
//            return new SettingObject(this.Table);
//        }
//    }

//    public class SettingObject
//    {
//        public SmsSetting SMS { get; set; } = new SmsSetting();
//        public EmailSetting Email { get; set; } = new EmailSetting();
//        public AccountsSetting Accounts { get; set; } = new AccountsSetting();
//        public SecuritySetting Security { get; set; } = new SecuritySetting();

//        public string Abbreviation { get; set; } = "GEC";
//        public string Company { get; set; } = "Gibs Example Company";
//        public string Address { get; set; } = "5 Gibs Example Address Street, Ikoyi, Lagos";
//        public string Website { get; set; } = "home.example.com";

//        public SettingObject(DbSet<Setting> Source = null)
//        {
//            if (Source != null)
//                LoadProperties(null, this, Source);
//        }

//        public void SaveTo(DbSet<Setting> Dest)
//        {
//            SaveProperties(null, this, Dest);
//        }

//        public class AccountsSetting
//        {
//            public static DateTime LastReconciliationDate { get; set; } = DateTime.MinValue;
//            public static bool ClosePeriodPermanently { get; set; } = false;
//            public static bool LockLedgerTransSuppress { get; set; } = false;
//        }

//        public class SecuritySetting
//        {
//            public static int MaxLoginAttempts { get; set; } = 5;
//            public static int MinPasswordLength { get; set; } = 6;
//            public static int InitialPasswordDays { get; set; } = 2;
//            public static int ChangePasswordDays { get; set; } = 60;
//            public static int DisableDays { get; set; } = 0; // 0 = infinite
//        }

//        public class EmailSetting
//        {
//            public enum EmailProtocolEnum : byte
//            {
//                NONE = 0,
//                SMTP = 1
//            }

//            public class SmtpSetting
//            {
//                public static string Username { get; set; } = "";
//                public static string Password { get; set; } = "";
//                public static string ServerIp { get; set; } = "127.0.0.1";
//                public static int ServerPort { get; set; } = 25;
//                public static string FriendlyName { get; set; } = "Gibs Example";
//            }

//            public static EmailProtocolEnum Protocol { get; set; } = EmailProtocolEnum.SMTP;
//            public static SmtpSetting Smtp { get; set; } = new SmtpSetting();
//        }

//        public class SmsSetting
//        {
//            public enum SmsProtocolEnum : byte
//            {
//                NONE = 0,
//                HTTP = 1,
//                SMPP = 2
//            }

//            public class HttpSetting
//            {
//                public static string ApiPath { get; set; } = "http://api.smslive247.com/http/index.aspx";
//                public static string ApiAuthParam { get; set; } = "apikey=[key-here]"; // or "username=[]&password=[]"
//                public static string ApiParams { get; set; } = "destination={DEST}&message={MESSAGE}&senderid={SENDER}";
//            }

//            public class SmppSetting
//            {
//                public static string SystemId { get; set; } = "";
//                public static string Password { get; set; } = "";
//                public static string SytemType { get; set; } = "";
//                public static string ServerIp { get; set; } = "127.0.0.1";
//                public static int ServerPort { get; set; } = 8888;
//            }

//            public static string SenderId { get; set; } = "GibsExample";
//            public static SmsProtocolEnum Protocol { get; set; } = SmsProtocolEnum.HTTP;
//            public static HttpSetting Http { get; set; } = new HttpSetting();
//            public static SmppSetting Smpp { get; set; } = new SmppSetting();
//        }

//        private void LoadProperties(string ParentName, object obj, DbSet<Setting> Source)
//        {
//            var objType = obj.GetType();
//            var Properties = objType.GetProperties();

//            foreach (var P in Properties)
//            {
//                var FqName = string.IsNullOrEmpty(ParentName) ? P.Name : ParentName + "." + P.Name;
//                var pValue = P.GetValue(obj);
//                // Dim pItems = TryCast(pValue, IList)

//                // If pItems IsNot Nothing Then
//                // For Each i In pItems
//                // LoadProperties(i, Source)
//                // Next
//                // Else
//                if (P.PropertyType.Assembly == objType.Assembly)
//                    LoadProperties(FqName, pValue, Source);
//                else
//                {
//                    var A = Source.Find(FqName);

//                    if (A != null)
//                        P.SetValue(obj, Convert.ChangeType(A.Value, P.PropertyType));
//                }
//            }
//        }

//        private void SaveProperties(string ParentName, object obj, DbSet<Setting> Dest)
//        {
//            var objType = obj.GetType();
//            var Properties = objType.GetProperties();

//            foreach (var P in Properties)
//            {
//                var FqName = string.IsNullOrEmpty(ParentName) ? P.Name : ParentName + "." + P.Name;
//                var pValue = P.GetValue(obj).ToString();
//                // Dim pItems = TryCast(pValue, IList)

//                // If pItems IsNot Nothing Then
//                // For Each i In pItems
//                // SaveProperties(i, Dest)
//                // Next
//                // Else
//                if (P.PropertyType.Assembly == objType.Assembly)
//                    SaveProperties(FqName, pValue, Dest);
//                else
//                {
//                    Debug.WriteLine(FqName);

//                    var A = Dest.Find(FqName);

//                    if (A == null)
//                    {
//                        Setting B = new Setting(FqName, P.Name.ToSentenceCase(), pValue);
//                        Dest.Add(B);
//                    }
//                    else
//                        A.UpdateValue(pValue, P.Name.ToSentenceCase());
//                }
//            }
//        }
//    }
//}