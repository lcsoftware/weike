'use strict';

var appResource = angular.module('app.resource.controllers', []);

appResource.controller('ResourceCtrl', ['$scope', function ($scope) {
    $scope.$emit('onActived', 'B21');

    $scope.tabs = [
        { id: 0, name: '个人资料' },
        { id: 1, name: '毛泽东思想和中国特色社会主义毛泽东思想和中国特色社会主义' },
        { id: 2, name: '大学英语' },
        { id: 3, name: '形式与政策' }
    ];

    $scope.tabChanged = function (tab) {
        console.log(tab);
    }
}]);

