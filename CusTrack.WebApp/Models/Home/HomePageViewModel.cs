using CusTrack.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CusTrack.WebApp.Models.Home
{
    public class HomePageViewModel
    {
        public IEnumerable<Customer> CustomerList { get; set; }
        public IEnumerable<Seans> SeansList { get; set; }

        public Seans Seans { get; set; }
    }
}