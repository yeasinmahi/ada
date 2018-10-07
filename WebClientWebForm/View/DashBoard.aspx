<%@ Page Title="" Language="C#" MasterPageFile="../Admin.Master" AutoEventWireup="true" CodeBehind="DashBoard.aspx.cs" Inherits="WebClientWebForm.View.DashBoard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
    <div class="col-md-12 row-eq-height">
        <div class="col-md-2 col-sm-2 bg-blue">
            <div class="panel-body">
                <span>latest News</span>
            </div>
        </div>
        <div class="col-md-8 col-sm-8 bg-gray-light">
            <div class="panel-body">
                <span>Akij Group Arrenge a grand Picnic On CoxsBazar at 30-August-2018</span>
            </div>
        </div>
        <div class="col-md-2 col-sm-2 bg-gray-light">
            <div class="panel-body pull-right">
                <span><i class="fa fa-arrow-circle-left"></i> <i class="fa fa-arrow-circle-right"></i></span>
            </div>
        </div>
    </div>
</div>
<!-- /.row -->
<div class="clearfix visible-sm-block"></div>
<br />
<div class="row">
    <div class="col-md-6 col-sm-12">
        <div class="info-box">
            <span class="info-box-icon bg-blue"><i class="fa fa-cutlery"></i></span>
            <div class="info-box-content box-content-center">
                <span class="info-box-number">Todays Meal</span>
                <span class="info-box-text"><small id="mealSpan">Meal</small></span>
                <span class="info-box-text"><small id="altMealSpan">AltMeal</small></span>
            </div>
            <!-- /.info-box-content -->
        </div>
        <!-- /.info-box -->
    </div>
    <!-- /.col -->
    <div class="col-md-6 col-sm-12">
        <div class="info-box">
            <span class="info-box-icon bg-blue"><i class="fa fa-address-card"></i></span>
            <div class="info-box-content box-content-center">
                <span class="info-box-number">Attendance Details 
                    <input class="btn btn-primary btn-sm pull-right" id="attendanceViewButton" type="button" value="Show" />
                </span>
                <span class="info-box-text"><small>Daily Attendance (Details View)</small></span>
                
            </div>
            <!-- /.info-box-content -->
        </div>
        <!-- /.info-box -->
    </div>
    <!-- /.col -->
</div>
<!-- /.row -->
<!-- fix for small devices only -->
<div class="clearfix visible-sm-block"></div>

