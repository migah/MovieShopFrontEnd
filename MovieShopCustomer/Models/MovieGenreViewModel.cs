using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieShopDll.Entities;

namespace MovieShopCustomer.Models
{
    public class MovieGenreViewModel
    {
        public List<Movie> Movies { get; set; }

        public List<Genre> Genres { get; set; }

        public Currency Currency { get; set; }

        public String SelectedCurrency { get; set; }
    }
}