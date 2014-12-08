//图片切换

(function($){ 
	$.listScroll = {
		init: function (data) {
			this.listEvent(data)
		},
		listEvent: function (data) {
			var migBox = data.obj,
				runUl = migBox.find(data.run_ul),
				tabUl = migBox.siblings(data.tab_ul),
				btnL = migBox.siblings(data.btn_l),
				btnR = migBox.siblings(data.btn_r),
				liW = runUl.find('li:first').outerWidth(true),
				imgLong = runUl.eq(0).children('li').length,
				tabHtml = '';
			if(imgLong<=2){
				btnL.hide();
				btnR.hide();
			}
			runUl.width(liW*imgLong);
			var see = Math.ceil(runUl.parent().width()/liW);
			//限制运动个数不会 超过可视个数;
			if(data.run_number>see){
				data.run_number = see; 
			}
			//创建tab切换的li
			if(see!=data.run_number){
				for(var i=0;i<imgLong;i++){
					tabHtml+= '<li></li>';
				}
			}else{
				for(var i=0;i<Math.ceil(imgLong/data.run_number);i++){
					tabHtml+= '<li></li>';
				}
			}
			
			if(tabUl.length>0){
				
				tabUl.html(tabHtml).find('li:first').addClass(data.tab_name);
				tabUl.css({'right':'auto','left':'50%','margin-left':-tabUl.find('li').length*tabUl.find('li:first').outerWidth(true)/2})
				tabAdd(0);	
			}
			
			btnL.click(function(){ 
				var runW = liW*data.run_number;
				var index = tabUl.find('.'+data.tab_name).index()-1;
				if(see==data.run_number){var tabl = -(see-data.run_number+1)}else{var tabl = -data.run_number}
				ImgRrun(runW,index,tabl);
			});
			btnR.click(function(){ 
				var runW = -liW*data.run_number;
				var index = tabUl.find('.'+data.tab_name).index()+1;
				if(see==data.run_number){var tabl = see-data.run_number+1}else{var tabl = data.run_number}
				ImgRrun(runW,index,tabl);
			});
			//滚动检测方法
			function ImgRrun(runW,index,tabl){
				var L = runUl.position().left;
				if(L%liW!=0 && liW%liW==0 || L%liW==0 && liW%liW!=0){//处理ie少1px的bug
					L = L-1;
				}
				if(L%liW==0){//检测是否运动完毕
					if(see==data.run_number){
						var num = Math.ceil( Math.abs(L)/(liW*data.run_number));
					}else{
						var num = Math.ceil( Math.abs(L)/(liW*data.run_number)*data.run_number);
					}
					
					if(L==0 && runW>0){//达到最小值返回最后一个
						runUl.animate({'left':-(runUl.width()-see*liW)},500);
						if(see==data.run_number){
							tabAdd(tabUl.find('li').length-(see-data.run_number)-1)
						}else{
							tabAdd(tabUl.find('li').length-see)
						}
					}else if(L==-(runUl.width()-see*liW) && runW<0){//达到最大值返回第一个
						runUl.animate({'left':0},500);	
						tabAdd(0)
					}else{//检测正常左右运动，以及边界检测
						
						var L1 = L+runW;
						if(L+runW>0){
							L1 = 0;
							//alert(num+tabl+(see-data.run_number))
							if(see==data.run_number){
								tabAdd(num+tabl);
							}else{
								tabAdd(0);
							}
							//tabAdd(num+tabl+(data.run_number*liW-liW)/liW);
							runUl.animate({'left':L1},500);
						}else if(L+runW<-(runUl.width()-see*liW)){
							//tabAdd(num+tabl-(data.run_number*liW-liW)/liW);
							if(see==data.run_number){
								tabAdd(num+tabl);
							}else{
								tabAdd(num+(runUl.width()-see*liW-Math.abs(L))/liW);
							}
							
							runUl.animate({'left':-(runUl.width()-see*liW)},500);	
						}else{
							runUl.animate({'left':L1},500);
							tabAdd(num+tabl);
						}
					};
					
				}//滚动完毕执行下一次	
			};//滚动检测方法结束
			//检测是否可以点击切换
			if(see==data.run_number){
				tabUl.find('li').click(function(){ 
					var runW = -liW*data.run_number;
					var index = $(this).index();
					var L = -index*liW*data.run_number;
					if(L>0){L=0;}else if(L<-(runUl.width()-see*liW)){L=-(runUl.width()-see*liW)}
					runUl.animate({'left':L},500);
					$(this).addClass(data.tab_name).siblings().removeClass(data.tab_name);
				}).css('cursor','pointer');
			};
			//给tab添加class方法
			function tabAdd(index){
				tabUl.find('li').removeClass(data.tab_name);
				for(var i=0;i<tabUl.find('li').length;i++){
					if(see==data.run_number && i == index){
						for(var j=0;j<see-data.run_number+1;j++){
							tabUl.find('li').eq(index+j).addClass(data.tab_name);
						}
					}else if(i == index){
						for(var j=0;j<see;j++){
							tabUl.find('li').eq(index+j).addClass(data.tab_name);
						}
					}
				}	
			}//tabAdd

		}
	};
	
	$.fn.listScroll = function (options) {
        var data = {
			obj:this,
			run_ul: 'ul', //运动的列表；
			tab_ul:'ul.tab_ul',//不可更改
			tab_name:'active',
			run_number:1
        };
        $.extend(true, data, options || {});
        $.listScroll.init(data);
    };
	
})(jQuery);

