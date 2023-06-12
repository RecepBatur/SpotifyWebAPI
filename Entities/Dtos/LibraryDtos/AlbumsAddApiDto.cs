using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.LibraryDtos
{
    public class AlbumsAddApiDto
    {
        public class Root
        {
            public bool is_playable { get; set; }
            public string id { get; set; }
            public string label { get; set; }
            public string name { get; set; }
            public string release_date { get; set; }
        }

    }
}
