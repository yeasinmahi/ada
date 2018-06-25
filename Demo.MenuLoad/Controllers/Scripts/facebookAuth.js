function statusChangeCallback(response) {
    console.log('statusChangeCallback');
    console.log(response);

    // The response object is returned with a status field that lets the
    // app know the current login status of the person.
    // Full docs on the response object can be found in the documentation
    // for FB.getLoginStatus().
    if (response.status === 'connected') {
        // Logged into your app and Facebook.
        console.log("checking access token");
        console.log(response);
        //testAPI();
    } else {
        // The person is not logged into your app or we are unable to tell.
        document.getElementById('status').innerHTML = 'Please log ' +
            'into this app.';
    }
}

// This function is called when someone finishes with the Login
// Button.  See the onlogin handler attached to it in the sample
// code below.
function checkLoginState() {
    FB.getLoginStatus(function (response) {
        statusChangeCallback(response);
    });
}

window.fbAsyncInit = function () {
    FB.init({
        appId: '217685912355968',
        cookie: true,  // enable cookies to allow the server to access 
        // the session
        xfbml: true,  // parse social plugins on this page
        version: 'v2.8' // use graph api version 2.8
    });

    // Now that we've initialized the JavaScript SDK, we call 
    // FB.getLoginStatus().  This function gets the state of the
    // person visiting this page and can return one of three states to
    // the callback you provide.  They can be:
    //
    // 1. Logged into your app ('connected')
    // 2. Logged into Facebook, but not your app ('not_authorized')
    // 3. Not logged into Facebook and can't tell if they are logged into
    //    your app or not.
    //
    // These three cases are handled in the callback function.

    FB.getLoginStatus(function (response) {
        statusChangeCallback(response);
    });

};

// Load the SDK asynchronously
(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "https://connect.facebook.net/en_US/sdk.js";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));

// Here we run a very simple test of the Graph API after login is
// successful.  See statusChangeCallback() for when this call is made.
function testAPI() {
    console.log('Welcome!  Fetching your information.... ');
    FB.api('/me',
        { locale: 'en_US', fields: 'name, email' },
        function(response) {
            console.log(response);
            console.log('Successful login for: ' + response.name);
            console.log(response.email);
            var access_token = FB.getAuthResponse()["accessToken"];
            console.log(access_token);
            checkUserExistsByFacebook(response.email, access_token);
            /*
            document.getElementById('status').innerHTML =
                'Thanks for logging in, ' + response.email + '!';
            */


        });
}


function checkUserExistsByFacebook(facebookMail, accessToken) {
    var facebookUserName = facebookMail.split('@')[0];

    console.log("username is " + facebookUserName);


    jQuery.ajax
    (
        {
            type: "GET",
                url: facebookAuth + facebookUserName,

            contentType: 'application/json; charset = utf-8',
            dataType: 'json',

            success: function(data) {
                console.log("test "+data);
                if (data !== null) {
                    getAccessTokenFacebook(data);
                } else {
                    //todo
                }
            },

            failure: function() {
                console.log("User Sign In Failed!");
            }
        }
    );
}

function getAccessTokenFacebook(username) {
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
                    ExternalSignIn(username, data);
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
function ExternalSignIn(username, accessToken)
{
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

            localStorage.setItem("signInType", "facebook");

            //window.location = "Home/Index?token=" + accessToken;
            console.log("Before Redirect");
            window.location.href = window.GetDashboardUrl(accessToken);
        },

        failure: function() {
            console.log("User Sign In Failed!");
        }
    });
}


// This url explains various features of facebook permissions
// https://developers.facebook.com/docs/graph-api/reference/user/permissions/

function Login() {
    FB.login(function (response) {
        // handle the response
        console.log("Login Success");
        testAPI();
    },
        {
            scope: 'public_profile,email'
        });
}


function Logout() {
    FB.api('/me/permissions', 'DELETE', function (response) {
        if (response.success === true) {
            console.log("Logout success!");
        }
        else {
            console.log("Logout failed!");
        }
    });
}

console.log("ending logout");
