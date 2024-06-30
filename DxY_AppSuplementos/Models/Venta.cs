using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DxY_AppSuplementos.Models;

public class Venta
{
    [Key]
    public int VentaID {get; set;}
    public DateTime Fecha {get; set;}
    public decimal MontoPago {get; set;}
    public decimal MontoCambio {get; set;}
    public decimal TotalAPagar {get; set;}
    public int Usuario {get; set;}
    public int ClienteID {get; set;}
}