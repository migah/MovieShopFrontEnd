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
        private readonly IServiceGateway<Movie> _movieServiceGateway = new DllFacade().GetMovieManager();
        private readonly IServiceGateway<Genre> _genreServiceGateway = new DllFacade().GetGenreManager();

        // GET: Movies
        public ActionResult Index()
        {
            return View(_movieServiceGateway.Read());
        }

        // GET: Movies/Details/5
        public ActionResult Details
        (int
        id)
        {
            var movie = _movieServiceGateway.Read(id);

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
                Genres = _genreServiceGateway.Read()

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
                _movieServiceGateway.Create(movie);

                return RedirectToAction("Index");
            }
            var model = new AddMovieViewModel()
            {
                Movie = movie,
                Genres = _genreServiceGateway.Read()
            };
            return View(model);
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int id)
        {
            var movie = _movieServiceGateway.Read(id);
            var addMovieViewModel = new AddMovieViewModel
            {
                Genres = _genreServiceGateway.Read(),
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
                _movieServiceGateway.Update(movie);

                return RedirectToAction("Index");
            }
            var model = new AddMovieViewModel()
            {
                Movie = movie,
                Genres = _genreServiceGateway.Read()
            };
            return View(model);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete
        (int id)
        {
            var movie = _movieServiceGateway.Read(id);

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
            _movieServiceGateway.Delete(id);
            return RedirectToAction("Index");
        }

    }
}