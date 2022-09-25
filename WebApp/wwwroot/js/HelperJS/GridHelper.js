
function CreateCommonGridWithData(table, data, columns) {

    $(table).DataTable({
        dom: 'Bfrtip',
        "autoWidth": false,
        "processing": true,
        "filter": true, // this is for disable filter (search box)  
        "orderMulti": false, // for disable multiple column at once  
        "ordering": true,
        "scrollX": true,
        data: data,
        columns: columns
    });
}
function CreateCommonGridAjax(table, url, columns) {
    //WHAT TO PASS IN COLUMNS ARRAY
    //[
    //    { "data": "id", "name": "Id" },
    //]

    $(table).DataTable({
        dom: 'Bfrtip',
        "autoWidth": false,
        "processing": true,
        "filter": true, // this is for disable filter (search box)  
        "orderMulti": false, // for disable multiple column at once  
        "ordering": true,
        "scrollX": true,
        buttons: [
            'colvis',
            'excel',
            'print'
        ],
        "ajax": {
            "type": "GET",
            "url": url,
            "dataSrc": function (json) {
                return json
            }
        },

        columns: columns
    });
}

function CreateCommonGridServerSide(table, url, columns) {
    $(table).DataTable({
        dom: 'Bfrtip',
        "processing": true, // for show progress bar  
        "serverSide": true, // for process server side  
        "autoWidth": false,
        "filter": true, // this is for disable filter (search box)  
        "orderMulti": false, // for disable multiple column at once  
        "ordering": true,
        "scrollX": true,
        buttons: [
            'colvis',
            'excel',
            'print'
        ],
        "ajax": {
            "type": "GET",
            "url": url,
            "dataSrc": function (json) {
                return json
            }
        },

        columns: columns
    });
}