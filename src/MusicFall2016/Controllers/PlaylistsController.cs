using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MusicFall2016.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicFall2016.Controllers
{
    [Authorize]
    public class PlaylistsController : Controller
    {
        private readonly MusicDbContext db;
        private readonly UserManager<AppUser> _userManager;
        public PlaylistsController(MusicDbContext context, UserManager<AppUser> userManager)
        {
            db = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var user = _userManager.Users.SingleOrDefault(u => u.UserName == User.Identity.Name);
            ViewBag.Username = user.UserName;
            ViewBag.Date = user.DateJoined;
            return View(db.Playlists.Where(p => p.Owner == user).ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Playlist playlist)
        {
            playlist.Owner = _userManager.Users.SingleOrDefault(u => u.UserName == User.Identity.Name);
            if (ModelState.IsValid)
            {
                db.Add(playlist);
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
            var playlist = db.Playlists.SingleOrDefault(p => p.PlaylistID == id);
            if (playlist == null)
            {
                return NotFound();
            }
            ViewBag.Albums = db.PlaylistAlbums.Where(p => p.PlaylistID == id).Include(p => p.Album).Include(p => p.Playlist).ToList();   
            return View(playlist);
        }
        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var playlist = db.Playlists.SingleOrDefault(p => p.PlaylistID == id);
            if(playlist == null)
            {
                return NotFound();
            }
            return View(playlist);
        }
        [HttpPost]
        public IActionResult Edit(Playlist playlist)
        {
            if (ModelState.IsValid)
            {
                db.Update(playlist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(playlist);
        }
        public IActionResult Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var playlist = db.Playlists.SingleOrDefault(p => p.PlaylistID == id);
            if(playlist == null)
            {
                return NotFound();
            }
            return View(playlist);
        }
        [HttpPost]
        public IActionResult Delete(Playlist playlist)
        {
            db.Remove(playlist);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult AddAlbum(int? id)
        {
            ViewBag.Playlists = new SelectList(db.Playlists.Where(p => p.Owner.UserName == User.Identity.Name).ToList(), "PlaylistID", "Name");
            if (id == null)
            {
                return NotFound();
            }
            var album = db.Albums.SingleOrDefault(a => a.AlbumID == id);
            if (album == null)
            {
                return NotFound();
            }
            return View(album);
        }
        [HttpPost]
        public IActionResult AddAlbum(PlaylistAlbums playlist, int PlaylistID, int AlbumID)
        {
            ViewBag.Playlists = new SelectList(db.Playlists.Where(p => p.Owner.UserName == User.Identity.Name).ToList(), "PlaylistID", "Name");
            if (db.PlaylistAlbums.Any(p => (p.AlbumID == AlbumID && p.PlaylistID == PlaylistID)))
            {
                ModelState.AddModelError("", "Album already exists in this playlist!");
                return View(db.Albums.SingleOrDefault(a => a.AlbumID == AlbumID));
            }
            playlist.PlaylistID = PlaylistID;
            playlist.AlbumID = AlbumID;
            if (ModelState.IsValid)
            {
                db.Add(playlist);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = PlaylistID});
            }
            return View(db.Albums.SingleOrDefault(a => a.AlbumID == AlbumID));
        }
    }
}
