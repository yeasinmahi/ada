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
        var applicableFor = jQuery('#applicableFor :selected').val();
        var companyPolicy = jQuery('#companyPolicy').val();
        var maxApplicationAtAMonth = jQuery('#isOnlyOneTime').val();
        var maximumAllowedAtATime = jQuery('#maxAllowedAtATime').val();
        var isHalfDayAllowed = jQuery('#maxApplicationAtAMonth').is(":checked");
        var isBalanceChecked = jQuery('#isHalfDayAllowed').is(":checked");
        var isOnlyOneTime = jQuery('#isBalanceCheck').is(":checked");
        var isRestricted = jQuery('#isRestrict').is(":checked");

        // start of jQuery.ajax
        jQuery.ajax({
            type: "POST",
            url: apiUrlPrefix + "/leavetype/insert",

            data: JSON.stringify({
                'Id': 0,
                'Name': name,
                'ApplicableFor': applicableFor,
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
                loadTable(token);
            },

            failure: function () {
                ShowNotification('Leave Type Submission Failled', 'Leave Type Notification', 'error');
            }
        }
        );
    });
    loadTable(token);


    var table = $('#example1').DataTable({
        "columnDefs": [
            {
                "targets": [0],
                "visible": false
            }
        ]
        //dom: 'Bfrtip',
        //buttons: [
        //    {
        //        text: 'Edit',
        //        action: function (e, dt, node, config) {
        //            alert('Edit Button Click');
        //        }
        //    }
        //]
    });
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

    $('#button').click(function () {
        table.row('.selected').remove().draw(false);
    });
});
function loadDataForEdit(row) {
    var tds = row.find("td");
    jQuery('#leaveTypeName').val(tds[0].innerHTML);
    jQuery('#applicableFor').val(getApplicableForDataRev(tds[1].innerHTML)).change();
    console.log(tds[1].innerHTML);
    console.log(getApplicableForDataRev(tds[1].innerHTML));
    jQuery('#companyPolicy').val(tds[2].innerHTML);
    jQuery('#maxAllowedAtATime').val(tds[3].innerHTML);
    jQuery('#maxApplicationAtAMonth').val(tds[4].innerHTML);
    jQuery("#isHalfDayAllowed").prop('checked', getBool(tds[5].innerHTML));
    jQuery('#isBalanceCheck').prop('checked', getBool(tds[6].innerHTML));
    jQuery('#isOnlyOneTime').prop('checked', getBool(tds[7].innerHTML));
    jQuery('#isRestrict').prop('checked', getBool(tds[8].innerHTML));
}
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
                var applicableFor = getApplicableForData(leaveTypeArray[i]["applicableFor"]);
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
                        maximumAllowedAtATime,
                        maxApplicationAtAMonth,
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

function getApplicableForData(data) {
    if (data === "B") {
        return "All";
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
function getApplicableForDataRev(data) {
    if (data === "All") {
        return "B";
    } else if (data === "Only Male") {
        return "M";
    }
    else if (data === "Only Female") {
        return "F";
    }
    else {
        return data;
    }
}
function getBool(data) {
    if (data === "true") {
        return true;
    }
    else if (data === "false") {
        return false;
    }
}

