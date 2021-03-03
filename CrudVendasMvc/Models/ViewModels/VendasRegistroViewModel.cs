using System.Collections;
using System.Collections.Generic;
using CrudVendasMvc.Models.Enums;

namespace CrudVendasMvc.Models.ViewModels
{
    public class VendasRegistroViewModel
    {
        public VendasRegistro VendasRegistro { get; set; }
        public ICollection<VendaEstado> Estados { get; set; }
        public ICollection<Vendedor> Vendedores { get; set; }
        
        
    }
}