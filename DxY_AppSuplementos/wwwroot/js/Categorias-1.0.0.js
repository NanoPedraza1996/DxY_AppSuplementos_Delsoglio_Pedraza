window.onload = ListadoCategorias();

function ListadoCategorias(){
    $.ajax({
        // la URL para la petición
        url: '../../Categorias/ListadoCategorias',
        // la información a enviar
        // (también es posible utilizar una cadena de datos)
        data: {  },
        // especifica si será una petición POST o GET
        type: 'POST',
        // el tipo de información que se espera de respuesta
        dataType: 'json',
        // código a ejecutar si la petición es satisfactoria;
        // la respuesta es pasada como argumento a la función
        success: function (categorias) {

            $("#ModalCategorias").modal("hide");
            LimpiarModal();
            let contenidoTabla = ``;

            $.each(categorias, function (index, categoria) {  
                console.log(categoria)
                contenidoTabla += `
                <tr>
                    <td class="text-center">${categoria.descipcion}</td>
                    <td class="text-center">${categoria.fechaRegistro}</td>
                    <td class="text-center">${categoria.disponibilidad}</td>
                    <td class="text-center">
                    <button type="button" class="btn btn-success" onclick="AbrirModalEditar(${Categoria.categoriaID})">
                    Editar
                    </button>
                    </td>
                    <td class="text-center">
                    <button type="button" class="btn btn-danger" onclick="DesahabilitarCategoria(${Categoria.categoriaID})">
                    Eliminar
                    </button>
                    </td>
                </tr>
             `;

            });

            document.getElementById("tbody-categorias").innerHTML = contenidoTabla;

        },

        // código a ejecutar si la petición falla;
        // son pasados como argumentos a la función
        // el objeto de la petición en crudo y código de estatus de la petición
        error: function (xhr, status) {
            console.log('Disculpe, existió un problema al cargar el listado');
        }
    });
}

function LimpiarModal(){
    document.getElementById("CategoriaID").value = 0;
    document.getElementById("Descripcion").value = "";
    document.getElementById("FechaRegistro").value = "";
    document.getElementById("Disponibilidad").value = 0;
}

function NuevoRegistro(){
    $("#ModalTitulo").text("Nueva Categoria");
}

function AbrirModalEditar(categoriaID){
    
    $.ajax({
        // la URL para la petición
        url: '../../Categorias/ListadoCategorias',
        // la información a enviar
        // (también es posible utilizar una cadena de datos)
        data: { categoriaID: categoriaID},
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
            document.getElementById("Descripcion").value = categoria.descipcion;
            document.getElementById("FechaRegistro").value = categoria.fechaRegistro;
            document.getElementById("Disponibilidad").value = categoria.disponibilidad;
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

function GuardarRegistro(){
    //GUARDAMOS EN UNA VARIABLE LO ESCRITO EN EL INPUT DESCRIPCION
    let categoriaID = document.getElementById("CategoriaID").value;
    let descripcion = document.getElementById("Descripcion").value;
    let fechaRegistro = document.getElementById("FechaRegistro").value;
    let disponibilidad = document.getElementById("Disponibilidad").value;
    $.ajax({
        // la URL para la petición
        url: '../../Categorias/GuardarCategoria',
        // la información a enviar
        // (también es posible utilizar una cadena de datos)
        data: {categoriaID: categoriaID, descripcion: descripcion, fechaRegistro: fechaRegistro, disponibilidad: disponibilidad},
        // especifica si será una petición POST o GET
        type: 'POST',
        // el tipo de información que se espera de respuesta
        dataType: 'json',
        // código a ejecutar si la petición es satisfactoria;
        // la respuesta es pasada como argumento a la función
        success: function (resultado) {

            
            ListadoCategorias();
            $("#ModalCategorias").modal("hide");
        },

        // código a ejecutar si la petición falla;
        // son pasados como argumentos a la función
        // el objeto de la petición en crudo y código de estatus de la petición
        error: function (xhr, status) {
            console.log('Disculpe, existió un problema al guardar el registro');
        }
    });    
}