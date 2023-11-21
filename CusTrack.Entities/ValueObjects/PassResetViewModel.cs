using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CusTrack.Entities.ValueObjects
{
    public class PassResetViewModel
    {
        [DisplayName("Şifre"),
        Required(ErrorMessage = "{0} alanı boş geçilemez."),
        DataType(DataType.Password),
        StringLength(25, ErrorMessage = "{0} max. {1} karakter olmalı.")]
        public string Password { get; set; }

        [DisplayName("Şifre Tekrar"),
        Required(ErrorMessage = "{0} alanı boş geçilemez."),
        DataType(DataType.Password),
        StringLength(25, ErrorMessage = "{0} max. {1} karakter olmalı."),
        Compare("Password", ErrorMessage = "{0} ile {1} uyuşmuyor")]
        public string RePassword { get; set; }
    }
}
