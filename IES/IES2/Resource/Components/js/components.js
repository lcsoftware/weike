'use strict';

var app = angular.module('app.custom.directives', ['app.assist.services', 'ui.tree']);


app.directive('moreCourse', function () {
    var directive = {};

    directive.restrict = 'EA';

    directive.replace = true;

    directive.scope = {
        courses: '=',
        courseMore: '=',
        course: '='
    }

    directive.templateUrl = '/Components/templates/moreCourse.html';

    directive.link = function (scope, elem, iAttrs) {
        //查看更多
        elem.find('li').hover(function () {
            var len = $('.second_nav').length;
            if (len > 0) {
                $(this).find('i').addClass('slide_up');
                $(this).find('.second_nav').show();
            }
        }, function () {
            $(this).find('i').removeClass('slide_up');
            $(this).find('.second_nav').hide();
        })
    }

    directive.controller = function ($scope) {
        $scope.courseChange = function (course) {
            $scope.$emit('onWillCourseChanged', course);
        }
    }
    return directive;
});

app.directive('fileOperation', function () {
    var directive = {};

    directive.restrict = 'EA';

    directive.replace = true;

    directive.scope = {
        onProperty: '&',
        onRename: '&',
        onDownload: '&',
        onVideo: '&',
        onRemove: '&',
        onReturnPage: '&',
        onMobile: '&',
        shareRanges: '=',
        folderRelation: '=',
        ocid: '='
    }

    directive.templateUrl = '/Components/templates/fileOperation.html';

    directive.link = function (scope, elem, iAttrs) {
        scope.rangeSelection = {};

        scope.shareRange = function (range) {
            scope.rangeSelection = range;
            scope.$emit('shareRange', scope.rangeSelection, scope.folderRelation);
        }

        //弹出右键菜单
        elem.parent().hover(function () {
            $(this).find('.mouse_right').toggle();
        });

        //右键菜单表现形式
        elem.parent().find('.mouse_right li').hover(function () {
            $(this).addClass('active').siblings().removeClass('active');
            $(this).find('.right_obj').show();
        }, function () {
            $(this).removeClass('active');
            $(this).find('.right_obj').hide();
        });

        elem.find('#eBrowser').hover(function () {
            elem.find('.batch_list2').show();
        });
    }

    return directive;
});

app.directive('fileProperty', function () {
    var directive = {};

    directive.restrict = 'EA';

    directive.scope = {
        chapter: '=',
        chapters: '=',
        ken: '=',
        kens: '=',
    }

    directive.templateUrl = '/Components/templates/fileProperty.html';

    directive.link = function (scope, elem, iAttrs) {
        elem.find('.save,.cancel,.close_pop').bind('click', function () {
            elem.hide();
        });
    }

    directive.controller = function ($scope) {

        $scope.onPropertySave = function (chapter, ken) {
            $scope.$emit('onPerpertySave', chapter, ken);
        }
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
        requireMent: '=',
        requireMents: '=',
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
        chapterName: '=',
        knowledge: '=',
        knowledges: '=',
        requireMent: '=',
        requireMents: '=',
        onSaveNew: '&',
        onSave: '&',
        onCancel: '&'
    }

    directive.replace = true;

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
        folderName: '=',
        folderExt: '=',
        folderItem: '='
    }

    directive.templateUrl = '/Components/templates/folder.html';

    directive.link = function (scope, elem, iAttrs) {
        //重命名表现形式
        var e = elem.find('.data_tit');
        e.bind('dblclick', function (e) {
            $(this).hide();
            $(this).next().show().select();
        });
    }

    //directive.controller = function ($scope, $element) {
    //    var e = elem.find('.data_tit');
    //    $scope.$on('onNewFolder', function (event) {
    //    });
    //}

    return directive;
});

app.directive('batchOperation', function () {
    var directive = {};

    directive.restrict = 'EA';

    directive.scope = {
        onFireRemoveAll: '&',
        onFireMobileBatch: '&',
        shareRanges: '='
    }

    directive.templateUrl = '/Components/templates/batchOperation.html';

    directive.link = function (scope, elem, iAttrs) {
        scope.shareRange = function (range) {
            scope.rangeSelection = range;
            scope.$emit('batchShareRange', scope.rangeSelection);
        }

        elem.find('.batch_list li').hover(function () {
            $(this).addClass('active').siblings().removeClass('active');
        }, function () {
            $(this).removeClass('active');
        });

        elem.find('.permissions li').hover(function () {
            $(this).addClass('current').siblings().removeClass('current');
        }, function () {
            $(this).removeClass('current');
        });
    }

    return directive;
});

