'use strict';

var app = angular.module('app.custom.directives', ['app.assist.services', 'ui.tree', 'app.auth.services']);


app.directive('moreCourse', function () {
    var directive = {};

    directive.restrict = 'EA';

    directive.replace = true;

    directive.scope = {
        courses: '=',
        courseMore: '=',
        course: '=',
        moreTitle: '='
    }

    directive.templateUrl = window.appPatch + '/Components/templates/moreCourse.html';

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
        $scope.courseChange = function (course, isMore) {
            $scope.$emit('onWillCourseChanged', course, isMore);
        }
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

    directive.templateUrl = window.appPatch + '/Components/templates/fileProperty.html';

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

    directive.templateUrl = window.appPatch + '/Components/templates/addKnowledge.html';

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

    directive.templateUrl = window.appPatch + '/Components/templates/addChapter.html';

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


app.directive('chapterEditor', function () {
    var directive = {};

    directive.restrict = 'EA';

    directive.scope = {
        editorTitle: '=',
        chapterName: '=',
        onSave: '&',
        onCancel: '&'
    }

    directive.templateUrl = window.appPatch + '/Components/templates/chapterEditor.html';

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

    directive.templateUrl = window.appPatch + '/Components/templates/folder.html';

    directive.link = function (scope, elem, iAttrs) {
        //重命名表现形式
        var e = elem.find('.data_tit');
        e.bind('dblclick', function (e) {
            $(this).hide();
            elem.find('input').show().select();
        });
    }
    return directive;
});

app.directive('fileOperation', ['authService', function (authService) {
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
        ocid: '=',
        creater: '='
    }

    directive.templateUrl = window.appPatch + '/Components/templates/fileOperation.html';

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
    }

    directive.controller = function ($scope) {
        $scope.hasAuth = authService.hasAuth($scope.creater, $scope.ocid);
    }

    return directive;
}]);

app.directive('shareRange', function () {
    var directive = {};

    directive.restrict = 'EA';

    directive.replace = true;

    directive.scope = {
        range: '='
    }

    directive.templateUrl = window.appPatch + '/Components/templates/shareRange.html';

    directive.link = function (scope, elem, iAttrs) {
        elem.hover(function () {
            $(this).addClass('current');
        }, function () {
            $(this).removeClass('current');
        });
    }

    return directive;
});

app.directive('fileShareRange', function () {
    var directive = {};

    directive.restrict = 'EA';

    directive.replace = true;

    directive.scope = {
        range: '='
    }

    directive.templateUrl = window.appPatch + '/Components/templates/fileShareRange.html';

    directive.link = function (scope, elem, iAttrs) {
        elem.hover(function () {
            $(this).addClass('current');
        }, function () {
            $(this).removeClass('current');
        });
    }

    return directive;
});

app.directive('batchOperation', function () {
    var directive = {};

    directive.restrict = 'EA';
    directive.replace = true;
    directive.scope = {
        onFireRemoveAll: '&',
        onFireMobileBatch: '&',
        shareRanges: '=',
        ocid: '='
    }

    directive.templateUrl = window.appPatch + '/Components/templates/batchOperation.html';

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
    }

    return directive;
});
app.directive('exerciseBatch', ['$window', 'exerciseService', function ($window, exerciseService) {
    var directive = {};

    directive.restrict = 'EA';

    directive.scope = {
        checks: '=',
        shareRanges: '=',
        difficults: '='
    }

    directive.templateUrl = window.appPatch + '/Components/templates/exerciseBatch.html';

    directive.link = function (scope, elem, iAttrs) {
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

        $scope.batchPreview = function () {
            if (!$scope.checks || $scope.checks.length === 0) return;
            exerciseService.ExercisePreviewHttpPrefix(function (data) {
                var prefix = data.d;
                var browsePaper = 'Resource/Paper/BrowsePaper'
                var exerciseIds = '?ExerciseIDs=';
                angular.forEach($scope.checks, function (item) {
                    exerciseIds += item.ExerciseID + ','
                });
                var url = prefix + browsePaper + exerciseIds.substring(0, exerciseIds.length - 1);
                $window.open(url);
            });
        }
    }

    return directive;
}]);


