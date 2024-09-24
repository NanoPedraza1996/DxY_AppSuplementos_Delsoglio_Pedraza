using DxY_AppSuplementos.Data;
using DxY_AppSuplementos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DxY_AppSuplementos.Controllers;

[Authorize]
public class ClientesController : Controller
{
    private DxY_DbContext _contexto;
    private ApplicationDbContext _context;

    private readonly UserManager<IdentityUser> _userManager;


    public ClientesController(DxY_DbContext contexto, ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _contexto = contexto;
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        // var email = await _userManager.GetEmailAsync(user);
        return View();
    }

    public JsonResult BuscarCliente(int? id)
    {
        var cliente = _contexto.Clientes.ToList();
        if (id != null)
        {
            cliente = cliente.Where(c => c.ClienteID == id).ToList();
        }

        // var mostrarCliente = cliente.Select(c => new MostrarVistaVentas
        // {

        // }).ToList();

        return Json(cliente);
    }


    public JsonResult GuardarCliente(int clienteID, string nombreCompleto, string telefono)
    {
        string resultado = "";

        if (!string.IsNullOrEmpty(nombreCompleto))
        {
            if (clienteID == 0)
            {
                // var usuarioLogueadoID = _userManager.GetUserId(HttpContext.User);
                var clienteExiste = _contexto.Clientes.Where(c => c.ClienteID == clienteID).Count();
                if (clienteExiste == 0)
                {
                    // var recuperarCliente = _context.Users.Where(u => u.Email == correo).First();

                    var cliente = new Cliente
                    {
                        // Correo = usuarioLogueadoID,
                        // Correo = recuperarCliente.Email,
                        NombreCompleto = nombreCompleto,
                        Telefono = telefono,
                        FechaCreacion = DateTime.Now
                    };
                    _contexto.Add(cliente);
                    _contexto.SaveChanges();

                }
                else
                {
                    resultado = "Cliente Ya Existe.";
                }
            }
            else
            {
                var existeCliente = _contexto.Clientes.Where(c => c.ClienteID == clienteID).SingleOrDefault();
                if (existeCliente == null)
                {
                    existeCliente.NombreCompleto = nombreCompleto;
                    existeCliente.Telefono = telefono;
                    existeCliente.FechaCreacion = DateTime.Now;
                }
            }
        }


        return Json(resultado);
    }


    public JsonResult EliminarCliente(int clienteID)
    {
        string resultado = "";
        var cliente = _contexto.Clientes.Find(clienteID);
        _contexto.Remove(cliente);
        _contexto.SaveChanges();
        return Json(resultado);
    }
}