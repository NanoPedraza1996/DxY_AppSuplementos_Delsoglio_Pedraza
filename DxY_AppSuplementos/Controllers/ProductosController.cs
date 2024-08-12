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
        var productos = _contexto.Productos.ToList();
        if (id != null)
        {
            productos = productos.Where(p => p.ProductoID == id).OrderBy(p => p.CategoriaID).ToList();
        }

        foreach (var producto in productos)
        {
            if (producto.Imagen != null)
            {
                producto.ImagenBase64 = Convert.ToBase64String(producto.Imagen);
            }
        }

        // var mostrarProducto = productos.Select(p => new VistaProductos
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
        //     Imagen = p.Imagen

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

    public JsonResult EliminarProducto(int productoID)
    {
        string resultado = "";
        var eliminarProducto = _contexto.Productos.Find(productoID);
        _contexto.Remove(eliminarProducto);
        _contexto.SaveChanges();
        return Json(resultado);
    }
}
