namespace Rubezh
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.savePicture = new System.Windows.Forms.PictureBox();
            this.lnkOpenData = new System.Windows.Forms.LinkLabel();
            this.lnkSaveData = new System.Windows.Forms.LinkLabel();
            this.lnkCreateSampleData = new System.Windows.Forms.LinkLabel();
            this.btnClearText = new System.Windows.Forms.Button();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.strFileName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgMain = new System.Windows.Forms.DataGridView();
            this.bindingSourceMain = new System.Windows.Forms.BindingSource(this.components);
            this.dsMain = new System.Data.DataSet();
            this.BuildingData = new System.Data.DataTable();
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataColumn2 = new System.Data.DataColumn();
            this.dataColumn3 = new System.Data.DataColumn();
            this.dataColumn4 = new System.Data.DataColumn();
            this.dataColumn5 = new System.Data.DataColumn();
            this.dataColumn6 = new System.Data.DataColumn();
            this.dataColumn7 = new System.Data.DataColumn();
            this.bindingNavigatorMain = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolTipMain = new System.Windows.Forms.ToolTip(this.components);
            this.openFileDialogSource = new System.Windows.Forms.OpenFileDialog();
            this.timerSave = new System.Windows.Forms.Timer(this.components);
            this.dataColumn8 = new System.Data.DataColumn();
            this.buildingNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.floorNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.heightDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.weightDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Lenght = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deviceCountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnFilterPreview = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnPreviewAll = new System.Windows.Forms.ToolStripMenuItem();
            this.btnPreviewErrorsRoom = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.dataColumn9 = new System.Data.DataColumn();
            this.strInformation = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.savePicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BuildingData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigatorMain)).BeginInit();
            this.bindingNavigatorMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.Panel2.Controls.Add(this.strInformation);
            this.splitContainer1.Size = new System.Drawing.Size(578, 346);
            this.splitContainer1.SplitterDistance = 405;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.BackColor = System.Drawing.Color.LightGray;
            this.splitContainer2.Panel1.Controls.Add(this.savePicture);
            this.splitContainer2.Panel1.Controls.Add(this.lnkOpenData);
            this.splitContainer2.Panel1.Controls.Add(this.lnkSaveData);
            this.splitContainer2.Panel1.Controls.Add(this.lnkCreateSampleData);
            this.splitContainer2.Panel1.Controls.Add(this.btnClearText);
            this.splitContainer2.Panel1.Controls.Add(this.btnSelectFile);
            this.splitContainer2.Panel1.Controls.Add(this.strFileName);
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.dgMain);
            this.splitContainer2.Panel2.Controls.Add(this.bindingNavigatorMain);
            this.splitContainer2.Size = new System.Drawing.Size(405, 346);
            this.splitContainer2.SplitterDistance = 75;
            this.splitContainer2.TabIndex = 0;
            // 
            // savePicture
            // 
            this.savePicture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.savePicture.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("savePicture.BackgroundImage")));
            this.savePicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.savePicture.Location = new System.Drawing.Point(241, 45);
            this.savePicture.Name = "savePicture";
            this.savePicture.Size = new System.Drawing.Size(28, 20);
            this.savePicture.TabIndex = 7;
            this.savePicture.TabStop = false;
            this.savePicture.Visible = false;
            // 
            // lnkOpenData
            // 
            this.lnkOpenData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkOpenData.AutoSize = true;
            this.lnkOpenData.LinkColor = System.Drawing.Color.SaddleBrown;
            this.lnkOpenData.Location = new System.Drawing.Point(339, 48);
            this.lnkOpenData.Name = "lnkOpenData";
            this.lnkOpenData.Size = new System.Drawing.Size(59, 13);
            this.lnkOpenData.TabIndex = 6;
            this.lnkOpenData.TabStop = true;
            this.lnkOpenData.Text = "Загрузить";
            this.toolTipMain.SetToolTip(this.lnkOpenData, "Загрузить и обработать");
            this.lnkOpenData.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkOpenData_LinkClicked);
            // 
            // lnkSaveData
            // 
            this.lnkSaveData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkSaveData.AutoSize = true;
            this.lnkSaveData.LinkColor = System.Drawing.Color.SaddleBrown;
            this.lnkSaveData.Location = new System.Drawing.Point(275, 48);
            this.lnkSaveData.Name = "lnkSaveData";
            this.lnkSaveData.Size = new System.Drawing.Size(60, 13);
            this.lnkSaveData.TabIndex = 5;
            this.lnkSaveData.TabStop = true;
            this.lnkSaveData.Text = "Сохранить";
            this.toolTipMain.SetToolTip(this.lnkSaveData, "Сохранить данные в файл");
            this.lnkSaveData.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSaveData_LinkClicked);
            // 
            // lnkCreateSampleData
            // 
            this.lnkCreateSampleData.AutoSize = true;
            this.lnkCreateSampleData.Location = new System.Drawing.Point(13, 48);
            this.lnkCreateSampleData.Name = "lnkCreateSampleData";
            this.lnkCreateSampleData.Size = new System.Drawing.Size(124, 13);
            this.lnkCreateSampleData.TabIndex = 4;
            this.lnkCreateSampleData.TabStop = true;
            this.lnkCreateSampleData.Text = "Сформировать данные";
            this.toolTipMain.SetToolTip(this.lnkCreateSampleData, "Сформировать тестовые данные");
            this.lnkCreateSampleData.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkCreateSampleData_LinkClicked);
            // 
            // btnClearText
            // 
            this.btnClearText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearText.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClearText.Image = ((System.Drawing.Image)(resources.GetObject("btnClearText.Image")));
            this.btnClearText.Location = new System.Drawing.Point(356, 23);
            this.btnClearText.Name = "btnClearText";
            this.btnClearText.Size = new System.Drawing.Size(21, 20);
            this.btnClearText.TabIndex = 3;
            this.btnClearText.Text = "...";
            this.toolTipMain.SetToolTip(this.btnClearText, "Очистить строку");
            this.btnClearText.UseVisualStyleBackColor = true;
            this.btnClearText.Click += new System.EventHandler(this.btnClearText_Click);
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectFile.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSelectFile.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectFile.Image")));
            this.btnSelectFile.Location = new System.Drawing.Point(378, 23);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(21, 20);
            this.btnSelectFile.TabIndex = 2;
            this.btnSelectFile.Text = "...";
            this.toolTipMain.SetToolTip(this.btnSelectFile, "Открыть файл");
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // strFileName
            // 
            this.strFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.strFileName.Location = new System.Drawing.Point(13, 23);
            this.strFileName.Name = "strFileName";
            this.strFileName.Size = new System.Drawing.Size(341, 20);
            this.strFileName.TabIndex = 1;
            this.toolTipMain.SetToolTip(this.strFileName, "Укажите наименование файла с исходными данные для загрузки и выгрузки данных");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Укажите файл (XML) с даными:";
            // 
            // dgMain
            // 
            this.dgMain.AllowUserToAddRows = false;
            this.dgMain.AllowUserToDeleteRows = false;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.OldLace;
            this.dgMain.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dgMain.AutoGenerateColumns = false;
            this.dgMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.buildingNameDataGridViewTextBoxColumn,
            this.floorNumberDataGridViewTextBoxColumn,
            this.heightDataGridViewTextBoxColumn,
            this.weightDataGridViewTextBoxColumn,
            this.Lenght,
            this.deviceCountDataGridViewTextBoxColumn});
            this.dgMain.DataSource = this.bindingSourceMain;
            this.dgMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgMain.Location = new System.Drawing.Point(0, 25);
            this.dgMain.MultiSelect = false;
            this.dgMain.Name = "dgMain";
            this.dgMain.ReadOnly = true;
            this.dgMain.RowHeadersWidth = 26;
            this.dgMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgMain.Size = new System.Drawing.Size(405, 242);
            this.dgMain.TabIndex = 1;
            this.dgMain.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgMain_RowPostPaint);
            // 
            // bindingSourceMain
            // 
            this.bindingSourceMain.DataMember = "BuildingData";
            this.bindingSourceMain.DataSource = this.dsMain;
            this.bindingSourceMain.CurrentChanged += new System.EventHandler(this.bindingSourceMain_CurrentChanged);
            // 
            // dsMain
            // 
            this.dsMain.DataSetName = "dsMain";
            this.dsMain.Tables.AddRange(new System.Data.DataTable[] {
            this.BuildingData});
            // 
            // BuildingData
            // 
            this.BuildingData.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn1,
            this.dataColumn2,
            this.dataColumn3,
            this.dataColumn4,
            this.dataColumn5,
            this.dataColumn6,
            this.dataColumn7,
            this.dataColumn8,
            this.dataColumn9});
            this.BuildingData.TableName = "BuildingData";
            // 
            // dataColumn1
            // 
            this.dataColumn1.ColumnName = "BuildingName";
            // 
            // dataColumn2
            // 
            this.dataColumn2.ColumnName = "FloorNumber";
            this.dataColumn2.DataType = typeof(int);
            // 
            // dataColumn3
            // 
            this.dataColumn3.ColumnName = "RoomNumber";
            this.dataColumn3.DataType = typeof(int);
            // 
            // dataColumn4
            // 
            this.dataColumn4.ColumnName = "Height";
            this.dataColumn4.DataType = typeof(double);
            // 
            // dataColumn5
            // 
            this.dataColumn5.ColumnName = "Weight";
            this.dataColumn5.DataType = typeof(double);
            // 
            // dataColumn6
            // 
            this.dataColumn6.ColumnName = "DeviceCount";
            this.dataColumn6.DataType = typeof(int);
            // 
            // dataColumn7
            // 
            this.dataColumn7.ColumnName = "IsError";
            this.dataColumn7.DataType = typeof(bool);
            // 
            // bindingNavigatorMain
            // 
            this.bindingNavigatorMain.AddNewItem = null;
            this.bindingNavigatorMain.BindingSource = this.bindingSourceMain;
            this.bindingNavigatorMain.CountItem = this.bindingNavigatorCountItem;
            this.bindingNavigatorMain.DeleteItem = null;
            this.bindingNavigatorMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.btnFilterPreview});
            this.bindingNavigatorMain.Location = new System.Drawing.Point(0, 0);
            this.bindingNavigatorMain.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bindingNavigatorMain.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bindingNavigatorMain.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bindingNavigatorMain.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bindingNavigatorMain.Name = "bindingNavigatorMain";
            this.bindingNavigatorMain.PositionItem = this.bindingNavigatorPositionItem;
            this.bindingNavigatorMain.Size = new System.Drawing.Size(405, 25);
            this.bindingNavigatorMain.TabIndex = 0;
            this.bindingNavigatorMain.Text = "bindingNavigator1";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(35, 22);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 23);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolTipMain
            // 
            this.toolTipMain.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTipMain.ToolTipTitle = "Revit Sample";
            // 
            // openFileDialogSource
            // 
            this.openFileDialogSource.Filter = "XML файлы | *.xml";
            this.openFileDialogSource.Title = "Выберите файл";
            // 
            // timerSave
            // 
            this.timerSave.Interval = 300;
            this.timerSave.Tick += new System.EventHandler(this.timerSave_Tick);
            // 
            // dataColumn8
            // 
            this.dataColumn8.ColumnName = "Lenght";
            this.dataColumn8.DataType = typeof(double);
            // 
            // buildingNameDataGridViewTextBoxColumn
            // 
            this.buildingNameDataGridViewTextBoxColumn.DataPropertyName = "BuildingName";
            this.buildingNameDataGridViewTextBoxColumn.HeaderText = "Строение";
            this.buildingNameDataGridViewTextBoxColumn.Name = "buildingNameDataGridViewTextBoxColumn";
            this.buildingNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.buildingNameDataGridViewTextBoxColumn.Width = 140;
            // 
            // floorNumberDataGridViewTextBoxColumn
            // 
            this.floorNumberDataGridViewTextBoxColumn.DataPropertyName = "FloorNumber";
            this.floorNumberDataGridViewTextBoxColumn.HeaderText = "Этаж";
            this.floorNumberDataGridViewTextBoxColumn.Name = "floorNumberDataGridViewTextBoxColumn";
            this.floorNumberDataGridViewTextBoxColumn.ReadOnly = true;
            this.floorNumberDataGridViewTextBoxColumn.Width = 60;
            // 
            // heightDataGridViewTextBoxColumn
            // 
            this.heightDataGridViewTextBoxColumn.DataPropertyName = "Height";
            this.heightDataGridViewTextBoxColumn.HeaderText = "Высота, м";
            this.heightDataGridViewTextBoxColumn.Name = "heightDataGridViewTextBoxColumn";
            this.heightDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // weightDataGridViewTextBoxColumn
            // 
            this.weightDataGridViewTextBoxColumn.DataPropertyName = "Weight";
            this.weightDataGridViewTextBoxColumn.HeaderText = "Ширина, м";
            this.weightDataGridViewTextBoxColumn.Name = "weightDataGridViewTextBoxColumn";
            this.weightDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // Lenght
            // 
            this.Lenght.DataPropertyName = "Lenght";
            this.Lenght.HeaderText = "Длина, м";
            this.Lenght.Name = "Lenght";
            this.Lenght.ReadOnly = true;
            // 
            // deviceCountDataGridViewTextBoxColumn
            // 
            this.deviceCountDataGridViewTextBoxColumn.DataPropertyName = "DeviceCount";
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.PaleGoldenrod;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.deviceCountDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle8;
            this.deviceCountDataGridViewTextBoxColumn.HeaderText = "Устройств";
            this.deviceCountDataGridViewTextBoxColumn.Name = "deviceCountDataGridViewTextBoxColumn";
            this.deviceCountDataGridViewTextBoxColumn.ReadOnly = true;
            this.deviceCountDataGridViewTextBoxColumn.Width = 60;
            // 
            // btnFilterPreview
            // 
            this.btnFilterPreview.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnFilterPreview.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnPreviewAll,
            this.toolStripMenuItem1,
            this.btnPreviewErrorsRoom});
            this.btnFilterPreview.Image = ((System.Drawing.Image)(resources.GetObject("btnFilterPreview.Image")));
            this.btnFilterPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFilterPreview.Name = "btnFilterPreview";
            this.btnFilterPreview.Size = new System.Drawing.Size(86, 22);
            this.btnFilterPreview.Text = "Показать";
            // 
            // btnPreviewAll
            // 
            this.btnPreviewAll.Name = "btnPreviewAll";
            this.btnPreviewAll.Size = new System.Drawing.Size(196, 22);
            this.btnPreviewAll.Text = "Показывать все";
            this.btnPreviewAll.Click += new System.EventHandler(this.btnPreviewAll_Click);
            // 
            // btnPreviewErrorsRoom
            // 
            this.btnPreviewErrorsRoom.Name = "btnPreviewErrorsRoom";
            this.btnPreviewErrorsRoom.Size = new System.Drawing.Size(196, 22);
            this.btnPreviewErrorsRoom.Text = "Ошибки по комнатам";
            this.btnPreviewErrorsRoom.Click += new System.EventHandler(this.btnPreviewErrors_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(193, 6);
            // 
            // dataColumn9
            // 
            this.dataColumn9.ColumnName = "Code";
            this.dataColumn9.DataType = typeof(int);
            // 
            // strInformation
            // 
            this.strInformation.BackColor = System.Drawing.Color.Ivory;
            this.strInformation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.strInformation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.strInformation.Location = new System.Drawing.Point(0, 0);
            this.strInformation.Multiline = true;
            this.strInformation.Name = "strInformation";
            this.strInformation.Size = new System.Drawing.Size(169, 346);
            this.strInformation.TabIndex = 0;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 346);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "frmMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Revit Sample";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.savePicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BuildingData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigatorMain)).EndInit();
            this.bindingNavigatorMain.ResumeLayout(false);
            this.bindingNavigatorMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.TextBox strFileName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClearText;
        private System.Windows.Forms.ToolTip toolTipMain;
        private System.Windows.Forms.OpenFileDialog openFileDialogSource;
        private System.Windows.Forms.LinkLabel lnkCreateSampleData;
        private System.Windows.Forms.LinkLabel lnkSaveData;
        private System.Windows.Forms.LinkLabel lnkOpenData;
        private System.Windows.Forms.PictureBox savePicture;
        private System.Windows.Forms.Timer timerSave;
        private System.Windows.Forms.DataGridView dgMain;
        private System.Windows.Forms.DataGridViewTextBoxColumn zoomNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource bindingSourceMain;
        private System.Data.DataSet dsMain;
        private System.Data.DataTable BuildingData;
        private System.Data.DataColumn dataColumn1;
        private System.Data.DataColumn dataColumn2;
        private System.Data.DataColumn dataColumn3;
        private System.Data.DataColumn dataColumn4;
        private System.Data.DataColumn dataColumn5;
        private System.Data.DataColumn dataColumn6;
        private System.Data.DataColumn dataColumn7;
        private System.Windows.Forms.BindingNavigator bindingNavigatorMain;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Data.DataColumn dataColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn buildingNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn floorNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn heightDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn weightDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Lenght;
        private System.Windows.Forms.DataGridViewTextBoxColumn deviceCountDataGridViewTextBoxColumn;
        private System.Windows.Forms.ToolStripDropDownButton btnFilterPreview;
        private System.Windows.Forms.ToolStripMenuItem btnPreviewAll;
        private System.Windows.Forms.ToolStripMenuItem btnPreviewErrorsRoom;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Data.DataColumn dataColumn9;
        private System.Windows.Forms.TextBox strInformation;
    }
}

