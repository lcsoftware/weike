'use strict'

var appModule = angular.module('app.user', []);

appModule.directive('datepicker', function () {
    return {
        restrict: 'A',
        controller: 'datepickerCtrl',
        controllerAs: 'dp',
        templateUrl: '../../Views/Shared/datepicker.html',
        scope: {
            'value': '='
        },
        link: function (scope) {

        }
    };
});



appModule.controller('datepickerCtrl', function ($scope) {
    var self = this;
    
    $('.date').datepicker({ autoclose: true, todayHighlight: true });
    $scope.$watch('value', function (oldVal, newVal) {
        //console.log("Value: " + $scope.value);
    });
});


//define(['app', 'directive/datepickerCtrl'], function (app) {
//    app.directive('datepicker', function () {
//        return {
//            restrict: 'A',
//            controller: 'datepickerCtrl',
//            controllerAs: 'dp',
//            templateUrl: 'Script/directive/datepicker.html',
//            scope: {
//                'value': '='
//            },
//            link: function (scope) {

//            }
//        };
//    });
//});