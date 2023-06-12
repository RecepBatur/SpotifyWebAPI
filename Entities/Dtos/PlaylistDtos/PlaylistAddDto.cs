using Core.Entities;
using Entities.Dtos.BaseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.PlaylistDtos
{
    public class PlaylistAddDto : BaseEntityDto, IDto
    {
        //public string Name { get; set; }
        //public int Id { get; set; }
        public string PlaylistName { get; set; }
        //public int FollowerId { get; set; }


    }
}
