function GetExpenses() {
    jQuery.support.cors = true;
    $.ajax({
        url: 'http://localhost:56826/api/ExpenseApi',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            UpdatePlot(data);
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}

function UpdatePlot(data) {
    var crmTotal = 0;
    var expenseTotal = 0;

    data.forEach(function (element , index , array) {
        if(element.Source == "Crm")
        {
            crmTotal = crmTotal + element.Cost;
        }
        else if (element.Source == "ExpenseApp")
        {
            expenseTotal = expenseTotal + element.Cost;
        }

    });

    $("#chart").chart({
        data: new Array(crmTotal, expenseTotal),
        labels: new Array("Crm", "ExpenseApp"),
        width: 400
    });
}

setInterval(GetExpenses, 2000);