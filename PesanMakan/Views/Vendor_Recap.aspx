<%@ Page Language="C#" MasterPageFile="PesanMakan.Master" AutoEventWireup="true" CodeBehind="Vendor_Recap.aspx.cs" Inherits="PesanMakan.Views.Vendor_Recap" %>
<%@ Import Namespace="PesanMakan.Presentation.Component" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
<div class="container-fluid">
    <br />
  <!-- start slider -->
  <header id="registration" style="background:#ffffff;">
     <div class="w-container">
        <div class="w-row row form">
            <div class="form-group" style="margin-bottom:15px;">
                <div class="col-lg-12 col-md-12 col-sm-12">
                    <p style="font-size:30px;color:black;line-height:120%; vertical-align:middle; text-align:center;"  >Form Recap Order</p>
                    <hr class="colorgraph">
                </div>
            </div>
                
            <div class="form-group">
                <label class="col-sm-2 control-label" style="font-weight: bold;">Periode</label>
                <div class="col-lg-4 col-md-4 col-sm-4">
                    <input id="txtDate" type="text" class="form-control m-bot15" />
                </div>
            </div>

             <div class="form-group" style="margin-bottom:40px; margin-left:400px;">
                <input type="button" id="btnAdd" onclick="Show()" class="btn btn btn-primary" value="Show" />
                  <%--<asp:Button runat="server" ID="btnAdd" class="btn btn btn-primary" Text="ADD" />--%>
                
            </div>
            <div class="col-sm-12">
                <div class="table-responsive">
                    <table id="tblMenu" class="display nowrap" cellspacing="0" width="100%">
                        <thead >
                            <tr>
                                <td>Tanggal</td>
                                <td>Hari</td>
                                <td>Total Booking</td>
                                <td>Total Konfirmasi</td>
                                <td>Persentase</td>
                                <td>Status</td>
                            </tr>
                        </thead>
                        <tbody>
                           
                        </tbody>
                        
                    </table>
                </div>
           </div>
        <div id="result"></div>
     </div>
    </div><!-- end slider -->
  </header>
</div>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Javascript" runat="server">

    <script>
    
    $(document).ready(function () {
        initTextBox();
        $("#txtDate").datepicker({
            changeMonth: true,
            changeYear: true,
            showButtonPanel: true,
            format: "yyyy-mm",
            viewMode: "months",
            minViewMode: "months"
            
        });
        
        
           
    });

    function Show() {
        var parameters = {};
        parameters.bulan = $("#txtDate").val();
        $.ajax({
            //async: true,
            //cache: false,
            data: JSON.stringify(parameters),
            dataType: "text",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "../Handler/GetSetService.asmx/setMonthRecap",
            success: function () {
                initTextBox();

            }
        });

        initTable();
    }
   
     
    function initTable() {
        var period = $("#txtDate").val();
        $("#tblMenu" + " tbody").empty();
        $("#tblMenu" + " tbody").append('<tr><td colspan="12" class="text-center"><i class="fa fa-spin fa-refresh"></i>&nbsp;Loading...</td></tr>');
        
        var table = $('#tblMenu').dataTable({
            scrollY: 300,
            paging: false,
            "bDestroy": true,
            "dom": '<"html5buttons"B>lTfgitp',
            "buttons": [
                { extend: 'csv', title: 'Recap' },
                { extend: 'excel', title: 'Recap' },
            ],
            "ajax": {
                "url": "../Handler/GetSetService.asmx/RecapOrder",
                "type": "POST"
            },
            "columns": [
                { "data": "Tanggal" },
                { "data": "Hari" },             
                { "data": "Total_Booking" },
                { "data": "Total_Konfirmasi" },
                { "data": "Persentase" },
                { "data": "Status" }
            ],
            "order": [[0, 'asc']]
        });
    }
    
    
    function initTextBox() {
       
        document.getElementById("txtDate").value = "";
        
    }

     function back_page() {
         
            var page = 'index.aspx';
            document.location.href = page;
     }

    

</script>
</asp:Content>







