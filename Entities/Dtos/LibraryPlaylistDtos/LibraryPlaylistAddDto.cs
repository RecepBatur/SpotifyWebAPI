using Core.Entities;
using Entities.Dtos.BaseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.LibraryPlaylistDtos
{
    public class LibraryPlaylistAddDto : BaseEntityDto, IDto
    {
        public int PlaylistId { get; set; }

    }
}
