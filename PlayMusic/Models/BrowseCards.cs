using System.Collections.Generic;
using PlayMusic.Models;

namespace PlayMusic.Models
{
    public class BrowseCards
    {
        public List<Album> top_albums { get; set; }
        public Genre top_jazz { get; set; }
        public Genre top_rock { get; set; }
        public Genre top_classic { get; set; }
        public Genre top_pop { get; set; }
        public List<Album> Search_albums { get; set; }
        public List<Genre> Genrelist { get; set; }



    }
}