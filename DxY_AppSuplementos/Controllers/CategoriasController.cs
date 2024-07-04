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
        //crear una lista de SelectListItem que incluya el elemento adicional
        var selectListItems = new List<SelectListItem>
        {
            new SelectListItem { Value = "0", Text = "[SELECCIONE]"}
        };

        //Obtener todas las opciones del enum
        var enumValues = Enum.GetValues(typeof(Disponibilidad)).Cast<Disponibilidad>();

        //Convertir las opciones del enum en SelectListItem
        selectListItems.AddRange(enumValues.Select(e => new SelectListItem 
        {   Value = e.GetHashCode().ToString(),
            Text = e.ToString().ToUpper()
        }));

        //Pasar la Lista de opciones al modelo de la vista
        ViewBag.Disponibilidad = selectListItems.OrderBy(t => t.Text).ToList();

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

        return Json(categorias);
    }

    public JsonResult GuardarCategoria(int categoriaID, string descripcion, DateTime fechaRegistro, Disponibilidad disponibilidad)
    {
       //1. VERIFICAMOS SI REALMENTE INGRESO ALGUN CARACTER Y LA VARIABLE NO SEA NULL
       // if (descripcion != null && descripcion != "")
       // {
       //     //INGRESA SI ESCRIBIO SI O SI
       // }

        // if (String.IsNullOrEmpty(descripcion) == false)
        // {
        //     //INGRESA SI ESCRIBIO SI O SI 
        // } 

        if (!String.IsNullOrEmpty(descripcion))
        {
            //INGRESA SI ESCRIBIO SI O SI

            //2. VERIFICA SI ESTA EDITANDO O CREANSDO UN NUEVO RESULTADO
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
                        FechaRegistro = fechaRegistro,
                        //Disponibilidad = disponibilidad
                    };
                    _contexto.Add(categoria);
                    _contexto.SaveChanges();
                }
            }
            else
            {
                //QUIERE DECIR QUE VAMOS A EDITAR LA CATEGORIA
                var categoriaEditar = _contexto.Categorias.Where(c => c.CategoriaID == categoriaID).SingleOrDefault();
                if(categoriaEditar != null)
                {
                    // QUIERE DECIR QUE LA CATEGORIA EXISTE Y CONTINUA CON LA EDICION
                    categoriaEditar.Descripcion = descripcion;
                    categoriaEditar.FechaRegistro = fechaRegistro;
                    //categoriaEditar.Disponibilidad = disponibilidad;
                    _contexto.SaveChanges();
                } 
            }
        }

        return Json(true);
    }

    public JsonResult DesahabilitarCategoria(int CategoriaID, int Eliminado)
    {
        int resultado = 0;
        //SE BUSCA EL ID DE LA CATEGORIA EN EL CONTEXTO
        var categoria = _contexto.Categorias.Find(CategoriaID);
        //CATEGORIA DIFERENTE DE NULL
        if (categoria != null)
        {
            if (Eliminado == 0)
            {
                categoria.Eliminado = false;
                _contexto.SaveChanges();
            }
            else
            {
                //NO PUEDE ELIMINAR LA CATEGORIA SI TIENE PRODUCTOS ACTIVOS
                if (Eliminado == 1)
                {
                    categoria.Eliminado = true;
                    _contexto.SaveChanges();
                }
            }
        }
         
         resultado = 1;

        return Json(resultado);
    }
    
}