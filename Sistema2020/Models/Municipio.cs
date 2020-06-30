using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema2020.Models
{
    public class Municipio
    {
        public int Id { get; set; }
        [Display(Name = "Código")]
        [Required(ErrorMessage = "{0} obrigatório")]
        [Range(1000000, 9999999, ErrorMessage = "{0} deve ter 7 digitos")]
        public int Codigo { get; set; }
        [Required(ErrorMessage = "{0} obrigatório")]
        public string Nome { get; set; }        
        public bool ZFM { get; set; }
        public int EstadoId { get; set; }
        public Estado Estado { get; set; }

        public Municipio(int id, Estado estado, int codigo, string nome, bool zFM)
        {
            Id = id;
            Estado = estado;
            Codigo = codigo;
            Nome = nome;
            ZFM = zFM;
        }

        public Municipio()
        {
        }
    }
}
