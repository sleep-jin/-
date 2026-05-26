namespace 发票
{
    partial class MakeModes
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
            add = new Sunny.UI.UIButton();
            button1 = new Sunny.UI.UIButton();
            dataGridView1 = new Sunny.UI.UIDataGridView();
            Column1 = new DataGridViewImageColumn();
            classname = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            groupBox1 = new Sunny.UI.UIGroupBox();
            SaveMode = new Sunny.UI.UIButton();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // add
            // 
            add.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            add.Location = new Point(1519, 50);
            add.MinimumSize = new Size(1, 1);
            add.Name = "add";
            add.Size = new Size(112, 39);
            add.TabIndex = 1;
            add.Text = "添加模板";
            add.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            add.Click += add_Click;
            // 
            // button1
            // 
            button1.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            button1.Location = new Point(1658, 50);
            button1.MinimumSize = new Size(1, 1);
            button1.Name = "button1";
            button1.Size = new Size(112, 39);
            button1.TabIndex = 3;
            button1.Text = "删除模板";
            button1.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            button1.Click += button1_Click_1;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(235, 243, 255);
            dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle2.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.ColumnHeadersHeight = 32;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Column1, classname, Column2 });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridView1.GridColor = Color.FromArgb(80, 160, 255);
            dataGridView1.Location = new Point(1493, 95);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(235, 243, 255);
            dataGridViewCellStyle4.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle4.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle4.SelectionForeColor = Color.White;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dataGridView1.RowHeadersWidth = 62;
            dataGridViewCellStyle5.BackColor = Color.White;
            dataGridViewCellStyle5.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle5;
            dataGridView1.SelectedIndex = -1;
            dataGridView1.Size = new Size(712, 1011);
            dataGridView1.StripeOddColor = Color.FromArgb(235, 243, 255);
            dataGridView1.TabIndex = 4;
            // 
            // Column1
            // 
            Column1.HeaderText = "图片";
            Column1.MinimumWidth = 8;
            Column1.Name = "Column1";
            Column1.Width = 300;
            // 
            // classname
            // 
            classname.HeaderText = "分类";
            classname.MinimumWidth = 8;
            classname.Name = "classname";
            classname.Resizable = DataGridViewTriState.True;
            classname.SortMode = DataGridViewColumnSortMode.NotSortable;
            classname.Width = 150;
            // 
            // Column2
            // 
            Column2.HeaderText = "ROI";
            Column2.MinimumWidth = 8;
            Column2.Name = "Column2";
            Column2.Resizable = DataGridViewTriState.True;
            Column2.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column2.Width = 200;
            // 
            // groupBox1
            // 
            groupBox1.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            groupBox1.Location = new Point(6, 35);
            groupBox1.Margin = new Padding(4, 5, 4, 5);
            groupBox1.MinimumSize = new Size(1, 1);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(0, 32, 0, 0);
            groupBox1.Size = new Size(1466, 1071);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            groupBox1.Text = "PDF";
            groupBox1.TextAlignment = ContentAlignment.MiddleLeft;
            // 
            // SaveMode
            // 
            SaveMode.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            SaveMode.Location = new Point(1814, 50);
            SaveMode.MinimumSize = new Size(1, 1);
            SaveMode.Name = "SaveMode";
            SaveMode.Size = new Size(112, 39);
            SaveMode.TabIndex = 6;
            SaveMode.Text = "保存";
            SaveMode.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            SaveMode.Click += SaveMode_Click;
            // 
            // MakeModes
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(2208, 1125);
            Controls.Add(SaveMode);
            Controls.Add(groupBox1);
            Controls.Add(dataGridView1);
            Controls.Add(button1);
            Controls.Add(add);
            Name = "MakeModes";
            Text = "MakeModes";
            ZoomScaleRect = new Rectangle(22, 22, 2208, 1080);
            Load += MakeModes_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Sunny.UI.UIButton add;
        private Sunny.UI.UIButton button1;
        private Sunny.UI.UIDataGridView dataGridView1;
        private Sunny.UI.UIGroupBox groupBox1;
        private Sunny.UI.UIButton SaveMode;
        private DataGridViewImageColumn Column1;
        private DataGridViewTextBoxColumn classname;
        private DataGridViewTextBoxColumn Column2;
    }
}