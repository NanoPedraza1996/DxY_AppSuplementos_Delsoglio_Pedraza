using DxY_AppSuplementos.Data;
using DxY_AppSuplementos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DxY_AppSuplementos.Controllers;

public class ProductosController : Controller
{
    private DxY_DbContext _contexto;
    //private ApplicationDbContext _context; , ApplicationDbContext context  _context = context;

    public ProductosController(DxY_DbContext contexto)
    {
        _contexto = contexto;
    }


    public IActionResult Index()
    {
        // Crear una lista de SelectListItem que incluya el elemento adicional
        var SelectListItem = new List<SelectListItem>
        {
            new SelectListItem {Value = "0", Text = "[SELECCIONE...]" }
        };

        var categoria = _contexto.Categorias.Where(t => t.Eliminado == false).OrderBy(c => c.Descripcion).ToList();

        categoria.Add(new Categoria { CategoriaID = 0, Descripcion = "[SELECCIONE..]" });
        ViewBag.CategoriaID = new SelectList(categoria.OrderBy(t => t.Descripcion), "CategoriaID", "Descripcion");


        return View();
    }


    public JsonResult BuscarProducto(int? id)
    {
        //List<VistaProductosMostrar> ProductosMostrar = new List<VistaProductosMostrar>();
        var productos = _contexto.Productos.ToList();
        if (id != null)
        {
            productos = productos.Where(p => p.ProductoID == id).ToList();
        }

        foreach (var producto in productos)
        {
            if (producto.Imagen != null)
            {
                producto.ImagenBase64 = Convert.ToBase64String(producto.Imagen);
            }

        }

        // var mostrarProducto = productos.Select(p => new VistaProductosMostrar
        // {
        //     ProductoID = p.ProductoID,
        //     Nombre = p.Nombre,
        //     Descripcion = p.Descripcion,
        //     FechaRegistro = p.FechaRegistro,
        //     FechaRegistroString = p.FechaRegistro.ToString("yyyy/MM/dd"),
        //     PrecioCompra = p.PrecioCompra,
        //     PrecioVenta = p.PrecioVenta,
        //     Stock = p.Stock,
        //     CategoriaID = p.CategoriaID,
        //     CategoriaIDNombre = p.Categoria.Descripcion,

        // }).ToList();

        return Json(productos);
    }

    public JsonResult GuardarProductos(int productoID, string nombre, string descripcion, DateTime fechaRegistro, int precioCompra, int precioVenta, int stock, int categoriaID, IFormFile imagen)
    {

        string resultado = "";

        // if (categoriaID == 0)
        // {
        if (!string.IsNullOrEmpty(nombre))
        {
            if (productoID == 0)
            {
                var exixteProducto = _contexto.Productos.Where(p => p.Nombre == nombre).Count();
                if (exixteProducto == 0)
                {
                    var producto = new Producto
                    {
                        Nombre = nombre.ToUpper(),
                        Descripcion = descripcion,
                        FechaRegistro = DateTime.Now,
                        PrecioCompra = precioCompra,
                        PrecioVenta = precioVenta,
                        Stock = stock,
                        CategoriaID = categoriaID
                    };

                    if (imagen != null && imagen.Length > 0)
                    {
                        byte[] imagenBinaria = null;
                        using (var fs1 = imagen.OpenReadStream())
                        using (var ms1 = new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            imagenBinaria = ms1.ToArray();
                        }
                        producto.Imagen = imagenBinaria;
                        producto.TipoImagen = imagen.ContentType;
                        producto.NombreImagen = imagen.FileName;
                    }

                    _contexto.Add(producto);
                    _contexto.SaveChanges();

                }
                else
                {
                    resultado = "El Producto Ya Existe.";
                }
            }
            else
            {
                //QUIERE DECIR QUE VAMOS A EDITAR LA CATEGORIA
                var productoEditar = _contexto.Productos.Where(p => p.ProductoID == productoID).SingleOrDefault();
                if (productoEditar != null)
                {
                    productoEditar.Nombre = nombre.ToUpper();
                    productoEditar.Descripcion = descripcion;
                    productoEditar.FechaRegistro = DateTime.Now;
                    productoEditar.PrecioCompra = precioCompra;
                    productoEditar.PrecioVenta = precioVenta;
                    productoEditar.CategoriaID = categoriaID;
                    _contexto.SaveChanges();
                }
            }

            // }
            resultado = "Por Favor Ingrese Un Tipo De Categoria";
        }

        return Json(resultado);
    }

    public JsonResult DesahabilitarProducto(int productoID, int disponibilidad)
    {
        string resultado = "";
        var producto = _contexto.Productos.Find(productoID);
        if (producto != null)
        {
            if (disponibilidad == 0)
            {
                producto.Disponibilidad = false;
                _contexto.SaveChanges();
            }
            else
            {
                var ProducEnDetaVent = _contexto.DetalleVentas.Where(v => v.ProductoID == productoID).Count();
                if (ProducEnDetaVent == 0)
                {
                    producto.Disponibilidad = true;
                    _contexto.SaveChanges();
                }
                else
                {
                    resultado = "No Se Pudo Deshabilitar Porque Esta Realcionado Con Productos.";
                }
            }
            // resultado = "Se Pudo Deshabilitar Correctamente.";
        }


        return Json(resultado);
    }

    public JsonResult EliminarProducto(int productoID, int eliminado)
    {
        string resultado = "";
        var eliminarProducto = _contexto.Productos.Find(productoID);
        if (eliminarProducto != null)
        {
            var ProducEnDetaVenta = _contexto.DetalleVentas.Where(d => d.ProductoID == productoID).Count();
            if (ProducEnDetaVenta == 0)
            {
                eliminado = 0;
                _contexto.Remove(eliminarProducto);
                _contexto.SaveChanges();
            }
            else
            {
                eliminado = 1;
                _contexto.SaveChanges();
                resultado = "NO";
            }
        }

        return Json(resultado);
    }
}
