using Gibs.Api.Services;
using Gibs.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace Gibs.Api.Controllers
{
    public class ControllerServices(
                    IHttpContextAccessor accessor,
                    GibsContext dbContext,
                    Settings settings,
                    EmailService email/*,
                    NaicomService naicom,
                    DocumentService document*/)
    {
        public HttpContext? HttpContext { get; } = accessor.HttpContext;
        public GibsContext DbContext { get; } = dbContext;
        public Settings Settings { get; } = settings;
        public EmailService Email { get; } = email;
        //public NaicomService Naicom { get; } = naicom;
        //public DocumentService Document { get; } = document;
    }
}
