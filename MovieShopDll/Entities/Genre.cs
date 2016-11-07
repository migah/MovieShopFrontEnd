using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieShopDll.Entities
{
    public class Genre
    {
        public int GenreId { get; set; }
        [Display(Name = "Genre")]
        public string Name { get; set; }
        public virtual List<Movie> Movies { get; set; }
    }
}
