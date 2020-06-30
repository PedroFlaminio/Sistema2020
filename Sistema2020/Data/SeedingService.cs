using Microsoft.AspNetCore.Identity;
using Sistema2020.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema2020.Data
{
    public class SeedingService
    {
        private Sistema2020Context _context;
        private UserManager<Usuario> _userManager;
        private IdentityContext _users;

        public SeedingService(
            Sistema2020Context context, 
            UserManager<Usuario> userManager,
            IdentityContext users)
        {
            _context = context;
            _userManager = userManager;
            _users = users;
        }

        public void Seed()
        {
            if (!_context.Pais.Any())
            {
                string path = @"D:\Atualiza\Paises.txt";
                StreamReader sr = null;
                try
                {
                    sr = File.OpenText(path);
                    while (!sr.EndOfStream)
                    {
                        string[] line = sr.ReadLine().Split(',');
                        _context.Pais.Add(new Pais(int.Parse(line[0]), int.Parse(line[1]), line[2]));
                    }
                }
                finally
                {
                    if (sr != null) sr.Close();
                    _context.SaveChanges();
                }
            }

            if (!_context.Estado.Any())
            {
                string path = @"D:\Atualiza\Estados.txt";
                StreamReader sr = null;
                try
                {
                    sr = File.OpenText(path);
                    while (!sr.EndOfStream)
                    {
                        string[] line = sr.ReadLine().Split(',');
                        _context.Estado.Add(new Estado(int.Parse(line[0]), int.Parse(line[1]), line[2], line[3]));
                    }
                }
                finally
                {
                    if (sr != null) sr.Close();
                    _context.SaveChanges();
                }
            }
            if (!_context.Municipio.Any())
            {
                string path = @"D:\Atualiza\Cidades.txt";
                StreamReader sr = null;
                try
                {
                    sr = File.OpenText(path);
                    while (!sr.EndOfStream)
                    {
                        string[] line = sr.ReadLine().Split(',');
                        _context.Municipio.Add(new Municipio(int.Parse(line[0]), _context.Estado.Where(est => est.Codigo == int.Parse(line[2])).FirstOrDefault(), int.Parse(line[1]), line[3], line[4] == "true"));
                    }
                }
                finally
                {
                    if (sr != null) sr.Close();
                    _context.SaveChanges();
                }
            }
        }
    }
}
