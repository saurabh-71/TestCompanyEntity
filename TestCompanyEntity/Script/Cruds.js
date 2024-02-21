$(document).ready(function () {
    var selectState = $("#State").val();
    $("#State").empty();
    $("#State").append("<option value=''>-Select Option-</option>");

    var CountryID = $("#Country").val();
    if (CountryID != '' || CountryID != 0) {
        $.get("/cruds/StatesBind", { CountryID: CountryID }, function (data) {
            if (data.length > 0) {
                $("#State").empty();
                $("#State").append("<option value=''>-Select Option-</option>");
                for (var i = 0; i < data.length; i++) {
                    var item = data[i];
                    if (selectState == item.id) {
                        $("#State").append("<option selected value='" + item.id + "'>" + item.name + "</option>");
                    } else {
                        $("#State").append("<option value='" + item.id + "'>" + item.name + "</option>");
                    }
                }

            } else {
                $("#State").empty();
                $("#State").append("<option value=''>-Select Option-</option>");
            }
        });
    }
    

    $("#Country").change(function () {
        var CountryID = $(this).val();
        $.get("/cruds/StatesBind", { CountryID: CountryID }, function (data) {
            if (data.length > 0) {
                $("#State").empty();
                $("#State").append("<option value=''>-Select Option-</option>");
                for (var i = 0; i < data.length; i++) {
                    var item = data[i];
                    $("#State").append("<option value='" + item.id + "'>" + item.name + "</option>");
                }

            } else {
                $("#State").empty();
                $("#State").append("<option value=''>-Select Option-</option>");
            }
        });
    });
});
