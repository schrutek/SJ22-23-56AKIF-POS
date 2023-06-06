using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using Spg.SpengerShop.Application.Helpers;
using Spg.SpengerShop.Domain.Dtos;
using Spg.SpengerShop.Domain.Interfaces;
using Spg.SpengerShop.Domain.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;

namespace Spg.SpengerShop.MvcFrontEnd.Services
{
    public class MvcApiAuthService
    {
        private const string _salt = "5Snh3qZNODtDd2Ibsj7irayIl6E1WWmpbvXtcSGlm1o=";
        private readonly byte[] _secret = new byte[0];
        private readonly IAuthService _authService;

        /// <summary>
        /// Konstruktor für die Verwendung ohne JWT.
        /// </summary>
        public MvcApiAuthService(IAuthService authService)
        {
            _authService = authService;
            _secret = Encoding.ASCII.GetBytes(_salt);
        }

        /// <summary>
        /// Konstruktor mit Secret für die Verwendung mit JWT.
        /// </summary>
        /// <param name="secret">Base64 codierter String für das Secret des JWT.</param>
        public MvcApiAuthService(string secret, IAuthService authService)
        {
            if (string.IsNullOrEmpty(secret))
            {
                throw new ArgumentException("Secret is null or empty.", nameof(secret));
            }
            _secret = Convert.FromBase64String(secret);
            _authService = authService;
        }

        /// <summary>
        /// Erstellt einen neuen Benutzer in der Datenbank. Dafür wird ein Salt generiert und der
        /// Hash des Passwortes berechnet.
        /// Wird eine PupilId übergeben, so wird die Rolle "Pupil" zugewiesen, ansonsten "Teacher".
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns></returns>
        /*
        public async Task<User> CreateUser(UserCredentials credentials)
        {
            string salt = GenerateRandom();
            // Den neuen Userdatensatz erstellen
            User newUser = new User
            {
                U_Name = credentials.Username,
                U_Salt = salt,
                U_Hash = CalculateHash(credentials.Password, salt),
            };
            // Die Rolle des Users zuweisen
            newUser.U_Role = "";
            db.Entry(newUser).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            await db.SaveChangesAsync();
            return newUser;
        }
        */

        public string GenerateToken(LoginDto credentials)
        {
            return GenerateToken(credentials, TimeSpan.FromDays(7));
        }

        /// <summary>
        /// Generiert den JSON Web Token für den übergebenen User.
        /// </summary>
        /// <param name="credentials">Userdaten, die in den Token codiert werden sollen.</param>
        /// <returns>
        /// JSON Web Token, wenn der User Authentifiziert werden konnte. 
        /// Null wenn der Benutzer nicht gefunden wurde.
        /// </returns>
        public string GenerateToken(LoginDto credentials, TimeSpan lifetime)
        {
            if (credentials is null) { throw new ArgumentNullException(nameof(credentials)); }

            UserInformationDto authInfos = new UserInformationDto();
            //try
            //{
            //    authInfos = CheckUser(credentials);
            //}
            //catch (AuthenticationException) { throw; }

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                // Payload für den JWT.
                Subject = new ClaimsIdentity(new Claim[]
                {
                    // Benutzername als Typ ClaimTypes.Name.
                    new Claim(ClaimTypes.Name, credentials.UserName),
                    new Claim(ClaimTypes.Surname, authInfos.FirstName),
                    new Claim(ClaimTypes.GivenName, authInfos.LastName),
                    // Rolle des Benutzer als ClaimTypes.DefaultRoleClaimType
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, authInfos.Role),
                }),
                Expires = DateTime.UtcNow + lifetime,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(_secret),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// Erstellt für den User ein ClaimsIdentity Objekt, wenn der User angemeldet werden konnte.
        /// </summary>
        /// <param name="credentials">Username und Passwort, welches geprüft werden soll.</param>
        /// <returns></returns>
        public ClaimsIdentity GenerateIdentity(LoginDto credentials)
        {
            UserInformationDto authInfos = new UserInformationDto();
            //try
            //{
            //    authInfos = CheckUser(credentials);
            //}
            //catch (AuthenticationException) { throw; }

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, credentials.UserName),
                new Claim(ClaimTypes.Surname, authInfos.FirstName),
                new Claim(ClaimTypes.GivenName, authInfos.LastName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, authInfos.Role),
                //...
            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                claims,
                Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme);
            return claimsIdentity;
        }

        public AuthenticationProperties GetAuthenticationProperties() 
        {
            return new AuthenticationProperties
            {
                //AllowRefresh = <bool>,
                // Refreshing the authentication session should be allowed.

                //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                // The time at which the authentication ticket expires. A 
                // value set here overrides the ExpireTimeSpan option of 
                // CookieAuthenticationOptions set with AddCookie.

                //IsPersistent = true,
                // Whether the authentication session is persisted across 
                // multiple requests. When used with cookies, controls
                // whether the cookie's lifetime is absolute (matching the
                // lifetime of the authentication ticket) or session-based.

                //IssuedUtc = <DateTimeOffset>,
                // The time at which the authentication ticket was issued.

                //RedirectUri = <string>
                // The full path or absolute URI to be used as an http 
                // redirect response value.
            };
        }
    }
}
