<%@ Page Language="C#" MasterPageFile="PesanMakan.Master" AutoEventWireup="true" CodeBehind="Calendar.aspx.cs" Inherits="PesanMakan.Views.Calendar" %>

<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="server">
<!-- Calendar CSS -->
<link href="../Scripts/plugins/bower_components/calendar/dist/fullcalendar.css" rel="stylesheet" />
<style>
    /*hide time di event pada calendar*/
    .fc-time{
        display : none;
    }
    /**/
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
<div class="container-fluid">
    <div class="row">
        <p>Pellentesque habitant <a id="popover" class="btn" rel="popover" data-content="" title="Popover with image">Popover button</a> tristique senectus et netus et malesuada fames ac turpis egestas.</p>
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
    $(document).ready(function () {
        var image = '<img src="../Scripts/plugins/images/chair.jpg"">';
        $('#popover').popover({ placement: 'bottom', content: image, html: true });
    });
</script>
</asp:Content>
