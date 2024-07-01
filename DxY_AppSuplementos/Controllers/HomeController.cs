using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DxY_AppSuplementos.Models;
using Microsoft.AspNetCore.Authorization;
using DxY_AppSuplementos.Data;
using Microsoft.AspNetCore.Identity;

namespace DxY_AppSuplementos.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private DxY_DbContext _contexto;
    private ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _rolManager;

    public HomeController(ILogger<HomeController> logger, DxY_DbContext contexto, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> rolManager, ApplicationDbContext context)
    {
        _logger = logger;
        _contexto = contexto;
        _userManager = userManager;
        _rolManager = rolManager;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        await InicializarPermisoUsuario();
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public async Task<JsonResult> InicializarPermisoUsuario()
    {
        var usuarioRolExiste = _context.Roles.Where(r => r.Name == "ADMINISTRADOR").SingleOrDefault();
        if (usuarioRolExiste == null)
        {
            var rolUsuario = await _rolManager.CreateAsync(new IdentityRole("ADMINISTRADOR"));
        }

        bool creado = false;
        var usuarioEmail = _context.Users.Where(u => u.Email == "DxY@sistema.com").SingleOrDefault();
        if (usuarioEmail == null)
        {
            var user = new IdentityUser { UserName = "DxY@sistema.com", Email = "DxY@sistema.com" };
            var resultado = await _userManager.CreateAsync(user, "Password");

            await _userManager.AddToRoleAsync(user, "ADMINISTRADOR");
            creado = resultado.Succeeded;
        }

        var superUsuario = _context.Users.Where(u => u.Email == "DxY@sistema.com").SingleOrDefault();
        if (superUsuario != null)
        {
            var usuarioID = superUsuario.Id;
        }
        return Json(creado);
    }

    [Authorize(Roles = "Administrador")]
    public IActionResult PrivadoAdministrador()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
