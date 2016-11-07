using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieShopDll.Contexts;
using MovieShopDll.Entities;

namespace MovieShopDll.Manager
{
    class GenreManager : IManager<Genre>
    {
        public Genre Create(Genre t)
        {
            using (var db = new MovieShopContext())
            {
                db.Genres.Add(t);
                db.SaveChanges();
                return t;
            }
        }

        public Genre Read(int id)
        {
            using (var db = new MovieShopContext())
            {
                return db.Genres.FirstOrDefault(x => x.GenreId == id);
            }
        }

        public List<Genre> Read()
        {
            using (var db = new MovieShopContext())
            {
                return db.Genres.ToList();
            }
        }

        public void Delete(int id)
        {
            using (var db = new MovieShopContext())
            {
                var genreTodelete = db.Genres.FirstOrDefault(x => x.GenreId == id);
                db.Entry(genreTodelete).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
        }

        public Genre Update(Genre t)
        {
            using (var db = new MovieShopContext())
            {              
                db.Entry(t).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return t;
            }
        }
    }
}
