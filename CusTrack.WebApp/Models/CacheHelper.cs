using CusTrack.BusinessLayerr;
using CusTrack.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace CusTrack.WebApp.Models
{
    public class CacheHelper
    {
        public static List<Customer> GetCustomersFromCache()
        {
            var result = WebCache.Get("customer-cache");

            if (result == null)
            {
                CustomerManager cm = new CustomerManager();
                result = cm.ListQueryable().OrderByDescending(x => x.Name).ToList();

                WebCache.Set("customer-cache", result, 20, true);
            }
            return result;
        }

        public static void RemoveCustomersFromCache()
        {
            Remove("customer-cache");
        }

        public static void Remove(string key)
        {
            WebCache.Remove(key);
        }
    }
}