//Valdidation using jquery
function validate() {
    var isValid = true;
    if ($('#Name').val().trim() == "") {
        $('#Name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Name').css('border-color', 'lightgrey');
    }
    if ($('#Age').val().trim() == "") {
        $('#Age').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Age').css('border-color', 'lightgrey');
    }
    if ($('#State').val().trim() == "") {
        $('#State').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#State').css('border-color', 'lightgrey');
    }
    if ($('#Country').val().trim() == "") {
        $('#Country').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Country').css('border-color', 'lightgrey');
    }
    return isValid;
}

//Add Data Function 
function Add() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var empObj = {
        EmployeeID: $('#EmployeeID').val(),
        Name: $('#Name').val(),
        Age: $('#Age').val(),
        State: $('#State').val(),
        Country: $('#Country').val()
    };
    $.ajax({
        url: "/Home/Add",
        data: JSON.stringify(empObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
$('.modal').on('shown.bs.modal', function () {
    $('#bithday').datepicker({
        format: "dd/mm/yyyy",
        //startDate: "01-01-2015",
        //endDate: "01-01-2020",
        todayBtn: "linked",
        autoclose: true,
        todayHighlight: true,
        //container: '#myModal modal-body'
    });
});
$(document).ready(function () {
    $("#myModal").show();
    $('.tblEmployee').DataTable({
        'paging': true,
        'lengthChange': true,
        'searching': true,
        "sDom": 'Rfrtlip',
        "pageLength": 10,
        //'ordering': true,
        //'info': true,
        'autoWidth': false,
        //'scrollX': true,
        //  'lengthMenu': [[5, 10, 15, 20, 25], [5, 10, 15, 20, 25]],
        //"fixedColumns": true,
        //"sScrollX": "100%",
        //"sScrollXInner": "100%",
        //"bScrollCollapse": true,
        //"columnDefs": [{
        //    "targets": 'countclass',
        //    "orderable": false,
        //}]
    });
});
//addnew popup
$("#addEmpNew").click(function () {
    var options = {
        "backdrop": "static",
        keyboard: true
    };
    $.ajax({
        type: 'GET',
        url: '/Employee/CreateEmployee',
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (data) {
            debugger;
            $('.modalbodypopup').html(data);
            $('#popupmodel').modal(options);
            $('#popupmodel').modal('show');
            $("#bithday").val("");

        },
        error: function () {
            alert("Content load failed.");
        }
    });
});

//country on change
$("#ContryID").change(function () {
    $("#StateID").empty();
    $.ajax({
        type: 'POST',
        url: '/Employee/ContryChange',
        dataType: 'json',
        data: { id: $("#ContryID").val() },
        success: function (city) {

            $.each(city, function (i, city) {
                $("#StateID").append('<option value="'
                    + city.Value + '">'
                    + city.Text + '</option>');
            });
        },
        error: function (ex) {
            alert('Failed.' + ex);
        }
    });
    return false;
})
//deleteuser popup
$("#Deletepopup").on("click", "#DeleteUser", function () {
    // $("#DeleteUser").click(function () {
    debugger;
    var deleteuser = $("#HiddenEmployeeid").val();
    $.ajax({
        type: 'POST',
        data: { Id: deleteuser },
        url: '/Employee/DeleteEmployee',
        success: function (response) {
            window.location.reload();
        }
    });
})

$(".Employeediv").on("click", "a[id*='Editempbtn_']", function () {
    var id = this.id.split('_')[1];
    // var id = $(this).parent().find('#hdnRoomId').val()
    EmployeeEdit(id);
});
//$("#DeleteUser").click(function () {
$(".Employeediv").on("click", "a[id*='Deleteempbtn_']", function () {
    debugger;
    var id = this.id.split('_')[1];
    // var id = $(this).parent().find('#hdnRoomId').val()
    EmployeeDelete(id);
});
function EmployeeEdit(id) {
    debugger;
    var options = {
        "backdrop": "static",
        keyboard: true
    };
    $.ajax({
        type: 'POST',
        data: { id: id },
        url: '/Employee/EditEmployee',
        success: function (data) {
            $('.modalbodypopup').html(data);
            //$('#popupmodel').modal(options);
            //$('#popupmodel').modal('show');
        },
        error: function () {
            alert("Content load failed.");
        }
    });
}
function EmployeeDelete(id) {
    debugger;
    var $description = $('<div/>');
    $description.append($('<p/>').html('<h3> Are you sure you want to permanently Delete Employee?  </h3></br>'));
    $description.append($('<p/>').html('<input type="hidden" id="HiddenEmployeeid" name="custId" value=' + id + '></br>'));
    $('#Deletepopup .modalbodypopup').empty().html($description);
    $('#Deletepopup').modal();
}
