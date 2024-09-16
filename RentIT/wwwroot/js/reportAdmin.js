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
            { data: 'userName', "width": "30%" },     
            { data: 'description', "width": "50%" },    
            {
                data: 'id',
                "render": function (data, type, row) {
                    return `<div class="w-75 btn-group" role="group">
                        <a onClick=Delete('/customer/report/delete/${data}') class="btn btn-danger mx-2"> 
                            <i class="bi bi-exclamation-triangle"></i>Dismiss report
                        </a>
                    </div>`;
                },
                "width": "20%"
            }
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, dismiss it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax(
                {
                    url: url,
                    type: 'DELETE',
                    success: (data) => {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    }
                })
        }
    });
}