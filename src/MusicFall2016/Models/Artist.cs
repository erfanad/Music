using System.ComponentModel.DataAnnotations;

namespace MusicFall2016.Models
{
    public class Artist
    {
        public int ArtistID { get; set; }
        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; }

        public string Bio { get; set; }
    }
}