$(function () {
    if ($.cookie('cartid')) {
        var id = $.cookie('cartid')
      
        $.get("/home/cartQuantity", { cartid: id }, function (quantity) {

            $('#cartindex').val(quantity);

        })
    }
})