//图片切换调用
$('.course_switch').listScroll({      
	run_ul: '.course_list', //运动的列表；
	btn_l : '.course_prev',    //左按钮
	btn_r : '.course_next',    //右按钮
	run_number:1         //运动张数,超过可见数量就默认显示可见数量
});

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
		$(this).find('.user_info').slideDown();	
	},function(){
		$(this).find('span').removeClass('zhankai');
		$(this).find('.user_info').slideUp();	
	})
	//左侧导航展开收起
	$('.side_box').delegate('.more_tool','click',function(){
		var len = $('.side_nav li').length;
		var oHeight = $('.side_nav li').height();
		if(!$(this).hasClass('click')){
			$(this).addClass('click');
			$(this).html('收起工具<i class="icon icon_less"></i>');
			$(this).prev('.side_nav').animate({"height":len*oHeight});
		}else{
			$(this).removeClass('click');
			$(this).html('更多工具<i class="icon icon_more"></i>');
			$(this).prev('.side_nav').animate({"height":200});	
		}
	});
	//鼠标经过显示课程介绍
	$('.img_tit').hover(function(){
		$(this).find('.course_detail').stop(true).animate({top:0},500);	
	},function(){
		$(this).find('.course_detail').stop(true).animate({top:'-150px'},500);	
	})
	
	$('.icon_list li').hover(function(){
		$(this).addClass('active').siblings().removeClass('active');	
	},function(){
		$(this).removeClass('active');	
	})
	
	//首页课程鼠标经过动画
	$('.course_all>li').hover(function(){
		$(this).addClass('active').siblings().removeClass('active');
		$(this).find('p').stop(true).animate({bottom:'32px'},300);
		$(this).find('.small_icon').stop(true).animate({top:'70px'},300);	
	},function(){
		$(this).removeClass('active');
		$(this).find('p').stop(true).animate({bottom:'0'},300);
		$(this).find('.small_icon').stop(true).animate({top:'102px'},300);	
	})
	
	//收起全部课程
	$('.shouqi').live('click',function(){
		if(!$(this).hasClass('click')){
			$(this).addClass('click');
			$(this).next().slideUp();
			$(this).html('展开全部课程↓')
		}else{
			$(this).removeClass('click');
			$(this).next().slideDown();
			$(this).html('收起全部课程↑')
		}	
	})
	//评论
	$(".comment_btn").click(function(){
		if(!$(this).hasClass("click")){
			$(this).addClass("click");
			$(this).html('收起（330）');
			$(this).parent().next().slideDown();
		}else{
			$(this).removeClass("click");
			$(this).html('评论（330）');
			$(this).parent().next().slideUp();
		}
	});
	
	$('.chat_detail').click(function(){
		$(this).hide();
		$(this).next().show().focus();
		$(this).parent().next().show();
	})
	$('.text_area').blur(function(){
		$(this).hide();
		$(this).prev().show();
		$(this).parent().next().show();	
	})
	
	
	//首页通知状态点击
	$('.message_nav li').click(function(){
		$(this).addClass('active').siblings().removeClass('active');	
	})
	
	
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

	//我的课表hover上去的状态
	$('.curriculum').hover(function(){
		var oWidth = $(this).parents('td').outerWidth(true);
		if($('.curri_info').length == 0){
			$(this).find('b').css('color','#666');
			$(this).css('background','#fff');
		}else{
			$(this).find('.num').css('color','#fff');
			$(this).find('b').css('color','#fff');
			$(this).css('background','#b6bfc4');
			$(this).find('.curri_info').show().css('left',oWidth);	
		}	
	},function(){
		$(this).find('.num').css('color','#999');
		$(this).find('b').css('color','#666');
		$(this).css('background','#fff');	
		$(this).find('.curri_info').hide();
	})
	
	
	
	
	$('.language_select a').click(function(){
		$(this).addClass('active').siblings().removeClass('active');	
	})
	
	$('.pattern_list li').click(function(){
		$(this).addClass('active').siblings().removeClass('active');	
	})
	
	$('.links_list li').click(function(){
		$(this).addClass('active').siblings().removeClass('active');	
	})
	
	

	
	$('.close_btn').live('click',function(){
		$(this).parents('.course_set').slideUp();	
	})
	$('.add_teacher').live('click',function(){
		$(this).prev().slideDown();	
	})
	
	$('.member li').hover(function(){
		$(this).addClass('active').siblings().removeClass('active');	
	},function(){
		$(this).removeClass('active');	
	})
	
	//用户管理-选择站内用户
	$('.select_user').live('click',function(){
		var oHeight = $(document).height();
		var oScroll = $(window).scrollTop();
		$('.pop_bg').show().css('height',oHeight);
		$('.pop_box').show().css('top',oScroll+200);	
	})
	$('.name_box').click(function(){
		$(this).parents('.form_box').siblings('.search_content').show();	
	})

	$('.close_pop').live('click',function(){
		$('.pop_bg,.pop_box,.add_member_pop,.paper_pop').hide();
	})
	$('.search_result tr').hover(function(){
		$(this).addClass('active').siblings().removeClass('active');	
	},function(){
		$(this).removeClass('active');	
	})
	$('.search_result tr:even').css('background','#f2f2f2');
	
	
	
	//用户管理-添加新成员
	$('.add_new').live('click',function(){
		var oHeight = $(document).height();
		var oScroll = $(window).scrollTop();
		$('.pop_bg').show().css('height',oHeight);
		$('.add_member_pop').show().css('top',oScroll+50)		
	})
	$('.member_list li').hover(function(){
		$(this).addClass('active').siblings().removeClass('active');	
	},function(){
		$(this).removeClass('active');	
	})	
	
	//审核学生申请
	$('.check_info').next().hide();
	$('.check_info a').live('click',function(){
		if(!$(this).hasClass('click')){
			$(this).addClass('click');
			$(this).parent().next().show();
			$(this).text('[收起]');	
		}else{
			$(this).removeClass('click');
			$(this).parent().next().hide();
			$(this).text('[申请审核]');	
		}
	})
	
	/*$('.function_list li').hover(function(){
		$(this).addClass('active').siblings().removeClass('active');	
	},function(){
		$(this).removeClass('active');	
	})*/
	
	//表格隔行变色&鼠标放上去背景颜色变化
	$('.class_box').each(function(){
		$(this).find('tr:even').css('background','#f2f2f2');	
	})
	$('.class_box tr').hover(function(){
		$(this).addClass('active').siblings().removeClass('active');	
	},function(){
		$(this).removeClass('active');	
	})
	
	
	$('.open_btn').live('click',function(){
		var oHeight = $(document).height();
		var oScroll = $(window).scrollTop();
		$('.pop_bg').show().css('height',oHeight);
		$('.pop_box').show().css('top',oScroll+200);	
	})
	
	
	$('.function_add').live('click',function(){
		var oHeight = $(document).height();
		var oScroll = $(window).scrollTop();
		$('.pop_bg').show().css('height',oHeight);
		$('.add_member_pop').show().css('top',oScroll+50);		
	})
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
	
	$('.guanbi').live('click',function(){
		$(this).parent().slideUp();	
	})
	
	$('.course_data').each(function(){
		$(this).find('tr:odd').css('background','#f2f2f2');	
	})
	
	$('.data_list tr:even').find('td').css('background','#f2f2f2');	
	
	$('.exercise_nav_list li').hover(function(){
		var len = $('.second_nav').length;
		if(len>0){
			$(this).find('i').addClass('slide_up');
			$(this).find('.second_nav').show();	
		}
	},function(){
		$(this).find('i').removeClass('slide_up');
		$(this).find('.second_nav').hide();	
	})
	
	//新建文件夹
	//$('.add_topic').live('click',function(){
