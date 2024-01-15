
$(function () {
    debugger

    getAllData('CompanyTeam/GetAllMemberTeams', (error, data) => {
        debugger
        if (error) {
            console.error('Error:', error);
        } else {
            //console.log('Data:', data);

            // Check if data.res is defined and not null
            if (data.members) {
                let html = ``;
               
                $(".teams").empty();
                let obj = data.members.find(a => a.postionId == 1);
                if (obj != null || obj != undefined) {
                    html += `
                    <div class="d-flex justify-content-center align-items-center text-center">
                        <div>
                            <img src="/Images/${obj.imageName}" width="180" height="100">
                            <p>${obj.nickname}</p>
                            <p>${obj.postion}</p>
                            <h5>${obj.name}</h5>
                        </div>
                    </div>
                `;
                }
                obj = data.members.find(a => a.postionId == 2);
                //console.log(obj)
                if (obj != null || obj != undefined) {
                    html += `
                    <div class="d-flex justify-content-center align-items-center text-center">
                        <div>
                            <img src="/Images/${obj.imageName}" width="180" height="100">
                            <p>${obj.nickname}</p>
                            <p>${obj.postion}</p>
                            <h5>${obj.name}</h5>
                        </div>
                    </div>
                `;
                }
                obj = data.members.find(a => a.postionId == 3);
                //console.log(obj)
                if (obj != null || obj != undefined) {
                    html += `
                    <div class="d-flex justify-content-center align-items-center text-center">
                        <div>
                            <img src="/Images/${obj.imageName}" width="180" height="100">
                            <p>${obj.nickname}</p>
                            <p>${obj.postion}</p>
                            <h5>${obj.name}</h5>
                        </div>
                    </div>
                `;
                }
                $(".teams").append(html); // Use .append() instead of .html() to add multiple elements

            } else {
                console.warn('Data.res is undefined or null.');
            }
        }
    });



    ///////////////////////Ajax Code

    //$.ajax({
    //    url: '/CompanyTeam/GetAllMemberTeams/',
    //    type: 'GET',
    //    success: function (data) {
    //        let html
    //        // $("#allteam").empty();
    //        /*console.log(data.members[0]);*/
    //        for (let i = 0; i < data.members.length; i++) {
    //            let imageurl;

    //            if (data.members[i].imageName == null) {
    //                imageurl = 'Images/c6892fc9-d14b-4a49-b110-d3b067b2b8db_IMG-20210315-WA0001.jpg';
    //            } else {
    //                imageurl = data.members && data.members[i] ? `Images/${data.members[i].imageName}` : '';
    //            }

    //            html += `




    //            <div class="testimonial-item text-center" style-"overflow-y:auto" >
    //                <div class="testimonial-img position-relative" >
    //                    <img class="img-fluid rounded-circle mx-auto mb-5" src="${imageurl}">
    //                    <div class="btn-square bg-primary rounded-circle">
    //                        <i class="fa fa-quote-left text-white"></i>
    //                    </div>
    //                </div>
    //                <div class="testimonial-text text-center rounded p-4">
    //                    <p>${data.members[i].nickname}</p>
    //                    <h5 class="mb-1">${data.members[i].name}</h5>
    //                    <span class="fst-italic">${data.members[i].postion}</span>  <!-- Corrected typo here -->
    //                </div>
    //            </div >






    //            `;
                
                
    //            /*$(".owl-item").html(html);*/
    //        }
    //        $(".allteam").html(html);
    //        //allteam  owl-item
    //        //$(".allteam").html(html);

    //    },
    //    error: function () {
    //        console.log('Error loading data.');
    //    }
    //});


    

});
        /*       error: function () {
                   console.log('Error loading data.');
               }*/
    





//<div class="testimonial-item text-center" style-"overflow-y:auto" >
//                    <div class="testimonial-img position-relative" >
//                        <img class="img-fluid rounded-circle mx-auto mb-5" src="${imageurl}">
//                        <div class="btn-square bg-primary rounded-circle">
//                            <i class="fa fa-quote-left text-white"></i>
//                        </div>
//                    </div>
//                    <div class="testimonial-text text-center rounded p-4">
//                        <p>${data.members[i].nickname}</p>
//                        <h5 class="mb-1">${data.members[i].name}</h5>
//                        <span class="fst-italic">${data.members[i].postion}</span>  <!-- Corrected typo here -->
//                    </div>
//                </div >




//<li class="MyCard">
//    <div class="img"><img src="${imageurl}" alt="Carousel" draggable="false"></div>
//    <h2>${data.members[i].nickname}</h2>
//    <span>${data.members[i].name}</span>
//</li>