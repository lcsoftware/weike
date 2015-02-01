
/*
  @Name:课程中心4.0基础类
  @Date:2014.07.21
  @Author: 郭凯炬
  @Versions:1.0
*/


$(function () {
});

var $G2S = {};

//动态引用js或者css
$G2S.DomAdd = function (str) {
    var pos = str.lastIndexOf(".");
    var su = str.substring(pos + 1);
    if (su.toLowerCase() == "js") {
        var oHead = document.getElementsByTagName('HEAD').item(0);
        var oScript = document.createElement("script");
        oScript.type = "text/javascript";
        oScript.src = str;
        oHead.appendChild(oScript);
    } else {
        var doc = document;
        var link = doc.createElement("link");
        link.setAttribute("rel", "stylesheet");
        link.setAttribute("type", "text/css");
        link.setAttribute("href", str);
        var heads = doc.getElementsByTagName("head");
        if (heads.length) { heads[0].appendChild(link); }
        else { doc.documentElement.appendChild(link); }
    }
};
//判断引用的js,css 文件是否存在
$G2S.isInclude = function (name) {
    var js = /js$/i.test(name);
    var es = document.getElementsByTagName(js ? 'script' : 'link');
    for (var i = 0; i < es.length; i++)
        if (es[i][js ? 'src' : 'href'].indexOf(name) != -1) return true;
    return false;
};

//根据参数读取一般处理程序 返回json
$G2S.GetAjaxJson = function (params, url) {
    var strdata;
    $.ajax({
        url: url, data: params, type: 'post', cache: false, async: false,
        success: function (data) {
            if (data != "empty") {
                strdata = data.replace(/[\r\n]/g, "\\r\\n");
                strdata = eval("(" + strdata + ")");
            } else {
                strdata = "暂无数据";
            }

        }
    });
    return strdata;

};


//根据参数读取一般处理程序 返回XML
$G2S.GetAjaxXML = function (params, url) {

    var strdata;
    $.ajax({
        url: url, data: params, type: 'post', cache: false, async: false,
        success: function (data) {
            if (data != "empty") {
                strdata = data;
            } else {
                strdata = "暂无数据";
            }

        }
    });
    return strdata;

};


//根据参数读取一般处理程序 返回bool
$G2S.GetAjaxNoPar = function (params, url) {

    var strdata;
    $.ajax({
        url: url, data: params, type: 'post', cache: false, async: false,
        success: function (data) {
            strdata = data;

        }
    });
    return strdata;

};



//播放视频
$G2S.Play = function (data) {
    var thePlayer = jwplayer(data.id);
    thePlayer.setup({
        flashplayer: data.flash,
        file: data.url,
        width: data.width,
        height: data.height,
        hide: true,
        title: 'Able',
        startparam: 'start',
        provider: 'http'
    });
    if (data.autoplay) {
        thePlayer.play();
    }
};


//秒转换为时分秒
$G2S.formatSeconds = function (value) {
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
};

//时分秒转换成秒
$G2S.GetSecond = function (value) {
    var strvalue = "时间格式错误";
    if (value.indexOf(":") > -1) {
        var vrrstr = value.split(':');
        if (vrrstr.length == 3) {
            strvalue = parseInt(vrrstr[0]) * 3600 + parseInt(vrrstr[1]) * 60 + parseInt(vrrstr[2]);
        }
    }
    return strvalue;
}

//弹出框
$G2S.alert = function () {
    var len = arguments.length;
    if (len == 1) {
        layer.alert(arguments[0], -1, "提示框");
    } else if (len == 2) {
        layer.alert(arguments[0], -1, arguments[1]);
    } else if (len == 3) {
        layer.alert(arguments[0], arguments[2], arguments[1]);
    }
}
$G2S.message = function () {
    var len = arguments.length;
    if (len == 1) {
        layer.msg(arguments[0], 2, -1);
    } else if (len == 2) {
        layer.msg(arguments[0], 2, arguments[1]);
    }
}
$G2S.confirm = function (title, code) {
    layer.confirm(title, function () {
        eval(code());
    });
}



//md5加密
$G2S.md5 = function (str) {
    return hex_md5(str);
}

