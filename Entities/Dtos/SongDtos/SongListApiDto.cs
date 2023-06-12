using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.SongDtos
{
    public class SongListApiDto
    {

        //public class Artist
        //{
        //    public string href { get; set; }
        //    public string id { get; set; }
        //    public string name { get; set; }
        //    public string type { get; set; }
        //    public string uri { get; set; }
        //}

        public class Item
        {
            //public List<Artist> artists { get; set; }
            public string id { get; set; }
            public string name { get; set; }
            //public string preview_url { get; set; }
            //public string type { get; set; }
        }

        public class Root
        {
            //public string album_group { get; set; }
            //public string album_type { get; set; }
            //public List<Artist> artists { get; set; }
            //public string href { get; set; }
            public string id { get; set; }
            //public string label { get; set; }
            public string name { get; set; }
            public string release_date { get; set; }
            public Tracks tracks { get; set; }
        }

        public class Tracks
        {
            //public string href { get; set; }
            public List<Item> items { get; set; }
        }







        //public class Item
        //{
        //    public string Id { get; set; }
        //    public string Name { get; set; }
        //    public bool Status { get; set; }
        //    public DateTime CreatedDate { get; set; }
        //    public DateTime ModifiedDate { get; set;}

        //}

        //public class Root
        //{
        //    //public string release_date { get; set; }
        //    public Tracks tracks { get; set; }
        //}

        //public class Tracks
        //{
        //    public List<Item> items { get; set; }
        //}
    }
}
