var CartController = function () {
    this.initialize = function () {
        loadData();
        registerEvent();
    }

    function registerEvent() {
        $('body').on('click', '.btn-plus', function (e) {
            e.preventDefault();
            const id = $(this).data('id');
            const quantity = parseInt($('#input_' + id + '').val()) + 1;
            updateCart(id, quantity);
        });
        $('body').on('click', '.btn-minus', function (e) {
            e.preventDefault();
            const id = $(this).data('id');
            const quantity = parseInt($('#input_' + id + '').val()) - 1;
            updateCart(id, quantity);
        });
        $('body').on('click', '.btn-danger', function (e) {
            e.preventDefault();
            const id = $(this).data('id');
            updateCart(id, 0);
        });
        $('#btnOrder').off('click').on('click', function () {
            const name = $('#txt_name').val();
            const phoneNumber = $('#txt_phoneNumber').val();
            const address = $('#txt_address').val();
            const email = $('#txt_email').val();
            const totalPayment = $('#totalPayment').text();
            $.ajax({
                type: "POST",
                url: '/Cart/Order',
                data: {
                    name: name,
                    phoneNumber: phoneNumber,
                    address: address,
                    email: email,
                    totalPayment: totalPayment
                },
                success: function (res) {
                    if (res.status == true) {
                        $('#numberCartItem').text(res.length);
                        loadData();
                        window.location.href = "/Cart/OrderSuccess"
                    }
                },
                error: function (err) {
                    console.log(err);
                }
            });
        });
    }

    function updateCart(id, quantity) {
        $.ajax({
            type: "POST",
            url: '/Cart/UpdateCart',
            data: {
                id: id,
                quantity: quantity
            },
            success: function (res) {
                $('#input_' + id + '').val(quantity);
                $('#numberCartItem').text(res.length);
                loadData();
            },
            error: function (err) {
                console.log(err);
            }
        });
    }

    function loadData() {
        $.ajax({
            type: "GET",
            url: '/Cart/GetCartItem',
            success: function (res) {
                var html = '';
                var totalPayment = 0;
                $.each(res, function (i, item) {
                    var amount = item.price * item.quantity;

                    html += "<tr>"
                        + "<td style=\"float: left;\" class=\"align-middle\"><img src=\"" + $('#urlImage').val() +"/"+ item.image +"\" alt=\"\" style=\"width: 50px;\">"+ item.name + "</td>"
                        + "<td class=\"align-middle\">" + numberWithCommas(item.price) + "</td>"
                            + "<td class=\"align-middle\">"
                            + "<div class=\"input-group quantity mx-auto\" style=\"width: 100px;\">"
                            + "<div class=\"input-group-btn\">"
                            + "<button data-id=\""+item.productId+"\" class=\"btn btn-sm btn-primary btn-minus\">"
                            + "<i class=\"fa fa-minus\"></i>"
                            + "</button>"
                        + "</div>"
                        + "<input id=\"input_" + item.productId + "\" type=\"text\" class=\"form-control form-control-sm input_quantity bg-secondary border-0 text-center\" value=\"" + item.quantity + "\">"
                            + "<div class=\"input-group-btn\">"
                            + "<button data-id=\"" + item.productId +"\" class=\"btn btn-sm btn-primary btn-plus\">"
                            + "<i class=\"fa fa-plus\"></i>"
                            + "</button>"
                            + "</div>"
                            + "</div>"
                        + "</td>"
                        + "<td class=\"align-middle\">" + numberWithCommas(amount) + "</td>"
                            + "<td class=\"align-middle\"><button data-id=\""+item.productId+"\" class=\"btn btn-sm btn-danger\"><i class=\"fa fa-times\"></i></button></td>"
                        + "</tr >"
                    totalPayment += amount;
                });
                $('#cartBody').html(html);
                $('#numberCartItem').text(res.length);
                $('#totalPayment').text(numberWithCommas(totalPayment));
            }
        });
    }
}