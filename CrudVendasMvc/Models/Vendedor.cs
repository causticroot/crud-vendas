using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace CrudVendasMvc.Models
{
    public class Vendedor
    {
        // Propriedades
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} requerido.")]
        [StringLength(60, MinimumLength = 4, ErrorMessage = "O tamanho do {0} deve está entre {2} e {1}.")]
        public string Nome { get; set; }
        
        
        [Required(ErrorMessage = "{0} requerido.")]
        [EmailAddress(ErrorMessage = "Insira um {0} válido.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        
        [Required(ErrorMessage = "{0} requerido.")]
        [Display(Name = "Data de nascimento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }
        
        
        [Required(ErrorMessage = "{0} requerido.")]
        [Range(500.0, 20000.0, ErrorMessage = "O valor de {0} deve estar entre {1} e {2}.")]
        [DisplayFormat(DataFormatString = "R$ {0:F2}")]
        public double Salario { get; set; }
        
        
        public Departamento Departamento { get; set; }
        
        [Display(Name = "Departamento")]
        public int DepartamentoId { get; set; } // Referencial de intregridaade
        public ICollection<VendasRegistro> Vendas { get; set; } = new List<VendasRegistro>();

        // Construtores
        public Vendedor()
        {
            
        }
        public Vendedor(int id, string nome, string email, DateTime dataNascimento, double salario, Departamento departamento)
        {
            Id = id;
            Nome = nome;
            Email = email;
            DataNascimento = dataNascimento;
            Salario = salario;
            Departamento = departamento;
        }
    }
}