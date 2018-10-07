<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Leave.aspx.cs" Inherits="WebClientWebForm.View.Leave" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
<div class="box box-default">
    <div class="box-header with-border">
        <h3 class="box-title"><i class="fa fa-tag"></i> Leave Entry</h3>
        <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                <i class="fa fa-minus"></i>
            </button>
        </div>
        <!-- /.box-tools -->
    </div>
    <!-- /.box-header -->
    <div class="box-body">
        <div class="row">
            <div class="col-md-4">
                <div class="form-group">
                    <label>Leave</label>
                    <select id="leaveDropdown" class="form-control selectpicker"></select>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <label>From Date</label>
                    <div class="input-group date">
                        <div class="input-group-addon">
                            <i class="fa fa-calendar"></i>
                        </div>
                        <input type="text" class="form-control pull-right" id="fromDate">
                    </div>
                    <!-- /.input group -->
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <label>To Date</label>
                    <div class="input-group date">
                        <div class="input-group-addon">
                            <i class="fa fa-calendar"></i>
                        </div>
                        <input type="text" class="form-control pull-right" id="toDate">
                    </div>
                    <!-- /.input group -->
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4">
                <div class="form-group">
                    <label>Leave Cause</label>
                    <div class="input-group">
                        <input type="text" class="form-control pull-right" id="leaveCause">
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <label>Leave Address</label>
                    <div class="input-group">
                        <input type="text" class="form-control pull-right" id="leaveAddress">
                    </div>
                </div>
            </div>
        </div>

    </div>
    <!-- /.box-body -->
    <div class="box-footer">
        <button id="submitButton" type="submit" class="btn btn-primary">Submit</button>
        <button id="cancelButton" type="submit" class="btn btn-primary">Cancel</button>
    </div>
</div>
    <div class="box box-default">
    <div class="box box-default">
        <div class="box-header with-border">
            <h3 class="box-title"><i class="fa fa-list-alt"></i> Leave Entry List</h3>
            <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse">
                    <i class="fa fa-minus"></i>
                </button>
            </div>
            <!-- /.box-tools -->
        </div>
        <!-- /.box-header -->
        <div class="box-body">
            <table id="example1" class="table table-bordered table-striped">
                <thead>
                    <tr>
                        <th class="hidden">id</th>
                        <th>Leave Type Name</th>
                        <th>LeaveTypeId</th>
                        <th>Date</th>
                        <th>Leave Cause</th>
                        <th>Leave Location</th>
                    </tr>
                </thead>
                <tfoot>
                    <tr>
                        <th class="hidden">id</th>
                        <th>Leave Type Name</th>
                        <th>LeaveTypeId</th>
                        <th>Date</th>
                        <th>Leave Cause</th>
                        <th>Leave Location</th>
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
    <link href="../Content/bootstrap-datepicker.min.css" rel="stylesheet" />
    <script src="../Scripts/bootstrap-datepicker.min.js"></script>
    <%: Styles.Render("~/Content/datatables") %>
    <%: Scripts.Render("~/bundles/datatables") %>
    <!--Controller-->
    <script src="../Controllers/Scripts/leaveController.js"></script>
</asp:Content>
