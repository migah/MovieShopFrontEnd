using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieShopDll.Entities;

namespace MovieShopAdmin.Models
{
	public class AddMovieViewModel
	{
	    public List<Genre> Genres { get; set; }
	    public Movie Movie { get; set; }
	}
}