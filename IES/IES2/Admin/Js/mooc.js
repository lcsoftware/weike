// JavaScript Document

$(function(){
	
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
		$('.pop_bg,.pop_600,.pop_400,.pop_800,.pop_box').hide();	
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
	$('.j_data').click(function(){
		tanchu($('.pop_600').eq(0));	
	})
	$('.j_know').click(function(){
		tanchu($('.pop_600').eq(1));	
	})
	$('.j_discuss').click(function(){
		tanchu($('.pop_600').eq(2));	
	})
	$('.js_data').click(function(){
		tanchu($('.pop_600').eq(3));	
	})
	$('.light_blue').click(function(){
		tanchu($('.pop_400'));	
	})
	$('.light_green').click(function(){
		tanchu($('.pop_600'));
	})
	$('.create_course a').click(function(){
		tanchu($('.pop_box'));	
	})
	$('.add_student').live('click',function(){
		tanchu($('.pop_600'));
	})
	
	$('.icon_zhankai').click(function(){
		$(this).parent().toggle();
		$(this).parent().siblings().toggle();	
	})
	
	$('.exercise_list li').hover(function(){
		$(this).addClass('active').siblings().removeClass('active');
	},function(){
		$(this).removeClass('active');	
	})	

	$('.data_table').each(function(){
		$(this).find('tr:odd').css('background','#f2f2f2');	
	})
	/*$('.data_table tr').hover(function(){
		$(this).addClass('active').siblings().removeClass('active');	
	},function(){
		$(this).removeClass('active');	
	})*/
	
	$('.curri_table').each(function(){
		$(this).find('tr:even').css('background','#f2f2f2');	
	})
	$('.curri_table tr').hover(function(){
		$(this).find('.edit_btn').toggle();	
	})
	
	$('.way1').click(function(){
		$('.recruit_list').eq(0).show();
		$('.recruit_list').eq(1).hide();		
	})
	$('.way2').click(function(){
		$('.recruit_list').eq(1).show();
		$('.recruit_list').eq(0).hide();		
	})
	//教学计划
	$('.group_discuss a').live('click',function(){
		if(!$(this).hasClass('click')){
			$(this).addClass('click');
			$(this).parents('.chapt_box').next().slideDown();
			$(this).text('[收起]');
		}else{
			$(this).removeClass('click');
			$(this).parents('.chapt_box').next().slideUp();
			$(this).text('[展开]');
		}	
	})
	
	//预览
	$('.note_list li').hover(function(){
		$(this).addClass('active').siblings().removeClass('active');	
	},function(){
		$(this).removeClass('active');	
	})
	
	$('.item_list li').live('click',function(){
		var num = $(this).index();
		$(this).addClass('active').siblings().removeClass('active');
		$(this).parent().siblings('.detail_info').eq(num).show().siblings('.detail_info').hide();	
	})
	
	$('.fold_chapter').live('click',function(){
		if(!$(this).hasClass('click')){
			var len = $(this).parents('.rate_box').next().length;
			if(len>0){
				$(this).addClass('click');
				$(this).text('-');
				$(this).parents('.rate_box').next().slideDown();	
			}
		}else{
			$(this).removeClass('click');
			$(this).text('+');
			$(this).parents('.rate_box').next().slideUp();
		}	
	})
	
	var screenWidth = $(window).width();
	$('.video_tap').attr("width",(screenWidth-340));
	
})


