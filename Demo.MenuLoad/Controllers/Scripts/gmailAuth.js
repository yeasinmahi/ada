
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
    $('.loader').show();
    $('#myContainer').fadeOut();
    console.log("signin google called");
    var user = GoogleAuth.currentUser.get();
    console.log("GoogleUser: ");
    console.log(user);
    var isAuthorized = user.hasGrantedScopes(SCOPE);
    console.log("isAuthorized: " + isAuthorized);
    if (isAuthorized) {
        checkUserExistsByEmai(user["w3"]["U3"], user["Zi"]["access_token"]);
    } else {
        $('.loader').hide();
        $('#myContainer').fadeIn();
    }
}

function startApp() {
    gapi.load('auth2', function () {
        gapi.client.load('plus', 'v1').then(function () {
            gapi.signin2.render('signin-button', {
                scope: 'profile',
                fetch_basic_profile: false
            });
            gapi.auth2.init({
                fetch_basic_profile: false,
                client_id: '527506972274-etnt0ndgdelr2t372eofmoqhvum12abn.apps.googleusercontent.com',
                scope: 'profile'
            }).then(function () {
                console.log('init');
                auth2 = gapi.auth2.getAuthInstance();
                auth2.isSignedIn.listen(function () {
                    console.log(auth2.currentUser.get());
                });
                auth2.then(function (resp) {
                    console.log(auth2.currentUser.get());
                });
            });
        });
    });
}

function checkUserExistsByEmai(gmail, accessToken) {
    var gmailkUserName = gmail.split('@')[0];

    console.log("username is " + gmailkUserName);
    console.log("url is " + gmailAuth);

    jQuery.ajax
    (
        {
            type: "POST",
            url: gmailAuth, // + gmailkUserName,

            contentType: 'application/json; charset = utf-8',
            dataType: 'json',

            data:
                JSON.stringify({
                    UserName: gmailkUserName
                }),

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

