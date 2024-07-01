using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DxY_AppSuplementos.Models;

public class DetallePromocion
{
    [Key]
    public int DetallePromocionID { get; set; }

    [Display(Name = "Descripcion.")]
    [Required(ErrorMessage = "La Descripcion Es Obligatorio.")]
    [MaxLength(150, ErrorMessage = "El Largo Maximo Es de {0} Caracteres.")]
    public string? Descripcion { get; set; }


    [Display(Name = "Fecha De Registro.")]
    public Disponibilidad Disponibilidad { get; set; }


    [Display(Name = "Foto.")]
    public byte[]? Imagen { get; set; }
    public string? TipoImagen { get; set; }
    public string? NombreImagen { get; set; }


    [Display(Name = "Promociones.")]
    public int PromocionID { get; set; }
    public virtual Promocion? Promocion { get; set; }


    [Display(Name = "Productos.")]
    public int ProductoID { get; set; }
    public virtual Producto? Producto { get; set; }


    public bool Eliminado { get; set; }


    [NotMapped]
    public string? ImagenBase64 { get; set; }


}