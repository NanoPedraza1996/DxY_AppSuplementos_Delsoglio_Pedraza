window.onload = CargarPagina();
function CargarPagina() {
    ListadoVentas();
}

function ListadoVentas() {
    $.ajax({
        type: 'GET',
        url: '../../Ventas/ListadoVentas',
        data: {},
        success: function (ventas) {
            $("#staticBackdrop").modal("hide");

            let contenidoTabla = ``;
            $.each(ventas, function (index, ventaa) {
                contenidoTabla += `
                <tr>
                    <td class="text-center">${ventaa.fechaString}</td>
                    <td class="text-center">${ventaa.totalAPagar}</td>
                    <td class="text-center">${ventaa.clienteIDNombre}</td>
                    <td class="text-center">
                    <button type="button" class="btn btn-danger" onclick="EliminarVenta(${ventaa.ventaID})">
                    Eliminar
                    </button>
                    </td>
                    <td class="text-center">
                    <button type="button" class="btn btn-success" onclick="MostrarDetalleVenta(${ventaa.ventaID})">
                    Tabla Detalle
                    </button>
                    </td>
                </tr>
                `;
            });
            document.getElementById("TablaVenta").innerHTML = contenidoTabla;
        },
        error: function (xhr, status) {
            alert("Disculpe, Existio Un Problema.");
        }
    });
}

function MostrarDetalleVenta(ventaID) {
    $.ajax({
        // la URL para la petición
        url: '../../Ventas/ListaDetallesVentas',
        // la información a enviar
        // (también es posible utilizar una cadena de datos)
        data: { ventaID: ventaID },
        // especifica si será una petición POST o GET
        type: 'GET',
        // el tipo de información que se espera de respuesta
        // dataType: 'json',
        // código a ejecutar si la petición es satisfactoria;
        // la respuesta es pasada como argumento a la función
        success: function (detalleVentas) {
            $("#staticBackdrop1").modal("show");
            // $("#staticBackdropLabel1").text("Detalle De La Venta");

            let contenidoTabla = ``;
            $.each(detalleVentas, function (index, ventaa) {
                contenidoTabla += `
                <tr>
                    <td class="text-center">${ventaa.ventaID}</td>
                    <td class="text-center">${ventaa.productoID}</td>
                    <td class="text-center">${ventaa.precioVenta}</td>
                    <td class="text-center">${ventaa.cantidad}</td>
                    <td class="text-center">${ventaa.subTotal}</td>

                </tr>
                `;
            });
            document.getElementById("tablaDetalleVenta").innerHTML = contenidoTabla;
        },

        // código a ejecutar si la petición falla;
        // son pasados como argumentos a la función
        // el objeto de la petición en crudo y código de estatus de la petición
        error: function (xhr, status) {
            console.log('Disculpe, existió un problema al consultar el detalle.');
        }
    });
}


function EliminarVenta(ventaID) {
    $.ajax({
        url: '../../Ventas/EliminarVenta',
        data: { ventaID: ventaID },
        type: 'POST',
        dataType: 'json',
        success: function (resultado) {
            ListadoVentas();
        },
        error: function (xhr, status) {
            alert("Disculpe, Existio Un Problema.");
        }
    })
}



// function MostrarDetalleVenta(ventaID) {
//     $.ajax({
//         type: 'GET',
//         url: '../../Ventas/ListaDetallesVentas',
//         data: { ventaID },
//         success: function (detalleVenta) {
//             $("#staticBackdrop1").modal("show");


//         },
//         error: function (xhr, status) {
//             alert("Disculpe, Existio Un Problema.");
//         }
//     });
// }