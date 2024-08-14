using DxY_AppSuplementos.Data;
using Microsoft.AspNetCore.Mvc;

namespace DxY_AppSuplementos.Controllers;

public class PedidosController : Controller
{
    private DxY_DbContext _contexto;

    private ApplicationDbContext _context;

    public PedidosController(DxY_DbContext contexto, ApplicationDbContext context)
    {
        _contexto = contexto;
        _context = context;
    }

    public IActionResult Index()
    {
        // // Crear una lista de SelectListItem que incluya el elemento adicional
        // var SelectListItem = new List<SelectListItem>
        // {
        //     new SelectListItem {Value = "0", Text = "[SELECCIONE...]" }
        // };

        // // Obtener todas las opciones del enum
        // var enumValue = Enum.GetValues(typeof(EstadoEmocional)).Cast<EstadoEmocional>();

        // // Convertir las opciones del enum en SelectListItem
        // SelectListItem.AddRange(enumValue.Select(e => new SelectListItem
        // {
        //     Value = e.GetHashCode().ToString(),
        //     Text = e.ToString().ToUpper()
        // }));

        // // Pasar la lista de opciones al modelo de la vista
        // ViewBag.EstadoEmocionalInicio = SelectListItem.OrderBy(e => e.Text).ToList();
        // ViewBag.EstadoEmocionalFin = SelectListItem.OrderBy(e => e.Text).ToList();

        // var tipoEjercicio = _context.TipoEjercicios.Where(t => t.Eliminado == false).ToList();
        // var TipoEjerciciosBuscar = tipoEjercicio.ToList();

        // tipoEjercicio.Add(new TipoEjercicio { TipoEjercicioID = 0, Descripcion = "[SELECCIONE..]" });
        // ViewBag.TipoEjercicioID = new SelectList(tipoEjercicio.OrderBy(t => t.Descripcion), "TipoEjercicioID", "Descripcion");

        // TipoEjerciciosBuscar.Add(new TipoEjercicio { TipoEjercicioID = 0, Descripcion = "[TODOS LOS TIPOS DE EJERCICIOS]" });
        // ViewBag.TipoEjerciciosIDBuscar = new SelectList(TipoEjerciciosBuscar.OrderBy(t => t.Descripcion), "TipoEjercicioID", "Descripcion");


        return View();
    }

    // public JsonResult ListadoVentas(int? id)
    // {
    //     var venta = _contexto.Ventas.Include(v => v.Cliente).ToList();
    //     if (id != null)
    //     {
    //         venta = venta.Where(v => v.VentaID == id).ToList();
    //     }

    //     var ventas = venta.Select(v => new MostrarVistaVentas
    //     {
    //         VentaID = v.VentaID,
    //         Fecha = v.Fecha,
    //         FechaString = v.Fecha.ToString("dd/MM/yyyy"),
    //         TotalAPagar = v.TotalAPagar,
    //         ClienteID = v.ClienteID,
    //         ClienteIDNombre = v.Cliente.NombreCompleto
    //     }).ToList();

    //     return Json(ventas);
    // }


    // public JsonResult ListadoDetalleVentaTemporal(int? id)
    // {

    //     var detalleTemporal = _contexto.DetalleVentaTemporales.ToList();
    //     if (id != null)
    //     {
    //         detalleTemporal = detalleTemporal.Where(d => d.DetalleVentaTemporalID == id).ToList();
    //     }

    //     var MostrardetalleTemporal = detalleTemporal.Select(d => new VistaMostrarDetalleTemporal
    //     {
    //         DetalleVentaTemporalID = d.DetalleVentaTemporalID,
    //         PrecioVenta = d.PrecioVenta,
    //         Cantidad = d.Cantidad,
    //         SubTotal = d.SubTotal,
    //         ProductoID = d.ProductoID,
    //         Nombre = d.Nombre
    //     }).ToList();
    //     // List<DetalleVentaTemporal> detalleVentaTemporales = new List<DetalleVentaTemporal>();

    //     // var detalleTemporal = _contexto.DetalleVentaTemporales.ToList();
    //     // foreach (var temporal in detalleTemporal)
    //     // {
    //     //     detalleVentaTemporales.Add(temporal);
    //     // }


    //     return Json(MostrardetalleTemporal);
    // }


    // public JsonResult ListaDetallesVentas(int ventaID)
    // {
    //     var detalleVenta = _contexto.DetalleVentas.ToList();
    //     if (ventaID != 0)
    //     {
    //         detalleVenta = detalleVenta.Where(d => d.VentaID == ventaID).ToList();
    //     }

    //     var detalleVentas = detalleVenta.Select(d => new MostrarVistaDetalleVenta
    //     {
    //         DetalleVentaID = d.DetalleVentaID,
    //         PrecioVenta = d.PrecioVenta,
    //         Cantidad = d.Cantidad,
    //         SubTotal = d.SubTotal,
    //         VentaID = d.VentaID,
    //         ProductoID = d.ProductoID
    //     }).ToList();
    //     // List<DetalleVenta> detalleVenta = new List<DetalleVenta>();

    //     // var detalleVentas = _contexto.DetalleVentas.Where(d => d.DetalleVentaID == ventaID).SingleOrDefault();
    //     // if (detalleVentas != null)
    //     // {
    //     //     detalleVentas = detalleVentas.Where(d => d.DetalleVentaID == ventaID).ToList();
    //     //     foreach (var temporal in detalleVentas)
    //     //     {
    //     //         detalleVenta.Add(temporal);
    //     //     }
    //     // }

    //     return Json(detalleVentas);
    // }
}
