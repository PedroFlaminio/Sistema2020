using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema2020.Models
{
    public class Estado
    {
        public int Id { get; set; }
        [Display(Name = "Código")]
        [Required(ErrorMessage = "Campo obrigatório")]
        [Range(10, 99, ErrorMessage = "{0} deve ter 2 digitos")]
        public int Codigo { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [MaxLength(2, ErrorMessage = "{0} deve ter 2 digitos")]
        [MinLength(2, ErrorMessage = "{0} deve ter 2 digitos")]
        public string Sigla { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Nome { get; set; }

        public ICollection<Municipio> Municipios { get; set; } = new List<Municipio>();

        public Estado()
        {
        }

        public Estado(int id, int codigo, string sigla, string nome)
        {
            Id = id;
            Codigo = codigo;
            Sigla = sigla;
            Nome = nome;
        }

        public void AddMunicipio(Municipio mun)
        {
            Municipios.Add(mun);
        }
        public void RemoveMunicipio(Municipio mun)
        {
            Municipios.Remove(mun);
        }
    }
}
