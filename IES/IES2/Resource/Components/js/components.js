'use strict';

var app = angular.module('app.custom.directives', []);


app.directive('fileOperation', function () {
    var directive = {};

    directive.restrict = 'EA';

    directive.scope = {
        onProperty: '&',
        onRename: '&',
        onDownload: '&',
        onVideo: '&',
        onRemove: '&'
    }

    directive.templateUrl = '/Components/templates/fileOperation.html';

    directive.link = function (scope, elem, iAttrs) {
        //弹出右键菜单
        elem.find('.more_operation').hover(function () {
            $(this).find('.mouse_right').toggle();
        });

        //右键菜单表现形式
        elem.find('.mouse_right li').hover(function () {
            $(this).addClass('active').siblings().removeClass('active');
            $(this).find('.right_obj').show();
        }, function () {
            $(this).removeClass('active');
            $(this).find('.right_obj').hide();
        });
    }

    return directive;
});

app.directive('addKnowledge', function () {
    var directive = {};

    directive.restrict = 'EA';

    directive.scope = {
        knowledge: '=',
        chapter: '=',
        chapters: '=',
        importance: '=',
        importances: '=',
        onSaveNew: '&',
        onSave: '&',
        onCancel: '&'
    }

    directive.templateUrl = '/Components/templates/addKnowledge.html';

    directive.link = function (scope, elem, iAttrs) {
        //弹出右键菜单
        var oHeight = $(document).height();
        var oScroll = $(window).scrollTop();
        var bgCls = '.' + scope.bgClass;
        var popCls = '.' + scope.popClass;
        elem.find('.pop_bg').show().css('height', oHeight);
        elem.find('.pop_400').show().css('top', oScroll + 200);


        elem.find('#btnCancel,#btnSave,.close_pop').bind('click', function () {
            elem.hide();
        }) 
    }

    return directive;
});;


app.directive('addChapter', function () {
    var directive = {};

    directive.restrict = 'EA';

    directive.scope = {
        chapter: '=',
        knowledge: '=',
        knowledges: '=',
        importance: '=',
        importances: '=',
        onSaveNew: '&',
        onSave: '&',
        onCancel: '&'
    }

    directive.templateUrl = '/Components/templates/addChapter.html';

    directive.link = function (scope, elem, iAttrs) {
        //弹出右键菜单
        var oHeight = $(document).height();
        var oScroll = $(window).scrollTop();
        var bgCls = '.' + scope.bgClass;
        var popCls = '.' + scope.popClass;
        elem.find('.pop_bg').show().css('height', oHeight);
        elem.find('.pop_400').show().css('top', oScroll + 200);

        elem.find('#btnCancel,#btnSave,.close_pop').bind('click', function () {
            elem.hide();
        })
    }

    return directive;
});

app.directive('folder', function () {
    var directive = {};

    directive.restrict = 'EA';

    directive.scope = {
        onOpen: '&',
        onBlur: '&',
        folderName: '='
    }

    directive.templateUrl = '/Components/templates/folder.html';

    directive.link = function (scope, elem, iAttrs) {
        //重命名表现形式
        elem.find('.data_tit').live('dblclick', function () {
            $(this).hide();
            $(this).next().show().select();
        });
    }

    return directive;
});