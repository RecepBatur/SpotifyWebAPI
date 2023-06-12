using Core.Entities.Concrete;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Core.Utilities.Security.Enycption;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Core.Extensions;
using IdentityServer3.Core.Services;

namespace Core.Utilities.Security.Jwt
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; }
        private TokenOptions _tokenOptions;
        private DateTime _accessTokenExpiration;
        //private IUserService _userService;
        //private IUserDal _userDal;
        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            //Configuration dosyasında yani WebAPI içindeki appsettings.json içinde "TokenOptions" oku dedik. Ve TokenOptions içerisine aktardık.
            _tokenOptions = Configuration.GetSection(key: "TokenOptions").Get<TokenOptions>();
            //_accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            //_userService = userService;
        }

        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey); //securityKey oluşturduk.
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey); //giriş
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt); //tokeni yazdırdık.

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };
        }


        //token döndüren bir operasyon yazıyoruz.
        //Parametre olarak; bir token istedik. Kullanıcı istedik. signingCredentials istedik. Ve son olarak kullanıcınıın sahip olduğu OperationClaim'leri istedik.
        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user, SigningCredentials signingCredentials, List<OperationClaim> operationClaims)
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now, //Eğer tokken'ın Expiration bilgisi şu andan önce ise geçerli değil. 
                claims: SetClaims(user, operationClaims),
                signingCredentials: signingCredentials

                );
            return jwt;
        }

        private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddEmail(user.Email);
            claims.AddName($"{user.FirstName} {user.LastName}");
            claims.AddRoles(operationClaims.Select(c => c.Name).ToArray());
            return claims;
        }
        //public bool ValidateToken(string token)
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes(_tokenOptions.SecurityKey);
        //    var validationParameters = new TokenValidationParameters
        //    {
        //        ValidateIssuerSigningKey = true,
        //        IssuerSigningKey = new SymmetricSecurityKey(key),
        //        ValidateIssuer = true,
        //        ValidIssuer = _tokenOptions.Issuer,
        //        ValidateAudience = true,
        //        ValidAudience = _tokenOptions.Audience,
        //        ValidateLifetime = true,
        //        ClockSkew = TimeSpan.Zero
        //    };

        //    SecurityToken validatedToken;
        //    var principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);

        //    return true;
        //}

        public int GetAuthenticatedUser(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_tokenOptions.SecurityKey);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = int.Parse(jwtToken.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            // userId kullanarak User nesnesini veritabanından çekebilirsiniz
            return userId;
        }
    }


}
