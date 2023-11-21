using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CusTrack.Core.Helpers
{
    public class ConfigHelper
    {
        public static T Get<T>(string key)
        {
            //Refereans tan (System.configuration) u ekle
            return (T)Convert.ChangeType(ConfigurationManager.AppSettings[key], typeof(T));
        }
    }
}
