namespace 发票
{
    partial class MakeXMLMode
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle10 = new DataGridViewCellStyle();
            XMLGridView = new Sunny.UI.UIDataGridView();
            节点 = new DataGridViewTextBoxColumn();
            结果 = new DataGridViewTextBoxColumn();
            jsonGridView = new Sunny.UI.UIDataGridView();
            名称 = new DataGridViewTextBoxColumn();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            uiLabel1 = new Sunny.UI.UILabel();
            addPort = new Sunny.UI.UIButton();
            deletePort = new Sunny.UI.UIButton();
            saveJson = new Sunny.UI.UIButton();
            uiLabel2 = new Sunny.UI.UILabel();
            ((System.ComponentModel.ISupportInitialize)XMLGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)jsonGridView).BeginInit();
            SuspendLayout();
            // 
            // XMLGridView
            // 
            XMLGridView.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(235, 243, 255);
            XMLGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            XMLGridView.BackgroundColor = Color.White;
            XMLGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle2.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            XMLGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            XMLGridView.ColumnHeadersHeight = 32;
            XMLGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            XMLGridView.Columns.AddRange(new DataGridViewColumn[] { 节点, 结果 });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            XMLGridView.DefaultCellStyle = dataGridViewCellStyle3;
            XMLGridView.EnableHeadersVisualStyles = false;
            XMLGridView.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            XMLGridView.GridColor = Color.FromArgb(80, 160, 255);
            XMLGridView.Location = new Point(3, 74);
            XMLGridView.MultiSelect = false;
            XMLGridView.Name = "XMLGridView";
            XMLGridView.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(235, 243, 255);
            dataGridViewCellStyle4.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle4.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle4.SelectionForeColor = Color.White;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            XMLGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            XMLGridView.RowHeadersWidth = 62;
            dataGridViewCellStyle5.BackColor = Color.White;
            dataGridViewCellStyle5.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            XMLGridView.RowsDefaultCellStyle = dataGridViewCellStyle5;
            XMLGridView.SelectedIndex = -1;
            XMLGridView.Size = new Size(961, 467);
            XMLGridView.StripeOddColor = Color.FromArgb(235, 243, 255);
            XMLGridView.TabIndex = 0;
            // 
            // 节点
            // 
            节点.HeaderText = "节点";
            节点.MinimumWidth = 8;
            节点.Name = "节点";
            节点.Width = 450;
            // 
            // 结果
            // 
            结果.HeaderText = "值";
            结果.MinimumWidth = 8;
            结果.Name = "结果";
            结果.Width = 450;
            // 
            // jsonGridView
            // 
            jsonGridView.AllowUserToAddRows = false;
            dataGridViewCellStyle6.BackColor = Color.FromArgb(235, 243, 255);
            jsonGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            jsonGridView.BackgroundColor = Color.White;
            jsonGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle7.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle7.ForeColor = Color.White;
            dataGridViewCellStyle7.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = DataGridViewTriState.True;
            jsonGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            jsonGridView.ColumnHeadersHeight = 32;
            jsonGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            jsonGridView.Columns.AddRange(new DataGridViewColumn[] { 名称, Column1, Column2 });
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = SystemColors.Window;
            dataGridViewCellStyle8.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle8.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle8.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = DataGridViewTriState.False;
            jsonGridView.DefaultCellStyle = dataGridViewCellStyle8;
            jsonGridView.EnableHeadersVisualStyles = false;
            jsonGridView.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            jsonGridView.GridColor = Color.FromArgb(80, 160, 255);
            jsonGridView.Location = new Point(3, 584);
            jsonGridView.MultiSelect = false;
            jsonGridView.Name = "jsonGridView";
            jsonGridView.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = Color.FromArgb(235, 243, 255);
            dataGridViewCellStyle9.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle9.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle9.SelectionBackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle9.SelectionForeColor = Color.White;
            dataGridViewCellStyle9.WrapMode = DataGridViewTriState.True;
            jsonGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            jsonGridView.RowHeadersWidth = 62;
            dataGridViewCellStyle10.BackColor = Color.White;
            dataGridViewCellStyle10.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            jsonGridView.RowsDefaultCellStyle = dataGridViewCellStyle10;
            jsonGridView.SelectedIndex = -1;
            jsonGridView.Size = new Size(961, 401);
            jsonGridView.StripeOddColor = Color.FromArgb(235, 243, 255);
            jsonGridView.TabIndex = 1;
            // 
            // 名称
            // 
            名称.HeaderText = "名称";
            名称.MinimumWidth = 8;
            名称.Name = "名称";
            名称.Width = 150;
            // 
            // Column1
            // 
            Column1.HeaderText = "节点";
            Column1.MinimumWidth = 8;
            Column1.Name = "Column1";
            Column1.Width = 450;
            // 
            // Column2
            // 
            Column2.HeaderText = "值";
            Column2.MinimumWidth = 8;
            Column2.Name = "Column2";
            Column2.Width = 300;
            // 
            // uiLabel1
            // 
            uiLabel1.BackColor = Color.FromArgb(0, 192, 0);
            uiLabel1.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiLabel1.ForeColor = Color.FromArgb(48, 48, 48);
            uiLabel1.Location = new Point(3, 544);
            uiLabel1.Name = "uiLabel1";
            uiLabel1.Size = new Size(961, 37);
            uiLabel1.TabIndex = 2;
            uiLabel1.Text = "配置结点";
            uiLabel1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // addPort
            // 
            addPort.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            addPort.Location = new Point(970, 503);
            addPort.MinimumSize = new Size(1, 1);
            addPort.Name = "addPort";
            addPort.Radius = 38;
            addPort.Size = new Size(114, 38);
            addPort.TabIndex = 3;
            addPort.Text = "添加节点";
            addPort.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            addPort.Click += addPort_Click;
            // 
            // deletePort
            // 
            deletePort.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            deletePort.Location = new Point(970, 584);
            deletePort.MinimumSize = new Size(1, 1);
            deletePort.Name = "deletePort";
            deletePort.Radius = 38;
            deletePort.Size = new Size(114, 38);
            deletePort.TabIndex = 4;
            deletePort.Text = "删除节点";
            deletePort.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            deletePort.Click += deletePort_Click;
            // 
            // saveJson
            // 
            saveJson.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            saveJson.Location = new Point(970, 628);
            saveJson.MinimumSize = new Size(1, 1);
            saveJson.Name = "saveJson";
            saveJson.Radius = 38;
            saveJson.Size = new Size(114, 38);
            saveJson.TabIndex = 5;
            saveJson.Text = "保存";
            saveJson.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            saveJson.Click += saveJson_Click;
            // 
            // uiLabel2
            // 
            uiLabel2.BackColor = Color.FromArgb(0, 192, 0);
            uiLabel2.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiLabel2.ForeColor = Color.FromArgb(48, 48, 48);
            uiLabel2.Location = new Point(3, 35);
            uiLabel2.Name = "uiLabel2";
            uiLabel2.Size = new Size(961, 37);
            uiLabel2.TabIndex = 6;
            uiLabel2.Text = "模板结点";
            uiLabel2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // MakeXMLMode
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1097, 988);
            Controls.Add(uiLabel2);
            Controls.Add(saveJson);
            Controls.Add(deletePort);
            Controls.Add(addPort);
            Controls.Add(uiLabel1);
            Controls.Add(jsonGridView);
            Controls.Add(XMLGridView);
            Name = "MakeXMLMode";
            Text = "XMLtempleta";
            ZoomScaleRect = new Rectangle(22, 22, 1133, 988);
            Load += MakeXMLMode_Load;
            ((System.ComponentModel.ISupportInitialize)XMLGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)jsonGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Sunny.UI.UIDataGridView XMLGridView;
        private DataGridViewTextBoxColumn 节点;
        private DataGridViewTextBoxColumn 结果;
        private Sunny.UI.UIDataGridView jsonGridView;
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UIButton addPort;
        private Sunny.UI.UIButton deletePort;
        private Sunny.UI.UIButton saveJson;
        private Sunny.UI.UILabel uiLabel2;
        private DataGridViewTextBoxColumn 名称;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
    }
}