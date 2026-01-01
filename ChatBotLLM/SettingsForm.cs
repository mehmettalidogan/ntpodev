using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ChatBotLLM
{
    public partial class SettingsForm : Form
    {
        private const string SETTINGS_FILE = "settings.txt";
        
        public string ApiKey { get; private set; }
        public bool UseOnlineMode { get; private set; }
        public string SelectedModel { get; private set; }
        public string SystemPrompt { get; private set; }
        
        public SettingsForm()
        {
            InitializeComponent();
            LoadSettings();
        }
        
        private void LoadSettings()
        {
            try
            {
                if (File.Exists(SETTINGS_FILE))
                {
                    string[] lines = File.ReadAllLines(SETTINGS_FILE);
                    if (lines.Length > 0)
                        txtApiKey.Text = lines[0];
                    if (lines.Length > 1)
                        chkOnlineMode.Checked = lines[1] == "True";
                    if (lines.Length > 2)
                        cmbModel.SelectedItem = lines[2];
                    if (lines.Length > 3)
                        txtSystemPrompt.Text = lines[3];
                }
            }
            catch
            {
                // Ignore loading errors
            }
        }
        
        private void SaveSettings()
        {
            try
            {
                File.WriteAllLines(SETTINGS_FILE, new[]
                {
                    txtApiKey.Text,
                    chkOnlineMode.Checked.ToString(),
                    cmbModel.SelectedItem?.ToString() ?? "gpt-3.5-turbo",
                    txtSystemPrompt.Text
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ayarlar kaydedilemedi: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (chkOnlineMode.Checked && string.IsNullOrWhiteSpace(txtApiKey.Text))
            {
                MessageBox.Show("Online mod için API Key gereklidir!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            ApiKey = txtApiKey.Text.Trim();
            UseOnlineMode = chkOnlineMode.Checked;
            SelectedModel = cmbModel.SelectedItem?.ToString() ?? "gpt-3.5-turbo";
            SystemPrompt = string.IsNullOrWhiteSpace(txtSystemPrompt.Text) 
                ? "Sen yardımsever bir AI asistanısın. Türkçe konuşuyorsun."
                : txtSystemPrompt.Text;
            
            SaveSettings();
            DialogResult = DialogResult.OK;
            Close();
        }
        
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        
        private void chkOnlineMode_CheckedChanged(object sender, EventArgs e)
        {
            txtApiKey.Enabled = chkOnlineMode.Checked;
            cmbModel.Enabled = chkOnlineMode.Checked;
            lblWarning.Visible = !chkOnlineMode.Checked;
        }
        
        private void linkGetApiKey_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://platform.openai.com/api-keys");
        }
    }
}


