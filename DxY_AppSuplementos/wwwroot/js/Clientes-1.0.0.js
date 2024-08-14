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
    doc.text('Listado de Clientes', 14, 22);

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
            3: { halign: 'center', fontSize: 7 }
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