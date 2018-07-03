﻿$(document).ready(function() {
    getTodayMeal();
});
function getTodayMeal() {
    var weekday = new Array('Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday');
    var day = weekday[new Date().getDay()];
    console.log("Today = " + day);
    $.ajax({
        type: "GET",
        url: mealUrl + "/mealbyday",
        contentType: 'application/json; charset = utf-8',
        dataType: 'json',
        data: { day},
        success: function (data) {
            
            console.log("Meal object " + JSON.stringify(data));
            data = JSON.stringify(data);
            data = JSON.parse(data);
            console.log("MenuList " + data.menuList);
            //$('#mealSpan').html = data.menuList;
            $("#mealSpan").html(data.menuList);
            //for (var i = 0; i < data.length; i++) {
            //    $('#leaveDropdown')
            //        .append($('<option>',
            //                {
            //                    value: data[i]["id"],
            //                    text: data[i]["name"]
            //                })
            //        );
            //}

            //$('#dropdownLeave').selectpicker('refresh');
        },

        failure: function () {
            console.log("Users Get Failed!");
        }
    });
}