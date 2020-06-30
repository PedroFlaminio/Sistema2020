using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema2020.Models
{
    public class Pais
    {
        public int Id { get; set; }

        [Display(Name = "Código")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public int Codigo { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Nome { get; set; }

        public Pais()
        {
        }

        public Pais(int id, int codigo, string nome)
        {
            Id = id;
            Codigo = codigo;
            Nome = nome;
        }
    }
}
