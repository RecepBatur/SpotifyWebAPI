using Core.DataAccess.EfEntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Concrete.EntityFramework.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class EfAlbumDal : EfEntityRepositoryBase<Album, SpotifyContext>,IAlbumDal
    {
    }

}
