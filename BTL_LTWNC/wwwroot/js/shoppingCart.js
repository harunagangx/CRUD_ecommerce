

$(document).ready(function () {
    getTotal();
    showCount();
    $("body").on("click", ".btnAddToCart", function (e) {
        e.preventDefault();

        var id = $(this).data('id')

        var quantity = 1

        var tquantity = $('#quantity_value').text();

        if (tquantity != "") {
            quantity = parseInt(tquantity);
        }

        $.ajax({
            url: "/Cart/AddToCart",
            type: "POST",
            data: {
                id: id,
                quantity: quantity,
            },
            success: function () {
                showCount();
                getTotal();
            },

        })

    });

    $("body").on("click", ".btnDeleteFromCart", function (e) {
        e.preventDefault();

        var id = $(this).data('id');

        $.ajax({
            url: "/Cart/Delete",
            type: "POST",
            data: { id: id },
            success: function () {
                $('#trow_' + id).remove();
                getTotal();
                showCount();
            },
            
        });
    });
 
})


function showCount() {
    $.ajax({
        url: "/cart/showcount",
        type: "GET",
        success: function (res) {
            $("#cart_count").html(res.quantity);
        }
    })
}

function getTotal() {
    $.ajax({
        url: "/cart/getTotalCart",
        type: "GET",
        success: function (res) {
            $("#cart_total").html(res.subtotal);
        }
    })
}





