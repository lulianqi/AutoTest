using AutoTest.myControl;
using MyCommonControl;

namespace AutoTest
{
    partial class AutoRunner
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_DirectionalTest_1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoRunner));
            this.ribbonControl1 = new DevComponents.DotNetBar.RibbonControl();
            this.ribbonPanel1 = new DevComponents.DotNetBar.RibbonPanel();
            this.expandablePanel_testMode = new DevComponents.DotNetBar.ExpandablePanel();
            this.pictureBox_reLoadCase = new System.Windows.Forms.PictureBox();
            this.pictureBox_RunHereOnly = new System.Windows.Forms.PictureBox();
            this.pictureBox_runClose = new System.Windows.Forms.PictureBox();
            this.pictureBox_StopRun = new System.Windows.Forms.PictureBox();
            this.pictureBox_RunNext = new System.Windows.Forms.PictureBox();
            this.pictureBox_PauseRun = new System.Windows.Forms.PictureBox();
            this.pictureBox_RunHere = new System.Windows.Forms.PictureBox();
            this.label_moveFlagForRun = new System.Windows.Forms.Label();
            this.expandablePanel_dataAdd = new DevComponents.DotNetBar.ExpandablePanel();
            this.pictureBox_dataAddStop = new System.Windows.Forms.PictureBox();
            this.pictureBox_changeDataAddSize = new System.Windows.Forms.PictureBox();
            this.pictureBox_dataAddSave = new System.Windows.Forms.PictureBox();
            this.pictureBox_dataAddclean = new System.Windows.Forms.PictureBox();
            this.pictureBox_dataAddClose = new System.Windows.Forms.PictureBox();
            this.label_moveFlagForDataAdd = new System.Windows.Forms.Label();
            this.listView_DataAdd = new MyCommonControl.ListViewExDB();
            this.columnHeader0 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.expandablePanel_messageBox = new DevComponents.DotNetBar.ExpandablePanel();
            this.pictureBox_MessageInfoList = new System.Windows.Forms.PictureBox();
            this.richTextBox_showMessage = new System.Windows.Forms.RichTextBox();
            this.pictureBox_openInterfaceTest = new System.Windows.Forms.PictureBox();
            this.pictureBox_exportReport = new System.Windows.Forms.PictureBox();
            this.pictureBox_tryTest1 = new System.Windows.Forms.PictureBox();
            this.pictureBox_reLoadCase1 = new System.Windows.Forms.PictureBox();
            this.tb_tryTestData = new System.Windows.Forms.TextBox();
            this.checkBox_run = new System.Windows.Forms.CheckBox();
            this.checkBox_dataBack = new System.Windows.Forms.CheckBox();
            this.pictureBox_set1 = new System.Windows.Forms.PictureBox();
            this.pictureBox_set = new System.Windows.Forms.PictureBox();
            this.pictureBox_stopRun2 = new System.Windows.Forms.PictureBox();
            this.pictureBox_RunHere2 = new System.Windows.Forms.PictureBox();
            this.trb_addRecord = new MyCommonControl.DataRecordBox();
            this.bt_openFile = new System.Windows.Forms.Button();
            this.tb_caseFilePath = new System.Windows.Forms.TextBox();
            this.lb_msg2 = new System.Windows.Forms.Label();
            this.lb_msg1 = new System.Windows.Forms.Label();
            this.progressBar_case = new MyCommonControl.ProgressBarList();
            this.test = new System.Windows.Forms.Button();
            this.tvw_Case = new MyCommonControl.TreeViewExDB();
            this.contextMenuStrip_CaseTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RunHereToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PauseRunToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopRunToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RunNextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runHereOnlyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑选定项ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ModifyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.组件控制ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_dataAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_runQuick = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_editQuick = new System.Windows.Forms.ToolStripMenuItem();
            this.caseParameterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListForCase = new System.Windows.Forms.ImageList(this.components);
            this.pictureBox_caseParameter = new System.Windows.Forms.PictureBox();
            this.lb_msg5 = new System.Windows.Forms.Label();
            this.lb_msg4 = new System.Windows.Forms.Label();
            this.lb_msg3 = new System.Windows.Forms.Label();
            this.ribbonPanel3 = new DevComponents.DotNetBar.RibbonPanel();
            this.pictureBox_selRunerMax = new System.Windows.Forms.PictureBox();
            this.listView_SelectRunner = new MyCommonControl.ListViewExDB();
            this.columnHeader18 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader19 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader20 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader21 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader22 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader23 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader24 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lb_cr_info3 = new System.Windows.Forms.Label();
            this.lb_cr_info2 = new System.Windows.Forms.Label();
            this.lb_cr_info1 = new System.Windows.Forms.Label();
            this.llb_showRunner = new System.Windows.Forms.LinkLabel();
            this.pictureBox_cr_delSelect = new System.Windows.Forms.PictureBox();
            this.pictureBox_cr_StopSelect = new System.Windows.Forms.PictureBox();
            this.pictureBox_cr_runSelect = new System.Windows.Forms.PictureBox();
            this.cb_cr_SelectAll = new System.Windows.Forms.CheckBox();
            this.pictureBox_cr_addUser = new System.Windows.Forms.PictureBox();
            this.cb_cr_isCb = new System.Windows.Forms.CheckBox();
            this.listView_CaseRunner = new AutoTest.myControl.ListView_RunnerView();
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader16 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader17 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ribbonPanel2 = new DevComponents.DotNetBar.RibbonPanel();
            this.pictureBox_gwListMax = new System.Windows.Forms.PictureBox();
            this.listViewEx_GWlist = new System.Windows.Forms.ListView();
            this.columnHeader_vc_id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_vc_ip = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_vc_sn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_vc_Alias = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1_vc_Version = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_vc_Ability = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip_GwList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.richTextBox_BroadcastRecord = new System.Windows.Forms.RichTextBox();
            this.panel_configMain = new System.Windows.Forms.Panel();
            this.expandablePanel_vaneWifiConfig = new DevComponents.DotNetBar.ExpandablePanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox_startWifiConfig = new System.Windows.Forms.PictureBox();
            this.lb_WifiConfig_info3 = new System.Windows.Forms.Label();
            this.lb_WifiConfig_info2 = new System.Windows.Forms.Label();
            this.lb_WifiConfig_info1 = new System.Windows.Forms.Label();
            this.tb_wifiCfg_Key = new System.Windows.Forms.TextBox();
            this.tb_wifiCfg_SSID = new System.Windows.Forms.TextBox();
            this.cb_wifiCfg_Mode = new System.Windows.Forms.ComboBox();
            this.listView_WifiConfigDataBack = new System.Windows.Forms.ListView();
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.progressBarX_WifiConfig = new DevComponents.DotNetBar.Controls.ProgressBarX();
            this.ribbonPanel4 = new DevComponents.DotNetBar.RibbonPanel();
            this.pictureBox_rr_DelSelect = new System.Windows.Forms.PictureBox();
            this.pictureBox_rr_RefreshSelect = new System.Windows.Forms.PictureBox();
            this.pictureBox_rr_PuaseSelect = new System.Windows.Forms.PictureBox();
            this.pictureBox_rr_StopSelect = new System.Windows.Forms.PictureBox();
            this.pictureBox_rr_RunSelect = new System.Windows.Forms.PictureBox();
            this.pictureBox_ConnectHost = new System.Windows.Forms.PictureBox();
            this.advTree_remoteTree = new DevComponents.AdvTree.AdvTree();
            this.columnHeader_AdvTree_name = new DevComponents.AdvTree.ColumnHeader();
            this.columnHeader_AdvTree_nowCell = new DevComponents.AdvTree.ColumnHeader();
            this.columnHeader_AdvTree_result = new DevComponents.AdvTree.ColumnHeader();
            this.columnHeader_AdvTree_time = new DevComponents.AdvTree.ColumnHeader();
            this.columnHeader_AdvTree_state = new DevComponents.AdvTree.ColumnHeader();
            this.imageListForRemoteRunner = new System.Windows.Forms.ImageList(this.components);
            this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
            this.panel_RemoteRunner = new System.Windows.Forms.Panel();
            this.ribbonTabItem1 = new DevComponents.DotNetBar.RibbonTabItem();
            this.ribbonTabItem2 = new DevComponents.DotNetBar.RibbonTabItem();
            this.ribbonTabItem3 = new DevComponents.DotNetBar.RibbonTabItem();
            this.ribbonTabItem4 = new DevComponents.DotNetBar.RibbonTabItem();
            this.office2007StartButton1 = new DevComponents.DotNetBar.Office2007StartButton();
            this.itemContainer1 = new DevComponents.DotNetBar.ItemContainer();
            this.itemContainer2 = new DevComponents.DotNetBar.ItemContainer();
            this.galleryContainer1 = new DevComponents.DotNetBar.GalleryContainer();
            this.labelItem8 = new DevComponents.DotNetBar.LabelItem();
            this.btitem_openError = new DevComponents.DotNetBar.ButtonItem();
            this.btitem_openCase = new DevComponents.DotNetBar.ButtonItem();
            this.itemContainer_case = new DevComponents.DotNetBar.ItemContainer();
            this.btitem_openTip = new DevComponents.DotNetBar.ButtonItem();
            this.itemContainer_Tip = new DevComponents.DotNetBar.ItemContainer();
            this.btitem_changeTip = new DevComponents.DotNetBar.ButtonItem();
            this.itemContainer_changeTip = new DevComponents.DotNetBar.ItemContainer();
            this.btitem_openExport = new DevComponents.DotNetBar.ButtonItem();
            this.checkBoxItem_run = new DevComponents.DotNetBar.CheckBoxItem();
            this.checkBoxItem_edit = new DevComponents.DotNetBar.CheckBoxItem();
            this.checkBoxItem_dataAdd = new DevComponents.DotNetBar.CheckBoxItem();
            this.btitem_help = new DevComponents.DotNetBar.ButtonItem();
            this.btitem_about = new DevComponents.DotNetBar.ButtonItem();
            this.itemContainer4 = new DevComponents.DotNetBar.ItemContainer();
            this.buttonItem12 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem13 = new DevComponents.DotNetBar.ButtonItem();
            this.qatCustomizeItem1 = new DevComponents.DotNetBar.QatCustomizeItem();
            this.timer_show = new System.Windows.Forms.Timer(this.components);
            this.toolTip_clickInfo = new System.Windows.Forms.ToolTip(this.components);
            this.openFileDialog_caseFile = new System.Windows.Forms.OpenFileDialog();
            this.checkBoxItem1 = new DevComponents.DotNetBar.CheckBoxItem();
            this.checkBoxItem2 = new DevComponents.DotNetBar.CheckBoxItem();
            this.imageListForButton = new System.Windows.Forms.ImageList(this.components);
            this.stopRunForceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ToolStripMenuItem_DirectionalTest_1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ribbonControl1.SuspendLayout();
            this.ribbonPanel1.SuspendLayout();
            this.expandablePanel_testMode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_reLoadCase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_RunHereOnly)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_runClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_StopRun)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_RunNext)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_PauseRun)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_RunHere)).BeginInit();
            this.expandablePanel_dataAdd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_dataAddStop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_changeDataAddSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_dataAddSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_dataAddclean)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_dataAddClose)).BeginInit();
            this.expandablePanel_messageBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_MessageInfoList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_openInterfaceTest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_exportReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_tryTest1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_reLoadCase1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_set1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_set)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_stopRun2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_RunHere2)).BeginInit();
            this.contextMenuStrip_CaseTree.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_caseParameter)).BeginInit();
            this.ribbonPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_selRunerMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_cr_delSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_cr_StopSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_cr_runSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_cr_addUser)).BeginInit();
            this.ribbonPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_gwListMax)).BeginInit();
            this.contextMenuStrip_GwList.SuspendLayout();
            this.panel_configMain.SuspendLayout();
            this.expandablePanel_vaneWifiConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_startWifiConfig)).BeginInit();
            this.ribbonPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_rr_DelSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_rr_RefreshSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_rr_PuaseSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_rr_StopSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_rr_RunSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ConnectHost)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.advTree_remoteTree)).BeginInit();
            this.SuspendLayout();
            // 
            // ToolStripMenuItem_DirectionalTest_1
            // 
            ToolStripMenuItem_DirectionalTest_1.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripMenuItem_DirectionalTest_1.Image")));
            ToolStripMenuItem_DirectionalTest_1.Name = "ToolStripMenuItem_DirectionalTest_1";
            ToolStripMenuItem_DirectionalTest_1.Size = new System.Drawing.Size(136, 22);
            ToolStripMenuItem_DirectionalTest_1.Text = "定向测试_1";
            ToolStripMenuItem_DirectionalTest_1.Click += new System.EventHandler(this.ToolStripMenuItem_DirectionalTest_1_Click);
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ribbonControl1.CaptionVisible = true;
            this.ribbonControl1.Controls.Add(this.ribbonPanel1);
            this.ribbonControl1.Controls.Add(this.ribbonPanel3);
            this.ribbonControl1.Controls.Add(this.ribbonPanel2);
            this.ribbonControl1.Controls.Add(this.ribbonPanel4);
            this.ribbonControl1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.ribbonTabItem1,
            this.ribbonTabItem2,
            this.ribbonTabItem3,
            this.ribbonTabItem4});
            this.ribbonControl1.KeyTipsFont = new System.Drawing.Font("Tahoma", 7F);
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.ribbonControl1.QuickToolbarItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.office2007StartButton1});
            this.ribbonControl1.Size = new System.Drawing.Size(1000, 600);
            this.ribbonControl1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.ribbonControl1.TabGroupHeight = 14;
            this.ribbonControl1.TabIndex = 0;
            this.ribbonControl1.Text = "TestMan";
            // 
            // ribbonPanel1
            // 
            this.ribbonPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.ribbonPanel1.Controls.Add(this.expandablePanel_testMode);
            this.ribbonPanel1.Controls.Add(this.expandablePanel_dataAdd);
            this.ribbonPanel1.Controls.Add(this.expandablePanel_messageBox);
            this.ribbonPanel1.Controls.Add(this.pictureBox_openInterfaceTest);
            this.ribbonPanel1.Controls.Add(this.pictureBox_exportReport);
            this.ribbonPanel1.Controls.Add(this.pictureBox_tryTest1);
            this.ribbonPanel1.Controls.Add(this.pictureBox_reLoadCase1);
            this.ribbonPanel1.Controls.Add(this.tb_tryTestData);
            this.ribbonPanel1.Controls.Add(this.checkBox_run);
            this.ribbonPanel1.Controls.Add(this.checkBox_dataBack);
            this.ribbonPanel1.Controls.Add(this.pictureBox_set1);
            this.ribbonPanel1.Controls.Add(this.pictureBox_set);
            this.ribbonPanel1.Controls.Add(this.pictureBox_stopRun2);
            this.ribbonPanel1.Controls.Add(this.pictureBox_RunHere2);
            this.ribbonPanel1.Controls.Add(this.trb_addRecord);
            this.ribbonPanel1.Controls.Add(this.bt_openFile);
            this.ribbonPanel1.Controls.Add(this.tb_caseFilePath);
            this.ribbonPanel1.Controls.Add(this.lb_msg2);
            this.ribbonPanel1.Controls.Add(this.lb_msg1);
            this.ribbonPanel1.Controls.Add(this.progressBar_case);
            this.ribbonPanel1.Controls.Add(this.test);
            this.ribbonPanel1.Controls.Add(this.tvw_Case);
            this.ribbonPanel1.Controls.Add(this.pictureBox_caseParameter);
            this.ribbonPanel1.Controls.Add(this.lb_msg5);
            this.ribbonPanel1.Controls.Add(this.lb_msg4);
            this.ribbonPanel1.Controls.Add(this.lb_msg3);
            this.ribbonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ribbonPanel1.Location = new System.Drawing.Point(0, 55);
            this.ribbonPanel1.Name = "ribbonPanel1";
            this.ribbonPanel1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.ribbonPanel1.Size = new System.Drawing.Size(1000, 543);
            this.ribbonPanel1.TabIndex = 1;
            // 
            // expandablePanel_testMode
            // 
            this.expandablePanel_testMode.CanvasColor = System.Drawing.SystemColors.Control;
            this.expandablePanel_testMode.CollapseDirection = DevComponents.DotNetBar.eCollapseDirection.LeftToRight;
            this.expandablePanel_testMode.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.expandablePanel_testMode.Controls.Add(this.pictureBox_reLoadCase);
            this.expandablePanel_testMode.Controls.Add(this.pictureBox_RunHereOnly);
            this.expandablePanel_testMode.Controls.Add(this.pictureBox_runClose);
            this.expandablePanel_testMode.Controls.Add(this.pictureBox_StopRun);
            this.expandablePanel_testMode.Controls.Add(this.pictureBox_RunNext);
            this.expandablePanel_testMode.Controls.Add(this.pictureBox_PauseRun);
            this.expandablePanel_testMode.Controls.Add(this.pictureBox_RunHere);
            this.expandablePanel_testMode.Controls.Add(this.label_moveFlagForRun);
            this.expandablePanel_testMode.Location = new System.Drawing.Point(767, 353);
            this.expandablePanel_testMode.Name = "expandablePanel_testMode";
            this.expandablePanel_testMode.Size = new System.Drawing.Size(227, 64);
            this.expandablePanel_testMode.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel_testMode.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel_testMode.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanel_testMode.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.expandablePanel_testMode.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.expandablePanel_testMode.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandablePanel_testMode.Style.GradientAngle = 90;
            this.expandablePanel_testMode.TabIndex = 19;
            this.expandablePanel_testMode.TitleHeight = 25;
            this.expandablePanel_testMode.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel_testMode.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel_testMode.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanel_testMode.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.expandablePanel_testMode.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandablePanel_testMode.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.expandablePanel_testMode.TitleStyle.GradientAngle = 90;
            this.expandablePanel_testMode.TitleText = "Run";
            // 
            // pictureBox_reLoadCase
            // 
            this.pictureBox_reLoadCase.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_reLoadCase.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_reLoadCase.Image")));
            this.pictureBox_reLoadCase.Location = new System.Drawing.Point(188, 27);
            this.pictureBox_reLoadCase.Name = "pictureBox_reLoadCase";
            this.pictureBox_reLoadCase.Size = new System.Drawing.Size(35, 35);
            this.pictureBox_reLoadCase.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_reLoadCase.TabIndex = 9;
            this.pictureBox_reLoadCase.TabStop = false;
            this.toolTip_clickInfo.SetToolTip(this.pictureBox_reLoadCase, "刷新当前用例集");
            this.pictureBox_reLoadCase.Click += new System.EventHandler(this.pictureBox_MianRun_Click);
            this.pictureBox_reLoadCase.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            this.pictureBox_reLoadCase.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            // 
            // pictureBox_RunHereOnly
            // 
            this.pictureBox_RunHereOnly.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_RunHereOnly.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_RunHereOnly.Image")));
            this.pictureBox_RunHereOnly.Location = new System.Drawing.Point(151, 27);
            this.pictureBox_RunHereOnly.Name = "pictureBox_RunHereOnly";
            this.pictureBox_RunHereOnly.Size = new System.Drawing.Size(35, 35);
            this.pictureBox_RunHereOnly.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_RunHereOnly.TabIndex = 8;
            this.pictureBox_RunHereOnly.TabStop = false;
            this.toolTip_clickInfo.SetToolTip(this.pictureBox_RunHereOnly, "仅执行选定项");
            this.pictureBox_RunHereOnly.Click += new System.EventHandler(this.pictureBox_MianRun_Click);
            this.pictureBox_RunHereOnly.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            this.pictureBox_RunHereOnly.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            // 
            // pictureBox_runClose
            // 
            this.pictureBox_runClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_runClose.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_runClose.Image")));
            this.pictureBox_runClose.InitialImage = null;
            this.pictureBox_runClose.Location = new System.Drawing.Point(0, 1);
            this.pictureBox_runClose.Name = "pictureBox_runClose";
            this.pictureBox_runClose.Size = new System.Drawing.Size(23, 23);
            this.pictureBox_runClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_runClose.TabIndex = 6;
            this.pictureBox_runClose.TabStop = false;
            this.toolTip_clickInfo.SetToolTip(this.pictureBox_runClose, "移除该组件");
            this.pictureBox_runClose.Click += new System.EventHandler(this.pictureBox_runClose_Click);
            this.pictureBox_runClose.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            this.pictureBox_runClose.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            // 
            // pictureBox_StopRun
            // 
            this.pictureBox_StopRun.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_StopRun.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_StopRun.Image")));
            this.pictureBox_StopRun.Location = new System.Drawing.Point(114, 27);
            this.pictureBox_StopRun.Name = "pictureBox_StopRun";
            this.pictureBox_StopRun.Size = new System.Drawing.Size(35, 35);
            this.pictureBox_StopRun.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_StopRun.TabIndex = 7;
            this.pictureBox_StopRun.TabStop = false;
            this.toolTip_clickInfo.SetToolTip(this.pictureBox_StopRun, "终止任务");
            this.pictureBox_StopRun.Click += new System.EventHandler(this.pictureBox_MianRun_Click);
            this.pictureBox_StopRun.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            this.pictureBox_StopRun.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            // 
            // pictureBox_RunNext
            // 
            this.pictureBox_RunNext.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_RunNext.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_RunNext.Image")));
            this.pictureBox_RunNext.Location = new System.Drawing.Point(77, 27);
            this.pictureBox_RunNext.Name = "pictureBox_RunNext";
            this.pictureBox_RunNext.Size = new System.Drawing.Size(35, 35);
            this.pictureBox_RunNext.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_RunNext.TabIndex = 6;
            this.pictureBox_RunNext.TabStop = false;
            this.toolTip_clickInfo.SetToolTip(this.pictureBox_RunNext, "单步执行下一个");
            this.pictureBox_RunNext.Click += new System.EventHandler(this.pictureBox_MianRun_Click);
            this.pictureBox_RunNext.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            this.pictureBox_RunNext.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            // 
            // pictureBox_PauseRun
            // 
            this.pictureBox_PauseRun.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_PauseRun.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_PauseRun.Image")));
            this.pictureBox_PauseRun.Location = new System.Drawing.Point(40, 27);
            this.pictureBox_PauseRun.Name = "pictureBox_PauseRun";
            this.pictureBox_PauseRun.Size = new System.Drawing.Size(35, 35);
            this.pictureBox_PauseRun.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_PauseRun.TabIndex = 5;
            this.pictureBox_PauseRun.TabStop = false;
            this.toolTip_clickInfo.SetToolTip(this.pictureBox_PauseRun, "暂停当前任务");
            this.pictureBox_PauseRun.Click += new System.EventHandler(this.pictureBox_MianRun_Click);
            this.pictureBox_PauseRun.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            this.pictureBox_PauseRun.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            // 
            // pictureBox_RunHere
            // 
            this.pictureBox_RunHere.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox_RunHere.BackgroundImage")));
            this.pictureBox_RunHere.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_RunHere.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_RunHere.Location = new System.Drawing.Point(3, 27);
            this.pictureBox_RunHere.Name = "pictureBox_RunHere";
            this.pictureBox_RunHere.Size = new System.Drawing.Size(35, 35);
            this.pictureBox_RunHere.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_RunHere.TabIndex = 4;
            this.pictureBox_RunHere.TabStop = false;
            this.toolTip_clickInfo.SetToolTip(this.pictureBox_RunHere, "从此处开始执行测试");
            this.pictureBox_RunHere.Click += new System.EventHandler(this.pictureBox_MianRun_Click);
            this.pictureBox_RunHere.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            this.pictureBox_RunHere.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            // 
            // label_moveFlagForRun
            // 
            this.label_moveFlagForRun.BackColor = System.Drawing.Color.Transparent;
            this.label_moveFlagForRun.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.label_moveFlagForRun.Location = new System.Drawing.Point(1, 1);
            this.label_moveFlagForRun.Name = "label_moveFlagForRun";
            this.label_moveFlagForRun.Size = new System.Drawing.Size(197, 23);
            this.label_moveFlagForRun.TabIndex = 2;
            this.label_moveFlagForRun.Text = "Run";
            this.label_moveFlagForRun.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_moveFlagForRun.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label_moveFlagForRun_MouseDown);
            this.label_moveFlagForRun.MouseMove += new System.Windows.Forms.MouseEventHandler(this.label_moveFlagForRun_MouseMove);
            this.label_moveFlagForRun.MouseUp += new System.Windows.Forms.MouseEventHandler(this.label_moveFlagForRun_MouseUp);
            // 
            // expandablePanel_dataAdd
            // 
            this.expandablePanel_dataAdd.CanvasColor = System.Drawing.SystemColors.Control;
            this.expandablePanel_dataAdd.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.expandablePanel_dataAdd.Controls.Add(this.pictureBox_dataAddStop);
            this.expandablePanel_dataAdd.Controls.Add(this.pictureBox_changeDataAddSize);
            this.expandablePanel_dataAdd.Controls.Add(this.pictureBox_dataAddSave);
            this.expandablePanel_dataAdd.Controls.Add(this.pictureBox_dataAddclean);
            this.expandablePanel_dataAdd.Controls.Add(this.pictureBox_dataAddClose);
            this.expandablePanel_dataAdd.Controls.Add(this.label_moveFlagForDataAdd);
            this.expandablePanel_dataAdd.Controls.Add(this.listView_DataAdd);
            this.expandablePanel_dataAdd.Location = new System.Drawing.Point(389, 91);
            this.expandablePanel_dataAdd.Name = "expandablePanel_dataAdd";
            this.expandablePanel_dataAdd.Size = new System.Drawing.Size(605, 262);
            this.expandablePanel_dataAdd.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel_dataAdd.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel_dataAdd.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanel_dataAdd.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.expandablePanel_dataAdd.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.expandablePanel_dataAdd.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandablePanel_dataAdd.Style.GradientAngle = 90;
            this.expandablePanel_dataAdd.TabIndex = 6;
            this.expandablePanel_dataAdd.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel_dataAdd.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel_dataAdd.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanel_dataAdd.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.expandablePanel_dataAdd.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandablePanel_dataAdd.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.expandablePanel_dataAdd.TitleStyle.GradientAngle = 90;
            this.expandablePanel_dataAdd.TitleText = "DataBack";
            // 
            // pictureBox_dataAddStop
            // 
            this.pictureBox_dataAddStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_dataAddStop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_dataAddStop.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_dataAddStop.Image")));
            this.pictureBox_dataAddStop.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox_dataAddStop.InitialImage")));
            this.pictureBox_dataAddStop.Location = new System.Drawing.Point(492, 1);
            this.pictureBox_dataAddStop.Name = "pictureBox_dataAddStop";
            this.pictureBox_dataAddStop.Size = new System.Drawing.Size(23, 23);
            this.pictureBox_dataAddStop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_dataAddStop.TabIndex = 11;
            this.pictureBox_dataAddStop.TabStop = false;
            this.toolTip_clickInfo.SetToolTip(this.pictureBox_dataAddStop, "一直滚动到底部");
            this.pictureBox_dataAddStop.Click += new System.EventHandler(this.pictureBox_dataAddStop_Click);
            this.pictureBox_dataAddStop.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            this.pictureBox_dataAddStop.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            // 
            // pictureBox_changeDataAddSize
            // 
            this.pictureBox_changeDataAddSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_changeDataAddSize.BackColor = System.Drawing.Color.AliceBlue;
            this.pictureBox_changeDataAddSize.Cursor = System.Windows.Forms.Cursors.PanSE;
            this.pictureBox_changeDataAddSize.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_changeDataAddSize.Image")));
            this.pictureBox_changeDataAddSize.Location = new System.Drawing.Point(588, 245);
            this.pictureBox_changeDataAddSize.Name = "pictureBox_changeDataAddSize";
            this.pictureBox_changeDataAddSize.Size = new System.Drawing.Size(16, 16);
            this.pictureBox_changeDataAddSize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_changeDataAddSize.TabIndex = 7;
            this.pictureBox_changeDataAddSize.TabStop = false;
            this.pictureBox_changeDataAddSize.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_changeDataAddSize_MouseDown);
            this.pictureBox_changeDataAddSize.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_changeDataAddSize_MouseMove);
            this.pictureBox_changeDataAddSize.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_changeDataAddSize_MouseUp);
            // 
            // pictureBox_dataAddSave
            // 
            this.pictureBox_dataAddSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_dataAddSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_dataAddSave.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_dataAddSave.Image")));
            this.pictureBox_dataAddSave.Location = new System.Drawing.Point(519, 1);
            this.pictureBox_dataAddSave.Name = "pictureBox_dataAddSave";
            this.pictureBox_dataAddSave.Size = new System.Drawing.Size(23, 23);
            this.pictureBox_dataAddSave.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_dataAddSave.TabIndex = 9;
            this.pictureBox_dataAddSave.TabStop = false;
            this.toolTip_clickInfo.SetToolTip(this.pictureBox_dataAddSave, "保存数据");
            this.pictureBox_dataAddSave.Click += new System.EventHandler(this.pictureBox_dataAddSave_Click);
            this.pictureBox_dataAddSave.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            this.pictureBox_dataAddSave.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            // 
            // pictureBox_dataAddclean
            // 
            this.pictureBox_dataAddclean.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_dataAddclean.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_dataAddclean.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_dataAddclean.Image")));
            this.pictureBox_dataAddclean.Location = new System.Drawing.Point(546, 1);
            this.pictureBox_dataAddclean.Name = "pictureBox_dataAddclean";
            this.pictureBox_dataAddclean.Size = new System.Drawing.Size(23, 23);
            this.pictureBox_dataAddclean.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_dataAddclean.TabIndex = 10;
            this.pictureBox_dataAddclean.TabStop = false;
            this.toolTip_clickInfo.SetToolTip(this.pictureBox_dataAddclean, "清除数据");
            this.pictureBox_dataAddclean.Click += new System.EventHandler(this.pictureBox_dataAddclean_Click);
            this.pictureBox_dataAddclean.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            this.pictureBox_dataAddclean.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            // 
            // pictureBox_dataAddClose
            // 
            this.pictureBox_dataAddClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_dataAddClose.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_dataAddClose.Image")));
            this.pictureBox_dataAddClose.Location = new System.Drawing.Point(0, 1);
            this.pictureBox_dataAddClose.Name = "pictureBox_dataAddClose";
            this.pictureBox_dataAddClose.Size = new System.Drawing.Size(23, 23);
            this.pictureBox_dataAddClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_dataAddClose.TabIndex = 8;
            this.pictureBox_dataAddClose.TabStop = false;
            this.toolTip_clickInfo.SetToolTip(this.pictureBox_dataAddClose, "关闭窗口");
            this.pictureBox_dataAddClose.Click += new System.EventHandler(this.pictureBox_dataAddClose_Click);
            this.pictureBox_dataAddClose.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            this.pictureBox_dataAddClose.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            // 
            // label_moveFlagForDataAdd
            // 
            this.label_moveFlagForDataAdd.BackColor = System.Drawing.Color.Transparent;
            this.label_moveFlagForDataAdd.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.label_moveFlagForDataAdd.Location = new System.Drawing.Point(1, 1);
            this.label_moveFlagForDataAdd.Name = "label_moveFlagForDataAdd";
            this.label_moveFlagForDataAdd.Size = new System.Drawing.Size(574, 23);
            this.label_moveFlagForDataAdd.TabIndex = 6;
            this.label_moveFlagForDataAdd.Text = "DataBack";
            this.label_moveFlagForDataAdd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_moveFlagForDataAdd.DoubleClick += new System.EventHandler(this.label_moveFlagForDataAdd_DoubleClick);
            this.label_moveFlagForDataAdd.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label_moveFlagForDataAdd_MouseDown);
            this.label_moveFlagForDataAdd.MouseMove += new System.Windows.Forms.MouseEventHandler(this.label_moveFlagForDataAdd_MouseMove);
            this.label_moveFlagForDataAdd.MouseUp += new System.Windows.Forms.MouseEventHandler(this.label_moveFlagForDataAdd_MouseUp);
            // 
            // listView_DataAdd
            // 
            this.listView_DataAdd.BackColor = System.Drawing.Color.AliceBlue;
            this.listView_DataAdd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView_DataAdd.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader0,
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.listView_DataAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_DataAdd.FullRowSelect = true;
            this.listView_DataAdd.Location = new System.Drawing.Point(0, 26);
            this.listView_DataAdd.Name = "listView_DataAdd";
            this.listView_DataAdd.Size = new System.Drawing.Size(605, 236);
            this.listView_DataAdd.TabIndex = 2;
            this.listView_DataAdd.UseCompatibleStateImageBehavior = false;
            this.listView_DataAdd.View = System.Windows.Forms.View.Details;
            this.listView_DataAdd.DoubleClick += new System.EventHandler(this.listView_DataAdd_DoubleClick);
            // 
            // columnHeader0
            // 
            this.columnHeader0.Text = "index";
            this.columnHeader0.Width = 50;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "caseID";
            this.columnHeader1.Width = 85;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "startTime";
            this.columnHeader2.Width = 72;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "spanTime";
            this.columnHeader3.Width = 67;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "ret";
            this.columnHeader4.Width = 41;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "result";
            this.columnHeader5.Width = 156;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "remark";
            this.columnHeader6.Width = 127;
            // 
            // expandablePanel_messageBox
            // 
            this.expandablePanel_messageBox.CanvasColor = System.Drawing.SystemColors.Control;
            this.expandablePanel_messageBox.CollapseDirection = DevComponents.DotNetBar.eCollapseDirection.TopToBottom;
            this.expandablePanel_messageBox.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.expandablePanel_messageBox.Controls.Add(this.pictureBox_MessageInfoList);
            this.expandablePanel_messageBox.Controls.Add(this.richTextBox_showMessage);
            this.expandablePanel_messageBox.Expanded = false;
            this.expandablePanel_messageBox.ExpandedBounds = new System.Drawing.Rectangle(797, 421, 200, 100);
            this.expandablePanel_messageBox.Location = new System.Drawing.Point(797, 495);
            this.expandablePanel_messageBox.Name = "expandablePanel_messageBox";
            this.expandablePanel_messageBox.Size = new System.Drawing.Size(200, 26);
            this.expandablePanel_messageBox.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel_messageBox.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.expandablePanel_messageBox.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.expandablePanel_messageBox.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.expandablePanel_messageBox.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandablePanel_messageBox.Style.GradientAngle = 90;
            this.expandablePanel_messageBox.TabIndex = 33;
            this.expandablePanel_messageBox.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel_messageBox.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel_messageBox.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanel_messageBox.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.expandablePanel_messageBox.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandablePanel_messageBox.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.expandablePanel_messageBox.TitleStyle.GradientAngle = 90;
            this.expandablePanel_messageBox.TitleText = "Message";
            // 
            // pictureBox_MessageInfoList
            // 
            this.pictureBox_MessageInfoList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_MessageInfoList.BackColor = System.Drawing.Color.LightCyan;
            this.pictureBox_MessageInfoList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_MessageInfoList.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_MessageInfoList.Image")));
            this.pictureBox_MessageInfoList.Location = new System.Drawing.Point(177, 76);
            this.pictureBox_MessageInfoList.Name = "pictureBox_MessageInfoList";
            this.pictureBox_MessageInfoList.Size = new System.Drawing.Size(23, 23);
            this.pictureBox_MessageInfoList.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_MessageInfoList.TabIndex = 11;
            this.pictureBox_MessageInfoList.TabStop = false;
            this.toolTip_clickInfo.SetToolTip(this.pictureBox_MessageInfoList, "查看历史信息");
            this.pictureBox_MessageInfoList.Click += new System.EventHandler(this.pictureBox_MessageInfoList_Click);
            this.pictureBox_MessageInfoList.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            this.pictureBox_MessageInfoList.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            // 
            // richTextBox_showMessage
            // 
            this.richTextBox_showMessage.BackColor = System.Drawing.Color.LightCyan;
            this.richTextBox_showMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox_showMessage.Location = new System.Drawing.Point(0, 26);
            this.richTextBox_showMessage.Name = "richTextBox_showMessage";
            this.richTextBox_showMessage.Size = new System.Drawing.Size(200, 0);
            this.richTextBox_showMessage.TabIndex = 1;
            this.richTextBox_showMessage.Text = "";
            // 
            // pictureBox_openInterfaceTest
            // 
            this.pictureBox_openInterfaceTest.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_openInterfaceTest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_openInterfaceTest.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_openInterfaceTest.Image")));
            this.pictureBox_openInterfaceTest.Location = new System.Drawing.Point(373, 48);
            this.pictureBox_openInterfaceTest.Name = "pictureBox_openInterfaceTest";
            this.pictureBox_openInterfaceTest.Size = new System.Drawing.Size(35, 35);
            this.pictureBox_openInterfaceTest.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_openInterfaceTest.TabIndex = 32;
            this.pictureBox_openInterfaceTest.TabStop = false;
            this.toolTip_clickInfo.SetToolTip(this.pictureBox_openInterfaceTest, "打开InterfaceTest");
            this.pictureBox_openInterfaceTest.Click += new System.EventHandler(this.pictureBox_openInterfaceTest_Click);
            this.pictureBox_openInterfaceTest.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            this.pictureBox_openInterfaceTest.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            // 
            // pictureBox_exportReport
            // 
            this.pictureBox_exportReport.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_exportReport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_exportReport.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_exportReport.Image")));
            this.pictureBox_exportReport.Location = new System.Drawing.Point(332, 48);
            this.pictureBox_exportReport.Name = "pictureBox_exportReport";
            this.pictureBox_exportReport.Size = new System.Drawing.Size(35, 35);
            this.pictureBox_exportReport.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_exportReport.TabIndex = 31;
            this.pictureBox_exportReport.TabStop = false;
            this.toolTip_clickInfo.SetToolTip(this.pictureBox_exportReport, "生成测试报告");
            this.pictureBox_exportReport.Click += new System.EventHandler(this.pictureBox_exportReport_Click);
            this.pictureBox_exportReport.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            this.pictureBox_exportReport.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            // 
            // pictureBox_tryTest1
            // 
            this.pictureBox_tryTest1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_tryTest1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_tryTest1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_tryTest1.Image")));
            this.pictureBox_tryTest1.Location = new System.Drawing.Point(474, 25);
            this.pictureBox_tryTest1.Name = "pictureBox_tryTest1";
            this.pictureBox_tryTest1.Size = new System.Drawing.Size(20, 20);
            this.pictureBox_tryTest1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_tryTest1.TabIndex = 29;
            this.pictureBox_tryTest1.TabStop = false;
            this.toolTip_clickInfo.SetToolTip(this.pictureBox_tryTest1, "Test");
            this.pictureBox_tryTest1.Click += new System.EventHandler(this.pictureBox_tryTest1_Click);
            this.pictureBox_tryTest1.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            this.pictureBox_tryTest1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            // 
            // pictureBox_reLoadCase1
            // 
            this.pictureBox_reLoadCase1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_reLoadCase1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_reLoadCase1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_reLoadCase1.Image")));
            this.pictureBox_reLoadCase1.Location = new System.Drawing.Point(130, 48);
            this.pictureBox_reLoadCase1.Name = "pictureBox_reLoadCase1";
            this.pictureBox_reLoadCase1.Size = new System.Drawing.Size(35, 35);
            this.pictureBox_reLoadCase1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_reLoadCase1.TabIndex = 28;
            this.pictureBox_reLoadCase1.TabStop = false;
            this.toolTip_clickInfo.SetToolTip(this.pictureBox_reLoadCase1, "刷新当前用例集");
            this.pictureBox_reLoadCase1.Click += new System.EventHandler(this.pictureBox_reLoadCase_Click);
            this.pictureBox_reLoadCase1.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            this.pictureBox_reLoadCase1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            // 
            // tb_tryTestData
            // 
            this.tb_tryTestData.BackColor = System.Drawing.Color.Azure;
            this.tb_tryTestData.Location = new System.Drawing.Point(4, 24);
            this.tb_tryTestData.Name = "tb_tryTestData";
            this.tb_tryTestData.Size = new System.Drawing.Size(471, 21);
            this.tb_tryTestData.TabIndex = 27;
            // 
            // checkBox_run
            // 
            this.checkBox_run.AutoSize = true;
            this.checkBox_run.BackColor = System.Drawing.Color.Transparent;
            this.checkBox_run.Checked = true;
            this.checkBox_run.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_run.Location = new System.Drawing.Point(416, 67);
            this.checkBox_run.Name = "checkBox_run";
            this.checkBox_run.Size = new System.Drawing.Size(42, 16);
            this.checkBox_run.TabIndex = 26;
            this.checkBox_run.Text = "Run";
            this.checkBox_run.UseVisualStyleBackColor = false;
            this.checkBox_run.CheckedChanged += new System.EventHandler(this.checkBox_run_CheckedChanged);
            // 
            // checkBox_dataBack
            // 
            this.checkBox_dataBack.AutoSize = true;
            this.checkBox_dataBack.BackColor = System.Drawing.Color.Transparent;
            this.checkBox_dataBack.Checked = true;
            this.checkBox_dataBack.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_dataBack.Location = new System.Drawing.Point(416, 48);
            this.checkBox_dataBack.Name = "checkBox_dataBack";
            this.checkBox_dataBack.Size = new System.Drawing.Size(72, 16);
            this.checkBox_dataBack.TabIndex = 25;
            this.checkBox_dataBack.Text = "DataBack";
            this.checkBox_dataBack.UseVisualStyleBackColor = false;
            this.checkBox_dataBack.CheckedChanged += new System.EventHandler(this.checkBox_dataBack_CheckedChanged);
            // 
            // pictureBox_set1
            // 
            this.pictureBox_set1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_set1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_set1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_set1.Image")));
            this.pictureBox_set1.Location = new System.Drawing.Point(474, 1);
            this.pictureBox_set1.Name = "pictureBox_set1";
            this.pictureBox_set1.Size = new System.Drawing.Size(20, 20);
            this.pictureBox_set1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_set1.TabIndex = 24;
            this.pictureBox_set1.TabStop = false;
            this.toolTip_clickInfo.SetToolTip(this.pictureBox_set1, "公共参数设置");
            this.pictureBox_set1.Click += new System.EventHandler(this.pictureBox_set1_Click);
            this.pictureBox_set1.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            this.pictureBox_set1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            // 
            // pictureBox_set
            // 
            this.pictureBox_set.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_set.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_set.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_set.Image")));
            this.pictureBox_set.Location = new System.Drawing.Point(88, 48);
            this.pictureBox_set.Name = "pictureBox_set";
            this.pictureBox_set.Size = new System.Drawing.Size(35, 35);
            this.pictureBox_set.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_set.TabIndex = 23;
            this.pictureBox_set.TabStop = false;
            this.toolTip_clickInfo.SetToolTip(this.pictureBox_set, "运行时设置");
            this.pictureBox_set.Click += new System.EventHandler(this.pictureBox_set_Click);
            this.pictureBox_set.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            this.pictureBox_set.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            // 
            // pictureBox_stopRun2
            // 
            this.pictureBox_stopRun2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_stopRun2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_stopRun2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_stopRun2.Image")));
            this.pictureBox_stopRun2.Location = new System.Drawing.Point(47, 48);
            this.pictureBox_stopRun2.Name = "pictureBox_stopRun2";
            this.pictureBox_stopRun2.Size = new System.Drawing.Size(35, 35);
            this.pictureBox_stopRun2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_stopRun2.TabIndex = 22;
            this.pictureBox_stopRun2.TabStop = false;
            this.toolTip_clickInfo.SetToolTip(this.pictureBox_stopRun2, "终止执行");
            this.pictureBox_stopRun2.Click += new System.EventHandler(this.pictureBox_stopRun_Click);
            this.pictureBox_stopRun2.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            this.pictureBox_stopRun2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            // 
            // pictureBox_RunHere2
            // 
            this.pictureBox_RunHere2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_RunHere2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_RunHere2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_RunHere2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_RunHere2.Image")));
            this.pictureBox_RunHere2.Location = new System.Drawing.Point(6, 48);
            this.pictureBox_RunHere2.Name = "pictureBox_RunHere2";
            this.pictureBox_RunHere2.Size = new System.Drawing.Size(35, 35);
            this.pictureBox_RunHere2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_RunHere2.TabIndex = 21;
            this.pictureBox_RunHere2.TabStop = false;
            this.toolTip_clickInfo.SetToolTip(this.pictureBox_RunHere2, "从此处开始执行测试");
            this.pictureBox_RunHere2.Click += new System.EventHandler(this.pictureBox_RunHere_Click);
            this.pictureBox_RunHere2.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            this.pictureBox_RunHere2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            // 
            // trb_addRecord
            // 
            this.trb_addRecord.BackColor = System.Drawing.Color.Azure;
            this.trb_addRecord.CanFill = true;
            this.trb_addRecord.Location = new System.Drawing.Point(499, 0);
            this.trb_addRecord.MaxLine = 5000;
            this.trb_addRecord.MianDirectory = "DataRecord";
            this.trb_addRecord.Name = "trb_addRecord";
            this.trb_addRecord.Size = new System.Drawing.Size(496, 86);
            this.trb_addRecord.TabIndex = 20;
            this.trb_addRecord.TextChanged += new System.EventHandler(this.trb_addRecord_TextChanged);
            // 
            // bt_openFile
            // 
            this.bt_openFile.BackColor = System.Drawing.Color.White;
            this.bt_openFile.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bt_openFile.Location = new System.Drawing.Point(359, 1);
            this.bt_openFile.Name = "bt_openFile";
            this.bt_openFile.Size = new System.Drawing.Size(43, 21);
            this.bt_openFile.TabIndex = 18;
            this.bt_openFile.Text = "浏览";
            this.bt_openFile.UseVisualStyleBackColor = false;
            this.bt_openFile.Click += new System.EventHandler(this.bt_openFile_Click);
            // 
            // tb_caseFilePath
            // 
            this.tb_caseFilePath.BackColor = System.Drawing.Color.Beige;
            this.tb_caseFilePath.Location = new System.Drawing.Point(4, 1);
            this.tb_caseFilePath.Name = "tb_caseFilePath";
            this.tb_caseFilePath.Size = new System.Drawing.Size(355, 21);
            this.tb_caseFilePath.TabIndex = 17;
            // 
            // lb_msg2
            // 
            this.lb_msg2.AutoSize = true;
            this.lb_msg2.BackColor = System.Drawing.Color.Transparent;
            this.lb_msg2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lb_msg2.Location = new System.Drawing.Point(577, 526);
            this.lb_msg2.Name = "lb_msg2";
            this.lb_msg2.Size = new System.Drawing.Size(47, 12);
            this.lb_msg2.TabIndex = 10;
            this.lb_msg2.Text = "lb_msg2";
            // 
            // lb_msg1
            // 
            this.lb_msg1.AutoSize = true;
            this.lb_msg1.BackColor = System.Drawing.Color.Transparent;
            this.lb_msg1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lb_msg1.Location = new System.Drawing.Point(3, 526);
            this.lb_msg1.Name = "lb_msg1";
            this.lb_msg1.Size = new System.Drawing.Size(47, 12);
            this.lb_msg1.TabIndex = 8;
            this.lb_msg1.Text = "lb_msg1";
            // 
            // progressBar_case
            // 
            this.progressBar_case.AutoScroll = true;
            this.progressBar_case.IsShowMore = true;
            this.progressBar_case.IsShowTip = true;
            this.progressBar_case.Location = new System.Drawing.Point(132, 523);
            this.progressBar_case.MaxCount = 3;
            this.progressBar_case.Name = "progressBar_case";
            this.progressBar_case.ProgressBarHight = 10;
            this.progressBar_case.ShowMode = MyCommonControl.ProgressBarList.BarListShowMode.BarListQueue;
            this.progressBar_case.Size = new System.Drawing.Size(442, 18);
            this.progressBar_case.TabIndex = 7;
            // 
            // test
            // 
            this.test.Location = new System.Drawing.Point(402, 0);
            this.test.Name = "test";
            this.test.Size = new System.Drawing.Size(73, 23);
            this.test.TabIndex = 1;
            this.test.Text = "加载文件";
            this.test.UseVisualStyleBackColor = true;
            this.test.Click += new System.EventHandler(this.test_Click);
            // 
            // tvw_Case
            // 
            this.tvw_Case.ContextMenuStrip = this.contextMenuStrip_CaseTree;
            this.tvw_Case.ImageIndex = 0;
            this.tvw_Case.ImageList = this.imageListForCase;
            this.tvw_Case.Location = new System.Drawing.Point(2, 89);
            this.tvw_Case.Name = "tvw_Case";
            this.tvw_Case.SelectedImageIndex = 0;
            this.tvw_Case.Size = new System.Drawing.Size(995, 433);
            this.tvw_Case.TabIndex = 0;
            this.tvw_Case.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvw_Case_AfterSelect);
            this.tvw_Case.Click += new System.EventHandler(this.tvw_Case_Click);
            this.tvw_Case.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tvw_Case_MouseDoubleClick);
            // 
            // contextMenuStrip_CaseTree
            // 
            this.contextMenuStrip_CaseTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runToolStripMenuItem,
            this.编辑选定项ToolStripMenuItem,
            this.组件控制ToolStripMenuItem,
            this.caseParameterToolStripMenuItem});
            this.contextMenuStrip_CaseTree.Name = "contextMenuStrip_CaseTree";
            this.contextMenuStrip_CaseTree.Size = new System.Drawing.Size(153, 114);
            // 
            // runToolStripMenuItem
            // 
            this.runToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RunHereToolStripMenuItem,
            this.PauseRunToolStripMenuItem,
            this.stopRunToolStripMenuItem,
            this.RunNextToolStripMenuItem,
            this.runHereOnlyToolStripMenuItem,
            this.stopRunForceToolStripMenuItem});
            this.runToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("runToolStripMenuItem.Image")));
            this.runToolStripMenuItem.Name = "runToolStripMenuItem";
            this.runToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.runToolStripMenuItem.Text = "执行项目";
            // 
            // RunHereToolStripMenuItem
            // 
            this.RunHereToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("RunHereToolStripMenuItem.Image")));
            this.RunHereToolStripMenuItem.Name = "RunHereToolStripMenuItem";
            this.RunHereToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.RunHereToolStripMenuItem.Text = "从此处执行";
            this.RunHereToolStripMenuItem.Click += new System.EventHandler(this.RunHereToolStripMenuItem_Click);
            // 
            // PauseRunToolStripMenuItem
            // 
            this.PauseRunToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("PauseRunToolStripMenuItem.Image")));
            this.PauseRunToolStripMenuItem.Name = "PauseRunToolStripMenuItem";
            this.PauseRunToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.PauseRunToolStripMenuItem.Text = "暂停任务";
            this.PauseRunToolStripMenuItem.Click += new System.EventHandler(this.PauseRunToolStripMenuItem_Click);
            // 
            // stopRunToolStripMenuItem
            // 
            this.stopRunToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("stopRunToolStripMenuItem.Image")));
            this.stopRunToolStripMenuItem.Name = "stopRunToolStripMenuItem";
            this.stopRunToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.stopRunToolStripMenuItem.Text = "停止执行";
            this.stopRunToolStripMenuItem.Click += new System.EventHandler(this.stopRunToolStripMenuItem_Click);
            // 
            // RunNextToolStripMenuItem
            // 
            this.RunNextToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("RunNextToolStripMenuItem.Image")));
            this.RunNextToolStripMenuItem.Name = "RunNextToolStripMenuItem";
            this.RunNextToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.RunNextToolStripMenuItem.Text = "单步执行下一条";
            this.RunNextToolStripMenuItem.Click += new System.EventHandler(this.RunNextToolStripMenuItem_Click);
            // 
            // runHereOnlyToolStripMenuItem
            // 
            this.runHereOnlyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("runHereOnlyToolStripMenuItem.Image")));
            this.runHereOnlyToolStripMenuItem.Name = "runHereOnlyToolStripMenuItem";
            this.runHereOnlyToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.runHereOnlyToolStripMenuItem.Text = "仅执行选定项";
            this.runHereOnlyToolStripMenuItem.Click += new System.EventHandler(this.runHereOnlyToolStripMenuItem_Click);
            // 
            // 编辑选定项ToolStripMenuItem
            // 
            this.编辑选定项ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ModifyToolStripMenuItem,
            this.AddToolStripMenuItem,
            this.DelToolStripMenuItem});
            this.编辑选定项ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("编辑选定项ToolStripMenuItem.Image")));
            this.编辑选定项ToolStripMenuItem.Name = "编辑选定项ToolStripMenuItem";
            this.编辑选定项ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.编辑选定项ToolStripMenuItem.Text = "编辑选定项";
            // 
            // ModifyToolStripMenuItem
            // 
            this.ModifyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("ModifyToolStripMenuItem.Image")));
            this.ModifyToolStripMenuItem.Name = "ModifyToolStripMenuItem";
            this.ModifyToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.ModifyToolStripMenuItem.Text = "修改";
            this.ModifyToolStripMenuItem.Click += new System.EventHandler(this.ModifyToolStripMenuItem_Click);
            // 
            // AddToolStripMenuItem
            // 
            this.AddToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("AddToolStripMenuItem.Image")));
            this.AddToolStripMenuItem.Name = "AddToolStripMenuItem";
            this.AddToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.AddToolStripMenuItem.Text = "添加";
            this.AddToolStripMenuItem.Click += new System.EventHandler(this.AddToolStripMenuItem_Click);
            // 
            // DelToolStripMenuItem
            // 
            this.DelToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("DelToolStripMenuItem.Image")));
            this.DelToolStripMenuItem.Name = "DelToolStripMenuItem";
            this.DelToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.DelToolStripMenuItem.Text = "删除";
            this.DelToolStripMenuItem.Click += new System.EventHandler(this.DelToolStripMenuItem_Click);
            // 
            // 组件控制ToolStripMenuItem
            // 
            this.组件控制ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_dataAdd,
            this.ToolStripMenuItem_runQuick,
            this.ToolStripMenuItem_editQuick});
            this.组件控制ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("组件控制ToolStripMenuItem.Image")));
            this.组件控制ToolStripMenuItem.Name = "组件控制ToolStripMenuItem";
            this.组件控制ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.组件控制ToolStripMenuItem.Text = "组件控制";
            // 
            // ToolStripMenuItem_dataAdd
            // 
            this.ToolStripMenuItem_dataAdd.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripMenuItem_dataAdd.Image")));
            this.ToolStripMenuItem_dataAdd.Name = "ToolStripMenuItem_dataAdd";
            this.ToolStripMenuItem_dataAdd.Size = new System.Drawing.Size(152, 22);
            this.ToolStripMenuItem_dataAdd.Text = "信息反馈";
            this.ToolStripMenuItem_dataAdd.Click += new System.EventHandler(this.ToolStripMenuItem_dataAdd_Click);
            // 
            // ToolStripMenuItem_runQuick
            // 
            this.ToolStripMenuItem_runQuick.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripMenuItem_runQuick.Image")));
            this.ToolStripMenuItem_runQuick.Name = "ToolStripMenuItem_runQuick";
            this.ToolStripMenuItem_runQuick.Size = new System.Drawing.Size(152, 22);
            this.ToolStripMenuItem_runQuick.Text = "快速运行";
            this.ToolStripMenuItem_runQuick.Click += new System.EventHandler(this.ToolStripMenuItem_runQuick_Click);
            // 
            // ToolStripMenuItem_editQuick
            // 
            this.ToolStripMenuItem_editQuick.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripMenuItem_editQuick.Image")));
            this.ToolStripMenuItem_editQuick.Name = "ToolStripMenuItem_editQuick";
            this.ToolStripMenuItem_editQuick.Size = new System.Drawing.Size(152, 22);
            this.ToolStripMenuItem_editQuick.Text = "快速编辑";
            this.ToolStripMenuItem_editQuick.Click += new System.EventHandler(this.ToolStripMenuItem_editQuick_Click);
            // 
            // caseParameterToolStripMenuItem
            // 
            this.caseParameterToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("caseParameterToolStripMenuItem.Image")));
            this.caseParameterToolStripMenuItem.Name = "caseParameterToolStripMenuItem";
            this.caseParameterToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.caseParameterToolStripMenuItem.Text = "运行时参数";
            this.caseParameterToolStripMenuItem.Click += new System.EventHandler(this.caseParameterToolStripMenuItem_Click);
            // 
            // imageListForCase
            // 
            this.imageListForCase.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListForCase.ImageStream")));
            this.imageListForCase.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListForCase.Images.SetKeyName(0, "1_1.png");
            this.imageListForCase.Images.SetKeyName(1, "2_1.png");
            this.imageListForCase.Images.SetKeyName(2, "2.2.png");
            this.imageListForCase.Images.SetKeyName(3, "0_1.png");
            this.imageListForCase.Images.SetKeyName(4, "0_2.png");
            this.imageListForCase.Images.SetKeyName(5, "2_3.png");
            this.imageListForCase.Images.SetKeyName(6, "2_4.png");
            this.imageListForCase.Images.SetKeyName(7, "2_5.png");
            this.imageListForCase.Images.SetKeyName(8, "2_6.png");
            this.imageListForCase.Images.SetKeyName(9, "2_7.png");
            this.imageListForCase.Images.SetKeyName(10, "2_8.png");
            this.imageListForCase.Images.SetKeyName(11, "2_9.png");
            this.imageListForCase.Images.SetKeyName(12, "2_10.png");
            this.imageListForCase.Images.SetKeyName(13, "2_11.png");
            this.imageListForCase.Images.SetKeyName(14, "2_12.png");
            this.imageListForCase.Images.SetKeyName(15, "2_13.png");
            this.imageListForCase.Images.SetKeyName(16, "2_14.png");
            this.imageListForCase.Images.SetKeyName(17, "0_3.png");
            this.imageListForCase.Images.SetKeyName(18, "2_15.png");
            this.imageListForCase.Images.SetKeyName(19, "2_16.png");
            this.imageListForCase.Images.SetKeyName(20, "2_17.png");
            this.imageListForCase.Images.SetKeyName(21, "2_18.png");
            this.imageListForCase.Images.SetKeyName(22, "2_19.png");
            this.imageListForCase.Images.SetKeyName(23, "2_20.png");
            this.imageListForCase.Images.SetKeyName(24, "2_21.png");
            this.imageListForCase.Images.SetKeyName(25, "2_22.png");
            // 
            // pictureBox_caseParameter
            // 
            this.pictureBox_caseParameter.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_caseParameter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_caseParameter.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_caseParameter.Image")));
            this.pictureBox_caseParameter.Location = new System.Drawing.Point(171, 48);
            this.pictureBox_caseParameter.Name = "pictureBox_caseParameter";
            this.pictureBox_caseParameter.Size = new System.Drawing.Size(35, 35);
            this.pictureBox_caseParameter.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_caseParameter.TabIndex = 34;
            this.pictureBox_caseParameter.TabStop = false;
            this.toolTip_clickInfo.SetToolTip(this.pictureBox_caseParameter, "编辑运行时参数");
            this.pictureBox_caseParameter.Click += new System.EventHandler(this.pictureBox_caseParameter_Click);
            this.pictureBox_caseParameter.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            this.pictureBox_caseParameter.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            // 
            // lb_msg5
            // 
            this.lb_msg5.AutoSize = true;
            this.lb_msg5.BackColor = System.Drawing.Color.Transparent;
            this.lb_msg5.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lb_msg5.Location = new System.Drawing.Point(916, 527);
            this.lb_msg5.Name = "lb_msg5";
            this.lb_msg5.Size = new System.Drawing.Size(47, 12);
            this.lb_msg5.TabIndex = 37;
            this.lb_msg5.Text = "lb_msg5";
            // 
            // lb_msg4
            // 
            this.lb_msg4.AutoSize = true;
            this.lb_msg4.BackColor = System.Drawing.Color.Transparent;
            this.lb_msg4.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lb_msg4.Location = new System.Drawing.Point(838, 527);
            this.lb_msg4.Name = "lb_msg4";
            this.lb_msg4.Size = new System.Drawing.Size(47, 12);
            this.lb_msg4.TabIndex = 36;
            this.lb_msg4.Text = "lb_msg4";
            // 
            // lb_msg3
            // 
            this.lb_msg3.AutoSize = true;
            this.lb_msg3.BackColor = System.Drawing.Color.Transparent;
            this.lb_msg3.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lb_msg3.Location = new System.Drawing.Point(751, 527);
            this.lb_msg3.Name = "lb_msg3";
            this.lb_msg3.Size = new System.Drawing.Size(47, 12);
            this.lb_msg3.TabIndex = 35;
            this.lb_msg3.Text = "lb_msg3";
            // 
            // ribbonPanel3
            // 
            this.ribbonPanel3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.ribbonPanel3.Controls.Add(this.pictureBox_selRunerMax);
            this.ribbonPanel3.Controls.Add(this.listView_SelectRunner);
            this.ribbonPanel3.Controls.Add(this.lb_cr_info3);
            this.ribbonPanel3.Controls.Add(this.lb_cr_info2);
            this.ribbonPanel3.Controls.Add(this.lb_cr_info1);
            this.ribbonPanel3.Controls.Add(this.llb_showRunner);
            this.ribbonPanel3.Controls.Add(this.pictureBox_cr_delSelect);
            this.ribbonPanel3.Controls.Add(this.pictureBox_cr_StopSelect);
            this.ribbonPanel3.Controls.Add(this.pictureBox_cr_runSelect);
            this.ribbonPanel3.Controls.Add(this.cb_cr_SelectAll);
            this.ribbonPanel3.Controls.Add(this.pictureBox_cr_addUser);
            this.ribbonPanel3.Controls.Add(this.cb_cr_isCb);
            this.ribbonPanel3.Controls.Add(this.listView_CaseRunner);
            this.ribbonPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ribbonPanel3.Location = new System.Drawing.Point(0, 55);
            this.ribbonPanel3.Name = "ribbonPanel3";
            this.ribbonPanel3.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.ribbonPanel3.Size = new System.Drawing.Size(1000, 543);
            this.ribbonPanel3.TabIndex = 3;
            this.ribbonPanel3.Visible = false;
            // 
            // pictureBox_selRunerMax
            // 
            this.pictureBox_selRunerMax.BackColor = System.Drawing.Color.AliceBlue;
            this.pictureBox_selRunerMax.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_selRunerMax.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_selRunerMax.Image")));
            this.pictureBox_selRunerMax.Location = new System.Drawing.Point(973, 0);
            this.pictureBox_selRunerMax.Name = "pictureBox_selRunerMax";
            this.pictureBox_selRunerMax.Size = new System.Drawing.Size(23, 23);
            this.pictureBox_selRunerMax.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_selRunerMax.TabIndex = 44;
            this.pictureBox_selRunerMax.TabStop = false;
            this.pictureBox_selRunerMax.Click += new System.EventHandler(this.pictureBox_selRunerMax_Click);
            this.pictureBox_selRunerMax.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            this.pictureBox_selRunerMax.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            // 
            // listView_SelectRunner
            // 
            this.listView_SelectRunner.BackColor = System.Drawing.Color.AliceBlue;
            this.listView_SelectRunner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView_SelectRunner.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader18,
            this.columnHeader19,
            this.columnHeader20,
            this.columnHeader21,
            this.columnHeader22,
            this.columnHeader23,
            this.columnHeader24});
            this.listView_SelectRunner.FullRowSelect = true;
            this.listView_SelectRunner.Location = new System.Drawing.Point(212, 0);
            this.listView_SelectRunner.Name = "listView_SelectRunner";
            this.listView_SelectRunner.Size = new System.Drawing.Size(785, 90);
            this.listView_SelectRunner.TabIndex = 26;
            this.listView_SelectRunner.UseCompatibleStateImageBehavior = false;
            this.listView_SelectRunner.View = System.Windows.Forms.View.Details;
            this.listView_SelectRunner.DoubleClick += new System.EventHandler(this.listView_SelectRunner_DoubleClick);
            // 
            // columnHeader18
            // 
            this.columnHeader18.Text = "index";
            this.columnHeader18.Width = 50;
            // 
            // columnHeader19
            // 
            this.columnHeader19.Text = "caseID";
            this.columnHeader19.Width = 96;
            // 
            // columnHeader20
            // 
            this.columnHeader20.Text = "startTime";
            this.columnHeader20.Width = 72;
            // 
            // columnHeader21
            // 
            this.columnHeader21.Text = "spanTime";
            this.columnHeader21.Width = 79;
            // 
            // columnHeader22
            // 
            this.columnHeader22.Text = "ret";
            this.columnHeader22.Width = 58;
            // 
            // columnHeader23
            // 
            this.columnHeader23.Text = "result";
            this.columnHeader23.Width = 299;
            // 
            // columnHeader24
            // 
            this.columnHeader24.Text = "remark";
            this.columnHeader24.Width = 107;
            // 
            // lb_cr_info3
            // 
            this.lb_cr_info3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lb_cr_info3.AutoSize = true;
            this.lb_cr_info3.BackColor = System.Drawing.Color.Transparent;
            this.lb_cr_info3.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lb_cr_info3.Location = new System.Drawing.Point(210, 528);
            this.lb_cr_info3.Name = "lb_cr_info3";
            this.lb_cr_info3.Size = new System.Drawing.Size(71, 12);
            this.lb_cr_info3.TabIndex = 43;
            this.lb_cr_info3.Text = "lb_cr_info3";
            // 
            // lb_cr_info2
            // 
            this.lb_cr_info2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lb_cr_info2.AutoSize = true;
            this.lb_cr_info2.BackColor = System.Drawing.Color.Transparent;
            this.lb_cr_info2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lb_cr_info2.Location = new System.Drawing.Point(106, 527);
            this.lb_cr_info2.Name = "lb_cr_info2";
            this.lb_cr_info2.Size = new System.Drawing.Size(71, 12);
            this.lb_cr_info2.TabIndex = 42;
            this.lb_cr_info2.Text = "lb_cr_info1";
            // 
            // lb_cr_info1
            // 
            this.lb_cr_info1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lb_cr_info1.AutoSize = true;
            this.lb_cr_info1.BackColor = System.Drawing.Color.Transparent;
            this.lb_cr_info1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lb_cr_info1.Location = new System.Drawing.Point(6, 527);
            this.lb_cr_info1.Name = "lb_cr_info1";
            this.lb_cr_info1.Size = new System.Drawing.Size(71, 12);
            this.lb_cr_info1.TabIndex = 41;
            this.lb_cr_info1.Text = "lb_cr_info1";
            // 
            // llb_showRunner
            // 
            this.llb_showRunner.ActiveLinkColor = System.Drawing.Color.DimGray;
            this.llb_showRunner.BackColor = System.Drawing.Color.Transparent;
            this.llb_showRunner.LinkColor = System.Drawing.Color.SlateBlue;
            this.llb_showRunner.Location = new System.Drawing.Point(86, 4);
            this.llb_showRunner.Name = "llb_showRunner";
            this.llb_showRunner.Size = new System.Drawing.Size(112, 12);
            this.llb_showRunner.TabIndex = 27;
            this.llb_showRunner.TabStop = true;
            this.llb_showRunner.Text = "runnerName >";
            this.llb_showRunner.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.toolTip_clickInfo.SetToolTip(this.llb_showRunner, "点击停止刷新");
            this.llb_showRunner.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llb_showRunner_LinkClicked);
            // 
            // pictureBox_cr_delSelect
            // 
            this.pictureBox_cr_delSelect.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_cr_delSelect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_cr_delSelect.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_cr_delSelect.Image")));
            this.pictureBox_cr_delSelect.Location = new System.Drawing.Point(168, 44);
            this.pictureBox_cr_delSelect.Name = "pictureBox_cr_delSelect";
            this.pictureBox_cr_delSelect.Size = new System.Drawing.Size(35, 35);
            this.pictureBox_cr_delSelect.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_cr_delSelect.TabIndex = 25;
            this.pictureBox_cr_delSelect.TabStop = false;
            this.toolTip_clickInfo.SetToolTip(this.pictureBox_cr_delSelect, "删除所有勾选用户");
            this.pictureBox_cr_delSelect.Click += new System.EventHandler(this.pictureBox_CR_Window_Click);
            this.pictureBox_cr_delSelect.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            this.pictureBox_cr_delSelect.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            // 
            // pictureBox_cr_StopSelect
            // 
            this.pictureBox_cr_StopSelect.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_cr_StopSelect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_cr_StopSelect.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_cr_StopSelect.Image")));
            this.pictureBox_cr_StopSelect.Location = new System.Drawing.Point(127, 44);
            this.pictureBox_cr_StopSelect.Name = "pictureBox_cr_StopSelect";
            this.pictureBox_cr_StopSelect.Size = new System.Drawing.Size(35, 35);
            this.pictureBox_cr_StopSelect.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_cr_StopSelect.TabIndex = 24;
            this.pictureBox_cr_StopSelect.TabStop = false;
            this.toolTip_clickInfo.SetToolTip(this.pictureBox_cr_StopSelect, "停止所有勾选用户");
            this.pictureBox_cr_StopSelect.Click += new System.EventHandler(this.pictureBox_CR_Window_Click);
            this.pictureBox_cr_StopSelect.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            this.pictureBox_cr_StopSelect.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            // 
            // pictureBox_cr_runSelect
            // 
            this.pictureBox_cr_runSelect.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_cr_runSelect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_cr_runSelect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_cr_runSelect.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_cr_runSelect.Image")));
            this.pictureBox_cr_runSelect.Location = new System.Drawing.Point(86, 44);
            this.pictureBox_cr_runSelect.Name = "pictureBox_cr_runSelect";
            this.pictureBox_cr_runSelect.Size = new System.Drawing.Size(35, 35);
            this.pictureBox_cr_runSelect.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_cr_runSelect.TabIndex = 23;
            this.pictureBox_cr_runSelect.TabStop = false;
            this.toolTip_clickInfo.SetToolTip(this.pictureBox_cr_runSelect, "启动所有勾选用户");
            this.pictureBox_cr_runSelect.Click += new System.EventHandler(this.pictureBox_CR_Window_Click);
            this.pictureBox_cr_runSelect.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            this.pictureBox_cr_runSelect.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            // 
            // cb_cr_SelectAll
            // 
            this.cb_cr_SelectAll.AutoSize = true;
            this.cb_cr_SelectAll.BackColor = System.Drawing.Color.Transparent;
            this.cb_cr_SelectAll.Location = new System.Drawing.Point(86, 21);
            this.cb_cr_SelectAll.Name = "cb_cr_SelectAll";
            this.cb_cr_SelectAll.Size = new System.Drawing.Size(72, 16);
            this.cb_cr_SelectAll.TabIndex = 6;
            this.cb_cr_SelectAll.Text = "全部选择";
            this.cb_cr_SelectAll.UseVisualStyleBackColor = false;
            this.cb_cr_SelectAll.CheckStateChanged += new System.EventHandler(this.cb_cr_SelectAll_CheckStateChanged);
            // 
            // pictureBox_cr_addUser
            // 
            this.pictureBox_cr_addUser.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_cr_addUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_cr_addUser.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_cr_addUser.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_cr_addUser.Image")));
            this.pictureBox_cr_addUser.Location = new System.Drawing.Point(6, 3);
            this.pictureBox_cr_addUser.Name = "pictureBox_cr_addUser";
            this.pictureBox_cr_addUser.Size = new System.Drawing.Size(50, 50);
            this.pictureBox_cr_addUser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_cr_addUser.TabIndex = 5;
            this.pictureBox_cr_addUser.TabStop = false;
            this.toolTip_clickInfo.SetToolTip(this.pictureBox_cr_addUser, "添加新用户");
            this.pictureBox_cr_addUser.Click += new System.EventHandler(this.pictureBox_CR_Window_Click);
            this.pictureBox_cr_addUser.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            this.pictureBox_cr_addUser.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            // 
            // cb_cr_isCb
            // 
            this.cb_cr_isCb.AutoSize = true;
            this.cb_cr_isCb.BackColor = System.Drawing.Color.Transparent;
            this.cb_cr_isCb.Checked = true;
            this.cb_cr_isCb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_cr_isCb.Location = new System.Drawing.Point(6, 67);
            this.cb_cr_isCb.Name = "cb_cr_isCb";
            this.cb_cr_isCb.Size = new System.Drawing.Size(72, 16);
            this.cb_cr_isCb.TabIndex = 2;
            this.cb_cr_isCb.Text = "复选模式";
            this.cb_cr_isCb.UseVisualStyleBackColor = false;
            this.cb_cr_isCb.CheckedChanged += new System.EventHandler(this.cb_cr_isCb_CheckedChanged);
            // 
            // listView_CaseRunner
            // 
            this.listView_CaseRunner.CheckBoxes = true;
            this.listView_CaseRunner.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader13,
            this.columnHeader14,
            this.columnHeader15,
            this.columnHeader16,
            this.columnHeader17});
            this.listView_CaseRunner.FullRowSelect = true;
            this.listView_CaseRunner.Location = new System.Drawing.Point(3, 91);
            this.listView_CaseRunner.Name = "listView_CaseRunner";
            this.listView_CaseRunner.Size = new System.Drawing.Size(994, 433);
            this.listView_CaseRunner.TabIndex = 0;
            this.listView_CaseRunner.UseCompatibleStateImageBehavior = false;
            this.listView_CaseRunner.View = System.Windows.Forms.View.Details;
            this.listView_CaseRunner.SelectedIndexChanged += new System.EventHandler(this.listView_CaseRunner_SelectedIndexChanged);
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Name";
            this.columnHeader9.Width = 56;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "StartCell";
            this.columnHeader10.Width = 78;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "NowCell";
            this.columnHeader11.Width = 70;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "RunDetails";
            this.columnHeader12.Width = 177;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "CellResult";
            this.columnHeader13.Width = 88;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "Time";
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "RunProgress";
            this.columnHeader15.Width = 187;
            // 
            // columnHeader16
            // 
            this.columnHeader16.Text = "State";
            this.columnHeader16.Width = 110;
            // 
            // columnHeader17
            // 
            this.columnHeader17.Text = "RunnerControl";
            this.columnHeader17.Width = 102;
            // 
            // ribbonPanel2
            // 
            this.ribbonPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.ribbonPanel2.Controls.Add(this.pictureBox_gwListMax);
            this.ribbonPanel2.Controls.Add(this.listViewEx_GWlist);
            this.ribbonPanel2.Controls.Add(this.richTextBox_BroadcastRecord);
            this.ribbonPanel2.Controls.Add(this.panel_configMain);
            this.ribbonPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ribbonPanel2.Location = new System.Drawing.Point(0, 55);
            this.ribbonPanel2.Name = "ribbonPanel2";
            this.ribbonPanel2.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.ribbonPanel2.Size = new System.Drawing.Size(1000, 543);
            this.ribbonPanel2.TabIndex = 2;
            this.ribbonPanel2.Visible = false;
            // 
            // pictureBox_gwListMax
            // 
            this.pictureBox_gwListMax.BackColor = System.Drawing.Color.AliceBlue;
            this.pictureBox_gwListMax.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_gwListMax.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_gwListMax.Image")));
            this.pictureBox_gwListMax.Location = new System.Drawing.Point(656, 5);
            this.pictureBox_gwListMax.Name = "pictureBox_gwListMax";
            this.pictureBox_gwListMax.Size = new System.Drawing.Size(23, 23);
            this.pictureBox_gwListMax.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_gwListMax.TabIndex = 22;
            this.pictureBox_gwListMax.TabStop = false;
            this.pictureBox_gwListMax.Click += new System.EventHandler(this.pictureBox_gwListMax_Click);
            this.pictureBox_gwListMax.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            this.pictureBox_gwListMax.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            // 
            // listViewEx_GWlist
            // 
            this.listViewEx_GWlist.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewEx_GWlist.BackColor = System.Drawing.Color.AliceBlue;
            this.listViewEx_GWlist.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_vc_id,
            this.columnHeader_vc_ip,
            this.columnHeader_vc_sn,
            this.columnHeader_vc_Alias,
            this.columnHeader1_vc_Version,
            this.columnHeader_vc_Ability});
            this.listViewEx_GWlist.ContextMenuStrip = this.contextMenuStrip_GwList;
            this.listViewEx_GWlist.FullRowSelect = true;
            this.listViewEx_GWlist.Location = new System.Drawing.Point(4, 3);
            this.listViewEx_GWlist.Name = "listViewEx_GWlist";
            this.listViewEx_GWlist.Size = new System.Drawing.Size(676, 120);
            this.listViewEx_GWlist.TabIndex = 1;
            this.listViewEx_GWlist.UseCompatibleStateImageBehavior = false;
            this.listViewEx_GWlist.View = System.Windows.Forms.View.Details;
            this.listViewEx_GWlist.DoubleClick += new System.EventHandler(this.listViewEx_GWlist_DoubleClick);
            // 
            // columnHeader_vc_id
            // 
            this.columnHeader_vc_id.Text = "ID";
            this.columnHeader_vc_id.Width = 39;
            // 
            // columnHeader_vc_ip
            // 
            this.columnHeader_vc_ip.Text = "IP";
            this.columnHeader_vc_ip.Width = 104;
            // 
            // columnHeader_vc_sn
            // 
            this.columnHeader_vc_sn.Text = "SN";
            this.columnHeader_vc_sn.Width = 107;
            // 
            // columnHeader_vc_Alias
            // 
            this.columnHeader_vc_Alias.Text = "别名";
            this.columnHeader_vc_Alias.Width = 123;
            // 
            // columnHeader1_vc_Version
            // 
            this.columnHeader1_vc_Version.Text = "版本状态";
            this.columnHeader1_vc_Version.Width = 133;
            // 
            // columnHeader_vc_Ability
            // 
            this.columnHeader_vc_Ability.Text = "网关能力";
            this.columnHeader_vc_Ability.Width = 103;
            // 
            // contextMenuStrip_GwList
            // 
            this.contextMenuStrip_GwList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            ToolStripMenuItem_DirectionalTest_1});
            this.contextMenuStrip_GwList.Name = "contextMenuStrip_GwList";
            this.contextMenuStrip_GwList.Size = new System.Drawing.Size(137, 26);
            // 
            // richTextBox_BroadcastRecord
            // 
            this.richTextBox_BroadcastRecord.BackColor = System.Drawing.Color.Azure;
            this.richTextBox_BroadcastRecord.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox_BroadcastRecord.HideSelection = false;
            this.richTextBox_BroadcastRecord.Location = new System.Drawing.Point(683, 3);
            this.richTextBox_BroadcastRecord.Name = "richTextBox_BroadcastRecord";
            this.richTextBox_BroadcastRecord.Size = new System.Drawing.Size(311, 118);
            this.richTextBox_BroadcastRecord.TabIndex = 21;
            this.richTextBox_BroadcastRecord.Text = "";
            this.richTextBox_BroadcastRecord.TextChanged += new System.EventHandler(this.richTextBox_BroadcastRecord_TextChanged);
            // 
            // panel_configMain
            // 
            this.panel_configMain.BackColor = System.Drawing.Color.White;
            this.panel_configMain.Controls.Add(this.expandablePanel_vaneWifiConfig);
            this.panel_configMain.Location = new System.Drawing.Point(3, 127);
            this.panel_configMain.Name = "panel_configMain";
            this.panel_configMain.Size = new System.Drawing.Size(994, 392);
            this.panel_configMain.TabIndex = 0;
            // 
            // expandablePanel_vaneWifiConfig
            // 
            this.expandablePanel_vaneWifiConfig.CanvasColor = System.Drawing.SystemColors.Control;
            this.expandablePanel_vaneWifiConfig.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.expandablePanel_vaneWifiConfig.Controls.Add(this.pictureBox1);
            this.expandablePanel_vaneWifiConfig.Controls.Add(this.pictureBox_startWifiConfig);
            this.expandablePanel_vaneWifiConfig.Controls.Add(this.lb_WifiConfig_info3);
            this.expandablePanel_vaneWifiConfig.Controls.Add(this.lb_WifiConfig_info2);
            this.expandablePanel_vaneWifiConfig.Controls.Add(this.lb_WifiConfig_info1);
            this.expandablePanel_vaneWifiConfig.Controls.Add(this.tb_wifiCfg_Key);
            this.expandablePanel_vaneWifiConfig.Controls.Add(this.tb_wifiCfg_SSID);
            this.expandablePanel_vaneWifiConfig.Controls.Add(this.cb_wifiCfg_Mode);
            this.expandablePanel_vaneWifiConfig.Controls.Add(this.listView_WifiConfigDataBack);
            this.expandablePanel_vaneWifiConfig.Controls.Add(this.progressBarX_WifiConfig);
            this.expandablePanel_vaneWifiConfig.Expanded = false;
            this.expandablePanel_vaneWifiConfig.ExpandedBounds = new System.Drawing.Rectangle(590, 0, 403, 195);
            this.expandablePanel_vaneWifiConfig.Location = new System.Drawing.Point(590, 0);
            this.expandablePanel_vaneWifiConfig.Name = "expandablePanel_vaneWifiConfig";
            this.expandablePanel_vaneWifiConfig.Size = new System.Drawing.Size(403, 26);
            this.expandablePanel_vaneWifiConfig.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel_vaneWifiConfig.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel_vaneWifiConfig.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanel_vaneWifiConfig.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.expandablePanel_vaneWifiConfig.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.expandablePanel_vaneWifiConfig.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandablePanel_vaneWifiConfig.Style.GradientAngle = 90;
            this.expandablePanel_vaneWifiConfig.TabIndex = 0;
            this.expandablePanel_vaneWifiConfig.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel_vaneWifiConfig.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel_vaneWifiConfig.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanel_vaneWifiConfig.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.expandablePanel_vaneWifiConfig.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandablePanel_vaneWifiConfig.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.expandablePanel_vaneWifiConfig.TitleStyle.GradientAngle = 90;
            this.expandablePanel_vaneWifiConfig.TitleText = "Vane Wifi Config";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Location = new System.Drawing.Point(383, 176);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(18, 18);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 33;
            this.pictureBox1.TabStop = false;
            this.toolTip_clickInfo.SetToolTip(this.pictureBox1, "wifi配置监听");
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pictureBox_startWifiConfig
            // 
            this.pictureBox_startWifiConfig.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_startWifiConfig.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox_startWifiConfig.BackgroundImage")));
            this.pictureBox_startWifiConfig.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_startWifiConfig.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_startWifiConfig.Location = new System.Drawing.Point(173, 113);
            this.pictureBox_startWifiConfig.Name = "pictureBox_startWifiConfig";
            this.pictureBox_startWifiConfig.Size = new System.Drawing.Size(25, 25);
            this.pictureBox_startWifiConfig.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_startWifiConfig.TabIndex = 32;
            this.pictureBox_startWifiConfig.TabStop = false;
            this.toolTip_clickInfo.SetToolTip(this.pictureBox_startWifiConfig, "WIFI配置");
            this.pictureBox_startWifiConfig.Click += new System.EventHandler(this.pictureBox_startWifiConfig_Click);
            this.pictureBox_startWifiConfig.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            this.pictureBox_startWifiConfig.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            // 
            // lb_WifiConfig_info3
            // 
            this.lb_WifiConfig_info3.AutoSize = true;
            this.lb_WifiConfig_info3.Location = new System.Drawing.Point(6, 94);
            this.lb_WifiConfig_info3.Name = "lb_WifiConfig_info3";
            this.lb_WifiConfig_info3.Size = new System.Drawing.Size(29, 12);
            this.lb_WifiConfig_info3.TabIndex = 31;
            this.lb_WifiConfig_info3.Text = "KEY:";
            // 
            // lb_WifiConfig_info2
            // 
            this.lb_WifiConfig_info2.AutoSize = true;
            this.lb_WifiConfig_info2.Location = new System.Drawing.Point(6, 65);
            this.lb_WifiConfig_info2.Name = "lb_WifiConfig_info2";
            this.lb_WifiConfig_info2.Size = new System.Drawing.Size(65, 12);
            this.lb_WifiConfig_info2.TabIndex = 30;
            this.lb_WifiConfig_info2.Text = "加密模式：";
            // 
            // lb_WifiConfig_info1
            // 
            this.lb_WifiConfig_info1.AutoSize = true;
            this.lb_WifiConfig_info1.Location = new System.Drawing.Point(6, 37);
            this.lb_WifiConfig_info1.Name = "lb_WifiConfig_info1";
            this.lb_WifiConfig_info1.Size = new System.Drawing.Size(35, 12);
            this.lb_WifiConfig_info1.TabIndex = 29;
            this.lb_WifiConfig_info1.Text = "SSID:";
            // 
            // tb_wifiCfg_Key
            // 
            this.tb_wifiCfg_Key.Location = new System.Drawing.Point(75, 89);
            this.tb_wifiCfg_Key.Name = "tb_wifiCfg_Key";
            this.tb_wifiCfg_Key.Size = new System.Drawing.Size(122, 21);
            this.tb_wifiCfg_Key.TabIndex = 28;
            // 
            // tb_wifiCfg_SSID
            // 
            this.tb_wifiCfg_SSID.Location = new System.Drawing.Point(75, 33);
            this.tb_wifiCfg_SSID.Name = "tb_wifiCfg_SSID";
            this.tb_wifiCfg_SSID.Size = new System.Drawing.Size(122, 21);
            this.tb_wifiCfg_SSID.TabIndex = 27;
            // 
            // cb_wifiCfg_Mode
            // 
            this.cb_wifiCfg_Mode.FormattingEnabled = true;
            this.cb_wifiCfg_Mode.Location = new System.Drawing.Point(75, 61);
            this.cb_wifiCfg_Mode.Name = "cb_wifiCfg_Mode";
            this.cb_wifiCfg_Mode.Size = new System.Drawing.Size(122, 20);
            this.cb_wifiCfg_Mode.TabIndex = 26;
            // 
            // listView_WifiConfigDataBack
            // 
            this.listView_WifiConfigDataBack.BackColor = System.Drawing.Color.AliceBlue;
            this.listView_WifiConfigDataBack.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7,
            this.columnHeader8});
            this.listView_WifiConfigDataBack.Location = new System.Drawing.Point(203, 26);
            this.listView_WifiConfigDataBack.Name = "listView_WifiConfigDataBack";
            this.listView_WifiConfigDataBack.Size = new System.Drawing.Size(197, 151);
            this.listView_WifiConfigDataBack.TabIndex = 25;
            this.listView_WifiConfigDataBack.UseCompatibleStateImageBehavior = false;
            this.listView_WifiConfigDataBack.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "项目";
            this.columnHeader7.Width = 73;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "内容";
            this.columnHeader8.Width = 175;
            // 
            // progressBarX_WifiConfig
            // 
            this.progressBarX_WifiConfig.Location = new System.Drawing.Point(3, 177);
            this.progressBarX_WifiConfig.Maximum = 30;
            this.progressBarX_WifiConfig.Name = "progressBarX_WifiConfig";
            this.progressBarX_WifiConfig.Size = new System.Drawing.Size(379, 16);
            this.progressBarX_WifiConfig.TabIndex = 1;
            this.progressBarX_WifiConfig.Text = "WifiConfig";
            // 
            // ribbonPanel4
            // 
            this.ribbonPanel4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.ribbonPanel4.Controls.Add(this.pictureBox_rr_DelSelect);
            this.ribbonPanel4.Controls.Add(this.pictureBox_rr_RefreshSelect);
            this.ribbonPanel4.Controls.Add(this.pictureBox_rr_PuaseSelect);
            this.ribbonPanel4.Controls.Add(this.pictureBox_rr_StopSelect);
            this.ribbonPanel4.Controls.Add(this.pictureBox_rr_RunSelect);
            this.ribbonPanel4.Controls.Add(this.pictureBox_ConnectHost);
            this.ribbonPanel4.Controls.Add(this.advTree_remoteTree);
            this.ribbonPanel4.Controls.Add(this.panel_RemoteRunner);
            this.ribbonPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ribbonPanel4.Location = new System.Drawing.Point(0, 0);
            this.ribbonPanel4.Name = "ribbonPanel4";
            this.ribbonPanel4.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.ribbonPanel4.Size = new System.Drawing.Size(1000, 598);
            this.ribbonPanel4.TabIndex = 4;
            this.ribbonPanel4.Visible = false;
            // 
            // pictureBox_rr_DelSelect
            // 
            this.pictureBox_rr_DelSelect.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_rr_DelSelect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_rr_DelSelect.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_rr_DelSelect.Image")));
            this.pictureBox_rr_DelSelect.Location = new System.Drawing.Point(357, 20);
            this.pictureBox_rr_DelSelect.Name = "pictureBox_rr_DelSelect";
            this.pictureBox_rr_DelSelect.Size = new System.Drawing.Size(30, 30);
            this.pictureBox_rr_DelSelect.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_rr_DelSelect.TabIndex = 30;
            this.pictureBox_rr_DelSelect.TabStop = false;
            this.toolTip_clickInfo.SetToolTip(this.pictureBox_rr_DelSelect, "移除选定主机");
            this.pictureBox_rr_DelSelect.Click += new System.EventHandler(this.pictureBox_RR_Window_Click);
            this.pictureBox_rr_DelSelect.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            this.pictureBox_rr_DelSelect.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            // 
            // pictureBox_rr_RefreshSelect
            // 
            this.pictureBox_rr_RefreshSelect.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_rr_RefreshSelect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_rr_RefreshSelect.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_rr_RefreshSelect.Image")));
            this.pictureBox_rr_RefreshSelect.Location = new System.Drawing.Point(170, 20);
            this.pictureBox_rr_RefreshSelect.Name = "pictureBox_rr_RefreshSelect";
            this.pictureBox_rr_RefreshSelect.Size = new System.Drawing.Size(30, 30);
            this.pictureBox_rr_RefreshSelect.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_rr_RefreshSelect.TabIndex = 29;
            this.pictureBox_rr_RefreshSelect.TabStop = false;
            this.toolTip_clickInfo.SetToolTip(this.pictureBox_rr_RefreshSelect, "刷新所有主机");
            this.pictureBox_rr_RefreshSelect.Click += new System.EventHandler(this.pictureBox_RR_Window_Click);
            this.pictureBox_rr_RefreshSelect.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            this.pictureBox_rr_RefreshSelect.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            // 
            // pictureBox_rr_PuaseSelect
            // 
            this.pictureBox_rr_PuaseSelect.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_rr_PuaseSelect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_rr_PuaseSelect.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_rr_PuaseSelect.Image")));
            this.pictureBox_rr_PuaseSelect.Location = new System.Drawing.Point(134, 20);
            this.pictureBox_rr_PuaseSelect.Name = "pictureBox_rr_PuaseSelect";
            this.pictureBox_rr_PuaseSelect.Size = new System.Drawing.Size(30, 30);
            this.pictureBox_rr_PuaseSelect.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_rr_PuaseSelect.TabIndex = 28;
            this.pictureBox_rr_PuaseSelect.TabStop = false;
            this.toolTip_clickInfo.SetToolTip(this.pictureBox_rr_PuaseSelect, "删除所有勾选用户");
            this.pictureBox_rr_PuaseSelect.Click += new System.EventHandler(this.pictureBox_RR_Window_Click);
            this.pictureBox_rr_PuaseSelect.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            this.pictureBox_rr_PuaseSelect.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            // 
            // pictureBox_rr_StopSelect
            // 
            this.pictureBox_rr_StopSelect.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_rr_StopSelect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_rr_StopSelect.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_rr_StopSelect.Image")));
            this.pictureBox_rr_StopSelect.Location = new System.Drawing.Point(98, 20);
            this.pictureBox_rr_StopSelect.Name = "pictureBox_rr_StopSelect";
            this.pictureBox_rr_StopSelect.Size = new System.Drawing.Size(30, 30);
            this.pictureBox_rr_StopSelect.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_rr_StopSelect.TabIndex = 27;
            this.pictureBox_rr_StopSelect.TabStop = false;
            this.toolTip_clickInfo.SetToolTip(this.pictureBox_rr_StopSelect, "停止所有勾选用户");
            this.pictureBox_rr_StopSelect.Click += new System.EventHandler(this.pictureBox_RR_Window_Click);
            this.pictureBox_rr_StopSelect.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            this.pictureBox_rr_StopSelect.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            // 
            // pictureBox_rr_RunSelect
            // 
            this.pictureBox_rr_RunSelect.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_rr_RunSelect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_rr_RunSelect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_rr_RunSelect.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_rr_RunSelect.Image")));
            this.pictureBox_rr_RunSelect.Location = new System.Drawing.Point(62, 20);
            this.pictureBox_rr_RunSelect.Name = "pictureBox_rr_RunSelect";
            this.pictureBox_rr_RunSelect.Size = new System.Drawing.Size(30, 30);
            this.pictureBox_rr_RunSelect.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_rr_RunSelect.TabIndex = 26;
            this.pictureBox_rr_RunSelect.TabStop = false;
            this.toolTip_clickInfo.SetToolTip(this.pictureBox_rr_RunSelect, "启动所有勾选用户");
            this.pictureBox_rr_RunSelect.Click += new System.EventHandler(this.pictureBox_RR_Window_Click);
            this.pictureBox_rr_RunSelect.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            this.pictureBox_rr_RunSelect.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            // 
            // pictureBox_ConnectHost
            // 
            this.pictureBox_ConnectHost.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_ConnectHost.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_ConnectHost.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_ConnectHost.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_ConnectHost.Image")));
            this.pictureBox_ConnectHost.Location = new System.Drawing.Point(3, 0);
            this.pictureBox_ConnectHost.Name = "pictureBox_ConnectHost";
            this.pictureBox_ConnectHost.Size = new System.Drawing.Size(50, 50);
            this.pictureBox_ConnectHost.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_ConnectHost.TabIndex = 6;
            this.pictureBox_ConnectHost.TabStop = false;
            this.toolTip_clickInfo.SetToolTip(this.pictureBox_ConnectHost, "连接远程主机");
            this.pictureBox_ConnectHost.Click += new System.EventHandler(this.pictureBox_RR_Window_Click);
            this.pictureBox_ConnectHost.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            this.pictureBox_ConnectHost.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            // 
            // advTree_remoteTree
            // 
            this.advTree_remoteTree.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.advTree_remoteTree.AllowDrop = true;
            this.advTree_remoteTree.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.advTree_remoteTree.BackgroundStyle.Class = "TreeBorderKey";
            this.advTree_remoteTree.Columns.Add(this.columnHeader_AdvTree_name);
            this.advTree_remoteTree.Columns.Add(this.columnHeader_AdvTree_nowCell);
            this.advTree_remoteTree.Columns.Add(this.columnHeader_AdvTree_result);
            this.advTree_remoteTree.Columns.Add(this.columnHeader_AdvTree_time);
            this.advTree_remoteTree.Columns.Add(this.columnHeader_AdvTree_state);
            this.advTree_remoteTree.DoubleClickTogglesNode = false;
            this.advTree_remoteTree.DragDropEnabled = false;
            this.advTree_remoteTree.ImageList = this.imageListForRemoteRunner;
            this.advTree_remoteTree.Location = new System.Drawing.Point(4, 52);
            this.advTree_remoteTree.Name = "advTree_remoteTree";
            this.advTree_remoteTree.NodesConnector = this.nodeConnector1;
            this.advTree_remoteTree.NodeStyle = this.elementStyle1;
            this.advTree_remoteTree.PathSeparator = ";";
            this.advTree_remoteTree.Size = new System.Drawing.Size(385, 488);
            this.advTree_remoteTree.Styles.Add(this.elementStyle1);
            this.advTree_remoteTree.TabIndex = 2;
            this.advTree_remoteTree.Text = "advTree1";
            this.advTree_remoteTree.AfterCheck += new DevComponents.AdvTree.AdvTreeCellEventHandler(this.advTree_remoteTree_AfterCheck);
            this.advTree_remoteTree.DoubleClick += new System.EventHandler(this.advTree_remoteTree_DoubleClick);
            // 
            // columnHeader_AdvTree_name
            // 
            this.columnHeader_AdvTree_name.Name = "columnHeader_AdvTree_name";
            this.columnHeader_AdvTree_name.Text = "Name";
            this.columnHeader_AdvTree_name.Width.Absolute = 150;
            // 
            // columnHeader_AdvTree_nowCell
            // 
            this.columnHeader_AdvTree_nowCell.Name = "columnHeader_AdvTree_nowCell";
            this.columnHeader_AdvTree_nowCell.Text = "NowCell";
            this.columnHeader_AdvTree_nowCell.Width.Absolute = 80;
            // 
            // columnHeader_AdvTree_result
            // 
            this.columnHeader_AdvTree_result.Name = "columnHeader_AdvTree_result";
            this.columnHeader_AdvTree_result.Text = "Result";
            this.columnHeader_AdvTree_result.Width.Absolute = 50;
            // 
            // columnHeader_AdvTree_time
            // 
            this.columnHeader_AdvTree_time.Name = "columnHeader_AdvTree_time";
            this.columnHeader_AdvTree_time.Text = "Time";
            this.columnHeader_AdvTree_time.Width.Absolute = 40;
            // 
            // columnHeader_AdvTree_state
            // 
            this.columnHeader_AdvTree_state.Name = "columnHeader_AdvTree_state";
            this.columnHeader_AdvTree_state.Text = "State";
            this.columnHeader_AdvTree_state.Width.Absolute = 150;
            // 
            // imageListForRemoteRunner
            // 
            this.imageListForRemoteRunner.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListForRemoteRunner.ImageStream")));
            this.imageListForRemoteRunner.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListForRemoteRunner.Images.SetKeyName(0, "1111110.png");
            this.imageListForRemoteRunner.Images.SetKeyName(1, "1111112.png");
            this.imageListForRemoteRunner.Images.SetKeyName(2, "1175546.png");
            this.imageListForRemoteRunner.Images.SetKeyName(3, "1175746.png");
            this.imageListForRemoteRunner.Images.SetKeyName(4, "1186209.png");
            this.imageListForRemoteRunner.Images.SetKeyName(5, "2015062101221885_easyicon_net_128.png");
            this.imageListForRemoteRunner.Images.SetKeyName(6, "pause.png");
            this.imageListForRemoteRunner.Images.SetKeyName(7, "1878.png");
            this.imageListForRemoteRunner.Images.SetKeyName(8, "2015070304121672223_easyicon_net_128.png");
            // 
            // nodeConnector1
            // 
            this.nodeConnector1.LineColor = System.Drawing.SystemColors.ControlText;
            // 
            // elementStyle1
            // 
            this.elementStyle1.Name = "elementStyle1";
            this.elementStyle1.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // panel_RemoteRunner
            // 
            this.panel_RemoteRunner.Location = new System.Drawing.Point(393, 0);
            this.panel_RemoteRunner.Name = "panel_RemoteRunner";
            this.panel_RemoteRunner.Size = new System.Drawing.Size(603, 540);
            this.panel_RemoteRunner.TabIndex = 1;
            // 
            // ribbonTabItem1
            // 
            this.ribbonTabItem1.Checked = true;
            this.ribbonTabItem1.Name = "ribbonTabItem1";
            this.ribbonTabItem1.Panel = this.ribbonPanel1;
            this.ribbonTabItem1.Text = "vanelife_interface";
            // 
            // ribbonTabItem2
            // 
            this.ribbonTabItem2.Name = "ribbonTabItem2";
            this.ribbonTabItem2.Panel = this.ribbonPanel2;
            this.ribbonTabItem2.Text = "vanelife_config";
            // 
            // ribbonTabItem3
            // 
            this.ribbonTabItem3.Name = "ribbonTabItem3";
            this.ribbonTabItem3.Panel = this.ribbonPanel3;
            this.ribbonTabItem3.Text = "CaseRunner";
            // 
            // ribbonTabItem4
            // 
            this.ribbonTabItem4.Name = "ribbonTabItem4";
            this.ribbonTabItem4.Panel = this.ribbonPanel4;
            this.ribbonTabItem4.Text = "RemoteRunner";
            // 
            // office2007StartButton1
            // 
            this.office2007StartButton1.AutoExpandOnClick = true;
            this.office2007StartButton1.CanCustomize = false;
            this.office2007StartButton1.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.Image;
            this.office2007StartButton1.Icon = ((System.Drawing.Icon)(resources.GetObject("office2007StartButton1.Icon")));
            this.office2007StartButton1.ImagePaddingHorizontal = 2;
            this.office2007StartButton1.ImagePaddingVertical = 2;
            this.office2007StartButton1.Name = "office2007StartButton1";
            this.office2007StartButton1.ShowSubItems = false;
            this.office2007StartButton1.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainer1});
            this.office2007StartButton1.Text = "&File";
            // 
            // itemContainer1
            // 
            // 
            // 
            // 
            this.itemContainer1.BackgroundStyle.Class = "RibbonFileMenuContainer";
            this.itemContainer1.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.itemContainer1.Name = "itemContainer1";
            this.itemContainer1.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainer2,
            this.itemContainer4});
            // 
            // itemContainer2
            // 
            // 
            // 
            // 
            this.itemContainer2.BackgroundStyle.Class = "RibbonFileMenuTwoColumnContainer";
            this.itemContainer2.ItemSpacing = 0;
            this.itemContainer2.Name = "itemContainer2";
            this.itemContainer2.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.galleryContainer1});
            // 
            // galleryContainer1
            // 
            // 
            // 
            // 
            this.galleryContainer1.BackgroundStyle.Class = "RibbonFileMenuColumnTwoContainer";
            this.galleryContainer1.EnableGalleryPopup = false;
            this.galleryContainer1.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.galleryContainer1.MinimumSize = new System.Drawing.Size(180, 240);
            this.galleryContainer1.MultiLine = false;
            this.galleryContainer1.Name = "galleryContainer1";
            this.galleryContainer1.PopupUsesStandardScrollbars = false;
            this.galleryContainer1.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelItem8,
            this.btitem_openError,
            this.btitem_openCase,
            this.btitem_openTip,
            this.btitem_changeTip,
            this.btitem_openExport,
            this.checkBoxItem_run,
            this.checkBoxItem_edit,
            this.checkBoxItem_dataAdd,
            this.btitem_help,
            this.btitem_about});
            // 
            // labelItem8
            // 
            this.labelItem8.BorderSide = DevComponents.DotNetBar.eBorderSide.Bottom;
            this.labelItem8.BorderType = DevComponents.DotNetBar.eBorderType.Raised;
            this.labelItem8.CanCustomize = false;
            this.labelItem8.Name = "labelItem8";
            this.labelItem8.PaddingBottom = 2;
            this.labelItem8.PaddingTop = 2;
            this.labelItem8.Stretch = true;
            this.labelItem8.Text = "<font color=\"#1F497D\"><b><i>Vanelife Interface</i></b></font>";
            // 
            // btitem_openError
            // 
            this.btitem_openError.Name = "btitem_openError";
            this.btitem_openError.Text = "&1.错误日志";
            this.btitem_openError.Click += new System.EventHandler(this.btitem_openError_Click);
            // 
            // btitem_openCase
            // 
            this.btitem_openCase.Name = "btitem_openCase";
            this.btitem_openCase.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainer_case});
            this.btitem_openCase.Text = "&2.编辑用例文件";
            // 
            // itemContainer_case
            // 
            this.itemContainer_case.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.itemContainer_case.Name = "itemContainer_case";
            // 
            // btitem_openTip
            // 
            this.btitem_openTip.Name = "btitem_openTip";
            this.btitem_openTip.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainer_Tip});
            this.btitem_openTip.Text = "&3.编辑智能提示";
            // 
            // itemContainer_Tip
            // 
            this.itemContainer_Tip.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.itemContainer_Tip.Name = "itemContainer_Tip";
            // 
            // btitem_changeTip
            // 
            this.btitem_changeTip.Name = "btitem_changeTip";
            this.btitem_changeTip.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.itemContainer_changeTip});
            this.btitem_changeTip.Text = "&4.更换智能提示";
            // 
            // itemContainer_changeTip
            // 
            this.itemContainer_changeTip.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.itemContainer_changeTip.Name = "itemContainer_changeTip";
            // 
            // btitem_openExport
            // 
            this.btitem_openExport.Name = "btitem_openExport";
            this.btitem_openExport.Text = "&5.测试报告";
            this.btitem_openExport.Click += new System.EventHandler(this.btitem_openExport_Click);
            // 
            // checkBoxItem_run
            // 
            this.checkBoxItem_run.Checked = true;
            this.checkBoxItem_run.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxItem_run.Name = "checkBoxItem_run";
            this.checkBoxItem_run.Text = "在工作区显示运行组件";
            this.checkBoxItem_run.CheckedChanged += new DevComponents.DotNetBar.CheckBoxChangeEventHandler(this.checkBoxItem_run_CheckedChanged);
            // 
            // checkBoxItem_edit
            // 
            this.checkBoxItem_edit.Checked = true;
            this.checkBoxItem_edit.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxItem_edit.Name = "checkBoxItem_edit";
            this.checkBoxItem_edit.Text = "在工作区显示编辑组件";
            this.checkBoxItem_edit.CheckedChanged += new DevComponents.DotNetBar.CheckBoxChangeEventHandler(this.checkBoxItem_edit_CheckedChanged);
            // 
            // checkBoxItem_dataAdd
            // 
            this.checkBoxItem_dataAdd.Checked = true;
            this.checkBoxItem_dataAdd.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxItem_dataAdd.Name = "checkBoxItem_dataAdd";
            this.checkBoxItem_dataAdd.Text = "在工作区显示信息反馈";
            this.checkBoxItem_dataAdd.CheckedChanged += new DevComponents.DotNetBar.CheckBoxChangeEventHandler(this.checkBoxItem_dataAdd_CheckedChanged);
            // 
            // btitem_help
            // 
            this.btitem_help.Name = "btitem_help";
            this.btitem_help.Text = "&6.使用手册";
            this.btitem_help.Click += new System.EventHandler(this.btitem_help_Click);
            // 
            // btitem_about
            // 
            this.btitem_about.Name = "btitem_about";
            this.btitem_about.Text = "&7.关于";
            this.btitem_about.Click += new System.EventHandler(this.btitem_about_Click);
            // 
            // itemContainer4
            // 
            // 
            // 
            // 
            this.itemContainer4.BackgroundStyle.Class = "RibbonFileMenuBottomContainer";
            this.itemContainer4.HorizontalItemAlignment = DevComponents.DotNetBar.eHorizontalItemsAlignment.Right;
            this.itemContainer4.Name = "itemContainer4";
            this.itemContainer4.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem12,
            this.buttonItem13});
            // 
            // buttonItem12
            // 
            this.buttonItem12.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem12.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonItem12.Name = "buttonItem12";
            this.buttonItem12.SubItemsExpandWidth = 24;
            this.buttonItem12.Text = "Opt&ions";
            this.buttonItem12.Click += new System.EventHandler(this.buttonItem12_Click);
            // 
            // buttonItem13
            // 
            this.buttonItem13.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem13.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonItem13.Name = "buttonItem13";
            this.buttonItem13.SubItemsExpandWidth = 24;
            this.buttonItem13.Text = "E&xit";
            // 
            // qatCustomizeItem1
            // 
            this.qatCustomizeItem1.Name = "qatCustomizeItem1";
            // 
            // timer_show
            // 
            this.timer_show.Interval = 1000;
            this.timer_show.Tick += new System.EventHandler(this.timer_show_Tick);
            // 
            // openFileDialog_caseFile
            // 
            this.openFileDialog_caseFile.Filter = "测试用例|*.xml";
            // 
            // checkBoxItem1
            // 
            this.checkBoxItem1.Checked = true;
            this.checkBoxItem1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxItem1.Name = "checkBoxItem1";
            this.checkBoxItem1.Text = "在工作区显示运行组件";
            // 
            // checkBoxItem2
            // 
            this.checkBoxItem2.Checked = true;
            this.checkBoxItem2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxItem2.Name = "checkBoxItem2";
            this.checkBoxItem2.Text = "在工作区显示运行组件";
            // 
            // imageListForButton
            // 
            this.imageListForButton.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListForButton.ImageStream")));
            this.imageListForButton.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListForButton.Images.SetKeyName(0, "bt_DataBack_off");
            this.imageListForButton.Images.SetKeyName(1, "bt_DataBack_on");
            this.imageListForButton.Images.SetKeyName(2, "bt_heat_0.png");
            this.imageListForButton.Images.SetKeyName(3, "bt_heat_1.png");
            this.imageListForButton.Images.SetKeyName(4, "bt_heat_3.png");
            this.imageListForButton.Images.SetKeyName(5, "bt_heat_2.png");
            this.imageListForButton.Images.SetKeyName(6, "bt_mix.png");
            this.imageListForButton.Images.SetKeyName(7, "bt_max.png");
            // 
            // stopRunForceToolStripMenuItem
            // 
            this.stopRunForceToolStripMenuItem.Image = global::AutoTest.Properties.Resources._2013112005093916_easyicon_net_512;
            this.stopRunForceToolStripMenuItem.Name = "stopRunForceToolStripMenuItem";
            this.stopRunForceToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.stopRunForceToolStripMenuItem.Text = "强制终止";
            this.stopRunForceToolStripMenuItem.Click += new System.EventHandler(this.stopRunForceToolStripMenuItem_Click);
            // 
            // AutoRunner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.ribbonControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AutoRunner";
            this.Text = "TestMan";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AutoTest_FormClosing);
            this.Load += new System.EventHandler(this.AutoTest_Load);
            this.Resize += new System.EventHandler(this.AutoTest_Resize);
            this.ribbonControl1.ResumeLayout(false);
            this.ribbonControl1.PerformLayout();
            this.ribbonPanel1.ResumeLayout(false);
            this.ribbonPanel1.PerformLayout();
            this.expandablePanel_testMode.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_reLoadCase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_RunHereOnly)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_runClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_StopRun)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_RunNext)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_PauseRun)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_RunHere)).EndInit();
            this.expandablePanel_dataAdd.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_dataAddStop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_changeDataAddSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_dataAddSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_dataAddclean)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_dataAddClose)).EndInit();
            this.expandablePanel_messageBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_MessageInfoList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_openInterfaceTest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_exportReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_tryTest1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_reLoadCase1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_set1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_set)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_stopRun2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_RunHere2)).EndInit();
            this.contextMenuStrip_CaseTree.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_caseParameter)).EndInit();
            this.ribbonPanel3.ResumeLayout(false);
            this.ribbonPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_selRunerMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_cr_delSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_cr_StopSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_cr_runSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_cr_addUser)).EndInit();
            this.ribbonPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_gwListMax)).EndInit();
            this.contextMenuStrip_GwList.ResumeLayout(false);
            this.panel_configMain.ResumeLayout(false);
            this.expandablePanel_vaneWifiConfig.ResumeLayout(false);
            this.expandablePanel_vaneWifiConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_startWifiConfig)).EndInit();
            this.ribbonPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_rr_DelSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_rr_RefreshSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_rr_PuaseSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_rr_StopSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_rr_RunSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ConnectHost)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.advTree_remoteTree)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.RibbonControl ribbonControl1;
        private DevComponents.DotNetBar.RibbonPanel ribbonPanel1;
        private DevComponents.DotNetBar.RibbonPanel ribbonPanel2;
        private DevComponents.DotNetBar.RibbonTabItem ribbonTabItem1;
        private DevComponents.DotNetBar.RibbonTabItem ribbonTabItem2;
        private DevComponents.DotNetBar.QatCustomizeItem qatCustomizeItem1;
        private System.Windows.Forms.ImageList imageListForCase;
        private TreeViewExDB tvw_Case;
        private System.Windows.Forms.Button test;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_CaseTree;
        private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runHereOnlyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编辑选定项ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ModifyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopRunToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 组件控制ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_dataAdd;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_runQuick;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_editQuick;
        private System.Windows.Forms.Timer timer_show;
        private DevComponents.DotNetBar.ExpandablePanel expandablePanel_dataAdd;
        private System.Windows.Forms.PictureBox pictureBox_changeDataAddSize;
        private System.Windows.Forms.PictureBox pictureBox_dataAddSave;
        private System.Windows.Forms.PictureBox pictureBox_dataAddclean;
        private System.Windows.Forms.PictureBox pictureBox_dataAddClose;
        private System.Windows.Forms.Label label_moveFlagForDataAdd;
        private System.Windows.Forms.ColumnHeader columnHeader0;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ToolTip toolTip_clickInfo;
        private System.Windows.Forms.Label lb_msg1;
        private System.Windows.Forms.Label lb_msg2;
        private System.Windows.Forms.Button bt_openFile;
        private System.Windows.Forms.TextBox tb_caseFilePath;
        private System.Windows.Forms.OpenFileDialog openFileDialog_caseFile;
        private DevComponents.DotNetBar.ExpandablePanel expandablePanel_testMode;
        private System.Windows.Forms.PictureBox pictureBox_RunHereOnly;
        private System.Windows.Forms.PictureBox pictureBox_runClose;
        private System.Windows.Forms.PictureBox pictureBox_StopRun;
        private System.Windows.Forms.PictureBox pictureBox_RunNext;
        private System.Windows.Forms.PictureBox pictureBox_PauseRun;
        private System.Windows.Forms.PictureBox pictureBox_RunHere;
        private System.Windows.Forms.Label label_moveFlagForRun;
        private DevComponents.DotNetBar.Office2007StartButton office2007StartButton1;
        private DevComponents.DotNetBar.ItemContainer itemContainer1;
        private DevComponents.DotNetBar.ItemContainer itemContainer2;
        private DevComponents.DotNetBar.GalleryContainer galleryContainer1;
        private DevComponents.DotNetBar.LabelItem labelItem8;
        private DevComponents.DotNetBar.ButtonItem btitem_openError;
        private DevComponents.DotNetBar.ButtonItem btitem_openCase;
        private DevComponents.DotNetBar.ItemContainer itemContainer_case;
        private DevComponents.DotNetBar.ButtonItem btitem_openTip;
        private DevComponents.DotNetBar.ItemContainer itemContainer_Tip;
        private DevComponents.DotNetBar.ButtonItem btitem_changeTip;
        private DevComponents.DotNetBar.ItemContainer itemContainer_changeTip;
        private DevComponents.DotNetBar.CheckBoxItem checkBoxItem_run;
        private DevComponents.DotNetBar.CheckBoxItem checkBoxItem_edit;
        private DevComponents.DotNetBar.CheckBoxItem checkBoxItem_dataAdd;
        private DevComponents.DotNetBar.ButtonItem btitem_help;
        private DevComponents.DotNetBar.ItemContainer itemContainer4;
        private DevComponents.DotNetBar.ButtonItem buttonItem12;
        private DevComponents.DotNetBar.ButtonItem buttonItem13;
        //private System.Windows.Forms.RichTextBox trb_addRecord;
        private DataRecordBox trb_addRecord;
        private System.Windows.Forms.PictureBox pictureBox_stopRun2;
        private System.Windows.Forms.PictureBox pictureBox_RunHere2;
        private DevComponents.DotNetBar.CheckBoxItem checkBoxItem1;
        private DevComponents.DotNetBar.CheckBoxItem checkBoxItem2;
        private System.Windows.Forms.PictureBox pictureBox_set;
        private System.Windows.Forms.PictureBox pictureBox_set1;
        private System.Windows.Forms.CheckBox checkBox_run;
        private System.Windows.Forms.CheckBox checkBox_dataBack;
        private System.Windows.Forms.TextBox tb_tryTestData;
        private System.Windows.Forms.PictureBox pictureBox_reLoadCase1;
        private System.Windows.Forms.PictureBox pictureBox_tryTest1;
        private System.Windows.Forms.PictureBox pictureBox_exportReport;
        private DevComponents.DotNetBar.ButtonItem btitem_openExport;
        private System.Windows.Forms.PictureBox pictureBox_openInterfaceTest;
        private DevComponents.DotNetBar.ExpandablePanel expandablePanel_messageBox;
        private System.Windows.Forms.RichTextBox richTextBox_showMessage;
        private System.Windows.Forms.PictureBox pictureBox_reLoadCase;
        private System.Windows.Forms.ToolStripMenuItem RunHereToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem caseParameterToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox_caseParameter;
        private DevComponents.DotNetBar.ButtonItem btitem_about;
        private System.Windows.Forms.Panel panel_configMain;
        //private global::AutoTest.myControl.ListViewEx listViewEx_GWlist;
        private System.Windows.Forms.ListView listViewEx_GWlist;
        private System.Windows.Forms.ColumnHeader columnHeader_vc_id;
        private System.Windows.Forms.ColumnHeader columnHeader_vc_ip;
        private System.Windows.Forms.ColumnHeader columnHeader_vc_sn;
        private System.Windows.Forms.ColumnHeader columnHeader_vc_Alias;
        private System.Windows.Forms.ColumnHeader columnHeader1_vc_Version;
        private System.Windows.Forms.ColumnHeader columnHeader_vc_Ability;
        private System.Windows.Forms.Label lb_msg5;
        private System.Windows.Forms.Label lb_msg4;
        private System.Windows.Forms.Label lb_msg3;
        private System.Windows.Forms.RichTextBox richTextBox_BroadcastRecord;
        private System.Windows.Forms.PictureBox pictureBox_dataAddStop;
        public System.Windows.Forms.ImageList imageListForButton;
        private System.Windows.Forms.PictureBox pictureBox_MessageInfoList;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_GwList;
        private DevComponents.DotNetBar.ExpandablePanel expandablePanel_vaneWifiConfig;
        private DevComponents.DotNetBar.Controls.ProgressBarX progressBarX_WifiConfig;
        private System.Windows.Forms.ListView listView_WifiConfigDataBack;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.TextBox tb_wifiCfg_Key;
        private System.Windows.Forms.TextBox tb_wifiCfg_SSID;
        private System.Windows.Forms.ComboBox cb_wifiCfg_Mode;
        private System.Windows.Forms.Label lb_WifiConfig_info3;
        private System.Windows.Forms.Label lb_WifiConfig_info2;
        private System.Windows.Forms.Label lb_WifiConfig_info1;
        private System.Windows.Forms.PictureBox pictureBox_startWifiConfig;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem PauseRunToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RunNextToolStripMenuItem;
        private ListViewExDB listView_DataAdd;
        private ProgressBarList progressBar_case;
        private DevComponents.DotNetBar.RibbonPanel ribbonPanel3;
        private DevComponents.DotNetBar.RibbonTabItem ribbonTabItem3;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader15;
        private System.Windows.Forms.ColumnHeader columnHeader17;
        private System.Windows.Forms.ColumnHeader columnHeader16;
        private ListView_RunnerView listView_CaseRunner;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.CheckBox cb_cr_SelectAll;
        private System.Windows.Forms.PictureBox pictureBox_cr_addUser;
        private System.Windows.Forms.CheckBox cb_cr_isCb;
        private System.Windows.Forms.PictureBox pictureBox_cr_delSelect;
        private System.Windows.Forms.PictureBox pictureBox_cr_StopSelect;
        private System.Windows.Forms.PictureBox pictureBox_cr_runSelect;
        private ListViewExDB listView_SelectRunner;
        private System.Windows.Forms.ColumnHeader columnHeader18;
        private System.Windows.Forms.ColumnHeader columnHeader19;
        private System.Windows.Forms.ColumnHeader columnHeader20;
        private System.Windows.Forms.ColumnHeader columnHeader21;
        private System.Windows.Forms.ColumnHeader columnHeader22;
        private System.Windows.Forms.ColumnHeader columnHeader23;
        private System.Windows.Forms.ColumnHeader columnHeader24;
        private System.Windows.Forms.LinkLabel llb_showRunner;
        private System.Windows.Forms.Label lb_cr_info3;
        private System.Windows.Forms.Label lb_cr_info2;
        private System.Windows.Forms.Label lb_cr_info1;
        private DevComponents.DotNetBar.RibbonPanel ribbonPanel4;
        private DevComponents.DotNetBar.RibbonTabItem ribbonTabItem4;
        private System.Windows.Forms.Panel panel_RemoteRunner;
        private DevComponents.AdvTree.AdvTree advTree_remoteTree;
        private DevComponents.AdvTree.NodeConnector nodeConnector1;
        private DevComponents.DotNetBar.ElementStyle elementStyle1;
        private System.Windows.Forms.PictureBox pictureBox_ConnectHost;
        private DevComponents.AdvTree.ColumnHeader columnHeader_AdvTree_name;
        private DevComponents.AdvTree.ColumnHeader columnHeader_AdvTree_nowCell;
        private DevComponents.AdvTree.ColumnHeader columnHeader_AdvTree_result;
        private DevComponents.AdvTree.ColumnHeader columnHeader_AdvTree_time;
        private DevComponents.AdvTree.ColumnHeader columnHeader_AdvTree_state;
        private System.Windows.Forms.ImageList imageListForRemoteRunner;
        private System.Windows.Forms.PictureBox pictureBox_rr_PuaseSelect;
        private System.Windows.Forms.PictureBox pictureBox_rr_StopSelect;
        private System.Windows.Forms.PictureBox pictureBox_rr_RunSelect;
        private System.Windows.Forms.PictureBox pictureBox_rr_RefreshSelect;
        private System.Windows.Forms.PictureBox pictureBox_rr_DelSelect;
        private System.Windows.Forms.PictureBox pictureBox_gwListMax;
        private System.Windows.Forms.PictureBox pictureBox_selRunerMax;
        private System.Windows.Forms.ToolStripMenuItem stopRunForceToolStripMenuItem;

    }
}

