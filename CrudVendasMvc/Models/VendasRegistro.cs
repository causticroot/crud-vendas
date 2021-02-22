using System;
using CrudVendasMvc.Models.Enums;

namespace CrudVendasMvc.Models
{
    public class VendasRegistro
    {
        // Propriedades
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public double Quantia { get; set; }
        public VendaEstado Estado { get; set; }
        public Vendedor Vendedor { get; set; }
    }
}