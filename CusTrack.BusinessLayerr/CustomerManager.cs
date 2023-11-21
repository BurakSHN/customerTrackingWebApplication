using CusTrack.BusinessLayerr.Abstract;
using CusTrack.BusinessLayerr.Results;
using CusTrack.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CusTrack.BusinessLayerr
{
    public class CustomerManager:ManagerBase<Customer>
    {
        public new BusinessLayerResult<Customer> Insert(Customer data)
        {
            Customer user = Find(x => x.Name.ToLower() == data.Name.ToLower() && x.Surname.ToLower() == data.Surname.ToLower());
            BusinessLayerResult<Customer> layerResult = new BusinessLayerResult<Customer>();

            layerResult.Result = data;

            if (user != null)
            {
                if (user.Name.ToLower() == data.Name.ToLower())
                {
                    layerResult.AddError(Entities.Messages.ErrorMessageCode.MüşteriAdiKayitli, "Müşteri Adı Daha Önce Kayıt Edilmiş");
                }
                if (user.Surname.ToLower()==data.Surname.ToLower())
                {
                    layerResult.AddError(Entities.Messages.ErrorMessageCode.MüşteriSoyadiKayitli, "Müşteri Soyadı Daha Önce Kayıt Edilmiş");
                }
            }
            else
            {
                if (base.Insert(layerResult.Result)==0)
                {
                    layerResult.AddError(Entities.Messages.ErrorMessageCode.MusteriGuncellenemedi, "Müşteri Kayıt Edilemedi");
                }
            }
            return layerResult;
        }

        public new BusinessLayerResult<Customer> Update(Customer data)
        {
            Customer db_user = Find(x => x.Id != data.Id && (x.Name.ToLower() == data.Name.ToLower() && x.Surname.ToLower() == data.Surname.ToLower()));
            BusinessLayerResult<Customer> layerResult = new BusinessLayerResult<Customer>();

            if (db_user != null)
            {
                if (db_user.Name.ToLower() == data.Name.ToLower())
                {
                    layerResult.AddError(Entities.Messages.ErrorMessageCode.MüşteriAdiKayitli, "Müşteri Adı Daha Önce Kayıt Edilmiş");
                }
                if (db_user.Surname.ToLower() == data.Surname.ToLower())
                {
                    layerResult.AddError(Entities.Messages.ErrorMessageCode.MüşteriSoyadiKayitli, "Müşteri Soyadı Daha Önce Kayıt Edilmiş");
                }
                return layerResult;
            }

            layerResult.Result = Find(x => x.Id == data.Id);
            layerResult.Result.Name = data.Name;
            layerResult.Result.Surname = data.Surname;
            layerResult.Result.Birthday = data.Birthday;
            layerResult.Result.Address = data.Address;
            layerResult.Result.Phone = data.Phone;

            base.Update(layerResult.Result);

            return layerResult;
        }
    }
}
