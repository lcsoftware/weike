// JavaScript Document
$(function(){ 
	//弹出层方法
	function tanchu(popbox){
		var oHeight = $(document).height();
		var oScroll = $(window).scrollTop();
		$('.pop_bg').show().css('height',oHeight);
		popbox.show().css('top',oScroll+200);
	}
	//关闭弹出层
	$('.close_pop').live('click',function(){
		$('.pop_bg,.pop_400,.pop_600,.pop_800,.pop_1000').hide();
	})
	$('.icon_close').live('click',function(){
		$('.pop_bg,.pop_400,.pop_600,.pop_800,.pop_1000').hide();
	})
	//去掉主讲人
	$('.close_btn').live('click',function(){
		$(this).parents('.course_set').slideUp();	
	})
	//添加主讲人
	$('.add_teacher').live('click',function(){
		$(this).prev().slideDown();	
	})
	//教学团队成员hover效果
	$('.member li').hover(function(){
		$(this).addClass('active').siblings().removeClass('active');	
	},function(){
		$(this).removeClass('active');
	})
	
	//用户管理-选择站内用户
	$('.select_user').live('click',function(){
		tanchu($('.pop_400').eq(0));	
	})
	
	//用户管理-添加新成员
	$('.add_new').live('click',function(){
		tanchu($('.pop_600'));
	})
	//锁定用户
	$('.team_lock').live('click',function(){
		tanchu($('.pop_400').eq(1));	
	})
	//解锁用户
	$('.team_unlock').live('click',function(){
		tanchu($('.pop_400').eq(2));	
	})
	
	//添加学生弹出框
	$('.add_btn').live('click',function(){
		tanchu($('.pop_600'));		
	})
	
	//审核学生申请
	$('.apply_btn').live('click',function(){
		if(!$(this).hasClass('click')){
			$(this).addClass('click');
			$(this).parent().next().show();
			$(this).text('[收起]');
		}else{
			$(this).removeClass('click');
			$(this).parent().next().hide();
			$(this).text('[申请审核]')
		}	
	})
	//班级图标解释说明
	$('.register_btn span').hover(function(){
		$(this).find('.icon_explain').toggle();	
	})
	//教学班班级table鼠标经过状态
	$('.class_table tr:odd').css('background','#f2f2f2');
	$('.class_table tr').hover(function(){
		$(this).find('.register_btn').toggle();	
	})
	//批量审核
	$('.batch_btn').live('click',function(){
		tanchu($('.pop_400'));	
	})
	//查看详细
	$('.detail_btn').live('click',function(){
		tanchu($('.pop_600'));	
	})
	//添加学生搜索框
	$('.name_box').click(function(){
		$(this).parents('.form_box').siblings('.search_content').show();	
	})
	
	//表格隔行变色&鼠标放上去背景颜色变化
	$('.class_box').each(function(){
		$(this).find('tr:even').css('background','#f2f2f2');	
	})
	$('.class_box tr').hover(function(){
		$(this).addClass('active').siblings().removeClass('active');	
	},function(){
		$(this).removeClass('active');	
	})
	
	//导入班级学生下拉
	$('.import_box').hover(function(){
		$(this).find('.download_box').toggle();
	})
	//添加学生tab切换
	$('.accord_box li').live('click',function(){
		var num =$(this).index();
		$(this).addClass('active').siblings().removeClass('active');
		$(this).parent().siblings('.result_box').eq(num).show().siblings('.result_box').hide();	
	})
	//选择站内用户弹出层table
	$('.search_result tr').hover(function(){
		$(this).addClass('active').siblings().removeClass('active');	
	},function(){
		$(this).removeClass('active');	
	})
	$('.search_result tr:even').css('background','#f2f2f2');


})