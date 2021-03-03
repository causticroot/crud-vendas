using System;
using System.Linq;
using System.Collections.Generic;

namespace CrudVendasMvc.Models.ViewModels
{
    public class VendedorViewModel
    {
        public Vendedor Vendedor { get; set; }
        public ICollection<Departamento> Departamentos { get; set; }
    }
}