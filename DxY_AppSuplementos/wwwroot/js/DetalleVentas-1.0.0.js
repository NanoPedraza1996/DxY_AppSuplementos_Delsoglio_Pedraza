
window.onload = CargarPagina();
function CargarPagina() {
    ListadoDetalleVentaTemporal();
}

function ListadoDetalleVentaTemporal() {
    $.ajax({
        type: 'GET',
        url: '../../Ventas/ListadoDetalleVentaTemporal',
        data: {},
        success: function (MostrardetalleTemporal) {
            // $("#staticBackdrop").modal("hide");

            let contenidoTabla = ``;
            $.each(MostrardetalleTemporal, function (index, venta) {
                contenidoTabla += `
                <tr>
                    <td class="text-center">${venta.nombre}</td>
                    <td class="text-center">${venta.precioVenta}</td>
                    <td class="text-center">${venta.cantidad}</td>
                    <td class="text-center">${venta.subTotal}</td>
                    <td class="text-center">
                    <button type="button" class="btn btn-danger" onclick="EliminarVentaDetalleTemporal(${venta.detalleVentaTemporalID})">
                    Eliminar
                    </button>
                    </td>
                </tr>
                `;
            });
            document.getElementById("TablaVentaCreado").innerHTML = contenidoTabla;
        },
        error: function (xhr, status) {
            alert("Disculpe, Existio Un Problema.");
        }
    });
}

function EliminarVentaDetalleTemporal(detalleVentaTemporalID) {
    $.ajax({
        url: '../../Ventas/EliminarVentaDetalleTemporal',
        data: { detalleVentaTemporalID: detalleVentaTemporalID },
        type: 'POST',
        success: function (resultado) {
            ListadoDetalleVentaTemporal();
        },
        error: function (xhr, status) {
            alert("Disculpe, Existio Un Problema.");
        }
    })
}


function ListaDetallesVentas() {
    $.ajax({
        type: 'GET',
        url: '../../Ventas/ListaDetallesVentas',
        data: {},
        success: function (detalleVentas) {
            // $("#staticBackdrop").modal("hide");

            let contenidoTabla = ``;
            $.each(detalleVentas, function (index, venta) {
                contenidoTabla += `
                <tr>
                    <td class="text-center">${venta.precioVenta}</td>   
                    <td class="text-center">${venta.productoID}</td>
                    <td class="text-center">${venta.cantidad}</td>
                    <td class="text-center">${venta.subTotal}</td>
                </tr>
                `;
            });
            document.getElementById("TablaVentaCreado").innerHTML = contenidoTabla;
        },
        error: function (xhr, status) {
            alert("Disculpe, Existio Un Problema.");
        }
    });
}


function AgregarDetalleVenta() {
    let detalleVentaID = document.getElementById("DetalleVentaID").value;
    // let precioVenta = document.getElementById("PrecioVenta").value;
    let cantidad = document.getElementById("Cantidad").value;
    let subTotal = document.getElementById("SubTotal").value;
    let productoID = document.getElementById("ProductoID").value;

    $.ajax({
        url: '../../Ventas/AgregarDetalleVenta',
        data: {
            detalleVentaID: detalleVentaID, cantidad: cantidad,
            subTotal: subTotal, productoID: productoID
        },
        type: 'POST',
        success: function (resultado) {
            $("#staticBackdrop").modal("hide");
            ListadoDetalleVentaTemporal();
            // if (resultado == "") {
            //     BuscarDetallesVentas()
            // } else {
            //     alert(resultado);
            // }
        },
        error: function (xhr, status) {
            alert("Disculpe, Existio Un Problema.");
        }
    });
}


function CancelarVenta() {
    $.ajax({
        url: '../../Ventas/CancelarVenta',
        data: {},
        type: 'POST',
        success: function (resultado) {
            if (resultado == "") {
                location.href = "../../Ventas/Index";
            }
        }
    });
}


function GuardarVentas() {
    let ventaID = document.getElementById("VentaID").value;
    let totalAPagar = document.getElementById("TotalAPagar").value;
    let clienteID = document.getElementById("ClienteID").value;

    $.ajax({
        url: '../../Ventas/GuardarVentas',
        data: { ventaID: ventaID, totalAPagar: totalAPagar, clienteID: clienteID },
        type: 'POST',
        dataType: 'json',
        success: function (resultado) {
            if (resultado == "") {
                location.href = "../../Ventas/Index";
            }
        }
    });
}