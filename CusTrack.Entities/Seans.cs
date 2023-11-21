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
    [Table("Seans")]
    public class Seans
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Başlık"), Required(ErrorMessage = "{0} alanı boş geçilemez")]
        public string Title { get; set; }

        [DisplayName("Açıklama")]
        public string Text { get; set; }

        [DisplayName("Seans gerçekleşti mi ?")]
        public bool IsDraft { get; set; }

        [DisplayName("Seans Tarihi")]
        public DateTime Date { get; set; }

        [DisplayName("Ücret")]
        public int Money { get; set; }

        [DisplayName("Müşteri Adı")]
        public int CustomerId { get; set; }

        //Bu seans kime yazıldı sahibi kim
        public virtual Customer Owner { get; set; }
    }
}
