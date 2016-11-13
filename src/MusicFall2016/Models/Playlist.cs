using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicFall2016.Models
{
    public class Playlist
    {
        public int PlaylistID { get; set; }
        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; }
        public AppUser Owner { get; set; }
        public List<PlaylistAlbums> Albums { get; set; }
    }
}
