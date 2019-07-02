using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Practica.Models;
using MailKit.Net.Smtp;
using MimeKit;

namespace Practica.Controllers
{
    public class FormulariosController : Controller
    {
        public readonly PracticaContext _context;
        public IConfiguration Configuration { get; }

        public FormulariosController(PracticaContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public async Task<IActionResult> Index(string FormularioBusqueda, string searchString)
        {
           
            IQueryable<string> genreQuery = from b in _context.Formulario
                                     
                                            orderby b.direccion
                                            select b.direccion;
            var busqueda = from b in _context.Formulario
                         select b;

            if (!String.IsNullOrEmpty(searchString))
            {
                busqueda = busqueda.Where(s => s.nombre.Contains(searchString));
            }
            if (!string.IsNullOrEmpty(FormularioBusqueda))
            {
                busqueda = busqueda.Where(x => x.direccion == FormularioBusqueda);
            }
            var formulariobusq = new FormularioGenreViewModel
            {
                Busqueda = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Formulario = await busqueda.ToListAsync()
            };

            return View(formulariobusq);
        }
      



        [HttpPost]
        public string Index(string searchString, bool notUsed)
        {
            return "From [HttpPost]Index: aceptar  " + searchString;
        }

        // GET: Formularios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formulario = await _context.Formulario
                .FirstOrDefaultAsync(m => m.Id == id);
            if (formulario == null)
            {
                return NotFound();
            }

            return View(formulario);
        }
        // GET: Formularios/Create
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Inicio()
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Restaurante Platino", "restauranteplatino2019@gmail.com"));
            message.To.Add(new MailboxAddress("Jose Perez", "joseperez123platino@gmail.com"));
            message.Subject = "Restaurante Platino";
            message.Body = new TextPart("plain")
            {
                Text = "En estos momentos se accedio al sistema"
            };
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("restauranteplatino2019@gmail.com", "123456789Restauranteplatino");
                client.Send(message);
                client.Disconnect(true);
            }
            return View();
        }

        // GET: Formularios/Create
        [HttpPost]
        public IActionResult Create(Formulario formulario)
        {
            if (ModelState.IsValid)
            {
                
                string connectionString = Configuration["ConnectionStrings:PracticaContext"];
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sql = $"Insert Into Formulario(nombre, apellidoP, apellidoM, usuario, contrasena, telefono, direccion) Values ('{formulario.nombre}', '{formulario.apellidoP}','{formulario.apellidoM}','{formulario.usuario}', '{formulario.contrasena}', '{formulario.telefono}', '{formulario.direccion}' )";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                        connection.Dispose();

                    }
                    return RedirectToAction("Index");
                }

            }
            else
                return View();
        }

    

  

    // GET: Formularios/Edit
    public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formulario = await _context.Formulario.FindAsync(id);
            if (formulario == null)
            {
                return NotFound();
            }
            return View(formulario);
        }

        // POST: Formularios/Edit

        [HttpPost]
        public IActionResult Edit(Formulario formulario)
        {
            if (ModelState.IsValid)
            {
                string connectionString = Configuration["ConnectionStrings:PracticaContext"];
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sql = $"UPDATE Formulario SET nombre='{formulario.nombre}', apellidoP ='{formulario.apellidoP}', apellidoM ='{formulario.apellidoM}', usuario ='{formulario.usuario}', contrasena ='{formulario.contrasena}', telefono ='{formulario.telefono}', direccion ='{formulario.direccion}' WHERE Id='{formulario.Id}'";

                  
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                        connection.Dispose();

                    }
                    return RedirectToAction("Index");
                }

            }
            else
                return View();
        }
                // GET: Formularios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formulario = await _context.Formulario
                .FirstOrDefaultAsync(m => m.Id == id);
            if (formulario == null)
            {
                return NotFound();
            }

            return View(formulario);
        }

        // POST: Formularios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var formulario = await _context.Formulario.FindAsync(id);
            _context.Formulario.Remove(formulario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FormularioExists(int id)
        {
            return _context.Formulario.Any(e => e.Id == id);
        }



        // GET: Formulario/ContactPDF
        public async Task<IActionResult> ContactPDF()
        {
            
            return new Rotativa.AspNetCore.ViewAsPdf("ContactPDF", await _context.Formulario.ToListAsync())
            {
                // Establece el número de página.
                CustomSwitches = "--page-offset 0 --footer-center [page] --footer-font-size 12"
            };
        }


    }
}


