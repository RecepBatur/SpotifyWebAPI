using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserRoleService
    {
        IResult Add(UserOperationClaim userOperationClaim);
    }
}
