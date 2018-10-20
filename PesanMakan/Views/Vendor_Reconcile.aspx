<%@ Page Language="C#" MasterPageFile="PesanMakan.Master" AutoEventWireup="true" CodeBehind="Vendor_Reconcile.aspx.cs" Inherits="PesanMakan.Views.Vendor_Reconcile" %>

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
                    <p style="font-size:30px;color:black;line-height:120%; vertical-align:middle; text-align:center;"  >Form Reconcile</p>
                    <hr class="colorgraph">
                </div>
            </div>

                
            <div class="form-group">
                <label class="col-sm-2 control-label" style="font-weight: bold;">Token</label>
                <div class="col-lg-4 col-md-4 col-sm-4">
                    <input id="txtToken" class="form-control m-bot15" />
                </div>
                <div class="col-lg-2 col-md-2 col-sm-4">
                    <button type="button" class="btn btn-primary" id="btnUpdateMakan">Update</button>
                </div>
    ;        </div>

            <div class="col-sm-12">
                <div class="table-responsive">
                    <table id="tblMenu" class="display nowrap" cellspacing="0" width="100%">
                        <thead >
                            <tr>
                                <td>Tanggal</td>
                                <td>Hari</td>
                                <td>Token</td>
                                <td>NIK</td>
                                <td>Nama</td>
                                <td>Time In</td> 
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
        $("#txtDate").datepicker({
            changeMonth: true,
            changeYear: true,
            showButtonPanel: true,
            format: "yyyy-mm",
            viewMode: "months",
            minViewMode: "months"
            
        });
        
        $("#txtToken").change(function () {
            var length = parseInt($("#txtToken").val().length);
            if (length == 6) {
                CheckToken();
            } else {
                $("#txtToken").val('');
            }
        })

        $("#btnUpdateMakan").click(function() {
            CheckToken();
        })
        
           
    });
    function CheckToken() {
        var parameters = {};
        parameters.token = $("#txtToken").val();
        
        $.ajax({
               
                data: JSON.stringify(parameters),
                dataType: "text",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "../Handler/GetSetService.asmx/CheckToken",
                dataType: "text",
                success: function (data) {

                    statinit = data.split('{');
                    statfix = statinit[0];
                    console.log(statfix);
                    if (statfix == "Token Sesuai") {
                        //document.getElementById('keterangan').innerHTML="Tidak Booking";
                            $.toast({ icon: 'success', heading: 'Notifikasi!', text: 'Reconsiliasi Berhasil', showHideTransition: 'slide', position: 'bottom-right', hideAfter: 3000 });
                       
                       // alert('Token Sesuai');

                    } else if (statfix == "Token Sama") {
                        $.toast({ icon: 'error', heading: 'Notifikasi!', text: 'Reconcile Gagal, Token Sudah Dimasukkan', showHideTransition: 'slide', position: 'bottom-right', hideAfter: 3000 });
                    }
                    else
                    {
                        //document.getElementById('keterangan').innerHTML="Tidak Booking";
                        $.toast({ icon: 'warning', heading: 'Notifikasi!', text: 'Reconcile Gagal, Token Tidak Sesuai', showHideTransition: 'slide', position: 'bottom-right', hideAfter: 3000 });
                       // alert('Token Tidak Sesuai ');

                    }

                  
                    initTextBox();
                }
            });
            initTable();
        }
    function initTable() {
        //console.log("tes");
        var period = "2018";
        //$("#txtDate").val();
        $("#tblMenu" + " tbody").empty();
        $("#tblMenu" + " tbody").append('<tr><td colspan="12" class="text-center"><i class="fa fa-spin fa-refresh"></i>&nbsp;Loading...</td></tr>');
        
        var table = $('#tblMenu').dataTable({
            scrollY: 300,
            paging: false,
            "bDestroy": true,
            "dom": '<"html5buttons"B>lTfgitp',
            "buttons": [
                { extend: 'csv', title: 'Reconcile' },
                { extend: 'excel', title: 'Reconcile' },
            ],
            "ajax": {
                "url": "../Handler/GetSetService.asmx/GetReconcile",
                "type": "POST"
            },
            "columns": [
                { "data": "Tanggal" },
                { "data": "Hari" },
                { "data": "Token" },
                { "data": "NIK" },
                { "data": "Nama"},
                { "data": "TimeIn" }
                
            ],
            "order": [[0, 'desc']]
        });

      
    }
    
    
    function initTextBox() {
     
        document.getElementById("txtToken").value = "";
      
    }

    function back_page() {
            
        var page = 'index.aspx';
        document.location.href = page;
    }
    var HTMLtbl =
      {
          getData: function (table) {
              var check = 0;
              var rows = [];
              table.find('tr').not(':first').each(function (rowIndex, r) {
                  var cols = [];

                  $(this).find('td').each(function (colIndex, c) {

                      if ($(this).children(':text,:hidden,textarea,select').length > 0) {
                          cols.push($(this).children('input,textarea,select').val());
                      }
                      else {
                          cols.push($(this).text().trim());
                      }
                  });
                  
                      rows.push(cols);
                      check = 0;
              });

              return rows;
          }
      }

</script>
</asp:Content>






