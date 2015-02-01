using System;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
namespace Admin.Views.UserControl
{
    public partial class KnowledgeSpotTree8 : System.Web.UI.UserControl
    {
        #region 属性


        public Panel palContainer
        {
            get
            {
                return palKnowledge;
            }
        }
        private int _nodeindent = 10;
        /// <summary>
        /// 节点缩进的宽度


        /// </summary>
        public int NodeIndent
        {
            set
            {
                _nodeindent = value;
            }
        }
        public Unit Width
        {
            get { return palKnowledge.Width; }
            set { palKnowledge.Width = value; }
        }
        public Unit Height
        {
            get { return palKnowledge.Height; }
            set { palKnowledge.Height = value; }
        }
        private string _parentidfield = "ParentID";
        /// <summary>
        /// 父节点字段


        /// </summary>
        public string ParentIDField
        {
            get { return _parentidfield; }
            set { _parentidfield = value; }
        }
        private bool _isshowcheckbox = true;
        /// <summary>
        /// 是否显示复选框（默认带复选框）


        /// </summary>
        public bool IsShowCheckBox
        {
            get { return _isshowcheckbox; }
            set { _isshowcheckbox = value; }
        }
        private bool _isshowimg = false;
        /// <summary>
        /// 是否显示图片（默认不显示）


        /// </summary>
        public bool IsShowImg
        {
            get { return _isshowimg; }
            set { _isshowimg = value; }
        }

        private DataTable _treedatasource;
        /// <summary>
        /// 数据源


        /// </summary>
        public DataTable TreeDataSource
        {
            get
            {
                return _treedatasource;
            }
            set
            {
                _treedatasource = value;
            }
        }
        private string _displayfield = "OrganizationName";
        /// <summary>
        /// 显示字段名称
        /// </summary>
        public string DisplayField
        {
            get { return _displayfield; }
            set { _displayfield = value; }
        }
        private string _valuefield = "OrganizationID";
        /// <summary>
        /// 值字段名称


        /// </summary>
        public string ValueField
        {
            get { return _valuefield; }
            set { _valuefield = value; }
        }

        private string _forder = "Orde";
        /// <summary>
        /// 排序字段(默认是fOrder)
        /// </summary>
        public string fOrder
        {
            get
            {
                if (_forder == null)
                {
                    return _forder;
                }
                return _forder;
            }
            set { _forder = value; }
        }

        private string _pagetype;
        public string PageType
        {
            get { return _pagetype; }
            set { _pagetype = value; }
        }

        private string _RowClickJS;
        /// <summary>
        /// 行点击JS
        /// </summary>
        public string RowClickJS
        {
            get { return _RowClickJS; }
            set { _RowClickJS = value; }
        }

        #endregion
        private string _selectedknowlegeids = "";
        /// <summary>
        /// 已经选中的知识点ID（多个用","分隔）


        /// </summary>
        public string SelectedKnowlegeIDs
        {
            set
            {
                if (value != string.Empty)
                {
                    _selectedknowlegeids = value;
                    Page.ClientScript.RegisterStartupScript(GetType(), "_checkselnodes", string.Format("var ids = '{0}'.split(',');for (var i = 0; i < ids.length; i++)  if (document.getElementById('{1}_chkNodeInfo_' + ids[i])!=null) document.getElementById('{1}_chkNodeInfo_' + ids[i]).checked = true;", value, palContainer.ClientID), true);
                }
            }
        }

        #region 方法
        /// <summary>
        /// 实例化一个新的知识点树


        /// </summary>
        public KnowledgeSpotTree8()
        {

        }
        /// <summary>
        /// 生成一个新的知识点树


        /// </summary>
        /// <param name="displayfield">显示字段名称</param>
        /// <param name="valuefield">值字段名称</param>

        public KnowledgeSpotTree8(string displayfield, string valuefield)
        {
            _displayfield = displayfield;
            _valuefield = valuefield;
        }
        /// <summary>
        /// 实例化一个新的知识点树


        /// </summary>
        /// <param name="displayfield">显示字段名称</param>
        /// <param name="valuefield">值字段名称</param>
        ///   /// <param name="parentidfield">父节点字段名称</param>
        public KnowledgeSpotTree8(string displayfield, string valuefield, string parentidfield)
        {
            _displayfield = displayfield;
            _valuefield = valuefield;
        }

        /// <summary>
        /// 实例化一个新的知识点树


        /// </summary>
        /// <param name="displayfield">显示字段名称</param>
        /// <param name="valuefield">值字段名称</param>
        /// <param name="isshowcheckbox">是否显示复选框（默认显示）</param>
        public KnowledgeSpotTree8(string displayfield, string valuefield, bool isshowcheckbox)
        {
            _displayfield = displayfield;
            _valuefield = valuefield;
            _isshowcheckbox = isshowcheckbox;
        }
        /// <summary>
        /// 实例化一个新的知识点树


