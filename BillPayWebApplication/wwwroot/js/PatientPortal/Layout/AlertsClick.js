
function OnAlertsClick() {

    $("#Alerts").click(function () {
        $.ajax({
            type: "Get",
            url: "/PatientPortal/Alerts",
            InsertionMode = InsertionMode.Replace
        })
    })

}