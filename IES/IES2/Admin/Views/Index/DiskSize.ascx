<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DiskSize.ascx.cs" Inherits="Admin.Views.Index.DiskSize" %>
<script src="../../Js/G2S.js"></script>
<script src="DaskSize.js"></script>
<div class="side_item">
    <h4>服务器状态</h4>
    <div class="online_box" id="online_box">
        <ul class="server_box">
            <li>
                <b>资料服务器</b>
                <p>容量：2000G  |  已使用：1500G</p>
                <span class="progress_bar"><em style="width: 70%"></em></span>
            </li>
            <li>
                <b>web服务器</b>
                <p>容量5000G  |  已使用213G</p>
                <span class="progress_bar"><em style="width: 50%"></em></span>
            </li>
            <li>
                <b>数据库服务器</b>
                <p>容量：500G  |  已使用：457G</p>
                <span class="progress_bar over_80"><em style="width: 90%"></em></span>
            </li>
        </ul>
    </div>
</div>
