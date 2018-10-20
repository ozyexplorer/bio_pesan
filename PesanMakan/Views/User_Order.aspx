<%@ Page Language="C#" MasterPageFile="PesanMakan.Master" AutoEventWireup="true" CodeBehind="User_Order.aspx.cs" Inherits="PesanMakan.Views.User_Order" %>


<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="server">
<!-- Calendar CSS -->
<link href="../Scripts/plugins/bower_components/calendar/dist/fullcalendar.css" rel="stylesheet" />
<style>
    /*hide time di event pada calendar*/
    .fc-time{
        display : none;
    }

    .fc-day-number{
        color:black;
    }

    .media{margin-top:15px}
    .media:first-child{margin-top:0}
    .media,.media-body{overflow:hidden;zoom:1}
    .media-body{width:10000px}
    .media-object{display:block}
    .media-object.img-thumbnail{max-width:none}
    .media-right,.media>.pull-right{padding-left:10px}
    .media-left,.media>.pull-left{padding-right:10px}
    .media-body,.media-left,.media-right{display:table-cell;vertical-align:top}
    .media-middle{vertical-align:middle}
    .media-bottom{vertical-align:bottom}
    .media-heading{margin-top:0;margin-bottom:5px}
    .media-list{padding-left:0;list-style:none}
    /**/
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
<div class="container-fluid">
    <br />
    <div class="row">
        <%--<div class="col-md-12">
            <div class="white-box">
            <h3 class="box-title">Drag and drop your event</h3>
            <div class="m-t-20">
                <div class="calendar-event" data-class="bg-primary">My Event One <a href="javascript:void(0);" class="remove-calendar-event"><i class="ti-close"></i></a></div>
                <div class="calendar-event"  data-class="bg-success">My Event Two <a href="javascript:void(0);" class="remove-calendar-event"><i class="ti-close"></i></a></div>
                <div class="calendar-event"  data-class="bg-warning">My Event Three <a href="javascript:void(0);" class="remove-calendar-event"><i class="ti-close"></i></a></div>
                <div class="calendar-event"  data-class="bg-custom">booked <a href="javascript:void(0);" class="remove-calendar-event"><i class="ti-close"></i></a></div>
                <input type="text" placeholder="Add Event and hit enter" class="form-control add-event m-t-20">
                <button type="button" class="btn btn-primary  add-event-btn">Add</button>
            </div>
            </div>
        </div>--%>
        <div class="col-md-12">
            <h3 class="box-title">Form Booking Menu</h3>
            <div class="white-box">
            <div id="calendar"></div>
            </div>
        </div>
        
    </div>
    <div class="row">
        <div class="col-lg-5 col-sm-4"> 
        </div>
        <div class="col-lg-2 col-sm-4 col-xs-12">
            <button id="btnSave" type="button" class="btn btn-block btn-primary btn-lg">Save</button>
        </div>
        <div class="col-lg-5 col-sm-4"></div>
    </div>
</div>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="Javascript" runat="server">
<!-- Calendar JavaScript -->
<script src="../Scripts/plugins/bower_components/calendar/jquery-ui.min.js"></script>
<script src="../Scripts/plugins/bower_components/moment/moment.js"></script>
<script src='../Scripts/plugins/bower_components/calendar/dist/fullcalendar.min.js'></script>
<script src="../Scripts/plugins/bower_components/calendar/dist/jquery.fullcalendar.js"></script>

