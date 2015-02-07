'use strict';

var appResource = angular.module('app.resource.controllers', [
    'app.res.services',
    'app.chapter.services',
    'app.content.services',
    'checklist-model',
    'ui.tree'
]);

appResource.controller('ResourceCtrl', ['$scope', 'resourceService', 'pageService', 'contentService', 'chapterService', 'kenService', function ($scope, resourceService, pageService, contentService, chapterService, kenService) {
    $scope.model = {};
    $scope.fileTypes = [];//文件类型
    $scope.timePass = [];//上传时间
    $scope.shareRanges = []; //使用权限
    $scope.model.FileType = -1;
    $scope.model.timeSelection = -1;
    $scope.model.ShareRange = -1;
    $scope.checksSelect = [];//复选框选中的值
    $scope.folders = [];//文件夹数组

    $scope.folderRelations = [];//文件夹文件数组

    $scope.model.ParentID = 0;//上级ID
    $scope.files = [];//移动文件夹数据

    $scope.delIsShow = false;//删除弹出框
    $scope.moveShow = false;//移动弹出框
    $scope.bgShow = false;//背景置灰

    $scope.folder = {};//文件夹对象

    $scope.propertyShow = false;//资料属性显示

    $scope.chapters = [];//关联章节
    $scope.kens = [];//关联知识点

    $scope.model.timePass = -1;

    $scope.$emit('willResetCourse');

    ///课程切换
    $scope.$on('willCourseChanged', function (event, course) {
        //console.log(course);
        $scope.model.ParentID = 0;
        $scope.model.OCID = course.OCID;
        $scope.model.CourseID = course.CourseID;
        $scope.filterChanged();
        $scope.folderRelation = {};

        $scope.tabTitles.length = 0;
        $scope.tabTitles.push({ id: 0, name: course.Name, order: $scope.order });

    });

    ///课程加载完成
    $scope.$on('courseLoaded', function (course) {
        contentService.OC_Get(function (data) {
            var course = data.d;
            if ($scope.courses.length > 0 && $scope.courses[0].OCID !== 0) {
                course.OCID = 0;
                course.Name = '个人资料';
                $scope.$parent.courses.insert(0, course); 
            }
            $scope.$parent.course = course;

            $scope.model.OCID = course.OCID;
            $scope.model.CourseID = course.CourseID;
            $scope.model.ParentID = 0;
            $scope.filterChanged();

            $scope.tabTitles = [];
            $scope.order = 0;
            $scope.tabTitles.push({ id: 0, name: course.Name, order: $scope.order });
        });
    });

    $scope.tabChange = function (item) {
        $scope.model.ParentID = item.id;
        $scope.filterChanged();

        $scope.folderRelation = {};

        var arr = [];
        for (var i = 0; i < $scope.tabTitles.length; i++) {
            if ($scope.tabTitles[i].order <= item.order) {
                arr.push($scope.tabTitles[i]);
            }
        }
        $scope.tabTitles = arr;
    }

    $scope.typeChanged = function (v) {
        $scope.model.FileType = v;
        $scope.filterChanged();
    }
    $scope.timeChanged = function (v) {
        $scope.model.timePass = v;
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
            $scope.timePass = data.d;
            var item = {};
            angular.copy($scope.timePass[0], item);
            item.id = -1;
            item.name = '不限';
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
        if (item.RelationType == 1) return;
        $scope.model.ParentID = item.Id;
        $scope.folderRelation = item;
        $scope.filterChanged();

        $scope.order += 1;
        $scope.tabTitles.push({ id: item.Id, name: '>>' + item.Name, order: $scope.order });
    }

    //查询
    $scope.filterChanged = function () {
        var folder = angular.copy($scope.model);
        var file = angular.copy($scope.model);
        resourceService.FolderRelation_List(folder, file, function (data) {
            if (data.d.length > 0) {
                $scope.folderRelations = data.d;
            } else {
                $scope.folderRelations = [];
            }
            $scope.checksSelect.length = 0;
        });
    }

    var fileIndex = 0;
    //新建文件
    $scope.AddFile = function () {
        var file = {};
        if ($scope.folderRelation == null) {
            file.FolderID = 0;
        } else {
            file.FolderID = $scope.folderRelation.Id;
        }
        file.OCID = $scope.model.OCID;
        file.CourseID = $scope.model.CourseID;
        file.ShareRange = -1;
        file.FileTitle = '1' + fileIndex.toString();
        file.FileName = '1' + fileIndex.toString();
        file.Ext = fileIndex % 2 === 0 ? 'PPT' : 'PDF';
        file.FileSize = '15';
        file.FileType = fileIndex % 2 === 0 ? 4 : 5;
        file.pingyin = 'pingyin';
        fileIndex++;
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
    $scope.assignShare = function (shareItem) {
        console.log(shareItem);
    }
    //移动文件夹选中数据
    $scope.selectedMove = function (item) {
        $scope.folder = item;
    }

    //移动文件框保存方法
    $scope.moveFileSubmit = function () {
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

                $scope.files = data.d;

                var rootFolder = {};
                angular.copy(data.d[0], rootFolder);
                rootFolder.Id = 0;
                rootFolder.Children.length = 0;
                rootFolder.ParentID = -1;
                rootFolder.Name = '根目录';

                //remove self
                for (var n = 0; n < $scope.checksSelect.length; n++) {
                    for (var i = 0; i < $scope.files.length; i++) {
                        if ($scope.files[i].RelationType == $scope.checksSelect[n].RelationType) {
                            if ($scope.files[i].Id == $scope.checksSelect[n].Id) {
                                $scope.files.splice(i, 1);
                            } else {
                                mobilesDel($scope.files[i].Children, $scope.checksSelect[n].Id)
                            }
                        }
                    }
                }

                var length = $scope.files.length;
                for (var i = 0; i < length; i++) {
                    if ($scope.files[i].ParentID == rootFolder.Id) {
                        rootFolder.Children.push($scope.files[i]);
                    }
                }
                $scope.files.length = 0;
                $scope.files.push(rootFolder);
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
        $scope.checksSelect.length = 0;
        $scope.moveShow = true;
        $scope.bgShow = true;
        $scope.checksSelect.push(item);
        $scope.mobileFolder(item);
    }
    //移动文件弹出框
    $scope.fireMobileBatch = function () {
        if ($scope.checksSelect.length == 0) return;
        $scope.moveShow = true;
        $scope.bgShow = true;
        $scope.mobileFolder();
    }

    //资料属性弹出框
    $scope.fileProperty = function (item) {
        getProperty();
        getKen();
        $scope.propertyShow = true;
        $scope.bgShow = true;
        $scope.file = item;

    }
    //关联知识点和章节保存
    $scope.fileChapterKen = function () {
        var file = { FileID: $scope.file.Id };
        var chapter = { ChapterID: $scope.chapter.ChapterID };
        var ken = { KenID: $scope.ken.KenID };
        resourceService.File_Chapter_Ken_Edit(file, chapter, ken, function (data) {
            if (data.d) {
                $scope.close();
            }
        });
    }

    //获取关联章节数据
    var getProperty = function () {
        var model = { OCID: $scope.model.OCID };
        chapterService.Chapter_List(model, function (data) {
            if (data.d.length > 0) {
                $scope.chapters = data.d;
                $scope.chapter = $scope.chapters[0];
            }
        });
    }
    //获取关联知识点数据
    var getKen = function () {
        var model = { OCID: $scope.model.OCID };
        kenService.Ken_List(model, function (data) {
            if (data.d.length > 0) {
                $scope.kens = data.d;
                $scope.ken = $scope.kens[0];
            }
        });
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
        $scope.moveShow = false;
        $scope.propertyShow = false;
        $scope.folder = {};
    }
    //批量设置使用权限
    $scope.batchShareRange = function (item) {
        var FileIDS = '';
        for (var i = 0; i < $scope.checksSelect.length; i++) {
            if ($scope.checksSelect[i].RelationType == 1) {
                FileIDS += $scope.checksSelect[i].Id + ',';
            }
        }
        FileIDS = FileIDS.substr(0, FileIDS.length - 1);
        if (FileIDS != '') {
            resourceService.File_Batch_ShareRange(FileIDS, item.id, function (data) {
                if (data.d) {
                    $scope.filterChanged();
                }
            });
        }
    }
    $scope.fileShareRange = function (item, sr) {
        console.log(item.Id);
        console.log(sr.id);
        var model = { FileID: item.Id, ShareRange: sr.id };
        resourceService.File_ShareRange_Upd(model, function (data) {

        });
    }

    $scope.visible = function (item) {
        if ($scope.query && $scope.query.length > 0
          && item.title.indexOf($scope.query) == -1) {
            return false;
        }
        return true;
    };

    //弹出右键菜单
    $('.more_operation').hover(function () {
        $(this).find('.mouse_right').toggle();
    });
    //右键菜单表现形式
    $('.mouse_right li').hover(function () {
        $(this).addClass('active').siblings().removeClass('active');
        $(this).find('.right_obj').show();
    }, function () {
        $(this).removeClass('active');
        $(this).find('.right_obj').hide();
    });
    $('#youlan').hover(function () {
        $('.permissions').show();
    }, function () {
        //elem.find('.permissions').hide();
    });
    $('#eShare').click(function () {
        //换图片
    })
    $('.permissions li').hover(function () {
        $(this).addClass('current').siblings().removeClass('current');
    }, function () {
        $(this).removeClass('current');
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


}]);
