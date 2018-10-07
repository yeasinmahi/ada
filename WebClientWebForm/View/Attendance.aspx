<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Attendance.aspx.cs" Inherits="WebClientWebForm.View.Attendance" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="box box-default">
        <div class="box box-default">
            <div class="box-header with-border">
                <h3 class="box-title"><i class="fa fa-list-alt"></i> Attendance View</h3>
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse">
                        <i class="fa fa-minus"></i>
                    </button>
                </div>
                <!-- /.box-tools -->
            </div>
            <!-- /.box-header -->
            <div class="box-body">
                <table id="example1" class="table table-hover">
                    <thead>
                    <tr>
                        <th class="hidden">id</th>
                        <th>Date</th>
                        <th>Attendance Time</th>
                        <th>Remarks</th>
                    </tr>
                    </thead>
                    <tbody id="attendanceBody"></tbody>
                    <tfoot>
                    <tr>
                        <th class="hidden">id</th>
                        <th>Date</th>
                        <th>Attendance Time</th>
                        <th>Remarks</th>
                    </tr>
                    </tfoot>
                </table>
            </div>
            <!-- /.box-body -->
        </div>
        <!-- /.box DataTable -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="foot" runat="server">
    <%: Styles.Render("~/Content/datatables") %>
    <%: Scripts.Render("~/bundles/datatables") %>
    <!--Controller-->
    <script src="../Controllers/Scripts/attendanceController.js"></script>
</asp:Content>
