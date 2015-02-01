// JavaScript Document
$(function(){ 

	$(function(){
		showRandomBigImage();
	})
	function showRandomBigImage(){
		var idx = Math.ceil(Math.random()*10);
		$("#lay_background_img img").attr("src","images/back"+idx+".jpg");
	}
	var w,h;
	if(!!(window.attachEvent && !window.opera)) {
		h = document.documentElement.clientHeight;
		w = document.documentElement.clientWidth;
	} else{
		h = window.innerHeight;
		w = window.innerWidth;
	}
	document.getElementById('lay_background').value  ='窗口大小：' + 'width:' + w + '; height:'+h;
	window.onresize = window.onload = function() {
		var bgImg = document.getElementById('lay_background_img').getElementsByTagName('img')[0];
		var winRate = w/h;
		var imgRate = 1920/1200;
		if(winRate > imgRate){
			bgImg.width = (w);
		}else{
			bgImg.height= (h) ;	
		}
	};

	$('.close_btn').live('click',function(){
		$('.pop_bg,.pop_400').hide();	
	})



})