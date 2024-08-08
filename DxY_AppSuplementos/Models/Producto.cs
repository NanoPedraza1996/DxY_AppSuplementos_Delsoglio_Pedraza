using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DxY_AppSuplementos.Models;

public class Producto
{
    [Key]
    public int ProductoID { get; set; }


    [Display(Name = "Nombre.")]
    [Required(ErrorMessage = "El Nombre Es Obligatorio.")]
    [MaxLength(50, ErrorMessage = "El Largo Maximo Es de {0} Caracteres.")]
    public string? Nombre { get; set; }


    [Display(Name = "Descripcion.")]
    [Required(ErrorMessage = "La Descripcion Es Obligatorio.")]
    [MaxLength(150, ErrorMessage = "El Largo Maximo Es de {0} Caracteres.")]
    public string? Descripcion { get; set; }


    [Display(Name = "Fecha De Registro.")]
    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
    public DateTime FechaRegistro { get; set; }


    [Display(Name = "Precio De Compra.")]
    [Required(ErrorMessage = "El Precio De Compra Es Obligatorio.")]
    [RegularExpression("^[1-10000]", ErrorMessage = "Solo Se Aceptan Numeros En El Campo.")]
    [StringLength(25, MinimumLength = 2, ErrorMessage = "Solo Se Aceptan 2 o Mas Digito En El Campo.")]
    public decimal PrecioCompra { get; set; }


    [Display(Name = "Precio De Venta.")]
    [Required(ErrorMessage = "El Precio De Venta Es Obligatorio.")]
    [RegularExpression("^[1-1000000]", ErrorMessage = "Solo Se Aceptan Numeros En El Campo.")]
    [StringLength(25, MinimumLength = 2, ErrorMessage = "Solo Se Aceptan 2 o Mas Digito En El Campo.")]
    public decimal PrecioVenta { get; set; }


    [Display(Name = "Stock.")]
    [Required(ErrorMessage = "El Stock Es Obligatorio.")]
    [RegularExpression("^[1-1000000]", ErrorMessage = "Solo Se Aceptan Numeros En El Campo.")]
    public int Stock { get; set; }


    [Display(Name = "Categoria.")]
    public int CategoriaID { get; set; }
    public virtual Categoria? Categoria { get; set; }



    [Display(Name = "Foto.")]
    public byte[]? Imagen { get; set; }
    public string? TipoImagen { get; set; }
    public string? NombreImagen { get; set; }


    public bool Eliminado { get; set; }

    public bool Disponibilidad { get; set; }


    public virtual ICollection<DetallePromocion>? DetallePromociones { get; set; }

    public virtual ICollection<DetalleVenta>? DetalleVentas { get; set; }

    [NotMapped]
    public string? ImagenBase64 { get; set; }

}



// public class VistaProductos
// {
//     public int ProductoID { get; set; }


//     public string? Nombre { get; set; }


//     public string? Descripcion { get; set; }


//     public DateTime FechaRegistro { get; set; }
//     public string? FechaRegistroString { get; set; }


//     public decimal PrecioCompra { get; set; }


//     public decimal PrecioVenta { get; set; }


//     public int Stock { get; set; }


//     public int CategoriaID { get; set; }
//     public string? CategoriaIDNombre { get; set; }


//     public byte[]? Imagen { get; set; }

// }