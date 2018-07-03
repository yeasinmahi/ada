function GetDashboardUrl(token) {
    var url = '@Url.Action("Index", "Home")' + '?token=' + token;
    return url;
}
function url_query(query) {
    query = query.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var expr = "[\\?&]" + query + "=([^&#]*)";
    var regex = new RegExp(expr);
    var results = regex.exec(window.location.href);
    if (results !== null) {
        return results[1];
    } else {
        return false;
    }
}
function GetSignedPage(token, redirect) {
    if (redirect === '' || redirect ===false) {
        console.log("Redirect Page:" + redirect);
        var url = GetDashboardUrl(token);
        return url;
    } else {
        console.log("Redirect Page:" + redirect);
        return redirect;
        
    }
}
function ShowNotification(message, title, type) {
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
    if (type === null) {
        toastr.warning('Notification type should not be null', 'Notification Format Error');
    } else {
        if (type === "success") {
            toastr.success(message, title);
        } else if (type === "warning") {
            toastr.warning(message, title);
        }
        else if (type === "error") {
            toastr.error(message, title);
        } else {
            toastr.warning('Please provide proper type of notification', 'Notification Format Error');
        }
    }
}
function clearAll() {
    var elements = document.getElementsByTagName("input");
    for (var ii = 0; ii < elements.length; ii++) {
        if (elements[ii].type === "text") {
            elements[ii].value = "";
        } else if (elements[ii].type === "hidden") {
            elements[ii].value = null;
        } else if (elements[ii].type === "checkbox") {
            elements[ii].checked = false;
        } else if (elements[ii].type === "checkbox") {
            elements[ii].checked = false;
        }
    }
    var selectTags = document.getElementsByTagName("select");
    for (var i = 0; i < selectTags.length; i++) {
        selectTags[i].selectedIndex = 0;
    }
    jQuery('#submitButton').text("Submit");
}
function getBool(data) {
    if (data === "true") {
        return true;
    }
    else if (data === "false") {
        return false;
    }
}