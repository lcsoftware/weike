// JavaScript Document

$(function(){
	//头部导航鼠标经过状态
	$('.nav_box li').hover(function(){
		if($(this).hasClass('active')){
			$(this).removeClass('hover');	
		}else{
			$(this).addClass('hover').siblings().removeClass('hover');	
		}
	},function(){
		$(this).removeClass('hover');	
	})
	//个人信息展开收起
	$('.user_box').hover(function(){
		$(this).find('span').addClass('zhankai');
		$(this).find('.user_info').stop(true).slideDown();	
	},function(){
		$(this).find('span').removeClass('zhankai');
		$(this).find('.user_info').stop(true).slideUp();	
	})
	
	$('.course_table tr').hover(function(){
		$(this).addClass('active').siblings().removeClass('active')	;
	},function(){
		$(this).removeClass('active')	;	
	})
	//关闭提示文本框
	$('.tip_text span').click(function(){
		$(this).parent().hide();	
	})
	
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
	$('.j_brief').click(function(){
		tanchu($('.pop_400').eq(0));
	})
	$('.j_chapter').click(function(){
		tanchu($('.pop_400').eq(1));
	})
	
})


