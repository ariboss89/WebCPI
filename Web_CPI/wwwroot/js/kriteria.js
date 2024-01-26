var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable(data) {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Kriteria/GetAll"
        },
        "columns": [
            { "data": "id", "width": "20%" },
            { "data": "nama", "width": "20%" },
            { "data": "type", "width": "10%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                    <div class="w-100">
                        <a href="/Kriteria/Upsert/${data}"style="background-color: var(--secondary-color)" class="col-5 btn text-white"> <i class="bi bi-pencil-square"></i>Edit</a>

                        <a onclick=Delete("/Kriteria/Delete/${data}") class="col-5 btn btn-danger text-white" style="cursor:pointer; margin-left:10px;">
                        <i class="bi bi-trash3"></i>
                        Delete</a>
                     </div>
                    `;
                }, "width": "20%"
            }
        ]
    });
}

function Delete(url) {
    swal({
        title: "Are you sure you want to Delete?",
        text: "You will not be able to restore the data !",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}