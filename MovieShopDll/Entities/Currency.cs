using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieShopDll.Entities
{
    public class Currency
    {
        public String Base { get; set; }
        public DateTime Date { get; set; }
        public List<CurrencyRate> Rates { get; set; }
    }
}
