using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Practica.Models
{
    public class Formulario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo nombre es requerido")]
        [StringLength(20, MinimumLength =3,ErrorMessage ="El nombre ingresado no es valido")]
        [Display(Name = "Nombre")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "El campo apellido paterno es requerido")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "El apellido ingresado no es valido")]
        [Display(Name = "Apellido Paterno")]
        public string apellidoP { get; set; }

        [Required(ErrorMessage = "El campo apellido materno es requerido")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "El apellido ingresado no es valido")]
        [Display(Name = "Apellido Materno")]
        public string apellidoM { get; set; }

        [Required(ErrorMessage = "El campo usuario es requerido")]
        [StringLength(15, MinimumLength = 5, ErrorMessage = "Intenta con otro usuario")]

        [Display(Name = "Usuario")]
        public string usuario { get; set; }

        [Required(ErrorMessage = "El campo contraseña es requerido")]
        [StringLength(15, MinimumLength = 5, ErrorMessage = "Intenta con otra contraseña")]

        [Display(Name = "Contraseña")]
        public string contrasena { get; set; }

        [Required(ErrorMessage = "El campo telefono es requerido")]
       [Range (10000000,100000000, ErrorMessage ="El telefono ingresado debe contener 8 digitos")]
        [Display(Name = "Telefono")]
        public int telefono { get; set; }

        [Required(ErrorMessage = "El campo dirección es requerido")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "La dirección que ingresaste no es valida")]

        [Display(Name = "Dirección")]
        public string direccion { get; set; }
    

        





       
        
    }
}
