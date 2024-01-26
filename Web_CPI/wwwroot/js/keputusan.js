var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable(data) {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Penilaian/GetAll"
        },
        "columns": [
            { "data": "id", "width": "10%" },
            { "data": "alternatif", "width": "15%" },
            { "data": "kriteria", "width": "15%" },
            { "data": "subkriteria", "width": "20%" },
            { "data": "nilai", "width": "20%" }
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

function Search() {
    var id = document.getElementById("cbKriteria").value;
    var jenisx = document.getElementById("txtJenis").value;

    $.ajax({
        type: "GET",
        url: "/Penilaian/GetDataKriteria/" + id,
        success: function (data) {

            var data2 = JSON.stringify(data);

            var penilaian = JSON.parse(data2);

            $("#txtJenis").val(penilaian['penilaian']['type']);

            let ele = document.getElementById('cbSubKriteria');

            if (ele.length > 1) {
                $("#cbSubKriteria").find("option").remove().end().append(
                    '<option disabled value = "Pilih Subkriteria">Pilih Subkriteria</option>');
            }

            var param = penilaian.listSub;

            for (let i = 0; i < param.length; i++) {
                ele.innerHTML += '<option value="' + param[i]['id'] + '">' + param[i]['pilihan'] + '</option>';
            }

            $("#txtNilai").val("");

        },
        error: function (data) {

            alert("GAGAL " + id);
        }
    });
}

function Search2() {
    var id = document.getElementById("cbSubKriteria").value;
    var nilai = document.getElementById("txtNilai").value;

    $.ajax({
        type: "GET",
        url: "/Penilaian/GetDataNilai/" + id,
        success: function (data) {
            var data2 = JSON.stringify(data);

            var penilaian = JSON.parse(data2);

            $("#txtNilai").val(penilaian['data']['nilai']);

        },
        error: function (data) {

            alert("GAGAL " + id);
        }
    });
}