using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.FavoriteDtos
{
    public class UserFavoriteListDto : IDto
    {
        public int UserId { get; set; }
        public int SongId { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
    }
}
