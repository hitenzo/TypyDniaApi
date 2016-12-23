function getDay() {
    
    var month = parseInt($("#monthId").val());
    var day = parseInt($("#dayId").val());
    var year = parseInt($("#yearId").val());
    var valid;
    var msg = "";
    try {
        Date.validateDay(day, year, month - 1);
        Date.validateMonth(month - 1);
        Date.validateYear(year);
        valid = true;
    } catch(err) {
        valid = false;
        msg = err;
    }

    var date = "" + year + "-" + month + "-" + day;

    if (valid) {
        $('#mainHeader').text("Sending request...  /../api/TypyDnia/GetDay");

        var url = "/../api/TypyDnia/GetDay";
        $.ajax({
            type: "POST",
            url: url,
            dataType: "json",
            data: JSON.stringify(date),
            contentType: "application/json",
            error: function(xhr, status, error) {
                var alertMsg = "Error: " + error;
                $('#mainHeader').text(xhr.responseText);
            },
            success: function (response, status, request) {
                $('#mainHeader').text("Success");

                var dataStr = "data:text/json;charset=utf-8," + encodeURIComponent(response);
                var dlAnchorElem = document.getElementById("downloadAnchorElem");
                dlAnchorElem.setAttribute("href", dataStr);
                dlAnchorElem.setAttribute("download", "response.txt");
                dlAnchorElem.click();
            }
        });
    } else {
        alert(msg);
    }
}

function getMatchDetail() {
    $('#mainHeader').text("Sending request...  /../api/WhoScored/GetMatchDetails");

    var data = {
        HomeTeamId: 75,
        Date: "30-10-16" //dd:mm:yy
    }

    var url = "/../api/WhoScored/GetMatchDetails";
    $.ajax({
        type: "POST",
        url: url,
        data: data,
        error: function (xhr, status, error) {
            var alertMsg = "Error: " + error;
            $('#mainHeader').text(xhr.responseText);
        },
        success: function (response, status, request) {
            $('#mainHeader').text("Success");

            var dataStr = "data:text/json;charset=utf-8," + encodeURIComponent(response);
            var dlAnchorElem = document.getElementById("downloadAnchorElem");
            dlAnchorElem.setAttribute("href", dataStr);
            dlAnchorElem.setAttribute("download", "match_detail.txt");
            dlAnchorElem.click();
        }
    });
}

function getSeasonMatches() {
    $('#mainHeader').text("Sending request...  /../api/WhoScored/GetSeasonMatches");

    var data = {
        Years: "2014/2015",
        League: "Serie A"
    }

    var url = "/../api/WhoScored/GetSeasonMatches";
    $.ajax({
        type: "POST",
        url: url,
        data: data,
        error: function (xhr, status, error) {
            var alertMsg = "Error: " + error;
            $('#mainHeader').text(xhr.responseText);
        },
        success: function (response, status, request) {
            $('#mainHeader').text("Success");

            var dataStr = "data:text/json;charset=utf-8," + encodeURIComponent(response);
            var dlAnchorElem = document.getElementById("downloadAnchorElem");
            dlAnchorElem.setAttribute("href", dataStr);
            dlAnchorElem.setAttribute("download", "match_detail.txt");
            dlAnchorElem.click();
        }
    });
}

function initializeVal() {
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth()+1; //January is 0!
    var yyyy = today.getFullYear();

    $("#monthId").val(mm);
    $("#dayId").val(dd);
    $("#yearId").val(yyyy);
}
