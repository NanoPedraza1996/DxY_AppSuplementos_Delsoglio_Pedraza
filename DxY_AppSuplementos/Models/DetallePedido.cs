using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DxY_AppSuplementos.Models;

public class DetallePedido
{
    [Key]
    public int DetallePedidoID { get; set; }
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }
    public decimal SubTotal { get; set; }
    public int ProductoID { get; set; }
    public virtual Producto? Producto { get; set; }
    public int PedidoID { get; set; }
    public virtual Pedido Pedido { get; set; }

    //public int PromocionID {get; set;}
}


public class MostrarVistaDetallePedido
{
    public int DetallePedidoID { get; set; }
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }
    public decimal SubTotal { get; set; }
    public int ProductoID { get; set; }
    public string? ProductoIDString { get; set; }
    public int PedidoID { get; set; }
    public string? PedidoIDString { get; set; }
}