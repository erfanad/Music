using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicFall2016.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicFall2016.Controllers
{
    public class GenresController : Controller
    {
        private readonly MusicDbContext db;
        public GenresController(MusicDbContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View(db.Genres.ToList());
        }
    }
}
