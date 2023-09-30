namespace TheKingdomProject
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
            components = new System.ComponentModel.Container();
            UpdateGuiInfo_Tm = new System.Windows.Forms.Timer(components);
            UserControlGuiPopulatin_Tm = new System.Windows.Forms.Timer(components);
            Balances_Gb = new GroupBox();
            total_assets_in_try_Lb = new Label();
            total_usdt_values_Lb = new Label();
            free_try_values_Lb = new Label();
            total_try_values_Lb = new Label();
            label4 = new Label();
            label3 = new Label();
            label1 = new Label();
            CoinTryBinance_Lb = new Label();
            GlobalConfiguration_Gb = new GroupBox();
            groupBox2 = new GroupBox();
            RobotCurrencyUsdt_Rb = new RadioButton();
            RobotCurrencyTry_Rb = new RadioButton();
            groupBox1 = new GroupBox();
            RobotStatusOff_Rb = new RadioButton();
            RobotStatusOn_Rb = new RadioButton();
            MaxAsymmetryValue_Tb = new TextBox();
            MaxAsymmetry_Lb = new Label();
            Balances_Gb.SuspendLayout();
            GlobalConfiguration_Gb.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // UpdateGuiInfo_Tm
            // 
            UpdateGuiInfo_Tm.Interval = 1000;
            UpdateGuiInfo_Tm.Tick += UpdateGuiInfo_Tm_Tick;
            // 
            // UserControlGuiPopulatin_Tm
            // 
            UserControlGuiPopulatin_Tm.Tick += UserControlGuiPopulatin_Tm_Tick;
            // 
            // Balances_Gb
            // 
            Balances_Gb.BackColor = SystemColors.Control;
            Balances_Gb.Controls.Add(total_assets_in_try_Lb);
            Balances_Gb.Controls.Add(total_usdt_values_Lb);
            Balances_Gb.Controls.Add(free_try_values_Lb);
            Balances_Gb.Controls.Add(total_try_values_Lb);
            Balances_Gb.Controls.Add(label4);
            Balances_Gb.Controls.Add(label3);
            Balances_Gb.Controls.Add(label1);
            Balances_Gb.Controls.Add(CoinTryBinance_Lb);
            Balances_Gb.Location = new Point(0, 5);
            Balances_Gb.Name = "Balances_Gb";
            Balances_Gb.Size = new Size(950, 80);
            Balances_Gb.TabIndex = 0;
            Balances_Gb.TabStop = false;
            Balances_Gb.Text = "TRY and USDT Balances";
            // 
            // total_assets_in_try_Lb
            // 
            total_assets_in_try_Lb.BackColor = Color.LightSkyBlue;
            total_assets_in_try_Lb.BorderStyle = BorderStyle.FixedSingle;
            total_assets_in_try_Lb.Font = new Font("Gadugi", 21.75F, FontStyle.Bold, GraphicsUnit.Point);
            total_assets_in_try_Lb.Location = new Point(742, 12);
            total_assets_in_try_Lb.Name = "total_assets_in_try_Lb";
            total_assets_in_try_Lb.RightToLeft = RightToLeft.No;
            total_assets_in_try_Lb.Size = new Size(177, 64);
            total_assets_in_try_Lb.TabIndex = 54;
            total_assets_in_try_Lb.Text = "325789.63";
            total_assets_in_try_Lb.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // total_usdt_values_Lb
            // 
            total_usdt_values_Lb.BackColor = Color.LightSkyBlue;
            total_usdt_values_Lb.BorderStyle = BorderStyle.FixedSingle;
            total_usdt_values_Lb.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            total_usdt_values_Lb.Location = new Point(436, 19);
            total_usdt_values_Lb.Name = "total_usdt_values_Lb";
            total_usdt_values_Lb.RightToLeft = RightToLeft.No;
            total_usdt_values_Lb.Size = new Size(100, 25);
            total_usdt_values_Lb.TabIndex = 52;
            total_usdt_values_Lb.Text = "0.00";
            total_usdt_values_Lb.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // free_try_values_Lb
            // 
            free_try_values_Lb.BackColor = Color.LightSkyBlue;
            free_try_values_Lb.BorderStyle = BorderStyle.FixedSingle;
            free_try_values_Lb.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            free_try_values_Lb.Location = new Point(164, 48);
            free_try_values_Lb.Name = "free_try_values_Lb";
            free_try_values_Lb.RightToLeft = RightToLeft.No;
            free_try_values_Lb.Size = new Size(100, 25);
            free_try_values_Lb.TabIndex = 51;
            free_try_values_Lb.Text = "0.00";
            free_try_values_Lb.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // total_try_values_Lb
            // 
            total_try_values_Lb.BackColor = Color.LightSkyBlue;
            total_try_values_Lb.BorderStyle = BorderStyle.FixedSingle;
            total_try_values_Lb.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            total_try_values_Lb.Location = new Point(164, 19);
            total_try_values_Lb.Name = "total_try_values_Lb";
            total_try_values_Lb.RightToLeft = RightToLeft.No;
            total_try_values_Lb.Size = new Size(100, 25);
            total_try_values_Lb.TabIndex = 50;
            total_try_values_Lb.Text = "0.00";
            total_try_values_Lb.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            label4.BackColor = Color.FromArgb(224, 224, 224);
            label4.BorderStyle = BorderStyle.FixedSingle;
            label4.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(574, 12);
            label4.Name = "label4";
            label4.RightToLeft = RightToLeft.No;
            label4.Size = new Size(162, 65);
            label4.TabIndex = 48;
            label4.Text = "Total Assets TRY Value:";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            label4.Click += label4_Click;
            // 
            // label3
            // 
            label3.BackColor = Color.FromArgb(224, 224, 224);
            label3.BorderStyle = BorderStyle.FixedSingle;
            label3.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(280, 19);
            label3.Name = "label3";
            label3.RightToLeft = RightToLeft.No;
            label3.Size = new Size(150, 25);
            label3.TabIndex = 45;
            label3.Text = "Total USDT Balance";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            label1.BackColor = Color.FromArgb(224, 224, 224);
            label1.BorderStyle = BorderStyle.FixedSingle;
            label1.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(12, 48);
            label1.Name = "label1";
            label1.RightToLeft = RightToLeft.No;
            label1.Size = new Size(150, 23);
            label1.TabIndex = 43;
            label1.Text = "Free Currency Balance";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // CoinTryBinance_Lb
            // 
            CoinTryBinance_Lb.BackColor = Color.FromArgb(224, 224, 224);
            CoinTryBinance_Lb.BorderStyle = BorderStyle.FixedSingle;
            CoinTryBinance_Lb.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            CoinTryBinance_Lb.Location = new Point(12, 19);
            CoinTryBinance_Lb.Name = "CoinTryBinance_Lb";
            CoinTryBinance_Lb.RightToLeft = RightToLeft.No;
            CoinTryBinance_Lb.Size = new Size(150, 25);
            CoinTryBinance_Lb.TabIndex = 41;
            CoinTryBinance_Lb.Text = "Total Currency Balance";
            CoinTryBinance_Lb.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // GlobalConfiguration_Gb
            // 
            GlobalConfiguration_Gb.BackColor = SystemColors.Control;
            GlobalConfiguration_Gb.Controls.Add(groupBox2);
            GlobalConfiguration_Gb.Controls.Add(groupBox1);
            GlobalConfiguration_Gb.Controls.Add(MaxAsymmetryValue_Tb);
            GlobalConfiguration_Gb.Controls.Add(MaxAsymmetry_Lb);
            GlobalConfiguration_Gb.Location = new Point(960, 5);
            GlobalConfiguration_Gb.Name = "GlobalConfiguration_Gb";
            GlobalConfiguration_Gb.Size = new Size(950, 80);
            GlobalConfiguration_Gb.TabIndex = 1;
            GlobalConfiguration_Gb.TabStop = false;
            GlobalConfiguration_Gb.Text = "Global Configuration";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(RobotCurrencyUsdt_Rb);
            groupBox2.Controls.Add(RobotCurrencyTry_Rb);
            groupBox2.Enabled = false;
            groupBox2.Location = new Point(372, 21);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(165, 52);
            groupBox2.TabIndex = 26;
            groupBox2.TabStop = false;
            groupBox2.Text = "Robot Currency";
            // 
            // RobotCurrencyUsdt_Rb
            // 
            RobotCurrencyUsdt_Rb.AutoSize = true;
            RobotCurrencyUsdt_Rb.Location = new Point(91, 22);
            RobotCurrencyUsdt_Rb.Name = "RobotCurrencyUsdt_Rb";
            RobotCurrencyUsdt_Rb.Size = new Size(52, 19);
            RobotCurrencyUsdt_Rb.TabIndex = 1;
            RobotCurrencyUsdt_Rb.Text = "USDT";
            RobotCurrencyUsdt_Rb.UseVisualStyleBackColor = true;
            // 
            // RobotCurrencyTry_Rb
            // 
            RobotCurrencyTry_Rb.AutoSize = true;
            RobotCurrencyTry_Rb.Checked = true;
            RobotCurrencyTry_Rb.Location = new Point(16, 22);
            RobotCurrencyTry_Rb.Name = "RobotCurrencyTry_Rb";
            RobotCurrencyTry_Rb.Size = new Size(45, 19);
            RobotCurrencyTry_Rb.TabIndex = 0;
            RobotCurrencyTry_Rb.TabStop = true;
            RobotCurrencyTry_Rb.Text = "TRY";
            RobotCurrencyTry_Rb.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(RobotStatusOff_Rb);
            groupBox1.Controls.Add(RobotStatusOn_Rb);
            groupBox1.Location = new Point(18, 21);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(165, 52);
            groupBox1.TabIndex = 25;
            groupBox1.TabStop = false;
            groupBox1.Text = "Robot Status";
            // 
            // RobotStatusOff_Rb
            // 
            RobotStatusOff_Rb.AutoSize = true;
            RobotStatusOff_Rb.Location = new Point(91, 22);
            RobotStatusOff_Rb.Name = "RobotStatusOff_Rb";
            RobotStatusOff_Rb.Size = new Size(46, 19);
            RobotStatusOff_Rb.TabIndex = 1;
            RobotStatusOff_Rb.Text = "OFF";
            RobotStatusOff_Rb.UseVisualStyleBackColor = true;
            RobotStatusOff_Rb.CheckedChanged += RobotStatusOff_Rb_CheckedChanged;
            // 
            // RobotStatusOn_Rb
            // 
            RobotStatusOn_Rb.AutoSize = true;
            RobotStatusOn_Rb.Checked = true;
            RobotStatusOn_Rb.Location = new Point(16, 22);
            RobotStatusOn_Rb.Name = "RobotStatusOn_Rb";
            RobotStatusOn_Rb.Size = new Size(43, 19);
            RobotStatusOn_Rb.TabIndex = 0;
            RobotStatusOn_Rb.TabStop = true;
            RobotStatusOn_Rb.Text = "ON";
            RobotStatusOn_Rb.UseVisualStyleBackColor = true;
            RobotStatusOn_Rb.CheckedChanged += RobotStatusOn_Rb_CheckedChanged;
            // 
            // MaxAsymmetryValue_Tb
            // 
            MaxAsymmetryValue_Tb.Location = new Point(306, 31);
            MaxAsymmetryValue_Tb.Name = "MaxAsymmetryValue_Tb";
            MaxAsymmetryValue_Tb.Size = new Size(50, 23);
            MaxAsymmetryValue_Tb.TabIndex = 24;
            MaxAsymmetryValue_Tb.Text = "0.375";
            MaxAsymmetryValue_Tb.TextAlign = HorizontalAlignment.Center;
            // 
            // MaxAsymmetry_Lb
            // 
            MaxAsymmetry_Lb.BackColor = Color.PaleGreen;
            MaxAsymmetry_Lb.BorderStyle = BorderStyle.FixedSingle;
            MaxAsymmetry_Lb.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            MaxAsymmetry_Lb.Location = new Point(189, 31);
            MaxAsymmetry_Lb.Name = "MaxAsymmetry_Lb";
            MaxAsymmetry_Lb.RightToLeft = RightToLeft.No;
            MaxAsymmetry_Lb.Size = new Size(111, 23);
            MaxAsymmetry_Lb.TabIndex = 23;
            MaxAsymmetry_Lb.Text = "Max Asymmetry";
            MaxAsymmetry_Lb.TextAlign = ContentAlignment.MiddleCenter;
            MaxAsymmetry_Lb.Click += MaxAsymmetry_Lb_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(1904, 619);
            Controls.Add(GlobalConfiguration_Gb);
            Controls.Add(Balances_Gb);
            Name = "Form1";
            Text = "Kingdom Project v1.09";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            Resize += Form1_Resize;
            Balances_Gb.ResumeLayout(false);
            GlobalConfiguration_Gb.ResumeLayout(false);
            GlobalConfiguration_Gb.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Timer UpdateGuiInfo_Tm;
        private System.Windows.Forms.Timer UserControlGuiPopulatin_Tm;
        private GroupBox Balances_Gb;
        private GroupBox GlobalConfiguration_Gb;
        private Label label3;
        private Label label1;
        private Label CoinTryBinance_Lb;
        private Label label4;
        private TextBox MaxAsymmetryValue_Tb;
        private Label MaxAsymmetry_Lb;
        private Label total_try_values_Lb;
        private Label total_usdt_values_Lb;
        private Label free_try_values_Lb;
        private Label total_assets_in_try_Lb;
        private GroupBox groupBox1;
        private RadioButton RobotStatusOff_Rb;
        private RadioButton RobotStatusOn_Rb;
        private GroupBox groupBox2;
        private RadioButton RobotCurrencyUsdt_Rb;
        private RadioButton RobotCurrencyTry_Rb;
    }
}