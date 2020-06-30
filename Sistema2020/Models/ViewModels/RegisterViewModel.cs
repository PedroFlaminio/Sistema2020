using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema2020.Models.ViewModels
{
    public class RegisterViewModel
    {

        [Display(Name = "Usuário")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Password { get; set; }


        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

    }
}
