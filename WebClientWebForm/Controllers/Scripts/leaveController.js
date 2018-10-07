var token = url_query('token');
$(document).ready(function () {
    var table = $('#example1').DataTable({
        "columnDefs": [
            {
                "targets": [0],
                className: "hidden"
            }
        ]
    });
    loadLeaveTypeDropDown();
    $('#fromDate').datepicker({
        format: 'dd/mm/yyyy',
        autoclose: true,
        todayHighlight: true,
        calendarWeeks: true
    });
    $('#toDate').datepicker({
        format: 'dd/mm/yyyy',
        autoclose: true,
        todayHighlight: true,
        calendarWeeks: true
    });
    console.log("Token: " + token);
    $('#cancelButton').on('click', function (e) {
        clearAll();
    });
    $('#submitButton').on('click', function (e) {

        // No token is in url, so access forbidden
        if (!token) {
            toastr.options =
                {
                    "closeButton": true,
                    "debug": false,
                    "positionClass": "toast-bottom-right",
                    "onclick": null,
                    "showDuration": "1000",
                    "hideDuration": "1000",
                    "timeOut": "5000",
                    "extendedTimeOut": "1000",
                    "showEasing": "swing",
                    "hideEasing": "linear",
                    "showMethod": "fadeIn",
                    "hideMethod": "fadeOut"
                };

            toastr.error('You are not allowed to perform this action!', 'Leave Notification');
            return;
        }
        var leaveId = $('#leaveId').val();
        var leaveTypeId = $('#leaveDropdown').val();
        var dateStart = $('#fromDate').val();
        var dateEnd = $('#toDate').val();
        var leaveCause = $('#leaveCause').val();
        var leaveAddress = $('#leaveAddress').val();

        var apiUrl;
        if (leaveId === null || leaveId === "") {
            apiUrl = apiUrlPrefix + "/leaves/insert";
        } else {
            apiUrl = apiUrlPrefix + "/leaves/update";
        }
        // start of $.ajax
        $.ajax({
            type: "POST",
            url: apiUrl,

            data: JSON.stringify({
                // leave parameter starts
                'Id': leaveId,
                'LeaveTypeId': leaveTypeId,
                //'UserName': userName, // userName is skipped now, we shall extract it from the token
                'DateStart': dateStart,
                'DateEnd': dateEnd,
                'LeaveCause': leaveCause,
                'LeaveAddress': leaveAddress
                // leave parameter ends
            }),

            contentType: 'application/json; charset = utf-8',
            dataType: 'json',

            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authorization", "Bearer " + token);
            },

            success: function (data) {

                console.log(data);

                if (data === "expired") {
                    console.log("token expired");
                    ShowNotification('Sorry, session expired, you\'ve to sign in again', 'Session Notification', 'warning');
                    return;
                }
                ShowNotification('Leave Submit Successfull', 'Leave Notification', 'success');
                loadTable(token);
            },

            failure: function () {
                ShowNotification('Leave Submission Failled', 'Leave Notification', 'error');
            }
        }
        );
    });
    loadTable(token);
    $('#example1 tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
            console.log("if");
        }
        else {
            table.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
            loadDataForEdit($(this));
        }
    });
    $('#submitButton').click(function () {
        table.row('.selected').remove().draw(false);
    });
});
function loadLeaveTypeDropDown() {
    $.ajax({
        type: "GET",
        url: leavetypeUrl,
        contentType: 'application/json; charset = utf-8',
        dataType: 'json',
        success: function (data) {
            console.log("Leave Data" + data);
            //console.log(data.length); // this data response is coming as js object, don't know why

            $('#leaveDropdown').empty();

            for (var i = 0; i < data.length; i++) {
                $('#leaveDropdown')
                    .append($('<option>',
                        {
                            value: data[i]["id"],
                            text: data[i]["name"]
                        })
                    );
            }

            //$('#dropdownLeave').selectpicker('refresh');
        },

        failure: function () {
            console.log("Users Get Failed!");
        }
    }
    );
}
function loadDataForEdit(row) {
    var tds = row.find("td");
    $('#leaveId').val(tds[0].innerHTML);
    $('#leaveDropdown').val(tds[2].innerHTML);
    $('#fromDate').val(tds[3].innerHTML);
    $('#toDate').val(tds[3].innerHTML);
    $('#leaveCause').val(tds[4].innerHTML);
    $('#leaveAddress').val(tds[5].innerHTML);

    $('#submitButton').text("Update");
}
function loadTable(token) {
    $.ajax({
        type: "GET",
        url: leaveUrl + "/own",

        contentType: "application/json; charset = utf-8",
        dataType: "json",

        beforeSend: function (xhr) {
            xhr.setRequestHeader("Authorization", "Bearer " + token);
        },

        success: function (data) {
            console.log(data);
            var leaveArray = data;
            $("#example1").DataTable().clear();

            for (var i = 0; i < leaveArray.length; i++) {
                var id = leaveArray[i]["id"];
                var leaveTypeName = leaveArray[i]["leaveTypeName"];
                var leaveTypeId = leaveArray[i]["leaveTypeId"];
                var dateStart = leaveArray[i]["dateStart"];
                var leaveCause = leaveArray[i]["leaveCause"];
                var leaveAddress = leaveArray[i]["leaveAddress"];
                $("#example1").dataTable().fnAddData([
                    id,
                    leaveTypeName,
                    leaveTypeId,
                    dateStart,
                    leaveCause,
                    leaveAddress
                ]);
            }
        },

        failure: function () {
            console.log("Users Get Failed!");
        }
    }
    );
}