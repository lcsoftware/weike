var treeviewlist = new Array();
var TreeView = {
    isshowcheckbox: true,
    selectedid: null
};

TreeView.AddTreeView = function(imgelmt, ismulti) {
    treeviewlist[treeviewlist.length] = imgelmt.id.replace('imgTrigger', 'palKnowledge');
    TreeView.isshowcheckbox = ismulti;
}
TreeView.GetSelectedNode = function(usercontrolid) {
    var selectedvalues = new Array();
    if (TreeView.isshowcheckbox) {
        var knowlegenodes = document.getElementsByName(usercontrolid + '_palKnowledge_chkKnowlegeNodeInfo');
        for (var i = 0; i < knowlegenodes.length; i++) {
            if (knowlegenodes[i].checked) {
                selectedvalues.push(knowlegenodes[i].value.split('_')[1]);
            }
        }
    }
    else {
        if (TreeView.selectedid) {
            selectedvalues[0] = TreeView.selectedid;
        }
        else {
            selectedvalues[0] = '';
        }
    }
    return selectedvalues;
}


//获取子节点
TreeView.GetChildNodes = function(parentelmt) {
    var pid = parentelmt.value.split('_')[1];
    var knowlegenodes = document.getElementsByName(parentelmt.id.replace('_palKnowledge_chkNodeInfo_' + pid, '_palKnowledge_chkKnowlegeNodeInfo'));
    var childs = new Array();
    for (var i = 0; i < knowlegenodes.length; i++) {
        var childp = knowlegenodes[i].value.split('_');
        if (childp[0] == pid) {
            childs[childs.length] = knowlegenodes[i];
        }
    }
    return childs;
}
TreeView.ChangeStyle = function(elmt, type, ismulti) {
    TreeView.isshowcheckbox = ismulti;
    var hidselectnodeid = elmt.id.substring(0, elmt.id.lastIndexOf('_ChildNode_')).replace('palKnowledge', 'hidSelectedNode');
    var hidSelectedNode = document.getElementById(hidselectnodeid);

    if (type == 1) {//鼠标划过
        if (ismulti) {
            if (!document.getElementById(elmt.id.replace('_ChildNode_', '_chkNodeInfo_')).checked) {
                elmt.style.backgroundColor = '#E6E9EB';
            }
        }
        else {
            if (elmt.id != hidSelectedNode.value) {
                elmt.style.backgroundColor = '#E6E9EB';
            }
        }
    }
    else if (type == 2) {//鼠标移出
        if (ismulti) {
            if (!document.getElementById(elmt.id.replace('_ChildNode_', '_chkNodeInfo_')).checked) {
                elmt.style.backgroundColor = '';
            }
        }
        else {
            if (elmt.id != hidSelectedNode.value) {
                elmt.style.backgroundColor = '';
            }
        }
    }
    else if (type == 3) {//鼠标点击
        if (ismulti) {
            if (elmt.checked) {
                document.getElementById(elmt.id.replace('_chkNodeInfo_', '_ChildNode_')).style.backgroundColor = '#FAF2D2';
            }
            else {
                document.getElementById(elmt.id.replace('_chkNodeInfo_', '_ChildNode_')).style.backgroundColor = 'White';
            }
            var childnodes = this.GetChildNodes(elmt);
            for (var i = 0; i < childnodes.length; i++) {
                if (!childnodes[i].checked) {
                    childnodes[i].checked = elmt.checked;
                    this.ChangeStyle(childnodes[i], 3, ismulti);
                }
                //qy 反选没效果的添加 start
                else if (childnodes[i].checked) {
                    childnodes[i].checked = elmt.checked;
                    this.ChangeStyle(childnodes[i], 3, ismulti);
                }
                //end
            }
            //$("#" + btnClicked).click();
        }
        else {
            //2010年9月26日15:12:58注释by蒋宏，修改G2S/AdminSpace/BaseData/CourseAdd.aspx页面勾选树前面checkbox样式错误
            if (!ismulti) {
                if (document.getElementById(hidSelectedNode.value)) {
                    document.getElementById(hidSelectedNode.value).style.backgroundColor = '';
                }
                hidSelectedNode.value = elmt.id;
                TreeView.selectedid = elmt.id.split('_')[elmt.id.split('_').length - 1];
                elmt.style.backgroundColor = '#FAF2D2';
            }
        }
        if (document.getElementById(elmt.id.replace('_ChildNode_', '_chkNodeInfo_'))) {
            if (document.getElementById(elmt.id.replace('_ChildNode_', '_chkNodeInfo_')).style.display != 'inline') {
                //取知识点信息
            }
        }
    }
}

TreeView.GetChild = function(elmt) {
    var childcontainer = document.getElementById(elmt.id.replace('_imgStatus_', '_ChildContainer_'));
    if (elmt.className == 'treeCollspand') {
        elmt.className = 'treeExpand';
        if (childcontainer) {
            childcontainer.style.display = 'block';
        }
    }
    else {
        if (childcontainer) {
            childcontainer.style.display = 'none';
            elmt.className = 'treeCollspand';
        }
    }
}
//###########################
TreeView.NodeChecked = function(elmt) {
    if (elmt.checked) {
        document.getElementById(elmt.id.replace('_chkNodeInfo_', '_ChildNode_')).style.backgroundColor = '#FAF2D2';
    }
    else {
        document.getElementById(elmt.id.replace('_chkNodeInfo_', '_ChildNode_')).style.backgroundColor = 'White';
    }
  
}

