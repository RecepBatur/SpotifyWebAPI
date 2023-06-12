using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class ClaimExtensions
    {
        public static void AddEmail(this ICollection<Claim> claims, string email)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, value: email));
        }
        public static void AddName(this ICollection<Claim> claims, string name)
        {
            claims.Add(new Claim(type: ClaimTypes.Name, value: name));
        }
        public static void AddNameIdentifier(this ICollection<Claim> claims, string nameIdentifier)
        {
            claims.Add(new Claim(type: ClaimTypes.NameIdentifier, value: nameIdentifier));
        }

        //roles array olarak parametre verdik.
        public static void AddRoles(this ICollection<Claim> claims, string[] roles)
        {
            //her bir rolü listeye çevir array olduğu için. Sonrasında her bir rolü sisteme claimlere tek tek ekle.
            roles.ToList().ForEach(role => claims.Add(new Claim(type: ClaimTypes.Role, value: role)));
        }
    }
}
