window.onload = BuscarCliente();

function BuscarCliente() {
    $.ajax({
        url: '../../Clientes/BuscarCliente',
        data: {},
        type: 'POST',
        dataType: 'json',
        success: function (cliente) {
            $("#staticBackdrop").modal("hide");

            let contenidoTabla = ``;
            $.each(cliente, function (index, clientes) {
                contenidoTabla += `
                <tr>
                    <td class="text-center">${clientes.nombreCompleto}</td>
                    <td class="text-center">${clientes.telefono}</td>
                    <td class="text-center">${clientes.fechaCreacion}</td>
                    <td class="text-center">
                    <button type="button" class="btn btn-success" onclick="AbrirModalEditar(${clientes.clienteID})">
                    Editar
                    </button>
                    </td>
                    <td class="text-center">
                    <button type="button" class="btn btn-danger" onclick="EliminarCliente(${clientes.clienteID})">
                    Eliminar
                    </button>
                    </td>
                </tr>
                `;
            });
            document.getElementById("TablaClientes").innerHTML = contenidoTabla;
        },
        error: function (xhr, status) {
            alert("Disculpe, Existio Un Problema.");
        }
    });
}



function GuardarCliente() {
    let clienteID = document.getElementById("ClienteID").value;
    let nombreCompleto = document.getElementById("NombreCompleto").value;
    let telefono = document.getElementById("Telefono").value;
    $.ajax({
        url: '../../Clientes/GuardarCliente',
        data: { clienteID: clienteID, nombreCompleto: nombreCompleto, telefono: telefono },
        type: 'POST',
        success: function (resultado) {
            BuscarCliente();
            $("#staticBackdrop").modal("hide");
        },
        error: function (xhr, status) {
            alert("Disculpe, Existio Un Problema.");
        }
    });
}


function EliminarCliente(clienteID) {
    $.ajax({
        url: '../../Clientes/EliminarCliente',
        data: { clienteID: clienteID },
        type: 'POST',
        dataType: 'json',
        success: function (resultado) {
            BuscarCliente();
        },
        error: function (xhr, status) {
            alert("Disculpe, Existio Un Problema.");
        }
    });
}