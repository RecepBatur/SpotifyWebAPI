using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Hashing
{
    public class HashingHelper
    {
        // out:parametleri gönderdiğimizde değişen nesne byte array'ımıza aktarmasını sağlıyor.

        //hashing oluşturma operasyonumuzu yazdık.
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                //bir tane passwordSalt vasıtasıyla ComputeHash oluşturacak ve bunu Key olarak passwordSalt'a atayacak.
                //passwordHash'e de oluşan hashi vermiş olduk.
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(buffer: Encoding.UTF8.GetBytes(password));
            }
        }
        //Password hashini doğrulama.
        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(buffer: Encoding.UTF8.GetBytes(password));
                //iki hashi karşılaştırıyoruz for döngüsü ile.
                for (int i = 0; i < computedHash.Length; i++) 
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
