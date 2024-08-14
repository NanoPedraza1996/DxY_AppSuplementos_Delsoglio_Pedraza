using DxY_AppSuplementos.Models;
using Microsoft.EntityFrameworkCore;

namespace DxY_AppSuplementos.Data;

public class DxY_DbContext : DbContext
{
    public DxY_DbContext(DbContextOptions<DxY_DbContext> options)
    : base(options)
    {
    }
    public DbSet<Categoria> Categorias { get; set; }

    public DbSet<Producto> Productos { get; set; }

    public DbSet<Promocion> Promociones { get; set; }

    public DbSet<DetallePromocion> DetallePromociones { get; set; }

    public DbSet<Venta> Ventas { get; set; }

    public DbSet<DetalleVenta> DetalleVentas { get; set; }

    public DbSet<Pedido> Pedidos { get; set; }

    public DbSet<DetallePedido> DetallePedidos { get; set; }

    public DbSet<Cliente> Clientes { get; set; }

    public DbSet<DetalleVentaTemporal> DetalleVentaTemporales { get; set; }

    public DbSet<DetallePedidoTemporal> DetallePedidoTemporales { get; set; }
}