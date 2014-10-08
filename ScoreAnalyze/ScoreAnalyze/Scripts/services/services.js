'use strict';
 
var serviceApp = angular.module('app.services', ['ngCookies']);

serviceApp.value('version', '0.1');

//工具服务类
serviceApp.factory('util', ['$http', '$q', function ($http, $q) {

    //向服务端提交数据
    function post(url, param, success) {
        $http.post(url, param).success(function (data) {
            success(data);
        });
    }

    ///向服务器请求数据
    function get(url, success) {
        $http.get(url).success(function (data) {
            success(data);
        });
    }

    //日期格式化为 yyyy-MM-dd hh:mm:ss
    function formatTime(val) {
        var re = /-?\d+/;
        var m = re.exec(val);
        var d = new Date(parseInt(m[0]));
        // 按【2012-02-13 09:09:09】的格式返回日期
        return d.format("yyyy-MM-dd hh:mm:ss");
    }

    //日期格式化为 yyyy-MM-dd
    function formatDate(val) {
        var re = /-?\d+/;
        var m = re.exec(val);
        var d = new Date(parseInt(m[0]));
        // 按【2012-02-13 09:09:09】的格式返回日期
        return d.format("yyyy-MM-dd");
    }
    var buildPromiseGet = function (url) {
        var deferred = $q.defer();
        $http.get(url)
            .success(function (data) {
                deferred.resolve(data);
            })
            .error(function (reason) {
                deferred.reject(reason);
            });
        return deferred.promise;
    }
    var buildPromise = function (url, param) {
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

    var execPromise = function (obj, thenFn) {
        $q.all(obj).then(function (results) {
            thenFn(results);
        });
    }
    //alert 提示框
    function showAlert(title, context) {
        window.alert(context);
    }

    //confirm 对话框
    function showConfirm(title, context) {
        return window.confirm(context);
    }

    return {
        post: post,
        get: get,
        formatDate: formatDate,
        formatTime: formatTime,
        buildPromise: buildPromise,
        buildPromiseGet: buildPromiseGet,
        execPromise: execPromise,
        showAlert: showAlert,
        showConfirm: showConfirm
    }
}]);

// 系统服务
serviceApp.factory('systemService', ['util', 'auth', function (util, auth) {

    ///根据路径
    var rootUrl = '/Services/SystemService.aspx';

    function verify(user, pwd, cb) {
        var url = rootUrl + '/Verify';
        var param = { user: user, pwd: pwd };
        util.post(url, param, cb);
    }

    function initSystem(user, cb) {
        var promiseMenu = util.buildPromiseGet('/Resource/menu.json');
        var promiseFuncs = util.buildPromise('/Services/SystemService.aspx/GetFuncs', { teacherID: user.TeacherID });

        util.execPromise({
           menuData: promiseMenu
         , funcData: promiseFuncs
        }, function (results) {
            if (results.menuData != null) {
                auth.menus = results.menuData;
            }
            if (results.funcData.d != null && results.funcData.d.length > 0) {
                var hasFunc = false;
                var funcs = results.funcData.d;
                for (var i = 0; i < funcs.length; i++) {
                    var func = funcs[i];
                    for (var j = auth.menus.length - 1; j >= 0 ; j--) {
                        var menu = auth.menus[j];
                        if (func.FuncName == menu.FuncName) {
                            menu.HasFunc = '1';
                            hasFunc = true;
                            break;
                        }
                    }
                }
            }
            cb(hasFunc);
        });
    }
    return {
        verify: verify,
        initSystem: initSystem
    }

}]);

serviceApp.factory('auth', ['$cookieStore',  function ($cookieStore) {
    var _user = $cookieStore.get('user');

    var setUser = function (user) {
        _user = user;
        $cookieStore.put('scoreuser', _user);
    }

    return {
        setUser: setUser,
        getUser: function () { return _user; },
        getToken: function () { return _user ? _user.Token : '' },        
        isAuthorized: function () { return _user && _user.Token != '' },
        logout: function () {
            $cookieStore.remove('user');
            _user = null;
        },
        menus: []
    }
}]);