app.directive('exerciseBatch', function () {
    var directive = {};

    directive.restrict = 'EA';

    directive.scope = {
        checks: '=',
        shareRanges: '=',
        difficults: '='
    }

    directive.templateUrl = '/Components/templates/exerciseBatch.html';

    directive.link = function (scope, elem, iAttrs) {
        elem.find('.batch_list li').hover(function () {
            $(this).addClass('active').siblings().removeClass('active');
        }, function () {
            $(this).removeClass('active');
        });

        elem.find('.permissions li').hover(function () {
            $(this).addClass('active');
        }, function () {
            $(this).removeClass('active');
        });

        elem.find('.permissions li').hover(function () {
            $(this).addClass('current').siblings().removeClass('current');
        }, function () {
            $(this).removeClass('current');
        });
    }

    directive.controller = function ($scope) {

        $scope.batchShareRange = function (item) {
            $scope.$emit('onBatchShareRange', item);
        }

        $scope.batchDifficult = function (item) {
            $scope.$emit('onBatchDifficult', item);
        }

        $scope.batchRemove = function () {
            $scope.$emit('onBatchRemove');
        }
    }

    return directive;
});


app.directive('exerciseList', ['assistService', function (assistService) {
    var directive = {};

    directive.restrict = 'EA';

    directive.replace = true;

    directive.scope = {
        exercise: '=',
        checks: '=',
        shareRanges: '='
    }

    directive.templateUrl = '/Components/templates/exerciseList.html';

    directive.link = function (scope, elem, iAttrs) {
        elem.hover(function () { $(this).find('.topic_icon').show(); },
                   function () { $(this).find('.topic_icon').hide(); }
                  );
        elem.find('.icon.share_topic,.icon.delete_topic,.icon.edit_topic').hover(
            function () { $(this).find('.icon_content').show(); },
            function () { $(this).find('.icon_content').hide(); }
            );
        elem.find('.spread').bind('click', function () {
            if (!elem.hasClass('show')) {
                elem.addClass('show');
            } else {
                elem.removeClass('show');
            }
        })
    }

    directive.controller = function ($scope, assistService) {

        $scope.editExercise = function (exercise) {
            $scope.$emit('onEditExercise', exercise);
        }
        $scope.shareExercise = function (exercise, range) {
            $scope.$emit('onShareExercise', exercise, range);
        }
        $scope.deleteExercise = function (exercise) {
            $scope.$emit('onDeleteExercise', exercise);
        }
        $scope.getDifficultName = function (exercise) {
            var name = '';
            assistService.Resource_Dict_Diffcult_Get(function (data) {
                if (data.length > 0) {
                    var length = data.length;
                    for (var i = 0; i < length; i++) {
                        if (data[i].id == exercise.Diffcult) {
                            name = data[i].name;
                            break;
                        }
                    }
                }
            });
            return name;
        }

        $scope.getShareRange = function (exercise) {
            var name = '';
            assistService.Resource_Dict_ShareRange_Get(function (data) {
                if (data.length > 0) {
                    var length = data.length;
                    for (var i = 0; i < length; i++) {
                        if (data[i].id == exercise.ShareRange) {
                            name = data[i].name;
                            break;
                        }
                    }
                }
            });
            return name;
        }

        $scope.getExerciseType = function (exercise) {
            var name = '';
            assistService.Resource_Dict_ExerciseType_Get(function (data) {
                if (data.length > 0) {
                    var length = data.length;
                    for (var i = 0; i < length; i++) {
                        if (data[i].id == exercise.ExerciseType) {
                            name = data[i].name;
                            break;
                        }
                    }
                }
            });
            return name;
        }
    }

    return directive;
}]);

//移动文件
app.directive('moveFolder', function () {
    var directive = {};

    directive.restrict = 'EA';

    directive.scope = {
        onMoveFileSubmit: '&',
        onClose: '&',
        onSelectedMove: '&',
        files: '='
    }

    directive.templateUrl = '/Components/templates/moveFolder.html';

    directive.link = function (scope, elem, iAttrs) {
    }

    return directive;
});

