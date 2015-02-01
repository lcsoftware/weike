'use strict';
var MOOCPreviewModule = angular.module('app.moocpreview', []);
MOOCPreviewModule.controller('MOOCPreviewController', ['$scope', '$state', 'MOOCPreviewProviderUrl', function ($scope, $state, MOOCPreviewProviderUrl) {
    $scope.Videoprogress = "0";
    //获取章节列表
    var ChapterStudy_List = function () {
        var ocid = 1;
        var url = MOOCPreviewProviderUrl + "/ChapterStudy_List";
        var param = { OCID: ocid };
        $scope.baseService.post(url, param, function (data) {
            $scope.chapterStudy_list = data.d;
        });
        $scope.$on('ngMoocChapter', function (ngRepeatFinishedEvent) {
            $('.wrap_item').hover(function () {
                $(this).find('.progress_tip').toggle();
                
                $(this).find('.column_btn').show();
                if (!$(this).parent().hasClass('active')) {
                    $(this).css('background', '#f2f2f2');
                } else {
                    $(this).css('background', '');
                }
            }, function () {
                $(this).find('.column_btn').hide();
                $(this).css('background', '');
            })
         
            var flag = 0;//标记是否有未完成的章节
            for (var i = 0; i < $scope.chapterStudy_list.length; i++) {
                for (var k = 0; k < $scope.chapterStudy_list[i].Children.length; k++) {
                    if ($scope.chapterStudy_list[i].Children[k].IsFinish == 0) {
                        $scope.chapterStudy_list[i].Children[k].IsActive = true;
                        flag = 1;
                        OCMoocFile_List($scope.chapterStudy_list[i].Children[k].ChapterID);
                        $scope.ChapterLIClick($scope.chapterStudy_list[i].ChapterID);
                        $scope.ChapterzhangName ="第"+(i+1)+"章";
                        $scope.ChapterjieName = $scope.chapterStudy_list[i].Children[k].Title;
                        return;
                    }
                }
            }
            if (flag == 0) {
                for (var i = 0; i < $scope.chapterStudy_list.length; i++) {
                    for (var k = 0; k < $scope.chapterStudy_list[i].Children.length; k++) {
                        if ($scope.chapterStudy_list[i].Children[k].IsFinish == 1) {
                            $scope.chapterStudy_list[i].Children[k].IsActive = true;
                            flag = 1;
                            OCMoocFile_List($scope.chapterStudy_list[i].Children[k].ChapterID);
                            $scope.ChapterLIClick($scope.chapterStudy_list[i].ChapterID);
                            $scope.ChapterzhangName = "第" + (i + 1) + "章";
                            $scope.ChapterjieName = $scope.chapterStudy_list[i].Children[k].Title;
                            return;
                        }

                    }
                }
            }
        });
    }
   
    //获取章节我文件列表
    var OCMoocFile_List = function (chapterid) {
        ForumTopic_ChapterID_List(chapterid); //论坛
       var ocid = 1;
       var url = MOOCPreviewProviderUrl + "/OCMoocFileStudy_List";
       var param = { ChapterID: chapterid, OCID: ocid };
       $scope.baseService.post(url, param, function (data) {
           $scope.moocfile_list = data.d;
       });

       $scope.$on('ngMoocChapterFile', function (ngRepeatFinishedEvent) {
           var flag = 0;
           for (var i = 0; i < $scope.moocfile_list.length; i++) {
               if ($scope.moocfile_list[i].FinishRate < 100) {
                   $scope.moocfile_list[i].IsActive = true;
                   play($scope.moocfile_list[i]);
                   OCMoocVideoInsert_List($scope.moocfile_list[i].ChapterID, $scope.moocfile_list[i].FileID);
                   flag = 1;
                   break;
               }
           }
           if (flag == 0) {
               $scope.moocfile_list[0].IsActive = true;
               play($scope.moocfile_list[0]);
               OCMoocVideoInsert_List($scope.moocfile_list[i].ChapterID, $scope.moocfile_list[i].FileID);
               flag = 1;
           }
       });
   }
    //章节点击事件
    $scope.ChapterClick = function (thi) {
       for (var i = 0; i < $scope.chapterStudy_list.length; i++) {
           for (var k = 0; k < $scope.chapterStudy_list[i].Children.length; k++) {
               if ($scope.chapterStudy_list[i].Children[k].IsActive) {
                   $scope.chapterStudy_list[i].Children[k].IsActive = false;
               }
               if (thi.chapterStudy1.ChapterID == $scope.chapterStudy_list[i].Children[k].ChapterID) {
                   $scope.ChapterzhangName = "第" + (i + 1) + "章";
                   $scope.ChapterjieName = thi.chapterStudy1.Title;
               }
           }
       }
       thi.chapterStudy1.IsActive = true;
       OCMoocFile_List(thi.chapterStudy1.ChapterID);
       ClearPieTime();
       
   }

    //章节测试事件
   $scope.ChapterTestClick = function (thi) {
      // alert(1);
   }

    //获取网站和负责人
   var OC_Get = function () {
       var ocid = 1;
       var url = MOOCPreviewProviderUrl + "/OC_Get";
       var param = { OCID: ocid };
       $scope.baseService.post(url, param, function (data) {
           if (data.d != null) {
               $scope.Name = data.d[0].Name;
               $scope.ChargeUserName = data.d[0].ChargeUserName;
           }
       });
   }

    //文件点击事件
   $scope.ChapterFileClick = function (thi) {
       for (var i = 0; i < $scope.moocfile_list.length; i++) {
           if ($scope.moocfile_list[i].MoocFileID == thi.moocfile.MoocFileID) {
               $scope.moocfile_list[i].IsActive = true;
               play($scope.moocfile_list[i]);
               //获取知识卡
               OCMoocVideoInsert_List($scope.moocfile_list[i].ChapterID, $scope.moocfile_list[i].FileID);
           }
           if ($scope.moocfile_list[i].IsActive) {
               $scope.moocfile_list[i].IsActive = false;
           }
       }
       thi.moocfile.IsActive = true;
       ClearPieTime();
   }

    //获取知识卡
   var OCMoocVideoInsert_List = function (chapterid, fileid) {
       var ocid = 1;
       var url = MOOCPreviewProviderUrl + "/OCMoocVideoInsert_List";
       var param = { ChapterID: chapterid, FileID: fileid };
       $scope.baseService.post(url, param, function (data) {
           $scope.moocvideoinsert_list = data.d;
           $scope.moocvideoinsertlength = data.d.length;
       });
   }



    //章+号点击事件
   $scope.ChapterLIClick = function (id) {
       var thi = "#sp_" + id;
       if (!$(thi).hasClass('click')) {
           var len = $(thi).parents('.rate_box').next().length;
           if (len > 0) {
               $(thi).addClass('click');
               $(thi).text('-');
               $(thi).parents('.rate_box').next().slideDown();
           }
       } else {
           $(thi).removeClass('click');
           $(thi).text('+');
           $(thi).parents('.rate_box').next().slideUp();
       }
   }
   
    //获取讨论
   var ForumTopic_ChapterID_List = function (chapterid) {
       var ocid = 1;
       var pageindex = 1;
       var pagesize = 10;
       var url = MOOCPreviewProviderUrl + "/ForumTopic_ChapterID_List";
       var param = { ChapterID: chapterid, PageIndex: pageindex, PageSize: pagesize };
       $scope.baseService.post(url, param, function (data) {
           $scope.forumtopic_list = data.d;
       });
   }
    //计算时间差
   $scope.getDateDiff = function (desc, dateTimeStamp) {
       if (dateTimeStamp == null || dateTimeStamp == '') {
           return;
       }
       var re = /-?\d+/;
       var m = re.exec(dateTimeStamp);
       if (m < 0) { return ""; }
       var minute = 1000 * 60;
       var hour = minute * 60;
       var day = hour * 24;
       var halfamonth = day * 15;
       var month = day * 30;
       var now = new Date().getTime();
       var diffValue = now - m;

       var monthC = diffValue / month;
       var weekC = diffValue / (7 * day);
       var dayC = diffValue / day;
       var hourC = diffValue / hour;
       var minC = diffValue / minute;

       if (monthC >= 1) {
           result = desc + parseInt(monthC) + "个月前";
       }
       else if (weekC >= 1) {
           result = desc + parseInt(weekC) + "个星期前";
       }
       else if (dayC >= 1) {
           result = desc + parseInt(dayC) + "天前";
       }
       else if (hourC >= 1) {
           result = desc + parseInt(hourC) + "个小时前";
       }
       else if (minC >= 1) {
           result = desc + parseInt(minC) + "分钟前";
       } else {
           result = "刚刚发表";
       }
       return result;
   }
    //视频/文件播放
   var play = function (filedata) {
       var URL=filedata.ViewUrl;
       if (filedata.IsAllowStudy == 0)
       {
           URL = "请先学习前面的章节";
       } else if (filedata.IsAllowStudy == 2) {
           URL = "你的班级尚未开始";
       } else if (filedata.IsAllowStudy == 3) {
           url = "请先学习完前面的章节";
       } else if (filedata.IsAllowStudy == 4) {
           url = "请先完成上一章的测试";
       } else if (filedata.IsAllowStudy == 5) {
           url = "请先学习完该章节";
       } else if (filedata.IsAllowStudy == 6) {
           url = "请先完成所有章节及测试";
       }
       var screenHeight = $(window).height()-50;
       if (filedata.FileType == 1) {
           $("#div_play").show();
           var strurl = window.location.href;
           var strsplit = strurl.split("/OC");
           var palyurl = strsplit[0];
           var data = {
               id: "div_play",
               flash: palyurl + "/Frameworks/jwplay/pl.swf",
               url: URL,
               width: "100%",
               height: screenHeight,
               autoplay: "false"
           }
           $G2S.Play(data);
           jwplayer(data.id).play();
           jwplayer(data.id).setMute(false);
       } else {
           $("#div_play").hide();
           jwplayer("div_play").setMute(true);
       }
       jwonTime(filedata);
   }
   var IsShow = false;
   var int_id;
    //自动记录观看秒
   var jwonTime = function (filedata) {
       var maxTime = filedata.Seconds;
       var VideoTime = filedata.TimeLength;
       jwplayer().onReady(function (obj) {
           if (maxTime < VideoTime)
               jwplayer("div_play").seek(maxTime);
           else {
               jwplayer("div_play").seek(0);
           }
       });
       jwplayer().onTime(function (obj){
               var s = parseInt(obj.position);
               if (s % 10 == 0 && s != 0) {
                   OCMoocStuFile_StuVideoDesc_Add(filedata, s);
               }
               if (s <= parseInt(obj.duration)) {
                   if (s == maxTime + 1) {
                       maxTime = s;
                   }
                   if (s > maxTime + 1) {
                      // jwplayer("div_play").seek(maxTime);
                   }
               }
               if (IsShow == false ) {
                   PopupOCMoocVideoInsert(s);
               }
       });
       //记录视频播放
       jwplayer().onPlay(function () {
           int_id= setInterval(function(){
               $scope.$apply(saveTime(filedata));
                   },1000);
       }); 
       //播放完成
       jwplayer("div_play").onComplete(function () {
           //视频时长入库;
           OCMoocStuFile_StuVideoDesc_Add(filedata, filedata.TimeLength);
           for (var i = 0; i < $scope.moocfile_list.length; i++) {
               if (filedata.FileID == $scope.moocfile_list[i].FileID && i != (($scope.moocfile_list.length)-1)) {
                   $scope.moocfile_list[i + 1].IsActive = true;
                   $scope.moocfile_list[i].IsActive = false;
                   $scope.moocfile_list[i].FinishRate = 100;
                   play($scope.moocfile_list[i+1]);
                 
               } else if (i == (($scope.moocfile_list.length) - 1)) {
                   refreshChapterStudy_List();
               }
           }
       });
      
   }
   var flagcount = 0;
    //记录时间
   var saveTime = function (filedata) {
       flagcount++;
       if (jwplayer().getState() == "PAUSED" || jwplayer().getState() == "IDLE") { //判断视频是否暂停
           clearInterval(int_id);
       }
       var rate = (Math.round((1 / filedata.TimeLength) * 10000) / 10000)*100;
       var zongrate = filedata.FinishRate + rate;
       zongrate = Math.round(zongrate * 100) / 100;
       if (zongrate >= 100) {
           zongrate = 100;
       }
       var clr = "#008573";
       if (zongrate >= filedata.NeedRate) {
           clr = "Red";
       }
       $G2S.PieNoCartoon("ddddd", zongrate, clr);
       if (flagcount%10==0) {
           OCMoocStuFile_Add(filedata, flagcount);
           flagcount == 0;
       }
       for (var i = 0; i < $scope.moocfile_list.length; i++) {
           if (filedata.FileID == $scope.moocfile_list[i].FileID) {
               $scope.moocfile_list[i].FinishRate = zongrate;
               break;
           }
       }
   }

    //清除圆形图
   var ClearPieTime = function () {
       flagcount = 0;
       clearInterval(int_id);
       $("#ddddd").html("");
   }

    //清除弹窗隐藏效果
   var ClearIsShow = function (s) {
       for (var i = 0; i < $scope.moocvideoinsert_list.length; i++) {
           if ((s+1) == $scope.moocvideoinsert_list[i].Second) {
               IsShow = true;
           }
       }
   }

    //定时器让该知识卡重新弹出状态
   var savemoocVideoInsert = function () {
       IsShow = false;
   }
    
    //弹出知识卡
   var PopupOCMoocVideoInsert = function (s) {
       for (var i = 0; i < $scope.moocvideoinsert_list.length; i++) {
           if (s == $scope.moocvideoinsert_list[i].Second) {
               IsShow = true;
               $scope.txt_Conten = $scope.moocvideoinsert_list[i].Conten;
               if (jwplayer().getState() == "PLAYING") {
                   jwplayer().pause();
               }
               $('#myModal').modal('show');
               ClearIsShow(s);
              
           }
       }
   }

    //知识点关闭按钮
   $scope.OCMOOCVideoHide = function () {
       if (jwplayer().getState() == "PAUSED") {
           jwplayer().play();
       }
       setTimeout(function () {
           $scope.$apply(savemoocVideoInsert());
       }, 1000);
       $scope.txt_Conten = "";
   }

    //学习资源时视频点入库,且记录日志  
   var OCMoocStuFile_StuVideoDesc_Add = function (filedata, seconds) {
       //更新对象的秒
       for (var i = 0; i < $scope.moocfile_list.length; i++) {
           if (filedata.FileID == $scope.moocfile_list[i].FileID) {
               $scope.moocfile_list[i].Seconds = seconds;
               break;
           }
       }
       var chapterid = filedata.ChapterID;
       var fileid = filedata.FileID;
       var url = MOOCPreviewProviderUrl + "/OCMoocStuFile_StuVideoDesc_Add";
       var param = { ChapterID: chapterid, FileID: fileid, Seconds: seconds };
       $scope.baseService.post(url, param, function (data) {
       });
   }

   var OCMoocStuFile_Add = function (filedata, seconds) {
       var chapterid = filedata.ChapterID;
       var fileid = filedata.FileID;
       var url = MOOCPreviewProviderUrl + "/OCMoocStuFile_Add";
       var param = { ChapterID: chapterid, FileID: fileid, Seconds: seconds };
       $scope.baseService.post(url, param, function (data) {
       });
   }

    //刷新章节
   var refreshChapterStudy_List = function () {
       var ocid = 1;
       var url = MOOCPreviewProviderUrl + "/ChapterStudy_List";
       var param = { OCID: ocid };
       $scope.baseService.post(url, param, function (data) {
           $scope.chapterStudy_list = data.d;
       });
   }

    //样式控制
   var cssInit = function () {
       //章节列表
       $('.first_chapter').hover(function () {
           $(this).find('.operation_btn').show();
           $(this).css('background', '#f2f2f2');
       }, function () {
           $(this).find('.operation_btn').hide();
           $(this).css('background', '#fff');
       })
       //是否启用
       $('.open_btn').live('click', function () {
           if (!$(this).hasClass('click')) {
               $(this).addClass('click');
               $(this).css('background-position', '-32px -48px');
           } else {
               $(this).removeClass('click');
               $(this).css('background-position', '-32px -32px');
           }
       })

       //关闭提示文本框
       $('.tip_text span').click(function () {
           $(this).parent().hide();
       })

       $('.icon_zhankai').click(function () {
           $(this).parent().toggle();
           $(this).parent().siblings().toggle();
       })

       $('.exercise_list li').hover(function () {
           $(this).addClass('active').siblings().removeClass('active');
       }, function () {
           $(this).removeClass('active');
       })

       $('.way1').click(function () {
           $('.recruit_list').eq(0).show();
           $('.recruit_list').eq(1).hide();
       })
       $('.way2').click(function () {
           $('.recruit_list').eq(1).show();
           $('.recruit_list').eq(0).hide();
       })
       //教学计划
       $('.group_discuss a').live('click', function () {
           if (!$(this).hasClass('click')) {
               $(this).addClass('click');
               $(this).parents('.chapt_box').next().slideDown();
               $(this).text('[收起]');
           } else {
               $(this).removeClass('click');
               $(this).parents('.chapt_box').next().slideUp();
               $(this).text('[展开]');
           }
       })

       //预览
       $('.note_list li').hover(function () {
           $(this).addClass('active').siblings().removeClass('active');
       }, function () {
           $(this).removeClass('active');
       })

       $('.item_list li').live('click', function () {
           var num = $(this).index();
           $(this).addClass('active').siblings().removeClass('active');
           $(this).parent().siblings('.detail_info').eq(num).show().siblings('.detail_info').hide();
       })

     

       var screenWidth = $(window).width();
       var screenHeight=$(window).height();
       $('.video_tap').attr("width", (screenWidth - 340));
       $(".preview_box").css("min-height", screenHeight);

   }
   var Init = function () {
       cssInit();
       ChapterStudy_List();
       OC_Get();
   }
   Init();
  
}]);


