jQuery(document).ready(function () {
    GetApplyCourse();

});


//获取适用课程
function GetApplyCourse()
{
    var params = {
        action: "GetApplyCourse",
        webSite: 5
    };
    var url = "Exercise.ashx";
    var strHtml = "";
    var json = $G2S.GetAjaxJson(params, url);
    if (json != "暂无数据") {
        for (var i = 0; i < json.Rows.length; i++) {
            var rows = json.Rows[i]; 
            $(".which_course").append("<option value='" + rows.OCID + "'>" + rows.Name + "</option>");
        }
    }
}