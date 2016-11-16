using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MovieShopDll.Entities;
using Newtonsoft.Json.Linq;

namespace MovieShopDll.ServiceGateway
{
    public class CurrencyServiceGateway : ICurrencyConverter
    {


        private Uri GetRestApiUri()
        {
            return new Uri("http://api.fixer.io/latest");
        }

        public Currency GetCurrencyRate(string from)
        {

            using (var client = new HttpClient())
            {

                HttpResponseMessage response = client.GetAsync($"http://api.fixer.io/latest?base" + $"={from}").Result;
              //  HttpResponseMessage response = client.GetAsync($"http://api.fixer.io/latest?base=DKK"}).Result;}

                if (response.IsSuccessStatusCode)
                {
                    var str = response.Content.ReadAsStringAsync().Result;
                    JObject root = JObject.Parse(str);
                    JObject rates = root.Value<JObject>("rates");
                    DateTime date = root.Value<DateTime>("date");

                    var currencyRate = new Currency()
                    {
                        Base = from,
                        Date = date,
                        Rates = new List<CurrencyRate>()
                    };

                    foreach (var rate in rates)
                    {
                        double value = 0;
                        Double.TryParse(rate.Value.ToString(), out value);

                        currencyRate.Rates.Add(new CurrencyRate()
                        {
                            Name = rate.Key,
                            Value = value
                        });
                    }
                    return currencyRate;

                }
                return null;

            }

        }
    }
}
