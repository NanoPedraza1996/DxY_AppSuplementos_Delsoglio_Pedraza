using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DxY_AppSuplementos.Models;

public class Venta
{
    [Key]
    public int VentaID { get; set; }
    public DateTime Fecha { get; set; }
    public decimal TotalAPagar { get; set; }
    public int ClienteID { get; set; }

    public virtual Cliente? Cliente { get; set; }

    public virtual ICollection<DetalleVenta>? DetalleVentas { get; set; }
}

public class MostrarVistaVentas
{
    public int VentaID { get; set; }

    public DateTime Fecha { get; set; }
    public string? FechaString { get; set; }

    public decimal TotalAPagar { get; set; }

    public int ClienteID { get; set; }
    public string? ClienteIDNombre { get; set; }
}