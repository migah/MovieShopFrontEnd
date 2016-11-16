using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieShopDll.Entities;

namespace MovieShopDll
{
    public interface ICurrencyConverter
    {
        Currency GetCurrencyRate(string from);
    }
}
