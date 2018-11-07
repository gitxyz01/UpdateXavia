

var NewCategoryController = {
    init: function () {
        NewCategoryController.registerEvent();


    },
    notify: function (from, align, icon, type, animIn, animOut) {
        $.growl({
            icon: icon,
            title: ' Xóa ',
            message: 'danh mục thành công',
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
            .addClass('btn btn-warning fa fa-exclamation-triangle')
                           .text('Bạn Muốn Xóa Tin Tức Này?');
    },




    registerEvent: function () {
        $(document).off('submit').on('submit', '#frm-Delete', function (e) {
            e.preventDefault();
            $.ajax({
                type: 'POST',
                url: '/Admin/CMSNews/Delete',
                data: $(this).serialize(),
                success: function (response) {
                    $('#modalDelete').modal('hide');
                    window.location.replace("/Admin/CMSNews/Index1");
                    NewCategoryController.notify("top", "right", '', "success", "", "");

                },
                error: function (result) {
                    $('#resultDelete').addClass('btn-danger fa fa-exclamation-triangle')
                        .text('xóa tin tức thất bại');
                }
            });
        });


        $('.btn-Delete').off('click').on('click', function () {
            $('#modalDelete').modal('show');
            console.log($(this).data('id'));
            var id = $(this).data('id');
            $('#hidenId').val(id);
            NewCategoryController.resetFormDelete();
        });
    }
}

NewCategoryController.init();

