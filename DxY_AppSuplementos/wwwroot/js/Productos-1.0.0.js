window.onload = BuscarProducto();
// function CargarPagina() {
//     BuscarProducto();
// }

function BuscarProducto() {
    // $("#TablaProducto").empty();
    $.ajax({
        url: '../../Productos/BuscarProducto',
        data: {},
        type: 'GET',
        dataType: 'json',
        success: function (productos) {
            $("#staticBackdrop").modal("hide");
            LimpiarModal()

            $("#TablaProducto").empty();
            $.each(productos, function (index, producto) {
                let contenidoTabla = '';
                let botonHabilitar = '';
                contenidoTabla += '<button type="button" class="btn btn-success" onclick="AbrirModalProducto(' + producto.productoID + '), ocultarInput(' + producto.productoID + ')"><i class="fa-solid fa-pen-to-square"></i></Button>||'
                    + '<button type="button" class="btn btn-danger" onclick="EliminarProducto(' + producto.productoID + ')"><i class="fa-solid fa-trash"></i></button>||'
                    + '<button type="button" class="btn btn-primary" onclick ="DesahabilitarProducto(' + producto.productoID + ',1)"><i class="fa-solid fa-xmark"></i></button>'

                if (producto.disponibilidad) {
                    botonHabilitar = 'table-danger';
                    contenidoTabla = `
                    <button type = "button" class = "btn btn-success" onclick = "DesahabilitarProducto(${producto.productoID} ,0)"><i class="fa-solid fa-check"></i></button>`;
                }


                let imagen = '<td></td>';
                if (producto.imagenBase64) {
                    imagen = '<td style="width: 40px;"><img class="text-center rounded-circle" src="data:' + producto.tipoImagen + ';base64,' + producto.imagenBase64 + '" style="width: 40px;"/></td>';
                }

                $("#TablaProducto").append(
                    '<tr class=' + botonHabilitar + '>'
                    + '<td>' + producto.nombre + '</td>'
                    + '<td>' + producto.descripcion + '</td>'
                    + '<td>' + producto.fechaRegistro + '</td>'
                    + '<td>' + producto.precioCompra + '</td>'
                    + '<td>' + producto.precioVenta + '</td>'
                    + '<td>' + producto.stock + '</td>'
                    + '<td>' + producto.categoriaID + '</td>'
                    + imagen
                    + '<td>' + contenidoTabla + '</td>'
                    + '</tr>'
                );
            });
            //document.getElementById("TablaProducto").innerHTML = contenidoTabla;

        },
        error: function (xhr, status) {
            alert("Disculpe, Existio Un Problema.");
        }
    });
}
function AbrirModalProducto(productoID) {
    // BuscarProducto();
    $.ajax({
        url: '../../Productos/BuscarProducto',
        data: { productoID: productoID },
        type: 'POST',
        dataType: 'json',
        success: function (productos) {
            var producto = productos[0];
            //$("#staticBackdropLabel").text("Editar Producto");

            document.getElementById("ProductoID").value = productoID;
            document.getElementById("Nombre").value = producto.nombre;
            document.getElementById("Descripcion").value = producto.descripcion;
            document.getElementById("PrecioCompra").value = producto.precioCompra;
            document.getElementById("PrecioVenta").value = producto.precioVenta;
            document.getElementById("CategoriaID").value = producto.categoriaID;
            $("#staticBackdrop").modal("show");
        },
        error: function (xhr, status) {
            alert("Disculpe, Existio Un Problema");
        }
    });
}


