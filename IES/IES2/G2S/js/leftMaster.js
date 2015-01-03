
jQuery(document).ready(function () {
   // GetLeftMaster();
});


function GetLeftMaster() {
    var strHtml = "";
    strHtml += "<div class='side_left'>";
    strHtml += "            <!--用户信息开始-->";
    strHtml += "            <div class='user'>";
    strHtml += "                <img src='http://placehold.it/110x100/cccccc' width='110' height='100' />";
    strHtml += "                <a class='teacher_name' href='#'>米左MIZOO	</a>";
    strHtml += "                <a class='switch_btn' href='#'>教师端<i class='icon icon_arrow'></i></a>";
    strHtml += "                <span class='icon_24 icon_user'></span>";
    strHtml += "            </div>";
    strHtml += "            <!--用户信息结束-->";
    strHtml += "            <!--侧边导航开始-->";
    strHtml += "            <div class='side_box'>";
    strHtml += "                <ul class='side_nav'>";
    strHtml += "                    <li class='active'><a href='#'>首页</a></li>";
    strHtml += "                    <li><a href='#'>答疑论坛</a></li>";
    strHtml += "                    <li><a href='#'>作业考试</a></li>";
    strHtml += "                    <li><a href='#'>教学投票</a></li>";
    strHtml += "                    <li><a href='#'>小组教学</a></li>";
    strHtml += "                    <li><a href='#'>成绩档案</a></li>";
    strHtml += "                    <li><a href='#'>学习进度</a></li>";
    strHtml += "                    <li><a href='#'>课程运行</a></li>";
    strHtml += "                    <li><a href='#'>申请预约</a></li>";
    strHtml += "                    <li><a href='#'>评审工作</a></li>";
    strHtml += "                </ul>";
    strHtml += "                <a class='more_tool' href='javascript:;'>更多工具<i class='icon icon_more'></i></a>";
    strHtml += "            </div>";
    strHtml += "            ";
    strHtml += "            <!--侧边导航结束-->";
    strHtml += "        </div>";
    $("#div_Left").html(strHtml);
}