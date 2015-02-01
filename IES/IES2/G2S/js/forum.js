// JavaScript Document
$(function(){
	//显示隐藏左侧模块
	$('.icon_fold').live('click',function(){
		if(!$(this).hasClass('click')){
			$(this).addClass('click');
			$('.side_left').hide();
			$(this).css('margin-left','-530px');
			$('.main_all').css('width','1000px');
		}else{
			$(this).removeClass('click');
			$('.side_left').show();
			$(this).css('margin-left','-430px');
			$('.main_all').css('width','1200px');
		}
	})
	 
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
	////个人信息展开收起
	//$('.user_box').hover(function(){
	//	$(this).find('span').addClass('zhankai');
	//	$(this).find('.user_info').stop(true).slideDown();	
	//},function(){
	//	$(this).find('span').removeClass('zhankai');
	//	$(this).find('.user_info').stop(true).slideUp();	
	//})
	
	//$('.prensent').mouseover(function(){
	//	$(this).children('.class_theme').toggle();	
	//})
	////左侧导航展开收起
	//$('.side_box').delegate('.more_tool','click',function(){
	//	var len = $('.side_nav li').length;
	//	var oHeight = $('.side_nav li').height();
	//	if(!$(this).hasClass('click')){
	//		$(this).addClass('click');
	//		$(this).html('收起工具<i class="icon icon_less"></i>');
	//		$(this).prev('.side_nav').animate({"height":len*oHeight});
	//	}else{
	//		$(this).removeClass('click');
	//		$(this).html('更多工具<i class="icon icon_more"></i>');
	//		$(this).prev('.side_nav').animate({"height":200});	
	//	}
	//});
	
	
	//公告
	$(function(){
		var s_index = 0;
		var num = s_index;	
		function autoImg(){
			s_index++;
			if(s_index>=$('.notice_list li').length){
				s_index = 0;	
			}
			$('.notice_list li').eq(s_index).show().siblings().hide();
			num = s_index;
		}
		var timer = setInterval(autoImg,3000);
		$('.notice_box').hover(function(){
			clearInterval(timer);	
		},function(){
			timer = setInterval(autoImg,3000);
		})
		$('.next_btn').click(function(){
			clearInterval(timer);
			autoImg();
			num = s_index;	
		});
		$('.prev_btn').click(function(){
			clearInterval(timer);
			s_index--;
			if(s_index<0){
				s_index = $('.notice_list li').length-1;	
			}
			$('.notice_list li').eq(s_index).show().siblings().hide();	
			num = s_index;
		})	
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
	
	//$('.create_zone').live('click',function(){
	//	tanchu($('.pop_800'));	
	//})
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