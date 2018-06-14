
var GoogleAuth;
var SCOPE = 'https://www.googleapis.com/auth/plus.profile.emails.read';

function googlesignin()
{
    console.log("button clicked!");

    handleClientLoad();
    handleAuthClick();
}



function handleClientLoad()
{
    // Load the API's client and auth2 modules.
    // Call the initClient function after the modules load.
    gapi.load('client:auth2', initClient);
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

        // Listen for sign-in state changes.
        GoogleAuth.isSignedIn.listen(updateSigninStatus);

        // Handle initial sign-in state. (Determine if user is already signed in.)
        var user = GoogleAuth.currentUser.get();

        setSigninStatus();

        // Call handleAuthClick function when user clicks on
        //      "Sign In/Authorize" button.
        $('#sign-in-or-out-button').click(function () {

        });
        /*
        $('#revoke-access-button').click(function() {
        revokeAccess();
    });
        */
    });
}

function handleAuthClick()
{
    if (GoogleAuth.isSignedIn.get()) {
        // User is authorized and has clicked 'Sign out' button.
        GoogleAuth.signOut();
        revokeAccess();
    } else {
        // User is not signed in. Start Google auth flow.
        GoogleAuth.signIn();
    }
}

function revokeAccess()
{
    GoogleAuth.disconnect();
}

function setSigninStatus(isSignedIn)
{
    var user = GoogleAuth.currentUser.get();
    var isAuthorized = user.hasGrantedScopes(SCOPE);
    if (isAuthorized) {
        $('#sign-in-or-out-button').html('Sign out');
        $('#revoke-access-button').css('display', 'inline-block');
        $('#auth-status').html('You are currently signed in and have granted ' +
            'access to this app.');
        console.log(user);
        console.log(user["w3"]["U3"]);
        checkUserExistsByEmai(user["w3"]["U3"], user["Zi"]["access_token"]);
    } else {
        $('#sign-in-or-out-button').html('Sign In/Authorize');
        $('#revoke-access-button').css('display', 'none');
        $('#auth-status').html('You have not authorized this app or you are ' +
            'signed out.');
    }
}

function updateSigninStatus(isSignedIn)
{
    setSigninStatus();
}

function checkUserExistsByEmai(gmail, accessToken) {
    var gmailkUserName = gmail.split('@')[0];

    console.log("username is " + gmailkUserName);


    jQuery.ajax
    (
        {
            type: "GET",
                url: "https://localhost:44386/api/users/gmail/" + gmailkUserName,

            contentType: 'application/json; charset = utf-8',
            dataType: 'json',

            success: function (data) {

                if (data != null) {
                    extarnalGmailSignIn(data, accessToken);
                } else {

                }
            },

            failure: function () {
                console.log("User Sign In Failed!");
            }
        }
    );
}
function extarnalGmailSignIn(username, accessToken) {
    var identityConnecTokenUrl = "https://localhost:44347/api/token/external";

    console.log("External Sign Called");
    jQuery.ajax({
        type: "POST",
        url: identityConnecTokenUrl,

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
            if (data == "invalid_grant") {
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

