using DxY_AppSuplementos.Data;
using Microsoft.AspNetCore.Mvc;

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
        return View();
    }
}