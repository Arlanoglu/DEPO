using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCMailSender.Models.Entity
{
    public class AppUser
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "kullanıcı adı zorunlu")]
        [Display(Name = "Kullanıcı adı")]
        public string Username { get; set; }

        [Required(ErrorMessage = "şifre zorunlu")]
        [Display(Name = "Şifre")]
        public string Password { get; set; }

        public string Firstname { get; set; }
        public string Lastname { get; set; }

        [Required(ErrorMessage = "email zorunlu")]
        [EmailAddress(ErrorMessage = "email formatında bir adres girin.")]
        [Display(Name = "EPosta")]
        public string Email { get; set; }

        public bool IsActive { get; set; }

        //benzsersiz bir anahtar değer oluşturulmalı.
        public Guid ActivationKey { get; set; }

    }
}

//https://localhost:44348/Home/Activation/benzersizanahtardeger