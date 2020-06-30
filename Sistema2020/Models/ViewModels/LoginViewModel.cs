using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema2020.Models.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Usuário")]
        public string UserName { get; set; }

        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
