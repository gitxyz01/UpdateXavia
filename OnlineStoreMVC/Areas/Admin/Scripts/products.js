'use strict';


$(document).ready(function () {

    /*--------------------------------------
         Notifications & Dialogs
     ---------------------------------------*/
    /*
     * Notifications
     */
    function notify(from, align, icon, type, animIn, animOut) {
        $.growl({
            icon: icon,
            title: ' Xóa ',
            message: 'sản phẩm thành công',
            url: ''
        }, {
            element: 'body',
            type: type,
            allow_dismiss: true,
            placement: {
                from: from,
                align: align
            },
            offset: {
                x: 30,
                y: 30
            },
            spacing: 10,
            z_index: 999999,
            delay: 2500,
            timer: 1000,
            url_target: '_blank',
            mouse_over: false,
            animate: {
                enter: animIn,
                exit: animOut
            },
            icon_type: 'class',
            template: '<div data-growl="container" class="alert" role="alert">' +
            '<button type="button" class="close" data-growl="dismiss">' +
            '<span aria-hidden="true">&times;</span>' +
            '<span class="sr-only">Close</span>' +
            '</button>' +
            '<span data-growl="icon"></span>' +
            '<span data-growl="title"></span>' +
            '<span data-growl="message"></span>' +
            '<a href="#" data-growl="url"></a>' +
            '</div>'
        });
    };

    $('.notifications .btn').on('click', function (e) {
        e.preventDefault();
        var nFrom = $(this).attr('data-from');
        var nAlign = $(this).attr('data-align');
        var nIcons = $(this).attr('data-icon');
        var nType = $(this).attr('data-type');
        var nAnimIn = $(this).attr('data-animation-in');
        var nAnimOut = $(this).attr('data-animation-out');

        notify(nFrom, nAlign, nIcons, nType, nAnimIn, nAnimOut);
    });

});





var productController = {
    init: function () {      
        productController.registerEvent();
    },
    notify: function (from, align, icon, type, animIn, animOut) {
        $.growl({
            icon: icon,
            title: ' Xóa ',
            message: 'sản phẩm thành công',
            url: ''
        }, {
            element: 'body',
            type: type,
            allow_dismiss: true,
            placement: {
                from: from,
                align: align
            },
            offset: {
                x: 30,
                y: 30
            },
            spacing: 10,
            z_index: 999999,
            delay: 2500,
            timer: 1000,
            url_target: '_blank',
            mouse_over: false,
            animate: {
                enter: animIn,
                exit: animOut
            },
            icon_type: 'class',
            template: '<div data-growl="container" class="alert" role="alert">' +
            '<button type="button" class="close" data-growl="dismiss">' +
            '<span aria-hidden="true">&times;</span>' +
            '<span class="sr-only">Close</span>' +
            '</button>' +
            '<span data-growl="icon"></span>' +
            '<span data-growl="title"></span>' +
            '<span data-growl="message"></span>' +
            '<a href="#" data-growl="url"></a>' +
            '</div>'
        });
    },
    resetFormDelete: function () {
        $('#submitDelete').show();
        $('#resultDelete').removeClass()
            .addClass('btn btn-danger icofont icofont-warning')
                           .text('Bạn Muốn Xóa Sản Phẩm Này?');
    },

    registerEvent: function () {
        $(document).off('submit').on('submit', '#frm-Delete', function (e) {
            e.preventDefault();
            $.ajax({
                type: 'POST',
                url: '/Admin/Product/Delete',
                data: $(this).serialize(),
                success: function (response) {                             
                    $('#modalDelete').modal('hide');
                    window.location.replace("/Admin/Product/Index1");
                    productController.notify("top", "right", '', "success", "", "");
                
                },
                error: function (result) {
                    $('#resultDelete').addClass('btn-danger fa fa-exclamation-triangle')
                        .text('xóa sản phẩm thất bại');
                }
            });
        });


        $('.btn-Delete').off('click').on('click', function () {
            $('#modalDelete').modal('show');
            console.log($(this).data('id'));
            var id = $(this).data('id');
            $('#hidenId').val(id);
            productController.resetFormDelete();
        });
    }
}

productController.init();

