using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Practica.Models
{
    public class FormularioGenreViewModel
    {
        public List<Formulario> Formulario { get; set; }
        public SelectList Busqueda { get; set; }
        public string FormularioBusqueda { get; set; }
        public string SearchString { get; set; }
    }
}
