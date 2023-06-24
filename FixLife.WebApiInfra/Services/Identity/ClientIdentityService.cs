using FixLife.WebApiDomain.User;
using FixLife.WebApiInfra.Abstraction.Identity;
using FixLife.WebApiInfra.Common;
using FixLife.WebApiInfra.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.WebApiInfra.Services.Identity
{
    public class ClientIdentityService : IClientIdentityService
    {
        private readonly IdentityContext _context;
        private readonly ApplicationContext _applicationContext;
        private JwtOptions _jwtOptions;
        public ClientIdentityService(IdentityContext context, ApplicationContext appContext, IOptions<JwtOptions> options)
        {
            _context = context;
            _applicationContext = appContext;
            _jwtOptions = options.Value;
        }

        public Guid UserId { get; private set; }

        public async Task<ClientUser> GetClientUser(string userId)
        {
            return await _context.ClientUsers.SingleOrDefaultAsync(a => a.Id == Guid.Parse(userId));
        }

        public async Task<ClientIdentityResponse> LoginAsync(ClientUser clientIdentityRequest)
        {
            var findUser = await _context.ClientUsers.Where(d => (d.Email == clientIdentityRequest.Email
            || d.PhoneNumber == clientIdentityRequest.PhoneNumber) && d.Password == clientIdentityRequest.Password).FirstOrDefaultAsync();
            try
            {
                if (findUser != null)
                {
                    var issuer = _jwtOptions.Issuer;
                    var audience = _jwtOptions.Audience;
                    var key = Encoding.ASCII.GetBytes
                    (_jwtOptions.Secret);
                    UserId = findUser.Id;
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new System.Security.Claims.ClaimsIdentity(new[]
                        {
                            new Claim("Id", Guid.NewGuid().ToString()),
                            new Claim("UserId", findUser.Id.ToString()),
                            new Claim(JwtRegisteredClaimNames.Sub, findUser.PhoneNumber),
                            new Claim(JwtRegisteredClaimNames.Email, findUser.Email),
                            new Claim(JwtRegisteredClaimNames.Jti,
                            Guid.NewGuid().ToString())
                        }),
                        Expires = DateTime.UtcNow.AddDays(14),
                        Issuer = issuer,
                        Audience = audience,
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
                    };
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var jwtToken = tokenHandler.WriteToken(token);
                    var stringToken = tokenHandler.WriteToken(token);

                    var hasPlans = _applicationContext.UserPlan.Any(d => d.Users == findUser);

                    return new ClientIdentityResponse()
                    {
                        Status = 200,
                        Details = "User logged!",
                        Token = stringToken,
                        Email = findUser.Email,
                        HasPlans = hasPlans
                    };

                }

                return new ClientIdentityResponse
                {
                    Status = 404,
                    Details = "User not found in database!",
                    Token = null,
                };
            } catch(Exception ex)
            {
                return new ClientIdentityResponse
                {
                    Status = 500,
                    Details = "There's a something problem with request.",
                    Token = null
                };
            }

        }

        public Task<ClientIdentityResponse> LogoutAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ClientIdentityResponse> RegisterAsync(ClientUser request)
        {
            var newUser = new ClientUser
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                Password = request.Password,
                PhoneNumber = request.PhoneNumber
            };

            await _context.ClientUsers.AddAsync(newUser);

            var save = await _context.SaveChangesAsync();

            if(save > 0)
            {
                return new ClientIdentityResponse { Status = 200, Details = "User created!" };
            }
            else
            {
                return new ClientIdentityResponse { Status = 400, Details = "User not created" };
            }

        }
    }
}
