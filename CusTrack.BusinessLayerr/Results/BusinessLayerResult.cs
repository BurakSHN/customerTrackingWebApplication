using CusTrack.Entities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CusTrack.BusinessLayerr.Results
{
    public class BusinessLayerResult<T> where T :class
    {
        //Hataları kodlama yapmadan önce ki list hali
        //public List<string> Errors { get; set; }

        public List<ErrorMessageObj> Errors { get; set; }

        public T Result { get; set; }

        //new lendiğinde çalışıcak fonksiyon
        public BusinessLayerResult()
        {
            Errors = new List<ErrorMessageObj>();
        }

        //Kodlamayı kolaylastırmak için atama yapılan yer
        public void AddError(ErrorMessageCode code,string message)
        {
            Errors.Add(new ErrorMessageObj() { Code = code, Message=message });
        }
    }
}
