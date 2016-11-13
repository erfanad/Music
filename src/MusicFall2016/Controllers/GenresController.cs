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
       
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var genre = db.Genres.SingleOrDefault(a => a.GenreID == id);
            ViewBag.Albums = db.Albums.Where(a => a.GenreID == id).ToList();
            if (genre == null)
            {
                return NotFound();
            }
            return View(genre);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Genre genre)
        {
            if (db.Genres.Any(a => a.Name == genre.Name))
            {
                ModelState.AddModelError("Name", "This genre already exists!");
            }
            if (ModelState.IsValid)
            {
                db.Add(genre);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(genre);
        }
    }
}
