﻿$(function () {

    $(".Details").click('off');
    $(".Detailsarea").click(function () {
        debugger;
        let id = $(this).data("id");
        $.ajax({
            url: '/Sector/DetailsArea/' + id,
            type: 'GET',
            success: function (data) {
                console.log(data);
               // $("#Detailpop").empty();


                let html = `
                                            <div class="modal fade" id="myModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true" style="direction:rtl">
                                                <div class="modal-dialog">
                                                    <div class="modal-content">
                                                        <div class="modal-header">

                                                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                                        </div>
                                                        <div class="modal-body">

                                                                                    المنطقة<p>${data.member.Area_Name}</p>
                                                                                        العنوان<p>${data.member.Address_Name}</p>


                                                        </div>
                                                        <div class="modal-footer">
                                                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                                                            <button type="button" class="btn btn-primary">Save changes</button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>`;
                $("#Detailpop").append(html);
                $("#myModal").modal('show'); // Show the modal after appending the content
            },
            error: function () {
                console.log('Error loading data.');
            }
        });
    });


    $(".deletearea").click(function () {
        debugger;

        // Use SweetAlert2 for confirmation
        Swal.fire({
            title: 'هل تريد حقا الحذف؟؟',
            text: '',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'نعم إحذف!',
            cancelButtonText: 'إلغاء الحذف',
            reverseButtons: true
        }).then((result) => {
            if (result.isConfirmed) {
                // User clicked the "Yes, delete it!" button

                let id = $(this).data("id");
                $.ajax({
                    url: '/Sector/DeleteArea/' + id,
                    type: 'GET',
                    success: function (data) {

                        // Handle success
                        Swal.fire({
                            title: 'تم الحذف بنجاح',
                            icon: 'sucess',
                            showCancelButton: true,
                            cancelButtonText: 'رجوع',
                        })
                        location.reload(true);
                    },
                    error: function () {
                        console.log('Error loading data.');
                    }

                });
            } else {
                // User clicked the "No, cancel!" button or closed the dialog
                console.log('Deletion canceled.');
            }
        });
    });

});

// Show the modal