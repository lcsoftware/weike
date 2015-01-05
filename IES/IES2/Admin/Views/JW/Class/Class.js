var classModule = (function () {
    var param = []; //private
    return { //exposed to public
        alter: function (values) {
            basket.push(values);
        },
        getItemCount: function () {
            return basket.length;
        },
        del: function () {
            return $G2S.confirm("这是信息框", "aa()");
        },
        getTotal: function () {
            var q = this.getItemCount(), p = 0;
            while (q--) {
                p += basket[q].price;
            }
            return p;
        }
    }
}());


function Getalter()
{
    $G2S.alert("这是弹出框");
}

//确认框
function message()
{
   $G2S.confirm("这是信息框", "aa()");
}

function Del()
{
    $G2S.confirm("确定要删除吗？", "aa()");
}
//确认框 方法
function aa()
{
    alert("确认框方法");
    layer.closeAll();//关闭所有的层;
}

//弹出本页面层
var pageii;
function page2()
{
     pageii = $.layer({
        type: 1,
        title: false,
        area: ['auto', 'auto'],
        border: [0], //去掉默认边框
        shade: [0], //去掉遮罩
        closeBtn: [0, false], //去掉默认关闭按钮
        shift: 'left', //从左动画弹出
        page: {
            html: '<div style="width:420px; height:260px; padding:20px; border:1px solid #ccc; background-color:#eee;"><p>我从左边来，我自定了风格。</p><button id="pagebtn" class="btns" onclick="closepage2()">关闭</button></div>'
        },
        end: function (index) {
            alert(1);
           
           
        }
    });
}

function closepage2()
{
    layer.close(pageii);
}

//弹出其他页
function page3()
{
    $.layer({
        type: 2,
        shadeClose: true,
        title: false,
        closeBtn: [0, false],
        shade: [0.8, '#000'],
        border: [0],
        offset: ['20px', ''],
        area: ['1000px', ($(window).height() - 50) + 'px'],
        iframe: { src: 'http://192.168.4.100:5555/G2S/ShowSystem/Index.aspx' }
    });
}