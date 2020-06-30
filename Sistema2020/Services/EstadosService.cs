using Microsoft.EntityFrameworkCore;
using Sistema2020.Services.Exceptions;
using Sistema2020.Data;
using Sistema2020.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema2020.Services
{
    public class EstadosService
    {
        private readonly Sistema2020Context _context;

        public EstadosService(Sistema2020Context context)
        {
            _context = context;
        }

        public async Task<List<Estado>> FindAllAsync()
        {
            return await _context.Estado.ToListAsync();
        }
        public async Task InsertAsync(Estado obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }
        public async Task<Estado> FindByIdAsync(int id)
        {
            return await _context.Estado.FirstOrDefaultAsync(obj => obj.Id == id);
        }
        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Estado.FindAsync(id);
                _context.Estado.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new IntegrityException("Impossível existem dados ligados a esse registro");
            }
        }
        public async Task UpdateAsync(Estado obj)
        {
            bool hasAny = await _context.Estado.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id não encontrado");
            }
            try
            {
                obj.Sigla = obj.Sigla.ToUpper();
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }

    }
}
