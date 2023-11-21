using CusTrack.BusinessLayerr.Abstract;
using CusTrack.BusinessLayerr.Results;
using CusTrack.Entities;
using CusTrack.Entities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CusTrack.BusinessLayerr
{
    public class SeansManager:ManagerBase<Seans>
    {
        public new BusinessLayerResult<Seans> Update(Seans data)
        {
            Seans db_seans = Find(x => x.Id == data.Id);
            BusinessLayerResult<Seans> layerResult = new BusinessLayerResult<Seans>();
            DateTime bugün = DateTime.Today;

            if (db_seans != null)
            {
                if (data.IsDraft==true && data.Date > bugün)
                {
                    layerResult.AddError(ErrorMessageCode.IsDraftDateToday, "Gelecek zamanda muayene yapılmış olamaz");
                    return layerResult;
                }
            }
            layerResult.Result = Find(x => x.Id == data.Id);
            layerResult.Result.Title = data.Title;
            layerResult.Result.Text = data.Text;
            layerResult.Result.IsDraft = data.IsDraft;
            layerResult.Result.CustomerId = data.CustomerId;
            layerResult.Result.Date = data.Date;
            layerResult.Result.Money = data.Money;

            //Aynı sayfayı post edersek sonuc sıfır döndürüyor !!!
            base.Update(layerResult.Result);
                     
            return layerResult;

        }

        public new BusinessLayerResult<Seans> Insert(Seans data)
        {
            DateTime bugün = DateTime.Today;

            BusinessLayerResult<Seans> layerResult = new BusinessLayerResult<Seans>();
            layerResult.Result = data;

            if (data.IsDraft==true && data.Date>bugün)
            {
                layerResult.AddError(ErrorMessageCode.IsDraftDateToday, "Gelecek zamanda muayene yapılmış olamaz");
                return layerResult;
            }

            if (base.Insert(layerResult.Result)==0)
            {
                layerResult.AddError(ErrorMessageCode.SeansEklenemedi, "Seans Eklenemedi");
            }
            return layerResult;
        }
    }
}
