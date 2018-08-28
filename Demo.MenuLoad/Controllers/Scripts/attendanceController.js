$(document).ready(function () {
    //console.log("Name is " + fullName);

    //$("#attendanceName").text(window.FullName);
    getOwnMonthlyAttendance();
});
function getOwnMonthlyAttendance() {
    console.log("Attendance Called");
    $.ajax({
        type: "GET",
        url: attendanceUrl + "/getmonthlyattendance",
        contentType: 'application/json; charset = utf-8',
        dataType: 'json',
        data: { enroll: "369116" },
        success: function (data) {
            console.log("Attendance " + data);
            var trs = "";
            for (var i = 0; i < data.length; i++) {
                trs += getAttendanceRow(data[i]);

            }
            $("#attendanceBody").html(trs);
        },

        failure: function () {
            console.log("Users Get Failed!");
        }
    });
}
function getAttendanceRow(attendance) {
    var td =
        '<tr>' +
        '<td class = "hidden">' + attendance.id + '</td>' +
        '<td>' + convertDatetoString(new Date(attendance.attendanceDate)) + '</td>' +
        '<td>' + attendance.attendanceTime + '</td>' +
        '<td>' + (attendance.remark == "AI" ? "In Punch" : attendance.remark == "AO" ? "Out Punch" : "Unknown") + '</td>' +
        '</tr>';

    return td;
}