//		var content ="<tr><td><div class=\"data_name\"><input type=\"checkbox\"><i class=\"icon_24 file\"></i><input class=\"name_file\" type=\"text\"></div></td><td>文件夹</td><td>256M</td><td>2014-10-23</td></tr>"
//		$('.course_data').append(content);	
//	})
	
	
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
	
	$('.attribute').live('click',function(){
		var oHeight = $(document).height();
		var oScroll = $(window).scrollTop();
		$('.pop_bg').show().css('height',oHeight);
		$('.pop_box').show().css('top',oScroll+200);	
	})
	
	
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
			$(this).addClass('click');
			$('.folder_list').hide();
		}
			
	})
	
	$('.folder_list li').hover(function(){
		$(this).addClass('active').siblings().removeClass('active');
	},function(){
		$(this).removeClass('active');	
	})
	//禁止右键
	/*$(document).bind("contextmenu", function() { return false; });*/ 
	$(function(){
		$('.course_data').find('td').each(function(i){
			if(i%4 == 0){
				$(this).mousedown(function(e){
					if(3 == e.which){
						var X = $(this).clientX;
						var Y = $(this).clientY;
						$('.mouse_right').show().css({'position':'absolute','top':e[cur.eventPosY],'left':e[cur.eventPosX]});
					}
				})	
			}
		})	
	})
	
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
	
	$('.difficulty_btn a b').each(function(){
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
	})
})


//论坛
$(function(){ 
	$('.prensent').hover(function(){
		$(this).children('.class_theme').toggle();	
	})

})






