MOOCPreviewModule.controller('MOOCChapterController', ['$scope', '$state', 'MOOCPreviewProviderUrl', function ($scope, $state, MOOCPreviewProviderUrl) {


    //获取知识卡
    var OCMoocVideoInsert_List = function (chapterid, fileid) {
        var ocid = 1;
        var url = MOOCPreviewProviderUrl + "/OCMoocVideoInsert_List";
        var param = { ChapterID: chapterid, FileID: fileid };
        $scope.baseService.post(url, param, function (data) {
            $scope.moocvideoinsert_list = data.d;
            $scope.moocvideoinsertlength = data.d.length;
        });
    }


    //获取章节我文件列表
    var OCMoocFile_List = function (chapterid) {
        var ocid = 1;
        var url = MOOCPreviewProviderUrl + "/OCMoocFileStudy_List";
        var param = { ChapterID: chapterid, OCID: ocid };
        $scope.baseService.post(url, param, function (data) {
            $scope.moocfile_list = data.d;
        });

        $scope.$on('ngMoocChapterFile', function (ngRepeatFinishedEvent) {
                $scope.moocfile_list[0].IsActive = true;
                play($scope.moocfile_list[0]);
                OCMoocVideoInsert_List($scope.moocfile_list[0].ChapterID, $scope.moocfile_list[0].FileID);
                $scope.ChapterID = $scope.moocfile_list[0].ChapterID;
                $scope.FileID = $scope.moocfile_list[0].FileID;
            
        });
    }

    //删除知识点
    $scope.OCMoocVideoInsert_Del = function (id) {
        if (confirm("您确认删除吗?")) {
            var url = MOOCPreviewProviderUrl + "/OCMoocVideoInsert_Del";
            var param = { InsertID: id };
            $scope.baseService.post(url, param, function (data) {
                var node = OCMoocVideoInsertCount(id);
                delete node.InsertID;
            });
        }

    }


    //添加知识点
    $scope.OCMoocVideoInsert_Edit = function (id) {
        if (jwplayer().getState() == "PLAYING") {
            jwplayer().pause();
        }
        if (id != "-1") {
            $('#myModal').modal('show');
            var node = OCMoocVideoInsertCount(id);
            $scope.noteSecond = node.Second;
            $scope.noteTime = $G2S.formatSeconds($scope.noteSecond);
            $scope.txt_Conten = node.Conten;
            $scope.InsertID = node.InsertID;
        } else {
            $scope.noteSecond = parseInt(jwplayer("div_play").getPosition());
            $scope.noteTime = $G2S.formatSeconds($scope.noteSecond);
            $scope.txt_Conten = "";
            $scope.InsertID = "-1";
        }
    }

    //知识点关闭按钮
    $scope.OCMOOCVideoHide = function () {
        if (jwplayer().getState() == "PAUSED") {
            jwplayer().play();
        }
    }

    //保存知识点
    $scope.EditOCMoocVideoInsert = function () {
        var url = MOOCPreviewProviderUrl + "/OCMoocVideoInsert_Edit";
        var param = { InsertID: $scope.InsertID, ChapterID: $scope.ChapterID, FileID: $scope.FileID, Second: $scope.noteSecond, Conten: $scope.txt_Conten };
        $scope.baseService.post(url, param, function (data) {
            if (data.d != null) {
                if (jwplayer().getState() == "PAUSED") {
                    jwplayer().play();
                }
                OCMoocVideoInsert_List($scope.ChapterID, $scope.FileID);
            }
        });
    }

    //获取知识卡详细
    var OCMoocVideoInsertCount = function (id) {
        for (var i = 0; i < $scope.moocvideoinsert_list.length; i++) {
            if (id == $scope.moocvideoinsert_list[i].InsertID) {
                return $scope.moocvideoinsert_list[i];
            }
        }
    }

    //点击视频时间跳转
    $scope.VideoSeek = function (time) {
        jwplayer().seek(time);
    }
    

    //文件点击事件
    $scope.ChapterFileClick = function (thi) {
        for (var i = 0; i < $scope.moocfile_list.length; i++) {
            if ($scope.moocfile_list[i].MoocFileID == thi.moocfile.MoocFileID) {
                $scope.moocfile_list[i].IsActive = true;
                play($scope.moocfile_list[i]);
                OCMoocVideoInsert_List($scope.moocfile_list[i].ChapterID, $scope.moocfile_list[i].FileID);
                $scope.ChapterID = $scope.moocfile_list[i].ChapterID;
                $scope.FileID = $scope.moocfile_list[i].FileID;
            }
            if ($scope.moocfile_list[i].IsActive) {
                $scope.moocfile_list[i].IsActive = false;
            }
        }
        thi.moocfile.IsActive = true;
    }


    //视频/文件播放
    var play = function (filedata) {
        var URL = filedata.ViewUrl;
        if (filedata.FileType == 1) {
            $("#div_play").show();
            var strurl = window.location.href;
            var strsplit = strurl.split("/OC");
            var palyurl = strsplit[0];
            var data = {
                id: "div_play",
                flash: palyurl + "/Frameworks/jwplay/pl.swf",
                url: URL,
                width: "100%",
                height: "630",
                autoplay: "false"
            }
            $G2S.Play(data);
            jwplayer(data.id).play();
            jwplayer(data.id).setMute(false);
        } else {
            $("#div_play").hide();
            jwplayer("div_play").setMute(true);
        }
       
    }


 





    //样式控制
    var cssInit = function () {
        //章节列表
        $('.first_chapter').hover(function () {
            $(this).find('.operation_btn').show();
            $(this).css('background', '#f2f2f2');
        }, function () {
            $(this).find('.operation_btn').hide();
            $(this).css('background', '#fff');
        })
        //是否启用
        $('.open_btn').live('click', function () {
            if (!$(this).hasClass('click')) {
                $(this).addClass('click');
                $(this).css('background-position', '-32px -48px');
            } else {
                $(this).removeClass('click');
                $(this).css('background-position', '-32px -32px');
            }
        })

        //关闭提示文本框
        $('.tip_text span').click(function () {
            $(this).parent().hide();
        })

        $('.icon_zhankai').click(function () {
            $(this).parent().toggle();
            $(this).parent().siblings().toggle();
        })

        

        $('.way1').click(function () {
            $('.recruit_list').eq(0).show();
            $('.recruit_list').eq(1).hide();
        })
        $('.way2').click(function () {
            $('.recruit_list').eq(1).show();
            $('.recruit_list').eq(0).hide();
        })
        //教学计划
        $('.group_discuss a').live('click', function () {
            if (!$(this).hasClass('click')) {
                $(this).addClass('click');
                $(this).parents('.chapt_box').next().slideDown();
                $(this).text('[收起]');
            } else {
                $(this).removeClass('click');
                $(this).parents('.chapt_box').next().slideUp();
                $(this).text('[展开]');
            }
        })

        //预览
        $('.note_list li').hover(function () {
            $(this).addClass('active').siblings().removeClass('active');
        }, function () {
            $(this).removeClass('active');
        })

        $('.item_list li').live('click', function () {
            var num = $(this).index();
            $(this).addClass('active').siblings().removeClass('active');
            $(this).parent().siblings('.detail_info').eq(num).show().siblings('.detail_info').hide();
        })


        $(".wrap").css("background", "none");


       

    }
    var Init = function () {
        cssInit();
        OCMoocFile_List(2);
        

    }
    Init();

}]);

//获取章节详细加载完成后执行
MOOCPreviewModule.directive('onMoocChapter', function ($timeout) {
    return {
        restrict: 'A',
        link: function (scope, element, attr) {
            if (scope.$last === true) {
                $timeout(function () {
                    scope.$emit('ngMoocChapter');
                });
            }
        }
    };
});


//获取资料加载完成后 执行
MOOCPreviewModule.directive('onMoocChapterFile', function ($timeout) {
    return {
        restrict: 'A',
        link: function (scope, element, attr) {
            if (scope.$last === true) {
                $timeout(function () {
                    scope.$emit('ngMoocChapterFile');
                });
            }
        }
    };
});




                      



