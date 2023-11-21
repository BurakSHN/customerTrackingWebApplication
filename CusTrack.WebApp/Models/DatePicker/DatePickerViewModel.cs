using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CusTrack.WebApp.Models.DatePicker
{
    public class DatePickerViewModel
    {

        [DisplayName("Başlık")]
        public string Title { get; set; }

        [DisplayName("Açıklama")]
        public string Text { get; set; }

        [DisplayName("Seans gerçekleşti mi")]
        public bool IsDraft { get; set; }

        [DisplayName("Bir sonraki seans tarihi")]
        public DateTime Date { get; set; }

        [DisplayName("Ücret")]
        public int Money { get; set; }

        [DisplayName("Müşteri Adı")]
        public int CustomerId { get; set; }
    }
}