//编辑器
app.directive('editor', function () {
    var directive = {};

    directive.restrict = 'EA';

    directive.scope = {
        editorText: '=',
        editorId: '@'
    }

    directive.templateUrl = '/Components/templates/editor.html';

    directive.link = function (scope, elem, iAttrs) {
    }

    return directive;
});

//文件上传
app.directive('iesFileUploader', ['FileUploader', function (FileUploader) {
    var directive = {};

    directive.restrict = 'EA';

    directive.scope = {
        uploadUrl: '@',
        ocid: '=',
        courseId: '=',
        folderObj: '='
    }

    directive.templateUrl = '/Components/templates/iesFileUploader.html';

    directive.link = function (scope, elem, iAttrs) {
        elem.find('.close_pop').bind('click', function () {
            elem.hide();
        }); 
    }

    directive.controller = function ($scope, $element) {
        //----------上传文件start--------
        var angularFileUploader = $scope.iesUploader = new FileUploader({ url: $scope.uploadUrl });

        //angularFileUploader.formData.push({ OCID: $scope.ocid });
        //angularFileUploader.formData.push({ CourseID: $scope.courseId});
        //if ($scope.folderObj) {
        //    angularFileUploader.formData.push({ FolderID: $scope.folderObj.Id });
        //} else {
        //    angularFileUploader.formData.push({ FolderID: 0 }); 
        //}
        //angularFileUploader.formData.push({ ShareRange: 2 });

        $scope.$watch('ocid', function (v) {
            angularFileUploader.formData.length = 0;
            angularFileUploader.formData.push({ OCID: v });
            angularFileUploader.formData.push({ CourseID: $scope.courseId });
            if ($scope.folderObj) {
                angularFileUploader.formData.push({ FolderID: $scope.folderObj.Id });
            } else {
                angularFileUploader.formData.push({ FolderID: 0 });
            }
            angularFileUploader.formData.push({ ShareRange: 2 });
        });

        $scope.$watch('courseId', function (v) {
            angularFileUploader.formData.length = 0;
            angularFileUploader.formData.push({ OCID: $scope.ocid });
            angularFileUploader.formData.push({ CourseID: v });
            if ($scope.folderObj) {
                angularFileUploader.formData.push({ FolderID: $scope.folderObj.Id });
            } else {
                angularFileUploader.formData.push({ FolderID: 0 });
            }
            angularFileUploader.formData.push({ ShareRange: 2 });
        });

        $scope.$watch('folderObj', function (v) {
            if (v) {
                angularFileUploader.formData.length = 0;
                angularFileUploader.formData.push({ OCID: $scope.ocid });
                angularFileUploader.formData.push({ CourseID: $scope.courseId });
                angularFileUploader.formData.push({ FolderID: v.Id });
                angularFileUploader.formData.push({ ShareRange: 2 });
            }
        });
        // FILTERS
        angularFileUploader.filters.push({
            name: 'customFilter',
            fn: function (item /*{File|FileLikeObject}*/, options) {
                return this.queue.length < 10;
            }
        });

        angularFileUploader.filters.push({
            name: 'fileSuffix',//过滤器名称 过滤文件类型
            fn: function (item, options) {
                var fileName = item.name;
                var suffix = fileName.substring(fileName.lastIndexOf('.'), fileName.length);
                //return suffix == ".txt" || suffix == ".jpg" || suffix == ".gif";
                return true;
            }
        });

        // CALLBACKS 
        angularFileUploader.onWhenAddingFileFailed = function (item /*{File|FileLikeObject}*/, filter, options) {
            $scope.$emit('onWhenAddingFileFailed', item, filter, options);
        };
        angularFileUploader.onAfterAddingFile = function (fileItem) {
            angularFileUploader.url = '/DataProvider/FileUpload.ashx/?FROM=1';
            $scope.$emit('onAfterAddingFile', fileItem);
        };
        angularFileUploader.onAfterAddingAll = function (addedFileItems) {
            $scope.$emit('onAfterAddingAll', addedFileItems);
        };
        angularFileUploader.onBeforeUploadItem = function (item) {
            $scope.$emit('onBeforeUploadItem', item);
        };
        angularFileUploader.onProgressItem = function (fileItem, progress) {
            $scope.$emit('onProgressItem', fileItem, progress);
        };
        angularFileUploader.onProgressAll = function (progress) {
            $scope.$emit('onProgressAll', progress);
        };
        angularFileUploader.onSuccessItem = function (fileItem, response, status, headers) {
            $scope.$emit('onSuccessItem', fileItem, response, status, headers);
        };
        angularFileUploader.onErrorItem = function (fileItem, response, status, headers) {
            $scope.$emit('onErrorItem', fileItem, response, status, headers);
        };
        angularFileUploader.onCancelItem = function (fileItem, response, status, headers) {
            $scope.$emit('onCancelItem', fileItem, response, status, headers);
        };
        angularFileUploader.onCompleteItem = function (fileItem, response, status, headers) {
            $scope.$emit('onCompleteItem', fileItem, response, status, headers);
        };
        angularFileUploader.onCompleteAll = function () {
            angularFileUploader.clearQueue();
            $scope.$emit('onCompleteAll');
            $element.hide();
        };
    }

    return directive;
}]);


