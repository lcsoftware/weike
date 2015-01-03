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
				liW = runUl.find('li:first').outerWidth(true),
				imgLong = runUl.eq(0).find('li').length,
				tabHtml = '';
			if(imgLong<=3){
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

$(function(){
	$('.video_item').listScroll({      
		run_ul: '.video_list', //运动的列表；
		btn_l : '.icon_l',    //左按钮
		btn_r : '.icon_r',    //右按钮
		run_number:1         //运动张数,超过可见数量就默认显示可见数量
	});
	//更换版面
	$('.version_box').hover(function () {
	    $(this).css('background', '#393939');
	    $(this).find('.change_version').show();
		//var oLeft = $(this).offset().left;
		//var oScroll = $(this).scrollTop();
		//if (oLeft != 0) {
		//    var strwidth = parseInt($(".change_version").width());
		//    $(".change_version").css('left', '-' + (strwidth + 29) + 'px');
		//    $(".change_version").css('left', 'none');
		//    $(".change_version").css('right', '200px');
		//} else {
		//    $(".change_version").removeAttr("style");
		//    $(this).find('.change_version').show();
		//}
		

	},function(){
		$(this).css('background','#727272');
		$(this).find('.change_version').hide();	
	})
	//网站建设模式切换
	$('.version_list li').click(function(){
		$(this).addClass('active').siblings().removeClass('active');	
	})
	
	//导航
	//$('.column_list li').hover(function () {
	//    $(this).children('.column_btn').show();
	//	$(this).addClass('active').siblings().removeClass('active');	
	//},function(){
	//	$(this).children('.column_btn').hide();
	//	$(this).removeClass('active');
    //})

	$('.column_list li').live('hover', function () {
	    if (event.type == 'mouseover') {
	        $(this).children('.column_btn').show();
	        $(this).addClass('active').siblings().removeClass('active');
	    } else {
	        $(this).children('.column_btn').hide();
	        $(this).removeClass('active');
	    }
	})
	
	$('.img_list li').hover(function(){
		$(this).addClass('active').siblings().removeClass('active');	
	},function(){
		$(this).removeClass('active');
	})
	//点击弹出banner样式图
	$('.banner_box a').live('click',function(){
		var oHeight = $(document).height();
		var oScroll = $(window).scrollTop();
		$('.pop_bg').show().css('height',oHeight);
		$('.pop_600').show().css('top',oScroll+100);		
	})
	
	//$('.add_columns').live('click',function(){
	  
	//})
	
	$('.add_list').live('click',function(){
		var oHeight = $(document).height();
		var oScroll = $(window).scrollTop();
		$('.pop_bg').show().css('height',oHeight);
		$('.pop_800').show().css('top',oScroll+100);
	})
	
	//var screenWidth = $(window).width();
	//var boxWidth = $('.main_content').width();
	//var sideWidth = $('.side_left').width();
	//$('.main_content').css('left',(screenWidth-boxWidth+sideWidth)/2); 与site_left有冲突
	
	//关闭弹出层
	$('.icon_close').click(function(){
		$('.pop_bg,.pop_600,.pop_400,.pop_800').hide();	
	})
	
	$('.knowledge_list tr').hover(function(){
		$(this).addClass('active').siblings().removeClass('active');	
	},function(){
		$(this).removeClass('active');
	})
	
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
	
	
	$('.course_data').each(function(){
		$(this).find('tr:odd').css('background','#f2f2f2');	
	})
	
	$('.guanbi').live('click',function(){
		$(this).parent().hide();	
	})
	
})


