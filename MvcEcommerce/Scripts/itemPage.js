$(function () {
    if ($.cookie('cartid')) {
        var id = $.cookie('cartid')

        $.get("/home/cartQuantity", { cartid: id }, function (quantity) {

            $('#cartindex').val(quantity);

        })
    }

    $('#addtocart').on('click', function () {
        $("#addtocart").button('loading');
        var id = $('#productid').val();
        var qty = $('#quantity').val();

        $.post("/home/cartIndex",{ pid: id, quantity: qty },function (){            
            $("#addtocart").button('reset');
            var a = parseInt($("#cartindex").val(), 10);
            var b = parseInt(qty, 10);
            $('#cartindex').val(a + b);
            $('#quantity').val(1);
        })
    })
  
       
      

    
})