<script>
    var dateM = InitDate();
    //var date = new Date();

    var menuDates = GetMenuDates(); //list menu dan tanggal
    var events = GetEvents();
    var day = parseInt(dateM.substring(8, 10));
    var month = parseInt(dateM.substring(5, 7) - 1);
    var year = parseInt(dateM.substring(0, 4));

    $('#calendar').fullCalendar({

        header: {
            left: 'prev,next today',
            center: 'title',
            right: 'month'
        },
        editable: false, // determines if the events can be dragged and resized
        droppable: false, // this allows things to be dropped onto the calendar
        eventLimit: true, // allow "more" link when too many events
        dayRender: function (date, cell, eventObj) {
            cell.css("background-color", "#ffe6e6"); //default all background color
            for (var i = 0; i < menuDates.length; i++) {
                var split = menuDates[i].Tanggal_Makan_S.split('/');
                y = split[0];
                m = parseInt(split[1] - 1);
                d = split[2];
                
                if (IsSameDate(date, new Date(y, m, d, 0))) {
                    cell.css("background-color", "white");
                    cell.css("background-image", "url('../Images/" + menuDates[i].Gambar + "')");
                    cell.css("background-size", "cover");
                    cell.html(menuDates[i].Menu_Makan);
                    cell.css("color", "white");
                    cell.css("font-weight", "bold");
                    /*var image = '<img src="../Images/' + menuDates[i].Gambar + '" alt="Tidak Ada Gambar" width="100"><p> ' + menuDates[i].Menu_Makan + '</p>';
                    cell.popover({
                        title: menuDates[i].Tanggal_Makan_S,
                        content: image,
                        trigger: 'hover',
                        placement: 'top',
                        container: 'body',
                        html: true
                    });*/
                }
            }

        },
        dayClick: function (date, jsEvent, view) {
            var menu, startHour, endHour;

            for (var i = 0; i < menuDates.length; i++) {
                var split = menuDates[i].Tanggal_Makan_S.split('/');
                y = split[0];
                m = parseInt(split[1] - 1);
                d = split[2];

                if (IsSameDate(date, new Date(y, m, d, 0))) {
                    menu = menuDates[i].Menu_Makan;
                    startHour = menuDates[i].Jam_Mulai;
                    endHour = menuDates[i].Jam_Akhir;
                }
            }

            //jika tanggal yg dipilih di tanggal warna merah (tidak tersedia)
            if ($(this).css('background-color') == "rgb(255, 230, 230)") {
                $.toast({ icon: 'warning', heading: 'Notifikasi!', text: 'tidak bisa update booking di tanggal yang dipilih', showHideTransition: 'slide', position: 'bottom-right', hideAfter: 3000 });
            }
                // jika kurang dari minggu depan
            else if (date < new Date(year, month, day + 7)) {
                $.toast({ icon: 'warning', heading: 'Notifikasi!', text: 'tidak bisa update booking di tanggal yang dipilih', showHideTransition: 'slide', position: 'bottom-right', hideAfter: 3000 });
            }
                //jika sudah booked
            else if ($('#calendar').fullCalendar('clientEvents', [date.format()]).length > 0) {
                $('#calendar').fullCalendar('removeEvents', [date.format()])
            } else {
                $('#calendar').fullCalendar('renderEvent', {
                    title: 'booked',
                    start: date,
                    allDay: true,
                    id: date.format(), // format : yyyy-MM-dd
                    menu: menu,
                    startHour: startHour,
                    endHour: endHour
                }, true);
            }
        },
        events: events

    });

    $('#btnSave').click(function () {
        clientEvents = $('#calendar').fullCalendar('clientEvents');
        console.log(clientEvents);

        list = [];

        for (var i = 0; i < clientEvents.length; i++) {
            obj = {};
            obj.id = clientEvents[i].id;
            obj.title = clientEvents[i].title;
            obj.start = clientEvents[i].start;
            obj.menu = clientEvents[i].menu;
            obj.startHour = clientEvents[i].startHour;
            obj.endHour = clientEvents[i].endHour;
            list.push(obj);
        }

        $.ajax({
            type: 'POST',
            url: '../Handler/GetSetService.asmx/InputProsesFromCalendar',
            data: JSON.stringify({ listObj: list }),
            contentType: "application/json; charset=utf-8",
            dataType: 'text',
            async: false,
            success: function (d) {
                d = d.replace('{"d":null}', "");
                dt = JSON.parse(d).data;

                swal({
                    title: dt.head,
                    text: dt.notes,
                    type: dt.type,
                    confirmButtonText: "OK",
                    closeOnConfirm: false
                },
                function (isConfirm) {
                    swal.close();
                });

            },
            failure: function (d) {
                $.toast({ icon: 'error', heading: 'Warning!', text: 'Ajax request failed', showHideTransition: 'slide', position: 'bottom-right', hideAfter: 3000 });
            }
        });
    });

    function InitDate() {
        var date;
        $.ajax({
            type: 'GET',
            url: '../Handler/GetSetService.asmx/GetDateTimeServer',
            //data: 'y=' + year,
            async: false,
            dataType: 'text',
            success: function (d) {
                d = d.replace('{"d":null}', "");
                dt = JSON.parse(d).data;
                date = dt;
            },
            failure: function (d) {
                $.toast({ icon: 'error', heading: 'Warning!', text: 'Ajax request failed', showHideTransition: 'slide', position: 'bottom-right', hideAfter: 3000 });
            }
        });
        return date;
    }

    function GetEvents() {
        var listEvent = [];
        $.ajax({
            type: 'GET',
            url: '../Handler/GetSetService.asmx/GetMenuBookingCalendar',
            //data: 'y=' + year,
            async: false,
            dataType: 'text',
            success: function (d) {
                d = d.replace('{"d":null}', "");
                dt = JSON.parse(d).data;
                listEvent = dt;
            },
            failure: function (d) {
                $.toast({ icon: 'error', heading: 'Warning!', text: 'Ajax request failed', showHideTransition: 'slide', position: 'bottom-right', hideAfter: 3000 });
            }
        });
        return listEvent;
    }

    function GetMenuDates() {
        var listMenu = [];

        $.ajax({
            type: 'GET',
            url: '../Handler/GetSetService.asmx/GetMenuBooking',
            //data: 'y=' + year,
            async: false,
            dataType: 'text',
            success: function (d) {
                d = d.replace('{"d":null}', "");
                dt = JSON.parse(d).data;
                if (dt.length > 0) {
                    $.each(dt, function (index, item) {
                        listMenu.push(item);
                    });
                }
            },
            failure: function (d) {
                $.toast({ icon: 'error', heading: 'Warning!', text: 'Ajax request failed', showHideTransition: 'slide', position: 'bottom-right', hideAfter: 3000 });
            }
        });
        console.log(listMenu);
        return listMenu;
    }

    function IsSameDate(date1, newdate2) {
        var retVal = false;

        if (date1.date() == newdate2.getDate()) {
            if (date1.month() == newdate2.getMonth()) {
                if (date1.year() == (newdate2.getYear() + 1900)) {
                    retVal = true;
                }
            }
        }
        return retVal;
    }
</script>
</asp:Content>






