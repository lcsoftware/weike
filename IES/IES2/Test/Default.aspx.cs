﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IES.Resource.Model;
using IES.G2S.Resource.BLL;

namespace Test
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {




        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Session["userid"] = TextBox1.Text;
            Response.Redirect("myspace.aspx");
        }
    }
}