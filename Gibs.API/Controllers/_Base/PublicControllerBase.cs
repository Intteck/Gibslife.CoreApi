using System;
using System.Text;
using System.Security.Claims;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;
using Gibs.Domain.Entities;
using Gibs.Infrastructure;

namespace Gibs.Api.Controllers
{
    [ApiController]
    [ApiExceptionFilter]
    [Produces("application/json")]
    [ApiConventionType(typeof(MyAppConventions))]
    public abstract class PublicControllerBase : ControllerBase
    {
        protected ControllerServices Services { get; }
        protected GibsContext UoW { get; }

        public PublicControllerBase(ControllerServices services)
        {
            Services = services;
            UoW = services.DbContext;

            //set the GetLogonUser() delegate
            //UoW.GetLogonUser = () => GibsContext.SystemAccount;
        }

        public static string CreateJwtToken(User user, string secret, int expiresIn)
        {
            var key = Encoding.ASCII.GetBytes(secret);
            var claims = new List<Claim>
            {
                new(ClaimTypes.Sid, user.Id),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Name, user.FullName),
                new(ClaimTypes.Uri, user.AvatarUrl ?? ""),
            };

            var jwtToken = new JwtSecurityToken(
                claims:     claims,
                notBefore:  DateTime.UtcNow,
                expires:    DateTime.UtcNow.AddSeconds(expiresIn),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
                );
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

        //protected static string CreateJwtToken(User user, string secret, int expiresIn)
        //{
        //    ArgumentNullException.ThrowIfNull(user);

        //    var key = Encoding.ASCII.GetBytes(secret);
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new Claim[]
        //        {
        //            new(ClaimTypes.Email, user.UserID),
        //            new(ClaimTypes.Name, user.FullName),
        //            new(ClaimTypes.Hash, user.ApiKey),
        //            new(ClaimTypes.Surname, "GIBS"),
        //        }),

        //        //Subject = new ClaimsIdentity(new Claim[]
        //        //{
        //        //    new(ClaimTypes.Sid, user.UserID),  
        //        //    new(ClaimTypes.Email, user.Email),
        //        //    new(ClaimTypes.Name, user.FullName),
        //        //    new(ClaimTypes.Surname, user.LastName),
        //        //    new(ClaimTypes.GivenName, user.FirstName),
        //        //    new(ClaimTypes.Uri, user.AvatarUrl ?? string.Empty),
        //        //}),
        //        Expires = DateTime.UtcNow.AddSeconds(expiresIn),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
        //        SecurityAlgorithms.HmacSha256Signature)
        //    };
        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    return tokenHandler.WriteToken(token);
        //}
    }
}
