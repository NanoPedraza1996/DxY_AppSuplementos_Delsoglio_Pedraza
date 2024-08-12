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




function EliminarCategoria(categoriaID, eliminado) {
    $.ajax({
        // la URL para la petición
        url: '../../Categorias/EliminarCategoria',
        // la información a enviar
        // (también es posible utilizar una cadena de datos)
        data: { categoriaID: categoriaID, eliminado: eliminado },
        // especifica si será una petición POST o GET
        type: 'POST',
        // el tipo de información que se espera de respuesta
        dataType: 'json',
        // código a ejecutar si la petición es satisfactoria;
        // la respuesta es pasada como argumento a la función
        success: function (resultado) {
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
                    title: "¡Eliminado!",
                    showConfirmButton: false,
                    timer: 1200
                });
                ListadoCategorias();
            }
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
                ListadoCategorias();
            }
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
    if (!descripcion) {
        $("#error").text("*Debes Ingresar una Descripcion");
        guardar = false;
    }

    if (guardar) {
        $.ajax({
            url: '../../Categorias/GuardarCategoria',
            data: { categoriaID: categoriaID, descripcion: descripcion },
            type: 'POST',
            // dataType: 'json',
            success: function (resultado) {
                if (resultado == "") {
                    Swal.fire({
                        position: "top-end",
                        icon: "success",
                        title: "¡Guardado!",
                        showConfirmButton: false,
                        timer: 1200
                    });
                    ListadoCategorias();
                    $("#ModalCategorias").modal("hide");
                }
                else {
                    Swal.fire({
                        icon: "error",
                        title: "Vaya Vaya...",
                        text: "Parece Que Ya Existe Otra Descripcion!",
                        footer: '<a href="../../Categorias/Index">Ver Existente</a>'
                    });
                }
            },
            error: function (xhr, status) {
                console.log('Disculpe, existió un problema al guardar el registro');
            }
        });
    }
    return false;
}



function Imprimir() {
    var doc = new jsPDF({
        orientation: "landscape",
        unit: "in",
        format: [4, 2]
    });
    doc.text("Hello world!", 1, 1);
    doc.save("a4.pdf");

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
    doc.text('Listado de Categorias', 14, 22);

    // Add current date
    var today = new Date();
    var dateString = today.toLocaleDateString(); // Format date as needed
    doc.setFontSize(12);
    doc.setFontStyle('normal');
    doc.text('Fecha: ' + dateString, 14, 32);

    var elem = document.getElementById("tabla-imprimir");
    var res = doc.autoTableHtmlToJson(elem);

    // Remove last two columns
    res.columns = res.columns.slice(0, -2);
    res.data = res.data.map(row => row.slice(0, -2));

    doc.autoTable(res.columns, res.data, {
        addPageContent: pageContent,
        theme: 'grid',
        headStyles: { halign: 'center' }, // Center align headers
        columnStyles: {
            0: { halign: 'center', fontSize: 7 },
            1: { halign: 'center', fontSize: 7 }
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





// function validarFormulario() {
//     let Descripcion = document.getElementById('Descripcion').value;
//     let esValido = true;

//     // Validar nombre
//     if (Descripcion === "") {
//         document.getElementById('Descripcion').textContent = "El nombre es obligatorio.";
//         esValido = false;
//     } else {
//         document.getElementById('Descripcion').textContent = "";
//     }

//     return esValido;
// }


// document.getElementById('miFormulario').addEventListener('submit', function (event) {
//     // Llama a la función de validación
//     if (!validarFormulario()) {
//         // Si la validación falla, previene el envío del formulario
//         event.preventDefault();
//     }
// });


// && descripcion === "" || !/\+@\+\.\+/.test(descripcion)


// function validarFormulario() {
//     let esValido = true;
//     let nombre = document.getElementById('nombre').value;
//     let email = document.getElementById('email').value;

// Validar campo de nombre
// if (nombre.trim() === '') {
//     alert('El campo de nombre es obligatorio.');
//     esValido = false;
// }
// Validar campo de email
//     if (email.trim() === '') {
//         alert('El campo de email es obligatorio.');
//         esValido = false;
//     } else if (!/\S+@\S+\.\S+/.test(email)) {
//         alert('Por favor, ingrese un email válido.');
//         esValido = false;
//     }

//     return esValido;
// }




// Captura del Evento submit: Utiliza el evento submit para interceptar el envío del formulario y realizar la validación.Puedes hacerlo agregando un listener al formulario.
// Función de Validación: Crea una función que verifique los datos ingresados en los campos del formulario.Aquí hay un ejemplo básico:
// Prevención del Envío: Si la validación falla, utiliza event.preventDefault() para evitar que el formulario se envíe.