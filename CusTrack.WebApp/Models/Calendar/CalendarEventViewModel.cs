using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CusTrack.WebApp.Models.Calendar
{
    public class CalendarEventViewModel
    {
        //Full Calendar içerik doldurabilmek için zorunlu attribute'ler
        public int id { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string color { get; set; }
        public bool allDay { get; set; }
        public string title { get; set; }
        //--------------------------------------------------------------
        public string Tarih { get; set; }
        public string TelNo { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public int Ucret { get; set; }
    }
}