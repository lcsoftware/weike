$(document).ready(function () {
    LoadServerState();
});

function LoadServerState() {
    var url = "DiskSize.ashx";
    var params = {
        action: "LoadServerState"
    };
    var strHtml = "";
    var json = $G2S.GetAjaxJson(params, url);
    if (json != "") {
        strHtml += '                        <ul class="server_box">';
        //var rows = json.Rows[0];
        //var ResponseID = rows.ResponseID;
        //var Conten = unescape(rows.Conten);
        //var UserID = rows.UserID;
        strHtml += '                            <li>';
        strHtml += '                                <b>资料服务器</b>';
        strHtml += '                                <p>容量：2001G  |  已使用：1501G</p>';
        strHtml += '                                <span class="progress_bar"><em style="width:70%"></em></span>';
        strHtml += '                            </li>';
        strHtml += '                            <li>';
        strHtml += '                                <b>web服务器</b>';
        strHtml += '                                <p>容量500G  |  已使用213G</p>';
        strHtml += '                                <span class="progress_bar"><em style="width:50%"></em></span>';
        strHtml += '                            </li>';
        strHtml += '                            <li>';
        strHtml += '                                <b>数据库服务器</b>';
        strHtml += '                                <p>容量：5000G  |  已使用：457G</p>';
        strHtml += '                                <span class="progress_bar over_80"><em style="width:90%"></em></span>';
        strHtml += '                            </li>';
        strHtml += '                        </ul>';
    }
    else {
        strHtml += '                        <ul class="server_box">';
        strHtml += '                            <li>';
        strHtml += '                                <b>资料服务器</b>';
        strHtml += '                                <p>容量：未知  |  已使用：未知</p>';
        strHtml += '                                <span class="progress_bar"><em style="width:70%"></em></span>';
        strHtml += '                            </li>';
        strHtml += '                            <li>';
        strHtml += '                                <b>web服务器</b>';
        strHtml += '                                <p>容量未知  |  已使用未知</p>';
        strHtml += '                                <span class="progress_bar"><em style="width:50%"></em></span>';
        strHtml += '                            </li>';
        strHtml += '                            <li>';
        strHtml += '                                <b>数据库服务器</b>';
        strHtml += '                                <p>容量：未知  |  已使用：未知</p>';
        strHtml += '                                <span class="progress_bar over_80"><em style="width:90%"></em></span>';
        strHtml += '                            </li>';
        strHtml += '                        </ul>';
    }
    $("#online_box").html(strHtml);
}