function GuardarProductos() {
    $("form#files").submit(function () {
        $("#Error").text("");
        //let productoID = document.getElementById("ProductoID").value;
        let nombre = document.getElementById("Nombre").value;
        let descripcion = document.getElementById("Descripcion").value;
        let precioCompra = document.getElementById("PrecioVenta").value;
        let precioVenta = document.getElementById("PrecioVenta").value;
        let stock = document.getElementById("Stock").value;
        let categoriaID = document.getElementById("CategoriaID").value;
        let formData = new FormData($(this)[0]);

        var guardar = true;
        if (!nombre) {
            $("#Error").text("*Debe ingresar un Nombre");
            guardar = false;
        }
        var guardar = true;
        if (!descripcion) {
            $("#Error").text("*Debe ingresar un Nombre");
            guardar = false;
        }
        var guardar = true;
        if (!precioCompra) {
            $("#Error").text("*Debe ingresar un Nombre");
            guardar = false;
        }
        var guardar = true;
        if (!precioVenta) {
            $("#Error").text("*Debe ingresar un Nombre");
            guardar = false;
        }
        var guardar = true;
        if (!stock) {
            $("#Error").text("*Debe ingresar un Nombre");
            guardar = false;
        }
        var guardar = true;
        if (!categoriaID) {
            $("#Error").text("*Debe ingresar un Nombre");
            guardar = false;
        }

        if (guardar) {
            $.ajax({
                url: '../../Productos/GuardarProductos',
                type: 'POST',
                data: formData,
                dataType: 'json',
                async: false,
                success: function (resultado) {
                    if (resultado == "") {
                        // Swal.fire({
                        //     position: "top-end",
                        //     icon: "success",
                        //     title: "¡Guardado!",
                        //     showConfirmButton: false,
                        //     timer: 1200
                        // });
                        $("#staticBackdrop").modal("hide");
                        BuscarProducto();
                    }
                },
                cache: false,
                contentType: false,
                processData: false
            });
        }
        return false;

    });
}



function EliminarProducto(productoID) {
    $.ajax({
        url: '../../Productos/EliminarProducto',
        data: { productoID: productoID },
        type: 'POST',
        dataType: 'json',
        success: function (resultado) {
            if (resultado != "") {
                Swal.fire({
                    icon: "error",
                    title: "Vaya Vaya...",
                    text: "Parece Que Esta relacionado con Ventas!",
                    // footer: '<a href="../../Views/Categorias/Index.cshtml">De Acuerdo</a>'
                });
            }
            else {
                Swal.fire({
                    position: "top-end",
                    icon: "success",
                    title: "¡Eliminado!",
                    showConfirmButton: false,
                    timer: 1200
                });
                BuscarProducto();
            }
        },
        error: function (xhr, status) {
            alert("Disculpe, Existio Un Problema");
        }
    });
}


function LimpiarModal() {
    document.getElementById("ProductoID").value = 0;
    document.getElementById("Nombre").value = "";
    document.getElementById("Descripcion").value = "";
    document.getElementById("PrecioCompra").value = "";
    document.getElementById("PrecioVenta").value = "";
    document.getElementById("Stock").value = "";
    document.getElementById("CategoriaID").value = 0;
}


function ocultarInput() {
    document.getElementById('ocultar').style.display = 'none';
}
function hideInput() {
    document.getElementById('ocultar').style.display = 'block';
}


function DesahabilitarProducto(productoID, disponibilidad) {
    $.ajax({
        url: '../../Productos/DesahabilitarProducto',
        data: { productoID: productoID, disponibilidad: disponibilidad },
        type: 'POST',
        dataType: 'json',
        success: function (resultado) {
            // BuscarProducto();
            if (resultado != "") {
                Swal.fire({
                    icon: "error",
                    title: "Vaya Vaya...",
                    text: "Parece Que Esta relacionado con Productos!",
                    // footer: '<a href="../../Views/Categorias/Index.cshtml">De Acuerdo</a>'
                });
            }
            else {
                Swal.fire({
                    position: "top-end",
                    icon: "success",
                    title: "Deshabilitado!",
                    showConfirmButton: false,
                    timer: 1200
                });
                BuscarProducto();
            }
        },
        error: function (xhr, status) {
            alert("Disculpe, Existio Un Problema.");
        },
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
    doc.text('Listado de Productos', 14, 22);

    // Add current date
    var today = new Date();
    var dateString = today.toLocaleDateString(); // Format date as needed
    doc.setFontSize(12);
    doc.setFontStyle('normal');
    doc.text('Fecha: ' + dateString, 14, 32);

    var elem = document.getElementById("Imprimir");
    var res = doc.autoTableHtmlToJson(elem);

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
            4: { halign: 'center', fontSize: 7 },
            5: { halign: 'center', fontSize: 7 },
            6: { halign: 'center', fontSize: 7 },
            7: { halign: 'center', fontSize: 7 },
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


