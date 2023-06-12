using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.PlaylistFollowingDtos
{
    public class PlaylistFollowAddDto : IDto
    {
        public int PlaylistId { get; set; }
        //public int FollowerId { get; set; }
    }
}
