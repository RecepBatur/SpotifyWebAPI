using Core.DataAccess.EfEntityFramework;
using DataAccess.Abstract;
using Entities.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class EfUserRoleDal : EfEntityRepositoryBase<UserOperationClaim, SpotifyContext>, IUserRoleDal
    {
    }
}
