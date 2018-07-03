var token = url_query('token');
$(document).ready(function () {

    jQuery('#submitButton').on('click', function (e) {
        console.log("Button Click");
        // No token is in url, so access forbidden
        if (!token) {
            ShowNotification('You are not allowed to perform this action!', 'Leave Notification', 'error');
            return;
        }
        var name = jQuery('#leaveTypeName').val();
        var applicationFor = jQuery('#applicableFor').val();
        var companyPolicy = jQuery('#companyPolicy').val();
        var maximumAllowedAtATime = jQuery('#maxAllowedAtATime').val();
        var isHalfDayAllowed = jQuery('#maxApplicationAtAMonth').is(":checked");
        var isBalanceChecked = jQuery('#isHalfDayAllowed').is(":checked");
        var isOnlyOneTime = jQuery('#isBalanceCheck').is(":checked");
        var maxApplicationAtAMonth = jQuery('#isOnlyOneTime').val();
        var isRestricted = jQuery('#isRestrict').is(":checked");

        // start of jQuery.ajax
        jQuery.ajax({
            type: "POST",
            url: apiUrlPrefix + "/leavetype/insert",

            data: JSON.stringify({
                'Id': 0,
                'Name': name,
                'ApplicationFor': applicationFor,
                'CompanyPolicy': companyPolicy,
                'MaximumAllowedAtATime': maximumAllowedAtATime,
                'IsHalfDayAllowed': isHalfDayAllowed,
                'IsBalanceChecked': isBalanceChecked,
                'IsOnlyOneTime': isOnlyOneTime,
                'MaxApplicationAtAMonth': maxApplicationAtAMonth,
                'IsRestricted': isRestricted

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
                ShowNotification('Leave Type Submit Successfull', 'Leave Type Notification', 'success');
            },

            failure: function () {
                ShowNotification('Leave Type Submission Failled', 'Leave Type Notification', 'error');
            }
        }
        );
    });
    loadTable(token);


    $('#example1').DataTable({
        "columnDefs": [
            {
                "targets": [0],
                "visible": false,
            }
        ]
    });
});
function loadTable(token) {
        jQuery.ajax({
            type: "GET",
            url: apiUrlPrefix + "/leavetype",

            contentType: 'application/json; charset = utf-8',
            dataType: 'json',

            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authorization", "Bearer " + token);
            },

            success: function (data) {
                console.log(data);
                var leaveTypeArray = data;
                console.log(leaveTypeArray);
                $("#example1").DataTable().clear();

                for (var i = 0; i < leaveTypeArray.length; i++) {
                    console.log(leaveTypeArray[i]);
                    var id = leaveTypeArray[i]["id"];
                    var name = leaveTypeArray[i]["name"];
                    var applicableFor = getApplicationForData(leaveTypeArray[i]["applicableFor"]);
                    var companyPolicy = leaveTypeArray[i]["companyPolicy"];
                    var maxApplicationAtAMonth = leaveTypeArray[i]["maxApplicationAtAMonth"];
                    var maximumAllowedAtATime = leaveTypeArray[i]["maximumAllowedAtATime"];
                    var isBalanceChecked = leaveTypeArray[i]["isBalanceChecked"];
                    var isHalfDayAllowed = leaveTypeArray[i]["isHalfDayAllowed"];
                    var isOnlyOneTime = leaveTypeArray[i]["isOnlyOneTime"];
                    var isRestricted = leaveTypeArray[i]["isRestricted"];

                    jQuery('#example1').dataTable().fnAddData
                        ([
                            id,
                            name,
                            applicableFor,
                            companyPolicy,
                            maxApplicationAtAMonth,
                            maximumAllowedAtATime,
                            isBalanceChecked,
                            isHalfDayAllowed,
                            isOnlyOneTime,
                            isRestricted
                        ]);

                }
            },

            failure: function () {
                console.log("Users Get Failed!");
            }
        }
        );
}

function getApplicationForData(data) {
    if (data==="B") {
        return "Male & Female";
    } else if (data === "M") {
        return "Only Male";
    }
    else if (data === "F") {
        return "Only Female";
    }
    else {
        return data;
    }
}

