using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using MovieShopAdmin.Models;
using MovieShopDll;
using MovieShopDll.Entities;

namespace MovieShopAdmin.Controllers
{
    public class HomeController : Controller
    {
        // private MovieShopContext db = new MovieShopContext();
        private readonly IManager<Movie> movieManager = new DllFacade().GetMovieManager();
        private readonly IManager<Genre> genreManager = new DllFacade().GetGenreManager();

        // GET: Movies
        public ActionResult Index()
        {
            return View(movieManager.Read());
        }

        // GET: Movies/Details/5
        public ActionResult Details
        (int
        id)
        {
            var movie = movieManager.Read(id);

            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // GET: Movies/Create
        public
        ActionResult Create
        ()
        {
            var addMovieViewModel = new AddMovieViewModel
            {
                Genres = genreManager.Read()

            };
            return View(addMovieViewModel);
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPostAttribute]
        [ValidateAntiForgeryTokenAttribute]
        public ActionResult Create ([Bind(Include = "Id,Title,Year,Price,ImageUrl,TrailerUrl, Genre")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                movieManager.Create(movie);

                return RedirectToAction("Index");
            }
            var model = new AddMovieViewModel()
            {
                Movie = movie,
                Genres = genreManager.Read()
            };
            return View(model);
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int id)
        {
            var movie = movieManager.Read(id);
            var addMovieViewModel = new AddMovieViewModel
            {
                Genres = genreManager.Read(),
                Movie = movie

            };
            return View(addMovieViewModel);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPostAttribute]
        [ValidateAntiForgeryTokenAttribute]
        public ActionResult Edit ([Bind(Include = "MovieId,Title,Year,Price,ImageUrl,TrailerUrl,GenreId")]Movie movie)
        {
            if (ModelState.IsValid)
            {
                movieManager.Update(movie);

                return RedirectToAction("Index");
            }
            var model = new AddMovieViewModel()
            {
                Movie = movie,
                Genres = genreManager.Read()
            };
            return View(model);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete
        (int id)
        {
            var movie = movieManager.Read(id);

            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPostAttribute,ActionName("Delete")]
        [ValidateAntiForgeryTokenAttribute]
        public ActionResult DeleteConfirmed(int id)
        {
            movieManager.Delete(id);
            return RedirectToAction("Index");
        }

    }
}