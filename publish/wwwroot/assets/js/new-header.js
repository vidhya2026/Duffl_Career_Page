// MUST REFRESH PAGE ON DIFFERENT VIEWPORT FOR HOVER EFFECTS

// if ($(window).width() > 739) {
//   $(".dropdown-holder").on("mouseenter", function(e) {
//     $(".hover-box", this).slideDown("fast");
//     $(".dropdown-holder").on("mouseleave", function(e) {
//       $(".hover-box", this).slideUp("fast");
//     });
//   });
// }

// document.querySelector(".toggle").addEventListener("click", function() {
//   document.querySelector(".new-header").classList.toggle("container__open");
//   document.querySelector('body').classList.toggle('menu_open')
// });

document.querySelector(".toggle").addEventListener("mouseenter", function () {
  document.querySelector(".new-header").classList.add("container__open");
    
  document.querySelector("body").classList.add("menu_open");
});

// Optional: Close when mouse leaves the toggle or menu area
// document.querySelector(".new-header").addEventListener("mouseleave", function () {
//   document.querySelector(".new-header").classList.remove("container__open");
//   document.querySelector("body").classList.remove("menu_open");
// });


// let menuOpen = false;

// document.querySelector(".toggle").addEventListener("mouseover", function () {
//   if (!menuOpen) {
//     document.querySelector(".new-header").classList.add("container__open");
//     document.querySelector("body").classList.add("menu_open");
//     menuOpen = true;
//   } else {
//     document.querySelector(".new-header").classList.remove("container__open");
//     document.querySelector("body").classList.remove("menu_open");
//     menuOpen = false;
//   }
// });


// <!-- form    -->


$('input').focus(function(){
  $(this).parents('.form-group').addClass('focused');
  });
  
  $('input').blur(function(){
  var inputValue = $(this).val();
  if ( inputValue == "" ) {
  $(this).removeClass('filled');
  $(this).parents('.form-group').removeClass('focused');  
  } else {
  $(this).addClass('filled');
  }
  })  
  
   document.querySelector(".menu-close").addEventListener("click", function() {
 
   document.querySelector(".new-header").classList.toggle("container__open");
   document.querySelector('body').classList.toggle('menu_open')
 });
  
  
  // <!-- form   -->
  
  
  
  
  
  
  
  
  var btn = $('.rotate-360-clockwise');
  
  $(window).scroll(function() {
    if ($(window).scrollTop() > 300) {
      btn.addClass('show');
    } else {
      btn.removeClass('show');
    }
  });
  
  btn.on('click', function(e) {
    e.preventDefault();
    $('html, body').animate({scrollTop:0}, '300');
  });
  
  