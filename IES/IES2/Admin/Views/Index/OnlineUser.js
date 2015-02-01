
var data = {
    title: { text: '               (当前在线23人数)', subtext: '               (当前在线23人数)' },
    yAxis: '人',
    legend: ["教师", "学生"],
    xAxis: ["13点", "14点", "15点", "16点", "17点"],
    series: { value01: [1, 23, 45, 68, 70], value02: [14, 26, 56, 30, 50] }
};

$(document).ready(function () {

    OnlineUser("main", data);
});
function OnlineUser(main, data) {
    // Step:3 conifg ECharts's path, link to echarts.js from current page.
    // Step:3 为模块加载器配置echarts的路径，从当前页面链接到echarts.js，定义所需图表路径
    require.config({
        paths: {
            echarts: './../../Frameworks/echarts-2.0.1/echarts',
            'echarts/chart/bar': './../../Frameworks/echarts-2.0.1/echarts-map',
            'echarts/chart/line': './../../Frameworks/echarts-2.0.1/echarts-map'
        }
    });
    // Step:4 require echarts and use it in the callback.
    // Step:4 动态加载echarts然后在回调函数中开始使用，注意保持按需加载结构定义图表路径
    require(
    [
        'echarts',
        'echarts/chart/bar',
        'echarts/chart/line'
    ],
    function (ec) {
        //--- 折柱 ---
        var myChart = ec.init(document.getElementById(main));
        myChart.setOption({
            title: {
                text: data.title.text,
                subtext: data.title.subtext,
                orient: 'vertical',
                x: 'left',
                y: 'top',
                textStyle: {
                    fontSize: 12,
                    fontWeight: "",
                    color: "#999999"
                },
                subtextStyle: {
                    fontSize: 12,
                    fontWeight: "",
                    color: "#999999"
                }
            },
            tooltip: {
                trigger: 'axis'
            },
            legend: {
                data: data.legend,
                orient: 'vertical',
                x: 'left',
                y: 'top'
            },
            grid: {
                x: 40,//画图左侧距离
                y: 60,//画图上侧距离
                x2: 1,
                y2: 20,
                backgroundColor: "rgba(0,0,0,0)",
                borderWidth: 1,
                borderColor: "#ccc"
            },
            toolbox: {
                show: true,
                feature: {
                    mark: { show: false },
                    dataView: { show: false, readOnly: false },
                    magicType: { show: true, type: ['line', 'bar'] },
                    restore: { show: false },
                    saveAsImage: { show: false }
                },
                orient: "vertical"
            },
            calculable: true,
            xAxis: [
                {
                    type: 'category',
                    data: data.xAxis
                }
            ],
            yAxis: [
                {
                    type: 'value',
                    splitArea: { show: true },
                    axisLabel: {
                        formatter: '{value}' + data.yAxis
                    }
                }
            ],
            series: [
                { name: '教师', type: 'line', data: data.series.value01 },
                { name: '学生', type: 'line', data: data.series.value02 }
            ]
        });

    }
);
}
