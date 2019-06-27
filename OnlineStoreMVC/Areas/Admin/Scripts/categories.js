

var CategoryController = {
    init: function () {

        CategoryController.loadData();

        CategoryController.registerEvent();


    },
    notify: function (message,from, align, type) {
        $.growl({            
            message: message,
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

    loadData: function () {
        $.ajax({
            url: 'LoadData',
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                var html = '';
                var data = response.data;
                var template = $('#data-template-Active').html();
                var template_Lock = $('#data-template-Locked').html();
                var template_waitingDelete = $('#data-template-waitingDelete').html();
                var table = $("#table").DataTable({
                    dom: "Blfrtip",
                    "searching": true,
                    "ordering": true,
                    "order": [[3, "desc"]],
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
                    "fnDrawCallback": function (oSettings) {
                        CategoryController.registerEvent();
                    }

                });

                table.clear();
                $.each(data, function (i, item) {

                    if (item.Status == 'Đang hoạt động') {
                        html = Mustache.render(template, {
                            id: item.Id,
                            SortOrder: item.SortOrder,
                            Description: item.Description,
                            CreateBy: item.CreatedBy,
                            CategoryName: item.Name,
                            CreatedDate: item.CreatedDate,
                            CategoryId: item.Id
                        });

                    }
                    else {
                        if (item.Status == "Duyệt xóa") {
                            html = Mustache.render(template_waitingDelete, {
                                id: item.Id,
                                SortOrder: item.SortOrder,
                                Description: item.Description,
                                CreateBy: item.CreatedBy,
                                CategoryName: item.Name,
                                CreatedDate: item.CreatedDate,
                                CategoryId: item.Id
                            });
                        }
                        else {
                            html = Mustache.render(template_Lock, {
                                id: item.Id,
                                SortOrder: item.SortOrder,
                                Description: item.Description,
                                CreateBy: item.CreatedBy,
                                CategoryName: item.Name,
                                CreatedDate: item.CreatedDate,
                                CategoryId: item.Id
                            });
                        }
                    }
                    
                   
                    table.row.add($(html)).draw();

                });
                CategoryController.registerEvent();
            }
        });
    },

    resetFormDelete: function () {
        $('#submitDelete').show();
        $('#resultDelete').removeClass()
            .addClass('label label-danger')
                           .text('Bạn Muốn Xóa Danh Mục Sản Phẩm Này?');
    },

    resetForm: function () {
        $('#modalCrUdTitle').removeClass().addClass('label label-primary').text('Thêm Mới Danh Mục Sản Phẩm');
        $('#Id').val('0');
        $('.txtName').val('');
        $('#ckstatus').prop('checked', true);
        $('.txtSortOrder').val('');
        $('.txtDescription').val('');
        $('.parentId').val('');
        $('#btnSubmit').text('Thêm Mới');
        $('#dismisModal').show();
        $('#btn-crOrUd').show();
    },

    loadDetail: function (id) {

        $.ajax({
            url: 'LoadDetail',
            data: {
                id: id
            },
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    var data = response.data;
                    $('#modalCrUdTitle').text('Cập Nhật Danh Mục Sản Phẩm').removeClass().addClass('label label-primary');
                    $('#Id').val(data.Id);
                    $('.txtName').val(data.Name);
                    $('.txtSortOrder').val(data.SortOrder);
                    $('.txtDescription').val(data.Description);
                    $('.parentId').val(parseInt(data.ParentId));
                    $('#btnSubmit').text('Cập Nhật');
                    data.Status === "Đang hoạt động" ? $('.statusDrd').val(1) : $('.statusDrd').val(0);
                    $('#dismisModal').show();
                    $('#btn-crOrUd').show();
                }
                else {
                    alert(response.message);
                }
            }
        });
    },
    registerEvent: function () {
        $(document).off('submit').on('submit', '#frm-DeleteProvider', function (e) {
            e.preventDefault();
            $.ajax({
                type: 'POST',
                url: 'DeleteCategory',
                data: $(this).serialize(),
                success: function (response) {

                    if (response.status == true) {
                        $('#modalDelete').modal('hide');
                        CategoryController.notify(response.message, "top", "right", "success");
                        CategoryController.loadData();
                    }
                    else {
                        $('#resultDelete').addClass('btn-danger fa fa-exclamation-triangle')
                            .text(response.message);

                    }
                },

            });
        });


        $(document).on('submit', '.frm-CreateProvider', function (e) {
            e.preventDefault();
            $.ajax({
                type: 'POST',
                url: 'SaveCategory',
                data: $(this).serialize(),
                success: function (response) {
                    if (response.status == false) {
                        $('#modalCrUdTitle').removeClass()
                            .addClass('btn-danger fa fa-exclamation-triangle')
                            .html(response.message);
                    }
                    else {
                        $('#modalAddUpdate').modal('hide');
                        CategoryController.notify(response.message, "top", "right", "success");
                        CategoryController.loadData();
                    }
                }
            });

        });
        $('.btn-AddCategory').off('click').on('click', function () {
            $('#modalAddUpdate').modal('show');
            CategoryController.resetForm();
        });

        $('.btn-Edit').off('click').on('click', function () {
            $('#modalAddUpdate').modal('show');  
            var id = $(this).data('id');
            console.log(id);
            CategoryController.loadDetail(id);
        });

        $('.btn-Delete').off('click').on('click', function () {
            $('#modalDelete').modal('show')
            var id = $(this).data('id');
            $('#hid-ProviderIdDelete').val(id);
            CategoryController.resetFormDelete();
        });
    }
}

CategoryController.init();

