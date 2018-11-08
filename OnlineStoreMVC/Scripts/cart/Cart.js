

var homeController = {
    init: function () {

        homeController.registerEvent();
        homeController.loadData();

    },
    registerEvent: function () {
        $(document).on('submit', '.frmUpdateCart', function (e) {
            e.preventDefault();
            $.ajax({
                type: 'POST',
                url: 'Update',
                data: $(this).serialize(),
                success: function (data) {
                    homeController.loadData();

                }
            });
        });


        $(document).on('submit', '#deletedProduct', function (e) {
            e.preventDefault();
            $.ajax({
                type: 'POST',
                url: 'Delete',
                data: $(this).serialize(),
                success: function (data) {
                    homeController.loadData();

                }
            });
        });
    },
    loadData: function () {
        $.ajax({
            url: 'AjaxCartIndex',
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                var html = '';
                var data = response.data;
                var template = $('#data-template').html();
                var totalQuantity = 0;
                var totalPrice = 0;
                var html2 = '';
                var template1 = $('#data-template1').html();
                var template2 = $('#last-row').html();
                $.each(data, function (i, item) {
                    totalQuantity += item.Quantity;
                    var Price = parseInt(item.Quantity) * parseInt(item.Price);
                    html += Mustache.render(template, {
                        Id: item.Id,
                        Name: item.Name,
                        Quantity: item.Quantity,
                        Price: item.Price,
                        Image: item.Image,
                        TotalProductPrice: item.TotalProductPrice,
                        TotalPrice: item.TotalPrice
                    })

                    html2 = Mustache.render(template2, {
                        TotalQuantity: totalQuantity,
                        TotalPrice: item.TotalPrice,
                    })
                });
                $('#table-data').html(html);
                $('#table-data').append(html2);
            }
        });
    }
}
homeController.init();