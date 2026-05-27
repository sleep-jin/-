namespace 发票
{
    partial class ExcelShow
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
            dataGridView1 = new Sunny.UI.UIDataGridView();
            textBox1 = new Sunny.UI.UITextBox();
            label1 = new Sunny.UI.UILabel();
            label2 = new Sunny.UI.UILabel();
            textBox2 = new Sunny.UI.UITextBox();
            InputExcel = new Sunny.UI.UIButton();
            button3 = new Sunny.UI.UIButton();
            textBox4 = new Sunny.UI.UITextBox();
            uiTableLayoutPanel1 = new Sunny.UI.UITableLayoutPanel();
            uiTableLayoutPanel2 = new Sunny.UI.UITableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            uiTableLayoutPanel1.SuspendLayout();
            uiTableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridViewCellStyle1.BackColor = Color.FromArgb(235, 243, 255);
            dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.BackgroundColor = SystemColors.ButtonShadow;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle2.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridView1.GridColor = Color.FromArgb(80, 160, 255);
            dataGridView1.Location = new Point(3, 3);
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
            dataGridView1.Size = new Size(1236, 884);
            dataGridView1.StripeOddColor = Color.FromArgb(235, 243, 255);
            dataGridView1.TabIndex = 0;
            // 
            // textBox1
            // 
            textBox1.DoubleValue = 12D;
            textBox1.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            textBox1.IntValue = 12;
            textBox1.Location = new Point(140, 5);
            textBox1.Margin = new Padding(4, 5, 4, 5);
            textBox1.MinimumSize = new Size(1, 16);
            textBox1.Name = "textBox1";
            textBox1.Padding = new Padding(5);
            textBox1.ShowText = false;
            textBox1.Size = new Size(80, 24);
            textBox1.TabIndex = 1;
            textBox1.Text = "12";
            textBox1.TextAlignment = ContentAlignment.MiddleLeft;
            textBox1.Watermark = "";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            label1.ForeColor = Color.FromArgb(48, 48, 48);
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(130, 24);
            label1.TabIndex = 2;
            label1.Text = "导入起始行";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            label2.ForeColor = Color.FromArgb(48, 48, 48);
            label2.Location = new Point(227, 0);
            label2.Name = "label2";
            label2.Size = new Size(130, 24);
            label2.TabIndex = 4;
            label2.Text = "导入起始列";
            // 
            // textBox2
            // 
            textBox2.DoubleValue = 1D;
            textBox2.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            textBox2.IntValue = 1;
            textBox2.Location = new Point(364, 5);
            textBox2.Margin = new Padding(4, 5, 4, 5);
            textBox2.MinimumSize = new Size(1, 16);
            textBox2.Name = "textBox2";
            textBox2.Padding = new Padding(5);
            textBox2.ShowText = false;
            textBox2.Size = new Size(80, 24);
            textBox2.TabIndex = 3;
            textBox2.Text = "1";
            textBox2.TextAlignment = ContentAlignment.MiddleLeft;
            textBox2.Watermark = "";
            // 
            // InputExcel
            // 
            InputExcel.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            InputExcel.Location = new Point(453, 4);
            InputExcel.Margin = new Padding(5, 4, 5, 4);
            InputExcel.MinimumSize = new Size(1, 1);
            InputExcel.Name = "InputExcel";
            InputExcel.Size = new Size(143, 26);
            InputExcel.TabIndex = 18;
            InputExcel.Text = "导入表格";
            InputExcel.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            InputExcel.Click += ImportExcelButton_Click;
            // 
            // button3
            // 
            button3.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            button3.Location = new Point(1087, 4);
            button3.Margin = new Padding(5, 4, 5, 4);
            button3.MinimumSize = new Size(1, 1);
            button3.Name = "button3";
            button3.Size = new Size(143, 26);
            button3.TabIndex = 20;
            button3.Text = "选择表格";
            button3.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            button3.Click += SelectExcelFileButton_Click;
            // 
            // textBox4
            // 
            textBox4.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            textBox4.Location = new Point(606, 4);
            textBox4.Margin = new Padding(5, 4, 5, 4);
            textBox4.MinimumSize = new Size(1, 16);
            textBox4.Name = "textBox4";
            textBox4.Padding = new Padding(5);
            textBox4.ShowText = false;
            textBox4.Size = new Size(471, 26);
            textBox4.TabIndex = 19;
            textBox4.TextAlignment = ContentAlignment.MiddleLeft;
            textBox4.Watermark = "";
            // 
            // uiTableLayoutPanel1
            // 
            uiTableLayoutPanel1.ColumnCount = 1;
            uiTableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            uiTableLayoutPanel1.Controls.Add(uiTableLayoutPanel2, 0, 1);
            uiTableLayoutPanel1.Controls.Add(dataGridView1, 0, 0);
            uiTableLayoutPanel1.Dock = DockStyle.Fill;
            uiTableLayoutPanel1.Location = new Point(0, 35);
            uiTableLayoutPanel1.Name = "uiTableLayoutPanel1";
            uiTableLayoutPanel1.RowCount = 2;
            uiTableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            uiTableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            uiTableLayoutPanel1.Size = new Size(1242, 930);
            uiTableLayoutPanel1.TabIndex = 21;
            uiTableLayoutPanel1.TagString = null;
            // 
            // uiTableLayoutPanel2
            // 
            uiTableLayoutPanel2.ColumnCount = 7;
            uiTableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            uiTableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            uiTableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            uiTableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            uiTableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            uiTableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            uiTableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            uiTableLayoutPanel2.Controls.Add(textBox1, 1, 0);
            uiTableLayoutPanel2.Controls.Add(button3, 6, 0);
            uiTableLayoutPanel2.Controls.Add(label2, 2, 0);
            uiTableLayoutPanel2.Controls.Add(textBox4, 5, 0);
            uiTableLayoutPanel2.Controls.Add(textBox2, 3, 0);
            uiTableLayoutPanel2.Controls.Add(InputExcel, 4, 0);
            uiTableLayoutPanel2.Controls.Add(label1, 0, 0);
            uiTableLayoutPanel2.Location = new Point(3, 893);
            uiTableLayoutPanel2.Name = "uiTableLayoutPanel2";
            uiTableLayoutPanel2.RowCount = 1;
            uiTableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            uiTableLayoutPanel2.Size = new Size(1234, 34);
            uiTableLayoutPanel2.TabIndex = 22;
            uiTableLayoutPanel2.TagString = null;
            // 
            // ExcelShow
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1242, 965);
            Controls.Add(uiTableLayoutPanel1);
            Name = "ExcelShow";
            Text = "ExcelShow";
            ZoomScaleRect = new Rectangle(22, 22, 1336, 710);
            Load += ExcelShow_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            uiTableLayoutPanel1.ResumeLayout(false);
            uiTableLayoutPanel2.ResumeLayout(false);
            uiTableLayoutPanel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Sunny.UI.UIDataGridView dataGridView1;
        private Sunny.UI.UITextBox textBox1;
        private Sunny.UI.UILabel label1;
        private Sunny.UI.UILabel label2;
        private Sunny.UI.UITextBox textBox2;
        private Sunny.UI.UIButton InputExcel;
        private Sunny.UI.UIButton button3;
        private Sunny.UI.UITextBox textBox4;
        private Sunny.UI.UITableLayoutPanel uiTableLayoutPanel1;
        private Sunny.UI.UITableLayoutPanel uiTableLayoutPanel2;
    }
}