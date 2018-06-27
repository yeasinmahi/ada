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