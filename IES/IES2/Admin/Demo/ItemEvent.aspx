<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ItemEvent.aspx.cs" Inherits="Admin.Demo.ItemEvent" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>OmGrid Demo</title>
   <link href="<%=ResolveUrl("~") %>css/ableico.css" rel="stylesheet" type="text/css" />
   
   <script src="<%=ResolveUrl("~") %>Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
   <link href="<%=ResolveUrl("~") %>Frameworks/OmGrid/css/OmGrid.css" rel="stylesheet" type="text/css" />
   <script src="<%=ResolveUrl("~") %>Frameworks/OmGrid/js/OmGrid-2.0-beta.js" type="text/javascript"></script>
   <style type="text/css">
   .pl10
   {
    padding-left:10px;	
   }
   </style>
<script type="text/javascript">
    var OmGrid1 = new OmGrid();
    var OmGrid2 = new OmGrid();
    $(document).ready(function () {
        OmGrid1.init("OmGrid1");
        OmGrid1.checkedID('2,4,7,123');
        //        OmGrid1.item(".lbl_FirstName").click(function(sender,id){
        //            sender.row();
        //            var e = sender.event;
        //            if(e.keyCode==13)
        //            {
        //                
        //            }
        //            alert(sender.row(".lbl_Sex").html());
        //        });

        //为下拉菜单绑定事件
        var omItem = OmGrid1.item(".list_Edit");
        omItem.click(function (sender, id) {
            alert("操作:编辑,id:" + id);
        });
        OmGrid1.item(".list_Detail").click(function (sender, id) {
            alert("操作:详细,id:" + id);
        });
        OmGrid1.item(".list_Del").click(function (sender, id) {
            alert("操作:删除,id:" + id);
        });

        //checkbox点击时事件 控制顶部的按钮状态
        OmGrid1.title("[name='ckbAll']").click(function () {
            checkButton();
        });

        OmGrid1.item("[name='ckbItem']").click(function (sender, id) {
            checkButton();
        });

        OmGrid1.item(".input_LastName").blur(function (sender, id) {
            //sender.row()方法获取触发事件所在的行内的所有对象
            var firstName = sender.row(".lbl_FirstName").html();
            var age = sender.row(".lbl_Age").html();
            var sex = sender.row(".lbl_Sex").html();

            alert("操作:行内编辑,id:" + id + ",lastName修改为:" + sender.val() +
            "\n\n修改后的信息为:\n\t姓名：" + firstName + sender.val() + "\n\t性别:" + sex + "\n\t年龄:" + age);
        });

        //lastName文本框内按回车
        OmGrid1.item(".input_LastName").bind("keydown", function (sender, id) {
            var e = sender.event; //sender中封装event对象
            if (e.keyCode == 13) {

                $("#btnOK").focus(); //焦点移至确定按钮上 触发文本框的失去焦点事件
                e.keyCode = 0;
            }
        });

        //绑定同上
        OmGrid2.init("OmGrid2");
    });


    var checkButton = function () {

        var ids = OmGrid1.checkedID();
        if (ids.indexOf(',') > -1) //选中多项
        {
            $("#edit").attr("disabled", true);
            $("#detail").attr("disabled", true);
            $("#del").attr("disabled", false);
        } else if (ids.length > 0) //选中一项
        {
            $("#edit").attr("disabled", false);
            $("#detail").attr("disabled", false);
            $("#del").attr("disabled", false);
        } else {
            $("#edit").attr("disabled", true);
            $("#detail").attr("disabled", true);
            $("#del").attr("disabled", true);
        }
    };

    var btnEdit_Click = function () {
        alert("操作:顶部按钮编辑,id:" + OmGrid1.checkedID());
    }

    var btnDetail_Click = function () {
        alert("操作:顶部按钮查看,id:" + OmGrid1.checkedID());
    }

    var btnDel_Click = function () {
        alert("操作:顶部按钮删除,id:" + OmGrid1.checkedID());
    }

</script>
</head>
<body>
    <form id="form1" runat="server">
<h1 style="background-color:Gray;text-align:center">OmGrid Demo</h1>
<div style="margin:0px auto;width:910px;border:solid 1px Gray;">
    <div style="width:190px;float:left;padding:5px;">
    <br />
    <ol>
        <li><a href="Basic.aspx">基本页</a></li>
        <li><a href="ItemEvent.aspx">为列表绑定事件</a></li>
        <li><a href="RowSelect.aspx">切换为行点击模式</a></li>
        <li><a href="AjaxData.aspx">异步加载的数据列表</a></li>
    </ol>
    </div>
    <div style="width:700px;float:right;padding:5px;">

