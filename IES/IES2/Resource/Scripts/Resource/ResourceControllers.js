'use strict';

var appResource = angular.module('app.resource.controllers', [
    'app.res.services',
    'app.content.services'
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

    $scope.model.ParentID = 0;//上级ID
    $scope.mobiles = [];//移动文件夹数据    


    $scope.fileShow = false;//是否显示

    $scope.$on('courseLoaded', function () {
        contentService.OC_Get(function (data) {
            var course = data.d;
            course.OCID = -1;
            course.Name = '个人资料';
            $scope.$parent.courses.insert(0, course);
            $scope.$parent.currentCourse = course;
        });
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

    var load = function () {
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
        if (file) {
            $scope.checksSelect.push(file);
        }
    }
    //全选
    $scope.checkALL = function () {

    }
    //单击文件夹名称方法,进入文件夹
    $scope.folderClick = function (item) {
        $scope.model.ParentID = item.FolderID;

        $scope.filterChanged();
    }

    //查询
    $scope.filterChanged = function () {
        resourceService.Folder_List($scope.model, function (data) {
            if (data.d) {
                $scope.folders = data.d;
            }
        });
        resourceService.File_Search($scope.model, function (data) {
            if (data.d) {
                $scope.files = data.d;
            }
        });
    }
    $scope.updFolderName = function (item) {
        console.log('updFolderName', item);
        //新建
        if (item.FolderID == 0) {
            var folder = { FolderName: item.FolderName, ParentID: $scope.model.ParentID };
            resourceService.Folder_ADD(folder, function (data) {
                if (data.d) {
                    $scope.filterChanged();
                }
            });
        }
        else {//修改            
            var folder = { FolderName: item.FolderName, FolderID: item.FolderID };
            resourceService.Folder_Name_Upd(folder, function (data) {
                if (data.d) {
                    $scope.filterChanged();
                }
            });
        }
    }

    //新建文件夹
    $scope.AddFolder = function () {
        resourceService.Folder_Get(function (data) {
            var folder = data.d;
            folder.FolderName = 'NewFolder';
            folder.ParentID = $scope.model.ParentID;
            $scope.folders.push(folder);
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

    //获取移动列表
    $scope.mobileFolder = function (item) {
        if (item) {
            var folder = { ParentID: -1 }
            $scope.FolderID = item.FolderID;
            resourceService.Folder_List(folder, function (data) {
                if (data.d) {
                    $scope.mobiles = data.d;
                    $scope.model.FolderID = $scope.mobiles[0].FolderID;
                }
            });
        }
    }
    //文件夹移动按钮
    $scope.mobileClick = function () {
        var folder = { FolderID: $scope.FolderID, ParentID: $scope.ParentID.FolderID }
        resourceService.Folder_ParentID_Upd(folder, function (data) {
            if (data.d) {
                $scope.filterChanged();
                //关闭弹出层
                $('.pop_bg,.pop_400').hide();
            }
        });
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

    load();

    $scope.fireProperty = function () {
        console.log('fireProperty');
    }

    $scope.fireRemove = function (item) {
        console.log(item);
    }

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
