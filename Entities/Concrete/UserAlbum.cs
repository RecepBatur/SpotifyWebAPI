using Core.Entities;
using Core.Entities.Concrete.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class UserAlbum : BaseEntity, IEntity
    {
        public int Id { get; set; }
        public int AlbumId { get; set; }
        public int UserId { get; set; }
    }
}