<h3>为OmGrid绑定事件</h3>
<label id=lbl1></label>
  <input type="button" id="edit" value="编辑" disabled="disabled" onclick="btnEdit_Click()" />
  <input type="button" id="detail"  value="详细"disabled="disabled" onclick="btnDetail_Click()" />
  <input id="del"  type="button" value="删除"disabled="disabled" onclick="btnDel_Click()" />
  <div id="OmGrid1" style="width:700px;z-index:999">
            <asp:Repeater ID="rptInfo" runat="server">
                <HeaderTemplate>
                   <table border="0" cellpadding="0" cellspacing="0" class='wp100'>
                      <colgroup>
                          <col width="30" /><col width="50" /><col width="210" /><col width="210" /><col width="80" /><col width="90" /><col width="30" />
                      </colgroup>
                     <tr class="item_title">
                         <td><input type="checkbox" name="ckbAll" />
                         </td>
                         <td>ID</td>
                         <td>FirstName</td>
                         <td>LastName</td>
                         <td>Age</td>
                         <td>Sex</td>
                         <td></td>
                     </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="item item_row" id="<%#Eval("ID")%>">
                        <td><input type="checkbox" name="ckbItem"/>
                        </td>
                        <td><%#Eval("ID")%></td>
                        <td><label class="lbl_FirstName"><%#Eval("FirstName")%></label><%--class="lbl_FirstName" 用于寻找和定位该元素 --%></td>
                        <td><input type=text class="item_input input_LastName" value="<%#Eval("LastName")%>" /><%--class="input_LastName" 用于寻找和定位该元素 --%></td>
                        <td><label class="lbl_Age"><%#Eval("Age")%></label><%--class="lbl_Age" 用于寻找和定位该元素 --%></td>
                        <td><label class="lbl_Sex"><%#Eval("Sex")%></label><%--class="lbl_Sex" 用于寻找和定位该元素 --%></td>
                        <td>
                            <div class='item_btn item_btnS'></div>
                            <div class="item_jslist" >
                                <div class="list_Edit"><%--class="list_Edit" 为下拉框定义样式 用于绑定事件 --%>
                                    <div class='edit btns ml5_f'></div><span class='pl10'>编辑</span>
                                </div>
                                <div class="list_Detail"><%--class="list_Detail" 为下拉框定义样式 用于绑定事件 --%>
                                    <div class='detail btns ml5_f'></div><span class='pl10'>查看</span>
                                </div>
                                <div class="list_Del"><%--class="list_Del" 为下拉框定义样式 用于绑定事件 --%>
                                    <div class='del btns ml5_f'></div><span class='pl10'>删除</span>
                                </div>
                            </div>
                        </td>
                    </tr>
                </ItemTemplate>
                <SeparatorTemplate>
                </SeparatorTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
        <div style="text-align:right"><input type="button" id="btnOK" value="确定" /></div>
  <br />
  ----------------------------------------------------------------------------------
  <br /><br />
  <div id="OmGrid2" style="width:700px;z-index:0">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <div id="<%#Eval("ID")%>" class="item" style="border:solid 1px Black;">
                    <table cellpadding=0 cellspacing=0 style="background-color:#999;width:700px;">
                        <tr>
                            <td style="width:600px"><%#Eval("FirstName")%> <%#Eval("LastName")%></td>
                            <td>
                                <div class='item_btn item_btnS'></div>
                                <div class="item_jslist" >
                                    <div><div class='edit btns ml5_f'></div><span class='pl10'>编辑</span></div>
                                    <div><div class='detail btns ml5_f'></div><span class='pl10'>查看</span></div>
                                    <div><div class='del btns ml5_f'></div><span class='pl10'>删除</span></div>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <div style="line-height:25px;">
                    性别：<%#Eval("Age")%><br />
                    年龄：<%#Eval("Sex")%>
                    </div>
                </div>
            </ItemTemplate>
            <SeparatorTemplate>
            <hr style="width:90%"/>
            </SeparatorTemplate>
        </asp:Repeater>
    </div>



    </div>
    <div style="clear:both"></div>
    </div>
</form>
</body>
</html>