        /// </summary>
        /// <param name="displayfield">显示字段名称</param>
        /// <param name="valuefield">值字段名称</param>
        /// <param name="parentidfield">父节点字段名称</param>
        /// <param name="isshowcheckbox">是否显示复选框（默认显示）</param>
        /// <param name="istreenodepostback">点击节点时是否回送</param>
        public KnowledgeSpotTree8(string displayfield, string valuefield, string parentidfield, bool isshowcheckbox)
        {
            _displayfield = displayfield;
            _valuefield = valuefield;
            _parentidfield = parentidfield;
            _isshowcheckbox = isshowcheckbox;
        }
        public void TreeDataBind()
        {
            this.palKnowledge.Width = Width;
            this.palKnowledge.Height = Height;
            palKnowledge.Controls.Clear();

            if (ValidateTree())
            {
                if (_parentidfield == null)
                {
                    throw new NullReferenceException("");
                }
                else
                {
                    DataTable dtSub = _treedatasource.Clone();
                    DataRow[] drs = _treedatasource.Select(_parentidfield + "=-1", _valuefield + " ASC");
                    foreach (DataRow dr in drs)
                    {
                        dtSub.ImportRow(dr);
                    }
                    foreach (DataRow dr in dtSub.Rows)
                    {
                        dr[_parentidfield] = "-1";
                    }
                    palKnowledge.Controls.Add(new LiteralControl(GetChildTreeNodes(-1, ref _treedatasource, 0, true)));
                }
            }
        }
        /// <summary>
        /// 获取子节点


