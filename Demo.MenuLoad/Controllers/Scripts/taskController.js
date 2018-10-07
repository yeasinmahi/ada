$(document).ready(function () {
    getTasks();
});
function getTasks() {
    console.log("getTasks called");
    $.ajax({
        type: "GET",
        url: taskUrl + "/getbyenroll",
        contentType: 'application/json; charset = utf-8',
        dataType: 'json',
        data: { enroll: '369116' },
        success: function (data) {
            console.log("Tasks " + data);
            var lists = "";
            for (var i = 0; i < data.length; i++) {
                lists += populateTask(data[i]);
            }
            $(".todo-list").html(lists);
        },

        failure: function () {
            console.log("Tasks Get Failed!");
        }
    });
}

function populateTask(task) {
    console.log("task: " + task.keyPoint);
    var li = 
        
        '<li class=' + (task.status === "Complete"? "done":"")+'>' +
        '<span class="handle ui-sortable-handle">' +
        '<i class="fa fa-ellipsis-v"></i>' +
        '<i class="fa fa-ellipsis-v"></i>' +
        '</span>' +
        '<input type="checkbox" value="" id="toDoCheck">' +
        '<input type="hidden" value="'+task.id+'">' +
        '<span class="text">' + task.keyPoint + '</span>' +
        '<small class="label label-danger"><i class="fa fa-clock-o"></i> 2 hour</small>' +
        '<div class="tools">' +
        '<i class="fa fa-edit"></i>' +
        '<i class="fa fa-trash-o"></i>' +
        '</div>' +
        '</li>';
    return li;
}