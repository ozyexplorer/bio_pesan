using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PesanMakan.Views
{
    public partial class User_Order : System.Web.UI.Page
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

        #region comment
        //<%@ Import Namespace="PesanMakan.Presentation.Component" %>

        //<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
        //<div class="container-fluid">
        //  <!-- start slider -->
        //  <header id="registration" style="background:#ffffff;">
        //     <div class="w-container">
        //        <div class="w-row row form">

        //            <div class="form-group" style="margin-bottom:15px;">
        //                <div class="col-lg-12 col-md-12 col-sm-12">
        //                    <p style="font-size:30px;color:black;line-height:120%; vertical-align:middle; text-align:center;"  >Form Order Menu</p>
        //                    <hr class="colorgraph">
        //                </div>
        //            </div>

                
        //            <div class="form-group">
        //                <label class="col-sm-2 control-label" style="font-weight: bold;">NIK</label>
        //                <div class="col-lg-4 col-md-4 col-sm-4">
        //                    <input ID="txtNIK" type="text" class="form-control m-bot15" />
        //                </div>
        //                <label class="col-sm-2 control-label" style="font-weight: bold;">Nama</label>
        //                <div class="col-lg-4 col-md-4 col-sm-4" style="margin-bottom:20px;">
        //                    <input ID="txtName" type="text" class="form-control m-bot15" />
        //                </div>
        //                <label class="col-sm-2 control-label" style="font-weight: bold;">Periode</label>
        //                <div class="col-lg-4 col-md-4 col-sm-4">
        //                    <input ID="txtperiod" type="text" class="form-control m-bot15" />
        //                </div>
                
                        
        //            </div>

        //            <div class="row">
        //                <div class="col-sm-12">
        //                    <div class="table-responsive">
        //                        <table id="tblMenuOrder" class="display nowrap" cellspacing="0" width="100%">
        //                            <thead >
        //                                <tr>
        //                                    <th>Pesan<br /><input type="checkbox" id="SelectAll"></th>
        //                                    <th>Tanggal</th>
        //                                    <th>Hari</th>
        //                                    <th>Menu Makan</th>
        //                                    <th>Jam Mulai</th>
        //                                    <th>Jam Akhir</th> 
        //                                </tr>
        //                            </thead>
        //                            <tbody>
                           
        //                            </tbody>
                        
        //                        </table>
        //                    </div>
        //                </div>
        //            </div>
        //              <div class="form-group" style="margin-bottom:40px; margin-left:400px;">
        //                <input type="button" id="btnSave" onclick="Save()" class="btn btn btn-primary" value="Save" />
                  
        //            </div>
        //        <div id="result"></div>
        //     </div>
        //    </div><!-- end slider -->
        //  </header>
        //</div>
        //</asp:Content>
        //<asp:Content ID="Content1" ContentPlaceHolderID="Javascript" runat="server">
        //    <script>
    
        //    $(document).ready(function () {
        //        initTextBox();
        //        initTable();
        //        $("#SelectAll").click(function () {
        //            $('input:checkbox').not(this).prop('checked', this.checked);
        //        });
        
                  
        //    });

   
        //    function Save() {
        //        var data = HTMLtbl.getData($('#tblMenuOrder'));  // passing that table's ID //          
        //        var parameters = {};
        //        parameters.nik = $('#txtNIK').val();
        //        parameters.nama = $('#txtName').val();
        //        parameters.array = data;
        //        console.log(parameters.nik, parameters.nama, parameters.array);
        //        $.ajax({
        //            //async: true,
        //            //cache: false,
        //            data: JSON.stringify(parameters),
        //            dataType: "json",
        //            type: "POST",
        //            contentType: "application/json; charset=utf-8",
        //            url: "../Handler/GetSetService.asmx/InputProses",
        //            success: function () {
        //                alert('Pemasukan Data Booking Makan Berhasil');
        //            }
        //        });

        //    }
        //    function initTextBox() {
        
        //        var nik = '<% =Session["NIK"] == null ? string.Empty : Session["NIK"].ToString()%>';
        //        var nama = '<% =Session["NAMA"] == null ? string.Empty : Session["NAMA"].ToString() %>';
        //        var Period = '<% =Session["PERIOD"] == null ? string.Empty : Session["PERIOD"].ToString() %>';
        //        document.getElementById("txtNIK").value = nik;
        //        document.getElementById("txtName").value = nama;
        //        document.getElementById("txtperiod").textContent = Period;
      
        //    }

        //    var HTMLtbl =
        //      {
        //          getData: function (table) {
        //              var check=0;
        //              var rows = [];
        //              table.find('tr').not(':first').each(function (rowIndex, r) {
        //                  var cols = [];

        //                  $(this).find('td').each(function (colIndex, c) {                     

        //                      if ($(this).children(':text,:hidden,textarea,select').length > 0){
        //                          cols.push($(this).children('input,textarea,select').val().trim());
        //                      }
           
        //                      else if ($(this).children(':checkbox').length > 0) {
        //                          if ($(this).children(':checkbox').is(':checked')) {
        //                              check = 1;
        //                          } else {
        //                              check = 0;
        //                          }
        //                      }              
        //                      else {
        //                          cols.push($(this).text().trim());
        //                      }
                      
                        
        //                  });
        //                  if (check == 1) {
        //                      rows.push(cols);
        //                      check = 0;
        //                  }
               
        //              });

        //              return rows;
        //          }
        //      }

        //     function back_page() {
            
        //            var page = 'index.aspx';
        //            document.location.href = page;
        //        }

        //    function initTable() {
        //        $("#tblMenuOrder" + " tbody").empty();
        //        $("#tblMenuOrder" + " tbody").append('<tr><td colspan="12" class="text-center"><i class="fa fa-spin fa-refresh"></i>&nbsp;Loading...</td></tr>');

        //        var table = $('#tblMenuOrder').dataTable({
        //            scrollY: 300,
        //            paging: false,
        //            "bDestroy": true,
        //            "dom": '<"html5buttons"B>lTfgitp',
        //            "buttons": [
        //                { extend: 'copy' },
        //                { extend: 'csv' },
        //                { extend: 'excel', title: 'Menu' },
        //                { extend: 'pdf', title: 'Menu', orientation: 'landscape' }
        //            ],
        //            "ajax": {
        //                "url": "../Handler/GetSetService.asmx/GetMenuBooking",
        //                "type": "POST"
        //            },
        //            "columns": [
        //                { "data": "Booking" },
        //                { "data": "Tanggal_Makan_S" },
        //                { "data": "Hari" },
        //                { "data": "Menu_Makan" },
        //                { "data": "Jam_Mulai" },
        //                { "data": "Jam_Akhir" },
        //            ],
        //            "order": [[1, 'asc']]
        //        });
        //    }

        //</script>
        //</asp:Content>
        #endregion
    }
}
