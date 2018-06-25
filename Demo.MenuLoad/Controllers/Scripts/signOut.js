function GetSignInType() {
    var type = localStorage.getItem("signInType");
    return type;
}

function SignOut() {
    var signType = GetSignInType();
    console.log("Sign Out Clicked");
    console.log(signType);
    if (signType == 'app') {
        signOutApp();
    } else if (signType == 'gmail') {
        signOutgGmail();
    } else if (signType == 'facebook') {
        signOutFacebook();
    } else {
        //error
    }

}

function signOutApp() {
    window.location.href = window.GetLoginUrl();
}
function signOutFacebook() {
    Logout();
    window.location.href = window.GetLoginUrl();
}
function signOutgGmail() {
    handleClientLoadOut();
    
    window.location.href = window.GetLoginUrl();
}