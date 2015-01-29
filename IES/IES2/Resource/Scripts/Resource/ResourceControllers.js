'use strict';

var appResource = angular.module('app.resource.controllers', [
    'app.res.services',
    'app.content.services',
    'checklist-model',
    'ui.tree'
]);

appResource.controller('ResourceCtrl', ['$scope', 'resourceService', 'pageService', 'contentService', function ($scope, resourceService, pageService, contentService) {
    $scope.model = {};
    $scope.fileTypes = [];//文件类型
    $scope.timePass = [];//上传时间
    $scope.shareRanges = []; //使用权限
    $scope.model.FileType = -1;
    $scope.model.timeSelection = -1;
    $scope.model.ShareRange = -1;
    $scope.checksSelect = [];//复选框选中的值
    $scope.folders = [];//文件夹数组
    $scope.files = [];//文件数组

    $scope.folderRelations = [];//文件夹文件数组

    $scope.model.ParentID = 0;//上级ID
    $scope.mobiles = [];//移动文件夹数据

    $scope.delIsShow = false;//删除弹出框
    $scope.mobileIsShow = false;//移动弹出框
    $scope.bgShow = false;//背景置灰

    $scope.folder = {};//文件夹对象

    $scope.fileShow = false;//是否显示

    var buildPersonal = function () {
        contentService.OC_Get(function (data) {
            if ($scope.$parent.courses.length === 0 ||
                $scope.$parent.courses[0].OCID !== -1) {
                var course = data.d;
                course.OCID = -1;
                course.Name = '个人资料';
                $scope.$parent.courses.insert(0, course);
                $scope.$parent.course = course;
                $scope.model.OCID = course.OCID;
                $scope.model.CourseID = course.CourseID;
                $scope.filterChanged();
            }
        });
    }

    ///添加个人资料 OCID=-1
    buildPersonal();

    ///课程切换
    $scope.$on('willCourseChanged', function (event, course) {
        //console.log(course);
        $scope.model.ParentID = 0;
        $scope.model.OCID = course.OCID;
        $scope.model.CourseID = course.CourseID;
    });
    ///课程加载完成
    $scope.$on('courseLoaded', function (course) {
        buildPersonal(); 
    });



    $scope.typeChanged = function (v) {
        $scope.model.FileType = v;
        $scope.filterChanged();
    }
    $scope.timeChanged = function (v) {
        $scope.model.timeSelection = v;
        $scope.filterChanged();
    }
    $scope.shareChanged = function (v) {
        $scope.model.ShareRange = v;
        $scope.filterChanged();
    }



    //文件类型初始化
    resourceService.Resource_Dict_FileType_Get(function (data) {
        if (data.d) {
            var item = {};
            angular.copy(data.d[0], item);
            item.id = -1;
            item.name = '不限';
            $scope.fileTypes = data.d;
            $scope.fileTypes.insert(0, item);
        }
    });
    //上传时间初始化
    resourceService.Resource_Dict_TimePass_Get(function (data) {
        if (data.d) {
            var item = {};
            angular.copy(data.d[0], item);
            item.id = -1;
            item.name = '不限';
            $scope.timePass = data.d;
            $scope.timePass.insert(0, item);
        }
    });
    //使用权限初始化
    resourceService.Resource_Dict_ShareRange_Get(function (data) {
        if (data.d) {
            var item = {};
            angular.copy(data.d[0], item);
            item.id = -1;
            item.name = '不限';
            $scope.shareRanges = data.d;
            $scope.shareRanges.insert(0, item);
        }
    });


    //复选框值
    $scope.checkAdd = function (file) {
        if ($.inArray(file, $scope.checksSelect) < 0) {
            $scope.checksSelect.push(file);
        } else {
            $scope.checksSelect.splice($.inArray(file, $scope.checksSelect), 1);
        }
    }
    //全选
    $scope.checkALL = function () {

    }
    //单击文件夹名称方法,进入文件夹
    $scope.folderClick = function (item) {
        $scope.model.ParentID = item.Id;
        $scope.folderRelation = item;
        $scope.filterChanged();
    }

    //查询
    $scope.filterChanged = function () {
        resourceService.FolderRelation_List($scope.model, $scope.model, function (data) {
            if (data.d.length > 0) {
                $scope.folderRelations = data.d;
            } else {
                $scope.folderRelations = [];
            }
        });
        //resourceService.Folder_List($scope.model, function (data) {
        //    if (data.d.length > 0) {
        //        $scope.folders = data.d;
        //    }
        //    console.log(data.d);
        //});
        //resourceService.File_Search($scope.model, function (data) {
        //    if (data.d.length > 0) {
        //        $scope.files = data.d;
        //    }
        //});
    }

    //新建文件
    $scope.AddFile = function () {
        if ($scope.folderRelation == null) return;
        var file = {};
        file.FolderID = $scope.folderRelation.Id;
        file.OCID = $scope.folderRelation.OCID;
        file.CourseID = $scope.folderRelation.CourseID;
        file.ShareRange = -1;
        file.FileTitle = '1';
        file.FileName = '1';
        file.Ext = 'txt';
        file.FileSize = '15';
        file.pingyin = 'pingyin';
        resourceService.File_ADD(file, function (data) {
            if (data.d) {
                $scope.filterChanged();
            }
        });
    }

    $scope.updFolderName = function (item) {
        if (item.RelationType == 1) {
            var file = { FileID: item.Id, FileTitle: item.Name };
            resourceService.File_FileTitle_Upd(file, function (data) {
                if (data.d) {
                    $scope.filterChanged();
                }
            });
        }
        else {
            //新建
            if (item.FolderID == 0) {
                var folder = { FolderName: item.Name, ParentID: $scope.model.ParentID, OCID: item.OCID };
                resourceService.Folder_ADD(folder, function (data) {
                    if (data.d) {
                        $scope.filterChanged();
                    }
                });
            }
            else {//修改            
                var folder = { FolderName: item.Name, FolderID: item.Id };
                resourceService.Folder_Name_Upd(folder, function (data) {
                    if (data.d) {
                        $scope.filterChanged();
                    }
                });
            }
        }
    }

    //新建文件夹
    $scope.AddFolder = function () {
        resourceService.Folder_Get(function (data) {
            var folder = data.d;
            folder.Name = 'NewFolder';
            folder.ParentID = $scope.model.ParentID;
            folder.OCID = $scope.model.OCID;
            folder.folderRelation = 0;
            $scope.folderRelations.push(folder);
        });
    }

    //返回上一层
    $scope.returnPage = function (item) {
        if (item) {
            var folder = { FolderID: item.ParentID };
            resourceService.Folder_GetModel(folder, function (data) {
                if (data.d) {
                    $scope.model.ParentID = data.d.ParentID;
                    $scope.filterChanged();
                }
            });
        }
    }

    //移动文件夹选中数据
    $scope.selectedMobile = function (item) {
        $scope.folder = item;
    }

    //移动文件框保存方法
    $scope.updFolderMobile = function () {
        for (var i = 0; i < $scope.checksSelect.length; i++) {
            if ($scope.checksSelect[i].RelationType == 0) {
                var folder = { FolderID: $scope.checksSelect[i].Id, ParentID: $scope.folder.Id };
                resourceService.Folder_ParentID_Upd(folder, function (data) {
                    if (data.d) {
                        $scope.filterChanged();
                        $scope.close();
                    }
                });
            } else {
                var file = { FolderID: $scope.folder.Id, FileID: $scope.checksSelect[i].Id };
                resourceService.File_FolderID_Upd(file, function (data) {
                    if (data.d) {
                        $scope.filterChanged();
                        $scope.close();
                    }
                });
            }
        }        
    }


    //获取移动列表
    $scope.mobileFolder = function (item) {
        var folder = { OCID: $scope.model.OCID, RelationType: 'Folder' }
        //var folder = { ParentID: -1 }
        if ($scope.checksSelect.length == 0) {
            $scope.checksSelect.push(item);
        }
        resourceService.FolderRelation_List(folder, null, function (data) {
            if (data.d) {
                $scope.mobiles = data.d;
                //$scope.model.FolderID = $scope.mobiles[0].FolderID;
                for (var n = 0; n < $scope.checksSelect.length; n++) {
                    for (var i = 0; i < $scope.mobiles.length; i++) {                       
                        if ($scope.mobiles[i].RelationType == $scope.checksSelect[n].RelationType) {
                            if ($scope.mobiles[i].Id == $scope.checksSelect[n].Id) {
                                $scope.mobiles.splice(i, 1);
                            } else {
                                mobilesDel($scope.mobiles[i].Children, $scope.checksSelect[n].Id)
                            }
                        }
                    }
                }                
            }
        });
    }
    //移动数据循环删除子项
    var mobilesDel = function (Children, item) {
        for (var n = 0; n < Children.length; n++) {
            if (Children[n].Id == item) {
                Children.splice(n, 1);
            } else {
                mobilesDel(Children[n].Children, item);
            }
        }
    }


    //文件夹移动按钮
    //$scope.mobileClick = function () {
    //    var folder = { FolderID: $scope.FolderID, ParentID: $scope.ParentID.FolderID }
    //    resourceService.Folder_ParentID_Upd(folder, function (data) {
    //        if (data.d) {
    //            $scope.filterChanged();
    //            //关闭弹出层
    //            $('.pop_bg,.pop_400').hide();
    //        }
    //    });
    //}
    
    $scope.fireProperty = function () {
        console.log('fireProperty');
    }
    //删除文件夹弹出框
    $scope.fireRemove = function (item) {
        $scope.delIsShow = true;
        $scope.bgShow = true;
        $scope.folder = item;
    }
    //批量删除文件夹弹出框
    $scope.fireRemoveAll = function () {
        if ($scope.checksSelect.length == 0) return;
        $scope.delIsShow = true;
        $scope.bgShow = true;

    }
    //移动文件弹出框
    $scope.fireMobile = function (item) {
        $scope.mobileIsShow = true;
        $scope.bgShow = true;
        $scope.mobileFolder(item);
    }
    //移动文件弹出框
    $scope.fireMobileBatch = function () {
        if ($scope.checksSelect.length == 0) return;
        $scope.mobileIsShow = true;
        $scope.bgShow = true;        
        $scope.mobileFolder();
    }


    //删除文件夹弹出框点击确定按钮
    $scope.folderDelClick = function () {
        if ($scope.checksSelect.length == 0) {
            $scope.checksSelect.push($scope.folder);
        }
        for (var i = 0; i < $scope.checksSelect.length; i++) {
            if ($scope.checksSelect[i].RelationType == 0) {
                var folder = { FolderID: $scope.checksSelect[i].Id };
                resourceService.Folder_Del(folder, function (data) {
                    if (data.d) {
                        $scope.filterChanged();
                        $scope.close();
                    }
                });
            } else {
                var file = { FileID: $scope.checksSelect[i].Id };
                resourceService.File_Del(file, function (data) {
                    if (data.d) {
                        $scope.filterChanged();
                        $scope.close();
                    }
                });
            }
        }
        
    }

    //删除文件夹
    $scope.delFolder = function (item) {
        var folder = { FolderID: item.FolderID };
        resourceService.Folder_Del(folder, function (data) {
            if (data.d) {
                $scope.filterChanged();
            }
        });
    }
    //批量删除文件夹
    $scope.delALLFolder = function () {
        if ($scope.checksSelect.length > 0) {
            var length = $scope.checksSelect.length;
            for (var i = 0; i < length; i++) {
                var folder = { FolderID: $scope.checksSelect[i].FolderID };
                resourceService.Folder_Del(folder, function (data) {
                    if (data.d)
                        $scope.filterChanged();
                });
            }

        }
    }

    //关闭删除文件夹弹出框
    $scope.close = function () {
        $scope.delIsShow = false;
        $scope.bgShow = false;
        $scope.mobileIsShow = false;
        $scope.folder = {};
    }



    $scope.visible = function (item) {
        if ($scope.query && $scope.query.length > 0
          && item.title.indexOf($scope.query) == -1) {
            return false;
        }
        return true;
    };

    $scope.$on('ngRepeatFinished', function (ngRepeatFinishedEvent) {
        //下面是在table render完成后执行的js
        $('.more_operation').hover(function () {
            $(this).find('.mouse_right').toggle();
        })
        $('.mouse_right li').hover(function () {
            $(this).addClass('active').siblings().removeClass('active');
            $(this).find('.right_obj').show();
        }, function () {
            $(this).removeClass('active');
            $(this).find('.right_obj').hide();
        })
        $('.right_obj li').hover(function () {
            $(this).addClass('current').siblings().removeClass('current');
        }, function () {
            $(this).removeClass('current');
        })
        //试卷名称重命名表现形式
        $('.data_tit').live('dblclick', function () {
            $(this).hide();
            $(this).next().show().select();
        })
        //弹出层方法
        function tanchu(popbox) {
            var oHeight = $(document).height();
            var oScroll = $(window).scrollTop();
            $('.pop_bg').show().css('height', oHeight);
            popbox.show().css('top', oScroll + 200);
        }
        //资料属性
        $('.attribute').live('click', function () {
            tanchu($('.pop_400'))
        })

        $('.file_list').each(function () {
            $(this).find('tr:even').css('background', '#f2f2f2');
        })

        $('.file_list tr').hover(function () {
            $(this).addClass('active').siblings().removeClass('active');
        }, function () {
            $(this).removeClass('active');
        })

        $('.batch_list li').hover(function () {
            $(this).addClass('active').siblings().removeClass('active');
        }, function () {
            $(this).removeClass('active');
        })
        $('.batch_list li').hover(function () {
            $(this).addClass('active').siblings().removeClass('active');
        }, function () {
            $(this).removeClass('active');
        })
        $('.permissions li').hover(function () {
            $(this).addClass('current').siblings().removeClass('current');
        }, function () {
            $(this).removeClass('current');
        })

        $('.select_box').live('click', function () {
            if (!$(this).hasClass('click')) {
                $(this).addClass('click');
                $('.folder_list').show();
            } else {
                $(this).removeClass('click');
                $('.folder_list').hide();
            }

        })

    });


    $scope.onRepeatFinished = function (onRepeatFinishedEvent) {

        $('.select_box').live('click', function () {
            if (!$(this).hasClass('click')) {
                $(this).addClass('click');
                $('.folder_list').show();
            } else {
                $(this).removeClass('click');
                $('.folder_list').hide();
            }
        });

        $('.folder_list li').hover(function () {
            $(this).addClass('active').siblings().removeClass('active');
        }, function () {
            $(this).removeClass('active');
        });

        //我的资源首页批量删除
        $('.batch_delete').live('click', function () {
            tanchu($('.pop_400').eq(2));
        });
    }
}]);
