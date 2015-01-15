'use strict';

var app = angular.module('app.custom.directives', []);

app.directive('folderList', function () {
    var directive = {};

    directive.restrict = 'EA';

    directive.scope = {
        itemName: '@',
        onSelected: '&',
        onCancel: '&'
    }

    directive.replace = true;

    directive.templateUrl = '/Components/templates/folderList.html';

    directive.compile = function () {
        var c = {};

        c.pre = function(scope, iElem, iAttrs){
            $('.cancel').bind('click', function () {
                console.log('33333333333333333');
            });

            $('.select_box').live('click', function () {
                if (!$(this).hasClass('click')) {
                    $(this).addClass('click');
                    $('.folder_list').show();
                } else {
                    $(this).removeClass('click');
                    $('.folder_list').hide();
                    directive.scope.itemName = 'fffff';
                } 
            }); 

            $('.folder_list li').hover(function () {
                $(this).addClass('active').siblings().removeClass('active');
            }, function () {
                $(this).removeClass('active');
            });

            $('li a').bind('click', function () {
                $('.select_box').click();
            });

        };

        c.post = function(scope, iElem, iAttrs){
           
        };

        return c; 
    }

    return directive;
});



app.directive('fileOperation', function () {
    var directive = {};

    directive.restrict = 'EA';

    directive.scope = {
        onSelected: '&',
        onCancel: '&'
    }

    directive.templateUrl = '/Components/templates/fileOperation.html';

    directive.compile = function () {
        var c = {};

        c.pre = function (scope, iElem, iAttrs) { 
            //弹出右键菜单
            $('.more_operation').hover(function () {
                $(this).find('.mouse_right').toggle();
            });

            //$('.mouse_right li').hover(function () {
            //    $(this).addClass('active').siblings().removeClass('active');
            //    $(this).find('.right_obj').show();
            //}, function () {
            //    $(this).removeClass('active');
            //    $(this).find('.right_obj').hide();
            //});
            console.log('pre' + iElem);
        };

        c.post = function (scope, iElem, iAttrs) {
            //弹出右键菜单
            //$('.more_operation').hover(function () {
            //    $(this).find('.mouse_right').toggle();
            //});
            console.log('post' + iElem);
            iElem.find('liA').hover(function () {
                $(this).addClass('active').siblings().removeClass('active');
                $(this).find('.right_obj').show();
            }, function () {
                $(this).removeClass('active');
                $(this).find('.right_obj').hide();
            });

            iElem.find('liB').hover(function () {
                $(this).addClass('active').siblings().removeClass('active');
                $(this).find('.right_obj').show();
            }, function () {
                $(this).removeClass('active');
                $(this).find('.right_obj').hide();
            });
            //$('.mouse_right li').hover(function () {
            //    $(this).addClass('active').siblings().removeClass('active');
            //    $(this).find('.right_obj').show();
            //}, function () {
            //    $(this).removeClass('active');
            //    $(this).find('.right_obj').hide();
            //});
        }; 
        return c;
    }

    directive.link = function (scope, iElem, iAttrs) {
        console.log('link');
    }

    return directive;
});