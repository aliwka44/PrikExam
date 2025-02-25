﻿using System.ComponentModel.DataAnnotations;

namespace Prk1.ViewModels.Account
{
    public class RegisterVM
    {
        [Required]
        public string FullName { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataType(DataType.Password), Compare("Password")]
        public string RepidePassword { get; set; }
    }
}
