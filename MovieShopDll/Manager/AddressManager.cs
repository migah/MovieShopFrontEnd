using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieShopDll.Contexts;
using MovieShopDll.Entities;

namespace MovieShopDll.Manager
{
    class AddressManager : IManager<Address>
    {
        public Address Create(Address t)
        {
            using (var db = new MovieShopContext())
            {
                db.Addresses.Add(t);
                db.SaveChanges();
                return t;
            }
        }

        public Address Read(int id)
        {
            using (var db = new MovieShopContext())
            {
                return db.Addresses.FirstOrDefault(x => x.Id == id);
            }
        }

        public List<Address> Read()
        {
            using (var db = new MovieShopContext())
            {
                return db.Addresses.ToList();
            }
        }

        public void Delete(int id)
        {
            using (var db = new MovieShopContext())
            {
                var addressTodelete = db.Movies.FirstOrDefault(x => x.MovieId == id);
                db.Entry(addressTodelete).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
        }

        public Address Update(Address t)
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
