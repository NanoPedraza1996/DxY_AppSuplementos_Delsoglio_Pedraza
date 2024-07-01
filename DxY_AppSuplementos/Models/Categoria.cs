using System.ComponentModel.DataAnnotations;

namespace DxY_AppSuplementos.Models;

public class Categoria
{
    [Key]
    public int CategoriaID { get; set; }


    // [Display(Name = "Telefono.")]
    // [Required(ErrorMessage = "El N° De Telefono Es Obligatorio.")]
    // [RegularExpression("^[1-10000]", ErrorMessage = "Solo Se Aceptan Numeros En El Campo.")]
    // [StringLength(50, MinimumLength = 7, ErrorMessage = "Solo Se Aceptan 7 o Mas Digito En El Campo.")]
    [Display(Name = "Descripcion.")]
    [Required(ErrorMessage = "La Descripcion Es Obligatorio.")]
    [MaxLength(50, ErrorMessage = "El Largo Maximo Es de {0} Caracteres.")]
    public string? Descripcion { get; set; }


    [Display(Name = "Fecha De Registro.")]
    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
    public DataType FechaRegistro { get; set; }


    [Display(Name = "Disponibilidad.")]
    public Disponibilidad Disponibilidad { set; get; }
    // public bool Disponibilidad { get; set; }


    public bool Eliminado { get; set; }


    public virtual ICollection<Producto>? Productos { get; set; }
}


public enum Disponibilidad
{
    Disponible = 1,
    NoDispoible,
}