Date.prototype.format = function (format) //author: meizz
{
    var o = {
        "M+": this.getMonth() + 1, //month
        "d+": this.getDate(),    //day
        "h+": this.getHours(),   //hour
        "m+": this.getMinutes(), //minute
        "s+": this.getSeconds(), //second
        "q+": Math.floor((this.getMonth() + 3) / 3),  //quarter
        "S": this.getMilliseconds() //millisecond
    }
    if (/(y+)/.test(format)) format = format.replace(RegExp.$1,
    (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o) if (new RegExp("(" + k + ")").test(format))
        format = format.replace(RegExp.$1,
      RegExp.$1.length == 1 ? o[k] :
        ("00" + o[k]).substr(("" + o[k]).length));
    return format;
}


var aService = angular.module('app.utils', ['app.services']); 

aService.factory('appUtils', ['$http', '$q', function ($http, $q) {

    var service = {};

    service.createPromise = function (url, param) {
        var deferred = $q.defer();
        $http.post(url, param)
            .success(function (data) {
                deferred.resolve(data);
            })
            .error(function (reason) {
                deferred.reject(reason);
            });
        return deferred.promise;
    }

    service.runPromises = function (obj, thenFn) {
        $q.all(obj).then(function (results) {
            thenFn(results);
        });
    }

    return service;
}]);

aService.factory('dialogUtils', ['softname', function (softname) {

    var dialogService = {};

    dialogService.info = function (message, okFn) {
        BootstrapDialog.show({
            title: softname,
            message: message,
            buttons: [{
                id: 'btn-ok',
                icon: 'glyphicon glyphicon-ok',
                label: '确定',
                cssClass: 'btn-primary',
                autospin: false,
                action: function (dialogRef) {
                    if (okFn) {
                        okFn();
                    }
                    dialogRef.close();
                }
            }]
        });
    }

    dialogService.confirm = function (message, okFn, cancelFn) {
        BootstrapDialog.show({
            title: softname,
            message: message,
            buttons: [{
                id: 'btn-ok',
                icon: 'glyphicon glyphicon-ok',
                label: '确定',
                cssClass: 'btn-primary',
                autospin: false,
                action: function (dialogRef) {
                    if (okFn) {
                        okFn();
                    }
                    dialogRef.close();
                }
            }, {
                id: 'btn-cancel',
                icon: 'glyphicon glyphicon-remove',
                label: '取消',
                cssClass: 'btn-default',
                autospin: false,
                action: function (dialogRef) {
                    if (cancelFn) {
                        cancelFn();
                    }
                    dialogRef.close();
                }
            }]
        });
    }

    return dialogService;
}]);

