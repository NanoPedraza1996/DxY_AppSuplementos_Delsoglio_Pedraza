using DxY_AppSuplementos.Data;
using DxY_AppSuplementos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DxY_AppSuplementos.Controllers;


public class VentasController : Controller
{
    private DxY_DbContext _contexto;
    private ApplicationDbContext _context;

    public VentasController(DxY_DbContext contexto, ApplicationDbContext context)
    {
        _contexto = contexto;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Crear()
    {
        var SelectListItem = new List<SelectListItem>
        {
            new SelectListItem {Value = "0", Text = "[SELECCIONE...]"}
        };

        var producto = _contexto.Productos.Where(p => p.Eliminado == false).OrderBy(p => p.Descripcion).ToList();
        var cliente = _contexto.Clientes.Where(c => c.Disponibilidad == false).OrderBy(c => c.ClienteID).ToList();

        producto.Add(new Producto { ProductoID = 0, Descripcion = "[SELECCIONE...]" });
        ViewBag.ProductoID = new SelectList(producto.OrderBy(p => p.Descripcion), "ProductoID", "Nombre");

        cliente.Add(new Cliente { ClienteID = 0, NombreCompleto = "[SELECCIONE...]" });
        ViewBag.ClienteID = new SelectList(cliente.OrderBy(p => p.ClienteID), "ClienteID", "NombreCompleto");

        return View();
    }

    // public JsonResult BuscarVentas (int? id)
    // {
    //     string resultado = "";
    //     var venta = _contexto.Ventas.ToList();
    //     if (id != null)
    //     {
    //         venta = venta.Where(v => v.VentaID == id).ToList();
    //     }
    //     return Json(venta);
    // }

    public JsonResult ListadoDetalleVentaTemporal(int? id)
    {

        var detalleTemporal = _contexto.DetalleVentaTemporales.ToList();
        if (id != null)
        {
            detalleTemporal = detalleTemporal.Where(d => d.DetalleVentaTemporalID == id).ToList();
        }

        var MostrardetalleTemporal = detalleTemporal.Select(d => new VistaMostrarDetalleTemporal
        {
            DetalleVentaTemporalID = d.DetalleVentaTemporalID,
            PrecioVenta = d.PrecioVenta,
            Cantidad = d.Cantidad,
            SubTotal = d.SubTotal,
            ProductoID = d.ProductoID,
            Nombre = d.Nombre
        }).ToList();
        // List<DetalleVentaTemporal> detalleVentaTemporales = new List<DetalleVentaTemporal>();

        // var detalleTemporal = _contexto.DetalleVentaTemporales.ToList();
        // foreach (var temporal in detalleTemporal)
        // {
        //     detalleVentaTemporales.Add(temporal);
        // }


        return Json(MostrardetalleTemporal);
    }


    public JsonResult ListaDetallesVentas(int? id)
    {
        List<DetalleVenta> detalleVenta = new List<DetalleVenta>();

        var detalleVentas = _contexto.DetalleVentas.ToList();
        foreach (var temporal in detalleVentas)
        {
            detalleVenta.Add(temporal);
        }

        return Json(detalleVenta);
    }


    public JsonResult AgregarDetalleVenta(int detalleVentaID, int cantidad, decimal subTotal, int productoID)
    {
        string resultado = "";
        using (var transaccion = _contexto.Database.BeginTransaction())
        {
            try
            {
                var detalleTemporal = _contexto.DetalleVentas.Where(d => d.DetalleVentaID == detalleVentaID).Count();
                if (detalleTemporal == 0)
                {
                    var detalleventaExiste = _contexto.Productos.Where(d => d.ProductoID == productoID).SingleOrDefault();

                    var VentaTemp = new DetalleVentaTemporal
                    {
                        PrecioVenta = detalleventaExiste.PrecioVenta,
                        Cantidad = cantidad,
                        SubTotal = subTotal,
                        ProductoID = detalleventaExiste.ProductoID,
                        Nombre = detalleventaExiste.Nombre

                    };
                    _contexto.DetalleVentaTemporales.Add(VentaTemp);
                    _contexto.SaveChanges();
                    transaccion.Commit();
                }

                // transaccion.Commit();
            }
            catch (System.Exception)
            {
                transaccion.Rollback();
                resultado = "Hubo Un Error";
            }

        }

        ViewData["ProductoID"] = new SelectList(_contexto.Productos.ToList(), "ProductoID", "Nombre");
        return Json(resultado);
    }


    // public JsonResult AgregarDetalleVenta(int productoID, int detalleVentaTemporalID, int cantidad, decimal subTotal)
    // {
    //     string resultado = "";
    //     using (var transaccion = _contexto.Database.BeginTransaction())
    //     {
    //         try
    //         {

    //             var detalleventaExiste = _contexto.Productos.Where(d => d.ProductoID == productoID).SingleOrDefault();
    //             detalleventaExiste.Disponibilidad = true;
    //             _contexto.SaveChanges();

    //             var VentaTemp = new DetalleVentaTemporal
    //             {
    //                 PrecioVenta = detalleventaExiste.PrecioVenta,
    //                 ProductoID = detalleventaExiste.ProductoID,
    //                 Nombre = detalleventaExiste.Nombre
    //             };
    //             _contexto.DetalleVentaTemporales.Add(VentaTemp);
    //             _contexto.SaveChanges();


    //             // if (productoID != 0)
    //             // {
    //             //     var detalleTemporal = new DetalleVentaTemporal
    //             //     {
    //             //         Cantidad = cantidad,
    //             //         SubTotal = subTotal,
    //             //     };
    //             //     _contexto.Add(detalleTemporal);
    //             //     _contexto.SaveChanges();
    //             // }

    //             transaccion.Commit();
    //         }
    //         catch (System.Exception)
    //         {
    //             transaccion.Rollback();
    //             resultado = "Hubo Un Error";
    //         }

    //     }

    //     ViewData["ProductoID"] = new SelectList(_contexto.Productos.ToList(), "ProductoID", "Nombre");
    //     return Json(resultado);
    // }


    public JsonResult EliminarVentaDetalleTemporal(int detalleVentaTemporalID)
    {
        string resultado = "";
        using (var transaccion = _contexto.Database.BeginTransaction())
        {
            try
            {
                // var producto = _contexto.Productos.Where(p => p.ProductoID == productoID).SingleOrDefault();
                // producto.Disponibilidad = false;
                // _contexto.SaveChanges();

                var ventaTemporal = _contexto.DetalleVentaTemporales.Where(d => d.DetalleVentaTemporalID == detalleVentaTemporalID).SingleOrDefault();
                _contexto.DetalleVentaTemporales.Remove(ventaTemporal);
                _contexto.SaveChanges();


                transaccion.Commit();
            }
            catch (System.Exception)
            {
                transaccion.Rollback();
            }
        }
        return Json(resultado);
    }

    public JsonResult CancelarVenta()
    {
        string resultado = "";

        using (var transaccion = _contexto.Database.BeginTransaction())
        {
            try
            {
                var detalleTemporal = _contexto.DetalleVentaTemporales.ToList();

                foreach (var temporal in detalleTemporal)
                {
                    var detalleventa = _contexto.DetalleVentas.Where(d => d.DetalleVentaID == temporal.DetalleVentaTemporalID).SingleOrDefault();
                }
                _contexto.DetalleVentaTemporales.RemoveRange(detalleTemporal);
                _contexto.SaveChanges();

                transaccion.Commit();
            }
            catch (System.Exception)
            {
                transaccion.Rollback();
                resultado = "Disculpe, Hubo Un Error.";
            }
        }

        return Json(resultado);
    }


    public JsonResult GuardarVentas(int ventaID, decimal totalAPagar, int clienteID)
    {
        string resultado = "";
        using (var transaccion = _contexto.Database.BeginTransaction())
        {
            try
            {
                var existeVenta = _contexto.Ventas.Where(v => v.VentaID == ventaID).SingleOrDefault();
                var venta = new Venta
                {
                    VentaID = ventaID,
                    Fecha = DateTime.Now,
                    TotalAPagar = totalAPagar,
                    ClienteID = clienteID
                };
                _contexto.Add(venta);
                _contexto.SaveChanges();

                var detalleVenta = _contexto.DetalleVentaTemporales.ToList();
                foreach (var detalles in detalleVenta)
                {
                    var detalle = new DetalleVenta
                    {
                        VentaID = venta.VentaID,
                        PrecioVenta = detalles.PrecioVenta,
                        Cantidad = detalles.Cantidad,
                        SubTotal = detalles.SubTotal,
                        ProductoID = detalles.ProductoID,

                    };
                    _contexto.DetalleVentas.Add(detalle);
                    _contexto.SaveChanges();
                }
                _contexto.DetalleVentaTemporales.RemoveRange(detalleVenta);
                _contexto.SaveChanges();

                transaccion.Commit();

            }
            catch (System.Exception)
            {
                transaccion.Rollback();
                resultado = "Disculpe, Hubo Un Problema.";
            }
        }


        return Json(resultado);
    }
}