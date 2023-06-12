using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Jwt
{
    public interface ITokenHelper
    {
        //user bilgisini ve user rollerini gönderiyoruz. Bu bilgiye göre token üretiliyor.
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
        int GetAuthenticatedUser(string token);
        //bool ValidateToken(string token);


    }
}
