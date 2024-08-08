window.onload = BuscarPromocion();

function BuscarPromocion() {
    $.ajax({
        url: '../../Promociones/BuscarPromocion',
        data: {},
        type: 'POST',
        dataType: 'json',
        success: function (promocion) {
            $("#staticBackdrop").modal("hide");
            let contenidoTabla = ``;

            $.each(promocion, function (index, promociones) {
                contenidoTabla += `
                <tr>
                    <td> ${promociones.nombre}</td>
                    <td> ${promociones.totalAPagar}</td>
                    <td> ${promociones.fechaRegistro}</td>
                    <td class="text-center">
                        <button type="button" class="btn btn-success" onclick="AbrirModalPromocion(${promociones.promocionID})"><i class="fa-solid fa-pen-to-square"></i></Button>
                    </td>
                    <td class ="text-center" >
                        <button type="button" class="btn btn-danger" onclick="EliminarPromocion(${promociones.promocionID})"><i class="fa-solid fa-trash"></i></button>
                    </td>
                </tr>
                `;
                // document.getElementById("TablaPromocionCreado").innerHTML = contenidoTabla;
            });
            document.getElementById("TablaPromocionCreado").innerHTML = contenidoTabla;
        },
        error: function (xhr, status) {
            alert("Disculpe, Existio Un Problema.");
        },

    })
}


function GuardarPromocion() {
    let promocionID = document.getElementById("PromocionID").value;
    let nombre = document.getElementById("Nombre").value;
    let fechaRegistro = document.getElementById("FechaRegistro").value;

    $.ajax({
        url: '../../Promociones/GuardarPromocion',
        data: { promocionID: promocionID, nombre: nombre, fechaRegistro: fechaRegistro },
        type: 'POST',
        dataType: 'json',
        success: function (resultado) {
            BuscarPromocion();
        },
        error: function (xhr, status) {
            alert("Disculpe Existio Un Problema.");
        }
    });
}


function EliminarPromocion(promocionID) {
    $.ajax({
        url: '../../Promociones/EliminarPromocion',
        data: { promocionID: promocionID },
        type: 'POST',
        dataType: 'json',
        success: function (resultado) {
            BuscarPromocion();
        },
        error: function (xhr, status) {
            alert("Disculpe, Existio Un Problema.");
        }
    });
}


function AbrirModalPromocion(promocionID) {
    $.ajax({
        url: '../../Promociones/BuscarPromocion',
        data: { id: promocionID },
        type: 'POST',
        dataType: 'json',
        success: function (promocion) {
            let promociones = promocion[0];

            document.getElementById("PromocionID").value = promocionID;
            document.getElementById("Nombre").value = promociones.nombre;
            document.getElementById("FechaRegistro").value = promociones.fechaRegistro;
            $("#staticBackdropLabel").text("Editar Promocion");
            $("#staticBackdrop").modal("show");

        },
        error: function (xhr, status) {
            alert("Disculpe, Existio Un Problema.");
        }
    });
}