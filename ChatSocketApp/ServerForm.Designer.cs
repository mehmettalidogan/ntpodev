namespace ChatSocketApp
{
    partial class ServerForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.Label lblMaxClients;
        private System.Windows.Forms.Label lblClientCount;
        private System.Windows.Forms.NumericUpDown nudPort;
        private System.Windows.Forms.NumericUpDown nudMaxClients;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.ListBox lstClients;
        private System.Windows.Forms.GroupBox groupBoxSettings;
        private System.Windows.Forms.GroupBox groupBoxClients;
        private System.Windows.Forms.GroupBox groupBoxLog;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblPort = new System.Windows.Forms.Label();
            this.lblMaxClients = new System.Windows.Forms.Label();
            this.lblClientCount = new System.Windows.Forms.Label();
            this.nudPort = new System.Windows.Forms.NumericUpDown();
            this.nudMaxClients = new System.Windows.Forms.NumericUpDown();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.lstClients = new System.Windows.Forms.ListBox();
            this.groupBoxSettings = new System.Windows.Forms.GroupBox();
            this.groupBoxClients = new System.Windows.Forms.GroupBox();
            this.groupBoxLog = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxClients)).BeginInit();
            this.groupBoxSettings.SuspendLayout();
            this.groupBoxClients.SuspendLayout();
            this.groupBoxLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = false;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(12, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(960, 40);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Chat Server";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBoxSettings
            // 
            this.groupBoxSettings.Controls.Add(this.lblPort);
            this.groupBoxSettings.Controls.Add(this.nudPort);
            this.groupBoxSettings.Controls.Add(this.lblMaxClients);
            this.groupBoxSettings.Controls.Add(this.nudMaxClients);
            this.groupBoxSettings.Controls.Add(this.btnStart);
            this.groupBoxSettings.Controls.Add(this.btnStop);
            this.groupBoxSettings.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.groupBoxSettings.Location = new System.Drawing.Point(12, 60);
            this.groupBoxSettings.Name = "groupBoxSettings";
            this.groupBoxSettings.Size = new System.Drawing.Size(300, 200);
            this.groupBoxSettings.TabIndex = 1;
            this.groupBoxSettings.TabStop = false;
            this.groupBoxSettings.Text = "Ayarlar";
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(15, 30);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(35, 15);
            this.lblPort.TabIndex = 0;
            this.lblPort.Text = "Port:";
            // 
            // nudPort
            // 
            this.nudPort.Location = new System.Drawing.Point(150, 28);
            this.nudPort.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            this.nudPort.Minimum = new decimal(new int[] { 1024, 0, 0, 0 });
            this.nudPort.Name = "nudPort";
            this.nudPort.Size = new System.Drawing.Size(120, 23);
            this.nudPort.TabIndex = 1;
            this.nudPort.Value = new decimal(new int[] { 5000, 0, 0, 0 });
            // 
            // lblMaxClients
            // 
            this.lblMaxClients.AutoSize = true;
            this.lblMaxClients.Location = new System.Drawing.Point(15, 65);
            this.lblMaxClients.Name = "lblMaxClients";
            this.lblMaxClients.Size = new System.Drawing.Size(104, 15);
            this.lblMaxClients.TabIndex = 2;
            this.lblMaxClients.Text = "Maksimum Client:";
            // 
            // nudMaxClients
            // 
            this.nudMaxClients.Location = new System.Drawing.Point(150, 63);
            this.nudMaxClients.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
            this.nudMaxClients.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudMaxClients.Name = "nudMaxClients";
            this.nudMaxClients.Size = new System.Drawing.Size(120, 23);
            this.nudMaxClients.TabIndex = 3;
            this.nudMaxClients.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnStart.ForeColor = System.Drawing.Color.White;
            this.btnStart.Location = new System.Drawing.Point(15, 110);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(255, 35);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "Server Başlat";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnStop.Enabled = false;
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnStop.ForeColor = System.Drawing.Color.White;
            this.btnStop.Location = new System.Drawing.Point(15, 151);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(255, 35);
            this.btnStop.TabIndex = 5;
            this.btnStop.Text = "Server Durdur";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // groupBoxClients
            // 
            this.groupBoxClients.Controls.Add(this.lblClientCount);
            this.groupBoxClients.Controls.Add(this.lstClients);
            this.groupBoxClients.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.groupBoxClients.Location = new System.Drawing.Point(330, 60);
            this.groupBoxClients.Name = "groupBoxClients";
            this.groupBoxClients.Size = new System.Drawing.Size(200, 500);
            this.groupBoxClients.TabIndex = 2;
            this.groupBoxClients.TabStop = false;
            this.groupBoxClients.Text = "Bağlı Clientlar";
            // 
            // lblClientCount
            // 
            this.lblClientCount.AutoSize = true;
            this.lblClientCount.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblClientCount.Location = new System.Drawing.Point(15, 25);
            this.lblClientCount.Name = "lblClientCount";
            this.lblClientCount.Size = new System.Drawing.Size(100, 15);
            this.lblClientCount.TabIndex = 0;
            this.lblClientCount.Text = "Bağlı Client: 0";
            // 
            // lstClients
            // 
            this.lstClients.FormattingEnabled = true;
            this.lstClients.ItemHeight = 15;
            this.lstClients.Location = new System.Drawing.Point(15, 50);
            this.lstClients.Name = "lstClients";
            this.lstClients.Size = new System.Drawing.Size(170, 424);
            this.lstClients.TabIndex = 1;
            // 
            // groupBoxLog
            // 
            this.groupBoxLog.Controls.Add(this.rtbLog);
            this.groupBoxLog.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.groupBoxLog.Location = new System.Drawing.Point(550, 60);
            this.groupBoxLog.Name = "groupBoxLog";
            this.groupBoxLog.Size = new System.Drawing.Size(422, 500);
            this.groupBoxLog.TabIndex = 3;
            this.groupBoxLog.TabStop = false;
            this.groupBoxLog.Text = "Log";
            // 
            // rtbLog
            // 
            this.rtbLog.BackColor = System.Drawing.Color.White;
            this.rtbLog.Font = new System.Drawing.Font("Consolas", 9F);
            this.rtbLog.Location = new System.Drawing.Point(15, 25);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.ReadOnly = true;
            this.rtbLog.Size = new System.Drawing.Size(392, 455);
            this.rtbLog.TabIndex = 0;
            this.rtbLog.Text = "";
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);
            this.ClientSize = new System.Drawing.Size(984, 571);
            this.Controls.Add(this.groupBoxLog);
            this.Controls.Add(this.groupBoxClients);
            this.Controls.Add(this.groupBoxSettings);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ServerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chat Server - NTP Projesi";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServerForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.nudPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxClients)).EndInit();
            this.groupBoxSettings.ResumeLayout(false);
            this.groupBoxSettings.PerformLayout();
            this.groupBoxClients.ResumeLayout(false);
            this.groupBoxClients.PerformLayout();
            this.groupBoxLog.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}

