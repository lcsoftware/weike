'use strict';

var aService = angular.module('app.services', ['ngCookies']);

aService.value('version', '0.1');

aService.constant('softname', '成绩分析系统');

aService.constant('utilProviderUrl', '/DataProvider/Util.aspx');
aService.constant('adminProviderUrl', '/DataProvider/Admin.aspx');
aService.constant('schoolProviderUrl', '/DataProvider/School.aspx');

aService.factory('baseService', ['$http', '$q', function ($http, $q) {

    var service = {};

    service.get = function (url, thenFn) {
        $http.get(url).then(thenFn);
    }

    service.post = function (url, param, thenFn, errFn) {
        $http.post(url, param)
            .success(function (data) { if (thenFn) { thenFn(data); } })
            .error(function (reason) { if (errFn) { errFn(reason); } });
    }

    service.postPromise = function (url, param) {
        var deferred = $q.defer();

        $http.post(url, param)
            .success(function (data) { deferred.resolve(data); })
            .error(function (reason) { deferred.reject(reason) });

        return deferred.promise;
    }

    service.runPromises = function (promises, thenFn) {
        $q.all(promises).then(function (results) {
            thenFn(results);
        });
    }

    return service;
}]);

aService.factory('utilService', ['baseService', 'utilProviderUrl', function (baseService, utilProviderUrl) {
    var service = {};

    service.GetNations = function (callback) {
        var url = utilProviderUrl + '/GetNations';
        var param = null;
        baseService.post(url, param, callback);
    }

    service.GetPolitics = function (callback) {
        var url = utilProviderUrl + '/GetPolitics';
        var param = null;
        baseService.post(url, param, callback);
    }

    service.GetResidenceType = function (callback) {
        var url = utilProviderUrl + '/GetResidenceType';
        var param = null;
        baseService.post(url, param, callback);
    }
    service.locate = function (collections, locateProp, locateValue) {
        var length = collections.length;
        for (var i = 0; i < length; i++) {
            var obj = collections[i];
            if (obj[locateProp] === locateValue) {
                return obj;
            }
        }
        return null;
    }

    return service;
}]);


