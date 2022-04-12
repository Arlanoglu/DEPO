using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCLogin.Models.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "kullanıcı adı zorunlu")]
        [Display(Name = "Kullanıcı adı")]
        public string Username { get; set; }

        [Required(ErrorMessage = "şifre zorunlu")]
        [Display(Name = "Şifre")]
        public string Password { get; set; }
    }
}