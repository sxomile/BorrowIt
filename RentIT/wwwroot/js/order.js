var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/customer/order/getall' },
        "columns": [
            { data: 'itemName', "width": "30%" },
            { data: 'lender.userName', "width": "30%" },
            { data: 'borrower.userName', "width": "30%" },

            //{
            //    data: 'rentTime',
            //    "render": (data) => {
            //        return data + ' days'
            //    },
            //    "width": "10%"
            //},
            //<a onClick=Delete('/customer/myitem/delete/${data}') class= "btn btn-danger mx-2" > <i class="bi bi-trash-fill"></i>Delete</a >
            {
                data: 'orderId',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                     <a href="/customer/order/details?id=${data}" class="btn btn-info mx-2"> <i class="bi bi-info-circle"></i>Info</a>
                    </div>`
                },
                "width": "10%"
            }

        ]
    });
}

//function Delete(url) {
//    Swal.fire({
//        title: "Are you sure?",
//        text: "You won't be able to revert this!",
//        icon: "warning",
//        showCancelButton: true,
//        confirmButtonColor: "#3085d6",
//        cancelButtonColor: "#d33",
//        confirmButtonText: "Yes, delete it!"
//    }).then((result) => {
//        if (result.isConfirmed) {
//            $.ajax(
//                {
//                    url: url,
//                    type: 'DELETE',
//                    success: (data) => {
//                        dataTable.ajax.reload();
//                        toastr.success(data.message);
//                    }
//                })
//        }
//    });
//}