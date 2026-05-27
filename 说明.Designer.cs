namespace 发票
{
    partial class 说明
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
            uiListBox1 = new Sunny.UI.UIListBox();
            uiListBox2 = new Sunny.UI.UIListBox();
            SuspendLayout();
            // 
            // uiListBox1
            // 
            uiListBox1.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiListBox1.HoverColor = Color.FromArgb(155, 200, 255);
            uiListBox1.Items.AddRange(new object[] { "软件功能：", "1识别发票中需要的字符", "2将识别到的字符导入表格中", "3将PDF发票按照自定义格式重命名", "", "字符格式定义：", "1字符格式按照 下划线（“_”）分割填入表格中", "2在大括号中填入对于模板的编号：{0}", "3识别完成后会组成对应的格式", "", "识别方式：", "PDF：", "1提取PDF中所有的字符 ", "2判断是否包含类型中的字符进行分类", "3将PDF转成图片更具模板匹配进行裁图", "4裁剪好的图片上传百度智能云进行识别", "5识别完成后进行将字符按照格式拼接", "XML：", "1读取XML文本判断是否包含类型中的字符进行分类", "2根据模板节点读取字符", "3将字符按照格式拼接" });
            uiListBox1.ItemSelectForeColor = Color.White;
            uiListBox1.Location = new Point(4, 40);
            uiListBox1.Margin = new Padding(4, 5, 4, 5);
            uiListBox1.MinimumSize = new Size(1, 1);
            uiListBox1.Name = "uiListBox1";
            uiListBox1.Padding = new Padding(2);
            uiListBox1.ShowText = false;
            uiListBox1.Size = new Size(564, 559);
            uiListBox1.TabIndex = 0;
            uiListBox1.Text = "uiListBox1";
            // 
            // uiListBox2
            // 
            uiListBox2.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            uiListBox2.HoverColor = Color.FromArgb(155, 200, 255);
            uiListBox2.Items.AddRange(new object[] { "使用说明：", "1先加载XML文件或者PDF文件（不支持混合）", "2编辑发票类型填入对应的类型", "3选择好发票类型和对应的发票", "4制作模板并保存", "5选择好文件类型和模板类型加载模板", "6填写对应类型的字符格式", "7识别并导出", "", "注意：", "1切换发票类型需要手动重新加载", "2字符格式的编号值不能超过对应模板的数量", "3模板类型添加需要在模板类型选择框是空的时候去添加（模板类型框可以手动删除文本）", "", "", "", "", "下载地址：", "https://github.com/sleep-jin/Reimbursement-form" });
            uiListBox2.ItemSelectForeColor = Color.White;
            uiListBox2.Location = new Point(576, 40);
            uiListBox2.Margin = new Padding(4, 5, 4, 5);
            uiListBox2.MinimumSize = new Size(1, 1);
            uiListBox2.Name = "uiListBox2";
            uiListBox2.Padding = new Padding(2);
            uiListBox2.ShowText = false;
            uiListBox2.Size = new Size(845, 559);
            uiListBox2.TabIndex = 2;
            uiListBox2.Text = "uiListBox2";
            // 
            // 说明
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1426, 610);
            Controls.Add(uiListBox2);
            Controls.Add(uiListBox1);
            Name = "说明";
            Text = "说明";
            ZoomScaleRect = new Rectangle(22, 22, 800, 450);
            ResumeLayout(false);
        }

        #endregion

        private Sunny.UI.UIListBox uiListBox1;
        private Sunny.UI.UIListBox uiListBox2;
    }
}