//Edit by bxlong 2013-7-1
TreeView.GetAllChildChecked = function(elmt) {
   
   if(elmt.id!=(elmt.id.split('_')[0]+"_palKnowledge_chkNodeInfo_0"))
      {
      document.getElementById(elmt.id.split('_')[0]+"_palKnowledge_chkNodeInfo_0").checked=false;
      }
      
    if (elmt.checked) {
        document.getElementById(elmt.id.replace('_chkNodeInfo_', '_ChildNode_')).style.backgroundColor = '#FAF2D2';
    }
    else {
        document.getElementById(elmt.id.replace('_chkNodeInfo_', '_ChildNode_')).style.backgroundColor = 'White';
    }
    //----------
    var childcontainer = document.getElementById(elmt.id.replace('_chkNodeInfo_', '_ChildContainer_'));
    if (childcontainer) {
        var checks = childcontainer.getElementsByTagName("input");
        for (var j = 0; j < checks.length; j++) {
            checks[j].checked = elmt.checked;
            if (checks[j].checked) {
                document.getElementById(checks[j].id.replace('_chkNodeInfo_', '_ChildNode_')).style.backgroundColor = '#FAF2D2';
            }
            else {
                document.getElementById(checks[j].id.replace('_chkNodeInfo_', '_ChildNode_')).style.backgroundColor = 'White';
            }
        }
    }
}
//##################
//###########################
//Edit by bxlong 2013-7-3
TreeView.GetChildChecked = function(elmt) {
   
   if(elmt.id!=(elmt.id.split('_')[0]+"_palKnowledge_chkNodeInfo_0"))
      {
      document.getElementById(elmt.id.split('_')[0]+"_palKnowledge_chkNodeInfo_0").checked=false;
      }
    if (elmt.checked) {
        document.getElementById(elmt.id.replace('_chkNodeInfo_', '_ChildNode_')).style.backgroundColor = '#FAF2D2';
    }
    else {
        document.getElementById(elmt.id.replace('_chkNodeInfo_', '_ChildNode_')).style.backgroundColor = 'White';
    }
    //----------
    var childcontainer = document.getElementById(elmt.id.replace('_chkNodeInfo_', '_ChildContainer_'));
    var checks = '';
    if (childcontainer) {
        checks = childcontainer.getElementsByTagName("input");
    }
    else {
        if(btnClicked!="")
        {
            $("#" + btnClicked).click();
        }
        return;
    }

    //alert(checks.length);
    for (var j = 0; j < checks.length; j++) {
        checks[j].checked = elmt.checked;
        if (checks[j].checked) {
            document.getElementById(checks[j].id.replace('_chkNodeInfo_', '_ChildNode_')).style.backgroundColor = '#FAF2D2';
        }
        else {
            document.getElementById(checks[j].id.replace('_chkNodeInfo_', '_ChildNode_')).style.backgroundColor = 'White';
        }
    }
    //添加点击事件调用客户端控件
    if(btnClicked!="")
    {
        $("#" + btnClicked).click();
    }
}
//##################

TreeView.ChangeStyle2 = function(elmt, type, ismulti) {
    TreeView.isshowcheckbox = ismulti;
    var hidselectnodeid = elmt.id.substring(0, elmt.id.lastIndexOf('_ChildNode_')).replace('palKnowledge', 'hidSelectedNode');
    var hidSelectedNode = document.getElementById(hidselectnodeid);

    if (type == 1) {//鼠标划过
        if (ismulti) {
            if (!document.getElementById(elmt.id.replace('_ChildNode_', '_chkNodeInfo_')).checked) {
                elmt.style.backgroundColor = '#E6E9EB';
            }
        }
        else {
            if (elmt.id != hidSelectedNode.value) {
                elmt.style.backgroundColor = '#E6E9EB';
            }
        }
    }
    else if (type == 2) {//鼠标移出
        if (ismulti) {
            if (!document.getElementById(elmt.id.replace('_ChildNode_', '_chkNodeInfo_')).checked) {
                elmt.style.backgroundColor = '';
            }
        }
        else {
            if (elmt.id != hidSelectedNode.value) {
                elmt.style.backgroundColor = '';
            }
        }
    }
    else if (type == 3) {//鼠标点击
        if (ismulti) {
            if (elmt.checked) {
                document.getElementById(elmt.id.replace('_chkNodeInfo_', '_ChildNode_')).style.backgroundColor = '#FAF2D2';
            }
            else {
                document.getElementById(elmt.id.replace('_chkNodeInfo_', '_ChildNode_')).style.backgroundColor = 'White';
            }

        }
        else {
            if (document.getElementById(hidSelectedNode.value)) {
                document.getElementById(hidSelectedNode.value).style.backgroundColor = '';
            }
            hidSelectedNode.value = elmt.id;
            TreeView.selectedid = elmt.id.split('_')[elmt.id.split('_').length - 1];
            elmt.style.backgroundColor = '#FAF2D2';
        }
        if (document.getElementById(elmt.id.replace('_ChildNode_', '_chkNodeInfo_'))) {
            if (document.getElementById(elmt.id.replace('_ChildNode_', '_chkNodeInfo_')).style.display != 'inline') {
                //取知识点信息
            }
        }

    }
}