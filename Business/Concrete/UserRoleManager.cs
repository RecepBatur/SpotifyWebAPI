using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserRoleManager : IUserRoleService
    {
        private readonly IUserRoleDal _userRoleDal;

        public UserRoleManager(IUserRoleDal userRoleDal)
        {
            _userRoleDal = userRoleDal;
        }

        public IResult Add(UserOperationClaim userOperationClaim)
        {
            var addedRole = new UserOperationClaim
            {
                UserId = userOperationClaim.UserId,
                OperationClaimId = userOperationClaim.OperationClaimId,
                Status = true,
                CreatedDate = userOperationClaim.CreatedDate,
                ModifiedDate= userOperationClaim.ModifiedDate,
            };
            _userRoleDal.Add(addedRole);
            return new SuccessResult(Messages.UserRole);
        }
    }
}