//获取后缀名
$G2S.Ext = function (str) {
    var pos = str.lastIndexOf(".");
    //截取点之后的字符串
    var su = str.substring(pos);
    return su;
}


//获取url参数
$G2S.request = function (paras) {
    var url = location.href;
    var paraString = url.substring(url.indexOf("?") + 1, url.length).split("&");
    var paraObj = {}
    for (i = 0; j = paraString[i]; i++) {
        paraObj[j.substring(0, j.indexOf("=")).toLowerCase()] = j.substring(j.indexOf("=") + 1, j.length);
    }
    var returnValue = paraObj[paras.toLowerCase()];
    if (typeof (returnValue) == "undefined") {
        return "";
    } else {
        return returnValue;
    }
};

//读取XML 
$G2S.getXMLDoc = function (xmlFile) {
    var xmlDoc;
    if (window.ActiveXObject || "ActiveXObject" in window) {
        xmlDoc = new ActiveXObject('Microsoft.XMLDOM');//IE浏览器
        xmlDoc.async = false;
        xmlDoc.load(xmlFile);
    }
    else if (isFirefox = navigator.userAgent.indexOf("Firefox") > 0) { //火狐浏览器
        xmlDoc = document.implementation.createDocument('', '', null);
        xmlDoc.async = false;
        xmlDoc.load(xmlFile);
    }
    else { //谷歌浏览器
        var xmlhttp = new window.XMLHttpRequest();
        xmlhttp.open("GET", xmlFile, false);
        xmlhttp.send(null);
        if (xmlhttp.readyState == 4) {
            xmlDoc = xmlhttp.responseXML;
        }
    }
    return xmlDoc;
}

//获取节点值:当前浏览器版本 
$G2S.getXMLDocNode = function (xmlDoc, nodeNum) {
    //strIE, strFF, strGL 分别为IE,firefox,google浏览器对应的节点值
    var nodes = xmlDoc.getElementsByTagName('banben')[0].childNodes;
    var value = 0;
    if (window.ActiveXObject || "ActiveXObject" in window) {
        var strIE = nodeNum;
        value = nodes.item(strIE).text;
    } else if (isFirefox = navigator.userAgent.indexOf("Firefox") > 0) {
        var strFF = parseInt(nodeNum) * 2 + 1;
        value = nodes.item(strFF).innerHTML;
    } else {
        var strGL = parseInt(nodeNum) * 2 + 1;
        value = nodes.item(strGL).childNodes[0].data;
    }

    if (value == 1) {
        return true;
    }
    else {
        return false;
    }
}


//最新取xml 兼容各种浏览器 亲测gkj

$G2S.GetACC3FP = function (xmlFile, nodeName) {
    var flag = 0;
    var acc = false;
    $.ajax({
        url: xmlFile, data: "", type: 'get', cache: false, async: false,
        success: function (data) {
            $(data).find(nodeName).each(function () {
                flag = $(this).text();
            });
            if (flag == 1) {
                acc = true;
            } else {
                acc = false;
            }

        }
    });
    return acc;

}


//圆形统计图
$G2S.Pie = function (id, rale, color) {
    var other;
    other = 100 - rale;   //t:完成所占的百分比
    var myChart = echarts.init(document.getElementById(id));
    myChart.setOption({
        animation: true, //不开启动画
        series: [
        {
            type: 'pie', //pie  圆形
            radius: ['90%', '75%'],
            itemStyle: {
                normal: {
                    color: color,  //图形颜色'#00846F'
                    label: { show: false },
                    labelLine: { show: false }
                },
                emphasis: {
                    color: 'rgba(0,0,0,0)'
                }
            },
            data: [
                {
                    value: other,
                    itemStyle: {
                        normal: {
                            color: '#eee',  //其他的颜色
                            label: { show: false },
                            labelLine: { show: false }
                        },
                        emphasis: {
                            color: 'rgba(0,0,0,0)'
                        }
                    }
                },
                {
                    value: rale,
                    name: rale + "%", //图形中间的文字
                    itemStyle: {
                        normal: {
                            label: {
                                show: true,
                                position: 'center',
                                textStyle: {
                                    fontSize: '25',
                                    color: color
                                }
                            },

                            labelLine: {
                                show: false
                            }
                        }
                    }
                }
            ]
        }
        ]

    });
}

