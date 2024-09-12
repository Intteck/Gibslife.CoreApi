namespace Gibs.Api
{
    public class Settings
    {
        public JWTOptions JWT { get; init; } = new();
        public SMTPOptions SMTP { get; init; } = new();
        public NaicomOptions Naicom { get; init; } = new();
        public ConnectionStringOptions ConnStrings { get; init; } = new();

        public class ConnectionStringOptions
        {
            public string ServiceBus { get; init; } = string.Empty;
            public string Storage { get; init; } = string.Empty;
            public string SqlDb { get; init; } = string.Empty;
        }

        public class SMTPOptions
        {
            public string ServerEndpoint { get; init; } = string.Empty;
            public string DisplayName { get; init; } = string.Empty;
            public string Username { get; init; } = string.Empty;
            public string Password { get; init; } = string.Empty;
            public bool UseSSL { get; init; } = false;
            public string FromAddress { get; init; } = string.Empty;
            public string SenderAddress { get; init; } = string.Empty;
            public string ReplyToAddress { get; init; } = string.Empty;
        }

        public class JWTOptions
        {
            public string Secret { get; init; } = string.Empty;
            public int ExpiresIn { get; init; } = 3600;
        }

        public class NaicomOptions
        {
            public string SID { get; init; } = string.Empty;
            public string Token { get; init; } = string.Empty;
            public bool IsTest { get; init; } = true;
        }

        public static class Headers
        {
            public const string PAGE_SIZE = "X-Page-Size";
            public const string PAGE_NUMBER = "X-Page-Number";
            public const string TOTAL_PAGES = "X-Total-Pages";
            public const string TOTAL_ITEMS = "X-Total-Count";
        }
    }
}
