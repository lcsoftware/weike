'use strict';

var aService = angular.module('app.chapter.services', []);

aService.factory('chapterService', ['httpService', function (httpService) {
    var service = {};

    var chapterProviderUrl = '/DataProvider/Chapter/ChapterProvider.aspx'; 

    service.Chapter_Get = function (callback) {
        httpService.ajaxPost(chapterProviderUrl, 'Chapter_Get', null, callback);
    }

    service.Chapter_List = function (model, callback) {
        httpService.ajaxPost(chapterProviderUrl, 'Chapter_List', { model: model }, callback);
    }

    service.Chapter_ADD = function (model, callback) {
        httpService.ajaxPost(chapterProviderUrl, 'Chapter_ADD', { model: model }, callback);
    }

    service.Chapter_Upd = function (model, callback) {
        httpService.ajaxPost(chapterProviderUrl, 'Chapter_Upd', { model: model }, callback);
    } 

    return service;
}])