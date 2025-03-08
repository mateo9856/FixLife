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

        public Task<ClientIdentityResponse> AddOrLoginOAuthUserAsync(string email, OAuthLoginProvider oauthAccount)
        {
            //TODO: Implement this method
            throw new NotImplementedException();
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
                    var issuer = _jwtOptions.Issuer;
                    var audience = _jwtOptions.Audience;
                    var key = Encoding.ASCII.GetBytes
                    (_jwtOptions.Secret);
                    UserId = findUser.Id.ToString();
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new[]
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

                    var hasPlans = appCtx.Plans.Any(d => d.UserId == findUser.Id);

                    return new ClientIdentityResponse()
                    {
                        Status = HttpCodes.Ok,
                        Details = "User logged!",
                        Token = stringToken,
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

            var newUser = new ClientUser
            {
                Id = ObjectId.GenerateNewId(),
                Email = request.Email,
                Password = request.Password,
                PhoneNumber = request.PhoneNumber
            };

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
    }
}
