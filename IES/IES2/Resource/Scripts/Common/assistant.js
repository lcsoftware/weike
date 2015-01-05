'use strict';

var aService = angular.module('app.common.assistant', []);

aService.value('version', '0.1');

aService.constant('appName', '我的项目');

aService.constant('userProviderUrl', '/DataProvider/User/UserProvider.aspx');
aService.constant('authProviderUrl', '/DataProvider/Authority/AuthProvider.aspx');
aService.constant('resourceProviderUrl', '/DataProvider/Resource/ResourceProvider.aspx');
aService.constant('knowProviderUrl', '/DataProvider/Knowledge/KnowProvider.aspx');
aService.constant('exerciseProviderUrl', '/DataProvider/Exercise/ExerciseProvider.aspx');
aService.constant('paperProviderUrl', '/DataProvider/Paper/PaperProvider.aspx');