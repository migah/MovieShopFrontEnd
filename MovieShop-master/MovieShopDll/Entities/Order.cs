using System;
using System.Collections.Generic;

namespace MovieShopDll.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public Customer Customer { get; set; }
        public List<Movie> Movies { get; set; }
    }
}