// JavaScript Document
$(function(){ 
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
	//进度详情-自动督促
	
	$('.auto_box').hover(function(){
		$(this).find('a').css('border-color','#797979');
		$('.ducu_box').show();	
	},function(){
		$(this).find('a').css('border-color','#fff');
		$('.ducu_box').hide();	
	})
	
	
})