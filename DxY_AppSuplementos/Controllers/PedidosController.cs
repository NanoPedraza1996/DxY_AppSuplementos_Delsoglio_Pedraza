using DxY_AppSuplementos.Data;
using Microsoft.AspNetCore.Mvc;

namespace DxY_AppSuplementos.Controllers;

public class PedidosController : Controller
{
    private DxY_DbContext _contexto;

    // private ApplicationDbContext _context; (Y despues para declararlo abajo utilizamos) 
    //, ApplicationDbContext context (y igualamos) _context = context;

    public PedidosController(DxY_DbContext contexto)
    {
        _contexto = contexto;
    }

    public IActionResult Index()
    {
        return View();
    }
}