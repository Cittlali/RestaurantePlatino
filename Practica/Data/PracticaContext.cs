using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Practica.Models
{
    public class PracticaContext : DbContext
    {
        public PracticaContext (DbContextOptions<PracticaContext> options)
            : base(options)
        {
        }

        public DbSet<Practica.Models.Formulario> Formulario { get; set; }
    }
}
