'use strict';

var appPaper= angular.module('app.paper.controllers', []);

appPaper.controller('PaperCtrl', ['$scope', function ($scope) { 
    $scope.$emit('onActived', 2);
}]); 