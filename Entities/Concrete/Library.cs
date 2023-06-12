using Core.Entities;
using Entities.Concrete.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Library : BaseEntity, IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }

    }


}