<div class="row">
    <div class="col-md-6 connectedSortable">
        <div class="box box-primary">
            <div class="box-header with-border">
                <h3 class="box-title">Inbox</h3>
                <div class="box-tools pull-right">
                    <div class="has-feedback">
                        <input type="text" class="form-control input-sm" placeholder="Search Mail">
                        <span class="glyphicon glyphicon-search form-control-feedback"></span>
                    </div>
                </div>
                <!-- /.box-tools -->
            </div>
            <!-- /.box-header -->
            <div class="box-body no-padding">
                <div class="mailbox-controls">
                    <!-- Check all button -->
                    <button type="button" class="btn btn-default btn-sm checkbox-toggle">
                        <i class="fa fa-square-o"></i>
                    </button>
                    <div class="btn-group">
                        <button type="button" class="btn btn-default btn-sm"><i class="fa fa-trash-o"></i></button>
                        <button type="button" class="btn btn-default btn-sm"><i class="fa fa-reply"></i></button>
                        <button type="button" class="btn btn-default btn-sm"><i class="fa fa-share"></i></button>
                    </div>
                    <!-- /.btn-group -->
                    <button type="button" class="btn btn-default btn-sm"><i class="fa fa-refresh"></i></button>
                    <div class="pull-right">
                        1-50/200
                        <div class="btn-group">
                            <button type="button" class="btn btn-default btn-sm"><i class="fa fa-chevron-left"></i></button>
                            <button type="button" class="btn btn-default btn-sm"><i class="fa fa-chevron-right"></i></button>
                        </div>
                        <!-- /.btn-group -->
                    </div>
                    <!-- /.pull-right -->
                </div>
                <div class="table-responsive mailbox-messages">
                    <table class="table table-hover table-striped">
                        <tbody>
                            <tr>
                                <td><input type="checkbox"></td>
                                <td class="mailbox-star"><a href="#"><i class="fa fa-star text-yellow"></i></a></td>
                                <td class="mailbox-name"><a href="read-mail.html">A.M Rafat Rahman</a></td>
                                <td class="mailbox-subject">
                                    <b>Regarding Akij ERP Template</b> - Please check the links below...
                                </td>
                                <td class="mailbox-attachment"></td>
                                <td class="mailbox-date">5 mins ago</td>
                            </tr>
                            <tr>
                                <td><input type="checkbox"></td>
                                <td class="mailbox-star"><a href="#"><i class="fa fa-star-o text-yellow"></i></a></td>
                                <td class="mailbox-name"><a href="read-mail.html">Abani Sarker</a></td>
                                <td class="mailbox-subject">
                                    <b>Modula WMS and ERP Integration Format</b> - As discussed, we will integrate using shared...
                                </td>
                                <td class="mailbox-attachment"><i class="fa fa-paperclip"></i></td>
                                <td class="mailbox-date">28 mins ago</td>
                            </tr>
                            <tr>
                                <td><input type="checkbox"></td>
                                <td class="mailbox-star"><a href="#"><i class="fa fa-star-o text-yellow"></i></a></td>
                                <td class="mailbox-name"><a href="read-mail.html">Md. Ruhul Amin</a></td>
                                <td class="mailbox-subject">
                                    <b>Leave</b> - Dear Sir,Please be informed that I will ...
                                </td>
                                <td class="mailbox-attachment"><i class="fa fa-paperclip"></i></td>
                                <td class="mailbox-date">11 hours ago</td>
                            </tr>
                            <tr>
                                <td><input type="checkbox"></td>
                                <td class="mailbox-star"><a href="#"><i class="fa fa-star text-yellow"></i></a></td>
                                <td class="mailbox-name"><a href="read-mail.html">Kazi Md. Golam Kibria</a></td>
                                <td class="mailbox-subject">
                                    <b>Undeliverable: Fwd: Modula WMS and ERP Integration Format</b> - Problem...
                                </td>
                                <td class="mailbox-attachment"></td>
                                <td class="mailbox-date">15 hours ago</td>
                            </tr>
                        </tbody>
                    </table>
                    <!-- /.table -->
                </div>
                <!-- /.mail-box-messages -->
            </div>
            <!-- /.box-body -->
            <div class="box-footer no-padding">
                <div class="mailbox-controls">
                    <!-- Check all button -->
                    <button type="button" class="btn btn-default btn-sm checkbox-toggle">
                        <i class="fa fa-square-o"></i>
                    </button>
                    <div class="btn-group">
                        <button type="button" class="btn btn-default btn-sm"><i class="fa fa-trash-o"></i></button>
                        <button type="button" class="btn btn-default btn-sm"><i class="fa fa-reply"></i></button>
                        <button type="button" class="btn btn-default btn-sm"><i class="fa fa-share"></i></button>
                    </div>
                    <!-- /.btn-group -->
                    <button type="button" class="btn btn-default btn-sm"><i class="fa fa-refresh"></i></button>
                    <div class="pull-right">
                        1-50/200
                        <div class="btn-group">
                            <button type="button" class="btn btn-default btn-sm"><i class="fa fa-chevron-left"></i></button>
                            <button type="button" class="btn btn-default btn-sm"><i class="fa fa-chevron-right"></i></button>
                        </div>
                        <!-- /.btn-group -->
                    </div>
                    <!-- /.pull-right -->
                </div>
            </div>
        </div>
        <!-- /. box -->
    </div>
    <div class="col-md-6 connectedSortable">
        <div class="box box-primary">
            <div class="box-header">
                <i class="ion ion-clipboard"></i>
                <h3 class="box-title">Task List</h3>
                <div class="box-tools pull-right">
                    <ul class="pagination pagination-sm inline">
                        <li><a href="#">&laquo;</a></li>
                        <li><a href="#">1</a></li>
                        <li><a href="#">2</a></li>
                        <li><a href="#">3</a></li>
                        <li><a href="#">&raquo;</a></li>
                    </ul>
                </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
                <!-- See dist/js/pages/dashboard.js to activate the todoList plugin -->
                <ul class="todo-list">
                    <li>
                        <!-- drag handle -->
                        <span class="handle">
                            <i class="fa fa-ellipsis-v"></i>
                            <i class="fa fa-ellipsis-v"></i>
                        </span>
                        <!-- checkbox -->
                        <input type="checkbox" value="">
                        <!-- todo text -->
                        <span class="text">ERP-Sales-Customize Sales Report</span>
                        <!-- Emphasis label -->
                        <small class="label label-danger"><i class="fa fa-clock-o"></i> 2 hour</small>
                        <!-- General tools such as edit or delete-->
                        <div class="tools">
                            <i class="fa fa-edit"></i>
                            <i class="fa fa-trash-o"></i>
                        </div>
                    </li>
                    <li>
                        <span class="handle">
                            <i class="fa fa-ellipsis-v"></i>
                            <i class="fa fa-ellipsis-v"></i>
                        </span>
                        <input type="checkbox" value="">
                        <span class="text">Others- Change the Process flow of DevOps</span>
                        <small class="label label-info"><i class="fa fa-clock-o"></i> 4 hours</small>
                        <div class="tools">
                            <i class="fa fa-edit"></i>
                            <i class="fa fa-trash-o"></i>
                        </div>
                    </li>
                    <li>
                        <span class="handle">
                            <i class="fa fa-ellipsis-v"></i>
                            <i class="fa fa-ellipsis-v"></i>
                        </span>
                        <input type="checkbox" value="">
                        <span class="text">ERP-HR-Added OverTime Calculation Automation</span>
                        <small class="label label-warning"><i class="fa fa-clock-o"></i> 1 day</small>
                        <div class="tools">
                            <i class="fa fa-edit"></i>
                            <i class="fa fa-trash-o"></i>
                        </div>
                    </li>
                    <li>
                        <span class="handle">
                            <i class="fa fa-ellipsis-v"></i>
                            <i class="fa fa-ellipsis-v"></i>
                        </span>
                        <input type="checkbox" value="">
                        <span class="text">ERP-SCM- Add a field to purchase order</span>
                        <small class="label label-success"><i class="fa fa-clock-o"></i> 3 days</small>
                        <div class="tools">
                            <i class="fa fa-edit"></i>
                            <i class="fa fa-trash-o"></i>
                        </div>
                    </li>
                    <li>
                        <span class="handle">
                            <i class="fa fa-ellipsis-v"></i>
                            <i class="fa fa-ellipsis-v"></i>
                        </span>
                        <input type="checkbox" value="">
                        <span class="text">Android-Add new service</span>
                        <small class="label label-primary"><i class="fa fa-clock-o"></i> 1 week</small>
                        <div class="tools">
                            <i class="fa fa-edit"></i>
                            <i class="fa fa-trash-o"></i>
                        </div>
                    </li>
                    <li>
                        <span class="handle">
                            <i class="fa fa-ellipsis-v"></i>
                            <i class="fa fa-ellipsis-v"></i>
                        </span>
                        <input type="checkbox" value="">
                        <span class="text">ERP-Templete-Customize ERP template</span>
                        <small class="label label-default"><i class="fa fa-clock-o"></i> 1 month</small>
                        <div class="tools">
                            <i class="fa fa-edit"></i>
                            <i class="fa fa-trash-o"></i>
                        </div>
                    </li>
                </ul>
            </div>
            <!-- /.box-body -->
            <div class="box-footer clearfix no-border">
                <button type="button" class="btn btn-default pull-right"><i class="fa fa-plus"></i> Add item</button>
            </div>
        </div>
        <!-- /.box -->
    </div>
    <!-- /.col -->
    <!-- fix for small devices only -->
    <div class="clearfix visible-sm-block"></div>

