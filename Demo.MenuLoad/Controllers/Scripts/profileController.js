var token = url_query('token');
$(document).ready(function () {
    getProfile();
    $('#dateOfJoiningTextBox').datepicker({
        format: 'dd/mm/yyyy',
        autoclose: true,
        todayHighlight: true,
        calendarWeeks: true
    });
    console.log("Token: " + token);
    $('#submitButton').on('click', function (e) {
        console.log("submit button clicked");
        // No token is in url, so access forbidden
        if (!token) {
            ShowNotification('Token expaire', 'Leave Notification', 'warning');
            return;
        }
        var nameTextBox = $('#nameTextBox').val();
        var emailTextBox = $('#emailTextBox').val();
        var designationTextBox = $('#designationTextBox').val();
        var dateOfJoiningTextBox = $('#dateOfJoiningTextBox').val();
        var educationTextBox = $('#educationTextBox').val();
        var currentAddressTextBox = $('#currentAddressTextBox').val();
        var parmanentAddressTextBox = $('#parmanentAddressTextBox').val();
        var noteTextBox = $('#noteTextBox').val();
        
        $.ajax({
            type: "POST",
            url: profileUrl+"/update",

            data: JSON.stringify({
                // leave parameter starts
                'UserName': emailTextBox,
                'FullName': nameTextBox,
                'Email': emailTextBox,
                'Designation': designationTextBox,
                'DateOfJoining': dateOfJoiningTextBox,
                'Education': educationTextBox,
                'CurrentAddress': currentAddressTextBox,
                'ParmanentAddress': parmanentAddressTextBox,
                'Note': noteTextBox
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
                if (data === "failed") {
                    console.log("token expired");
                    ShowNotification('Profile Update Failed', 'Profile Notification', 'error');
                    return;
                }
                ShowNotification('Profile Update Successfull', 'Profile Notification', 'success');
                getProfile();
            },

            failure: function () {
                ShowNotification('Profile Submission Failled', 'Profile Notification', 'error');
            }
        }
        );
    });
    
});

function getProfile() {
    console.log("Get Profile");

    jQuery.ajax(
        {
            type: "GET",
            url: userUrl,

            data: {
                'TokenContent': token
            },
            contentType: 'application/json; charset = utf-8',
            dataType: 'json',

            /*
            beforeSend: function (xhr)
            {
                xhr.setRequestHeader("Authorization", "Bearer " + token);
            },
            */
            success: function (data) {
                console.log(data);
                $("#profileUserName").text(data.FullName);
                $("#profileDesignation").text(data.Designation);
                $("#profileEducation").text(data.Education);
                $("#profileCurrentLocation").text(data.CurrentAddress);
                $("#profileNote").text(data.Note);

                $("#nameLabel").text(data.FullName);
                $("#emailLabel").text(data.Email);
                //$("#facebookAddressLabel").text(data.Email);
                //$("#googleAddressLabel").text(data.Email);
                $("#designationLabel").text(data.Designation);
                $("#dateOfJoiningLabel").text($.datepicker.formatDate('dd M yy', new Date(data.DateOfJoining)));
                $("#educationLabel").text(data.Education);
                $("#currentAddressLabel").text(data.CurrentAddress);
                $("#parmanentAddressLabel").text(data.ParmanentAddress);
                $("#noteLabel").text(data.Note);

                $("#nameTextBox").val(data.FullName);
                $("#emailTextBox").val(data.Email);
                //$("#facebookAddressTextBox").val(data.Email);
                //$("#googleAddressTextBox").val(data.Email);
                $("#designationTextBox").val(data.Designation);
                $("#dateOfJoiningTextBox").val($.datepicker.formatDate('dd/mm/yy', new Date(data.DateOfJoining)));
                $("#educationTextBox").val(data.Education);
                $("#currentAddressTextBox").val(data.CurrentAddress);
                $("#parmanentAddressTextBox").val(data.ParmanentAddress);
                $("#noteTextBox").val(data.Note);
            },

            failure: function () {

            }
        }
    );
    // end of jQuery ajax
}