app.directive('exerciseList', ['assistService', 'previewService', 'exerciseService', '$state', 'authService',
    function (assistService, previewService, exerciseService, $state, authService) {
    var directive = {};

    directive.restrict = 'EA';

    directive.replace = true;

    directive.scope = {
        exercise: '=',
        checks: '=',
        shareRanges: '=',
        disableDifficult: '=',
        creater: '=',
        ocid: '='
    }

    directive.templateUrl = window.appPatch + '/Components/templates/exerciseList.html';

    directive.link = function (scope, elem, iAttrs) {
        elem.hover(function () { $(this).find('.topic_icon').show(); },
                   function () { $(this).find('.topic_icon').hide(); }
                  );
        elem.find('.icon.delete_topic,.icon.edit_topic,.icon.icon_eye').hover(
            function () {
                $(this).find('.icon_content').show();
                elem.find('.icon.share_topic').find('.icon_share_content').hide();
            },
            function () { $(this).find('.icon_content').hide(); }
            );
        elem.find('.icon.share_topic').hover(
           function () { $(this).find('.icon_share_content').show(); },
            function () { $(this).find('.icon_share_content').hide(); }
           );
        elem.find('.spread').bind('click', function () {
            if (!elem.hasClass('show')) {
                elem.addClass('show');
            } else {
                elem.removeClass('show');
            }
        })
    }

    directive.controller = function ($scope, assistService, previewService, exerciseService, $state, $window) {

        $scope.hasAuth = authService.hasAuth($scope.creater, $scope.ocid);

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

        var goPreView = function (model) {
            previewService.exercise = angular.copy(model);
            $scope.$emit('onPreviewExercise', previewService.exercise);
            //$state.go('preview');
        }

        $scope.preview = function (exercise) {
            exerciseService.ExercisePreviewHttpPrefix(function (data) {
                var prefix = data.d;
                var browsePaper = 'Resource/Paper/BrowsePaper'
                var exerciseIds = '?ExerciseIDs=' + exercise.ExerciseID; 
                var url = prefix + browsePaper + exerciseIds;
                $window.open(url);
            });
        }

        //$scope.preView = function (exercise) {
        //    switch (exercise.ExerciseType) {
        //        case 5: //填空题
        //            exerciseService.Exercise_Analysis_Get(exercise.ExerciseID, function (data) {
        //                var model = data.d;
        //                for (var i = 0; i < model.exercisechoicelist.length; i++) {
        //                    var a = model.exercisechoicelist[i].Answer.split('wshgkjqbwhfbxlfrh_c');
        //                    model.exercisechoicelist[i].Answer = a[0];
        //                    model.exercisechoicelist[i].Spare = a[1];
        //                }
        //                goPreView(model);
        //            });
        //            break;
        //        case 4: //名词解释                
        //        case 8: //分析题
        //        case 9: //计算题
        //            exerciseService.Exercise_Analysis_Get(exercise.ExerciseID, function (data) {
        //                goPreView(data.d);
        //            });
        //            break;
        //        case 12: //听力题
        //        case 17: //自定义题
        //        case 14: //阅读理解题
        //            exerciseService.Exercise_Custom_Get(exercise.ExerciseID, function (data) {
        //                goPreView(data.d);
        //            });
        //            break;
        //        case 10: //问答题
        //        case 13: //写作题
        //        case 11: //翻译题
        //            exerciseService.Exercise_Writing_Get(exercise.ExerciseID, function (data) {
        //                goPreView(data.d);
        //            });
        //            break;
        //        case 1: //判断题
        //            exerciseService.Exercise_Judge_Get(exercise.ExerciseID, function (data) {
        //                goPreView(data.d);
        //            });
        //            break;
        //        case 6:  //连线题
        //        case 2:  //单选题
        //        case 3:  //多选题
        //        case 7:  //排序题
        //            exerciseService.Exercise_MultipleChoice_Get(exercise.ExerciseID, function (data) {
        //                goPreView(data.d);
        //            });
        //            break;
        //        default:
        //            break;
        //    }
        //}
    }

    return directive;
}]);

