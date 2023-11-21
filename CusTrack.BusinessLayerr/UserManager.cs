using CusTrack.BusinessLayerr.Abstract;
using CusTrack.BusinessLayerr.Results;
using CusTrack.Core.Helpers;
using CusTrack.Entities;
using CusTrack.Entities.Messages;
using CusTrack.Entities.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace CusTrack.BusinessLayerr
{
    public class UserManager:ManagerBase<User>
    {
        public BusinessLayerResult<User> LoginUser(LoginViewModel data)
        {
            //Giriş Kontrolü           
            BusinessLayerResult<User> layerResult = new BusinessLayerResult<User>();
            layerResult.Result = Find(x => x.Username == data.Username);

            //layerResult.Result = Find(x => x.Username == data.Username && x.Password == data.Password);

            bool sifreKontrol = Crypto.VerifyHashedPassword(layerResult.Result.Password, data.Password);

            //if (layerResult.Result == null)
            //{
            //    layerResult.AddError(ErrorMessageCode.UsernameOrPassWrong, "Kullanıcı adı ya da şifre uyuşmuyor");;
            //}

            if (layerResult.Result != null && sifreKontrol == false)
            {
                layerResult.AddError(ErrorMessageCode.UsernameOrPassWrong, "Kullanıcı adı ya da şifre uyuşmuyor");
            }

            if (layerResult.Result != null && sifreKontrol == true)
            {
                return layerResult;
            }

            return layerResult;
        }

        public new BusinessLayerResult<User> Insert(User data)
        {
            User user = Find(x => x.Username.ToLower() == data.Username.ToLower());
            BusinessLayerResult<User> layerResult = new BusinessLayerResult<User>();

            layerResult.Result = data;

            if (user != null)
            {
                if (user.Username.ToLower()==data.Username.ToLower())
                {
                    layerResult.AddError(ErrorMessageCode.YoneticiAdiKayitli, "Böyle bir yönetici mevcut");
                }
            }
            else
            {
                if (base.Insert(layerResult.Result)==0)
                {
                    layerResult.AddError(ErrorMessageCode.YoneticiEklenemedi, "Yönetici Kayıt Edilemedi");
                }
            }
            return layerResult;

        }

        public BusinessLayerResult<User> EmailSend(EmailSendViewModel data)
        {
            BusinessLayerResult<User> layerResult = new BusinessLayerResult<User>();
            layerResult.Result = Find(x => x.Email == data.Email);

            //TODO : Aktivasyon mail i atılıcak
            if (layerResult.Result !=null)
            {
                //Developer modda pc de calısıcak bunu kullan
                string siteUri = ConfigHelper.Get<string>("SiteRootUri");
                string activateUri = $"{siteUri}/Home/PassReset/{layerResult.Result.Id}";

                ////Domain de calısıcak bunu kullan
                //string activateUri = $"http://beacode.net/Home/PassReset/{layerResult.Result.Id}";

                string body = $"Merhaba {layerResult.Result.Username}<br><br>Parolanızı sıfırlamak için <a href='{activateUri}' target='_blank'>TIKLAYINIZ</a>.";

                MailHelper.SendMail(body, layerResult.Result.Email, "BGM Meltem Arabaci Parola Sıfırlma");
            }
            return layerResult;
        }

    }
}
