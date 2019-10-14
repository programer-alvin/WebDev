using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DynamicWeb
{
    public partial class index : System.Web.UI.Page
    {

        

        protected void Page_Load(object sender, EventArgs e)
        {


            string mappath = Server.MapPath("~/");//@"D:\Project\The Restaurant\"
            ContentBuilder contentBuilder = new ContentBuilder();
            lbl_test.Text = contentBuilder.analyzeText(contentBuilder.readFile(mappath + "menu.data"));//intergration test

        }


    }
}