app.directive('preView', function () {
    var directive = {};

    directive.restrict = 'EA';

    directive.scope = {
        exercise: '=',
        onClose: '&'
    }

    directive.templateUrl = window.appPatch + '/Components/templates/preView.html';


    directive.link = function (scope, elem, iAttrs) {
    }
    directive.controller = function ($scope, $element) {
    }

    return directive;
});

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

    directive.templateUrl = window.appPatch + '/Components/templates/moveFolder.html';


    directive.link = function (scope, elem, iAttrs) {
        //移动文件弹出框           

        //$('.first_file span').bind('click', function () {
        //    if ($(this).parent().next().is(':hidden')) {
        //        $(this).html('<em>-</em>');
        //        $(this).parent().next().slideDown();
        //    } else {
        //        $(this).html('<em>+</em>');
        //        $(this).parent().next().slideUp();
        //    }
        //})
    }
    directive.controller = function ($scope, $element) {


        $scope.$on('$includeContentLoaded', function (a) {
            $element.find('.first_file span').unbind('click');
            $element.find('.first_file span').bind('click', function () {
                if ($(this).parent().next().is(':hidden')) {
                    //展开
                    $(this).html('<em>-</em>');
                    $(this).parent().next().slideDown();
                } else {
                    //隐藏
                    $(this).html('<em>+</em>');
                    $(this).parent().next().slideUp();
                }
            })
        });

        $scope.onSelectedMove = function (node) {
            $scope.$emit('onSelectedMove', node);
            $scope.selected = node.Id;
        }
        $scope.$on('selectMoveClear', function (event, item) {
            $scope.selected = -1;
        });
    }

    return directive;
});

//移动文件子类
app.directive('moveItem', function () {
    var directive = {};

    directive.restrict = 'EA';

    directive.scope = {
        itemNode: '='
    }

    directive.templateUrl = window.appPatch + '/Components/templates/moveItem.html';

    directive.link = function (scope, elem, iAttrs) {
        //移动文件弹出框    
        console.log(elem.find('.pop_wrap .first_file'));
        //$('.first_file span').bind('click', function () {
        //    if ($(this).parent().next().is(':hidden')) {
        //        $(this).html('<em>-</em>');
        //        $(this).parent().next().slideDown();
        //    } else {
        //        $(this).html('<em>+</em>');
        //        $(this).parent().next().slideUp();
        //    }
        //})
    }

    directive.controller = function ($scope) {
        $scope.onSelectedMove = function (node) {
            $scope.$emit('onSelectedMove', node);
        }
    }

    return directive;
});

//编辑器
app.directive('editor', ['$timeout', function ($timeout) {
    var directive = {};

    directive.restrict = 'EA';

    directive.scope = {
        editorText: '=',
        editorId: '@'
    }

    directive.templateUrl = window.appPatch + '/Components/templates/editor.html';

    directive.controller = function ($scope, $element) {
        $scope.$on('onEditorSetValue', function (event) {
            //$element.find("#frame01").contentWindow.setHTML($scope.editorText);
            document.getElementById("frame01").contentWindow.setHTML($scope.editorText);
        })
    } 
    return directive;
}]);

