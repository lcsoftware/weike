'use strict';
var filtersModule = angular.module('app.filters', []);

filtersModule.filter('dateFormatAll', [function () {
    return function (v) {
        if (v == undefined) { return ""; }
        var re = /-?\d+/;
        var m = re.exec(v);
        if (parseInt(m) < 0) { return ""; }
        var d = new Date(parseInt(m[0]));
        return d.Format("yyyy-MM-dd hh:mm:ss");

    }
}]);

filtersModule.filter('dateFormat', [function () {
    return function (v) {
        //debugger;
        if (v == undefined) { return ""; }
        var re = /-?\d+/;
        var m = re.exec(v);
        if (parseInt(m) < 0) { return ""; }
        var d = new Date(parseInt(m[0]));
        return d.Format("yyyy-MM-dd");

    }
}]);

filtersModule.filter('dateFormatYear', [function () {
    return function (v) {
        //debugger;
        if (v == undefined) { return ""; }
        var re = /-?\d+/;
        var m = re.exec(v);
        if (parseInt(m) < 0) { return ""; }
        var d = new Date(parseInt(m[0]));
        return d.Format("yyyy");

    }
}]);

//秒转时分秒
filtersModule.filter('formatSeconds', ['$sce', function ($sce) {
    return function (value) {
        var second = parseInt(value); // 秒 
        var minute = 0; // 分 
        var hour = 0; // 小时 
        if (second >= 60) {
            minute = parseInt(second / 60);
            second = parseInt(second % 60);
            if (minute >= 60) {
                hour = parseInt(minute / 60);
                minute = parseInt(minute % 60);
            }
        }
        var sec = "";
        var result = "";
        if (second == 0) {
            sec = "00";
            result += sec;
        } else {
            if (second >= 10) {
                result += second;
            } else {
                result = result + "0" + second;
            }
        }
        if (minute >= 0) {
            if (minute >= 10) {
                result = "" + parseInt(minute) + ":" + result;
            } else {
                result = "0" + parseInt(minute) + ":" + result;
            }
        }
        if (hour >= 0) {
            if (hour >= 10) {
                result = "" + parseInt(hour) + ":" + result;
            } else {
                result = "0" + parseInt(hour) + ":" + result;
            }
        }
        return result;
    }
}]
)

Date.prototype.Format = function (fmt) { //author: meizz 
    var o = {
        "M+": this.getMonth() + 1, //月份 
        "d+": this.getDate(), //日 
        "h+": this.getHours(), //小时 
        "m+": this.getMinutes(), //分 
        "s+": this.getSeconds(), //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds() //毫秒 
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}

//转html
filtersModule.filter('toHtml', ['$sce', function ($sce) {
    return function (text) {
        return $sce.trustAsHtml(text);
    }
}]
)


//转换成时分秒
filtersModule.filter('FormatSeconds', function ($sce) {
    return function (seconds) {
        return $G2S.formatSeconds(seconds);
    }
});