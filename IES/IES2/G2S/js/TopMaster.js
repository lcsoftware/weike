jQuery(document).ready(function () {
    //$G2S.DomAdd("http://localhost:5093/js/index.js");
    //$G2S.DomAdd("http://localhost:5093/css/index.css");
    //$G2S.DomAdd("http://localhost:5093/css/header.css");
    //  GetTop();
});



function GetTop()
{
    var params = {
        action: "GetUserName"
    };
        var strHtml = "";
        strHtml += "<div class='public_header' id='topest'>";
        strHtml += "        <div class='header_center'>";
        strHtml += "            <div class='company_logo'>";
        strHtml += "                <img src='/Images/company_logo.png' width='70' height='30' alt=''>上海卓越睿新数码科技有限公司";
        strHtml += "            </div>";
        strHtml += "            <div class='user_box'>";
        strHtml += "                <p class='user_name'>";
        strHtml += "                    <img src='http://placehold.it/20x20/cccccc' width='20' height='20' />";
        strHtml += "                    张三";
        strHtml += "                    <span class='icon icon_arrow'></span>";
        strHtml += "                </p>";
        strHtml += "                <ul class='user_info'>";
        strHtml += "                    <li><a href='#'>消息中心</a></li>";
        strHtml += "                    <li><a href='#'>在线帮助</a></li>";
        strHtml += "                    <li><a href='#'>帐号管理</a></li>";
        strHtml += "                    <li><a href='#'>我的通讯录</a></li>";
        strHtml += "                    <li><a href='#'>我的回收站</a></li>";
        strHtml += "                    <li><a href='#'>返回主页</a></li>";
        strHtml += "                    <li><a href='#'>退出系统</a></li>";
        strHtml += "                </ul>";
        strHtml += "            </div>";
        strHtml += "            <ul class='nav_box'>";
        strHtml += "                <li class='active'><a href='#'>教学管理</a></li>";
        strHtml += "                <li><a href='#'>我的资源</a></li>";
        strHtml += "                <li><a href='#'>我的评价</a></li>";
        strHtml += "                <li><a href='#'>管理评估</a></li>";
        strHtml += "                <li><a href='#'>实践实习</a></li>";
        strHtml += "                <li><a href='#'>质量工程</a></li>";
        strHtml += "                <li><a href='#'>我的直播</a></li>";
        strHtml += "                <li><a href='#'>我的录制</a></li>";
        strHtml += "                <li><a href='#'>教务管理</a></li>";
        strHtml += "            </ul>";
        strHtml += "        </div>";
        strHtml += "    </div>";
    
    

    $("#div_Top").html(strHtml);

}