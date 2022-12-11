
function OnPlusMinusClick() {

    $('#PlusMinus').unbind().click(function () {
        if ($('#PlusMinus').hasClass('bx bxs-plus-circle bx-tada') == true) {

            $(this).closest('tr').after("<tr><td></td><td colspan = '999'>" + $('#hiddenTable').html() + "</td></tr>");

            $('#PlusMinus').removeClass('bx bxs-plus-circle bx-tada');
            $('#PlusMinus').addClass('bx bxs-minus-circle bx-tada');

        } else if ($('#PlusMinus').hasClass('bx bxs-minus-circle bx-tada') == true) {

            $('#PlusMinus').removeClass('bx bxs-minus-circle bx-tada');
            $('#PlusMinus').addClass('bx bxs-plus-circle bx-tada');
            $(this).closest('tr').next().remove();

        }
    })

}

