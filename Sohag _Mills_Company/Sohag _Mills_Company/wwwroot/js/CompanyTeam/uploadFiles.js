
$(function () {
    $(".Detailsfiles").click(function () {
        debugger;
        let id = $(this).data("id");
        $.ajax({
            url: '/UploadFiles/Details/' + id,
            type: 'GET',
            success: function (data) {
                console.log(data.res);
                $("#Detailpop").empty();
                let html = `
                            <div class="modal fade" id="myModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true" style="direction:rtl">
                                <div class="modal-dialog">
                                    <div class="modal-content">
                                        <div class="modal-header ">
                                            <h5 class="modal-title" id="exampleModalLabel">${data.res.title}</h5>
                                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                        </div>
                                        <div class="modal-body">
                                       <div class="container-fluid">
                                            <textarea class="w-100 h-50" readonly>${data.res.description}</textarea>
                                        </div

                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                                            <a  class="btn btn-primary " href="CreateAbout/${id}">تعديل</a>
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


    $(".DeleteFile").click(function () {
        debugger;
        let id = $(this).data("id");
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


                $.ajax({
                    url: '/UploadFile/Delete/' + id,
                    type: 'GET',
                    success: function (data) {

                        // Handle success
                        Swal.fire({
                            title: 'تم الحذف بنجاح',
                            icon: 'sucess',
                            showCancelButton: true,
                            cancelButtonText: 'رجوع',
                        })
                        location.reload();
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


    getAllData('AboutCompany/GetAllAboutcompany', (error, data) => {
        if (error) {
            console.error('Error:', error);
        } else {
            console.log('Data:', data);
            $(".alldata").empty();
            for (let i = 0; i < data.res.length; i++) {
                let html = `
            <h1>${data.res[i].title}</h1>
            <p>${data.res[i].description}</p>

               `
                $(".alldata").html(html);
            }

        }

    });

  

});

// Show the modal
var myModal = new bootstrap.Modal(document.getElementById('myModal'));
myModal.show();

