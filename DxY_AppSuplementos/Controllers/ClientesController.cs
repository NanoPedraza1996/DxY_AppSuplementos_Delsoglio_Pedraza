using DxY_AppSuplementos.Data;
using DxY_AppSuplementos.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DxY_AppSuplementos.Controllers;

public class ClientesController : Controller
{
    private DxY_DbContext _contexto;
    private ApplicationDbContext _context;

    public ClientesController(DxY_DbContext contexto, ApplicationDbContext context)
    {
        _contexto = contexto;
        _context = context;
    }

    public IActionResult Index()
    {
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


    public JsonResult GuardarCliente(int clienteID, string correo, string contrasenia, string nombreCompleto, string telefono)
    {
        string resultado = "";

        if (!string.IsNullOrEmpty(nombreCompleto))
        {
            if (clienteID == 0)
            {
                var clienteExiste = _contexto.Clientes.Where(c => c.ClienteID == clienteID).Count();
                if (clienteExiste == 0)
                {
                    //var recuperarCliente = _context.Users
                    var cliente = new Cliente
                    {
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