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

    public async Task<IActionResult> Index()
    {
        var ventas = _contexto.Ventas.Include(v => v.Cliente);
        return View(await ventas.ToListAsync());
    }

    public async Task<IActionResult> Crear()
    {
        var SelectListItem = new List<SelectListItem>
        {
            new SelectListItem {Value = "0", Text = "[SELECCIONE...]"}
        };

        var producto = await _contexto.Productos.Where(p => p.Eliminado == false).OrderBy(p => p.Descripcion).ToListAsync();
        var cliente = await _contexto.Clientes.Where(c => c.Disponibilidad == false).OrderBy(c => c.ClienteID).ToListAsync();

        producto.Add(new Producto { ProductoID = 0, Descripcion = "[SELECCIONE...]" });
        ViewBag.ProductoID = new SelectList(producto.OrderBy(p => p.Descripcion), "ProductoID", "Nombre");

        cliente.Add(new Cliente { ClienteID = 0, NombreCompleto = "[SELECCIONE...]" });
        ViewBag.ClienteID = new SelectList(cliente.OrderBy(p => p.ClienteID), "ClienteID", "NombreCompleto");

        return View();
    }


    public JsonResult ListadoVentas(int? id)
    {
        var venta = _contexto.Ventas.Include(v => v.Cliente).ToList();
        if (id != null)
        {
            venta = venta.Where(v => v.VentaID == id).ToList();
        }

        var ventas = venta.Select(v => new MostrarVistaVentas
        {
            VentaID = v.VentaID,
            Fecha = v.Fecha,
            FechaString = v.Fecha.ToString("dd/MM/yyyy"),
            TotalAPagar = v.TotalAPagar,
            ClienteID = v.ClienteID,
            ClienteIDNombre = v.Cliente.NombreCompleto
        }).ToList();

        return Json(ventas);
    }


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


    public JsonResult ListaDetallesVentas(int ventaID)
    {
        var detalleVenta = _contexto.DetalleVentas.ToList();
        if (ventaID != 0)
        {
            detalleVenta = detalleVenta.Where(d => d.VentaID == ventaID).ToList();
        }

        var detalleVentas = detalleVenta.Select(d => new MostrarVistaDetalleVenta
        {
            DetalleVentaID = d.DetalleVentaID,
            PrecioVenta = d.PrecioVenta,
            Cantidad = d.Cantidad,
            SubTotal = d.SubTotal,
            VentaID = d.VentaID,
            ProductoID = d.ProductoID
        }).ToList();
        // List<DetalleVenta> detalleVenta = new List<DetalleVenta>();

        // var detalleVentas = _contexto.DetalleVentas.Where(d => d.DetalleVentaID == ventaID).SingleOrDefault();
        // if (detalleVentas != null)
        // {
        //     detalleVentas = detalleVentas.Where(d => d.DetalleVentaID == ventaID).ToList();
        //     foreach (var temporal in detalleVentas)
        //     {
        //         detalleVenta.Add(temporal);
        //     }
        // }

        return Json(detalleVentas);
    }


    public async Task<JsonResult> AgregarDetalleVenta(int detalleVentaID, int cantidad, decimal subTotal, int productoID)
    {
        string resultado = "";
        if (detalleVentaID == 0)
        {
            using (var transaccion = await _contexto.Database.BeginTransactionAsync())
            {
                try
                {
                    var detalleTemporal = await _contexto.DetalleVentas.Where(d => d.DetalleVentaID == detalleVentaID).CountAsync();
                    if (detalleTemporal == 0)
                    {
                        var detalleventaExiste = await _contexto.Productos.Where(d => d.ProductoID == productoID).SingleOrDefaultAsync();

                        var VentaTemp = new DetalleVentaTemporal
                        {
                            PrecioVenta = detalleventaExiste.PrecioVenta,
                            Cantidad = cantidad,
                            SubTotal = subTotal,
                            ProductoID = detalleventaExiste.ProductoID,
                            Nombre = detalleventaExiste.Nombre

                        };
                        await _contexto.DetalleVentaTemporales.AddAsync(VentaTemp);
                        await _contexto.SaveChangesAsync();
                        await transaccion.CommitAsync();
                    }

                    // transaccion.Commit();
                }
                catch (System.Exception)
                {
                    transaccion.Rollback();
                    resultado = "Hubo Un Error";
                }

            }
        }

        //ViewData["ProductoID"] = new SelectList(_contexto.Productos.ToList(), "ProductoID", "Nombre");
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


    public async Task<JsonResult> EliminarVentaDetalleTemporal(int detalleVentaTemporalID)
    {
        string resultado = "";
        using (var transaccion = await _contexto.Database.BeginTransactionAsync())
        {
            try
            {
                // var producto = _contexto.Productos.Where(p => p.ProductoID == productoID).SingleOrDefault();
                // producto.Disponibilidad = false;
                // _contexto.SaveChanges();

                var ventaTemporal = await _contexto.DetalleVentaTemporales.Where(d => d.DetalleVentaTemporalID == detalleVentaTemporalID).SingleOrDefaultAsync();
                _contexto.DetalleVentaTemporales.Remove(ventaTemporal);
                await _contexto.SaveChangesAsync();


                await transaccion.CommitAsync();
            }
            catch (System.Exception)
            {
                transaccion.Rollback();
            }
        }
        return Json(resultado);
    }

    public async Task<JsonResult> CancelarVenta()
    {
        string resultado = "";

        using (var transaccion = await _contexto.Database.BeginTransactionAsync())
        {
            try
            {
                var detalleTemporal = await _contexto.DetalleVentaTemporales.ToListAsync();

                foreach (var temporal in detalleTemporal)
                {
                    var detalleventa = await _contexto.DetalleVentas.Where(d => d.DetalleVentaID == temporal.DetalleVentaTemporalID).SingleOrDefaultAsync();
                }
                _contexto.DetalleVentaTemporales.RemoveRange(detalleTemporal);
                await _contexto.SaveChangesAsync();

                await transaccion.CommitAsync();
            }
            catch (System.Exception)
            {
                transaccion.Rollback();
                resultado = "Disculpe, Hubo Un Error.";
            }
        }

        return Json(resultado);
    }





    public async Task<JsonResult> GuardarVentas(int ventaID, decimal totalAPagar, int clienteID)
    {
        string resultado = "";
        using (var transaccion = await _contexto.Database.BeginTransactionAsync())
        {
            try
            {
                var existeVenta = await _contexto.Ventas.Where(v => v.VentaID == ventaID).SingleOrDefaultAsync();
                var venta = new Venta
                {
                    VentaID = ventaID,
                    Fecha = DateTime.Now,
                    TotalAPagar = totalAPagar,
                    ClienteID = clienteID
                };
                await _contexto.AddAsync(venta);
                await _contexto.SaveChangesAsync();

                var detalleVenta = await _contexto.DetalleVentaTemporales.ToListAsync();
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
                    await _contexto.DetalleVentas.AddAsync(detalle);
                    await _contexto.SaveChangesAsync();
                }
                _contexto.DetalleVentaTemporales.RemoveRange(detalleVenta);
                //_contexto.DetalleVentaTemporales.UpdateRange();
                await _contexto.SaveChangesAsync();

                await transaccion.CommitAsync();

                // return Json(nameof(Index));
                return Json(RedirectToAction(nameof(Index)));
            }
            catch (System.Exception)
            {
                transaccion.Rollback();
                resultado = "Disculpe, Hubo Un Problema.";
            }
        }

        return Json(resultado);
    }



    public async Task<JsonResult> EliminarVenta(int ventaID)
    {
        string resultado = "";
        var eliminarVenta = await _contexto.Ventas.FindAsync(ventaID);
        _contexto.Remove(eliminarVenta);
        await _contexto.SaveChangesAsync();
        return Json(resultado);
    }

}