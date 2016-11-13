using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicFall2016.Models
{
    public class Genre
    {
        public int GenreID { get; set; }
        [Required(ErrorMessage = "Name is required!")]
        [StringLength(20, ErrorMessage ="Name can be 20 characters long at most!")]
        public string Name { get; set; }
    }
}