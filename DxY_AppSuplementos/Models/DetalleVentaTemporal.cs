using System.ComponentModel.DataAnnotations;

namespace DxY_AppSuplementos.Models;

public class DetalleVentaTemporal
{
    [Key]
    public int DetalleVentaTemporalID { get; set; }

    public decimal PrecioVenta { get; set; }

    public int Cantidad { get; set; }

    public decimal SubTotal { get; set; }

    public int ProductoID { get; set; }

    public string? Nombre { get; set; }
}

public class VistaMostrarDetalleTemporal
{
    public int DetalleVentaTemporalID { get; set; }

    public decimal PrecioVenta { get; set; }

    public int Cantidad { get; set; }

    public decimal SubTotal { get; set; }

    public int ProductoID { get; set; }

    public string? Nombre { get; set; }
}