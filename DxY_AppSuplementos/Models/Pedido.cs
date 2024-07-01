using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DxY_AppSuplementos.Models;

public class Pedido
{
    public int PedidoID {get; set;}
    public int ClienteID {get; set;}
    public decimal TotalAPagar {get; set;}
    public Estado Estado {get; set;}
    public DateTime FechaRegistro {get; set;}
    //public DateTime CondicionesDePago {get; set;}
}

public enum Estado
{
    Preparacion = 1,
    Preparado,
    Entregado, 
    Confirmado,
    Anulado
}