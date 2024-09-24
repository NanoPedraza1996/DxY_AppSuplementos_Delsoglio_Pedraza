using DxY_AppSuplementos.Data;
using DxY_AppSuplementos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DxY_AppSuplementos.Controllers;

public class VistasController : Controller
{
    private DxY_DbContext _contexto;

    public VistasController(DxY_DbContext contexto)
    {
        _contexto = contexto;
    }

    public IActionResult Index()
    {
        // var producto = _contexto.Productos.Include(p => p.Categoria).ToList();
        // var categorias = _contexto.Categorias.ToList();
        // var vistaMostrarCategorias = producto.Select(p => new 
        // {

        // }).ToList();
        // var categorias_productos = (from categorias in _contexto.Categorias
        //                             join productos in _contexto.Productos
        //                                on categorias.CategoriaID equals productos.CategoriaID
        //                             select new
        //                             {
        //                                 categorias.CategoriaID,
        //                                 categorias.Descripcion,
        //                                 idProducto = productos.ProductoID,
        //                                 nombreProducto = productos.Nombre
        //                             }).ToList();
        // var categoriasProd =
        // (from categorias in _contexto.Categorias
        //  join productos in _contexto.Productos
        //     on categorias.CategoriaID equals
        //                    productos.CategoriaID
        //         into grupo
        //  select new
        //  {
        //      Categoria = categorias.Descripcion,
        //      TotalProductos = grupo.Count()
        //  }).ToList();

        return View();
    }

    public JsonResult MostrarVista()
    {
        // var categoriasProd =
        // (from categorias in _contexto.Categorias
        //  join productos in _contexto.Productos
        //     on categorias.CategoriaID equals
        //                    productos.CategoriaID
        //         into grupo
        //  select new
        //  {
        //      Categoria = categorias.Descripcion,
        //      TotalProductos = grupo.Count()
        //  }).ToList();
        List<VistasDeModelos> vistasDeModelos = new List<VistasDeModelos>();


        var produc = _contexto.Productos.Include(p => p.Categoria).ToList();

        foreach (var recorrer in produc)
        {
            var cate = vistasDeModelos.Where(c => c.CategoriaID == recorrer.CategoriaID).SingleOrDefault();
            if (cate == null)
            {
                cate = new VistasDeModelos
                {
                    CategoriaID = recorrer.CategoriaID,
                    Descripcion = recorrer?.Categoria?.Descripcion,
                    vistaProductosMostrarss = new List<VistaProductosMostrar>()
                };
                vistasDeModelos.Add(cate);
            }
            var mostrar = new VistaProductosMostrar
            {
                ProductoID = recorrer.ProductoID,
                Nombre = recorrer.Nombre,
                FechaRegistroString = recorrer.FechaRegistro.ToString("dd/MM/yyyy")
            };
            cate?.vistaProductosMostrarss?.Add(mostrar);
        }
        return Json(vistasDeModelos);
    }

    public JsonResult MostrarVentas()
    {
        return Json(true);
    }





}