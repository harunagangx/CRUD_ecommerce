

$(document).ready(function () {

    showCountWishListItem();

    $("body").on("click", ".btnAddToWishList", function (e) {
        e.preventDefault();

        var id = $(this).data('id');

        $.ajax({
            url: "/WishList/AddToWishList",
            type: "POST",
            data: {
                id: id,
            },
            success: function () {
                
                showCountWishListItem();
            },

        })
    });

    $("body").on("click", ".btnDeleteFromWishList", function (e) {
        e.preventDefault();

        var id = $(this).data('id');

        $.ajax({
            url: "/WishList/RemoveFormWishList",
            type: "POST",
            data: {
                id: id,
            },
            success: function () {
                $('#trow_' + id).remove();
                showCountWishListItem();
            },

        })
    });
})

function showCountWishListItem() {
    $.ajax({
        url: "/WishList/showCountWishList",
        type: "GET",
        success: function (res) {
            $("#wish_count").html(res.wishListQuantity);
        }
    })
}
