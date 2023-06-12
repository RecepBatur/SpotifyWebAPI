using Core.Entities;
using Entities.Concrete.Base;
using Entities.Dtos.BaseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.FollowerDtos
{
    public class FollowerAddDto : IDto
    {
        public int FollowerId { get; set; }
    }
}
