using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PlayMusic.Models;

namespace PlayMusic.Controllers
{
    public class AdminController : Controller
    {

        MusicStoreEntities db = new MusicStoreEntities();
        public ActionResult Index()
        {
           

            return View();


        }
        [ChildActionOnly]

        public ActionResult GenreMenu()
        {
            var genres = db.Genres.ToList();

            return PartialView(genres);
        }


        public ActionResult ArtistMenu()
        {
            var artists = db.Artists.ToList();

            return PartialView(artists);
        }
        private List<Album> GetTopSellingAlbums(int count)
        {
            // Group the order details by album and return
            // the albums with the highest count

            return db.Albums
                .OrderByDescending(a => a.OrderDetails.Count())
                .Take(count)
                .ToList();
        }

        private Genre GetTopAlbums(int count,string genre)
        {
    
            return db.Genres.Include("Albums").ToList()
                .Single(g => g.Name == genre);
                
            
        }
        public ActionResult AlbumDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        
    }
}