
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
            //$("#staticBackdrop").modal("hide");

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
                    <i class="fa-solid fa-trash"></i>
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
            Swal.fire({
                position: "top-end",
                icon: "success",
                title: "¡Removido!",
                showConfirmButton: false,
                timer: 1200
            });
            ListadoDetalleVentaTemporal();

        },
        error: function (xhr, status) {
            alert("Disculpe, Existio Un Problema.");
        }
    })
}


// function ListaDetallesVentas() {
//     $.ajax({
//         type: 'GET',
//         url: '../../Ventas/ListaDetallesVentas',
//         data: {},
//         success: function (detalleVentas) {
//             $("#staticBackdrop").modal("hide");

//             let contenidoTabla = ``;
//             $.each(detalleVentas, function (index, venta) {
//                 contenidoTabla += `
//                 <tr>
//                     <td class="text-center">${venta.precioVenta}</td>   
//                     <td class="text-center">${venta.productoID}</td>
//                     <td class="text-center">${venta.cantidad}</td>
//                     <td class="text-center">${venta.subTotal}</td>
//                 </tr>
//                 `;
//             });
//             document.getElementById("TablaVentaCreado").innerHTML = contenidoTabla;
//         },
//         error: function (xhr, status) {
//             alert("Disculpe, Existio Un Problema.");
//         }
//     });
// }


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
            // $("#staticBackdrop").modal("hide");
            ListadoDetalleVentaTemporal();
            LimpiarModal();
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

function LimpiarModal() {
    document.getElementById("ProductoID").value = 0;
    document.getElementById("Cantidad").value = "";
    document.getElementById("SubTotal").value = "";
    document.getElementById("TotalAPagar").value = "";
    document.getElementById("ClienteID").value = 0;
}



function CancelarVenta() {
    $.ajax({
        url: '../../Ventas/CancelarVenta',
        data: {},
        type: 'POST',
        success: function (resultado) {
            if (resultado == "") {
                $("#staticBackdrop").modal("hide");
                // location.href = "../../Ventas/Index";
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
            Swal.fire({
                position: "top-end",
                icon: "success",
                title: "¡Guardado!",
                showConfirmButton: false,
                timer: 1200
            });
            $("#staticBackdrop").modal("hide");
            // location.href = "../../Ventas/Index";


        }
    });
}



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
    doc.text('Listado de Ventas', 14, 22);

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

    var elem = document.getElementById("Imprimir");
    var res = doc.autoTableHtmlToJson(elem);

    // //Tabla Detalle
    // var elem = document.getElementById("Imprimirr");
    // var res = doc.autoTableHtmlToJson(elem);

    // Remove last two columns
    res.columns = res.columns.slice(0, -1);
    res.data = res.data.map(row => row.slice(0, -1));

    doc.autoTable(res.columns, res.data, {
        addPageContent: pageContent,
        theme: 'grid',
        headStyles: { halign: 'center' }, // Center align headers
        columnStyles: {
            0: { halign: 'center', fontSize: 7 },
            1: { halign: 'center', fontSize: 7 },
            2: { halign: 'center', fontSize: 7 },
            3: { halign: 'center', fontSize: 7 },
            4: { halign: 'center', fontSize: 7 }
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



