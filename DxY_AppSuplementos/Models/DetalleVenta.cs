using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DxY_AppSuplementos.Models;

public class DetalleVenta
{
    [Key]
    public int DetalleVentaID {get; set;}
    public DateTime Fecha {get; set;}
    public decimal PrecioVenta {get; set;}
    public int Cantidad {get; set;}
    public decimal SubTotal {get; set;}
    public int VentaID {get; set;}
    public int ProductoID {get; set;}
    public int PromocionID {get; set;}
}