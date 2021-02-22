using System.Reflection.Emit;
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