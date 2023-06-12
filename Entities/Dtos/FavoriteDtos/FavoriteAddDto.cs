using Core.Entities;
using Entities.Dtos.BaseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.FavoriteDtos
{
    public class FavoriteAddDto : BaseEntityDto, IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public List<int> SongId { get; set; }
    }
}
