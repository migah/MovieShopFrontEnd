using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MovieShopDll.Contexts;
using MovieShopDll.Entities;

namespace MovieShopDll.Manager
{
    class AddressServiceGateway : IServiceGateway<Address>
    {
        public Address Create(Address t)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://movieshopwebapi.azurewebsites.net");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.PostAsJsonAsync("api/addresses", t).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<Address>().Result;
                }
                return null;
            }
        }

        public Address Read(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://movieshopwebapi.azurewebsites.net");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync($"api/addresses/{id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<Address>().Result;
                }
                return null;
            }
        }

        public List<Address> Read()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://movieshopwebapi.azurewebsites.net");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync("/api/addresses").Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<List<Address>>().Result;
                }

                return new List<Address>();
            }
        }

        public bool Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://movieshopwebapi.azurewebsites.net/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.DeleteAsync($"/api/addresses/{id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<Address>().Result != null;
                }
                return false;
            }
        }

        public Address Update(Address t)
        {
            throw new NotImplementedException();

            /* using (var db = new MovieShopContext())
             {
                 db.Entry(t).State = System.Data.Entity.EntityState.Modified;
                 db.SaveChanges();
                 return t;
             }*/
        }
    }
}
