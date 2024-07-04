using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DxY_AppSuplementos.Models;

public class Categoria
{
    [Key]
    public int CategoriaID { get; set; }


    [Display(Name = "Descripcion.")]
    [Required(ErrorMessage = "La Descripcion Es Obligatorio.")]
    [MaxLength(50, ErrorMessage = "El Largo Maximo Es de {0} Caracteres.")]
    public string? Descripcion { get; set; }


    [Display(Name = "Fecha De Registro.")]
    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
    public DateTime FechaRegistro { get; set; }


    [Display(Name = "Disponibilidad.")]
    public bool Disponibilidad { set; get; }
    // public bool Disponibilidad { get; set; }


    public bool Eliminado { get; set; }


    public virtual ICollection<Producto>? Productos { get; set; }
}


public enum Disponibilidad
{
    Disponible = 1,
    NoDispoible,
}