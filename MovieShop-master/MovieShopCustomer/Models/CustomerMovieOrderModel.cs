using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieShopDll.Entities;

namespace MovieShopCustomer.Models
{
    public class CustomerMovieOrderModel
    {
        public Customer Customer { get; set; }

        public Order Order { get; set; }

        public Movie Movie { get; set; }
    }
}