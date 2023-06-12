using Core.Entities;
using Entities.Dtos.BaseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.PlaylistDtos
{
    public class PlaylistUpdateDto : BaseEntityDto, IDto
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
}
