using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CusTrack.Entities.Messages
{
    public enum ErrorMessageCode
    {
        CustomerNameSurnameAlreadyExists=100,
        UsernameOrPassWrong = 101,
        IsDraftDateToday = 102,
        SeansBulunamadı = 103,
        SeansGüncellenemdi = 104,
        SeansEklenemedi = 105,
        MüşteriAdiKayitli = 106,
        MüşteriSoyadiKayitli = 107,
        MusteriGuncellenemedi = 108,
        YoneticiAdiKayitli = 109,
        YoneticiEklenemedi = 110,
    }
}
