$(document).ready(function() {
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
            console.log("Menu " + data);
            //$('#mealSpan').html = data.menuList;
            $("#mealSpan").html(data.menuList);
            $("#altMealSpan").html(data.altMenuList);
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