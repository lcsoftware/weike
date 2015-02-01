// JavaScript Document
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
				liW = runUl.find('li:first').outerHeight(true),
				imgLong = runUl.children('li').length,
				tabHtml = '';
			runUl.height(liW*imgLong);
			var see = Math.ceil(runUl.parent().height()/liW);
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
				//tabUl.css({'right':'auto','left':'50%','margin-left':-tabUl.find('li').length*tabUl.find('li:first').outerWidth(true)/2})
				tabAdd(0);	
			}
			//滚动检测方法
			function ImgRrun(runW,index,tabl){
				var L = runUl.position().top;
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
						runUl.animate({'top':-(runUl.height()-see*liW)},500);
						if(see==data.run_number){
							tabAdd(tabUl.find('li').length-(see-data.run_number)-1)
						}else{
							tabAdd(tabUl.find('li').length-see)
						}
					}else if(L==-(runUl.height()-see*liW) && runW<0){//达到最大值返回第一个
						runUl.animate({'top':0},500);	
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
							runUl.animate({'top':L1},500);
						}else if(L+runW<-(runUl.height()-see*liW)){
							//tabAdd(num+tabl-(data.run_number*liW-liW)/liW);
							if(see==data.run_number){
								tabAdd(num+tabl);
							}else{
								tabAdd(num+(runUl.height()-see*liW-Math.abs(L))/liW);
							}
							
							runUl.animate({'top':-(runUl.height()-see*liW)},500);	
						}else{
							runUl.animate({'top':L1},500);
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
					if(L>0){L=0;}else if(L<-(runUl.height()-see*liW)){L=-(runUl.height()-see*liW)}
					runUl.animate({'top':L},500);
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


$(function(){
	//待处理事项 
	$('.matters_box').listScroll({      
		  run_ul: '.matters_list', //运动的列表；
		  tab_ul: '.switch_btn_list',     //切换的列表
		  tab_name : 'active', //切换的高亮class
		  run_number:7         //运动张数,超过可见数量就默认显示可见数量
	});
	
	//导入行政班鼠标经过展开
	$('.class_operation').hover(function(){
		$(this).find('ul').toggle();	
	})
	//收起展开更多搜索条件
	$('.fold_btn').click(function(){
		if(!$(this).hasClass('click')){
			$(this).addClass('click');
			$(this).siblings('.select_require').css('height','auto');
			$(this).text('[收起]');
		}else{
			$(this).removeClass('click');
			$(this).siblings('.select_require').css('height','30px');
			$(this).text('[更多]');	
		}	
	})
	//关闭搜索条件
	$('.close_box').live('click',function(){
		$(this).parent().slideToggle();	
	})
	//鼠标经过table每行出现操作按钮
	$('.result_table tr').hover(function(){
		$(this).find('.operation_box').toggle();	
	})
	//table隔行变色
	$('.result_table').each(function(){
		$(this).find('tr:odd').css('background','#f7f7f7');	
	})
	
	//关闭提示信息
	$('.close_tip').live('click',function(){
		$(this).parent().fadeOut();	
	})
	//新增信息input框focus和blur
	$('.info_detail input').each(function(){
		$(this).focus(function(){
			$(this).css('border','1px solid #3366ff');
		})
		$(this).blur(function(){
			$(this).css('border','1px solid #e4e4e4');
		})		
	})
	//出生日期
	$('.info_detail input.birth_date').focus(function(){
		$(this).css('border','1px solid #284a51');
		$('.birth_date_box').show();
	})
	
	
	//教学安排-校历table
	$('.schedule_table tr').hover(function(){
		$(this).find('.schedule_operation').show();
		$(this).css('background','#f2f2f2');
	},function(){
		$(this).find('.schedule_operation').hide();
		$(this).css('background','#fff');	
	})
	//点击展开课程安排
	$('.J_fold').live('click',function(){
		if(!$(this).hasClass('click')){
			$(this).addClass('click');
			$(this).css('background-position','-64px 0');
			$(this).parents('tr').css('background','#f2f2f2');
			$(this).parents('tr').next().show();
		}else{
			$(this).removeClass('click');
			$(this).css('background-position','-48px 0');
			$(this).parents('tr').css('background','#fff');
			$(this).parents('tr').next().hide();
		}	
	})
	//新增校历table
	$('.curri_table').each(function(){
		$(this).find('tr:even').css('background','#f2f2f2');	
	})
	$('.curri_table tr').hover(function(){
		$(this).find('.curri_operation').toggle();	
	})
	
	//关闭弹出层
	$('.icon_close').click(function(){
		$('.pop_bg,.pop_600,.pop_400,.pop_800,.pop_1000').hide();	
	})
	
	//弹出层方法
	function tanchu(popbox){
		var oHeight = $(document).height();
		var oScroll = $(window).scrollTop();
		$('.pop_bg').show().css('height',oHeight);
		popbox.show().css('top',oScroll+200);
	}
	/*上级机构下拉框*/
	$('.default_status').live('click',function(){
		if(!$(this).hasClass('click')){
			$(this).addClass('click');
			$(this).children('input').css('border-color','#366061');
			$(this).next().show();	
		}else{
			$(this).removeClass('click');
			$(this).children('input').css('border-color','#ccc');
			$(this).next().hide();	
		}
	})
	//组织机构列表
	$('.department_box').hover(function(){
		$(this).children('.edit_box').show();
		$(this).css('background','#f2f2f2');	
	},function(){
		$(this).children('.edit_box').hide();
		$(this).css('background','#fff');
	})
	//组织机构列表展开收起
	$('.menu_btn').live('click',function(){
		if(!$(this).hasClass('click')){
			$(this).addClass('click');
			$(this).children('.btn_fold').css('background-position','-48px -48px');
			$(this).parent().next().show();	
		}else{
			$(this).removeClass('click');
			$(this).children('.btn_fold').css('background-position','-48px -32px');
			$(this).parent().next().hide();	
		}	
	})
})

