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
                data: 'orderDate',
                "render": function (data, type, row) {

                    let borrowingDate = moment(row.orderDate);
                    let endDate = borrowingDate.add(row.rentTime, 'days');
                    return endDate.format('YYYY-MM-DD'); 
                },
                "width": "15%"
            },

            {
                data: null,
                "render": function (data, type, row) {
                    let borrowingDate = moment(row.orderDate);
                    let endDate = borrowingDate.add(row.rentTime, 'days');

                    if (endDate.isBefore(moment(), 'day') && row.isReturned == false) {
                        return `<div class="w-75 btn-group" role="group">
                        <a href="/customer/order/details?id=${row.orderId}" class="btn btn-danger mx-2"> <i class="bi bi-exclamation-triangle"></i>Late!</a>
                         </div>`
                    } else {
                        return `<div class="w-75 btn-group" role="group">
                        <a href="/customer/order/details?id=${row.orderId}" class="btn btn-info mx-2"> <i class="bi bi-info-circle"></i>Info</a>
                         </div>`
                    }
                },
                "width": "15%"
            }

        ]
    });
}
