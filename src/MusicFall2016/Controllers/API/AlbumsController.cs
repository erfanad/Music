using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicFall2016.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicFall2016.Controllers.API
{
    public class AlbumsController : Controller
    {
        MusicDbContext db;
        public AlbumsController(MusicDbContext context)
        {
            db = context;
        }
        // GET: /<controller>/
        [HttpGet("api/Albums")]
        public IActionResult Get()
        {
            // *model state* if (true) return BadRequest("Something went wrong");
            var album = db.Albums.Include(a => a.Artist).SingleOrDefault(a => a.AlbumID == 3);
            return Ok(/*new Artist{ Name = "Some Artist", Bio = "21st Century Musician" }*/ album);
        }
    }
}
