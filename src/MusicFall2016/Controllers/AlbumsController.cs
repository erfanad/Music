using Microsoft.AspNetCore.Mvc;
using MusicFall2016.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

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
        public IActionResult Index(string search, string sort)
        {
            ViewBag.ArtistSort = sort == "Artist" ? "ArtDesc" : "Artist";
            ViewBag.GenreSort = sort == "Genre" ? "GenreDesc" : "Genre";
            ViewBag.PriceSort = sort == "Price" ? "PriceDesc" : "Price";
            ViewBag.LikesSort = sort == "Likes" ? "LikesDesc" : "Likes";
            var albums = db.Albums.Include(a => a.Artist).Include(a => a.Genre).ToList();
            if (!string.IsNullOrEmpty(search))
            {
                ViewBag.Search = search;
                albums = db.Albums.Where(a => a.Title.Contains(search) || a.Artist.Name.Contains(search) || a.Genre.Name.Contains(search)).ToList();
            }
            switch (sort)
            {
                case "ArtDesc":
                    albums = albums.OrderByDescending(a => a.Artist.Name).ToList();
                    break;
                case "Artist":
                    albums = albums.OrderBy(a => a.Artist.Name).ToList();
                    break;
                case "GenreDesc":
                    albums = albums.OrderByDescending(a => a.Genre.Name).ToList();
                    break;
                case "Genre":
                    albums = albums.OrderBy(a => a.Genre.Name).ToList();
                    break;
                case "PriceDesc":
                    albums = albums.OrderByDescending(a => a.Price).ToList();
                    break;
                case "Price":
                    albums = albums.OrderBy(a => a.Price).ToList();
                    break;
                case "LikesDesc":
                    albums = albums.OrderByDescending(a => a.Likes).ToList();
                    break;
                case "Likes":
                    albums = albums.OrderBy(a => a.Likes).ToList();
                    break;
            }
            return View(albums);
        }
        public IActionResult Create()
        {
            ViewBag.Artists = new SelectList(db.Artists.ToList(), "ArtistID", "Name");
            ViewBag.Genres = new SelectList(db.Genres.ToList(), "GenreID", "Name");
            return View();

        }
        [HttpPost]
        public IActionResult Create(Album album, string NewArtist, string NewGenre)
        {
            if(db.Artists.Any(a => a.Name == NewArtist))
            {
                album.ArtistID = db.Artists.SingleOrDefault(a => a.Name == NewArtist).ArtistID;
            }
            if (db.Genres.Any(a => a.Name == NewGenre))
            {
                album.GenreID = db.Artists.SingleOrDefault(a => a.Name == NewArtist).ArtistID;
            }
            if (string.IsNullOrEmpty(NewArtist) && album.ArtistID == 0)
            {
                ModelState.AddModelError("ArtistID", "Artist is required!");
            }
            if (string.IsNullOrEmpty(NewGenre) && album.GenreID == 0)
            {
                ModelState.AddModelError("GenreID", "Genre is required!");
            }
            if (!string.IsNullOrEmpty(NewArtist) && !db.Artists.Any(a => a.Name == NewArtist))
            {
                db.Artists.Add(new Artist { Name = NewArtist });
                db.SaveChanges();
                album.ArtistID = db.Artists.SingleOrDefault(a => a.Name == NewArtist).ArtistID;
            }
            if (!string.IsNullOrEmpty(NewGenre) && !db.Genres.Any(a => a.Name == NewGenre))
            {
                db.Genres.Add(new Genre { Name = NewGenre });
                db.SaveChanges();
                album.GenreID = db.Genres.SingleOrDefault(a => a.Name == NewGenre).GenreID;
            }
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
            var album = db.Albums.Include(a => a.Artist).Include(a => a.Genre).SingleOrDefault(a => a.AlbumID == id);
            ViewBag.Suggest = db.Albums.Where(a => (a.ArtistID == album.ArtistID || a.GenreID == album.GenreID) && a.Likes >= 2 && a.AlbumID != album.AlbumID).Take(5);
            if (id == null)
            {
                return NotFound();
            }
            
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

        public IActionResult Like(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var album = db.Albums.SingleOrDefault(a => a.AlbumID == id);
            if (album == null)
            {
                return NotFound();
            }
            album.Likes++;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}