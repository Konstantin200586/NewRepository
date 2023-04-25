using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SMBTools.Contract.Enums;
using SMBTools.Web.BLL.Helpers;
using SMBTools.Web.BLL.Services.Interfaces;
using SMBTools.Web.Common.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SMBTools.Contract.Filters;
using SMBTools.Contract.Models.Requests;
using SMBTools.Web.BLL.Settings;
using SMBTools.Web.DAL.Repositories.Interfaces;
using SMBTools.Web.DAL.DataModels;
using SMBTools.Contract.Models.Responses;
using SMBTools.Web.BLL.Models;

namespace SMBTools.Web.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly AuthenticationHelper _authenticationHelper;
        private readonly IAccountRepository _accountRepository;
        private readonly JwtSettings _tokenSettings;

        public AuthService(IOptions<JwtSettings> tokenSettings,
            AuthenticationHelper authenticationHelper,
            IAccountRepository accountRepository)
        {
            _authenticationHelper = authenticationHelper;
            _accountRepository = accountRepository;
            _tokenSettings = tokenSettings.Value;
        }

        public async Task<LoginResponseDto> GetUserTokenAsync(LoginModel loginModel)
        {
            var account = await _accountRepository.GetOneByFilterAsync<AccountDataModel>(new AccountFilter {Email = loginModel.Email});

            if (account == null)
            {
                throw new UserCredentialIsNotValidException();
            }

            var isValidCredentials = _authenticationHelper.Verify(account.Password, loginModel.Password);

            if (!isValidCredentials)
            {
                throw new UserCredentialIsNotValidException();
            }

            var claims = CreateClaimsForUser(account);

            var response = CreateToken(account.Id, account.Role, claims);

            return response;
        }

        public LoginResponseDto RefreshToken(string refreshToken, string oldAccessToken)
        {
            var userIdFromToken = GetUserIdFromAccessToken(oldAccessToken);

            var claims = ReadToken(oldAccessToken)?.Claims?.ToList() ?? new List<Claim>();

            if (claims == null || claims.Count == 0)
            {
                throw new IncorrectRefreshTokenException();
            }

            var roleTypeClaims = claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role)?.Value;

            if (string.IsNullOrEmpty(roleTypeClaims) ||
                !Enum.TryParse(typeof(UserRole), roleTypeClaims, true, out var roleType))
            {
                throw new IncorrectRefreshTokenException();
            }

            var response = CreateToken(userIdFromToken, (UserRole)roleType, claims);

            return response;
        }

        private LoginResponseDto CreateToken(Guid userId, UserRole roleType, List<Claim> claims)
        {
            var identity = _authenticationHelper.CreateClaimsIdentity(claims);

            if (identity == null)
            {
                throw new UserCredentialIsNotValidException();
            }

            var accessToken = CreateAccessToken(identity);

            var token = new LoginResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = Guid.NewGuid().ToString("N"),
                RoleType = roleType
            };

            return token;
        }

        private List<Claim> CreateClaimsForUser(AccountDataModel accountDataModel)
        {
            return new List<Claim>
            {
                new Claim(ClaimTypes.PrimarySid, accountDataModel.Id.ToString()),
                new Claim(ClaimTypes.Email, accountDataModel.Email),
                new Claim(ClaimTypes.Role, accountDataModel.Role.ToString()),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, accountDataModel.Role.ToString())
            };
        }

        private Guid GetUserIdFromAccessToken(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(accessToken);
            var userId = jwtToken.Claims.First(claim => claim.Type == ClaimTypes.PrimarySid).Value;
            return Guid.Parse(userId);
        }

        private JwtSecurityToken ReadToken(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            return handler.ReadJwtToken(accessToken);
        }

        private string CreateAccessToken(ClaimsIdentity identity)
        {
            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                issuer: _tokenSettings.Issuer,
                audience: _tokenSettings.Audience,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(_tokenSettings.ExpirationMinutes)),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII
                    .GetBytes(_tokenSettings.SymmetricSecurityKey)),
                    SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
