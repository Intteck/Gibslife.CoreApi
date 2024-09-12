using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Gibs.Api.Contracts.V1;

namespace Gibs.Api.Controllers
{
    [Route("api/auth")]
    public class AuthController(ControllerServices services) : PublicControllerBase(services)
    {
        [HttpPost, AllowAnonymous]
        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var user = await UoW.Users.Where(x => x.Id == request.Username)
                                      .FirstOrDefaultAsync();
            if (user == null)
                throw new UnauthorizedException("User does not exist");

            if (!user.IsPasswordValid(request.Password))
                throw new UnauthorizedException("Password is incorrect");

            if (!user.IsActive)
                throw new UnauthorizedException("User was deactivated. Contact your Admin");

            if (user.IsPasswordExpired)
                throw new UnauthorizedException("Password has expired. Contact your Admin");

            var jwt = Services.Settings.JWT;
            var token = CreateJwtToken(user, jwt.Secret, jwt.ExpiresIn);

            var response = new LoginResponse(token, jwt.ExpiresIn);

            user.UpdateLastLoginDate();
            await UoW.SaveChangesAsync();
            return response;
        }

        [HttpPost("reset")]
        public async Task ResetPassword(LoginRequest request)
        {
            var user = await UoW.Users.FindAsync(request.Username)
               ?? throw new NotFoundException($"User [{request.Username}] was not found");

            var newPassword = user.ResetPassword(TimeSpan.FromDays(60));

            await UoW.SaveChangesAsync();

            await SendResetEmail(user.Email, newPassword);
        }

        private Task SendResetEmail(string email, string newPassword)
        {
            var mailMessage = services.Email.CreateEmailMessage(email);

            mailMessage.Subject = "Password Reset";
            mailMessage.Body =  $"Your new password {newPassword}";

            return services.Email.SendEmailMessage(mailMessage);
        }

        [HttpDelete("current")]
        public void Logout()
        {
            //destroy the token
            //return Ok();
        }
    }
}
