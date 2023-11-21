using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CusTrack.Entities
{
    [Table("Customers")]
    public class Customer
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Ad"),Required(ErrorMessage ="{0} alanı boş geçilemez")]
        public string Name { get; set; }

        [DisplayName("Soyad"), Required(ErrorMessage = "{0} alanı boş geçilemez")]
        public string Surname { get; set; }

        [DisplayName("Doğum Tarihi"), Required(ErrorMessage = "{0} alanı boş geçilemez")]
        public DateTime Birthday { get; set; }

        [DisplayName("Adres")]
        public string Address { get; set; }

        [DisplayName("Telefon No"), Required(ErrorMessage = "{0} alanı boş geçilemez")]
        public string Phone { get; set; }

        //Bir müşterinin Seansları vardır birden fazla olabileceği için List
        public virtual List<Seans> Seansing { get; set; }
    }
}