//圆形统计图
$G2S.PieNoCartoon = function (id, rale, color) {
    var other;
    other = 100 - rale;   //t:完成所占的百分比
    var myChart = echarts.init(document.getElementById(id));
    myChart.setOption({
        animation: false, //不开启动画
        series: [
        {
            type: 'pie', //pie  圆形
            radius: ['90%', '75%'],
            itemStyle: {
                normal: {
                    color: color,  //图形颜色'#00846F'
                    label: { show: false },
                    labelLine: { show: false }
                },
                emphasis: {
                    color: 'rgba(0,0,0,0)'
                }
            },
            data: [
                {
                    value: other,
                    itemStyle: {
                        normal: {
                            color: '#eee',  //其他的颜色
                            label: { show: false },
                            labelLine: { show: false }
                        },
                        emphasis: {
                            color: 'rgba(0,0,0,0)'
                        }
                    }
                },
                {
                    value: rale,
                    name: rale, //图形中间的文字
                    itemStyle: {
                        normal: {
                            label: {
                                show: true,
                                position: 'center',
                                textStyle: {
                                    fontSize: '15',
                                    color: color
                                }
                            },

                            labelLine: {
                                show: false
                            }
                        }
                    }
                }
            ]
        }
        ]

    });
}

//一根折线图
$G2S.Line = function (id, data) {
    //data = {
    //    title: "分数分布",
    //    desc: { formatter: '人', name: '人数' },
    //    legend: ["百分制"],
    //    key: ["50分以下", "60~70", "70~80", "80~90", "90~"],
    //    value: [14, 10, 2, 23, 10]
    //}
    var myChart = echarts.init(document.getElementById(id));
    myChart.setOption({
        title: {
            text: data.title
        },
        tooltip: {
            trigger: 'axis'
        },
        legend: {
            data: data.legend
        },
        toolbox: {
            show: true,
            feature: {
                magicType: { show: true, type: ['line', 'bar'] },
                restore: { show: true },
                saveAsImage: { show: false }
            }
        },
        calculable: true,
        xAxis: [
            {
                type: 'category',
                boundaryGap: false,
                data: data.key
            }
        ],
        yAxis: [
            {
                type: 'value',
                axisLabel: {
                    formatter: '{value}' + data.desc.formatter
                }
            }
        ],
        series: [
            {
                name: data.desc.name,
                type: 'line',
                data: data.value
            }
        ]
    }
    );
}

//层拖动
$G2S.moveEvent = function (e, id) {
    var isIE = (document.all) ? true : false;
    drag = true;
    xx = isIE ? event.x : e.pageX;
    yy = isIE ? event.y : e.pageY;
    L = document.getElementById(id).offsetLeft;
    T = document.getElementById(id).offsetTop;

    document.onmousemove = function (e) {
        if (drag) {
            x = isIE ? event.x : e.pageX; if (x < 0) x = 0;
            y = isIE ? event.y : e.pageY; if (y < 0) y = 0;
            
            $("#" + id).css("left", L - xx + x);
            $("#" + id).css("top", T - yy + y);
        }
    }
    document.onmouseup = function () {
        drag = false;
    }
}

