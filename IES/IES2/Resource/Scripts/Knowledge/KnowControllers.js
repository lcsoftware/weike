'use strict';

var appKnow = angular.module('app.knowledge.controllers', [
    'app.content.services'
]);

appKnow.controller('KnowledgeCtrl', ['$scope', 'contentService', function ($scope, contentService) {
    $scope.tabSelection = -1; 
    $scope.tabs = []; 
    $scope.tabChanged = function (tab) {
        $scope.tabSelection = tab;
        console.log(tab);
    }

    ///获取课程列表
    contentService.User_OC_List(function (data) {
        if (data.d) {
            $scope.tabs = data.d;
            $scope.tabSelection = $scope.tabs[0].OCID;
        }
    });
}]);

appKnow.controller('KnowChapterCtrl', ['$scope', 'PaperService', function ($scope, PaperService) {
}]);

appKnow.controller('KnowTopicCtrl', ['$scope', 'PaperService', function ($scope, PaperService) {

}]);

