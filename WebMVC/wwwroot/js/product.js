let dataTable;

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
                        <button type="button" onClick="destroy('/admin/product/delete?id=${data}')" class="btn btn-sm btn-danger rounded mx-1"><i class="bi bi-trash-fill"></i></button>
                    </div>`;
                }
            }
        ]
    });
}

function destroy (url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    });
}
