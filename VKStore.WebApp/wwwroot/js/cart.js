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
        $('body').on('click', '#btnOrder', function (e) {
            e.preventDefault();
            $('#btnOrder').attr('disabled', true);
            const shipName = $('#shipName').val();
            const shipPhoneNumber = $('#shipPhoneNumber').val();
            const shipAddress = $('#shipAddress').val();
            const shipEmail = $('#shipEmail').val();
            if (shipName == "") {
                $('#btnOrder').attr('disabled', false);
                $('#shipNameValid').text('Vui lòng nhập họ và tên');

            } else if (shipAddress == "") {
                $('#btnOrder').attr('disabled', false);
                $('#shipNameValid').text('');
                $('#shipAddressValid').text('Vui lòng nhập địa chỉ nhận hàng');
            }
            else if (shipEmail == "") {
                $('#btnOrder').attr('disabled', false);
                $('#shipNameValid').text('');
                $('#shipAddressValid').text('');
                $('#shipEmailValid').text('Vui lòng nhập Email');
            }
            
            else if (shipPhoneNumber == "") {
                $('#btnOrder').attr('disabled', false);
                $('#shipNameValid').text('');
                $('#shipAddressValid').text('');
                $('#shipEmailValid').text('');
                $('#shipPhoneNumberValid').text('Vui lòng nhập số điện thoại');
            }
            else {
                $('#shipNameValid').text('');
                $('#shipAddressValid').text('');
                $('#shipEmailValid').text('');
                $('#shipPhoneNumberValid').text('');
                $.ajax({
                    type: "POST",
                    url: '/Cart/Index',
                    data: {
                        shipName: shipName,
                        shipPhoneNumber: shipPhoneNumber,
                        shipAddress: shipAddress,
                        shipEmail: shipEmail
                    },
                    success: function (res) {
                        if (res.status == true) {
                            window.location.href = "/Cart/OrderSuccess"
                        }
                        if (res.cart == false) {
                            $('#btnOrder').attr('disabled', false);
                            $('#shipPhoneNumberValid').text('Chưa có sản phẩm trong giỏ hàng');
                        }
                    },
                    error: function (err) {
                        console.log(err);
                    }
                });
            }
            
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