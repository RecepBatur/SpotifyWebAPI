using Core.Entities;
using Entities.Dtos.BaseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.LibraryDtos
{
    public class LibraryAddDto : BaseEntityDto, IDto
    {
        public int PlaylistId { get; set; }
        public string AlbumId { get; set; }
        public int UserId { get; set; }

    }
}
