

var VerifyController = {
    init: function () {
        VerifyController.registerEvent();
        VerifyController.datatable();

    },
    datatable: function () {
        var table = $("#table").DataTable({

            "searching": true,
            "ordering": true,
            dom: "Blfrtip",
            buttons: [
            {
                extend: "csv",
                className: "btn-sm",
                text: "Excel"
            },
            {
                extend: "pdf",
                className: "btn-sm"
            },
            ],
            "pagingType": "full_numbers",
            destroy: true,

            responsive: true,
            columnDefs: [

                { responsivePriority: 2, targets: -1 },
                 { responsivePriority: 3, targets: -2 }
            ],

            "language": {
                "lengthMenu": "Hiển Thị _MENU_ Bản Ghi Trên Trang",
                "search": "Tìm Kiếm:",
                "info": "Hiển Thị _START_ Tới _END_ Của _TOTAL_ Bản Ghi",
                "paginate": {
                    "first": "Đầu",
                    "last": "Cuối",
                    "next": "Tới",
                    "previous": "Lui"
                },
                "zeroRecords": "Không Có Bản Ghi Nào Được Tìm Thấy",
                "infoEmpty": "Không Có Bản Ghi Nào Hiển Thị",
                "infoFiltered": "(Trong Tổng Cộng _MAX_ Bản Ghi)"
            }
        });

    },
    verify: function (id, status) {
        $.ajax({
            type: 'POST',
            url: '/Admin/Verify/VerifyCategory',
            data: {
                id: id,
                status: status
            },
            success: function (response) {
                $('#modalDelete').modal('hide');
                window.location.replace("/Admin/Verify/Categories");
                VerifyController.notify("top", "right", '', "success", "", "");

            },
            error: function (result) {
                $('#resultDelete').addClass('btn-danger fa fa-exclamation-triangle')
                    .text('Duyệt Danh Mục Thất Bại');
            }
        });
    },
    notify: function (from, align, icon, type, animIn, animOut) {
        $.growl({
            icon: icon,
            title: ' Duyệt ',
            message: 'Danh Mục Thành Công',
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


    resetFormCreate: function () {
        $('#resultCreate').removeClass()
            .addClass('btn btn-grd-success')
                           .text('Bạn Muốn Thêm Mới Danh Mục Này?');
    },
    resetFormDelete: function () {
        $('#resultDelete').removeClass()
            .addClass('btn btn-grd-danger')
                           .text('Bạn Muốn Xóa Danh Mục Này?');
    },




    registerEvent: function () {

        $(document).on('click', '#AcceptCreate', function (e) {
            e.preventDefault();
            VerifyController.verify($('.IdCreate').val(), 1);
        });

        $(document).on('click', '#CancelCreate', function (e) {
            e.preventDefault();
            VerifyController.verify($('.IdCreate').val(), 2);
        });
        $(document).on('click', '#AcceptDelete', function (e) {
            e.preventDefault();
            VerifyController.verify($('.IdDelete').val(), 2);
        });
        $(document).on('click', '#CancelDelete', function (e) {
            e.preventDefault();
            VerifyController.verify($('.IdDelete').val(), 1);
        });
        $('.showCreate').off('click').on('click', function () {
            $('#modalCreate').modal('show');
            var id = $(this).data('id');
            console.log(id);
            $('.IdCreate').val(id);
            VerifyController.resetFormCreate();
        });
        $('.showDelete').off('click').on('click', function () {
            $('#modalDelete').modal('show');
            var id = $(this).data('id');
            console.log(id);
            $('.IdDelete').val(id);
            VerifyController.resetFormDelete();
        });
    }
};

VerifyController.init();

