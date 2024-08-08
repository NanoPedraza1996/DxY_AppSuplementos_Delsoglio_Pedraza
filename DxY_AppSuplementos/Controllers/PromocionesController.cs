using DxY_AppSuplementos.Data;
using DxY_AppSuplementos.Models;
using Microsoft.AspNetCore.Mvc;

namespace DxY_AppSuplementos.Controllers;

public class PromocionesController : Controller
{
    private DxY_DbContext _contexto;
    //private ApplicationDbContext _context; , ApplicationDbContext context  _context = context;

    public PromocionesController(DxY_DbContext contexto)
    {
        _contexto = contexto;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Crear()
    {
        return View();
    }

    public JsonResult BuscarPromocion(int? id)
    {
        var detallePromocion = _contexto.DetallePromociones.OrderBy(p => p.Descripcion).ToList();
        if (id != null)
        {
            detallePromocion = detallePromocion.Where(p => p.PromocionID == id).ToList();
        }

        foreach (var detallePromociones in detallePromocion)
        {
            if (detallePromociones.Imagen != null)
            {
                detallePromociones.ImagenBase64 = Convert.ToBase64String(detallePromociones.Imagen);
            }
        }

        return Json(detallePromocion);
    }

    public JsonResult GuardarPromocion(int promocionID, string nombre, DateTime fechaRegistro, IFormFile imagen)
    {
        string resultado = "";

        if (!string.IsNullOrEmpty(nombre))
        {

            nombre = nombre.ToUpper();

            if (promocionID == 0)
            {
                var promocionExiste = _contexto.Promociones.Where(e => e.Nombre == nombre).Count();
                if (promocionExiste == 0)
                {
                    var promocion = new Promocion
                    {
                        Nombre = nombre,
                        FechaRegistro = DateTime.Now
                    };
                    _contexto.Add(promocion);
                    _contexto.SaveChanges();
                }
                else
                {
                    resultado = "Ya Existe Una Registro Igual.";
                }
            }
            else
            {
                var editarPromocion = _contexto.Promociones.Where(e => e.PromocionID == promocionID).SingleOrDefault();
                if (editarPromocion != null)
                {
                    var existeEditar = _contexto.Promociones.Where(e => e.Nombre == nombre && e.PromocionID != promocionID).Count();
                    if (existeEditar == 0)
                    {
                        editarPromocion.Nombre = nombre.ToUpper();
                        editarPromocion.FechaRegistro = DateTime.Now;
                        _contexto.SaveChanges();
                    }
                }
                else
                {

                }
            }

        }
        else
        {
            resultado = "Ingrese Un Nuevo Ejercicio.";
        }
        return Json(resultado);
    }

    public JsonResult EliminarPromocion(int promocionID)
    {
        string resultado = "";
        var eliminarPromocion = _contexto.Promociones.Find(promocionID);
        _contexto.Remove(eliminarPromocion);
        _contexto.SaveChanges();
        return Json(resultado);
    }

}