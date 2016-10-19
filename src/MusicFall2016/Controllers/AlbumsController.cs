using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicFall2016.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
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
        public IActionResult Details(int? id)
        {

            var albums = db.Albums.Include(a => a.Artist).Include(a => a.Genre).ToList();
            Album album = albums.Single(a => a.AlbumID == id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }
        public IActionResult Edit(int? id)
        {
            var albums = db.Albums.Include(a => a.Artist).Include(a => a.Genre).ToList();
            Album album = albums.Single(a => a.AlbumID == id);
            ViewBag.Artists = new SelectList(db.Artists.ToList(), "ArtistID", "Name");
            ViewBag.Genres = new SelectList(db.Genres.ToList(), "GenreID", "Name");
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }
        [HttpPost]
        public IActionResult Edit(Album album, int? id)
        {
            var albums = db.Albums.Include(a => a.Artist).Include(a => a.Genre).ToList();
            album = albums.Single(a => a.AlbumID == id);
            if (ModelState.IsValid)
            {
                db.Entry(album).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(album);
        }

        private IActionResult HttpNotFound()
        { 
            throw new NotImplementedException();
        }
    }
}
