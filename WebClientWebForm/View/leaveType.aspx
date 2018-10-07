<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="leaveType.aspx.cs" Inherits="WebClientWebForm.View.leaveType" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <input type="hidden" id="leaveTypeId" />
<div class="box box-default">
    <div class="box-header with-border">
        <h3 class="box-title"><i class="fa fa-tag"></i> Leave Type Entry</h3>
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
            <div class="col-md-3 col-sm-4 col-xs-6">
                <div class="form-group">
                    <label>Leave Type Name</label>
                    <div class="input-group">
                        <input type="text" class="form-control" id="leaveTypeName" placeholder="Leave Type Name">
                    </div>
                </div>
            </div>
            <div class="col-md-3 col-sm-4 col-xs-6">
                <div class="form-group">
                    <label>Applicable For</label>
                    <div class="input-group">
                        <select class="form-control" id="applicableFor">
                            <option value="B">All</option>
                            <option value="M">Only Male</option>
                            <option value="F">Only Female</option>
                        </select>
                    </div>
                </div>
            </div>
            <div class="col-md-3 col-sm-4 col-xs-6">
                <div class="form-group">
                    <label>Company Policy</label>
                    <div class="input-group">
                        <input type="text" class="form-control" id="companyPolicy" placeholder="Day for this leave">
                    </div>
                </div>
            </div>
            <div class="col-md-3 col-sm-4 col-xs-6">
                <div class="form-group">
                    <label>Max Allowed At A Time</label>
                    <div class="input-group">
                        <input type="text" class="form-control" id="maxAllowedAtATime" placeholder="Maximum allowed per apply">
                    </div>
                </div>
            </div>
            <div class="col-md-3 col-sm-4 col-xs-6">
                <div class="form-group">
                    <label>Max Application At A Month</label>
                    <div class="input-group">
                        <input type="text" class="form-control" id="maxApplicationAtAMonth" placeholder="Maximum allowed per month">
                    </div>
                </div>
            </div>
            <div class="col-md-3 col-sm-4 col-xs-6">
                <div class="form-group">
                    <input type="checkbox" class="checkbox" id="isHalfDayAllowed">
                    <label>Is Half Day Allow</label>
                </div>
            </div>
            <div class="col-md-3 col-sm-4 col-xs-6">
                <div class="form-group">
                    <input type="checkbox" class="checkbox" id="isBalanceCheck">
                    <label>Is Balance Check</label>
                </div>
            </div>
            <div class="col-md-3 col-sm-4 col-xs-6">
                <div class="form-group">
                    <input type="checkbox" class="checkbox" id="isOnlyOneTime">
                    <label>is Only One Time</label>
                </div>
            </div>
            <div class="col-md-3 col-sm-4 col-xs-6">
                <div class="form-group">
                    <input type="checkbox" class="checkbox" id="isRestrict">
                    <label>is Restrict</label>
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
            <h3 class="box-title"><i class="fa fa-list-alt"></i> Leave Type Entry List</h3>
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
                        <th>Applicable For</th>
                        <th>company Policy</th>
                        <th>maximum Allowed At AT Time</th>
                        <th>max Application At A Month</th>
                        <th>is HalfDay Allowed</th>
                        <th>is Balance Checked</th>
                        <th>is Only One Time</th>
                        <th>is Restricted</th>
                    </tr>
                </thead>
                <tfoot>
                    <tr>
                        <th class="hidden">id</th>
                        <th>Leave Type Name</th>
                        <th>Applicable For</th>
                        <th>company Policy</th>
                        <th>maximum Allowed At AT Time</th>
                        <th>max Application At A Month</th>
                        <th>is HalfDay Allowed</th>
                        <th>is Balance Checked</th>
                        <th>is Only One Time</th>
                        <th>is Restricted</th>
                    </tr>
                </tfoot>
            </table>
        </div>
        <!-- /.box-body -->
    </div>
    <!-- /.box DataTable -->
</div>
<!-- /.box -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="foot" runat="server">
    <%:Styles.Render("~/Content/datatables") %>
    <%:Scripts.Render("~/bundles/datatables") %>
    <!--Controller-->
    <script src="../Controllers/Scripts/leaveTypeController.js"></script>
</asp:Content>
