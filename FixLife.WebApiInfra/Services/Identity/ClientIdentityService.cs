using FixLife.WebApiDomain.Enums;
using FixLife.WebApiDomain.User;
using FixLife.WebApiInfra.Abstraction.Identity;
using FixLife.WebApiInfra.Common;
using FixLife.WebApiInfra.Common.Constants;
using FixLife.WebApiInfra.Contexts;
using FixLife.WebApiInfra.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ClaimsIdentity = System.Security.Claims.ClaimsIdentity;

namespace FixLife.WebApiInfra.Services.Identity
{
    public class ClientIdentityService : IClientIdentityService
    {
        private readonly IMongoContextFactory<IdentityContext> _context;
        private readonly IMongoContextFactory<ApplicationContext> _applicationContext;
        private JwtOptions _jwtOptions;
        public ClientIdentityService(IMongoContextFactory<IdentityContext> idContext, IMongoContextFactory<ApplicationContext> appContext, IOptions<JwtOptions> options)
        {
            _context = idContext;
            _applicationContext = appContext;
            _jwtOptions = options.Value;
        }

        public string UserId { get; private set; }

        public async Task<ClientIdentityResponse> AddOrLoginOAuthUserAsync(string email, OAuthLoginProvider oauthAccount)
        {
            using var idCtx = _context.CreateDbContext();
            using var appCtx = _applicationContext.CreateDbContext();

            ClientUser? clientUser = await idCtx.ClientUsers.FirstOrDefaultAsync(x => x.Email == email);

            if (clientUser == null)
            {
                clientUser = CreateNewUser(new ClientUser
                {
                    Id = ObjectId.GenerateNewId(),
                    Password = string.Empty,
                    Email = email,
                    PhoneNumber = string.Empty
                });

                await idCtx.ClientUsers.AddAsync(clientUser);

                var save = await idCtx.SaveChangesAsync();

                if (save <= 0)
                {
                    return new ClientIdentityResponse { Status = HttpCodes.NotFound, Details = "User not created" };
                }

            }

            var token = PrepareToken(clientUser);

            return new ClientIdentityResponse()
            {
                Status = HttpCodes.Ok,
                Details = "User logged!",
                Token = token,
                Email = clientUser.Email,
                HasPlans = appCtx.Plans.Any(d => d.UserId == clientUser.Id)
            };
        }

        public async Task<ClientUser> GetClientUser(string userId)
        {
            using var idCtx = _context.CreateDbContext();
            return await idCtx.ClientUsers.FindAsync(ObjectId.Parse(userId))
                ?? throw new RecordNotFoundException(userId, "ClientUser");
        }

        public async Task<ClientIdentityResponse> LoginAsync(ClientUser clientIdentityRequest)
        {
            using var idCtx = _context.CreateDbContext();
            using var appCtx = _applicationContext.CreateDbContext();

            var findUser = await idCtx.ClientUsers.FirstOrDefaultAsync(d => (d.Email == clientIdentityRequest.Email
            || d.PhoneNumber == clientIdentityRequest.PhoneNumber) && d.Password == clientIdentityRequest.Password);
            try
            {
                if (findUser != null)
                {

                    var token = PrepareToken(findUser);

                    var hasPlans = appCtx.Plans.Any(d => d.UserId == findUser.Id);

                    return new ClientIdentityResponse()
                    {
                        Status = HttpCodes.Ok,
                        Details = "User logged!",
                        Token = token,
                        Email = findUser.Email,
                        HasPlans = hasPlans
                    };

                }

                return new ClientIdentityResponse
                {
                    Status = HttpCodes.NotFound,
                    Details = "User not found in database!",
                    Token = null,
                };
            } catch(Exception)
            {
                return new ClientIdentityResponse
                {
                    Status = HttpCodes.InternalServerError,
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
            using var idCtx = _context.CreateDbContext();

            var newUser = CreateNewUser(request);

            await idCtx.ClientUsers.AddAsync(newUser);

            var save = await idCtx.SaveChangesAsync();

            if(save > 0)
            {
                return new ClientIdentityResponse { Status = HttpCodes.Ok, Details = "User created!" };
            }
            else
            {
                return new ClientIdentityResponse { Status = HttpCodes.NotFound, Details = "User not created" };
            }

        }

        private ClientUser CreateNewUser(ClientUser request) 
            => new ClientUser
        {
            Id = ObjectId.GenerateNewId(),
            Email = request.Email,
            Password = request.Password,
            PhoneNumber = request.PhoneNumber
        };

        private string PrepareToken(ClientUser user)
        {
            var issuer = _jwtOptions.Issuer;
            var audience = _jwtOptions.Audience;
            var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret)
                ?? throw new NullReferenceException("Error from loading JWT Secret, please check server config.");

            UserId = user.Id.ToString();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                            new Claim("Id", Guid.NewGuid().ToString()),
                            new Claim("UserId", user.Id.ToString()),
                            new Claim(JwtRegisteredClaimNames.Sub, user.PhoneNumber),
                            new Claim(JwtRegisteredClaimNames.Email, user.Email),
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

            return stringToken;
        }
    }
}
