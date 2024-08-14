using System.ComponentModel.DataAnnotations;

namespace DxY_AppSuplementos.Models;

public class DetallePedidoTemporal
{
    [Key]
    public int DetallePedidoTemporalID { get; set; }
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }
    public decimal SubTotal { get; set; }
    public int ProductoID { get; set; }
    public string? Nombre { get; set; }
}