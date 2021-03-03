using System.Security.AccessControl;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using CrudVendasMvc.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CrudVendasMvc.Services.Exceptions;

namespace CrudVendasMvc.Services
{
    public class VendasRegistroService
    {
        private readonly CrudVendasMvcContext _context;
        
        public VendasRegistroService(CrudVendasMvcContext context)
        {
            _context = context;
        }

        public async Task<VendasRegistro> EncontrarPorIdAsync(int id)
        {
            return await _context.VendasRegistro
            .Include(x => x.Vendedor)
            .FirstOrDefaultAsync(venda => venda.Id == id);
        }

        public async Task<List<VendasRegistro>> EncontrarTodosAsync()
        {
            return await _context.VendasRegistro
            .Include(x => x.Vendedor)//Para mostrar o nome do vendedor
            .OrderByDescending(x => x.Data)   
            .ToListAsync();
        }

        public async Task<List<VendasRegistro>> EncontrarPorDataAsync(DateTime? dataMin, DateTime? dataMax)
        {
            var resultado = from obj in _context.VendasRegistro select obj;

            if (dataMin.HasValue)
            {
                resultado = resultado.Where(x => x.Data >= dataMin.Value);
            }
            if (dataMax.HasValue)
            {
                resultado = resultado.Where(x => x.Data <= dataMax.Value);
            }
            return await resultado
                        .Include(x => x.Vendedor)
                        .Include(x => x.Vendedor.Departamento)
                        .OrderByDescending(x => x.Data)
                        .ToListAsync();
        }


        public async Task InserirAsync(VendasRegistro obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(int id)
        {
            try
            {
                var venda = await _context.VendasRegistro.FindAsync(id);
                _context.VendasRegistro.Remove(venda);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new IntegrityException(ex.Message);
            }
        }

        public async Task AtualizarAsync(VendasRegistro obj)
        {
            //Any verifica a existência do registro no banco de dados
            // Se não existir...
            if (!await _context.VendasRegistro.AnyAsync(venda => venda.Id == obj.Id))
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
                throw new DbUpdateConcurrencyException(ex.Message);
            }
        }
    }
}