using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.PlaylistSongDtos
{
    public class PlaylistSongAddDto : IDto
    {
        //public int Id { get; set; }
        public int PlaylistId { get; set; }
        //public string PlaylistName? { get; set; }
        public List<int> SongId { get; set; }
        //public int UserId { get; set; }

    }
}
