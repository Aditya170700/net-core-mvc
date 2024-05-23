$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        ajax: {
            url: '/admin/product/getall',
            dataSrc: 'data'
        },
        columns: [
            { data: 'title', width: '25%' },
            { data: 'isbn', width: '15%' },
            { data: 'category.name', width: '15%' },
            { data: 'author', width: '15%' },
            { data: 'listPrice', width: '10%' },
            {
                data: 'id',
                render: function (data) {
                    return `<duv class="w-75 bth-group" role="group">
                        <a href="/admin/product/upsert?id=${data}" class="btn btn-sm btn-warning rounded mx-1"><i class="bi bi-pencil-square"></i></a>
                        <a href="/admin/product/delete?id=${data}" class="btn btn-sm btn-danger rounded mx-1"><i class="bi bi-trash-fill"></i></a>
                    </div>`;
                }
            }
        ]
    });
}
