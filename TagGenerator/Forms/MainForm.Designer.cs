namespace TagGenerator
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.dbConnection1 = new RapidInterface.DBConnection();
            this._dbForm1 = new RapidInterface.DBForm();
            this.barLayoutItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.baseNavBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarGroup3 = new DevExpress.XtraNavBar.NavBarGroup();
            this.ResultViewNavBarItem1 = new DevExpress.XtraNavBar.NavBarItem();
            this.baseNavBarGroup1 = new DevExpress.XtraNavBar.NavBarGroup();
            this.NoteViewNavBarItem1 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarGroup1 = new DevExpress.XtraNavBar.NavBarGroup();
            this.NoteTypeViewNavBarItem1 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarGroup2 = new DevExpress.XtraNavBar.NavBarGroup();
            this.icons1 = new DevExpress.Utils.ImageCollection(this.components);
            this.baseControlContainer1 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.baseDockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.baseDockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.baseDocumentManager1 = new DevExpress.XtraBars.Docking2010.DocumentManager(this.components);
            this.baseTabbedView1 = new DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView(this.components);
            this.baseLayoutControl1 = new RapidInterface.LayoutControlEx();
            this.filterClearButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.filterTextEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.baseLayoutGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.filterLayoutItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.filterClearLayoutItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.NoteTypeView1 = new RapidInterface.DBFormItemView();
            this.NoteView1 = new RapidInterface.DBFormItemView();
            this.ResultView1 = new RapidInterface.DBFormItemView();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dbConnection1)).BeginInit();
            this._dbForm1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barLayoutItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.baseNavBarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icons1)).BeginInit();
            this.baseControlContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.baseDockManager1)).BeginInit();
            this.baseDockPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.baseDocumentManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.baseTabbedView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.baseLayoutControl1)).BeginInit();
            this.baseLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.filterTextEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.baseLayoutGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.filterLayoutItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.filterClearLayoutItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // dbConnection1
            // 
            this.dbConnection1.AutoCreateOption = DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema;
            this.dbConnection1.CustomInit = true;
            this.dbConnection1.DataBase = "";
            this.dbConnection1.DataBaseType = RapidInterface.DBConnection.SQLType.Access;
            this.dbConnection1.OwnerForm = this;
            this.dbConnection1.OwnerFormInited = false;
            this.dbConnection1.Password = "";
            this.dbConnection1.Server = null;
            this.dbConnection1.User = null;
            // 
            // _dbForm1
            // 
            this._dbForm1.BarLayoutItem = this.barLayoutItem1;
            this._dbForm1.BaseControlContainer = this.baseControlContainer1;
            this._dbForm1.BaseDockManager = this.baseDockManager1;
            this._dbForm1.BaseDockPanel = this.baseDockPanel1;
            this._dbForm1.BaseDocumentManager = this.baseDocumentManager1;
            this._dbForm1.BaseLayoutControl = this.baseLayoutControl1;
            this._dbForm1.BaseLayoutGroup = this.baseLayoutGroup1;
            this._dbForm1.BaseNavBarControl = this.baseNavBarControl1;
            this._dbForm1.BaseNavBarGroup = this.baseNavBarGroup1;
            this._dbForm1.BaseTabbedView = this.baseTabbedView1;
            this._dbForm1.Controls.Add(this.baseLayoutControl1);
            this._dbForm1.CountOpenDesigner = 69;
            this._dbForm1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dbForm1.FilterClearButton = this.filterClearButton1;
            this._dbForm1.FilterClearLayoutItem = this.filterClearLayoutItem1;
            this._dbForm1.FilterLayoutItem = this.filterLayoutItem1;
            this._dbForm1.FilterTextEdit = this.filterTextEdit1;
            this._dbForm1.Icons = this.icons1;
            this._dbForm1.ImagePath = "d:\\Мои документы\\!Projects\\TagGenerator\\TagGenerator\\Image\\";
            this._dbForm1.Items.Add(this.NoteTypeView1);
            this._dbForm1.Items.Add(this.NoteView1);
            this._dbForm1.Items.Add(this.ResultView1);
            this._dbForm1.Location = new System.Drawing.Point(0, 0);
            this._dbForm1.Name = "_dbForm1";
            this._dbForm1.NotifyIcon = this.notifyIcon1;
            this._dbForm1.OwnerForm = this;
            this._dbForm1.Size = new System.Drawing.Size(192, 679);
            this._dbForm1.TabIndex = 0;
            // 
            // barLayoutItem1
            // 
            this.barLayoutItem1.Control = this.baseNavBarControl1;
            this.barLayoutItem1.CustomizationFormText = "Навигация";
            this.barLayoutItem1.Location = new System.Drawing.Point(0, 26);
            this.barLayoutItem1.Name = "barLayoutItem1";
            this.barLayoutItem1.Size = new System.Drawing.Size(192, 653);
            this.barLayoutItem1.TextSize = new System.Drawing.Size(0, 0);
            this.barLayoutItem1.TextVisible = false;
            // 
            // baseNavBarControl1
            // 
            this.baseNavBarControl1.ActiveGroup = this.navBarGroup3;
            this.baseNavBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.baseNavBarGroup1,
            this.navBarGroup1,
            this.navBarGroup2,
            this.navBarGroup3});
            this.baseNavBarControl1.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
            this.NoteTypeViewNavBarItem1,
            this.NoteViewNavBarItem1,
            this.ResultViewNavBarItem1});
            this.baseNavBarControl1.LinkSelectionMode = DevExpress.XtraNavBar.LinkSelectionModeType.OneInControl;
            this.baseNavBarControl1.Location = new System.Drawing.Point(2, 28);
            this.baseNavBarControl1.Name = "baseNavBarControl1";
            this.baseNavBarControl1.OptionsNavPane.ExpandedWidth = 140;
            this.baseNavBarControl1.Size = new System.Drawing.Size(188, 649);
            this.baseNavBarControl1.SmallImages = this.icons1;
            this.baseNavBarControl1.TabIndex = 5;
            // 
            // navBarGroup3
            // 
            this.navBarGroup3.Caption = "Вывод";
            this.navBarGroup3.Expanded = true;
            this.navBarGroup3.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.ResultViewNavBarItem1)});
            this.navBarGroup3.Name = "navBarGroup3";
            this.navBarGroup3.SmallImage = ((System.Drawing.Image)(resources.GetObject("navBarGroup3.SmallImage")));
            // 
            // ResultViewNavBarItem1
            // 
            this.ResultViewNavBarItem1.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.ResultViewNavBarItem1.AppearancePressed.ForeColor = System.Drawing.Color.Red;
            this.ResultViewNavBarItem1.AppearancePressed.Options.UseFont = true;
            this.ResultViewNavBarItem1.AppearancePressed.Options.UseForeColor = true;
            this.ResultViewNavBarItem1.Caption = "Результат";
            this.ResultViewNavBarItem1.Name = "ResultViewNavBarItem1";
            this.ResultViewNavBarItem1.SmallImageIndex = 2;
            // 
            // baseNavBarGroup1
            // 
            this.baseNavBarGroup1.Caption = "Формы";
            this.baseNavBarGroup1.Expanded = true;
            this.baseNavBarGroup1.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.NoteViewNavBarItem1)});
            this.baseNavBarGroup1.Name = "baseNavBarGroup1";
            this.baseNavBarGroup1.SmallImage = ((System.Drawing.Image)(resources.GetObject("baseNavBarGroup1.SmallImage")));
            // 
            // NoteViewNavBarItem1
            // 
            this.NoteViewNavBarItem1.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.NoteViewNavBarItem1.AppearancePressed.ForeColor = System.Drawing.Color.Red;
            this.NoteViewNavBarItem1.AppearancePressed.Options.UseFont = true;
            this.NoteViewNavBarItem1.AppearancePressed.Options.UseForeColor = true;
            this.NoteViewNavBarItem1.Caption = "Объект";
            this.NoteViewNavBarItem1.Name = "NoteViewNavBarItem1";
            this.NoteViewNavBarItem1.SmallImageIndex = 1;
            // 
            // navBarGroup1
            // 
            this.navBarGroup1.Caption = "Справочники";
            this.navBarGroup1.Expanded = true;
            this.navBarGroup1.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.NoteTypeViewNavBarItem1)});
            this.navBarGroup1.Name = "navBarGroup1";
            this.navBarGroup1.SmallImage = ((System.Drawing.Image)(resources.GetObject("navBarGroup1.SmallImage")));
            // 
            // NoteTypeViewNavBarItem1
            // 
            this.NoteTypeViewNavBarItem1.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.NoteTypeViewNavBarItem1.AppearancePressed.ForeColor = System.Drawing.Color.Red;
            this.NoteTypeViewNavBarItem1.AppearancePressed.Options.UseFont = true;
            this.NoteTypeViewNavBarItem1.AppearancePressed.Options.UseForeColor = true;
            this.NoteTypeViewNavBarItem1.Caption = "Тип объекта";
            this.NoteTypeViewNavBarItem1.Name = "NoteTypeViewNavBarItem1";
            this.NoteTypeViewNavBarItem1.SmallImageIndex = 0;
            // 
            // navBarGroup2
            // 
            this.navBarGroup2.Caption = "Коллекции";
            this.navBarGroup2.Expanded = true;
            this.navBarGroup2.Name = "navBarGroup2";
            this.navBarGroup2.SmallImage = ((System.Drawing.Image)(resources.GetObject("navBarGroup2.SmallImage")));
            // 
            // icons1
            // 
            this.icons1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("icons1.ImageStream")));
            this.icons1.Images.SetKeyName(0, "ObjectType.png");
            this.icons1.Images.SetKeyName(1, "Object.png");
            this.icons1.Images.SetKeyName(2, "Result.png");
            // 
            // baseControlContainer1
            // 
            this.baseControlContainer1.Controls.Add(this._dbForm1);
            this.baseControlContainer1.Location = new System.Drawing.Point(4, 23);
            this.baseControlContainer1.Name = "baseControlContainer1";
            this.baseControlContainer1.Size = new System.Drawing.Size(192, 679);
            this.baseControlContainer1.TabIndex = 0;
            // 
            // baseDockManager1
            // 
            this.baseDockManager1.Form = this;
            this.baseDockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.baseDockPanel1});
            this.baseDockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // baseDockPanel1
            // 
            this.baseDockPanel1.Controls.Add(this.baseControlContainer1);
            this.baseDockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.baseDockPanel1.ID = new System.Guid("2edc5f72-f965-4593-bccd-c795b82e4a6d");
            this.baseDockPanel1.Location = new System.Drawing.Point(0, 0);
            this.baseDockPanel1.Name = "baseDockPanel1";
            this.baseDockPanel1.OriginalSize = new System.Drawing.Size(200, 200);
            this.baseDockPanel1.Size = new System.Drawing.Size(200, 706);
            this.baseDockPanel1.Text = "Навигация";
            // 
            // baseDocumentManager1
            // 
            this.baseDocumentManager1.Images = this.icons1;
            this.baseDocumentManager1.MdiParent = this;
            this.baseDocumentManager1.View = this.baseTabbedView1;
            this.baseDocumentManager1.ViewCollection.AddRange(new DevExpress.XtraBars.Docking2010.Views.BaseView[] {
            this.baseTabbedView1});
            // 
            // baseLayoutControl1
            // 
            this.baseLayoutControl1.Controls.Add(this.filterClearButton1);
            this.baseLayoutControl1.Controls.Add(this.baseNavBarControl1);
            this.baseLayoutControl1.Controls.Add(this.filterTextEdit1);
            this.baseLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.baseLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.baseLayoutControl1.Name = "baseLayoutControl1";
            this.baseLayoutControl1.Root = this.baseLayoutGroup1;
            this.baseLayoutControl1.Size = new System.Drawing.Size(192, 679);
            this.baseLayoutControl1.TabIndex = 0;
            // 
            // filterClearButton1
            // 
            this.filterClearButton1.Image = ((System.Drawing.Image)(resources.GetObject("filterClearButton1.Image")));
            this.filterClearButton1.Location = new System.Drawing.Point(2, 2);
            this.filterClearButton1.Name = "filterClearButton1";
            this.filterClearButton1.Size = new System.Drawing.Size(26, 22);
            this.filterClearButton1.StyleController = this.baseLayoutControl1;
            this.filterClearButton1.TabIndex = 4;
            // 
            // filterTextEdit1
            // 
            this.filterTextEdit1.Location = new System.Drawing.Point(32, 2);
            this.filterTextEdit1.Name = "filterTextEdit1";
            this.filterTextEdit1.Size = new System.Drawing.Size(158, 20);
            this.filterTextEdit1.StyleController = this.baseLayoutControl1;
            this.filterTextEdit1.TabIndex = 6;
            // 
            // baseLayoutGroup1
            // 
            this.baseLayoutGroup1.CustomizationFormText = "Формы";
            this.baseLayoutGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.baseLayoutGroup1.GroupBordersVisible = false;
            this.baseLayoutGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.filterLayoutItem1,
            this.barLayoutItem1,
            this.filterClearLayoutItem1});
            this.baseLayoutGroup1.Location = new System.Drawing.Point(0, 0);
            this.baseLayoutGroup1.Name = "baseLayoutGroup1";
            this.baseLayoutGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.baseLayoutGroup1.Size = new System.Drawing.Size(192, 679);
            this.baseLayoutGroup1.TextVisible = false;
            // 
            // filterLayoutItem1
            // 
            this.filterLayoutItem1.Control = this.filterTextEdit1;
            this.filterLayoutItem1.CustomizationFormText = "Фильтр";
            this.filterLayoutItem1.Location = new System.Drawing.Point(30, 0);
            this.filterLayoutItem1.Name = "filterLayoutItem1";
            this.filterLayoutItem1.Size = new System.Drawing.Size(162, 26);
            this.filterLayoutItem1.TextSize = new System.Drawing.Size(0, 0);
            this.filterLayoutItem1.TextVisible = false;
            // 
            // filterClearLayoutItem1
            // 
            this.filterClearLayoutItem1.Control = this.filterClearButton1;
            this.filterClearLayoutItem1.CustomizationFormText = "Очистка фильтра";
            this.filterClearLayoutItem1.Location = new System.Drawing.Point(0, 0);
            this.filterClearLayoutItem1.Name = "filterClearLayoutItem1";
            this.filterClearLayoutItem1.Size = new System.Drawing.Size(30, 26);
            this.filterClearLayoutItem1.TextSize = new System.Drawing.Size(0, 0);
            this.filterClearLayoutItem1.TextVisible = false;
            // 
            // NoteTypeView1
            // 
            this.NoteTypeView1.BaseNavBarItem = this.NoteTypeViewNavBarItem1;
            this.NoteTypeView1.Caption = "Тип объекта";
            this.NoteTypeView1.DBForm = this._dbForm1;
            this.NoteTypeView1.ImageIndex = 0;
            this.NoteTypeView1.ImageName = "NoteType.png";
            this.NoteTypeView1.Images = this.icons1;
            this.NoteTypeView1.IsDocumentActivated = false;
            this.NoteTypeView1.TableType = typeof(TagGenerator.ObjectType);
            this.NoteTypeView1.View = null;
            this.NoteTypeView1.ViewType = typeof(TagGenerator.ObjectTypeView);
            // 
            // NoteView1
            // 
            this.NoteView1.BaseNavBarItem = this.NoteViewNavBarItem1;
            this.NoteView1.Caption = "Объект";
            this.NoteView1.DBForm = this._dbForm1;
            this.NoteView1.ImageIndex = 1;
            this.NoteView1.ImageName = "NoteType.png";
            this.NoteView1.Images = this.icons1;
            this.NoteView1.IsDocumentActivated = false;
            this.NoteView1.TableType = typeof(TagGenerator.Object);
            this.NoteView1.View = null;
            this.NoteView1.ViewType = typeof(TagGenerator.ObjectView);
            // 
            // ResultView1
            // 
            this.ResultView1.BaseNavBarItem = this.ResultViewNavBarItem1;
            this.ResultView1.Caption = "Результат";
            this.ResultView1.DBForm = this._dbForm1;
            this.ResultView1.ImageIndex = 2;
            this.ResultView1.ImageName = "Result.png";
            this.ResultView1.Images = this.icons1;
            this.ResultView1.IsDocumentActivated = false;
            this.ResultView1.TableType = null;
            this.ResultView1.View = null;
            this.ResultView1.ViewType = typeof(TagGenerator.ResultView);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Генератор тегов";
            this.notifyIcon1.Visible = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(969, 706);
            this.Controls.Add(this.baseDockPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Генератор тегов";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dbConnection1)).EndInit();
            this._dbForm1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.barLayoutItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.baseNavBarControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icons1)).EndInit();
            this.baseControlContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.baseDockManager1)).EndInit();
            this.baseDockPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.baseDocumentManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.baseTabbedView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.baseLayoutControl1)).EndInit();
            this.baseLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.filterTextEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.baseLayoutGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.filterLayoutItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.filterClearLayoutItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private RapidInterface.DBConnection dbConnection1;
        private DevExpress.XtraBars.Docking.DockPanel baseDockPanel1;
        private DevExpress.XtraBars.Docking.ControlContainer baseControlContainer1;
        private RapidInterface.DBForm _dbForm1;
        private DevExpress.XtraLayout.LayoutControlItem barLayoutItem1;
        private DevExpress.XtraNavBar.NavBarControl baseNavBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup baseNavBarGroup1;
        private DevExpress.Utils.ImageCollection icons1;
        private DevExpress.XtraBars.Docking.DockManager baseDockManager1;
        private DevExpress.XtraBars.Docking2010.DocumentManager baseDocumentManager1;
        private DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView baseTabbedView1;
        private RapidInterface.LayoutControlEx baseLayoutControl1;
        private DevExpress.XtraEditors.SimpleButton filterClearButton1;
        private DevExpress.XtraEditors.TextEdit filterTextEdit1;
        private DevExpress.XtraLayout.LayoutControlGroup baseLayoutGroup1;
        private DevExpress.XtraLayout.LayoutControlItem filterLayoutItem1;
        private DevExpress.XtraLayout.LayoutControlItem filterClearLayoutItem1;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup2;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup3;
        private DevExpress.XtraNavBar.NavBarItem NoteTypeViewNavBarItem1;
        private DevExpress.XtraNavBar.NavBarItem NoteViewNavBarItem1;
        private RapidInterface.DBFormItemView NoteTypeView1;
        private RapidInterface.DBFormItemView NoteView1;
        private DevExpress.XtraNavBar.NavBarItem ResultViewNavBarItem1;
        private RapidInterface.DBFormItemView ResultView1;
    }
}

