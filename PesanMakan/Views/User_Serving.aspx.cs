using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PesanMakan.Views
{
    public partial class User_Serving : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["ROLE"] == null)
            {
                //comment due to view ui
                //Response.Redirect("index.aspx");
            }
            else
            {

                var nik = HttpContext.Current.Session["NIK"].ToString();
                var nama = HttpContext.Current.Session["NAMA"].ToString();
                var role = HttpContext.Current.Session["ROLE"].ToString();
                if (role == "")
                {
                    Response.Redirect("index.aspx");
                }
                else
                {
                    if (role == "USER")
                    {
                        //admin_menu.Visible = false;
                        //vendor_menu.Visible = false;

                    }
                    else if (role == "VENDOR")
                    {
                        //admin_menu.Visible = false;
                    }
                    else
                    {
                        //show all menu
                    }
                }

            }
        }

    }
}
