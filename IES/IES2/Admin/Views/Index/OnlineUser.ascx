<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OnlineUser.ascx.cs" Inherits="Admin.Views.Index.OnlineUser" %>
<!--Step:1 Import a module loader, such as esl.js or require.js-->
<!--Step:1 引入一个模块加载器，如esl.js或者require.js-->
<script src="../../Frameworks/echarts-2.0.1/esl.js"></script>
<script src="../../Js/jquery-1.8.3.min.js"></script>
<style type="text/css">
    .main {
        height: 270px;
        width: 210px;
    }
</style>
<div class="side_item">
    <h4>用户在线消息</h4>
    <div class="online_box">
        <!--Step:2 Prepare a dom for ECharts which (must) has size (width & hight)-->
        <!--Step:2 为ECharts准备一个具备大小（宽高）的Dom-->
        <div id="main" class="main" style="border: 1px solid #ccc; padding-right:10px; padding-bottom:10px;">
        </div>
    </div>
    <script src="OnlineUser.js"></script>
</div>
