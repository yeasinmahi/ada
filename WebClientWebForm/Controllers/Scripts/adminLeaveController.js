var token = url_query('token');
$(document).ready(function () {
    loadAutoComplete();
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
            ShowNotification('You are not allowed to perform this action!', 'Session Notification', 'warning');
            return;
        }
        var leaveId = $('#leaveId').val();
        var userNmae = $('#userAutoComplete').val();
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
                'UserName': userNmae,
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
    $('#userAutoComplete').val(tds[1].innerHTML);
    $('#leaveId').val(tds[0].innerHTML);
    $('#leaveDropdown').val(tds[3].innerHTML);
    //$("#leaveDropdown").find("option[text='" + tds[2].innerHTML + "']").attr("selected", true);
    $('#fromDate').val(tds[4].innerHTML);
    $('#toDate').val(tds[4].innerHTML);
    $('#leaveCause').val(tds[5].innerHTML);
    $('#leaveAddress').val(tds[6].innerHTML);

    $('#submitButton').text("Update");
}
function loadTable(token) {
    $.ajax({
        type: "GET",
        url: leaveUrl + "/all",

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
                var userName = leaveArray[i]["userName"];
                var leaveTypeName = leaveArray[i]["leaveTypeName"];
                var leaveTypeId = leaveArray[i]["leaveTypeId"];
                var dateStart = leaveArray[i]["dateStart"];
                var leaveCause = leaveArray[i]["leaveCause"];
                var leaveAddress = leaveArray[i]["leaveAddress"];
                $('#example1').dataTable().fnAddData([
                    id,
                    userName,
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
function loadAutoComplete() {
    $("#userAutoComplete").autocomplete({
        source: function (request, response) { getUsers(request, response) },
        minLength: 1
    });
}
function getUsers(request,response) {
    $.ajax({
        type: "GET",
        url: usersUrl,
        contentType: "application/json; charset = utf-8",
        dataType: "json",
        data: {
            searchKey: request.term
        },
        beforeSend: function (xhr) {
            xhr.setRequestHeader("Authorization", "Bearer " + token);
        },

        success: function (data) {
            console.log("Users : ");
            console.log(data);
            response($.map(data, function (item) {
                console.log(item);
                return {
                    label: item.userName + "  "+ item.id,
                    value: item.userName
                };
            }));
        },

        failure: function () {
            console.log("Users Get Failed!");
        }
    }
    );
}