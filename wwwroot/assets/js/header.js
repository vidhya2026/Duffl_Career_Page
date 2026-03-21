
var btn = document.getElementById('btn')
var menu = document.getElementById('menu');
var menushow = document.getElementById('menu-show ');
var menuclose = document.getElementById('close-menu');
var  menu_set = document.getElementById('menu-set');

//Toggle
btn.addEventListener('click', function (e) {
menu.classList.toggle('open');
//  setTimeout(toggleMenu, 650);
$('#menu-set').fadeIn(2800);
$('body').css('overflow','hidden');
//  menushow.classList.toggle('open')
}, false)


//Preview

menuclose.addEventListener('click',function (e){
menu.classList.toggle('open')
// setTimeout(toggleMenu, 0);
$('#menu-set').fadeOut(10);
$('body').css('overflow','auto');
// menushow.classList.toggle('open')
},false)
function toggleMenu() {
menu_set.classList.toggle("show");
}




var wobbleElements = document.querySelectorAll('.wobble');

wobbleElements.forEach(function(el){
el.addEventListener('mouseover', function(){

if(!el.classList.contains('animating') && !el.classList.contains('mouseover')){

    el.classList.add('animating','mouseover');

    var letters = el.innerText.split('');
    
    setTimeout(function(){ el.classList.remove('animating'); }, (letters.length + 1) * 50);
    
    var animationName = el.dataset.animation;
    if(!animationName){ animationName = "jump"; }

    el.innerText = '';

    letters.forEach(function(letter){
        if(letter == " "){
            letter = "&nbsp;";
        }
        el.innerHTML += '<span class="letter">'+letter+'</span>';
    });
    
    var letterElements = el.querySelectorAll('.letter');
    letterElements.forEach(function(letter, i){
        setTimeout(function(){
            letter.classList.add(animationName);
        }, 50 * i);
    });
    
}

});

el.addEventListener('mouseout', function(){				
el.classList.remove('mouseover');
});
});











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

