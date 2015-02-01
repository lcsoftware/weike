'use strict';
var MarkModule = angular.module('app.test', []);
MarkModule.controller('MarkController', ['$scope', '$state', 'MarkingProviderUrl', function ($scope, $state, MarkingProviderUrl) {
    //获取试卷题目
    var PaperInfo_Get = function (paperid) {
       // var paperid = 11;
        var url = MarkingProviderUrl + "/PaperInfo_Get";
        var param = { PaperID: paperid };
        $scope.baseService.post(url, param, function (data) {
            $scope.paper = data.d.paper;
            $scope.papergrouplist = data.d.papergrouplist;
            $scope.exerciselist = data.d.exerciselist;
            $scope.ExerciseChoices = data.d.ExerciseChoices;

           
        });
    }

//获取考试详细
    var Test_Get = function () {
        var testid = 34;
        var url = MarkingProviderUrl + "/Test_Get";
        var param = { TestID: testid };
        $scope.baseService.post(url, param, function (data) {
            $scope.test = data.d;
            if (data.d.ScaleType == 1) {
                $scope.ScaleTypeName = "百分制";
            } else if (data.d.ScaleType == 2) {
                $scope.ScaleTypeName = "通过制";
            }
            else if (data.d.ScaleType == 3) {
                $scope.ScaleTypeName = "等级制(中文)";
            }
            else if (data.d.ScaleType == 4) {
                $scope.ScaleTypeName = "等级制(英文)";
            }
            PaperInfo_Get($scope.test.PaperID);


        });
        
    }
    var Init = function () {
        cssInit();
        Test_Get();
        //PaperInfo_Get();
    }

    var cssInit = function () {
        $('.fold_btn').live('click', function () {
            if (!$(this).hasClass('click')) {
                $(this).addClass('click');
                $(this).text('↑ 点击展开');
                $(this).next().slideUp();
            } else {
                $(this).removeClass('click');
                $(this).text('↓ 点击收缩');
                $(this).next().slideDown();
            }
        })

       
        var arr = [];
        for (var i = 0; i < $('.section_work').length; i++) {
            var _topA = $('.section_work').eq(i).offset().top;
            arr.push(_topA);
        };

        var ie6 = !-[1, ] && !window.XMLHttpRequest;
        $(window).scroll(function () {
            var _scrollTop = $(document).scrollTop();
            if (!ie6) {
                setTimeout(function () {
                    for (var j = 0; j < $('.section_work').length; j++) {
                        //var _top = $('.section').eq(j).offset().top;
                        if (_scrollTop >= arr[j]) {
                            $('.question_num li').eq(j).addClass('thisLi').siblings().removeClass('thisLi');
                        } else if (_scrollTop < arr[0]) {
                            $('.question_num li').removeClass('thisLi');
                        }
                    };
                }, 200);
            };
        });
    }
    Init();
}]);