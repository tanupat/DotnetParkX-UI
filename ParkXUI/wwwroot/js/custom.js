
$(window).on("load scroll",function(e){   
    var scroll = $(window).scrollTop();

    if (scroll >= 300) {
        $("body").addClass("scrolling has-scroll"); 
    } else {
        $("body").removeClass("scrolling");
    }
});

 
 
$(document).ready(function(){
   $('.navbar-toggle').click(function () {
      $("html").toggleClass("nav-opened delay");
      setTimeout($.proxy(function(){
          $('html').removeClass("delay");
      },this),700);
  });
 
  $(".nav-menu li a").click(function () {
      $("html").toggleClass("nav-opened");
      if($("html").hasClass("delay")){
        setTimeout($.proxy(function(){
          $('html').removeClass("delay");
        },this),700);
      }else{
        setTimeout($.proxy(function(){
          $('html').addClass("delay");
        },this),700);
      }
  });

  /*-----------------------[Start] Back to top-----------------------*/
  $('<div class="item-totop totop"><span></span></div>').appendTo('.page'); 
  $('<span class="arrow"></span>').appendTo('.nav li > a[data-toggle="dropdown"]'); 
 
  $('.totop').click(function () {
      $("html, body").animate({
          scrollTop: 0
      }, 600);
      return false;
  });

  setTimeout(function () {
      $(".cookie-content").addClass("cookie-show");
  }, 700);

  $(".cookie-content .btn-accept, .cookie-content .btn-close").click(function() {
      $(".cookie-content").addClass("cookie-hide");
  });
   
  /*------------[Start] change color of SVG image using CSS ------------*/

  $('img.svg-js').each(function() {
      var $img = jQuery(this);
      var imgURL = $img.attr('src');
      var attributes = $img.prop("attributes");

      $.get(imgURL, function(data) {
          // Get the SVG tag, ignore the rest
          var $svg = jQuery(data).find('svg');

          // Remove any invalid XML tags
          $svg = $svg.removeAttr('xmlns:a');

          // Loop through IMG attributes and apply on SVG
          $.each(attributes, function() {
              $svg.attr(this.name, this.value);
          });

          // Replace IMG with SVG
          $img.replaceWith($svg);
      }, 'xml');
  });

});

  
$(window).on("load", function() {
   
  $("html").addClass("page-loaded");

  $(".preload").fadeOut();
 
  var isMobile = {
      Android: function() {
          return navigator.userAgent.match(/Android/i);
      },
      BlackBerry: function() {
          return navigator.userAgent.match(/BlackBerry/i);
      },
      iOS: function() {
          return navigator.userAgent.match(/iPhone|iPad|iPod/i);
      },
      Opera: function() {
          return navigator.userAgent.match(/Opera Mini/i);
      },
      Windows: function() {
          return navigator.userAgent.match(/IEMobile/i);
      },
      any: function() {
          return (isMobile.Android() || isMobile.BlackBerry() || isMobile.iOS() || isMobile.Opera() || isMobile.Windows());
      }
  };

  if(isMobile.any()) {
      $("html").addClass("device");
  }else{
      $("html").addClass("pc");    
  }

  /*------------[Start] skrollr.min.js  ------------*/

  skrollr.init({
      smoothScrolling: true,
      smoothScrollingDuration: 100,
      mobileDeceleration: 0,
      forceHeight: false,
      mobileCheck: function() {
          return false;
      }
  });
  
  /*------------[Start] Background Parallax  ------------*/

  (function($) { 
    var parallax = -0.2;

    var $bg_images = $(".parallax");
    var offset_tops = [];
    $bg_images.each(function(i, el) {
      offset_tops.push($(el).offset().top);
    });

    $(window).on("load scroll",function(e){
      var dy = $(this).scrollTop();
      $bg_images.each(function(i, el) {
        var ot = offset_tops[i];
        $(el).css("background-position", "50% " + (dy - ot) * parallax + "px");
      });
    });
  })(jQuery);
});

