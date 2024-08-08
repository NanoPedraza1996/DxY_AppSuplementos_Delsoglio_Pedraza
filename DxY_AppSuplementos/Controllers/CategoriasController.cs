using System.Diagnostics;
using DxY_AppSuplementos.Data;
using Microsoft.AspNetCore.Mvc;
using DxY_AppSuplementos.Models;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DxY_AppSuplementos.Controllers;

public class CategoriasController : Controller
{
    private DxY_DbContext _contexto;

    // private ApplicationDbContext _context; (Y despues para declararlo abajo utilizamos) 
    //, ApplicationDbContext context (y igualamos) _context = context;

    public CategoriasController(DxY_DbContext contexto)
    {
        _contexto = contexto;
    }

    public IActionResult Index()
    {
        return View();
    }

    public JsonResult ListadoCategorias(int? id)
    {
        //DEFINIMOS UNA VARIABLE EN DONDE GUARDAMOS EL LISTADO COMPLETO DE CATEGORIAS
        var categorias = _contexto.Categorias.ToList();

        //LUEGO PREGUNTAMOS SI EL USUARIO INGRESO UN ID
        //QUIERE DECIR QUE QUIERE UNA CATEGORIA EN PARTICULAR
        if (id != null)
        {
            //FILTRAMOS EL LISTADO COMPLETO DE EJERCICIOS POR EL EJERCICIO QUE COINCIDA CON EL ID
            categorias = categorias.Where(c => c.CategoriaID == id).ToList();
        }

        var categoria = categorias.Select(c => new VistaMostrarCategoria
        {
            CategoriaID = c.CategoriaID,
            Descripcion = c.Descripcion,
            FechaRegistro = c.FechaRegistro,
            FechaRegistroString = c.FechaRegistro.ToString("dd/MM/yyyy"),
            Eliminado = c.Eliminado,
            Disponibilidad = c.Disponibilidad
        }).ToList();

        return Json(categoria);
    }

    public JsonResult GuardarCategoria(int categoriaID, string descripcion)
    {
        string resultado = "";
        if (!String.IsNullOrEmpty(descripcion))
        {
            descripcion = descripcion.ToUpper();
            if (categoriaID == 0)
            {
                //3- VERIFICAMOS SI EXISTE EN BASE DE DATOS UN REGISTRO CON LA MISMA DESCRIPCION
                //PARA REALIZAR ESA VERIFICACION BUSCAMOS EN EL CONTEXTO, ES DECIR EN BASE DE DATOS 
                //SI EXISTE UN REGISTRO CON ESA DESCRIPCION   
                var exixteCategoria = _contexto.Categorias.Where(t => t.Descripcion == descripcion).Count();
                if (exixteCategoria == 0)
                {
                    //4. GUARDA LA CATEGORIA
                    var categoria = new Categoria
                    {
                        Descripcion = descripcion,
                        FechaRegistro = DateTime.Now,
                    };
                    _contexto.Add(categoria);
                    _contexto.SaveChanges();
                }

            }
            else
            {
                //QUIERE DECIR QUE VAMOS A EDITAR LA CATEGORIA
                var categoriaEditar = _contexto.Categorias.Where(c => c.CategoriaID == categoriaID).SingleOrDefault();
                if (categoriaEditar != null)
                {
                    // QUIERE DECIR QUE LA CATEGORIA EXISTE Y CONTINUA CON LA EDICION
                    categoriaEditar.Descripcion = descripcion;
                    categoriaEditar.FechaRegistro = DateTime.Now;
                    _contexto.SaveChanges();
                }
            }
        }

        return Json(resultado);
    }

    public JsonResult DesahabilitarCategoria(int categoriaID, int disponibilidad)
    {
        string resultado = "";
        var categoria = _contexto.Categorias.Find(categoriaID);
        if (categoria != null)
        {
            if (disponibilidad == 0)
            {
                categoria.Disponibilidad = false;
                _contexto.SaveChanges();
            }
            else
            {
                var catEnProduc = _contexto.Productos.Where(p => p.CategoriaID == categoriaID).Count();
                if (catEnProduc == 0)
                {
                    categoria.Disponibilidad = true;
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


    public JsonResult EliminarCategoria(int categoriaID)
    {
        string resultado = "";
        var eliminarCategoria = _contexto.Categorias.Find(categoriaID);
        _contexto.Remove(eliminarCategoria);
        _contexto.SaveChanges();
        return Json(resultado);
    }

}