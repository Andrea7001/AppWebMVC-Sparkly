using System;
using System.ComponentModel.DataAnnotations;

namespace AppWebMVC_Sparkly.Models;

public class User
{
    public int Id { get; set; } // Identificador único del usuario

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    public string Nombre { get; set; } // Nombre del usuario

    [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
    [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
    public string Email { get; set; } // Correo electrónico del usuario

    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } // Contraseña del usuario

    [Required(ErrorMessage = "La fecha de cumpleaños es obligatoria.")]
    [DataType(DataType.Date)]
    public DateTime FechaCumpleanos { get; set; } // Fecha de cumpleaños del usuario

    [Required(ErrorMessage = "El género es obligatorio.")]
    public string Genero { get; set; } // Género del usuario
}

