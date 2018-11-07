

var NewCategoryController = {
    init: function () {
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
                 { responsivePriority: 3, targets: -2 },

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
                "infoFiltered": "(Trong Tổng Cộng _MAX_ Bản Ghi)",
            },
        });
        $(document).ready(function () {
            $(".select2insidemodal").select2({
                dropdownParent: $("#modalAddUpdate")
            });
        });
        NewCategoryController.registerEvent();


    },
    notify: function (from, align, icon, type, animIn, animOut) {
        $.growl({
            icon: icon,
            title: ' Xóa ',
            message: 'thương hiệu thành công',
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
                           .text('Bạn Muốn Xóa Thương Hiệu Này?');
    },




    registerEvent: function () {
        $(document).off('submit').on('submit', '#frm-Delete', function (e) {
            e.preventDefault();
            $.ajax({
                type: 'POST',
                url: '/Admin/Brand/Delete',
                data: $(this).serialize(),
                success: function (response) {
                    $('#modalDelete').modal('hide');
                    window.location.replace("/Admin/Brand/Index1");
                    NewCategoryController.notify("top", "right", '', "success", "", "");

                },
                error: function (result) {
                    $('#resultDelete').addClass('btn-danger fa fa-exclamation-triangle')
                        .text('xóa thương hiệu thất bại');
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

