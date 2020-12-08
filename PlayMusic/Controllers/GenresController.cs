using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PlayMusic.Models;

namespace PlayMusic.Controllers
{
    public class GenresController : Controller
    {
        private MusicStoreEntities db = new MusicStoreEntities();

        // GET: Genres
        public ActionResult Index()
        {
            return View(db.Genres.ToList());
        }

        // GET: Genres/Details/5
        public ActionResult Details(int? id,string genrename,string search)
        {
            if (id == null&& genrename==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GenreAlbums genreAlbums = new GenreAlbums();
            
            Genre genre = db.Genres.Find(id);

            if (genre == null)
            {
                return HttpNotFound();
            }
            genreAlbums.genredetail = genre;
            genreAlbums.genrelist = GetgenreList(genre.Name, search);

            return View(genreAlbums);
        }

      
   


        public ActionResult GenreAlbums(int? id, string genrename, string option, string search)
        {
            if (id == null && genrename == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GenreAlbums genreAlbums = new GenreAlbums();

            Genre genre = db.Genres.Find(id);

            if (genre == null)
            {
                return HttpNotFound();
            }
            genreAlbums.genredetail = genre;
            genreAlbums.genrelist = GetgenreList(genre.Name, search);

            return View(genreAlbums);
        }

        private List<Album> GetgenreList( string genre,  string search)
        {

            //var albums = db.Albums.Include(a => a.Artist).Include(a => a.Genre).Where(c => c.Genre.Name.Equals(genre) && c.Title.Contains(search) || search == null).ToList();

            if (search == null)
            {
              //  return db.Genres.Include("Albums").ToList()
              //.Single(g => g.Name == genre);

                return db.Albums.Include(a => a.Artist).Include(a => a.Genre)
                    .Where(c => c.Genre.Name.Equals(genre)).ToList();
            }
            else
            {
                return db.Albums.Include(a => a.Artist).Include(a => a.Genre).Where(c => c.Genre.Name.Equals(genre) && c.Title.Contains(search) || search == null).ToList();

            }


            //if (search == null)
            //{
            //    return db.Genres.Include("Albums").ToList()
            //  .Single(g => g.Name == genre);
            //}
            //else
            //{

            //    return db.Genres.Where(c => c.Name == genre)
            //                  .Include("Albums")
            //                  .Where(c => c.Albums.Any(x => x.Title.StartsWith(search)))
            //                  .FirstOrDefault();
            //}



        }
            // GET: Genres/Create
            public ActionResult Create()
        {
            return View();
        }

        // POST: Genres/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GenreId,Name,Description")] Genre genre)
        {
            if (ModelState.IsValid)
            {
                db.Genres.Add(genre);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(genre);
        }

        // GET: Genres/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genre genre = db.Genres.Find(id);
            if (genre == null)
            {
                return HttpNotFound();
            }
            return View(genre);
        }

        // POST: Genres/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GenreId,Name,Description")] Genre genre)
        {
            if (ModelState.IsValid)
            {
                db.Entry(genre).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(genre);
        }

        // GET: Genres/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genre genre = db.Genres.Find(id);
            if (genre == null)
            {
                return HttpNotFound();
            }
            return View(genre);
        }

        // POST: Genres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Genre genre = db.Genres.Find(id);
            db.Genres.Remove(genre);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
