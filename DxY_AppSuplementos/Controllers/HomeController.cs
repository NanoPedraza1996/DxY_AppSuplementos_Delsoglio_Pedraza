using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DxY_AppSuplementos.Models;
using Microsoft.AspNetCore.Authorization;
using DxY_AppSuplementos.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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

        var selectListItem = new List<SelectListItem>
        {
           new SelectListItem {Value = "0", Text = "[SELECCIONE...]"}
        };

        // var email = await _userManager.GetEmailAsync(user);
        var user = await _context.Users.ToListAsync();

        // var usuarioLogueadoID = _userManager.GetUserId(HttpContext.User);
        user.Add(new IdentityUser { Id = "0", Email = "[SELECCIONE...]" });
        ViewBag.id = new SelectList(user.OrderBy(u => u.Email), "Id", "Email");
        //var usuario = _context.Users.Where(u => u.Email == email).SingleOrDefault();


        // user.Add(new IdentityUser { Id = "0", Email = "[SELECCIONE...]" });
        // ViewBag.id = new SelectList(user.OrderBy(u => u.Email), "Id", "Email");

        return View(user);
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



    //Gestión de Roles y Usuarios en .Net Core
    //Método para crear Roles, Usuarios y asignar Roles a los Usuarios
    // public async Task<JsonResult> GuardarRol(string rol)
    // {
    //     bool resultado = false;
    //     var rolSuperusuarioExiste = _context.Roles.Where(r => r.Name == rol).SingleOrDefault();
    //     if (rolSuperusuarioExiste == null)
    //     {
    //         var roleResult = await _rolManager.CreateAsync(new IdentityRole(rol));
    //         resultado = roleResult.Succeeded;
    //     }

    //     return Json(resultado);
    // }
    // public async Task<JsonResult> GuardarUsuario(string username, string email, string password)
    // {
    //     //CREAR LA VARIABLE USUARIO CON TODOS LOS DATOS
    //     var user = new IdentityUser { UserName = username, Email = email };

    //     //EJECUTAR EL METODO CREAR USUARIO PASANDO COMO PARAMETRO EL OBJETO CREADO ANTERIORMENTE Y LA CONTRASEÑA DE INGRESO
    //     var result = await _userManager.CreateAsync(user, password);

    //     //BUSCAR POR MEDIO DE CORREO ELECTRONICO ESE USUARIO CREADO PARA BUSCAR EL ID
    //     var usuario = _context.Users.Where(u => u.Email == email).SingleOrDefault();

    //     await _userManager.AddToRoleAsync(usuario, "Socio");

    //     return Json(result.Succeeded);
    // }
    //Fin Del Método para crear Roles, Usuarios y asignar Roles a los Usuarios



    //Crear proyecto net. Core 8 con autenticación de usuarios
    //Consultar ID del Usuario actual
    //Agregando la siguiente linea en el método que necesiten consultar el id del usuario logueado podran en base a esa variable consultar en las tablas relacionadas a este.

    //BUSCAR EL ID DEL USUARIO LOGUEADO

    // var usuarioLogueadoID = _userManager.GetUserId(HttpContext.User);


    //ES  IMPORTANTE ACLARAR QUE DEBE ESTAR DECLARADO EL _userManage EN EL CONSTRUCTOR DEL CONTROLADOR


    //FIN



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