</div>
<!-- /.row -->

<div class="row">
    <div class="col-md-4 col-sm-6 col-xs-12">
        <div class="info-box">
            <span class="info-box-icon bg-aqua"><i class="fa fa-shopping-cart"></i></span>
            <div class="info-box-content box-content-center">
                <a href="#">
                    <span class="info-box-number" style="display: inline-block">Store Requsition</span>
                    <span class="info-box-number pull-right" style="display: inline-block"><i class="fa fa-arrow-right"></i></span>
                </a>
            </div>
            <!-- /.info-box-content -->
        </div>
        <!-- /.info-box -->
    </div>
    <div class="col-md-4 col-sm-6 col-xs-12">
        <div class="info-box">
            <span class="info-box-icon bg-aqua"><i class="fa fa-user-plus"></i></span>
            <div class="info-box-content box-content-center">
                <a href="#">
                    <span class="info-box-number" style="display: inline-block">Visitor Booking</span>
                    <span class="info-box-number pull-right" style="display: inline-block"><i class="fa fa-arrow-right"></i></span>
                </a>
            </div>
            <!-- /.info-box-content -->
        </div>
        <!-- /.info-box -->
    </div>
    <div class="col-md-4 col-sm-6 col-xs-12">
        <div class="info-box">
            <span class="info-box-icon bg-aqua"><i class="fa fa-file-text"></i></span>
            <div class="info-box-content box-content-center">
                <a href="#">
                    <span class="info-box-number" style="display: inline-block">Dispach Request</span>
                    <span class="info-box-number pull-right" style="display: inline-block"><i class="fa fa-arrow-right"></i></span>
                </a>
            </div>
            <!-- /.info-box-content -->
        </div>
        <!-- /.info-box -->
    </div>
</div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="foot" runat="server">
    <link href="../Content/bootstrap3-wysihtml5.min.css" rel="stylesheet" />
    <script src="../Scripts/jquery-ui.min.js"></script>
    <script src="../Scripts/bootstrap3-wysihtml5.all.min.js"></script>
    <script src="../Scripts/dashboard.js"></script>

    <script src="../Controllers/Scripts/mealController.js"></script>
    <script src="../Controllers/Scripts/taskController.js"></script>
    <script src="../Controllers/Scripts/homeController.js"></script>
</asp:Content>
