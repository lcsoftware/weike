<%@ Page  Title="组织机构编辑" MasterPageFile="~/Site.Master"   Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="Admin.Views.JW.Organization.Edit" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        var menudata = [{
            id: "0.1",//唯一的ID即可
            text: "Beyondbit UI Demo",
            hasChildren: true,
            isexpand: true,
            complete: true,
            ChildNodes: [{
                id: "0.1.1",
                text: "日期选择",
                hasChildren: true,
                isexpand: false,
                complete: true,
                ChildNodes: [{
                    id: "0.1.1.1",
                    text: "控件演示",
                    value: "Testpages/datepickerDemo.htm",
                    hasChildren: false,
                    isexpand: false,
                    complete: true,
                    ChildNodes: null
                }]
            }]
        }];
        </script>
    <div></div>
</asp:Content>
