using Microsoft.AspNetCore.Mvc;
using MusicFall2016.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicFall2016.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly MusicDbContext db;

        public AlbumsController(MusicDbContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            var albums =  db.Albums.Include(a => a.Artist).Include(a => a.Genre).ToList();
            return View(albums);
        }
        public IActionResult Create()
        {
            ViewBag.Artists = new SelectList(db.Artists.ToList(), "ArtistID", "Name");
            ViewBag.Genres = new SelectList(db.Genres.ToList(), "GenreID", "Name");
            return View();

        }
        [HttpPost]
        public IActionResult Create(Album album)
        {
            if (ModelState.IsValid)
            {
                db.Add(album);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Artists = new SelectList(db.Artists.ToList(), "ArtistID", "Name");
            ViewBag.Genres = new SelectList(db.Genres.ToList(), "GenreID", "Name");
            return View();
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var album = db.Albums.Include(a => a.Artist).Include(a => a.Genre).SingleOrDefault(a => a.AlbumID == id);
            if (album == null)
            {
                return NotFound();
            }
            return View(album);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var album = db.Albums.Include(a => a.Artist).Include(a => a.Genre).SingleOrDefault(a => a.AlbumID == id);
            ViewBag.Artists = new SelectList(db.Artists.ToList(), "ArtistID", "Name");
            ViewBag.Genres = new SelectList(db.Genres.ToList(), "GenreID", "Name");
            if (album == null)
            {
                return NotFound();
            }
            return View(album);
        }
        [HttpPost]
        public ActionResult Edit(Album album)
        {
            if (ModelState.IsValid)
            {
                db.Update(album);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Artists = new SelectList(db.Artists.ToList(), "ArtistID", "Name");
            ViewBag.Genres = new SelectList(db.Genres.ToList(), "GenreID", "Name");
            return View(album);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var album = db.Albums.Include(a => a.Artist).Include(a => a.Genre).SingleOrDefault(a => a.AlbumID == id);
            if (album == null)
            {
                return NotFound();
            }
            return View(album);
        }
        [HttpPost]
        public IActionResult Delete(Album album)
        {
            db.Remove(album);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
