using Core.Entities;
using Entities.Dtos.BaseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.FavoriteDtos
{
    public class FavoriteDto : BaseEntityDto, IDto
    {
        public int UserId { get; set; }

    }
}
