function buttonAdd_onClick()
{
    var newExpense = '<div class="expense">' +
                        '<div class="form-group">' +

                            '<label class="col-md-2 control-label">Expense Name</label>' +
                            '<div class="col-md-4">' +
                                '<input name="name[]" type="text" placeholder="Write your expenses name.." class="form-control input-md">' +
                            '</div>' +

                            '<label class="col-md-2 control-label">Date</label>' +
                            '<div class="col-md-4">' +
                               '<input type="date" name="date[]" class="form-control input-md">' +
                        '</div>' +

                       '</div>' +
                       '<div class="form-group">' +
                            '<label class="col-md-2 control-label">Description</label>' +
                            '<div class="col-md-4">' +
                                '<textarea class="form-control" name="description[]"></textarea>' +
                            '</div>' +

                            '<label class="col-md-2 control-label">Cost</label>' +
                            '<div class="col-md-4">' +
                                '<div class="input-group">' +
                                    '<input name="cost[]" type="text" placeholder="20.00" class="form-control input-md cost"><span class="input-group-addon">TL</span>' +
                                '</div>' +
                            '</div>' +
                        '</div>';
    $("#expenses").append(newExpense);

    // Add an event handler for the new cost field.
    $(".cost").change(updateCost);
}

function getTotalCost()
{
    var totalCost = 0;
    $(".cost").each(function () {
        $(this).val().replace(",", ".");
        totalCost += (Number)($(this).val());
    });
    return totalCost;
}
function updateCost() {
    $("#totalCost").val(getTotalCost());
}
$(document).ready(function () {
    $("#new-expense-form").trigger("reset");
    $("#buttonAdd").click(buttonAdd_onClick);
    $(".cost").change(updateCost);
});