app.directive('attachList', function () {
    var directive = {};

    directive.restrict = 'EA';

    directive.replace = true;

    directive.templateUrl = '/Components/templates/attachmentList.html';

    directive.scope = {
        attachments: '='
    }

    directive.link = function (scope, elem, iAttrs) {
        elem.find('.close_pop').bind('click', function () {
            elem.hide();
        });
    }

    directive.controller = function ($scope) {
        $scope.remove = function (attachment) {
            $scope.$emit('onRemoveAttachment', attachment);
        }
    }

    return directive;
});

app.directive('exercisePreview', function () {
    var directive = {};

    directive.restrict = 'EA';

    directive.replace = true;

    directive.templateUrl = '/Components/templates/preview.html';

    directive.scope = {
        exercise: '='
    }

    directive.link = function (scope, elem, iAttrs) {
        elem.find('.save').bind('click', function () {
            elem.hide();
        });
    }

    return directive;
});


app.directive('layPage', function () {
    var directive = {};

    directive.restrict = 'EA';

    directive.replace = true;

    directive.scope = {
        PagesNum: '@'
    }

    directive.link = function (scope, elem, iAttrs) {
        //laypage({
        //    cont: $('#pager'), //容器。值支持id名、原生dom对象，jquery对象, 'page'/document.getElementById('page')/$('#page')
        //    pages: directive.scope.PagesNum, //总页数
        //    skip: true, //是否开启跳页
        //    skin: '#374760', //选中的颜色
        //    groups: 3,//连续显示分页数
        //    first: '首页', //若不显示，设置false即可
        //    last: '尾页', //若不显示，设置false即可
        //    jump: function (e) { //触发分页后的回调
        //        //$scope.PageIndex = e.curr;
        //        //GetClassList();
        //        console.log(e);
        //    }
        //});
    }

    directive.controller = function ($scope) {

        $scope.pageIndex = 1;
        $scope.cont = '';
        $scope.PagesNum = 100;
        $scope.skip = true;
        $scope.pickColor = '#374760';
        $scope.groups = 3;
        $scope.first = '首页';
        $scope.last = '尾页';

        console.log($scope.PagesNum);

        laypage({
            cont: $('#pager'), //容器。值支持id名、原生dom对象，jquery对象, 'page'/document.getElementById('page')/$('#page')
            pages: $scope.PagesNum, //总页数
            skip: $scope.skip, //是否开启跳页
            skin: $scope.pickColor, //选中的颜色
            groups: $scope.groups,//连续显示分页数
            first: $scope.first, //若不显示，设置false即可
            last: $scope.last, //若不显示，设置false即可
            jump: function (e) { //触发分页后的回调
                $scope.PageIndex = e.curr;
                //GetClassList();

            }
        });
    }

    return directive;
});


app.directive('iesDialog', function () {
    var directive = {};

    directive.restrict = 'EA'; 

    directive.templateUrl = '/Components/templates/confirm.html';

    directive.scope = {
        dialogTitle: '@',
        dialogText: '@',
        okTitle: '@',
        cancelTitle: '@'
    }

    directive.link = function (scope, elem, iAttrs) { 
        elem.find('.save,.cancel,.close_pop').bind('click', function () {
            elem.hide();
        });
    }

    directive.controller = function ($scope) {
        $scope.ok = function () {
            $scope.$emit('onDialogOk');
        };

        $scope.close = function () {
            $scope.$emit('onDialogClose');
        };

        $scope.cancel = function () {
            $scope.$emit('onDialogCancel'); 
        }

    }

    return directive;
});