//文件上传
app.directive('iesFileUploader', ['FileUploader', function (FileUploader) {
    var directive = {};

    directive.restrict = 'EA';

    directive.scope = {
        fileCatetory: '@',
        ocid: '=',
        courseId: '=',
        folderObj: '='
    }

    directive.templateUrl = window.appPatch + '/Components/templates/iesFileUploader.html';

    directive.link = function (scope, elem, iAttrs) {
        elem.find('.close_pop').bind('click', function () {
            elem.hide();
        });
    }

    directive.controller = function ($scope, $element) {
        //----------上传文件start--------
        var reqUrl = window.appPatch + '/DataProvider/FileUpload.ashx?FROM=' + $scope.fileCatetory;
        var angularFileUploader = $scope.iesUploader = new FileUploader({ url: reqUrl });

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
            //angularFileUploader.url = '/DataProvider/FileUpload.ashx/?FROM=1';
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

//文件上传
app.directive('iesExerciseUploader', ['FileUploader', 'exerciseService', 'httpService', function (FileUploader, exerciseService, httpService) {
    var directive = {};

    directive.restrict = 'EA';

    directive.scope = {
        fileCatetory: '@',
        exerciseCourse: '='
    }

    directive.templateUrl = window.appPatch + '/Components/templates/exerciseUploader.html';

    //directive.link = function (scope, elem, iAttrs) {
    //    elem.find('.close_pop').bind('click', function () {
    //        elem.hide();
    //    });
    //}

    directive.controller = function ($scope, $element) {
        $element.find('.close_pop,.pop_box .btn_box .cancel').bind('click', function () {
            $element.hide(); 
        }); 
       
        //----------上传文件start--------
        var reqUrl = window.appPatch + '/DataProvider/FileUpload.ashx?FROM=' + $scope.fileCatetory;
        var angularFileUploader = $scope.iesUploader = new FileUploader({ url: reqUrl });

        $scope.$watch('exerciseCourse', function (v) {
            angularFileUploader.formData.length = 0;
            angularFileUploader.formData.push({ OCID: $scope.exerciseCourse.OCID });
            angularFileUploader.formData.push({ CourseID: $scope.exerciseCourse.CourseID });
            angularFileUploader.formData.push({ Category: $scope.importTypeSelected.id });
        });

        $scope.$watch('importTypeSelected', function (v) {
            angularFileUploader.formData.length = 0;
            angularFileUploader.formData.push({ OCID: $scope.exerciseCourse.OCID });
            angularFileUploader.formData.push({ CourseID: $scope.exerciseCourse.CourseID });
            angularFileUploader.formData.push({ Category: $scope.importTypeSelected.id });
            $scope.templateUrl = v.templateUrl;
        });

        $scope.$watch('iesUploader.queue.length', function (v) {
            if (v > 0) {
                $scope.fileName = angularFileUploader.queue[0].file.name;
                $scope.stepA = true;
                $scope.iesUploader.uploadAll();
            }
        })

        $scope.importTypes = [
            { id: 2, name: "单选题", templateUrl: window.appPatch + '/ExerciseTemplates/单选题_多选题.xls' },
            { id: 3, name: "多选题", templateUrl: window.appPatch + '/ExerciseTemplates/单选题_多选题.xls' },
            { id: 1, name: "判断题", templateUrl: window.appPatch + '/ExerciseTemplates/判断题.xls' },
            { id: 5, name: "填空题", templateUrl: window.appPatch + '/ExerciseTemplates/填空题.xls' },
            { id: 10, name: "问答题", templateUrl: window.appPatch + '/ExerciseTemplates/问答题_翻译题_名词解释_写作题.xls' },
            { id: 11, name: "翻译题", templateUrl: window.appPatch + '/ExerciseTemplates/问答题_翻译题_名词解释_写作题.xls' },
            { id: 4, name: "名词解释", templateUrl: window.appPatch + '/ExerciseTemplates/问答题_翻译题_名词解释_写作题.xls' },
            { id: 13, name: "写作题", templateUrl: window.appPatch + '/ExerciseTemplates/问答题_翻译题_名词解释_写作题.xls' },
        ]

        $scope.importTypeSelected = $scope.importTypes[3];
        $scope.templateUrl = $scope.importTypeSelected.templateUrl;
        ///客户端文件名称
        $scope.fileName = '';
        ///保存在服务器上的文件名称
        $scope.serverFileName = '';
        $scope.process = 'initial'
        $scope.resultTable = [];

        $scope.stepA = $scope.process === 'loading';
        $scope.stepB = $scope.process === 'initial' || $scope.process === 'allRight';
        $scope.stepC = $scope.process === 'partRight' || $scope.process === 'fmtError';

        $scope.errors = 0;
        $scope.rights = 0;

        var decideStep = function (resultTable) {
            $scope.errors = 0;
            $scope.rights = 0;
            var length = resultTable.length;
            for (var i = 0; i < length; i++) {
                $scope.rights += resultTable[i].Status === '1' ? 1 : 0;
            }
            $scope.errors = length - $scope.rights;
            if ($scope.rights === length) {
                $scope.process = 'allRight'
            } else if ($scope.errors === length) {
                $scope.process = 'fmtError'
            } else {
                $scope.process = 'partRight'
            }

            $scope.stepA = $scope.process === 'loading';
            $scope.stepB = $scope.process === 'initial' || $scope.process === 'allRight';
            $scope.stepC = $scope.process === 'partRight' || $scope.process === 'fmtError';
        }

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
                return suffix == ".xls" || suffix == ".xlsx";
                //return true;
            }
        });

        angularFileUploader.onCompleteItem = function (fileItem, response, status, headers) {
            $scope.resultTable = response.data;
            $scope.serverFileName = response.fileName;
            decideStep($scope.resultTable);
            $scope.$emit('onExerciseImportComplete');
        };

        angularFileUploader.onCompleteAll = function () {
            angularFileUploader.clearQueue();
            $scope.iesUploader.clearQueue();
            $scope.$emit('onCompleteAll');
        };

        ///需要待校验逻辑实现后才能导入
        $scope.startImport = function () { 
            var uploadUrl = '/DataProvider/FileUpload.ashx?FROM=4';
            uploadUrl += "&fileName=" + $scope.serverFileName;
            uploadUrl += "&OCID=" + $scope.exerciseCourse.OCID;
            uploadUrl += "&CourseID=" + $scope.exerciseCourse.CourseID;
            uploadUrl += "&Category=" + $scope.importTypeSelected.id;
            httpService.post(uploadUrl, null, function (data) {
                console.log(data);
            });

        }

    }

    return directive;
}]);


app.directive('attachList', function () {
    var directive = {};

    directive.restrict = 'EA';

    directive.replace = true;

    directive.templateUrl = window.appPatch + '/Components/templates/attachmentList.html';

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

    directive.templateUrl = window.appPatch + '/Components/templates/preview.html';

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

app.directive('fileIcon', function () {
    var directive = {};

    directive.restrict = 'EA';

    directive.replace = true;

    directive.templateUrl = window.appPatch + '/Components/templates/file_icon.html';

    directive.scope = {
        iconType: '=',
        relationType: '='
    }

    directive.link = function (scope, elem, iAttrs) {
        elem.find('.save').bind('click', function () {
            elem.hide();
        });
    }

    return directive;
});

app.directive('iesKenItem', ['authService', function (authService) {
    var directive = {};

    directive.restrict = 'EA';

    directive.replace = true;

    directive.templateUrl = window.appPatch + '/Components/templates/kenItemList.html';

    directive.scope = {
        kenItem: '=',
        dataKen: '=',
        onEdit: '&',
        onRemove: '&'
    }

    directive.link = function (scope, elem, iAttrs) {
        $('.knowledge_list dd').hover(function () {
            $(this).addClass('active').siblings().removeClass('active');
        })
    }

    directive.controller = function ($scope) {
        $scope.hasAuth = authService.hasAuth($scope.kenItem.CreateUserID, $scope.kenItem.OCID);
    }

    return directive;
}]);

app.directive('kenChapterItem', function () {
    var directive = {};

    directive.restrict = 'EA';

    directive.replace = true;

    directive.templateUrl = window.appPatch + '/Components/templates/kenChapterItem.html';

    directive.scope = {
        kenChapter: '=',
        dataChapter: '=',
        onEdit: '&',
        onRemove: '&'
    }

    directive.link = function (scope, elem, iAttrs) {
        $('.knowledge_list dd').hover(function () {
            $(this).addClass('active').siblings().removeClass('active');
        })
    }

    return directive;
});

app.directive('iesChapterItem', ['$timeout', 'authService', function ($timeout, authService) {
    var directive = {};

    directive.restrict = 'EA';

    directive.replace = true;

    directive.templateUrl = window.appPatch + '/Components/templates/chapterItem.html';

    directive.scope = {
        chapterSelectedItem: '=',
        chapterItem: '=',
        childChapter: '=',
        items: '=',
        chapterSelected: '&',
        onEdit: '&'
    }

    directive.link = function (scope, elem, iAttrs) {

        ///编辑
        //elem.find('a:first span:first').bind('click', function () {
        //    elem.siblings().removeClass('active');
        //    elem.addClass('active');
        //});

        ///增加小节
        elem.find('a:first span:last').bind('click', function () {
            elem.siblings().removeClass('active');
            elem.addClass('active');
        });

        ///点选
        elem.find('a:first p').bind('click', function () {
            elem.siblings().removeClass('active');
            elem.addClass('active');
        });
    }

    directive.controller = function ($scope, $element) {

        $scope.hasChapterAuth = authService.hasChapterAuth($scope.chapterItem.OCID);

        $scope.childSelected = function (child) {
            $scope.$emit('onChildSelected', child);
        }

        $scope.willEdit = function (chapter) {
            $scope.$emit('onWillEdit', chapter);
        }

        $scope.$on('onChapterEdited', function (event, editResult) {
            $element.find('a:first').removeClass('chapterHide');
            $element.find('a:last').addClass('chapterHide');
        });

        $scope.willAddSection = function (chapter) {
            $element.addClass('active');
            $element.siblings().removeClass('active');

            $scope.$emit('onAddChild', chapter);
        }

        $scope.edit = function (chapter) {
            $element.siblings().removeClass('active');
            $element.addClass('active');
            $scope.$emit('onEdit', chapter);
        }
    }

    return directive;
}]);

app.directive('iesChapterSection', function () {
    var directive = {};

    directive.restrict = 'EA';

    directive.templateUrl = window.appPatch + '/Components/templates/chapterSection.html';

    directive.scope = {
        sectionChapter: '=',
        chapterChildSelectedItem: '='
    }

    directive.link = function (scope, elem, iAttrs) {
        ///编辑
        elem.find('a:first span').bind('click', function () {
            //elem.siblings().find('a:first').removeClass('chapterHide');
            //elem.siblings().find('a:last').addClass('chapterHide');
            //elem.siblings().removeClass('active');
            //elem.removeClass('active');
            //elem.find('a:first').addClass('chapterHide');
            //elem.find("a:last").removeClass('chapterHide');
            //elem.find("a:last input").focus();
        });

        ///点选
        elem.find('a p').bind('click', function () {
            elem.siblings().removeClass('active');
            elem.addClass('active');
        });
    }

    return directive;
});

app.directive('iesDialog', function () {
    var directive = {};

    directive.restrict = 'EA';

    directive.templateUrl = window.appPatch + '/Components/templates/confirm.html';

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

//文件类型
app.directive("fileExt", [function () {
    return {
        restrict: 'A',
        link: function (scope, elem, iAttrs) {
            var ext = iAttrs.ext.toLowerCase().replace('.', '');
            if (ext == "file" || !ext) ext = "file";
            if (ext == "docx") ext = "doc";
            if (ext == "pptx") ext = "ppt";
            if (ext == "xlsx" || ext == "xls") ext = "excel";
            if (ext == "jpg" || ext == "jpeg" || ext == "gif" || ext == "png" || ext == "bmp" || ext == "ico") ext = "pic";
            elem.addClass(ext);
        }
    };
}])