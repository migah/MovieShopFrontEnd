using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MovieShopDll.Contexts;
using MovieShopDll.Entities;

namespace MovieShopDll.Manager
{
    class CustomerServiceGateway : IServiceGateway<Customer>
    {
        public Customer Create(Customer t)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://movieshopwebapi.azurewebsites.net");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.PostAsJsonAsync("api/customers", t).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<Customer>().Result;
                }
                return null;
            }
        }

        public Customer Read(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://movieshopwebapi.azurewebsites.net");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync($"api/customers/{id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<Customer>().Result;
                }
                return null;
            }
        }

        public List<Customer> Read()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://movieshopwebapi.azurewebsites.net");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync("/api/customers").Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<List<Customer>>().Result;
                }
            }
            return new List<Customer>();
        }

        public bool Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://movieshopwebapi.azurewebsites.net");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.DeleteAsync($"/api/customers/{id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<Customer>().Result != null;
                }
                return false;
            }
        }

        public Customer Update(Customer t)
        {

            throw new NotImplementedException();

            /* using (var db = new MovieShopContext())
             {
                 var foundCustomer = db.Customers.Include(x => x.Address).FirstOrDefault(x => x.CustomerId == t.CustomerId);

                 db.Entry(foundCustomer).CurrentValues.SetValues(t);
                 foundCustomer.Address = t.Address;
                // db.Entry(t).State = System.Data.Entity.EntityState.Modified;
                 db.SaveChanges();


                 return t;
             }*/
         }
        }
    
}
