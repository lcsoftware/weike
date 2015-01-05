// JavaScript Document
$(function(){ 
	$('.class_operation').hover(function(){
		$(this).find('ul').toggle();	
	})

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
	
	$('.close_box').live('click',function(){
		$(this).parent().slideToggle();	
	})
	
	$('.result_table tr').hover(function(){
		$(this).find('.operation_box').toggle();	
	})
	
	$('.result_table').each(function(){
		$(this).find('tr:odd').css('background','#f7f7f7');	
	})

})

