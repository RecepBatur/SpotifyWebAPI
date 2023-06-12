using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.AlbumDtos
{
    public class AlbumListApiDto
    {
        
        public class Album
        {
            public List<Artist> Artists { get; set; }
            public string Id { get; set; }
            public string Label { get; set; }
            public string Name { get; set; }
            public string ReleaseDate { get; set; }
            public Tracks Tracks { get; set; }
        }

        public class Artist
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
        }

        public class Item
        {
            public List<Artist> Artists { get; set; }
            public string Id { get; set; }
            public string Name { get; set; }

        }

        public class Root
        {
            public List<Album> Albums { get; set; }
        }

        public class Tracks
        {
            public List<Item> Items { get; set; }
        }


    }
}
