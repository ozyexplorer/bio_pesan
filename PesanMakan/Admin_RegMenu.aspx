<%@ Page Language="C#" MasterPageFile="PesanMakan.Master" AutoEventWireup="true" CodeBehind="Admin_RegMenu.aspx.cs" Inherits="PesanMakan.Views.Admin_RegMenu" %>

<%@ Import Namespace="PesanMakan.Presentation.Component" %>

<asp:Content ID="Content4" ContentPlaceHolderID="Head" runat="server">
    <style>
        img{
            margin: 0 25px;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
<div class="container-fluid">
    <br />
  <!-- start slider -->
  <header id="registration" style="background:#ffffff;">
     <br />
      <div class="w-container">
        <div class="w-row row form">

            <div class="form-group" style="margin-bottom:15px;">
                <div class="col-lg-12 col-md-12 col-sm-12">
                    <p style="font-size:30px;color:black;line-height:120%; vertical-align:middle; text-align:center;"  >Form Menu</p>
                    <hr class="colorgraph">
                </div>
            </div>

                
            <div class="form-group">
                <label class="col-sm-2 control-label" style="font-weight: bold;">Periode</label>
                <div class="col-lg-4 col-md-4 col-sm-4">
                    <input id="txtDate" class="mydatepicker form-control m-bot15" type="text"/>
                </div>
                  
            </div>

             <div class="form-group" style="margin-bottom:40px; margin-left:400px;">
                <input type="button" id="btnAdd" class="btn btn btn-primary" value="Show" />
                  
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <div class="table-responsive">
                        <table id="tblMenu" class="display nowrap" cellspacing="0" style="overflow:scroll">
                            <thead >
                                <tr>
                                    <th>Tanggal</th>
                                    <th>Hari</th>
                                    <th>Menu Makan</th>
                                    <th>Upload</th>
                                    <th>Gambar</th>
                                    <th>Jam Mulai</th>
                                    <th>Jam Akhir</th> 
                                    <th>On/Off</th>                                                       
                                    <th>Note</th>
                                </tr>
                            </thead>
                            <tbody>
                           
                            </tbody>
                        
                        </table>
                    </div>
                </div>
            </div><!-- /.row -->
            <div class="row">
                <div class="col-md-3 pull-right">
                    <center>
                        <div class="form-group" style="margin-top:20px;">
                            <input type="button" id="btnSave" class="btn btn btn-primary" value="Save" />
                        </div>
                    </center>
                </div>
            </div><!-- /.row -->
        <div id="result"></div>
     </div>
    </div><!-- end slider -->
  </header>
</div>

    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Modal" runat="server">
  
    <!-- Modal -->
  <div class="modal fade" id="myModal" role="dialog">
    <div class="modal-dialog">
    
      <!-- Modal content-->
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title">Upload Gambar Makanan</h4>
        </div>
        <div class="modal-body">


          <input type="file" class="upload"  id="f_UploadImage"><br />
          <input type="hidden" id="tanggal" class="form-control" >
          <!--<div id="image-holder"> </div>-->
          <input type="button" id="btnUpload" class="btn btn btn-primary" value="Simpan" />
        
        
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
      
    </div>
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

            var tblM = $('#tblMenu').DataTable({
                scrollY: 300,
                paging: false,
                "bDestroy": true,
                "dom": '<"html5buttons"B>lTfgitp',
                "buttons": [
                    { extend: 'csv' },
                    { extend: 'excel', title: 'Menu' }
                ],
                "order": [[0, 'asc']]
            })

            $('#btnAdd').click(function () {
                tblM = Show(tblM);
            })


            $('#btnSave').click(function () {
                Save();
            })

                $('#tblMenu tbody').on('click', '.btn-info', function () {
                    var data_row = $(this).closest('tr');

                    var tgl;
                    //tgl.reset();



                    tgl = data_row[0].children[0].textContent;


                    console.log(tgl);
                    $("#tanggal").val(tgl);
                    //if (UploadImage(tgl)) {
                        //tgl = RemoveAll(tgl);
                        //console.log(tgl);
                    //}
                });
         
          
            
            function sendFile(file,Tanggal) {
                var formData = new FormData();
                formData.append('file', $('#f_UploadImage')[0].files[0]);
                formData.append('id', Tanggal);
                console.log(formData);
                console.log(Tanggal);
                
                $.ajax({
                    type: 'post',
                    url: '../Handler/HandlerImage.ashx',
                    data: formData,
                    success: function (data) {
                        data = data.replace('{"d":null}', "");
                        if (data != "") {
                            if (data == "0") {
                                alert("Whoops something went wrong!");
                            } else if (data == "1") {
                                $.toast({ icon: 'success', heading: 'Notifikasi!', text: 'Upload Gambar Berhasil', showHideTransition: 'slide', position: 'bottom-right', hideAfter: 3000 });
                                
                            }
                        }
                        formData.delete('file');
                        formData.delete('id');

                        $('#myModal').modal('hide');
                        joss();
                        
                    },
                    processData: false,
                    contentType: false,
                    error: function () {
                        alert("Whoops something went wrong!");
                    }
                });
                
            }

                var _URL = window.URL || window.webkitURL;
                $('#btnUpload').click(function () {
                    var file, img;

                    var Tanggal = $("#tanggal").val();
                    console.log(Tanggal);

                    if (file = $('#f_UploadImage')[0].files[0]) {
                        img = new Image();
                        img.onload = function () {
                            sendFile(file, Tanggal);
                        };
                        img.onerror = function () {
                            alert("Not a valid file:" + file.type);
                        };
                        img.src = _URL.createObjectURL(file);
                    }
                });
            



            $('#tblMenu tbody').on('change', '.ddlOnOff', function () {
                newVal = $(this).val();
                var tr = $(this).closest('tr');
                var row = tblM.row(tr);

                if (newVal == "Off") {
                    var onOff = "<select class='form-control m-bot10 ddlOnOff'><option value='On'>On</option><option value='Off' selected='selected'>Off</option></select>";
                    
                    obj = {}
                    obj.Tanggal = row.data().Tanggal;
                    obj.Hari = row.data().Hari;
                    obj.Menu_Makan = AddDisable(row.data().Menu_Makan);
                    obj.Gambar = AddDisableButton(row.data().Gambar);
                    console.log(obj.Gambar);
                    obj.Preview = row.data().Preview;
                    obj.Jam_Mulai = AddDisable(row.data().Jam_Mulai);
                    obj.Jam_Akhir = AddDisable(row.data().Jam_Akhir);
                    obj.OnOff_Day = onOff;
                    obj.Note = AddDisable(row.data().Note);
                } else {
                    var onOff = "<select class='form-control m-bot10 ddlOnOff'><option value='On' selected='selected'>On</option><option value='Off' >Off</option></select>";
                    obj = {}
                    obj.Tanggal = row.data().Tanggal;
                    obj.Hari = row.data().Hari;
                    obj.Menu_Makan = RemoveDisable(row.data().Menu_Makan);
                    obj.Gambar = RemoveDisable(row.data().Gambar);
                    obj.Preview = row.data().Preview;
                    obj.Jam_Mulai = RemoveDisable(row.data().Jam_Mulai);
                    obj.Jam_Akhir = RemoveDisable(row.data().Jam_Akhir);
                    obj.OnOff_Day = onOff;
                    obj.Note = RemoveDisable(row.data().Note);
                }

                row.data(obj).draw();
                $('.clockpicker').clockpicker();

            });
        });

        function Show(table) {
            if (!$("#txtDate").val()) { $("#txtDate").parents('.form-group').addClass('has-error'); $.toast({ icon: 'error', heading: 'Notification!', text: 'Isi periode terlebih dahulu!', showHideTransition: 'slide', position: 'bottom-right', hideAfter: 3000 }); }
            else {
                $("#txtDate").parents('.form-group').removeClass('has-error');

                var parameters = {};
                parameters.bulan = $("#txtDate").val();
                $.ajax({
                    //async: true,
                    //cache: false,
                    data: JSON.stringify(parameters),
                    dataType: "text",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "../Handler/GetSetService.asmx/setMonth",
                    success: function () {
                        initTextBox();

                    }
                });

                table = initTable(table);
            }
            return table;
        }

        function joss() {
            $("#tblMenu" + " tbody").empty();
            $("#tblMenu" + " tbody").append('<tr><td colspan="7" class="text-center"><i class="fa fa-spin fa-refresh"></i>&nbsp;Loading...</td></tr>');

            $.ajaxSetup({
                async: false
            });
            var table = $('#tblMenu').DataTable({
                scrollY: 300,
                paging: false,
                "bDestroy": true,
                "dom": '<"html5buttons"B>lTfgitp',
                "buttons": [
                { extend: 'csv', title: 'Menu' },
                { extend: 'excel', title: 'Menu' },
                ],
                "ajax": {
                    "url": "../Handler/GetSetService.asmx/GetMenu",
                    "type": "POST"
                },
                "columns": [
                    { "data": "Tanggal" },
                    { "data": "Hari" },
                    { "data": "Menu_Makan" },
                    { "data": "Gambar" },
                    { "data": "Preview" },
                    { "data": "Jam_Mulai" },
                    { "data": "Jam_Akhir" },
                    { "data": "OnOff_Day" },
                    { "data": "Note" }
                ],
                "order": [[0, 'asc']]
            });
            $.ajaxSetup({
                async: true
            });
            $('.clockpicker').clockpicker();
        }


        function initTable(table) {

            $("#tblMenu" + " tbody").empty();
            $("#tblMenu" + " tbody").append('<tr><td colspan="7" class="text-center"><i class="fa fa-spin fa-refresh"></i>&nbsp;Loading...</td></tr>');

            $.ajaxSetup({
                async: false
            });
            var table = $('#tblMenu').DataTable({
                scrollY: 300,
                paging: false,
                "bDestroy": true,
                "dom": '<"html5buttons"B>lTfgitp',
                "buttons": [
                { extend: 'csv', title: 'Menu' },
                { extend: 'excel', title: 'Menu' },
                ],
                "ajax": {
                    "url": "../Handler/GetSetService.asmx/GetMenu",
                    "type": "POST"
                },
                "columns": [
                    { "data": "Tanggal" },
                    { "data": "Hari" },
                    { "data": "Menu_Makan" },
                    { "data": "Gambar" },
                    { "data": "Preview" },
                    { "data": "Jam_Mulai" },
                    { "data": "Jam_Akhir" },
                    { "data": "OnOff_Day" },
                    { "data": "Note" }
                ],
                "order": [[0, 'asc']]
            });
            $.ajaxSetup({
                async: true
            });
            $('.clockpicker').clockpicker();
            return table;
        }


        function initTextBox() {
            // Clear forms here
            document.getElementById("txtDate").value = "";

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
                      console.log($(this).children());
                      if ($(this).children(':text,:hidden,textarea,select').length > 0) {
                          cols.push($(this).children('input,textarea,select').val());
                      }
                      else if ($(this).children('.clockpicker').length > 0) {
                          cols.push($(this).children('.clockpicker').children('input').val());
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

        function Save() {
            var data = HTMLtbl.getData($('#tblMenu'));  // passing that table's ID //          
            var parameters = {};
            parameters.array = data;
            console.log(parameters.array);
            $.ajax({
                //async: true,
                //cache: false,
                data: JSON.stringify(parameters),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "../Handler/GetSetService.asmx/InputMenu",
                success: function () {
                    $.toast({ icon: 'success', heading: 'Notifikasi!', text: 'Pemasukan Data Menu Makan Berhasil', showHideTransition: 'slide', position: 'bottom-right', hideAfter: 3000 });
                    
                }
            });
        }

        function AddDisableButton(text) {
            return text.replace("<button", "<button disabled");
        }

        function AddDisable(text) {
            return text.replace("<input", "<input disabled");
        }

        function AddTanggal(text) {
            return text.replace(text, "<input disabled");
        }

        function RemoveDisable(text) {
            return text.replace("disabled", "");
        }

        function RemoveAll(text) {
            return text.replace(text, "");
        }
</script>
</asp:Content>






