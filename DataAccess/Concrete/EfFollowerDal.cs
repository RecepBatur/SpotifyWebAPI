using Core.DataAccess.EfEntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Concrete.EntityFramework.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class EfFollowerDal : EfEntityRepositoryBase<Follower,SpotifyContext>,IFollowerDal
    {
    }
}
