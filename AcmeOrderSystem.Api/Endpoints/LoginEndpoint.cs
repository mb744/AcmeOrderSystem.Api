using AcmeOrderSystem.Api.Contracts;
using AcmeOrderSystem.Api.Entities;
using AcmeOrderSystem.Api.Services;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AcmeOrderSystem.Api.Endpoints
{
    public static class LoginEndpoint
    {

        public static void MapLoginEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapPost("login", async (
                IValidator<Login> validator,
                Login login,
                IConfiguration _configuration
                ) =>
            {
                var validationResult = await validator.ValidateAsync(login);
                if (!validationResult.IsValid)
                {
                    return Results.ValidationProblem(validationResult.ToDictionary());
                }

                LoginResponse response = new() { Username = login.Username };
                string issuer = _configuration.GetValue<string>("Authentication:Schemes:Bearer:ValidIssuer");
                string audience = _configuration.GetValue<string>("Authentication:Schemes:Bearer:ValidAudiences");
                byte[] key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("Authentication:Schemes:Bearer:ValidSecretKey"));


                if (login.Username == "acme" && login.Password == "acme123")
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenDescriptor = new SecurityTokenDescriptor()
                    {
                        Issuer = issuer,
                        Audience = audience,
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                        //Username
                        new Claim(ClaimTypes.Name, login.Username),
                        //Role
                        new Claim(ClaimTypes.Role, "admin")
                        }),
                        Expires = DateTime.Now.AddHours(4),
                        SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    response.token = tokenHandler.WriteToken(token);
                }
                else
                {
                    return Results.Ok("Invalid username and password");
                }
                return Results.Ok(response);
            }).WithTags("Login");

            
        }

    }
}
