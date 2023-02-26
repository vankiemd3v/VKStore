$('#btnUpdateStatus').off('click').on('click', function () {
    const id = $(this).data('id');
    const status = $('#selected').val();
    $.ajax({
        type: "POST",
        url: '/Order/Detail',
        data: {
            id: id,
            status: status,
        },
        success: function (res) {
            window.location.href = '/Order/Index'
        },
        error: function (err) {
            console.log(err);
        }
    });
});