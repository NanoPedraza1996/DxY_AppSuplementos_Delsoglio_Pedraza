using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DxY_AppSuplementos.Models;

public class Promocion
{
    [Key]
    public int PromocionID { get; set; }


    [Display(Name = "Nombre.")]
    [Required(ErrorMessage = "El Nombre Es Obligatorio.")]
    [MaxLength(150, ErrorMessage = "El Largo Maximo Es de {0} Caracteres.")]
    public string? Nombre { get; set; }


    [Display(Name = "Total A Pagar.")]
    public decimal TotalAPagar { get; set; }


    [Display(Name = "Fecha De Registro.")]
    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
    public DateTime FechaRegistro { get; set; }


    public virtual ICollection<DetallePromocion>? DetallePromociones { get; set; }


    public bool Eliminado { get; set; }

}