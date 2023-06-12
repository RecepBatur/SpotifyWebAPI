using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.AlbumDtos
{
    public class AlbumAddApiDto
    {
        public class Album
        {
            public string AlbumType { get; set; }
            public List<Artist> Artists { get; set; }
            public List<Copyright> Copyrights { get; set; }
            public ExternalIds ExternalIds { get; set; }
            public ExternalUrls ExternalUrls { get; set; }
            public List<object> Genres { get; set; }
            public string Id { get; set; }
            public List<Image> Images { get; set; }
            public string Label { get; set; }
            public string Name { get; set; }
            public int? Popularity { get; set; }
            public string ReleaseDate { get; set; }
            public string ReleaseDatePrecision { get; set; }
            public int? TotalTracks { get; set; }
            public Tracks Tracks { get; set; }
            public string Type { get; set; }
            public string Uri { get; set; }

        }

        public class Artist
        {
            public ExternalUrls ExternalUrls { get; set; }
            public string Id { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public string Uri { get; set; }
        }

        public class Copyright
        {
            public string Text { get; set; }
            public string Type { get; set; }
        }

        public class ExternalIds
        {
            public string Upc { get; set; }
        }

        public class ExternalUrls
        {
            public string Spotify { get; set; }
        }

        public class Image
        {
            public int? Height { get; set; }
            public string Url { get; set; }
            public int? Width { get; set; }
        }

        public class Item
        {
            public List<Artist> Artists { get; set; }
            public int? DiscNumber { get; set; }
            public int? DurationMs { get; set; }
            public bool? Explicit { get; set; }
            public ExternalUrls ExternalUrls { get; set; }
            public string Id { get; set; }
            public bool? IsLocal { get; set; }
            public bool? IsPlayable { get; set; }
            public string Name { get; set; }
            public string PreviewUrl { get; set; }
            public int? TrackNumber { get; set; }
            public string Type { get; set; }
            public string Uri { get; set; }
        }

        public class Root
        {
            public List<Album> Albums { get; set; }
        }

        public class Tracks
        {
            public List<Item> Items { get; set; }
            public int? Limit { get; set; }
            public object Next { get; set; }
            public int? Offset { get; set; }
            public object Previous { get; set; }
            public int? Total { get; set; }
        }
    }
}
