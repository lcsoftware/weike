// JavaScript Document
$(function(){
	//关闭弹出层
	$('.icon_close').click(function(){
		$('.pop_bg,.pop_600,.pop_400,.pop_800').hide();	
	})
	
	//弹出层方法
	function tanchu(popbox){
		var oHeight = $(document).height();
		var oScroll = $(window).scrollTop();
		$('.pop_bg').show().css('height',oHeight);
		popbox.show().css('top',oScroll+200);
	}
	
	$('.create_zone').live('click',function(){
		tanchu($('.pop_800'));	
	})
	$('.move_btn').click(function(){
		tanchu($('.pop_400').eq(1));	
	})
	
	$('.icon_cancel').click(function(){
		$(this).parent().slideUp();	
	})
	
	$('.go_input').click(function(){
		$('.section_list').toggle();	
	})
	
	
	
	//话题分享
	$('.share_btn').live('click',function(){
		if(!$(this).hasClass('click')){
			$(this).addClass('click');
			$(this).parent().next().slideDown();
		}else{
			$(this).removeClass('click');
			$(this).parent().next().slideUp();
		}
			
	})
	
	$('.post_select').hover(function(){
		$(this).find('.post_list').toggle();	
	})

	
	//投票
	var s_index = 0;
	$('.vote_list li').click(function(){
		var num = $(this).index();
		$(this).addClass('active').siblings().removeClass('active');
		$(this).parent().next().children('.vote_content').eq(num).show().siblings().hide();	
		s_index = num;
	})
	$('.icon_left').click(function(){
		s_index--;
		if(s_index<0){
			s_index = $('.vote_content').length-1;
		}
		$('.vote_list li').eq(s_index).addClass('active').siblings().removeClass('active');
		$('.vote_content').eq(s_index).show().siblings().hide();	
	})
	$('.icon_right').click(function(){
		s_index++;
		if(s_index>=$('.vote_content').length){
			s_index = 0;
		}
		$('.vote_list li').eq(s_index).addClass('active').siblings().removeClass('active');
		$('.vote_content').eq(s_index).show().siblings().hide();	
	})

})