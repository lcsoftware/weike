<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="KnowledgeSpotTree8.ascx.cs" Inherits="Admin.Views.UserControl.KnowledgeSpotTree8" %>

<asp:Panel ID="palKnowledge" runat="server" BorderStyle="Solid" BorderWidth="1px" CssClass="panelOrg" BorderColor="#BFC4C9">
</asp:Panel>
<script type="text/javascript">

    KnowledgeNodeSelectSingle = function (elmtid) {
        if (document.getElementById(elmtid)) {
            var elmt = document.getElementById(elmtid);
            TreeView.ChangeStyle(elmt, 3, false);
            while (elmt.parentNode && (elmt.tagName == 'div' || elmt.tagName == 'DIV')) {
                if (elmt.parentNode.id.indexOf('_ChildContainer_') > -1) {
                    if (document.getElementById(elmt.parentNode.id.replace('_ChildContainer_', '_imgStatus_'))) {
                        document.getElementById(elmt.parentNode.id.replace('_ChildContainer_', '_imgStatus_')).className = 'treeExpand';
                    }
                }
                elmt.parentNode.style.display = 'block';
                elmt = elmt.parentNode;
            }
        }
    }
    KnowledgeNodeSelectMulti = function (palprefix, elmtids) {
        var arrids = elmtids.split(',');
        for (var i = 0; i < arrids.length; i++) {
            var elmtid = arrids[i];
            if (document.getElementById(palprefix + '_chkNodeInfo_' + elmtid)) {
                var elmt = document.getElementById(palprefix + '_chkNodeInfo_' + elmtid);
                TreeView.ChangeStyle(elmt, 3, true);
                var pnode = elmt.parentNode;
                while (pnode && pnode.style) {
                    if (pnode.style.display == 'none') {
                        pnode.style.display = 'block';
                    }
                    while (pnode.id.indexOf('_ChildContainer_') < 0 && pnode.parentNode && pnode.parentNode.style) {
                        pnode = pnode.parentNode;
                        if (pnode.style.display == 'none') {
                            pnode.style.display = 'block';
                        }
                    }
                    var imgstatus = document.getElementById(pnode.id.replace('_ChildContainer_', '_imgStatus_'));
                    if (imgstatus) {
                        imgstatus.className = 'treeExpand';
                    }
                    pnode = pnode.parentNode;
                }
            }
        }
    }
</script>
<img alt="trigger" src="Images/collspand.gif" runat="server" id="imgTrigger" style="display: none;" />
<input type="hidden" runat="server" id="hidSelectedNode" value="-1" name="hidSelectedNode" />
<input type="hidden" runat="server" id="hidTreeViewClientID" name="hidTreeViewClientID" value="" />