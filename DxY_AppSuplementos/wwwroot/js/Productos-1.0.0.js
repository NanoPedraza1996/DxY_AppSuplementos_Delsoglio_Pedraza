window.onload = BuscarProducto();

function BuscarProducto() {
    $("#TablaProducto").empty();
    $.ajax({
        url: '../../Productos/BuscarProducto',
        data: {},
        type: 'GET',
        dataType: 'json',
        success: function (productos) {
            //$("#staticBackdrop").modal("hide");


            $("#TablaProducto").empty();
            $.each(productos, function (index, producto) {
                // let contenidoTabla = ``;
                let contenidoTabla = '<button type="button" class="btn btn-success" onclick="AbrirModalEjercicioFisico(' + producto.productoID + ')"><i class="fa-solid fa-pen-to-square"></i></Button>'
                    + '<button type="button" class="btn btn-danger" onclick="EliminarProducto(' + producto.productoID + ')"><i class="fa-solid fa-trash"></i></button>'

                let imagen = '<td></td>';
                if (producto.imagenBase64) {
                    imagen = '<td style="width: 150px;"><img class="text-center rounded-circle" src="data:' + producto.tipoImagen + ';base64,' + producto.imagenBase64 + '" style="width: 100px;"/></td>';
                }

                $("#TablaProducto").append(
                    '<tr>'
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


function GuardarProductos() {
    $("form#files").submit(function () {
        $("#Error").text("");
        //let productoID = document.getElementById("ProductoID").value;
        let nombre = document.getElementById("Nombre").value;
        let descripcion = document.getElementById("Descripcion").value;
        let fechaRegistro = document.getElementById("FechaRegistro").value;
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
        if (!fechaRegistro) {
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
                //dataType: 'json',
                async: false,
                success: function (resultado) {
                    if (resultado) {
                        $("#staticBackdrop").modal("hide");
                        BuscarProducto();
                    }
                    // if (resultado == "") {
                    //     BuscarProducto();
                    // }
                    // if (resultado != "") {
                    //     alert(resultado);
                    // }
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
            BuscarProducto();
        },
        error: function (xhr, status) {
            alert("Disculpe, Existio Un Problema");
        }
    });
}


{/* <td>${producto.nombre}</td>
                    <td>${producto.descripcion}</td>
                    <td>${producto.fechaRegistro}</td>
                    <td>${producto.precioCompra}</td>
                    <td>${producto.precioVenta}</td>
                    <td>${producto.stock}</td>
                    <td>${producto.categoriaID}</td> productoID: productoID, nombre: nombre, descripcion: descripcion, fechaRegistro: fechaRegistro,
                precioCompra: precioCompra, precioVenta: precioVenta, stock: stock,
                categoriaID: categoriaID,*/}


