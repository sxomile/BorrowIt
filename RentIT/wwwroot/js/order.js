var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/customer/order/getall' },
        "columns": [
            { data: 'itemName', "width": "30%" },
            { data: 'lender.userName', "width": "20%" },
            { data: 'borrower.userName', "width": "20%" },

            {
                data: 'rentTime',
                "render": (data) => {
                    return data + ' days'
                },
                "width": "5%"
            },

            {
                data: 'orderId',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                     <a href="/customer/order/details?id=${data}" class="btn btn-info mx-2"> <i class="bi bi-info-circle"></i>Info</a>
                    </div>`
                },
                "width": "25%"
            }

        ]
    });
}

