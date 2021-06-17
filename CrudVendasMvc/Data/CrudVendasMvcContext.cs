using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace CrudVendasMvc.Models
{
    public class CrudVendasMvcContext : DbContext
    {
        // Propriedades
        public DbSet<Departamento> Departamento { get; set; }
        public DbSet<Vendedor> Vendedor { get; set; }
    
        //Construtor
        public CrudVendasMvcContext(DbContextOptions<CrudVendasMvcContext> options)
            :base(options)
            {
            }
    }
}
