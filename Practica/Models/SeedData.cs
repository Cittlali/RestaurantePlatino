using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace Practica.Models
{
    public class SeedData { 
      public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new PracticaContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<PracticaContext>>()))
        {
                //Busqua el  formulario.
            if (context.Formulario.Any())
            {
                return;   // Siembra datos en caso que se haya eliminado todos
            }
                context.Formulario.AddRange(
                    

                    new Formulario
                    {
                        nombre = "c ",
                        apellidoP = "m",
                        apellidoM = "b",
                        usuario = "u",
                        contrasena = "w",
                        telefono = 1,
                        direccion = "q",
                       
                    }
                       
                );
                context.SaveChanges();
            }
        }
    }
}