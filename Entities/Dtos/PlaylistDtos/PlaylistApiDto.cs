using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.PlaylistDtos
{
    public class PlaylistApiDto
    {

        public class Item
        {
            public string id { get; set; }
            public string name { get; set; }
        }

        public class Root
        {
            public string id { get; set; }
            public string label { get; set; }
            public string name { get; set; }
            public string release_date { get; set; }
            public Tracks tracks { get; set; }
        }

        public class Tracks
        {
            public List<Item> items { get; set; }
        }
    }
}
