using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DxY_AppSuplementos.Models;

public class Cliente
{
    [Key]
    public int ClienteID { get; set; }
    public string? Correo { get; set; }
    public string? Contrasenia { get; set; }
    public string? ConfirmarContrasenia { get; set; }
    public string? NombreCompleto { get; set; }
    public string? Telefono { get; set; }
    public DateTime FechaCreacion { get; set; }
    public bool Disponibilidad { get; set; }

    public virtual ICollection<Venta>? Ventas { get; set; }

    public virtual ICollection<Pedido>? Pedidos { get; set; }
}
