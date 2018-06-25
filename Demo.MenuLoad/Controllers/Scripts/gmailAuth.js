
var GoogleAuth;
var SCOPE = 'https://www.googleapis.com/auth/plus.profile.emails.read';

function googlesignin()
{
    console.log("button clicked!");
    GoogleAuth.signIn();
    GoogleAuth.isSignedIn.listen(signinGoogle);
    signinGoogle();
}
function initClient()
{
    // Retrieve the discovery document for version 3 of Google Drive API.
    // In practice, your app can retrieve one or more discovery documents.
    var discoveryUrl = 'https://www.googleapis.com/discovery/v1/apis/plus/v1/rest';

    // Initialize the gapi.client object, which app uses to make API requests.
    // Get API key and client ID from API Console.
    // 'scope' field specifies space-delimited list of access scopes.
    gapi.client.init({
        'apiKey': 'AIzaSyA-GvpC1MUDnXfTrLyK7PF1jrCEbOYWSII',
        'discoveryDocs': [discoveryUrl],
        'clientId': '527506972274-etnt0ndgdelr2t372eofmoqhvum12abn.apps.googleusercontent.com',
        'scope': SCOPE
    }).then(function () {
        GoogleAuth = gapi.auth2.getAuthInstance();
    });
}

function signoutGoogle() {
    GoogleAuth.signOut();
    GoogleAuth.disconnect();
}

function signinGoogle()
{
    console.log("signin google called");
    var user = GoogleAuth.currentUser.get();
    var isAuthorized = user.hasGrantedScopes(SCOPE);
    if (isAuthorized) {
        checkUserExistsByEmai(user["w3"]["U3"], user["Zi"]["access_token"]);
    } else {
        //User not Exist
    }
}


function checkUserExistsByEmai(gmail, accessToken) {
    var gmailkUserName = gmail.split('@')[0];

    console.log("username is " + gmailkUserName);


    jQuery.ajax
    (
        {
            type: "GET",
                url: gmailAuth + gmailkUserName,

            contentType: 'application/json; charset = utf-8',
            dataType: 'json',

            success: function (data) {

                if (data !== null) {
                    getAccessTokenGmail(data);
                } else {
                    //ToDo
                }
            },

            failure: function () {
                console.log("User Sign In Failed!");
            }
        }
    );
}
function getAccessTokenGmail(username) {
    jQuery.ajax
    (
        {
            type: "GET",
            url: externaltoken,
            data: {
                'UserName': username
            },
            contentType: 'application/json; charset = utf-8',
            dataType: 'json',

            success: function (data) {
                console.log("test " + data);
                if (data !== null) {
                    extarnalGmailSignIn(username, data);
                } else {
                    //todo
                }
            },

            failure: function () {
                console.log("User Sign In Failed!");
            }
        }
    );
}
function extarnalGmailSignIn(username, accessToken) {

    console.log("External Sign Called");
    jQuery.ajax({
        type: "POST",
        url: externalAuthSignIn,

        data: JSON.stringify({
            // user signin parameter starts
            'UserName': username,
            'AccessToken': accessToken
            // user signin parameter ends
        }),

        contentType: 'application/json; charset = utf-8',
        dataType: 'json',

        success: function (data) {
            console.log(data);
            if (data === "invalid_grant") {
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

                toastr.error('Sorry, Facebook UserNot Authorized', 'SignIn Notification');

                return;
            }
            localStorage.setItem("signInType", "gmail");
            //window.location = "Home/Index?token=" + accessToken;
            window.location.href = window.GetDashboardUrl(accessToken);
        },

        failure: function () {
            console.log("User Sign In Failed!");
        }
    });
}

