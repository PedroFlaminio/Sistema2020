using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema2020.Models
{
    public class Usuario : IdentityUser
    {
        public int FuncionarioId { get; set; }
        public bool Administrador { get; set; }
    }
}
