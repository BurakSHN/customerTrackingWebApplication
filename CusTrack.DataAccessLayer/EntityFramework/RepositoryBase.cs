using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CusTrack.DataAccessLayer.EntityFramework
{   //Bu bir Singleton Patern dır.Singleton Patern : Bir nesnenin sadece proje çalışırken sadece bir kopyası olsun istiyorsak Singleton Patern          kullanırız.
    public class RepositoryBase
    {
        protected static DatabaseContext db;
        private static object _lock = new object();

        //Bu sınıf miras alındığında çalışır !!!
        //Bu sınıf artık new'lenemez.Sadece miras alan new'leyebilir !!!
        protected RepositoryBase()
        {
            CreateContext();
        }

        //Static metotlar new'lemeden çalışır !!!
        //Malta tred uygulamalar için bile Database'in bie kere oluşturulup aktarılmasını sağlıyor.
        //if'in içerisine aynı anda iki thread'tin girip işlem yaptırabiliyor lock sayesinde tek tek geçmeleri sağlanıyor.
        private static void CreateContext()
        {
            if (db==null)
            {
                lock (_lock)
                {
                    if (db==null)
                    {
                        db = new DatabaseContext();
                    }
                }
            }
        }


    }
}
