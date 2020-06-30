using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sistema2020.Models;

namespace Sistema2020.Data
{
    public class Sistema2020Context : DbContext
    {
        public Sistema2020Context (DbContextOptions<Sistema2020Context> options)
            : base(options)
        {
        }

        public DbSet<Sistema2020.Models.Municipio> Municipio { get; set; }
        public DbSet<Sistema2020.Models.Estado> Estado { get; set; }
        public DbSet<Sistema2020.Models.Pais> Pais { get; set; }
    }
}
