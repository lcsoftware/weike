'use strict';

var app = angular.module('app.custom.directives', ['app.assist.services', 'ui.tree']);


app.directive('moreCourse', function () {
    var directive = {};

    directive.restrict = 'EA';

    directive.replace = true;

    directive.scope = {
        onSelected: '&',
        selection: '=',
        course: '=',
        courseIndex: '='
    }

    directive.templateUrl = '/Components/templates/moreCourse.html';

    directive.link = function (scope, elem, iAttrs) {
        //查看更多
        elem.hover(function () {
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
        folderRelation: '='
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
        folderExt: '='
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
        onFireRemoveAll: '&',
        onFireShared: '&'
    }

    directive.templateUrl = '/Components/templates/exerciseBatch.html';

    directive.link = function (scope, elem, iAttrs) {
        elem.find('.batch_list li').hover(function () {
            $(this).addClass('active').siblings().removeClass('active');
        }, function () {
            $(this).removeClass('active');
        })
        elem.find('.permissions li').hover(function () {
            $(this).addClass('current').siblings().removeClass('current');
        }, function () {
            $(this).removeClass('current');
        })
    }

    return directive;
});


app.directive('exerciseList', ['assistService', function (assistService) {
    var directive = {};

    directive.restrict = 'EA';

    directive.replace = true;

    directive.scope = {
        exercise: '=',
        shareExercise: '&',
        editExercise: '&',
        deleteExercise: '&'
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
    }

    directive.controller = function ($scope, assistService) {

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

    directive.replace = true;

    directive.scope = {
        uploadUrl: '@',
        ocid: '=',
        courseId: '=',
        folderID: '='
    }

    directive.templateUrl = '/Components/templates/iesFileUploader.html';

    directive.link = function (scope, elem, iAttrs) {
        elem.find('.close_pop').bind('click', function () {
            elem.hide();
        }); 
    }

    directive.controller = function ($scope) {
        //----------上传文件start--------
        var angularFileUploader = $scope.iesUploader = new FileUploader({url: $scope.uploadUrl});
            //angularFileUploader.formData.push({ OCID: $scope.ocid });
            //angularFileUploader.formData.push({ CourseID: $scope.courseId});
            //angularFileUploader.formData.push({ FolderID: $scope.folderID});
            //angularFileUploader.formData.push({ ShareRange: -1 });

        $scope.$watch('ocid', function (v) {
            angularFileUploader.formData.length = 0;
            angularFileUploader.formData.push({ OCID: v });
            angularFileUploader.formData.push({ CourseID: $scope.courseId});
            angularFileUploader.formData.push({ FolderID: $scope.folderID});
            angularFileUploader.formData.push({ ShareRange: -1});
        });

        $scope.$watch('courseId', function (v) {
            angularFileUploader.formData.length = 0;
            angularFileUploader.formData.push({ OCID: $scope.ocid });
            angularFileUploader.formData.push({ CourseID: v});
            angularFileUploader.formData.push({ FolderID: $scope.folderID});
            angularFileUploader.formData.push({ ShareRange: -1});
        });

        $scope.$watch('folderID', function (v) {
            angularFileUploader.formData.length = 0;
            angularFileUploader.formData.push({ OCID: $scope.ocid });
            angularFileUploader.formData.push({ CourseID: $scope.courseId });
            angularFileUploader.formData.push({ FolderID: v });
            angularFileUploader.formData.push({ ShareRange: -1 });
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
            $scope.$emit('onCompleteAll');
        };
    }

    return directive;
}]);