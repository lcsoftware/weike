
'use strict';

var aService = angular.module('app.services', ['ngCookies']);

aService.value('version', '0.1');

aService.constant('projectName', '我的项目');

aService.constant('homeProviderUrl', '/DataProvider/Home/HomeProvider.aspx');

aService.constant('courseindexProviderUrl', '/DataProvider/OC/CourseIndexProvider.aspx');

aService.constant('userProviderUrl', '/DataProvider/UserProvider.aspx');

aService.constant('siteProviderUrl', '/DataProvider/OC/Site/SiteProvider.aspx');

aService.constant('fcProviderUrl', '/DataProvider/OC/FC/FCProvider.aspx');

aService.constant('teamProviderUrl', '/DataProvider/OC/Team/TeamProvider.aspx');

aService.constant('classProviderUrl', '/DataProvider/OC/Class/ClassProvider.aspx');

aService.constant('scoreProviderUrl', '/DataProvider/CourseLive/Score/ScoreProvider.aspx');

aService.constant('UserPermission', '/DataProvider/User/UserProvider.aspx');

aService.constant('moocProviderUrl', '/DataProvider/OC/MOOC/MOOCProvider.aspx');

aService.constant('forumProviderUrl', '/DataProvider/CourseLive/Forum/ForumProvider.aspx');

aService.constant('testProviderUrl', '/DataProvider/CourseLive/Test/TestProvider.aspx');
aService.constant('MarkingProviderUrl', '/DataProvider/CourseLive/Test/MarkingProvider.aspx');

aService.constant('MOOCPreviewProviderUrl', '/DataProvider/OC/MOOC/MOOCPreview.aspx');
aService.constant('affairsProviderUrl', '/DataProvider/Affairs/AffairsProvider.aspx');

aService.constant('studyprocessProviderUrl', '/DataProvider/CourseLive/StudyProcess/StudyProcessProvider.aspx');

///XHR调用
aService.factory('baseService', ['$http', '$q', function ($http, $q) {

    var service = {};

    service.get = function (url, thenFn) {
        $http.get(url).then(thenFn);
    }
    //异步post
    service.post = function (url, param, thenFn, errFn) {
        $http.post(url, param)
            .success(function (data) { if (thenFn) { thenFn(data); } })
            .error(function (reason) { if (errFn) { errFn(reason); } });
    }
    //同步
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