using System.Reflection.Emit;
using System;
using CrudVendasMvc.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace CrudVendasMvc.Models
{
    public class VendasRegistro
    {
        // Propriedades
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} requerido.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyy}")]
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }
        
        [Required(ErrorMessage = "{0} requerido.")]
        [DisplayFormat(DataFormatString = "R$ {0:F2}")]
        [Range(1, double.MaxValue, ErrorMessage = "{0} tem que ser maior ou igual à {1}")]
        public double Quantia { get; set; }
        
        
        public VendaEstado Estado { get; set; }
        
        //
        public Vendedor Vendedor { get; set; }
        
        [Display(Name = "Vendedor")]
        public int VendedorId { get; set; } //referencial de integridade

        // Construtores
        public VendasRegistro()
        {

        }

        public VendasRegistro(int id, DateTime data, double quantia, VendaEstado estado, Vendedor vendedor)
        {
            Id = id;
            Data = data;
            Quantia = quantia;
            Estado = estado;
            Vendedor = vendedor;
        }
    }
}