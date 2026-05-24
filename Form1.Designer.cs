
namespace 发票
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            textBox1 = new Sunny.UI.UITextBox();
            MakeMode = new Sunny.UI.UIButton();
            编辑分类 = new Sunny.UI.UIButton();
            Start = new Sunny.UI.UIButton();
            txtFomatbox = new Sunny.UI.UITextBox();
            setTXTFomat = new Sunny.UI.UIButton();
            testMode = new CheckBox();
            outPDF = new Sunny.UI.UIButton();
            button4 = new Sunny.UI.UIButton();
            textBox2 = new Sunny.UI.UITextBox();
            label1 = new Label();
            PDFfomat = new Sunny.UI.UITextBox();
            label2 = new Label();
            label3 = new Label();
            textBox3 = new Sunny.UI.UITextBox();
            textBox4 = new Sunny.UI.UITextBox();
            label4 = new Label();
            label5 = new Label();
            progressBar1 = new ProgressBar();
            lblProgress = new Label();
            Modlechoose = new Sunny.UI.UISwitch();
            showFomat = new Sunny.UI.UITextBox();
            listBox1 = new Sunny.UI.UIListBox();
            button1 = new Sunny.UI.UIButton();
            Button2 = new Sunny.UI.UIButton();
            PDFdata = new Sunny.UI.UIDataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            checkBox1 = new CheckBox();
            FileClass = new Sunny.UI.UIComboBox();
            label6 = new Label();
            ModeName = new Sunny.UI.UIComboBox();
            label7 = new Label();
            LoadTemplate = new Sunny.UI.UIButton();
            ((System.ComponentModel.ISupportInitialize)PDFdata).BeginInit();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            textBox1.Location = new Point(14, 39);
            textBox1.Margin = new Padding(5, 4, 5, 4);
            textBox1.MinimumSize = new Size(1, 16);
            textBox1.Name = "textBox1";
            textBox1.Padding = new Padding(5);
            textBox1.ShowText = false;
            textBox1.Size = new Size(471, 35);
            textBox1.TabIndex = 0;
            textBox1.TextAlignment = ContentAlignment.MiddleLeft;
            textBox1.Watermark = "";
            // 
            // MakeMode
            // 
            MakeMode.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            MakeMode.Location = new Point(14, 168);
            MakeMode.Margin = new Padding(5, 4, 5, 4);
            MakeMode.MinimumSize = new Size(1, 1);
            MakeMode.Name = "MakeMode";
            MakeMode.Radius = 15;
            MakeMode.Size = new Size(143, 32);
            MakeMode.TabIndex = 8;
            MakeMode.Text = "制作模板";
            MakeMode.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            MakeMode.Click += MakeMode_Click;
            // 
            // 编辑分类
            // 
            编辑分类.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            编辑分类.Location = new Point(14, 527);
            编辑分类.Margin = new Padding(5, 4, 5, 4);
            编辑分类.MinimumSize = new Size(1, 1);
            编辑分类.Name = "编辑分类";
            编辑分类.Radius = 15;
            编辑分类.Size = new Size(143, 33);
            编辑分类.TabIndex = 10;
            编辑分类.Text = "编辑分类";
            编辑分类.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            编辑分类.Click += 编辑分类_Click;
            // 
            // Start
            // 
            Start.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            Start.Location = new Point(473, 168);
            Start.Margin = new Padding(5, 4, 5, 4);
            Start.MinimumSize = new Size(1, 1);
            Start.Name = "Start";
            Start.Size = new Size(143, 32);
            Start.TabIndex = 11;
            Start.Text = " 开始识别";
            Start.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            Start.Click += Start_Click;
            // 
            // txtFomatbox
            // 
            txtFomatbox.Font = new Font("宋体", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtFomatbox.Location = new Point(275, 203);
            txtFomatbox.Margin = new Padding(5, 4, 5, 4);
            txtFomatbox.MinimumSize = new Size(1, 16);
            txtFomatbox.Name = "txtFomatbox";
            txtFomatbox.Padding = new Padding(5);
            txtFomatbox.ShowText = false;
            txtFomatbox.Size = new Size(363, 35);
            txtFomatbox.TabIndex = 12;
            txtFomatbox.Text = "XX-{0}_{1}-{2}_{3}";
            txtFomatbox.TextAlignment = ContentAlignment.MiddleLeft;
            txtFomatbox.Watermark = "";
            // 
            // setTXTFomat
            // 
            setTXTFomat.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            setTXTFomat.Location = new Point(167, 280);
            setTXTFomat.Margin = new Padding(5, 4, 5, 4);
            setTXTFomat.MinimumSize = new Size(1, 1);
            setTXTFomat.Name = "setTXTFomat";
            setTXTFomat.Radius = 15;
            setTXTFomat.Size = new Size(111, 24);
            setTXTFomat.TabIndex = 13;
            setTXTFomat.Text = "设置格式";
            setTXTFomat.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            setTXTFomat.Click += setTXTFomat_Click;
            // 
            // testMode
            // 
            testMode.AutoSize = true;
            testMode.Location = new Point(284, 279);
            testMode.Name = "testMode";
            testMode.Size = new Size(132, 28);
            testMode.TabIndex = 14;
            testMode.Text = "调试模板";
            testMode.UseVisualStyleBackColor = true;
            // 
            // outPDF
            // 
            outPDF.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            outPDF.Location = new Point(320, 168);
            outPDF.Margin = new Padding(5, 4, 5, 4);
            outPDF.MinimumSize = new Size(1, 1);
            outPDF.Name = "outPDF";
            outPDF.Radius = 15;
            outPDF.Size = new Size(143, 32);
            outPDF.TabIndex = 16;
            outPDF.Text = "导出新PDF";
            outPDF.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            outPDF.Click += outPDF_Click;
            // 
            // button4
            // 
            button4.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            button4.Location = new Point(167, 168);
            button4.Margin = new Padding(5, 4, 5, 4);
            button4.MinimumSize = new Size(1, 1);
            button4.Name = "button4";
            button4.Radius = 15;
            button4.Size = new Size(143, 32);
            button4.TabIndex = 18;
            button4.Text = "预览表格";
            button4.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            button4.Click += button4_Click;
            // 
            // textBox2
            // 
            textBox2.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            textBox2.Location = new Point(14, 78);
            textBox2.Margin = new Padding(5, 4, 5, 4);
            textBox2.MinimumSize = new Size(1, 16);
            textBox2.Name = "textBox2";
            textBox2.Padding = new Padding(5);
            textBox2.ShowText = false;
            textBox2.Size = new Size(471, 35);
            textBox2.TabIndex = 3;
            textBox2.TextAlignment = ContentAlignment.MiddleLeft;
            textBox2.Watermark = "";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(422, 280);
            label1.Name = "label1";
            label1.Size = new Size(226, 24);
            label1.TabIndex = 19;
            label1.Text = "根据模板显示格式：";
            // 
            // PDFfomat
            // 
            PDFfomat.Font = new Font("宋体", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 134);
            PDFfomat.Location = new Point(275, 241);
            PDFfomat.Margin = new Padding(5, 4, 5, 4);
            PDFfomat.MinimumSize = new Size(1, 16);
            PDFfomat.Name = "PDFfomat";
            PDFfomat.Padding = new Padding(5);
            PDFfomat.ShowText = false;
            PDFfomat.Size = new Size(363, 35);
            PDFfomat.TabIndex = 20;
            PDFfomat.Text = "XX-{0}_{1}-{2}_{3}";
            PDFfomat.TextAlignment = ContentAlignment.MiddleLeft;
            PDFfomat.Watermark = "";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            label2.Location = new Point(164, 209);
            label2.Name = "label2";
            label2.Size = new Size(98, 18);
            label2.TabIndex = 21;
            label2.Text = "表格的格式";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            label3.Location = new Point(167, 243);
            label3.Name = "label3";
            label3.Size = new Size(89, 18);
            label3.TabIndex = 22;
            label3.Text = "PDF的格式";
            // 
            // textBox3
            // 
            textBox3.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            textBox3.Location = new Point(14, 577);
            textBox3.Margin = new Padding(5, 4, 5, 4);
            textBox3.MinimumSize = new Size(1, 16);
            textBox3.Name = "textBox3";
            textBox3.Padding = new Padding(5);
            textBox3.PasswordChar = '*';
            textBox3.ShowText = false;
            textBox3.Size = new Size(363, 35);
            textBox3.TabIndex = 23;
            textBox3.TextAlignment = ContentAlignment.MiddleLeft;
            textBox3.Watermark = "";
            textBox3.TextChanged += textBox3_TextChanged;
            // 
            // textBox4
            // 
            textBox4.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            textBox4.Location = new Point(14, 615);
            textBox4.Margin = new Padding(5, 4, 5, 4);
            textBox4.MinimumSize = new Size(1, 16);
            textBox4.Name = "textBox4";
            textBox4.Padding = new Padding(5);
            textBox4.PasswordChar = '*';
            textBox4.ShowText = false;
            textBox4.Size = new Size(363, 35);
            textBox4.TabIndex = 24;
            textBox4.TextAlignment = ContentAlignment.MiddleLeft;
            textBox4.Watermark = "";
            textBox4.TextChanged += textBox3_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(395, 583);
            label4.Name = "label4";
            label4.Size = new Size(166, 24);
            label4.TabIndex = 25;
            label4.Text = "BAIDU_API_KEY";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(385, 615);
            label5.Name = "label5";
            label5.Size = new Size(202, 24);
            label5.TabIndex = 26;
            label5.Text = "BAIDU_SECRET_KEY";
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(14, 660);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(471, 25);
            progressBar1.TabIndex = 27;
            // 
            // lblProgress
            // 
            lblProgress.AutoSize = true;
            lblProgress.Location = new Point(495, 662);
            lblProgress.Name = "lblProgress";
            lblProgress.Size = new Size(70, 24);
            lblProgress.TabIndex = 28;
            lblProgress.Text = "0 / 0";
            // 
            // Modlechoose
            // 
            Modlechoose.ActiveText = "高精度模型";
            Modlechoose.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            Modlechoose.InActiveText = "标准模型";
            Modlechoose.Location = new Point(14, 705);
            Modlechoose.MinimumSize = new Size(1, 1);
            Modlechoose.Name = "Modlechoose";
            Modlechoose.Size = new Size(171, 44);
            Modlechoose.TabIndex = 29;
            Modlechoose.Text = "uiSwitch1";
            // 
            // showFomat
            // 
            showFomat.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            showFomat.Location = new Point(167, 313);
            showFomat.Margin = new Padding(4, 5, 4, 5);
            showFomat.MinimumSize = new Size(1, 16);
            showFomat.Multiline = true;
            showFomat.Name = "showFomat";
            showFomat.Padding = new Padding(5);
            showFomat.ShowText = false;
            showFomat.Size = new Size(486, 207);
            showFomat.TabIndex = 30;
            showFomat.Text = "选择分类";
            showFomat.TextAlignment = ContentAlignment.MiddleLeft;
            showFomat.Watermark = "";
            // 
            // listBox1
            // 
            listBox1.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            listBox1.HoverColor = Color.FromArgb(155, 200, 255);
            listBox1.ItemSelectForeColor = Color.White;
            listBox1.Location = new Point(14, 209);
            listBox1.Margin = new Padding(4, 5, 4, 5);
            listBox1.MinimumSize = new Size(1, 1);
            listBox1.Name = "listBox1";
            listBox1.Padding = new Padding(2);
            listBox1.ShowText = false;
            listBox1.Size = new Size(143, 311);
            listBox1.TabIndex = 31;
            listBox1.Text = "uiListBox1";
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            // 
            // button1
            // 
            button1.Font = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            button1.Location = new Point(493, 39);
            button1.MinimumSize = new Size(1, 1);
            button1.Name = "button1";
            button1.Radius = 35;
            button1.Size = new Size(155, 36);
            button1.TabIndex = 32;
            button1.Text = "选择发票文件夹";
            button1.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            button1.Click += button1_Click;
            // 
            // Button2
            // 
            Button2.Font = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            Button2.Location = new Point(493, 76);
            Button2.MinimumSize = new Size(1, 1);
            Button2.Name = "Button2";
            Button2.Radius = 35;
            Button2.Size = new Size(155, 37);
            Button2.TabIndex = 33;
            Button2.Text = "选择发票导出位置";
            Button2.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            Button2.Click += button2_Click;
            // 
            // PDFdata
            // 
            PDFdata.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(192, 255, 255);
            PDFdata.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            PDFdata.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            PDFdata.BackgroundColor = Color.White;
            PDFdata.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle2.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            PDFdata.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            PDFdata.ColumnHeadersHeight = 32;
            PDFdata.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            PDFdata.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2, Column3 });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            PDFdata.DefaultCellStyle = dataGridViewCellStyle3;
            PDFdata.EnableHeadersVisualStyles = false;
            PDFdata.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            PDFdata.GridColor = Color.FromArgb(80, 160, 255);
            PDFdata.Location = new Point(654, 39);
            PDFdata.MultiSelect = false;
            PDFdata.Name = "PDFdata";
            PDFdata.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(235, 243, 255);
            dataGridViewCellStyle4.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            dataGridViewCellStyle4.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(80, 160, 255);
            dataGridViewCellStyle4.SelectionForeColor = Color.White;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            PDFdata.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            PDFdata.RowHeadersWidth = 62;
            dataGridViewCellStyle5.BackColor = Color.Honeydew;
            dataGridViewCellStyle5.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            PDFdata.RowsDefaultCellStyle = dataGridViewCellStyle5;
            PDFdata.SelectedIndex = -1;
            PDFdata.Size = new Size(540, 666);
            PDFdata.StripeEvenColor = Color.Honeydew;
            PDFdata.StripeOddColor = Color.FromArgb(192, 255, 255);
            PDFdata.TabIndex = 34;
            // 
            // Column1
            // 
            Column1.HeaderText = "发票";
            Column1.MinimumWidth = 8;
            Column1.Name = "Column1";
            // 
            // Column2
            // 
            Column2.HeaderText = "路径";
            Column2.MinimumWidth = 8;
            Column2.Name = "Column2";
            Column2.Visible = false;
            // 
            // Column3
            // 
            Column3.HeaderText = "分类";
            Column3.MinimumWidth = 8;
            Column3.Name = "Column3";
            Column3.Visible = false;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Checked = true;
            checkBox1.CheckState = CheckState.Checked;
            checkBox1.Location = new Point(506, 532);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(132, 28);
            checkBox1.TabIndex = 35;
            checkBox1.Text = "存储密钥";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // FileClass
            // 
            FileClass.DataSource = null;
            FileClass.FillColor = Color.White;
            FileClass.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            FileClass.ItemHoverColor = Color.FromArgb(155, 200, 255);
            FileClass.Items.AddRange(new object[] { "PDF", "XML" });
            FileClass.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            FileClass.Location = new Point(127, 122);
            FileClass.Margin = new Padding(4, 5, 4, 5);
            FileClass.MinimumSize = new Size(63, 0);
            FileClass.Name = "FileClass";
            FileClass.Padding = new Padding(0, 0, 30, 2);
            FileClass.Size = new Size(111, 30);
            FileClass.SymbolSize = 24;
            FileClass.TabIndex = 36;
            FileClass.Text = "PDF";
            FileClass.TextAlignment = ContentAlignment.MiddleLeft;
            FileClass.Watermark = "";
            FileClass.SelectedIndexChanged += FileClass_SelectedIndexChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(14, 128);
            label6.Name = "label6";
            label6.Size = new Size(106, 24);
            label6.TabIndex = 37;
            label6.Text = "文件类型";
            // 
            // ModeName
            // 
            ModeName.DataSource = null;
            ModeName.FillColor = Color.White;
            ModeName.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            ModeName.ItemHoverColor = Color.FromArgb(155, 200, 255);
            ModeName.Items.AddRange(new object[] { "发票" });
            ModeName.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
            ModeName.Location = new Point(374, 122);
            ModeName.Margin = new Padding(4, 5, 4, 5);
            ModeName.MinimumSize = new Size(63, 0);
            ModeName.Name = "ModeName";
            ModeName.Padding = new Padding(0, 0, 30, 2);
            ModeName.Size = new Size(111, 30);
            ModeName.SymbolSize = 24;
            ModeName.TabIndex = 37;
            ModeName.Text = "发票";
            ModeName.TextAlignment = ContentAlignment.MiddleLeft;
            ModeName.Watermark = "";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(261, 128);
            label7.Name = "label7";
            label7.Size = new Size(106, 24);
            label7.TabIndex = 38;
            label7.Text = "模板名称";
            // 
            // LoadTemplate
            // 
            LoadTemplate.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            LoadTemplate.Location = new Point(167, 529);
            LoadTemplate.Margin = new Padding(5, 4, 5, 4);
            LoadTemplate.MinimumSize = new Size(1, 1);
            LoadTemplate.Name = "LoadTemplate";
            LoadTemplate.Radius = 15;
            LoadTemplate.Size = new Size(143, 33);
            LoadTemplate.TabIndex = 40;
            LoadTemplate.Text = "加载模板";
            LoadTemplate.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            LoadTemplate.Click += LoadTemplate_Click;
            // 
            // Form1
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1197, 840);
            Controls.Add(LoadTemplate);
            Controls.Add(label7);
            Controls.Add(ModeName);
            Controls.Add(label6);
            Controls.Add(FileClass);
            Controls.Add(checkBox1);
            Controls.Add(PDFdata);
            Controls.Add(Button2);
            Controls.Add(button1);
            Controls.Add(listBox1);
            Controls.Add(showFomat);
            Controls.Add(Modlechoose);
            Controls.Add(label5);
            Controls.Add(progressBar1);
            Controls.Add(lblProgress);
            Controls.Add(label4);
            Controls.Add(textBox4);
            Controls.Add(textBox3);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(PDFfomat);
            Controls.Add(label1);
            Controls.Add(button4);
            Controls.Add(outPDF);
            Controls.Add(testMode);
            Controls.Add(setTXTFomat);
            Controls.Add(txtFomatbox);
            Controls.Add(Start);
            Controls.Add(编辑分类);
            Controls.Add(MakeMode);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Name = "Form1";
            Text = "Form1";
            ZoomScaleRect = new Rectangle(22, 22, 1197, 708);
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)PDFdata).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Sunny.UI.UITextBox textBox1;
        private Sunny.UI.UIButton MakeMode;
        private Sunny.UI.UIButton 编辑分类;
        private Sunny.UI.UIButton Start;
        private DataGridViewTextBoxColumn Fullname;
        private DataGridViewTextBoxColumn classname;
        private Sunny.UI.UITextBox txtFomatbox;
        private Sunny.UI.UIButton setTXTFomat;
        private CheckBox testMode;
        private Sunny.UI.UIButton outPDF;
        private Sunny.UI.UIButton button4;
        private Sunny.UI.UITextBox textBox2;
        private Label label1;
        private Sunny.UI.UITextBox PDFfomat;
        private Label label2;
        private Label label3;
        private Sunny.UI.UITextBox textBox3;
        private Sunny.UI.UITextBox textBox4;
        private Label label4;
        private Label label5;
        private ProgressBar progressBar1;
        private Label lblProgress;
        private Sunny.UI.UISwitch Modlechoose;
        private Sunny.UI.UITextBox showFomat;
        private Sunny.UI.UIListBox listBox1;
        private Sunny.UI.UIButton button1;
        private Sunny.UI.UIButton Button2;
        private Sunny.UI.UIDataGridView PDFdata;
        private CheckBox checkBox1;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private Sunny.UI.UIComboBox FileClass;
        private Label label6;
        private Sunny.UI.UIComboBox ModeName;
        private Label label7;
        private Sunny.UI.UIButton LoadTemplate;
    }
}
