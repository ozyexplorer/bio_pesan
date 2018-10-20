<%@ Page Language="C#" MasterPageFile="PesanMakan.Master" AutoEventWireup="true" CodeBehind="Admin_RegCard.aspx.cs" Inherits="PesanMakan.Views.Admin_RegCard" %>


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
                    <p style="font-size:30px;color:black;line-height:120%; vertical-align:middle; text-align:center;"  >Form Registrasi Kartu</p>
                    <hr class="colorgraph">
                </div>
            </div>
            <div class="form-group col-sm-offset-3">
                <div class="row" style="margin-bottom:5px;">
                    <div class="col-sm-2">
                        <label style="font-weight: bold;">NIK</label>
                    </div>
                    <div class="col-sm-6">
                        <select  data-placeholder="Nik dan Nama" class="form-control m-bot15" id="ddlNIK" data-live-search="true">
                            <option></option>
                        </select>
                    </div>
                </div>

                <div class="row" style="margin-bottom:5px;">
                    <div class="col-sm-2">
                        <label style="font-weight: bold;">Card ID</label>
                    </div>
                    <div class="col-sm-6">
                        <input type="text" id="txtCardID" class="form-control" >
                    </div>
                </div>

                <div class="row" style="margin-bottom:5px;">
                    <div class="col-sm-2">
                        <label style="font-weight: bold;">Role</label>
                    </div>
                    <div class="col-sm-6">
                      <select class="form-control" id="ddlRole">
                        <option value="USER" selected>USER</option>
                        <option value="ADMIN">ADMIN</option>
                        <option value="VENDOR">VENDOR</option>
                      </select>
                    </div>
                </div>    
                <div class="row">
                    <div class="col-sm-6" style="float:right;">
                        <input type="button" id="btnAdd" onclick="submitData()" class="col-lg-2 btn btn btn-primary" value="ADD" />
                            <%--<asp:Button runat="server" ID="btnAdd" class="btn btn btn-primary" Text="ADD" />--%>
                        <%--<input type="button" id="btnSave" onclick="Add()" class="btn btn btn-primary" value="Save" />--%>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <div class="table-responsive">
                        <table id="tblCard" class="display nowrap" cellspacing="0" width="100%">
                            <thead >
                                <tr>
                                    <th>NIK</th>
                                    <th>Nama</th>
                                    <th>Card ID</th>                                                        
                                    <th>Role</th>
                                    <th>Action</th>
                                </tr>
                            </thead>
                        
                        </table>
                    </div>
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
            GetNik();
            var ddl = $('#ddlNIK');
            ddl.empty();


            var myTimer = window.setInterval(function () {
                init();
            }, 3000);
            initTable();

        });

        function submitData() {
            var parameters = {};
            console.log($("#ddlNIK option:selected").text());
            console.log($("#txtCardID").val());
            console.log($("#ddlRole option:selected").text());


            var niknama = $("#ddlNIK option:selected").text();
            var splitniknama = niknama.split(' - ');

            parameters.nik = splitniknama[0];
            parameters.nama = splitniknama[1];
            parameters.cardid = $("#txtCardID").val();
            parameters.role = $("#ddlRole option:selected").text();

            console.log(JSON.stringify(parameters));
            $.ajax({
                //async: true,
                //cache: false,
                data: JSON.stringify(parameters),
                dataType: "text",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "../Handler/GetSetService.asmx/RegCard",
                success: function (data) {
                    initTextBox();
                    var stat = data;
                    var statsplit = stat.split('{');
                    var statfix = statsplit[0];

                    if (statfix == 0) {
                        $.toast({ icon: 'success', heading: 'Notifikasi!', text: 'Data Berhasil Dimasukan', showHideTransition: 'slide', position: 'bottom-right', hideAfter: 3000 });
                    } else if (statfix == 1) {
                        $.toast({ icon: 'error', heading: 'Notifikasi!', text: 'Gagal Didaftarkan Karna ID Sudah Terpakai', showHideTransition: 'slide', position: 'bottom-right', hideAfter: 3000 });
                    }
                    console.log(statfix);

                }
            });
            initTable();
        }

        function GetNik() {
            var ddl = $('#ddlNIK');
            ddl.chosen();
            ddl.append("<option></option>");

            $.ajax({
                dataType: "text",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "../Handler/GetSetService.asmx/GetNikNama",
                success: function (r) {

                    r = r.replace('{"d":null}', "");

                    list = JSON.parse(r);
                    $.each(list.data, function (index, item) {

                        ddl.append($("<option/>", {
                            value: item.NIK,
                            text: item.NIK + " - " + item.NAMA
                        }));
                        ddl.chosen().trigger("chosen:updated");

                    });

                }

            });
            initTable();

        }

        function init() {

            $.ajax({
                url: "../Handler/GetSetService.asmx/initCardUID",
                type: "POST",
                contentType: "application/text; charset=utf-8",
                dataType: "text",
                success: function (data) {
                    document.getElementById("txtCardID").value = "";
                    var id = data;
                    var idsplit = id.split('{');
                    var idfix = idsplit[0];
                    $("#txtCardID").val(idfix);
                },

            });

        }

        function deleteRow(r) {
            $('#tblCard tbody').on('click', '.btn-danger', function () {
                var data_row = $(this).closest('tr');

                var parameters = {};
                parameters.nik = data_row[0].children[0].textContent;
                parameters.nama = data_row[0].children[1].textContent;
                parameters.cardid = data_row[0].children[2].textContent;
                parameters.role = data_row[0].children[3].textContent;

                $.ajax({

                    data: JSON.stringify(parameters),
                    dataType: "text",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "../Handler/GetSetService.asmx/DelData",
                    success: function () {
                        initTextBox();
                        $.toast({ icon: 'success', heading: 'Notifikasi!', text: 'Delete Data Berhasil', showHideTransition: 'slide', position: 'bottom-right', hideAfter: 3000 });

                    }
                });
                initTable();
            });
        }

        function Edit(r) {

            $('#tblCard tbody').on('click', '.btn-info', function () {
                var data_row = $(this).closest('tr');

                var nik = data_row[0].children[0].textContent;
                //console.log(nik);

                $("#ddlNIK").val(nik);
                $("#txtCardID").val(data_row[0].children[2].textContent);
                $("#ddlRole").val(data_row[0].children[3].textContent);
                $("#ddlNIK").chosen().trigger("chosen:updated");

                //console.log($("#ddlNIK").val());            
            });
        }

        function initTable() {
            $("#tblCard" + " tbody").empty();
            $("#tblCard" + " tbody").append('<tr><td colspan="12" class="text-center"><i class="fa fa-spin fa-refresh"></i>&nbsp;Loading...</td></tr>');

            var table = $('#tblCard').dataTable({
                "bDestroy": true,
                "dom": '<"html5buttons"B>lTfgitp',
                "buttons": [
                    { extend: 'csv', title: 'RegCard' },
                    { extend: 'excel', title: 'RegCard' },
                ],
                "ajax": {
                    "url": "../Handler/GetSetService.asmx/GetUserData",
                    "type": "POST"
                },
                "columns": [
                    { "data": "NIK" },
                    { "data": "NAMA" },
                    { "data": "CardID" },
                    { "data": "ROLE" },
                    { "data": "Action" }
                ],
                "order": [[1, 'desc']]
            });

        }

        function initTextBox() {
            // Clear forms here
            document.getElementById("ddlNIK").value = "";
            document.getElementById("txtCardID").value = "";

        }


        function back_page() {

            var page = 'index.aspx';
            document.location.href = page;
        }
</script>
</asp:Content>






