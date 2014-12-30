'use strict';

var appResource = angular.module('app.resource.controllers', [
    'app.res.services'
]);

appResource.controller('ResourceCtrl', ['$scope', 'resourceService', function ($scope, resourceService) {
    $scope.$emit('onActived', 1);
    $scope.$on("onActived", function (event, active) {
        $scope.actived = active;
    });
    $scope.fileTypes = [];//文件类型

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
}]);

