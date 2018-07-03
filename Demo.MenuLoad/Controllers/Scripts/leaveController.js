function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}
var token = getParameterByName('token');
$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: apiUrlPrefix + "/leavetype",
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
        var leaveTypeId = $('#leaveDropdown').val();
        var dateStart = $('#fromDate').val();
        var dateEnd = $('#toDate').val();
        var leaveCause = $('#leaveCause').val();
        var leaveAddress = $('#leaveAddress').val();

        // start of $.ajax
        $.ajax({
            type: "POST",
            url: apiUrlPrefix + "/leaves/own",

            data: JSON.stringify({
                // leave parameter starts
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

                    toastr.warning('Sorry, session expired, you\'ve to sign in again', 'Session Notification');

                    //window.location = "SignIn.aspx";
                    return;
                }
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
                toastr.success('Leave Submitted Successfully!', 'Leave Notification');
            },

            failure: function () {
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
                toastr.error('Leave Submission Failed!', 'Leave Notification');
            }
        }
        );
        // end of $ ajax



    });


});