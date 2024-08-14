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
            //$("#staticBackdrop").modal("hide");

            let contenidoTabla = ``;
            $.each(ventas, function (index, ventaa) {
                contenidoTabla += `
                <tr>
                    <td class="text-center">${ventaa.clienteIDNombre}</td>
                    <td class="text-center">$${ventaa.totalAPagar}</td>
                    <td class="text-center">${ventaa.fechaString}</td>
                    <td class="text-center">
                    <button type="button" class="btn btn-danger" onclick="EliminarVenta(${ventaa.ventaID})">
                    <i class="fa-solid fa-trash"></i>
                    </button>
                    ||
                    <button type="button" class="btn btn-success" onclick="MostrarDetalleVenta(${ventaa.ventaID})">
                    <i class="fa-solid fa-list"></i>
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

function LimpiarModal() {
    document.getElementById("ProductoID").value = 0;
    document.getElementById("Cantidad").value = "";
    document.getElementById("SubTotal").value = "";
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
            if (resultado == "") {
                Swal.fire({
                    position: "top-end",
                    icon: "success",
                    title: "¡Eliminado!",
                    showConfirmButton: false,
                    timer: 1200
                });
                ListadoVentas();
            }
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



function Imprimir() {
    var doc = new jsPDF();

    var totalPagesExp = "{total_pages_count_string}";
    var pageContent = function (data) {
        var pageHeight = doc.internal.pageSize.height || doc.internal.pageSize.getHeight();
        var pageWidth = doc.internal.pageSize.width || doc.internal.pageSize.getWidth();

        // FOOTER
        var str = "Pagina " + data.pageCount;
        if (typeof doc.putTotalPages == 'function') {
            str = str + " de " + totalPagesExp;
        }

        doc.setLineWidth(8);
        doc.setDrawColor(238, 238, 238);
        doc.line(14, pageHeight - 11, 196, pageHeight - 11);

        doc.setFontSize(10);
        doc.setFontStyle('bold');
        doc.text(str, 17, pageHeight - 10);
    };

    // Add title and date to the first page
    doc.setFontSize(18);
    doc.setFontStyle('bold');
    doc.text('Listado de Detalle de Ventas', 14, 22);

    // //Tabla Detalle
    // doc.setFontSize(18);
    // doc.setFontStyle('bold');
    // doc.text('Listado de Detalle De La Ventas', 14, 22);


    // Add current date
    var today = new Date();
    var dateString = today.toLocaleDateString(); // Format date as needed
    doc.setFontSize(12);
    doc.setFontStyle('normal');
    doc.text('Fecha Actual: ' + dateString, 14, 32);

    var elem = document.getElementById("Imprimirr");
    var res = doc.autoTableHtmlToJson(elem);

    // //Tabla Detalle
    // var elem = document.getElementById("Imprimirr");
    // var res = doc.autoTableHtmlToJson(elem);

    // Remove last two columns
    // res.columns = res.columns.slice(0, -1);
    // res.data = res.data.map(row => row.slice(0, -1));

    doc.autoTable(res.columns, res.data, {
        addPageContent: pageContent,
        theme: 'grid',
        headStyles: { halign: 'center' }, // Center align headers
        columnStyles: {
            0: { halign: 'center', fontSize: 7 },
            1: { halign: 'center', fontSize: 7 },
            2: { halign: 'center', fontSize: 7 },
            3: { halign: 'center', fontSize: 7 },
            4: { halign: 'center', fontSize: 7 },
        },
        styles: { halign: 'center' }, // Center align all cell content
        margin: { top: 40 } // Adjust top margin for title
    });

    if (typeof doc.putTotalPages === 'function') {
        doc.putTotalPages(totalPagesExp);
    }

    var string = doc.output('datauristring');
    var iframe = "<iframe width='100%' height='100%' src='" + string + "'></iframe>"

    var x = window.open();
    x.document.open();
    x.document.write(iframe);
    x.document.close();
}