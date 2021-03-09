$(document).ready(function () {
    $("#btn_Send").click(function () {
        debugger;
        var options = {};
        options.url = "https://192.168.1.16:8080/api/Shift/add";
        options.type = "POST";
        var obj = {};
        obj.DoctorId = $("#select_DoctorId").val();
        obj.RoomId = $("#select_RoomId").val();
        obj.Date = $("#select_Date").val();
        obj.TimeId = $("#select_TimeId").val();

        options.data = JSON.stringify(obj);
        options.contentType = "application/json";
        options.dataType = "html";

        options.success = function (msg) {
            alert("Hello! I am an alert box!");
        };
        options.error = function () {
            alert("SAI");
        };
        $.ajax(options);

    });
})