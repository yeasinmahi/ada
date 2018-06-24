
var clientId = "099153c2625149bc8ecb3e85e03f0022";

jQuery(document).ready(function (e) {
    var identityConnecTokenUrl = "https://agvdi3.akij.net:44321/api/token";


    function signIn(username, password) {
        jQuery.ajax({

            type: "POST",
            url: identityConnecTokenUrl,

            data: JSON.stringify({
                // user signin parameter starts
                'username': username,
                'Password': password
                // user signin parameter ends
            }),

            contentType: 'application/json; charset = utf-8',
            dataType: 'json',

            success: function (data) {
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

                    toastr.error('Sorry, User Name/Password is invalid!', 'SignIn Notification');

                    return;
                }

                var object = JSON.parse(data);
                var token = object.access_token;

                console.log(object);
                console.log(token);

                //window.location = "UsersWithRoles.aspx?token=" + token;
                //window.location = "Home/Index?token=" + token;
                //window.location.href = '@Url.Action("Index", "Home")' + '?token=' + token;
                localStorage.setItem("signInType", "app");
                window.location.href = window.GetDashboardUrl(token);
            },

            failure: function () {
                console.log("User Sign In Failed!");
            }
        }
        );
    }

    jQuery("#password").on('keyup', function (e) {
        if (e.keyCode === 13) {
            var username = $('#userName').val();
            var password = $('#password').val();

            signIn(username, password);
        }
    });




    $('#buttonSignIn').on('click', function (e) {

        var username = $('#userName').val();
        var password = $('#password').val();

        signIn(username, password);



    });

});