using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicFall2016.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
            Album album = db.Albums.Where(a => a.AlbumID == id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }
    }
}
