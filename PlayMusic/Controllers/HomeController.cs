using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PlayMusic.Models;

namespace PlayMusic.Controllers
{
    public class HomeController : Controller
    {

        MusicStoreEntities db = new MusicStoreEntities();
        public ActionResult Index()
        {
            var mymodel = db.Genres.Include("Albums").ToList()
              .Single(g => g.Name == "Rock");
            BrowseCards browse = new BrowseCards();
            browse.top_albums= GetTopSellingAlbums(10);
            browse.top_jazz = GetTopAlbums(10, "Jazz");
            browse.top_rock = GetTopAlbums(10, "Rock");
            browse.top_classic = GetTopAlbums(10, "Classical");
            browse.top_pop = GetTopAlbums(10, "Pop");
            browse.Genrelist = db.Genres.ToList();
           // var mymodel = db.Albums.Include(a => a.Artist).Include(a => a.Genre).ToList();

            return View(browse);


        }

        public ActionResult SearchResult(String search, string genreitem, string artistname)
        {
            var mymodel = db.Genres.Include("Albums").ToList().Single(g => g.Name == "Rock");
            BrowseCards browse = new BrowseCards();
            browse.Search_albums = GetSearchAlbums(search, genreitem, artistname);            
            browse.Genrelist = db.Genres.ToList();
            //var mymodel = db.Albums.Include(a => a.Artist).Include(a => a.Genre);

            return View(browse);


        }
        [ChildActionOnly]
        public ActionResult Browse(string genre)
        {
            // Retrieve Genre and its Associated Albums from database
            var genreModel = db.Genres.Include("Albums")
                .Single(g => g.Name == genre);

            return View(genreModel);
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
            //var artists = db.Artists
            //   .OrderByDescending(a => a.Name.Count())
            //   .Take(10)
            //   .ToList();

            return PartialView(artists);
        }
        
             private List<Album> GetSearchAlbums(string search,string genreitem,string artistname)
        {
            var albums = db.Albums.AsQueryable();
            var courseConditions = new List<IQueryable<Album>>();

            if (!string.IsNullOrEmpty(search))
            {
                courseConditions.Add(albums.Where(s => s.Title.Contains(search)));
            }
            if (genreitem != null)
            {
                courseConditions.Add(albums.Where(s => s.Genre.Name == genreitem));
            }
            if (artistname != null)
            {
                courseConditions.Add(albums.Where(s => s.Artist.Name == artistname));
            }

            switch (courseConditions.Count)
            {
                case 0:
                    {
                        break;
                    }
                case 1:
                    {
                        albums = courseConditions[0];
                        break;
                    }
                default:
                    {
                        albums = courseConditions.Aggregate((left, right) => left.Union(right));
                        break;
                    }
            }

            return albums.ToList();


            //if (search == null|| genreitem==null)
            //{
            //    //  return db.Genres.Include("Albums").ToList()
            //    //.Single(g => g.Name == genre);
            //    var albums = db.Albums.Include("Artist").Where(c => c.Artist.Name.Contains(artistname) || artistname == null);
            //    return albums.ToList();

            //  //  return db.Albums.ToList();
            //}
            //else
            //{
            //    var albums = db.Albums.Where(c => c.Genre.Name.Contains(genreitem) && c.Title.Contains(search) || search == null || genreitem == null);
            //    return albums.ToList();
               
            //}
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