aService.factory('userService', ['baseService', 'adminProviderUrl', 'appUtils', function (baseService, adminProviderUrl, appUtils) {
    var service = {};

    service.verify = function (userName, pwd, callback) {
        var url = adminProviderUrl + '/Verify';
        var param = { user: userName, pwd: pwd };
        baseService.post(url, param, callback);
    }

    service.logout = function (callback) {
        var url = adminProviderUrl + '/Logout';
        var param = null;
        baseService.post(url, param, callback);
    }

    service.isAuthorized = function (callback) {
        var url = adminProviderUrl + '/GetCookieInfo';
        var param = null;
        baseService.post(url, param, callback);
    }

    service.getUser = function (callback) {
        var url = adminProviderUrl + '/GetCookieInfo';
        var param = null;
        baseService.post(url, param, function (data) {
            callback(data.d !== '' ? JSON.parse(data.d) : null);
        });
    }

    service.getFuncs = function (teacher, callback) {
        var url = adminProviderUrl + '/GetFuncs';
        var param = { teacherID: teacher };
        baseService.post(url, param, callback);
    }

    service.getFuncTree = function (callback) {
        var url = adminProviderUrl + '/GetFuncTree';
        var param = null;
        baseService.post(url, param, callback);
    } 

    service.getUserFuncs = function (teacher, callback) {
        var url = adminProviderUrl + '/GetUserFuncs';
        var param = { teacher: teacher };
        baseService.post(url, param, callback); 
    }

    service.changePwd = function (teacher, oldPwd, newPwd, status, callback) {
        var url = adminProviderUrl + '/ChangePwd';
        var param = { teacherID: teacher, oldPwd: oldPwd, newPwd: newPwd, status: status };
        baseService.post(url, param, callback);
    } 

    service.getUserGroups = function (userOrGroup, callback) {
        var url = adminProviderUrl + '/GetUserGroups';
        var param = { userOrGroup: userOrGroup };
        baseService.post(url, param, callback);
    } 

    service.getGroupAndUsers = function (teacher, callback) {
        var url = adminProviderUrl + '/GetGroupAndUsers';
        var param = null;
        baseService.post(url, param, callback);
    }

    service.getAllUsers = function (callback) {
        var url = adminProviderUrl + '/GetAllUsers';
        var param = null;
        baseService.post(url, param, callback); 
    }

    service.getAllUserGroups = function (callback) {
        var url = adminProviderUrl + '/GetAllUserGroups';
        var param = null;
        baseService.post(url, param, callback); 
    }

    service.getUserAuths = function (teacher, callback) {
        var url = adminProviderUrl + '/GetUserAuths';
        var param = { teacher: teacher };
        baseService.post(url, param, callback); 
    }

    service.getUsersOfGroup = function (groupID, callback) {
        var url = adminProviderUrl + '/GetUsersOfGroup';
        var param = { groupID: groupID };
        baseService.post(url, param, callback); 
    }

    service.joinGroup = function (teacher, groupID, callback) {
        var url = adminProviderUrl + '/JoinGroup';
        var param = { teacher: teacher, groupID: groupID };
        baseService.post(url, param, callback); 
    }

    service.leaveGroup = function (teacher, groupID, callback) {
        var url = adminProviderUrl + '/LeaveGroup';
        var param = { teacher: teacher, groupID: groupID };
        baseService.post(url, param, callback);
    }

    service.addUserGroup = function (category, callback) {
        var url = adminProviderUrl + '/AddUserGroup';
        var param = { category: category };
        baseService.post(url, param, callback);
    }

    service.removeUserGroup = function (userGroup, callback) { 
        var url = adminProviderUrl + '/RemoveUserGroup';
        var param = { userGroup: userGroup };
        baseService.post(url, param, callback);
    }

    service.saveUserGroup = function (userGroup, callback) {
        var url = adminProviderUrl + '/SaveUserGroup';
        var param = { userGroup: userGroup };
        baseService.post(url, param, callback);
    }

    service.grant = function (teacher, func, rtype, sysNo, callback) {
        var url = adminProviderUrl + '/Grant';
        var param = { teacher: teacher, funcID: func, rtype: rtype, sysNo: sysNo };
        baseService.post(url, param, callback);
    }

    service.revoke = function (teacher, funcEntry, callback) {
        var url = adminProviderUrl + '/Revoke';
        var param = { teacher: teacher, funcEntry: funcEntry };
        baseService.post(url, param, callback);
    }

    service.buildGroupUserTree = function (callback) {
        var url = adminProviderUrl + '/GetGroupAndUsers';
        var param = null;
        var groupAndUserPromise = appUtils.createPromise(url, param);

        var url = adminProviderUrl + '/GetUserGroups';
        var param = { userOrGroup: '1' };
        var userPromise = appUtils.createPromise(url, param);

        appUtils.runPromises({
            GroupAndUsers: groupAndUserPromise,
            Users: userPromise
        }, function (results) {
            callback(results.GroupAndUsers.d, results.Users.d);
        });
    }

    return service;
}]);




aService.factory('menuService', ['baseService', 'adminProviderUrl', 'appUtils', function (baseService, adminProviderUrl, appUtils) {
    var service = {};

    //service.getMenus = function (callback) {
    //    var url = '../Assets/menu.json';
    //    baseService.get(url, callback);
    //}
    service.getMenus = function (callback) {
        var url = adminProviderUrl + '/GetMenus';
        var param = null;
        baseService.post(url, param, callback);
    }

    service.readMenu = function (callback) {
        var url = adminProviderUrl + '/GetMenuFromFile';
        var param = null;
        baseService.post(url, param, callback);
    }

    service.getFuncs = function (teacher, callback) {
        var url = adminProviderUrl + '/GetFuncs';
        var param = { teacherID: teacher };
        baseService.post(url, param, callback);
    }

    var filterMenus = function (funcs, menus) {
        var length = funcs.length;
        for (var i = 0; i < length; i++) {
            var func = funcs[i];
            var count = menus.length;
            for (var j = 0; j < count; j++) {
                var menu = menus[j];
                menu.visable = func.FuncID === menu.FuncID;
            }
        }
    }

    service.initMenus = function (teacher, callback) {
        var url = adminProviderUrl + '/GetFuncs';
        var param = { teacherID: teacher };
        var funcPromise = appUtils.createPromise(url, param);

        url = adminProviderUrl + '/GetMenuFromFile';
        param = null;
        var menuPromise = appUtils.createPromise(url, param);

        appUtils.runPromises({ funcs: funcPromise, menus: menuPromise }, function (results) {
            var aMenus = [];
            var aFuncs = null;
            if (results.menus.d !== null) {
                aMenus = JSON.parse(results.menus.d);
            }
            if (results.funcs.d !== null) {
                aFuncs = results.funcs.d;
            }

            if (aFuncs.length > 0 && aMenus.children.length > 0) {
                for (var menu in aMenus.children) {
                    filterMenus(aFuncs, menu.children);
                }
            }
            callback(aMenus);
        });
    }

    return service;
}]);