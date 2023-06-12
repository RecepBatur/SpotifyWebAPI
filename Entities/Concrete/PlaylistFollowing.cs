﻿using Core.Entities;
using Core.Entities.Concrete.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class PlaylistFollowing : BaseEntity, IEntity
    {
        public int Id { get; set; }
        public int PlaylistId { get; set; }
        public int FollowId { get; set; }
        public string PlaylistName { get; set; }
    }
}
