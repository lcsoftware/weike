
'use strict';
var siteModule = angular.module('app.site', []);
siteModule.controller('SiteController', ['$scope', '$state', 'siteProviderUrl', function ($scope, $state, siteProviderUrl) {
   
    //设置模板
    $scope.hid_sidemod = "1";
  
    //更改模板
    $scope.chickbanmian = function (type) {
        $scope.hid_sidemod = type;
       
        OCSite_DisplayStyle_Upd(type);
    }

    //加载模块
    var OCSite_GetLoad = function () {
       // debugger;
        var ocid = 1;
        var url = siteProviderUrl + "/OCSite_Get";
        var param = { OCID: ocid };
        $scope.baseService.post(url, param, function (data) {
            $scope.hid_sidemod = data.d[0].DisplayStyle;
            var Language = data.d[0].Language;
            
            if (Language == 1) {
                $("#Language_1").removeClass("english").addClass("chinese");
                $("#Language_0").removeClass("chinese").addClass("english");
            } else {
                $("#Language_1").removeClass("chinese").addClass("english");
                $("#Language_0").removeClass("english").addClass("chinese");
            }
            GetMod();

        });
    }

    
    //新增主栏目
    $scope.OCSiteColumn_ADD = function () {
        var txtname = $scope.txt_column_name;
        var ContentType = $('input[name="rad_addcolumns"]:checked').val();
        var url = siteProviderUrl + "/OCSiteColumn_ADD";
        var param = { columnsname: txtname, type: ContentType };
        $scope.baseService.post(url, param, function (data) {
            if (parseInt(data.d) > -1) {
                $('#myModal').modal("hide"); 
              
            }

        });
    }
    //获取网站栏目
    var OCSite_Get = function () {
        // var ocid = $rougeParams.OCID;
        var ocid = 1;
        var url = siteProviderUrl + "/OCSiteColumn_Tree";
        var param = { OCID: ocid };
        $scope.baseService.post(url, param, function (data) {
            $scope.ColumnList = data.d;

        });
    }

    //切换语言
    $scope.OCSite_Language_Upd = function (type) {
        if (type == 1) {
            $("#Language_1").removeClass("english").addClass("chinese");
            $("#Language_0").removeClass("chinese").addClass("english");
        } else {
            $("#Language_1").removeClass("chinese").addClass("english");
            $("#Language_0").removeClass("english").addClass("chinese");
        }
        var siteid = 1;
        var url = siteProviderUrl + "/OCSite_Language_Upd";
        var param = { SiteID: siteid, Language: type };
        $scope.baseService.post(url, param, function (data) {
           

        });
    }

    //更换网站风格
    var OCSite_DisplayStyle_Upd = function (type) {
        var siteid = 1;
        var url = siteProviderUrl + "/OCSite_DisplayStyle_Upd";
        var param = { SiteID: siteid, DisplayStyle: type };
        $scope.baseService.post(url, param, function (data) {
            if (data.d) {
                GetMod();
            }

        });
    }


    //导航点击方法
    $scope.OCSiteColumn_Conten_Upd = function (type) {
        //type 1: 内页模式 3:列表模式
        if (type == 0) {
            $("#div_indexhome").show();
            $("#div_ewebeditor").hide();
            $("#div_mainList").hide();
        }
        else if (type == 1) {
            $("#div_indexhome").hide();
            $("#div_ewebeditor").show();
            $("#div_mainList").hide();
        } else if (type == 3) {
            $("#div_indexhome").hide();
            $("#div_mainList").show();
            $("#div_ewebeditor").hide();

        }
    }

    //加载模板样式
    var GetMod = function () {
        if ($scope.hid_sidemod == 1) {
            //left
            $("#side_top").removeClass("across_nav side_right");
            $("#side_top").addClass("side_left");
            var oHeight = $('.main_content').outerHeight(true);
            $('.side_left').height(oHeight);
            var screenWidth = $(window).width();
            var boxWidth = $('.main_content').width();
            var sideWidth = $('.side_left').width();
            $('.main_content').css('left', (screenWidth - boxWidth + sideWidth) / 2);
        } else if ($scope.hid_sidemod == 2) {
            //right
            $("#side_top").removeClass("across_nav ");
            $("#side_top").addClass("side_right");
            $("#side_top").removeClass("side_left");
            var oHeight = $('.main_content').outerHeight(true);
            $('.side_right').height(oHeight);
            var screenWidth = $(window).width();
            var boxWidth = $('.main_content').width();
            var sideWidth = $('.side_right').width();
            $('.main_content').css('left', (screenWidth - boxWidth - sideWidth) / 2);
        } else if ($scope.hid_sidemod == 3) {
            //top
            var screenWidth = $(window).width();
            var boxWidth = $('.main_content').width();
            var sideWidth = $('.side_right').width();
            var strWidth = document.body.clientWidth;
            strWidth = (strWidth - 1300) / 2 + 28;
            $('.main_content').css('left', (screenWidth - boxWidth + sideWidth) / 2);
            $("#side_top").addClass("across_nav");
            $('#side_top').height(49);
        }
        $(".active").removeClass("active");
        $("#active_" + $scope.hid_sidemod).addClass("active");
    }
   
 
    OCSite_GetLoad();
    OCSite_Get();

   

}]);



