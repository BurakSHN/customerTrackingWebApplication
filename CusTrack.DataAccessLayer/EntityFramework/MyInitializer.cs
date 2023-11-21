using CusTrack.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace CusTrack.DataAccessLayer.EntityFramework
{
    public class MyInitializer:CreateDatabaseIfNotExists<DatabaseContext>
    {
        //Seed: Database oluştuktan sonra calısıcak fonksiyon
        protected override void Seed(DatabaseContext context)
        {
            Customer customer = new Customer() {
                Name="Sertac",
                Surname="Yıldız",
                Birthday=DateTime.Today,
                Address="Balıkesir/Susurluk",
                Phone= "544444"
            };
            context.Customers.Add(customer);

            Seans seans = new Seans() {
                Title = "Lazer Uygulaması",
                Text = "2. Seans Lazer Uygulaması",
                IsDraft = false,
                Date = DateTime.Today,
                Money = 200,
                CustomerId = 1,
                Owner=customer
               
            };
            context.Seansing.Add(seans);

            string sifresiz = "123";

            User user = new User() {
                Username="root",
                Password= Crypto.HashPassword(sifresiz),
                Email ="buraksahin94@hotmail.com"
            };
            context.Users.Add(user);

            context.SaveChanges();
        }
    }
}
