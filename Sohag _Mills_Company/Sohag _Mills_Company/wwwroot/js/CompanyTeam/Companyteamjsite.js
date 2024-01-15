

$(function () {
    $(".Details").click(function () {
        debugger;
        let id = $(this).data("id");
        $.ajax({
            url: '/CompanyTeam/Details/' + id,
            type: 'GET',
            success: function (data) {
                console.log(data.member);
                $("#Detailpop").empty();
                var baseUrl = '../Images/'
                let imageurl;
                if (data.member.imageName == null || data.member.imageName === undefined || data.member.imageName=='') {
                    imageurl = baseUrl+'c6892fc9-d14b-4a49-b110-d3b067b2b8db_IMG-20210315-WA0001.jpg';
                } else {
                    imageurl = baseUrl + data.member.imageName;
                }
             
                let html = `
                            <div class="modal fade" id="myModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true" style="direction:rtl">
                                <div class="modal-dialog">
                                    <div class="modal-content">
                                        <div class="modal-header ">
                                            <h5 class="modal-title" id="exampleModalLabel">${data.member.name}</h5>
                                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                        </div>
                                        <div class="modal-body">
                                                    <img src="${imageurl}" alt="Member Image" class="img-fluid mb-3" />
                                                <p>${data.member.postion}</p>
                                                    <p>${data.member.nationaity}</p>
                                                        <p>${data.member.nationaity}</p>
                                                        <p>${data.member.representation_Body}</p>

                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                                            <a  class="btn btn-primary " href="Companyteam/CreateTeam/${id}">تعديل</a>
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


    $(".Deletemember").click(function () {
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
                    url: '/CompanyTeam/Delete/' + id,
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

});

// Show the modal
var myModal = new bootstrap.Modal(document.getElementById('myModal'));
myModal.show();

