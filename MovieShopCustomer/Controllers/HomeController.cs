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
        private readonly IServiceGateway<Movie> _movieServiceGateway = new DllFacade().GetMovieManager();
        private readonly IServiceGateway<Genre> _genreServiceGateway = new DllFacade().GetGenreManager();

        // GET: Movies
        public ActionResult Index()
        {
            

            var model = new MovieGenreViewModel()
            {
                Movies = _movieServiceGateway.Read(),
                Genres = _genreServiceGateway.Read()

            };

            return View(model);
        }

        // GET: Movies/Details/5
        public ActionResult Details (int id)
        {
            var movie = _movieServiceGateway.Read(id);

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

            foreach (var movie in _movieServiceGateway.Read())
            {
                if (movie.Genre.GenreId == genreId)
                {
                    movies.Add(movie);
                }
            }

            var model = new MovieGenreViewModel()
            {
                Genres = _genreServiceGateway.Read(),
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
                _movieServiceGateway.Create(movie);

                return RedirectToAction("Index");
            }

            return View(movie);
        }

        // GET: Movies/Edit/5
        public ActionResult Edit (int id)
        {
            var movie = _movieServiceGateway.Read(id);

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
                _movieServiceGateway.Update(movie);

                return RedirectToAction("Index");
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete (int id)
        {
            var movie = _movieServiceGateway.Read(id);

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
            _movieServiceGateway.Delete(id);

            return RedirectToAction("Index");
        }

        public ActionResult BuyMovie(int id)
        {
            return RedirectToAction("Index", "Login", new { movieId = id });
        }

    }
}