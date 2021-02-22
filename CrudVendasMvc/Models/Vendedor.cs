using System;
using System.Collections.Generic;
using System.Linq;

namespace CrudVendasMvc.Models
{
    public class Vendedor
    {
        // Propriedades
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public double Salario { get; set; }
        public Departamento Departamento { get; set; }
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


        // Comportamentos
        public void AddVenda(VendasRegistro vr)
        {
            Vendas.Add(vr);
        }
        public void RemoveVenda(VendasRegistro vr)
        {
            Vendas.Remove(vr);
        }
        public double TotalVendas(DateTime periodoInicial, DateTime periodoFinal)
        {
            //Operação where filtra a pesquisa da lista -^
            return Vendas.Where(vr => vr.Data >= periodoInicial && vr.Data <= periodoFinal).Sum(vr => vr.Quantia);
        }


    }
}