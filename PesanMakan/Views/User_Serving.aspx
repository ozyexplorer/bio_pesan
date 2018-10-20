<%@ Page Language="C#" MasterPageFile="PesanMakan.Master" AutoEventWireup="true" CodeBehind="User_Serving.aspx.cs" Inherits="PesanMakan.Views.User_Serving" %>
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
                    <p style="font-size:30px;color:black;line-height:120%; vertical-align:middle; text-align:center;"  >View Booking Makan</p>
                    <hr class="colorgraph">
                </div>
            </div>

                
            <div class="form-group col-sm-offset-4">
                <div class="row" style="margin-bottom:10px;">
                    <div class="col-sm-1">
                        <label style="font-weight: bold;">NIK</label>
                    </div>
                    <div class="col-sm-5">
                        <input ID="txtNIK" type="text" class="form-control" />
                    </div>
                </div>

                <div class="row">
                    <div class="col-sm-1">
                        <label class="control-label" style="font-weight: bold;">Nama</label>
                    </div>
                    <div class="col-sm-5">
                        <input ID="txtName" type="text" class="form-control m-bot15" />
                    </div>
                </div>
            </div>

            <div class="col-sm-12">
                <div class="table-responsive">
                    <table id="tblMenuOrder" class="display nowrap" cellspacing="0" width="100%">
                        <thead >
                            <tr>
                                <th>Tanggal</th>
                                <th>Hari</th>
                                <th>Menu Makanan</th>
                                <th>Jam Mulai</th>
                                <th>Jam Akhir</th> 
                                <th>Status Makan</th> 
                                <th>Jam Makan</th> 
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
        initTable();
       
                   
    });

         
    function deleteRow(r) {
        $('#tblMenuOrder tbody').on('click', '.btn-danger', function () {
            var data_row = $(this).closest('tr');

            var parameters = {};
            parameters.tgl = data_row[0].children[0].textContent;
           
            $.ajax({

                data: JSON.stringify(parameters),
                dataType: "text",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "../Handler/GetSetService.asmx/DelOrder",
                success: function () {
                    initTextBox();

                    alert('Delete Data Berhasil');
                }
            });
            initTable();
        });
    }
    
   
    function initTextBox() {
        
        var nik = '<% =Session["NIK"] == null ? string.Empty : Session["NIK"].ToString()%>';
        var nama = '<% =Session["NAMA"] == null ? string.Empty : Session["NAMA"].ToString() %>';     
        document.getElementById("txtNIK").value = nik;
        document.getElementById("txtName").value = nama;   
    }

    

    function back_page() {
      
        var page = 'index.aspx';
        document.location.href = page;
    }

    function initTable() {
        $("#tblMenuOrder" + " tbody").empty();
        $("#tblMenuOrder" + " tbody").append('<tr><td colspan="12" class="text-center"><i class="fa fa-spin fa-refresh"></i>&nbsp;Loading...</td></tr>');

        var table = $('#tblMenuOrder').dataTable({
            scrollY: 300,
            paging: false,
            "bDestroy": true,
            "dom": '<"html5buttons"B>lTfgitp',
            "buttons": [
                { extend: 'csv', title: 'Serving' },
                { extend: 'excel', title: 'Serving' },
            ],
            "ajax": {
                "url": "../Handler/GetSetService.asmx/GetListOrder",
                "type": "POST"
            },
            "columns": [
                { "data": "Tanggal_Makan" },
                { "data": "Hari" },
                { "data": "Menu_Makan" },
                { "data": "Jam_Mulai" },
                { "data": "Jam_Akhir" },
                { "data": "Status_Makan" },
                { "data": "Jam_Makan" },
            ],
            "order": [[0, 'asc']]
        });
    }

</script>
</asp:Content>




