using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using CrudVendasMvc.Models;
using CrudVendasMvc.Services.Exceptions;

namespace CrudVendasMvc.Services
{
    public class DepartamentoService
    {
        private readonly CrudVendasMvcContext _context;

        public DepartamentoService (CrudVendasMvcContext context)
        {
            _context = context;
        }

        public async Task<List<Departamento>> EncontrarTodosAsync()
        {
            return await _context.Departamento.OrderBy(by => by.Nome).ToListAsync();
        }

        public async Task<Departamento> EcontrarPorIdAsync(int id)
        {
            return await _context.Departamento
            .FirstOrDefaultAsync(departamento => departamento.Id == id);
        }

        public async Task InserirAsync(Departamento obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(int id)
        {
            try
            {
                var departamento = await _context.Departamento.FindAsync(id);
                _context.Departamento.Remove(departamento);
                await _context.SaveChangesAsync();
            }
            catch (System.Exception ex)
            {
                 throw new IntegrityException(ex.Message);
            }
        }

        public async Task AtualizarAsync(Departamento obj)
        {
            if (! await _context.Departamento.AnyAsync(departamento => departamento.Id == obj.Id))
            {
                throw new NotFoundException("Id não encontrado");
            }

            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbConcurrencyException ex)
            {
                
                throw new DbConcurrencyException(ex.Message);
            }
        }   
    }
}