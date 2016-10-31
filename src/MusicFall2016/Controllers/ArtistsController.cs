using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicFall2016.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicFall2016.Controllers
{
    public class ArtistsController : Controller
    {

        private readonly MusicDbContext db;
        public ArtistsController(MusicDbContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View(db.Artists.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Artist artist)
        {
            if (db.Artists.Any(a => a.Name == artist.Name))
            {
                ModelState.AddModelError("Name", "This artist already exists!");
            }

            if (ModelState.IsValid)
            {
                db.Add(artist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var artist = db.Artists.SingleOrDefault(a => a.ArtistID == id);
            ViewBag.Albums = db.Albums.Where(a => a.ArtistID == id).ToList();
            if (artist == null)
            {
                return NotFound();
            }
            return View(artist);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var artist = db.Artists.SingleOrDefault(a => a.ArtistID == id);
            if (artist == null)
            {
                return NotFound();
            }
            return View(artist);
        }
        [HttpPost]
        public IActionResult Edit(Artist artist)
        {
            if (ModelState.IsValid)
            {
                db.Update(artist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(artist);
        }
    }
}
