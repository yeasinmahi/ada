$(document).ready(function () {
    $('#attendanceViewButton').click(function () {
        window.location.href = "../Attendance/Index?token=" + token;
        
    });
});