        /// </summary>
        /// <param name="pid"></param>
        /// <param name="tsource"></param>
        private string GetChildTreeNodes(int pid, ref DataTable tsource, int depth, bool isshow)
        {
            DataTable dtSub = _treedatasource.Clone();
            DataRow[] drs = _treedatasource.Select(_parentidfield + "='" + pid.ToString() + "'", string.Format("{0} Asc", fOrder));
            if (drs.Length > 0)
            {
                StringBuilder sb = new StringBuilder();
                if (pid == 0)
                {
                    sb.AppendFormat("<div id=\"{0}_ChildContainer_{1}\" style=\"display:{2};\">", palKnowledge.ClientID, pid, true ? "block" : "none");
                }
                else
                {
                    sb.AppendFormat("<div id=\"{0}_ChildContainer_{1}\" style=\"display:{2};\">", palKnowledge.ClientID, pid, isshow ? "block" : "none");
                }

                foreach (DataRow dr in drs)
                {
                    dtSub.ImportRow(dr);
                    //  tsource.Rows.Remove(dr);
                }
                foreach (DataRow dr in dtSub.Rows)
                {
                    #region 行点击JS
                    string strRowEvent = string.Format(" onclick=\"TreeView.ChangeStyle(this,3,false);{0}({1})\" ", _RowClickJS, dr[_valuefield].ToString());
                    #endregion


                    string strchildnodes = GetChildTreeNodes(Convert.ToInt32(dr[_valuefield].ToString()), ref tsource, depth + 1, false);

                    string strImg = "";

                    if (strchildnodes != string.Empty)
                    {
                        strImg = "<img src=\"../../../ico/16_ico_folder.gif\" style='padding-left:3px;padding-top:3px;'  />";
                    }
                    else
                    {
                        strImg = "<img src=\"../../../ico/16_ico_file.gif\" style='padding-left:3px;padding-top:3px;'  />";
                    }
                    int indent = depth * _nodeindent;
                    sb.AppendFormat("<div class=\"GridRow\" style=\"line-height:20px;vertical-align:middle;\" id=\"{0}_ChildNode_{1}\" onmouseover=\"TreeView.ChangeStyle(this,1,{3});\" onmouseout=\"TreeView.ChangeStyle(this,2,{3});\" {2}>", palKnowledge.ClientID, dr[_valuefield], _isshowcheckbox ? "" : strRowEvent, _isshowcheckbox.ToString().ToLower());

                    sb.AppendFormat("<table style=\"width:{0}px;margin-left:{1}px;\"><tr>", Width.Value - indent - 10, indent);
                    sb.Append("<td>");
                    if (pid == -1)
                    {
                        sb.AppendFormat("<div id=\"{0}_imgStatus_{1}\"  onclick=\"TreeView.GetChild(this);\" {2}></div>", palKnowledge.ClientID, dr[_valuefield], true ? "class=\"treeExpand\"" : "class=\"treeCollspand\"");
                    }
                    else
                    {
                        sb.AppendFormat("<div id=\"{0}_imgStatus_{1}\" {2}></div>", palKnowledge.ClientID, dr[_valuefield], strchildnodes == string.Empty ? "class=\"treeExpand\"" : "class=\"treeCollspand\" onclick=\"TreeView.GetChild(this);\"");
                    }
                    sb.Append("</td>");
                    sb.AppendFormat("{0}", !_isshowimg ? "" : "<td>" + strImg + "</td>");
                    sb.Append("<td>");
                    if (PageType == "AddClass")
                    {
                        sb.AppendFormat("<input type=\"checkbox\" class='allItem' onclick=\"TreeView.GetAllChildChecked(this);\" name=\"{0}_chkKnowlegeNodeInfo\" id=\"{0}_chkNodeInfo_{2}\" style=\"display:{3}\" value=\"{1}_{2}\" />", palKnowledge.ClientID, pid, dr[_valuefield], _isshowcheckbox ? "inline" : "none");
                        //    sb.AppendFormat("<input type=\"checkbox\" class='allItem' onclick=\"TreeView.ChangeStyle2(this,3,true);\" name=\"{0}_chkKnowlegeNodeInfo\" id=\"{0}_chkNodeInfo_{2}\" style=\"display:{3}\" value=\"{1}_{2}\" />", palKnowledge.ClientID, pid, dr[_valuefield], _isshowcheckbox ? "inline" : "none");
                    }
                    else
                    {
                        sb.AppendFormat("<input type=\"checkbox\" class='allItem' onclick=\"TreeView.ChangeStyle(this,3,true);\" name=\"{0}_chkKnowlegeNodeInfo\" id=\"{0}_chkNodeInfo_{2}\" style=\"display:{3}\" value=\"{1}_{2}\" />", palKnowledge.ClientID, pid, dr[_valuefield], _isshowcheckbox ? "inline" : "none");
                    }
                    sb.Append("</td>");
                    sb.Append("<td>");
                    sb.AppendFormat("<div class=\"TXTOverEllipsis\" style=\"width:{0}px;\"><label for=\"{1}_chkNodeInfo_{2}\" title=\"{3}\">{3}</label></div>", Width.Value - indent - 65, palKnowledge.ClientID, dr[_valuefield], dr[_displayfield]);
                    sb.Append("</td>");
                    sb.Append("</tr></table>");

                    sb.Append("</div>");
                    sb.Append(strchildnodes);
                }
                sb.Append("</div>");
                return sb.ToString();
            }
            else
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// 验证树
        /// </summary>
        private bool ValidateTree()
        {
            bool returnvalue = true;
            bool isexistdisplayfield = false;
            bool isexistvaluefield = false;
            bool isexistparentidfield = false;
            if (TreeDataSource.Rows.Count == 0)
            {
                returnvalue = false;
            }
            if (_displayfield == null)
            {
                throw new NullReferenceException("");
                //returnvalue = false;
            }
            if (_valuefield == null)
            {
                throw new NullReferenceException("");
                //returnvalue = false;
            }
            foreach (DataColumn dc in _treedatasource.Columns)
            {
                if (dc.ColumnName == _valuefield)
                {
                    isexistvaluefield = true;
                    break;
                }
            }
            if (!isexistvaluefield)
            {
                throw new NullReferenceException("" + ":" + _valuefield);
                //returnvalue = false;
            }
            foreach (DataColumn dc in _treedatasource.Columns)
            {
                if (dc.ColumnName == _displayfield)
                {
                    isexistdisplayfield = true;
                    break;
                }
            }
            if (!isexistdisplayfield)
            {
                throw new NullReferenceException("" + ":" + _displayfield);
                //returnvalue = false;
            }
            if (_parentidfield != null)
            {
                foreach (DataColumn dc in _treedatasource.Columns)
                {
                    if (dc.ColumnName == _parentidfield)
                    {
                        isexistparentidfield = true;
                        break;
                    }
                }
                if (!isexistparentidfield)
                {
                    //throw new NullReferenceException(this.GetResource("dangqianshujuyuanzhongbucunzaifuIDziduanmingcheng") + ":" + _parentidfield);
                    Response.Write("" + ":" + _parentidfield);
                    // returnvalue = false;
                }
            }
            return returnvalue;
        }
        #endregion

        #region 事件
        protected void Page_Load(object sender, EventArgs e)
        {
            palKnowledge.Attributes["style"] += "overflow-x:hidden; overflow-y:auto;";
            imgTrigger.Attributes["onload"] = string.Format("javascript:TreeView.AddTreeView(this,{0});", IsShowCheckBox);
            palKnowledge.Width = Width;
            palKnowledge.Height = Height;
            if (!IsPostBack)
            {
                hidTreeViewClientID.Value = palKnowledge.ClientID;
            }
        }
        #endregion
    }
}