var hexcase = 0; function hex_md5(a) { if (a == "") return a; return rstr2hex(rstr_md5(str2rstr_utf8(a))) } function hex_hmac_md5(a, b) { return rstr2hex(rstr_hmac_md5(str2rstr_utf8(a), str2rstr_utf8(b))) } function md5_vm_test() { return hex_md5("abc").toLowerCase() == "900150983cd24fb0d6963f7d28e17f72" } function rstr_md5(a) { return binl2rstr(binl_md5(rstr2binl(a), a.length * 8)) } function rstr_hmac_md5(c, f) { var e = rstr2binl(c); if (e.length > 16) { e = binl_md5(e, c.length * 8) } var a = Array(16), d = Array(16); for (var b = 0; b < 16; b++) { a[b] = e[b] ^ 909522486; d[b] = e[b] ^ 1549556828 } var g = binl_md5(a.concat(rstr2binl(f)), 512 + f.length * 8); return binl2rstr(binl_md5(d.concat(g), 512 + 128)) } function rstr2hex(c) { try { hexcase } catch (g) { hexcase = 0 } var f = hexcase ? "0123456789ABCDEF" : "0123456789abcdef"; var b = ""; var a; for (var d = 0; d < c.length; d++) { a = c.charCodeAt(d); b += f.charAt((a >>> 4) & 15) + f.charAt(a & 15) } return b } function str2rstr_utf8(c) { var b = ""; var d = -1; var a, e; while (++d < c.length) { a = c.charCodeAt(d); e = d + 1 < c.length ? c.charCodeAt(d + 1) : 0; if (55296 <= a && a <= 56319 && 56320 <= e && e <= 57343) { a = 65536 + ((a & 1023) << 10) + (e & 1023); d++ } if (a <= 127) { b += String.fromCharCode(a) } else { if (a <= 2047) { b += String.fromCharCode(192 | ((a >>> 6) & 31), 128 | (a & 63)) } else { if (a <= 65535) { b += String.fromCharCode(224 | ((a >>> 12) & 15), 128 | ((a >>> 6) & 63), 128 | (a & 63)) } else { if (a <= 2097151) { b += String.fromCharCode(240 | ((a >>> 18) & 7), 128 | ((a >>> 12) & 63), 128 | ((a >>> 6) & 63), 128 | (a & 63)) } } } } } return b } function rstr2binl(b) { var a = Array(b.length >> 2); for (var c = 0; c < a.length; c++) { a[c] = 0 } for (var c = 0; c < b.length * 8; c += 8) { a[c >> 5] |= (b.charCodeAt(c / 8) & 255) << (c % 32) } return a } function binl2rstr(b) { var a = ""; for (var c = 0; c < b.length * 32; c += 8) { a += String.fromCharCode((b[c >> 5] >>> (c % 32)) & 255) } return a } function binl_md5(p, k) { p[k >> 5] |= 128 << ((k) % 32); p[(((k + 64) >>> 9) << 4) + 14] = k; var o = 1732584193; var n = -271733879; var m = -1732584194; var l = 271733878; for (var g = 0; g < p.length; g += 16) { var j = o; var h = n; var f = m; var e = l; o = md5_ff(o, n, m, l, p[g + 0], 7, -680876936); l = md5_ff(l, o, n, m, p[g + 1], 12, -389564586); m = md5_ff(m, l, o, n, p[g + 2], 17, 606105819); n = md5_ff(n, m, l, o, p[g + 3], 22, -1044525330); o = md5_ff(o, n, m, l, p[g + 4], 7, -176418897); l = md5_ff(l, o, n, m, p[g + 5], 12, 1200080426); m = md5_ff(m, l, o, n, p[g + 6], 17, -1473231341); n = md5_ff(n, m, l, o, p[g + 7], 22, -45705983); o = md5_ff(o, n, m, l, p[g + 8], 7, 1770035416); l = md5_ff(l, o, n, m, p[g + 9], 12, -1958414417); m = md5_ff(m, l, o, n, p[g + 10], 17, -42063); n = md5_ff(n, m, l, o, p[g + 11], 22, -1990404162); o = md5_ff(o, n, m, l, p[g + 12], 7, 1804603682); l = md5_ff(l, o, n, m, p[g + 13], 12, -40341101); m = md5_ff(m, l, o, n, p[g + 14], 17, -1502002290); n = md5_ff(n, m, l, o, p[g + 15], 22, 1236535329); o = md5_gg(o, n, m, l, p[g + 1], 5, -165796510); l = md5_gg(l, o, n, m, p[g + 6], 9, -1069501632); m = md5_gg(m, l, o, n, p[g + 11], 14, 643717713); n = md5_gg(n, m, l, o, p[g + 0], 20, -373897302); o = md5_gg(o, n, m, l, p[g + 5], 5, -701558691); l = md5_gg(l, o, n, m, p[g + 10], 9, 38016083); m = md5_gg(m, l, o, n, p[g + 15], 14, -660478335); n = md5_gg(n, m, l, o, p[g + 4], 20, -405537848); o = md5_gg(o, n, m, l, p[g + 9], 5, 568446438); l = md5_gg(l, o, n, m, p[g + 14], 9, -1019803690); m = md5_gg(m, l, o, n, p[g + 3], 14, -187363961); n = md5_gg(n, m, l, o, p[g + 8], 20, 1163531501); o = md5_gg(o, n, m, l, p[g + 13], 5, -1444681467); l = md5_gg(l, o, n, m, p[g + 2], 9, -51403784); m = md5_gg(m, l, o, n, p[g + 7], 14, 1735328473); n = md5_gg(n, m, l, o, p[g + 12], 20, -1926607734); o = md5_hh(o, n, m, l, p[g + 5], 4, -378558); l = md5_hh(l, o, n, m, p[g + 8], 11, -2022574463); m = md5_hh(m, l, o, n, p[g + 11], 16, 1839030562); n = md5_hh(n, m, l, o, p[g + 14], 23, -35309556); o = md5_hh(o, n, m, l, p[g + 1], 4, -1530992060); l = md5_hh(l, o, n, m, p[g + 4], 11, 1272893353); m = md5_hh(m, l, o, n, p[g + 7], 16, -155497632); n = md5_hh(n, m, l, o, p[g + 10], 23, -1094730640); o = md5_hh(o, n, m, l, p[g + 13], 4, 681279174); l = md5_hh(l, o, n, m, p[g + 0], 11, -358537222); m = md5_hh(m, l, o, n, p[g + 3], 16, -722521979); n = md5_hh(n, m, l, o, p[g + 6], 23, 76029189); o = md5_hh(o, n, m, l, p[g + 9], 4, -640364487); l = md5_hh(l, o, n, m, p[g + 12], 11, -421815835); m = md5_hh(m, l, o, n, p[g + 15], 16, 530742520); n = md5_hh(n, m, l, o, p[g + 2], 23, -995338651); o = md5_ii(o, n, m, l, p[g + 0], 6, -198630844); l = md5_ii(l, o, n, m, p[g + 7], 10, 1126891415); m = md5_ii(m, l, o, n, p[g + 14], 15, -1416354905); n = md5_ii(n, m, l, o, p[g + 5], 21, -57434055); o = md5_ii(o, n, m, l, p[g + 12], 6, 1700485571); l = md5_ii(l, o, n, m, p[g + 3], 10, -1894986606); m = md5_ii(m, l, o, n, p[g + 10], 15, -1051523); n = md5_ii(n, m, l, o, p[g + 1], 21, -2054922799); o = md5_ii(o, n, m, l, p[g + 8], 6, 1873313359); l = md5_ii(l, o, n, m, p[g + 15], 10, -30611744); m = md5_ii(m, l, o, n, p[g + 6], 15, -1560198380); n = md5_ii(n, m, l, o, p[g + 13], 21, 1309151649); o = md5_ii(o, n, m, l, p[g + 4], 6, -145523070); l = md5_ii(l, o, n, m, p[g + 11], 10, -1120210379); m = md5_ii(m, l, o, n, p[g + 2], 15, 718787259); n = md5_ii(n, m, l, o, p[g + 9], 21, -343485551); o = safe_add(o, j); n = safe_add(n, h); m = safe_add(m, f); l = safe_add(l, e) } return Array(o, n, m, l) } function md5_cmn(h, e, d, c, g, f) { return safe_add(bit_rol(safe_add(safe_add(e, h), safe_add(c, f)), g), d) } function md5_ff(g, f, k, j, e, i, h) { return md5_cmn((f & k) | ((~f) & j), g, f, e, i, h) } function md5_gg(g, f, k, j, e, i, h) { return md5_cmn((f & j) | (k & (~j)), g, f, e, i, h) } function md5_hh(g, f, k, j, e, i, h) { return md5_cmn(f ^ k ^ j, g, f, e, i, h) } function md5_ii(g, f, k, j, e, i, h) { return md5_cmn(k ^ (f | (~j)), g, f, e, i, h) } function safe_add(a, d) { var c = (a & 65535) + (d & 65535); var b = (a >> 16) + (d >> 16) + (c >> 16); return (b << 16) | (c & 65535) } function bit_rol(a, b) { return (a << b) | (a >>> (32 - b)) };