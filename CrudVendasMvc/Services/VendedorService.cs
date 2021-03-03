using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CrudVendasMvc.Models;
using CrudVendasMvc.Services.Exceptions;

namespace CrudVendasMvc.Services
{
    public class VendedorService
    {
        private readonly CrudVendasMvcContext _context;

        public VendedorService(CrudVendasMvcContext context)
        {
            _context = context;
        }

        
        public async Task<List<Vendedor>> EncontrarTodosAsync()
        {
            //Classe Linq (Pesquisar depois)
            return await _context.Vendedor.ToListAsync();
        }

        public async Task<Vendedor> EncontrarPorIdAsync(int id)
        {
            return await _context.Vendedor
            .Include(x => x.Departamento)
            .FirstOrDefaultAsync(vendedor => vendedor.Id == id);
        }

        public async Task InserirAsync(Vendedor obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(int id)
        {
            try
            {
                var vendedor = await _context.Vendedor.FindAsync(id);
                _context.Vendedor.Remove(vendedor);
                await _context.SaveChangesAsync();       
            }
            catch (DbUpdateException ex)
            {
                throw new IntegrityException(ex.Message);
            }
        }

        public async Task AtualizarAsync(Vendedor obj)
        {
            //Any verifica a existência do registro no banco de dados
            // Se não existir...
            if (!await _context.Vendedor.AnyAsync(vendedor => vendedor.Id == obj.Id))
            {
                throw new NotFoundException("Id não encontrado.");
            }
            
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                 throw new DbConcurrencyException(ex.Message);
            }
        }
    }
}