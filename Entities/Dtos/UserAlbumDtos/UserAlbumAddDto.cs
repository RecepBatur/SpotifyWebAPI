using Core.Entities;
using Entities.Dtos.BaseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.UserAlbumDtos
{
    public class UserAlbumAddDto : BaseEntityDto, IDto
    {
        public int AlbumId { get; set; }
    }
}
