using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using SMBTools.Web.BLL.Settings;

namespace SMBTools.Web.BLL.Helpers
{
    public class AuthenticationHelper
    {
        private const int IterationCount = 100000;
        private const char HashSeparator = ':';
        private const int NumberOfComponentsInHashedPassword = 2;
        private const int IndexOfPassword = 0;
        private const int IndexOfSalt = 1;

        private readonly IOptions<JwtSettings> _tokenSettings;

        public AuthenticationHelper(IOptions<JwtSettings> tokenSettings)
        {
            _tokenSettings = tokenSettings;
        }

        public ClaimsIdentity CreateClaimsIdentity(List<Claim> claims)
        {
            if (claims != null && claims.Any())
            {
                var claimsIdentity = new ClaimsIdentity(claims, JwtConstants.TokenType, ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

                return claimsIdentity;
            }
            return null;
        }

        internal string Hash(string password, byte[] salt = null, bool needsOnlyHash = false)
        {
            if (salt is null)
            {
                salt = new byte[128 / 8];
                using (var rngCsp = new RNGCryptoServiceProvider())
                {
                    rngCsp.GetNonZeroBytes(salt);
                }
            }

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: IterationCount,
                numBytesRequested: 256 / 8));

            if (needsOnlyHash)
            {
                return hashed;
            }

            return $"{hashed}{HashSeparator}{Convert.ToBase64String(salt)}";
        }

        internal bool Verify(string hashedPasswordWithSalt, string password)
        {
            var passwordAndHash = hashedPasswordWithSalt.Split(HashSeparator);

            if (passwordAndHash.Length != NumberOfComponentsInHashedPassword)
            {
                return false;
            }

            var salt = Convert.FromBase64String(passwordAndHash[IndexOfSalt]);

            var passwordHash = Hash(password, salt, true);

            if (string.Compare(passwordAndHash[IndexOfPassword], passwordHash) == 0)
            {
                return true;
            }

            return false;
        }
    }
}
