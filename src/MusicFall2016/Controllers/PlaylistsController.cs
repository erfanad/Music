using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MusicFall2016.Models;
using Microsoft.AspNetCore.Identity;

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
            return View(db.Playlists.ToList());
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
    }
}
