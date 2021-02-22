using System;
using System.Collections.Generic;
using System.Linq;

namespace CrudVendasMvc.Models
{
    public class Departamento
    {
        // Propriedades
        public int Id { get; set; }
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

        // Comportamentos
        public void AddVendedor(Vendedor vendedor)
        {
            Vendedores.Add(vendedor);
        }
        public double TotalVendas(DateTime periodoInicial, DateTime periodoFinal)
        {
            return Vendedores.Sum(vendedor => vendedor.TotalVendas(periodoInicial, periodoFinal));
        }
    }
}