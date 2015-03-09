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
		$('.pop_bg,.pop_400,.pop_600,.pop_800').hide();
	})
	
	//我的资源首页上传文件
	$('.topic_import').live('click',function(){
		tanchu($('.pop_600'));	
	})
	//我的资源首页批量共享
	$('.batch_share').live('click',function(){
		tanchu($('.pop_400').eq(0));	
	})
	//我的资源首页批量移动
	$('.batch_move').live('click',function(){
		tanchu($('.pop_400').eq(1));	
	})
	//我的资源首页批量删除
	$('.batch_delete').live('click',function(){
		tanchu($('.pop_400').eq(2));	
	})
	//试卷首页-删除弹框提示
	$('.delete_topic').click(function(){
		tanchu($('.pop_400'));
	})
	//试卷名称重命名表现形式
	$('.data_tit').live('dblclick',function(){
		$(this).hide();
		$(this).next().show().select();	
	})
	//答题卡tab切换
	$('.tab_list li').click(function(){
		$(this).addClass('active').siblings().removeClass('active');	
	})
	
	//新建文件夹
	$('.add_topic').live('click',function(){
		var content ="<tr><td><div class=\"data_name\"><input type=\"checkbox\"><i class=\"icon_24 file\"></i><input class=\"name_file\" type=\"text\"></div></td><td>文件夹</td><td>256M</td><td>2014-10-23</td></tr>"
		$('.course_data').append(content);	
	})	
	
	//关联知识点
	$('.connect_knowledge').click(function(){
		tanchu($('.pop_600'));
	})
	
	//资料属性
	$('.attribute').live('click',function(){
		tanchu($('.pop_400'))	
	})
	//习题展开收起
	$('.topic_list li').hover(function(){
		$(this).addClass('active').siblings().removeClass('active');	
	},function(){
		$(this).removeClass('active');	
	})
	
	$('.file_list').each(function(){
		$(this).find('tr:even').css('background','#f2f2f2');	
	})
	
	$('.file_list tr').hover(function(){
		$(this).addClass('active').siblings().removeClass('active');
	},function(){
		$(this).removeClass('active');	
	})
	
	$('.batch_list li').hover(function(){
		$(this).addClass('active').siblings().removeClass('active');	
	},function(){
		$(this).removeClass('active');	
	})
	
	$('.permissions li').hover(function(){
		$(this).addClass('current').siblings().removeClass('current');
	},function(){
		$(this).removeClass('current');	
	})
	//关闭搜索条件
	$('.guanbi').live('click',function(){
		$(this).parent().slideUp();	
	})
	//试卷首页-表格列表隔行变色
	$('.course_data').each(function(){
		$(this).find('tr:odd').css('background','#f2f2f2');	
	})
	
	$('.data_list tr:even').find('td').css('background','#f2f2f2');	
	
	//右键菜单表现形式
	$('.mouse_right li').hover(function(){
		$(this).addClass('active').siblings().removeClass('active');
		$(this).find('.right_obj').show();	
	},function(){
		$(this).removeClass('active');
		$(this).find('.right_obj').hide();		
	})
	$('.right_obj li').hover(function(){
		$(this).addClass('current').siblings().removeClass('current');	
	},function(){
		$(this).removeClass('current');	
	})
	//弹出右键菜单
	$('.more_operation').hover(function(){
		$(this).find('.mouse_right').toggle();	
	})
	//智能组卷
	$('.intelligent_paper').live('click',function(){
		tanchu($('.pop_800'));
	})
	//题库选题
	$('.select_topic').live('click',function(){
		tanchu($('.pop_1000'));
	})
	//注释说明
	$('.note').live('click',function(){
		tanchu($('.pop_400'));
	})
	
	
	//答题卡页面鼠标hover状态
	$('.type_detail tr').hover(function(){
		$(this).addClass('active').siblings().removeClass('active');	
	},function(){
		$(this).removeClass('active');	
	})
	
	$('.select_box').live('click',function(){
		if(!$(this).hasClass('click')){
			$(this).addClass('click');
			$('.folder_list').show();
		}else{
			$(this).removeClass('click');
			$('.folder_list').hide();
		}
			
	})
	
	$('.folder_list li').hover(function(){
		$(this).addClass('active').siblings().removeClass('active');
	},function(){
		$(this).removeClass('active');	
	})
	
	//试卷首页-鼠标经过表格行出现图标
	$('.paper_data tr').hover(function(){
		$(this).find('.topic_icon').show();	
	},function(){
		$(this).find('.topic_icon').hide();	
	})
	$('.topic_table tr').hover(function(){
		$(this).addClass('active').siblings().removeClass('active');
	},function(){
		$(this).removeClass('active');	
	})
	
	$('.paper_table tr').hover(function(){
		$(this).addClass('active').siblings().removeClass('active');
	},function(){
		$(this).removeClass('active');	
	})
	$('.paper_table tr:odd').css('background','#f2f2f2');
	
	/*$('.difficulty_btn a b').each(function(){
		var text = $(this).text();
		$(this).next().keydown(function(){
			if($(this).val() == null){
				
			}else{
				if(text == "简")	{
					$(this).parent().addClass('simple');	
				}else if(text == "中"){
					$(this).parent().addClass('medium');
				}else{
					$(this).parent().addClass('difficult');
				}
			}	
		})
		
	})
	
	$('.difficulty_btn').hover(function(){
		$(this).find('.icon_score').show();	
	},function(){
		$(this).find('.icon_score').hide();	
	})*/
	
	//习题库首页
	$('.spread').live('click',function(){
		if(!$(this).parents('li').hasClass('show')){
			$(this).parents('li').addClass('show');
		}else{
			$(this).parents('li').removeClass('show');
		}	
	})
	$('.remove').live('click',function(){
		$(this).parents('li').addClass('hide');	
	})
	/*//答题卡的选择条置顶
	var Top = $('.topic_select').offset().top;
	$('.topic_select').css({'position':'absolute','top':Top});
	$(window).scroll(function(){
		var ScrollTop = $(document).scrollTop();
		if(ScrollTop < Top){
			$('.topic_select').css({'position':'absolute','top':Top});
		}else{
			$('.topic_select').css({'position':'fixed','top':0});
		}
	})*/
	
	//知识结构关联资料
	$('.chapter_list li').live('click',function(){
		var num = $(this).index();
		$(this).addClass('active').siblings().removeClass('active');
		$(this).parent().siblings('.connect_item').eq(num).show().siblings('.connect_item').hide();	
	})
	//知识结构（按章节）
	$('.chapter_item > li').live('click',function(){
		$(this).addClass('active').siblings().removeClass('active');	
	})
	
	//新增试卷-问答题
	$('.question_item li').click(function(){
		$(this).addClass('active').siblings().removeClass('active');	
	})
	
	//听力训练右侧导航
	$(window).scroll(function(){
		var _top = $(window).scrollTop();
		if(_top>300){
			$('.listening_btn').show();
		}else{
			$('.listening_btn').hide();
		}	
	})
	
	
	$('.knowledge_list dd').hover(function(){
		$(this).addClass('active').siblings().removeClass('active');	
	})
	
	
	//移动文件弹出框
	$('.first_file span').bind('click',function(){
		if($(this).parent().next().is(':hidden')){
			$(this).html('<em>-</em>');
			$(this).parent().next().slideDown();
		}else{
			$(this).html('<em>+</em>');
			$(this).parent().next().slideUp();
		}
	})
	
	
	
	
	
	
})