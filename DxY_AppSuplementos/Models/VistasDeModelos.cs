namespace DxY_AppSuplementos.Models;


public class VistasDeModelos
{

    // public int CategoriaID { get; set; }
    // public string? Descripcion { get; set; }
    // public int ProductoID { get; set; }
    // public string? Nombre { get; set; }
    // public List<Producto>? Productos { get; set; }
    // public List<Categoria>? Categorias { get; set; }


    public int CategoriaID { get; set; }
    public string? Descripcion { get; set; }
    // List<VistaProductosMostrar>? vistaProductosMostrars;
    public List<VistaProductosMostrar>? vistaProductosMostrarss { get; set; }

}


public class Vista
{

    public int ClienteID { get; set; }
    public string? ClienteIDNombre { get; set; }

    public int ProductoID { get; set; }
    public string? ProductoIDNombre { get; set; }

    public List<MostrarVistaDetalleVenta>? mostrarVistaDetalleVentas { get; set; }

    public List<MostrarVistaVentas>? mostrarVistaVentas { get; set; }


}