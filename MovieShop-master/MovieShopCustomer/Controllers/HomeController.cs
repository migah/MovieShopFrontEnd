using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieShopCustomer.Models;
using MovieShopDll;
using MovieShopDll.Entities;

namespace MovieShopCustomer.Controllers
{
    public class HomeController : Controller
    {
        private readonly IManager<Movie> movieManager = new DllFacade().GetMovieManager();
        private readonly IManager<Genre> genreManager = new DllFacade().GetGenreManager();

        // GET: Movies
        public ActionResult Index()
        {
            

            var model = new MovieGenreViewModel()
            {
                Movies = movieManager.Read(),
                Genres = genreManager.Read()

            };

            return View(model);
        }

        // GET: Movies/Details/5
        public ActionResult Details (int id)
        {
            var movie = movieManager.Read(id);

            if (movie == null)
            {
                return HttpNotFound();
            }
            
            return View(movie);
        }

        [ActionName("filter")]
        public ActionResult FilterGenre(int genreId)
        {
            List<Movie> movies = new List<Movie>();

            foreach (var movie in movieManager.Read())
            {
                if (movie.Genre.GenreId == genreId)
                {
                    movies.Add(movie);
                }
            }

            var model = new MovieGenreViewModel()
            {
                Genres = genreManager.Read(),
                Movies = movies
            };

            return View("~/Views/Home/Index.cshtml", model);
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

            return View(movie);
        }

        // GET: Movies/Edit/5
        public ActionResult Edit (int id)
        {
            var movie = movieManager.Read(id);

            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPostAttribute]
        [ValidateAntiForgeryTokenAttribute]
        public ActionResult Edit ([Bind(Include = "Id,Title,Year,Price,ImageUrl,TrailerUrl")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                movieManager.Update(movie);

                return RedirectToAction("Index");
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete (int id)
        {
            var movie = movieManager.Read(id);

            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPostAttribute, ActionName("Delete")]
        [ValidateAntiForgeryTokenAttribute]
        public ActionResult DeleteConfirmed (int id)
        {
            movieManager.Delete(id);

            return RedirectToAction("Index");
        }

        public ActionResult BuyMovie(int id)
        {
            return RedirectToAction("Index", "Login", new { movieId = id });
        }

    }
}