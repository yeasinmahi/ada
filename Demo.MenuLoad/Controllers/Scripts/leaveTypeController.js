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
        var isHalfDayAllowed = jQuery('#maxApplicationAtAMonth').val();
        var isBalanceChecked = jQuery('#isHalfDayAllowed').val();
        var isOnlyOneTime = jQuery('#isBalanceCheck').val();
        var maxApplicationAtAMonth = jQuery('#isOnlyOneTime').val();
        var isRestricted = jQuery('#isRestrict').val();

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


});