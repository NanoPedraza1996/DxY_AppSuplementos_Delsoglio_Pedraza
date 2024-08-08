window.onload = ListadoCategorias();

function ListadoCategorias() {
    $.ajax({
        // la URL para la petición
        url: '../../Categorias/ListadoCategorias',
        // la información a enviar
        // (también es posible utilizar una cadena de datos)
        data: {},
        // especifica si será una petición POST o GET
        type: 'POST',
        // el tipo de información que se espera de respuesta
        dataType: 'json',
        // código a ejecutar si la petición es satisfactoria;
        // la respuesta es pasada como argumento a la función
        success: function (categorias) {

            $("#ModalCategorias").modal("hide");
            LimpiarModal();


            $("#tbody-categorias").empty();
            $.each(categorias, function (index, categoria) {
                // console.log(categoria)
                let contenidoTabla = ``;
                let botonHabilitar = '';
                contenidoTabla += `
                    <td class="text-center">
                    <button type="button" class="btn btn-success" onclick="AbrirModalEditar(${categoria.categoriaID})"><i class="fa-solid fa-pen-to-square"></i></button>
                    <button type="button" class="btn btn-danger" onclick="EliminarCategoria(${categoria.categoriaID})"><i class="fa-solid fa-trash"></i></button>
                    <button type="button" class="btn btn-primary" onclick ="DesahabilitarCategoria(${categoria.categoriaID} ,1)"><i class="fa-solid fa-xmark"></i></button>
                    </td>`;

                if (categoria.disponibilidad) {
                    botonHabilitar = 'table-danger';
                    contenidoTabla = `
                        <td class="text-center">
                        <button type = "button" class = "btn btn-success" onclick = "DesahabilitarCategoria(${categoria.categoriaID} ,0)"><i class="fa-solid fa-check"></i></button>
                        </td>`;
                }

                $("#tbody-categorias").append(
                    '<tr class=' + botonHabilitar + '>'
                    + '<td class="text-center">' + categoria.descripcion + '</td><td class="text-center">'
                    + categoria.fechaRegistroString + '</td>' + contenidoTabla +
                    '</tr>');

            });

            //document.getElementById("tbody-categorias").innerHTML = contenidoTabla;


        },

        // código a ejecutar si la petición falla;
        // son pasados como argumentos a la función
        // el objeto de la petición en crudo y código de estatus de la petición
        error: function (xhr, status) {
            console.log('Disculpe, existió un problema al cargar el listado');
        }
    });
}

function LimpiarModal() {
    document.getElementById("CategoriaID").value = 0;
    document.getElementById("Descripcion").value = "";
}

function NuevoRegistro() {
    $("#ModalTitulo").text("Nueva Categoria");
}

function AbrirModalEditar(categoriaID) {

    $.ajax({
        // la URL para la petición
        url: '../../Categorias/ListadoCategorias',
        // la información a enviar
        // (también es posible utilizar una cadena de datos)
        data: { categoriaID: categoriaID },
        // especifica si será una petición POST o GET
        type: 'POST',
        // el tipo de información que se espera de respuesta
        dataType: 'json',
        // código a ejecutar si la petición es satisfactoria;
        // la respuesta es pasada como argumento a la función
        success: function (categorias) {
            let categoria = categorias[0];

            document.getElementById("CategoriaID").value = categoriaID;
            $("#ModalTitulo").text("Editar Categoria");
            document.getElementById("Descripcion").value = categoria.descripcion;
            $("#ModalCategorias").modal("show");
        },

        // código a ejecutar si la petición falla;
        // son pasados como argumentos a la función
        // el objeto de la petición en crudo y código de estatus de la petición
        error: function (xhr, status) {
            console.log('Disculpe, existió un problema al consultar el registro para ser modificado.');
        }
    });
}




function EliminarCategoria(categoriaID) {
    $.ajax({
        // la URL para la petición
        url: '../../Categorias/EliminarCategoria',
        // la información a enviar
        // (también es posible utilizar una cadena de datos)
        data: { categoriaID: categoriaID },
        // especifica si será una petición POST o GET
        type: 'POST',
        // el tipo de información que se espera de respuesta
        dataType: 'json',
        // código a ejecutar si la petición es satisfactoria;
        // la respuesta es pasada como argumento a la función
        success: function (resultado) {
            ListadoCategorias();
        },

        // código a ejecutar si la petición falla;
        // son pasados como argumentos a la función
        // el objeto de la petición en crudo y código de estatus de la petición
        error: function (xhr, status) {
            console.log('Disculpe, existió un problema al guardar el registro');
        }
    });
}




function DesahabilitarCategoria(categoriaID, disponibilidad) {
    $.ajax({
        url: '../../Categorias/DesahabilitarCategoria',
        data: { categoriaID: categoriaID, disponibilidad: disponibilidad },
        type: 'POST',
        dataType: 'json',
        success: function (resultado) {
            if (resultado != "") {
                alert(resultado);
            }
            ListadoCategorias();
        },
        error: function (xhr, status) {
            alert("Disculpe, Existio Un Problema.");
        },
    });
}



function GuardarRegistro() {
    $("#error").text("");
    let categoriaID = document.getElementById("CategoriaID").value;
    let descripcion = document.getElementById("Descripcion").value;
    let guardar = true;

    if (!descripcion && descripcion === "") {
        $("#error").text("*Debe ingresar una Descripcion");
        guardar = false;
    }
    if (guardar) {
        $.ajax({
            url: '../../Categorias/GuardarCategoria',
            data: { categoriaID: categoriaID, descripcion: descripcion },
            type: 'POST',
            // dataType: 'json',
            success: function (resultado) {
                ListadoCategorias();
                $("#ModalCategorias").modal("hide");
            },
            error: function (xhr, status) {
                console.log('Disculpe, existió un problema al guardar el registro');
            }
        });
    }
    return false;
}



function validarFormulario() {
    let Descripcion = document.getElementById('Descripcion').value;
    let esValido = true;

    // Validar nombre
    if (Descripcion === "") {
        document.getElementById('Descripcion').textContent = "El nombre es obligatorio.";
        esValido = false;
    } else {
        document.getElementById('Descripcion').textContent = "";
    }

    return esValido;
}