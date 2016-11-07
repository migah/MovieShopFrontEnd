using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieShopDll.Entities;

namespace MovieShopCustomer.Models
{
    public class CustomerMovieView
    {
        public Customer Customer { get; set; }

        public Movie Movie { get; set; }
        
    }
}