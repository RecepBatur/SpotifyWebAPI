using Core.Entities;
using Entities.Dtos.BaseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.LibraryDtos
{
    public class LibraryUpdateDto : BaseEntityDto, IDto
    {
        public int Id { get; set; }
        public int PlaylistId { get; set; }
        public string AlbumsId { get; set; }
    }
}
