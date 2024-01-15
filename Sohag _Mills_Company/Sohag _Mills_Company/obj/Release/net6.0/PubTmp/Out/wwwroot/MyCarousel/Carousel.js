const Mycarousel=document.querySelector(".Mycarousel");
const arrowButtons=document.querySelectorAll(".Mywrapper i");
//catch the width of MyCard 
const firstCardWidth=document.querySelector(".MyCard").offsetWidth;
//making array of Mycarousel children which are li elements to Infinite scroll 
const MycarouselChildrens=[...Mycarousel.children]
let IsDraging=false, StartX,StartScrollLeft;

//making array of Mycarousel children which are li elements to Infinite scroll 
//get the number of MyCards that can fit in Mycarousel at once
// here i devide the Mycarousel.offsetWidth / firstCardWidth : 3296px / 260px
//is the correct code don't change it .
let MyCardPreviw=Math.round(Mycarousel.offsetWidth / (firstCardWidth))

//the next code we add the last eight - about (MyCardPreviw)the result above- MyCards at the beginning by reverse function,
//and the the first four to end
//insert copies of the last few MyCards to beginning of Mycarousel for infinite scroll
MycarouselChildrens.slice(-MyCardPreviw).reverse().forEach(MyCard =>{
    Mycarousel.insertAdjacentHTML("afterbegin",MyCard.outerHTML)

})

//insert copies of the first few MyCards to end of Mycarousel for infinite scroll
MycarouselChildrens.slice(0,MyCardPreviw).forEach(MyCard =>{
    Mycarousel.insertAdjacentHTML("beforeend",MyCard.outerHTML)

})



//buttons icon i (arrowButtons) movement
//Start script
arrowButtons.forEach(btn => {
    btn.addEventListener("click",()=>{
        //print the id of icon i
        // console.log(btn.id)
        // summary condition
        // Mycarousel.scrollLeft += btn.id === "left" ? - firstCardWidth : firstCardWidth;
        //normal condtion
        if(btn.id === "left"){
            //on click the scrpll one move equall the width of MyCard 
            //mean move about one MyCard width every click
            Mycarousel.scrollLeft += - firstCardWidth
        }else{
            Mycarousel.scrollLeft += firstCardWidth
            // console.log(firstCardWidth) // 260px
            
        }
    })
    
});
//End script



//Dragging mouse movement
//Start script
const StartDraging = (e) =>{
    //    console.log(e.pageX)
          IsDraging=true;
          Mycarousel.classList.add("dragging")
          //records the intial cursor and scroll position of the Mycarousel
          StartX=e.pageX;
          StartScrollLeft=Mycarousel.scrollLeft
    }

const Draging = (e) =>{
//    console.log(e.pageX)
      if(!IsDraging) return;
      //update the scroll positionof the Mycarousel based on the cursor movement
      Mycarousel.scrollLeft=StartScrollLeft - (e.pageX - StartX);
}

const DragingStop = () =>{
    IsDraging=false;
    Mycarousel.classList.remove("dragging")
}
//End script

//Start Infinit Scroll 
//when you move between the pictures the scroll not end , countinous with you.
function IfinitScroll () {
    // using Math.ceil() because the scroll left return float number
    if( Mycarousel.scrollLeft === 0 ){
        //must to remove scroll:smooth to work InfinitScroll
        Mycarousel.classList.add("no-transition")
        // console.log("You have reached to the left end")
        //if the Mycarousel at the beginning at left end point , scroll to end  
        Mycarousel.scrollLeft= Mycarousel.scrollWidth - ( 2 * Mycarousel.offsetWidth)
        Mycarousel.classList.remove("no-transition")
        //console.log(Mycarousel.scrollWidth) // equall to 3296 the value of all MyCards that were put on more than one screen
        // don't forget the gird columns the 3296 equals three times to 1100 because i split the every screen into 4  columns
        //console.log(Mycarousel.offsetWidth) // equall to 1100 that is set into css file or i don't 
    }else if ( Math.ceil(Mycarousel.scrollLeft) === Mycarousel.scrollWidth - Mycarousel.offsetWidth ){
        //must to remove scroll:smooth to work InfinitScroll
        Mycarousel.classList.add("no-transition")
        // console.log("You have reached to the right end")
        //if the Mycarousel at the end at right end point , scroll to beginning
        // as you make overflow :none or auto  of your Mycarousel  but the all MyCard don't open the screen () 
        //because you make in css file overflow-x:hidden 
        Mycarousel.scrollLeft=Mycarousel.offsetWidth
        Mycarousel.classList.remove("no-transition")
    }
          
}



//Start Infinit Scroll 

Mycarousel.addEventListener("mousedown",StartDraging);
Mycarousel.addEventListener("mousemove",Draging);
document.addEventListener("mouseup",DragingStop);
document.addEventListener("mouseup",IfinitScroll);