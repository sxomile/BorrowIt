var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            url: '/customer/report/getall',
        },
        "columns": [
            { data: 'adminName', "width": "30%" }, 
            { data: 'description', "width": "70%" }  
        ]
    });
}
