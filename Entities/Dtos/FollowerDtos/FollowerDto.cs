using Core.Entities;
using Entities.Dtos.BaseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.FollowerDtos
{
    public class FollowerDto : BaseEntityDto, IDto
    {
        //public int Id { get; set; }
        public int UserId { get; set; }
        public int FollowerId { get; set; }
    }
}
