using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CrudVendasMvc.Models
{
    public class Departamento
    {
        // Propriedades
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} requerido")]
        [StringLength(60, MinimumLength = 4, ErrorMessage = "O tamanho do {0} deve está entre {2} e {1}.")]
        public string Nome { get; set; }
        
        public ICollection<Vendedor> Vendedores { get; set; } = new List<Vendedor>();

        // Construtores
        public Departamento()
        {

        }
        public Departamento(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }
}