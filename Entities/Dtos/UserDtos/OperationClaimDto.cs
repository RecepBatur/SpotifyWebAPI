using Core.Entities;
using Entities.Dtos.BaseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.UserDtos
{
    public class OperationClaimDto : BaseEntityDto, IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
