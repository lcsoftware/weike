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
	$('.urge_btn').click(function(){
		tanchu($('.pop_600').eq(0));	
	})
	$('.attendence p span').click(function(){
		tanchu($('.pop_600').eq(1));	
	})
	$('.import_score').click(function(){
		tanchu($('.pop_800').eq(0));	
	})
	$('.export_score').click(function(){
		tanchu($('.pop_800').eq(1));	
	})
	$('.score_rule').click(function(){
		tanchu($('.pop_800').eq(2));	
	})
	$('.learning_descrip').click(function(){
		tanchu($('.pop_400'));	
	})
	$('.select_leader').click(function(){
		tanchu($('.pop_800').eq(0));	
	})
	$('.select_teacher').click(function(){
		tanchu($('.pop_800').eq(1));	
	})
	
	
	
	//小组学习进度条
	$('.progress_bar li').hover(function(){
		$(this).find('.group_detail').toggle();	
	})
	//课程展开收起
	$('.unfold').live('click',function(){
		if(!$(this).hasClass('click')){
			$(this).addClass('click');
			$(this).text('收起 ∧');
			$(this).prev().children('.course_detail').slideDown();	
		}else{
			$(this).removeClass('click');
			$(this).text('展开  ∨');
			$(this).prev().children('.course_detail').slideUp();	
		}
		
	})
	//导入出勤名单
	$('.import_box').hover(function(){
		$(this).children('a.download_name').css('display','block');		
	},function(){
		$(this).children('a.download_name').css('display','none');		
	})
	//查看出勤详细
	$('.class_box tr').click(function(){
		if(!$(this).hasClass('click')){
			$(this).addClass('click');
			$(this).find('i').css('display','block');	
		}else{
			$(this).removeClass('click');
			$(this).find('i').css('display','none');
		}	
	})
	//表格隔行变色&鼠标放上去背景颜色变化
	$('.class_box').each(function(){
		$(this).find('tr:even').css('background','#f2f2f2');	
	})
	$('.class_box tr').hover(function(){
		$(this).addClass('active').siblings().removeClass('active');
		$(this).find('.operation_btn').show();	
	},function(){
		$(this).removeClass('active');
		$(this).find('.operation_btn').hide();		
	})
	
	$('.course_item li').click(function(){
		var num = $(this).index();
		$(this).addClass('active').siblings().removeClass('active');
		$(this).parent().siblings('.item_detail').eq(num).show().siblings('.item_detail').hide();	
	})
	
	$('.fold_btn').live('click',function(){
		if(!$(this).hasClass('click')){
			$(this).addClass('click');
			$(this).parent().next().show();
			$(this).text('[收起]');
		}else{
			$(this).removeClass('click');
			$(this).parent().next().hide();
			$(this).text('[展开]');
		}
	})
	
	$('.leader_list li').hover(function(){
		$(this).addClass('active').siblings().removeClass('active');	
	},function(){
		$(this).removeClass('active');	
	})
	
	//设置组长
	$('.member_info li').hover(function(){
		$(this).find('.leader_set').toggle();	
	})
	//设置指导教师
	$('.member_detail p span').dblclick(function(){
		$(this).parent().next().show();	
	})
	//点击空白关闭
	$(document).bind("click",function(){
		$(".teacher_box").hide();
	}) 
	$('.teacher_box').click(function(e){
		e.stopPropagation();
	});
	//选择讨论主题
	$('.zu_detail a').live('click',function(){
		tanchu($('.pop_600'));	
	})
	//线下课堂设置
	$('.course_arrange li').live('click',function(){
		tanchu($('.pop_600'));	
	})
	$('.mark_detail,.edit_score').hover(function(){
		$(this).find('i').toggle();
	})
	
	$('.comment_table').each(function(){
		$(this).find('tr:odd').css('background','#f2f2f2');	
	})
	$('.comment_list li:even').css('background','#f2f2f2');
	
	//翻转课堂预览导航
	$('.nav_item li').live('click',function(){
		$(this).addClass('click').siblings().removeClass('click');	
	})
	$('.second_reverse li').live('click',function(){
		$(this).addClass('active').siblings().removeClass('active');	
	})
	
	$('.reverse_table tr:odd').css('background','#f2f2f2');
	
	$('.offline_table tr:even').find('td').css('background','#f2f2f2');
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
})