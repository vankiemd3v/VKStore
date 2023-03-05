var SiteController = function () {
    this.initialize = function () {
        registerEvent()
        loadCart();
    }

    function loadCart() {
        $.ajax({
            type: "GET",
            url: '/Cart/GetCartItem',
            success: function (res) {
                $('#numberCartItem').text(res.length);
            }
        });
    }
    function registerEvent() {
        $('body').on('click', '#addToCart', function (e) {
            e.preventDefault();
            const id = $(this).data('id');
            $.ajax({
                type: "POST",
                url: '/Cart/AddToCart',
                data: {
                    id: id,
                },
                success: function (res) {
                    $('#numberCartItem').text(res.length);
                    window.location.href = "/Cart/Index"
                },
                error: function (err) {
                    console.log(err);
                }
            });
        });
        
    }
}


function numberWithCommas(x) {
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}