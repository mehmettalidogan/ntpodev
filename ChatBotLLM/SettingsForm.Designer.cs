namespace ChatBotLLM
{
    partial class SettingsForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkOnlineMode;
        private System.Windows.Forms.Label lblApiKey;
        private System.Windows.Forms.TextBox txtApiKey;
        private System.Windows.Forms.LinkLabel linkGetApiKey;
        private System.Windows.Forms.Label lblModel;
        private System.Windows.Forms.ComboBox cmbModel;
        private System.Windows.Forms.Label lblSystemPrompt;
        private System.Windows.Forms.TextBox txtSystemPrompt;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblWarning;
        
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkOnlineMode = new System.Windows.Forms.CheckBox();
            this.lblApiKey = new System.Windows.Forms.Label();
            this.txtApiKey = new System.Windows.Forms.TextBox();
            this.linkGetApiKey = new System.Windows.Forms.LinkLabel();
            this.lblModel = new System.Windows.Forms.Label();
            this.cmbModel = new System.Windows.Forms.ComboBox();
            this.lblSystemPrompt = new System.Windows.Forms.Label();
            this.txtSystemPrompt = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblWarning = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            
            // lblTitle
            this.lblTitle.AutoSize = false;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(12, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(560, 35);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "⚙️ Ayarlar - ChatBot Yapılandırması";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            
            // groupBox1
            this.groupBox1.Controls.Add(this.chkOnlineMode);
            this.groupBox1.Controls.Add(this.lblApiKey);
            this.groupBox1.Controls.Add(this.txtApiKey);
            this.groupBox1.Controls.Add(this.linkGetApiKey);
            this.groupBox1.Controls.Add(this.lblModel);
            this.groupBox1.Controls.Add(this.cmbModel);
            this.groupBox1.Controls.Add(this.lblSystemPrompt);
            this.groupBox1.Controls.Add(this.txtSystemPrompt);
            this.groupBox1.Controls.Add(this.lblWarning);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.groupBox1.Location = new System.Drawing.Point(12, 55);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(560, 380);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "AI Ayarları";
            
            // chkOnlineMode
            this.chkOnlineMode.AutoSize = true;
            this.chkOnlineMode.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.chkOnlineMode.Location = new System.Drawing.Point(20, 30);
            this.chkOnlineMode.Name = "chkOnlineMode";
            this.chkOnlineMode.Size = new System.Drawing.Size(280, 23);
            this.chkOnlineMode.TabIndex = 0;
            this.chkOnlineMode.Text = "🌐 Online Mod (Gerçek OpenAI API)";
            this.chkOnlineMode.CheckedChanged += new System.EventHandler(this.chkOnlineMode_CheckedChanged);
            
            // lblApiKey
            this.lblApiKey.AutoSize = true;
            this.lblApiKey.Location = new System.Drawing.Point(20, 65);
            this.lblApiKey.Name = "lblApiKey";
            this.lblApiKey.Size = new System.Drawing.Size(100, 15);
            this.lblApiKey.TabIndex = 1;
            this.lblApiKey.Text = "OpenAI API Key:";
            
            // txtApiKey
            this.txtApiKey.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtApiKey.Location = new System.Drawing.Point(20, 85);
            this.txtApiKey.Name = "txtApiKey";
            this.txtApiKey.PasswordChar = '●';
            this.txtApiKey.Size = new System.Drawing.Size(520, 22);
            this.txtApiKey.TabIndex = 2;
            
            // linkGetApiKey
            this.linkGetApiKey.AutoSize = true;
            this.linkGetApiKey.Location = new System.Drawing.Point(20, 115);
            this.linkGetApiKey.Name = "linkGetApiKey";
            this.linkGetApiKey.Size = new System.Drawing.Size(180, 15);
            this.linkGetApiKey.TabIndex = 3;
            this.linkGetApiKey.TabStop = true;
            this.linkGetApiKey.Text = "🔑 API Key nasıl alınır? (Tıkla)";
            this.linkGetApiKey.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkGetApiKey_LinkClicked);
            
            // lblModel
            this.lblModel.AutoSize = true;
            this.lblModel.Location = new System.Drawing.Point(20, 145);
            this.lblModel.Name = "lblModel";
            this.lblModel.Size = new System.Drawing.Size(70, 15);
            this.lblModel.TabIndex = 4;
            this.lblModel.Text = "AI Modeli:";
            
            // cmbModel
            this.cmbModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbModel.FormattingEnabled = true;
            this.cmbModel.Items.AddRange(new object[] {
                "gpt-3.5-turbo",
                "gpt-4",
                "gpt-4-turbo-preview"
            });
            this.cmbModel.Location = new System.Drawing.Point(20, 165);
            this.cmbModel.Name = "cmbModel";
            this.cmbModel.Size = new System.Drawing.Size(250, 23);
            this.cmbModel.TabIndex = 5;
            this.cmbModel.SelectedIndex = 0;
            
            // lblSystemPrompt
            this.lblSystemPrompt.AutoSize = true;
            this.lblSystemPrompt.Location = new System.Drawing.Point(20, 200);
            this.lblSystemPrompt.Name = "lblSystemPrompt";
            this.lblSystemPrompt.Size = new System.Drawing.Size(320, 15);
            this.lblSystemPrompt.TabIndex = 6;
            this.lblSystemPrompt.Text = "Sistem Promptu (Bot'un kişiliğini belirler):";
            
            // txtSystemPrompt
            this.txtSystemPrompt.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSystemPrompt.Location = new System.Drawing.Point(20, 220);
            this.txtSystemPrompt.Multiline = true;
            this.txtSystemPrompt.Name = "txtSystemPrompt";
            this.txtSystemPrompt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSystemPrompt.Size = new System.Drawing.Size(520, 80);
            this.txtSystemPrompt.TabIndex = 7;
            this.txtSystemPrompt.Text = "Sen yardımsever bir AI asistanısın. Türkçe konuşuyorsun ve programlama, teknoloji konularında uzmansın.";
            
            // lblWarning
            this.lblWarning.AutoSize = false;
            this.lblWarning.BackColor = System.Drawing.Color.FromArgb(255, 243, 205);
            this.lblWarning.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblWarning.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblWarning.ForeColor = System.Drawing.Color.FromArgb(133, 100, 4);
            this.lblWarning.Location = new System.Drawing.Point(20, 315);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Padding = new System.Windows.Forms.Padding(8);
            this.lblWarning.Size = new System.Drawing.Size(520, 50);
            this.lblWarning.TabIndex = 8;
            this.lblWarning.Text = "⚠️ Offline mod seçildi. Basit pattern matching ile çalışacak.\nGerçek AI için Online mod'u aktif edin.";
            this.lblWarning.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblWarning.Visible = false;
            
            // btnSave
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(392, 445);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 40);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "💾 Kaydet";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            
            // btnCancel
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(189, 195, 199);
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(488, 445);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(84, 40);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "İptal";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            
            // SettingsForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);
            this.ClientSize = new System.Drawing.Size(584, 497);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ChatBot Ayarları";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}


