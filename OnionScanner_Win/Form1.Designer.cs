namespace OnionScanner_Win {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.scan = new System.Windows.Forms.Button();
            this.file_radio = new System.Windows.Forms.RadioButton();
            this.yatd_radio = new System.Windows.Forms.RadioButton();
            this.output = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.status_label = new System.Windows.Forms.ToolStripStatusLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.verbose = new System.Windows.Forms.CheckBox();
            this.saveBad = new System.Windows.Forms.CheckBox();
            this.desc = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.proxyUrl_text = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.proxyPort_text = new System.Windows.Forms.TextBox();
            this.useragent_text = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.progressbar = new System.Windows.Forms.ProgressBar();
            this.label7 = new System.Windows.Forms.Label();
            this.numlinks_text = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.clear = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // scan
            // 
            this.scan.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.scan.Location = new System.Drawing.Point(94, 207);
            this.scan.Name = "scan";
            this.scan.Size = new System.Drawing.Size(75, 23);
            this.scan.TabIndex = 0;
            this.scan.Text = "Scan";
            this.scan.UseVisualStyleBackColor = true;
            this.scan.Click += new System.EventHandler(this.scan_Click);
            // 
            // file_radio
            // 
            this.file_radio.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.file_radio.AutoSize = true;
            this.file_radio.Location = new System.Drawing.Point(87, 13);
            this.file_radio.Name = "file_radio";
            this.file_radio.Size = new System.Drawing.Size(41, 17);
            this.file_radio.TabIndex = 1;
            this.file_radio.Text = "File";
            this.file_radio.UseVisualStyleBackColor = true;
            this.file_radio.CheckedChanged += new System.EventHandler(this.file_radio_CheckedChanged);
            // 
            // yatd_radio
            // 
            this.yatd_radio.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.yatd_radio.AutoSize = true;
            this.yatd_radio.Checked = true;
            this.yatd_radio.Location = new System.Drawing.Point(86, 36);
            this.yatd_radio.Name = "yatd_radio";
            this.yatd_radio.Size = new System.Drawing.Size(54, 17);
            this.yatd_radio.TabIndex = 2;
            this.yatd_radio.TabStop = true;
            this.yatd_radio.Text = "YATD";
            this.yatd_radio.UseVisualStyleBackColor = true;
            // 
            // output
            // 
            this.output.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.output.Location = new System.Drawing.Point(9, 236);
            this.output.Multiline = true;
            this.output.Name = "output";
            this.output.ReadOnly = true;
            this.output.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.output.Size = new System.Drawing.Size(259, 98);
            this.output.TabIndex = 3;
            this.output.TextChanged += new System.EventHandler(this.output_TextChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status_label});
            this.statusStrip1.Location = new System.Drawing.Point(0, 366);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(274, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // status_label
            // 
            this.status_label.Name = "status_label";
            this.status_label.Size = new System.Drawing.Size(121, 17);
            this.status_label.Text = "Waiting on user input";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "What to scan";
            // 
            // verbose
            // 
            this.verbose.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.verbose.AutoSize = true;
            this.verbose.Location = new System.Drawing.Point(189, 19);
            this.verbose.Name = "verbose";
            this.verbose.Size = new System.Drawing.Size(65, 17);
            this.verbose.TabIndex = 6;
            this.verbose.Text = "Verbose";
            this.verbose.UseVisualStyleBackColor = true;
            // 
            // saveBad
            // 
            this.saveBad.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.saveBad.AutoSize = true;
            this.saveBad.Location = new System.Drawing.Point(189, 43);
            this.saveBad.Name = "saveBad";
            this.saveBad.Size = new System.Drawing.Size(73, 17);
            this.saveBad.TabIndex = 7;
            this.saveBad.Text = "Save Bad";
            this.saveBad.UseVisualStyleBackColor = true;
            // 
            // desc
            // 
            this.desc.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.desc.AutoSize = true;
            this.desc.Checked = true;
            this.desc.CheckState = System.Windows.Forms.CheckState.Checked;
            this.desc.Location = new System.Drawing.Point(189, 67);
            this.desc.Name = "desc";
            this.desc.Size = new System.Drawing.Size(73, 17);
            this.desc.TabIndex = 8;
            this.desc.Text = "Add Desc";
            this.desc.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(165, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Advanced Settings";
            // 
            // proxyUrl_text
            // 
            this.proxyUrl_text.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.proxyUrl_text.Location = new System.Drawing.Point(66, 82);
            this.proxyUrl_text.MaxLength = 50;
            this.proxyUrl_text.Name = "proxyUrl_text";
            this.proxyUrl_text.Size = new System.Drawing.Size(100, 20);
            this.proxyUrl_text.TabIndex = 10;
            this.proxyUrl_text.Text = "127.0.0.1";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Proxy Url";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Proxy Port";
            // 
            // proxyPort_text
            // 
            this.proxyPort_text.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.proxyPort_text.Location = new System.Drawing.Point(66, 111);
            this.proxyPort_text.Name = "proxyPort_text";
            this.proxyPort_text.Size = new System.Drawing.Size(100, 20);
            this.proxyPort_text.TabIndex = 13;
            this.proxyPort_text.Text = "8118";
            // 
            // useragent_text
            // 
            this.useragent_text.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.useragent_text.Location = new System.Drawing.Point(66, 137);
            this.useragent_text.Name = "useragent_text";
            this.useragent_text.Size = new System.Drawing.Size(100, 20);
            this.useragent_text.TabIndex = 15;
            this.useragent_text.Text = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.8.0.4) Gecko/20060508 Firefo" +
    "x/1.5.0.4";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 140);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "UserAgent";
            // 
            // progressbar
            // 
            this.progressbar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.progressbar.Location = new System.Drawing.Point(173, 341);
            this.progressbar.Name = "progressbar";
            this.progressbar.Size = new System.Drawing.Size(100, 23);
            this.progressbar.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(130, 346);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Status";
            // 
            // numlinks_text
            // 
            this.numlinks_text.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.numlinks_text.Location = new System.Drawing.Point(143, 163);
            this.numlinks_text.MaxLength = 5;
            this.numlinks_text.Name = "numlinks_text";
            this.numlinks_text.Size = new System.Drawing.Size(23, 20);
            this.numlinks_text.TabIndex = 19;
            this.numlinks_text.Text = "0";
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 166);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(106, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Num. of links to scan";
            // 
            // clear
            // 
            this.clear.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.clear.Location = new System.Drawing.Point(9, 335);
            this.clear.Name = "clear";
            this.clear.Size = new System.Drawing.Size(75, 23);
            this.clear.TabIndex = 21;
            this.clear.Text = "Clear";
            this.clear.UseVisualStyleBackColor = true;
            this.clear.Click += new System.EventHandler(this.clear_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(274, 388);
            this.Controls.Add(this.clear);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.numlinks_text);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.progressbar);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.useragent_text);
            this.Controls.Add(this.proxyPort_text);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.proxyUrl_text);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.desc);
            this.Controls.Add(this.saveBad);
            this.Controls.Add(this.verbose);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.output);
            this.Controls.Add(this.yatd_radio);
            this.Controls.Add(this.file_radio);
            this.Controls.Add(this.scan);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(300, 450);
            this.MinimumSize = new System.Drawing.Size(290, 426);
            this.Name = "Form1";
            this.Text = "Onion Scanner";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button scan;
        private System.Windows.Forms.RadioButton file_radio;
        private System.Windows.Forms.RadioButton yatd_radio;
        private System.Windows.Forms.TextBox output;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel status_label;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox verbose;
        private System.Windows.Forms.CheckBox saveBad;
        private System.Windows.Forms.CheckBox desc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox proxyUrl_text;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox proxyPort_text;
        private System.Windows.Forms.TextBox useragent_text;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ProgressBar progressbar;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox numlinks_text;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button clear;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}

