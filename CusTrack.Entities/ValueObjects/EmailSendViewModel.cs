using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CusTrack.Entities.ValueObjects
{
    public class EmailSendViewModel
    {
        [DisplayName("E-Posta"), Required(ErrorMessage = "{0} alanı gereklidir."), StringLength(70)]
        public string Email { get; set; }
    }
}
