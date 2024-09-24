window.onload = MostrarVista();


function MostrarVista() {
    $.ajax({
        url: '../../Vistas/MostrarVista',
        data: {},
        type: 'POST',
        dataType: 'json',
        success: function (vistasDeModelos) {
            let contenidoTabla = ``;
            $.each(vistasDeModelos, function (index, venta) {
                contenidoTabla +=
                    '<tr class="text-center">'
                    + '<td class="text-center"> ' + venta.descripcion + '</td>'
                    + '<td class="text-center"></td>'
                    + '<td class="text-center"></td>'
                    + '</tr>';

                $.each(venta.vistaProductosMostrarss, function (index, ventas) {
                    contenidoTabla +=
                        '<tr class="text-center">'
                        + '<td class="text-center"></td>'
                        + '<td class="text-center"> ' + ventas.nombre + '</td>'
                        + '<td class="text-center"> ' + ventas.fechaRegistroString + '</td>'
                        + '<td class="text-center"></td>'
                        + '</tr>';
                });
            });
            document.getElementById("tablas").innerHTML = contenidoTabla;
        },
        error: function (xhr, status) {
            alert("Disculpe, Existio Un Problema.");
        }
    });
}