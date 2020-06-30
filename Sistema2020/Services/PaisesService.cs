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
    public class PaisesService
    {
        private readonly Sistema2020Context _context;

        public PaisesService(Sistema2020Context context)
        {
            _context = context;
        }

        public async Task<List<Pais>> FindAllAsync()
        {
            return await _context.Pais.ToListAsync();
        }
        public async Task InsertAsync(Pais obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }
        public async Task<Pais> FindByIdAsync(int id)
        {
            return await _context.Pais.FirstOrDefaultAsync(obj => obj.Id == id);
        }
        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Pais.FindAsync(id);
                _context.Pais.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new IntegrityException("Impossível existem dados ligados a esse registro");
            }
        }
        public async Task UpdateAsync(Pais obj)
        {
            bool hasAny = await _context.Pais.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id não encontrado");
            }
            try
            {
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
