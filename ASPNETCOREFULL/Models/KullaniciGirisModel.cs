using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCOREFULL.Models
{
    public class KullaniciGirisModel
    {
        [Required(ErrorMessage = "Kullanıcı Adı boş geçilemez.")]
        public string KullaniciAd { get; set; }
        [Required(ErrorMessage = "Şifre boş geçilemez.")]
        public string Sifre { get; set; }
        public bool BeniHatirla { get; set; }
    }
}
