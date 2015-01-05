jQuery(document).ready(function () {
   // $G2S.DomAdd("http://localhost:5093/css/footer.css");
    GetFoot();
});



function GetFoot() {
    var params = {
        action: "GetUserName"
    };
    var strHtml = "";
    strHtml += "	<div class='footer'>";
    strHtml += "    	<p class='copyright'><img src='/Images/company_logo2.png' width='70' height='30' alt=''>Copyright©2003-2014，版权所有 www.able-elec.com</p>";
    strHtml += "        <div class='company_info'>";
    strHtml += "        	<a href='#' target='_blank'>关于卓越</a>|";
    strHtml += "            <a href='#' target='_blank'>服务中心</a>|";
    strHtml += "            <a href='#' target='_blank'>版权说明</a>|";
    strHtml += "            ©2004-2015版权所有 上海卓越睿新数码科技有限公司";
    strHtml += "        </div>";
    strHtml += "    </div>";




    $("#div_Foot").html(strHtml);

}