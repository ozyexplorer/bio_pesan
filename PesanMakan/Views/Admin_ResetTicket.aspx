<%@ Page Language="C#" MasterPageFile="PesanMakan.Master" AutoEventWireup="true" CodeBehind="Admin_ResetTicket.aspx.cs" Inherits="PesanMakan.Views.Admin_ResetTicket" %>

<%@ Import Namespace="PesanMakan.Presentation.Component" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
<div class="container-fluid">
    <br />
  <!-- start slider -->
  <header id="registration" style="background:#ffffff;">
     <div class="w-container " >
        <div class="w-row row form">

            <div class="form-group" style="margin-bottom:15px;">
                <div class="col-lg-12 col-md-12 col-sm-12">
                    <p style="font-size:30px;color:black;line-height:120%; vertical-align:middle; text-align:center;"  >Resetter Ticket</p>
                    <hr class="colorgraph">
                </div>
            </div>

            <div runat="server" class="col-sm-12">
                <div class="table-responsive">
                    <table id="tblCard" class="display nowrap" cellspacing="0" width="100%">
                        <thead >
                            <tr>
                                <td>NIK</td>
                                <td>Nama</td>
                                <td>Action</td>
                            </tr>
                        </thead>
                        <tbody>
                           
                        </tbody>
                        
                    </table>
                </div>
            </div>
              
        <div id="result"></div>
     </div>
    </div>
  </header>
</div>
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="Javascript" runat="server">
    <script>

        $(document).ready(function () {
           initTable();
        });

        

        function Reset(r) {
            $('#tblCard tbody').on('click', '.btn-info', function () {
                var data_row = $(this).closest('tr');
                var parameters = {};
                parameters.nik = data_row[0].children[0].textContent;

                $.ajax({
                    data: JSON.stringify(parameters),
                    dataType: "text",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "../Handler/GetSetService.asmx/ResetData",
                    success: function () {
                        $.toast({ icon: 'success', heading: 'Notifikasi!', text: 'Data Berhasil Direset', showHideTransition: 'slide', position: 'bottom-right', hideAfter: 3000 });

                    }
                });
                initTable();
            });

        }

        function initTable() {
            $("#tblCard" + " tbody").empty();
            $("#tblCard" + " tbody").append('<tr><td colspan="12" class="text-center"><i class="fa fa-spin fa-refresh"></i>&nbsp;Loading...</td></tr>');

            var table = $('#tblCard').dataTable({
                "bDestroy": true,
                "dom": '<"html5buttons"B>lTfgitp',
                "buttons": [
                    { extend: 'csv', title: 'Reset' },
                    { extend: 'excel', title: 'Reset' },
                ],
                "ajax": {
                    "url": "../Handler/GetSetService.asmx/GetResetData",
                    "type": "POST"
                },
                "columns": [
                    { "data": "NIK" },
                    { "data": "NAMA" },
                    { "data": "ACTION" }
                ],
                "order": [[1, 'desc']]
            });
        }

       

        

        function back_page() {

            var page = 'index.aspx';
            document.location.href = page;
        }
</script>
</asp:Content>

