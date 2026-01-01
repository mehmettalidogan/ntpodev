using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChatBotLLM.Models;
using ChatBotLLM.Services;

namespace ChatBotLLM
{
    public partial class ChatForm : Form
    {
        private ChatBot chatBot;
        private User currentUser;
        private string conversationId;
        private ILLMService llmService;
        private bool useOnlineMode = false;
        private string apiKey = "";
        private string selectedModel = "gpt-3.5-turbo";
        private string systemPrompt = "";

        public ChatForm()
        {
            InitializeComponent();
            LoadSettingsAndInitialize();
        }

        private void LoadSettingsAndInitialize()
        {
            // Ayarları yükle
            try
            {
                if (File.Exists("settings.txt"))
                {
                    string[] lines = File.ReadAllLines("settings.txt");
                    if (lines.Length > 0)
                        apiKey = lines[0];
                    if (lines.Length > 1)
                        useOnlineMode = lines[1] == "True";
                    if (lines.Length > 2)
                        selectedModel = lines[2];
                    if (lines.Length > 3)
                        systemPrompt = lines[3];
                }
            }
            catch { }
            
            InitializeChatBot();
            UpdateModeLabel();
        }

        private void InitializeChatBot()
        {
            try
            {
                if (useOnlineMode && !string.IsNullOrWhiteSpace(apiKey))
                {
                    llmService = new RealOpenAIService(apiKey, selectedModel);
                    if (llmService is RealOpenAIService realService && !string.IsNullOrWhiteSpace(systemPrompt))
                    {
                        realService.SystemPrompt = systemPrompt;
                    }
                }
                else
                {
                    llmService = new OpenAIService("gpt-3.5-turbo");
                }
                
                IMessageStorage storage = new InMemoryStorage();
                chatBot = new ChatBot(llmService, storage);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ChatBot başlatma hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                llmService = new OpenAIService("gpt-3.5-turbo"); // Fallback to offline
                IMessageStorage storage = new InMemoryStorage();
                chatBot = new ChatBot(llmService, storage);
                useOnlineMode = false;
            }
        }
        
        private void UpdateModeLabel()
        {
            if (useOnlineMode)
            {
                lblMode.Text = "🌐 Online - " + selectedModel;
                lblMode.BackColor = Color.FromArgb(39, 174, 96);
            }
            else
            {
                lblMode.Text = "📵 Offline Mod";
                lblMode.BackColor = Color.FromArgb(243, 156, 18);
            }
        }

        private void ChatForm_Load(object sender, EventArgs e)
        {
            string username = Microsoft.VisualBasic.Interaction.InputBox(
                "Kullanıcı adınızı girin:",
                "Giriş",
                "Kullanıcı",
                -1, -1);

            if (string.IsNullOrWhiteSpace(username))
            {
                username = "Misafir";
            }

            currentUser = new User("user_" + DateTime.Now.Ticks, username);
            conversationId = chatBot.StartConversation(currentUser);

            lblUsername.Text = "Kullanıcı: " + currentUser.Name;
            AppendBotMessage("Merhaba " + currentUser.Name + "! Size nasıl yardımcı olabilirim?");
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            await SendMessage();
        }

        private async void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !e.Shift)
            {
                e.SuppressKeyPress = true;
                await SendMessage();
            }
        }

        private async Task SendMessage()
        {
            string message = txtInput.Text.Trim();

            if (string.IsNullOrWhiteSpace(message))
                return;

            AppendUserMessage(message);
            txtInput.Clear();
            txtInput.Enabled = false;
            btnSend.Enabled = false;

            try
            {
                string response = await chatBot.SendMessageAsync(conversationId, message);
                AppendBotMessage(response);
            }
            catch (Exception ex)
            {
                AppendSystemMessage("Hata: " + ex.Message);
            }
            finally
            {
                txtInput.Enabled = true;
                btnSend.Enabled = true;
                txtInput.Focus();
            }
        }

        private void AppendUserMessage(string message)
        {
            rtbChat.SelectionColor = Color.Blue;
            rtbChat.SelectionFont = new Font(rtbChat.Font, FontStyle.Bold);
            rtbChat.AppendText(currentUser.Name + ": ");
            rtbChat.SelectionColor = Color.Black;
            rtbChat.SelectionFont = new Font(rtbChat.Font, FontStyle.Regular);
            rtbChat.AppendText(message + "\r\n\r\n");
            rtbChat.ScrollToCaret();
        }

        private void AppendBotMessage(string message)
        {
            rtbChat.SelectionColor = Color.Green;
            rtbChat.SelectionFont = new Font(rtbChat.Font, FontStyle.Bold);
            rtbChat.AppendText("Bot: ");
            rtbChat.SelectionColor = Color.Black;
            rtbChat.SelectionFont = new Font(rtbChat.Font, FontStyle.Regular);
            rtbChat.AppendText(message + "\r\n\r\n");
            rtbChat.ScrollToCaret();
        }

        private void AppendSystemMessage(string message)
        {
            rtbChat.SelectionColor = Color.Red;
            rtbChat.SelectionFont = new Font(rtbChat.Font, FontStyle.Italic);
            rtbChat.AppendText("[Sistem] " + message + "\r\n\r\n");
            rtbChat.SelectionColor = Color.Black;
            rtbChat.ScrollToCaret();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            rtbChat.Clear();
            chatBot.ClearConversation(conversationId);
            AppendBotMessage("Konuşma geçmişi temizlendi.");
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            var conversation = chatBot.GetConversation(conversationId);
            if (conversation != null && conversation.Messages.Count > 0)
            {
                string history = "=== KONUŞMA GEÇMİŞİ ===\r\n\r\n";
                foreach (var msg in conversation.Messages)
                {
                    history += string.Format("[{0}] {1}: {2}\r\n", 
                        msg.Timestamp.ToString("HH:mm:ss"),
                        msg.IsUserMessage ? currentUser.Name : "Bot",
                        msg.Content);
                }
                MessageBox.Show(history, "Konuşma Geçmişi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Henüz mesaj geçmişi yok.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        private void btnSettings_Click(object sender, EventArgs e)
        {
            using (var settingsForm = new SettingsForm())
            {
                if (settingsForm.ShowDialog() == DialogResult.OK)
                {
                    // Ayarları güncelle
                    apiKey = settingsForm.ApiKey;
                    useOnlineMode = settingsForm.UseOnlineMode;
                    selectedModel = settingsForm.SelectedModel;
                    systemPrompt = settingsForm.SystemPrompt;
                    
                    // ChatBot'u yeniden başlat
                    InitializeChatBot();
                    
                    // Mevcut konuşmayı yeniden oluştur
                    if (currentUser != null)
                    {
                        conversationId = chatBot.StartConversation(currentUser);
                    }
                    
                    UpdateModeLabel();
                    
                    AppendSystemMessage(string.Format("Ayarlar güncellendi. Mod: {0}", 
                        useOnlineMode ? "Online (" + selectedModel + ")" : "Offline"));
                }
            }
        }
        
        private void btnExport_Click(object sender, EventArgs e)
        {
            var conversation = chatBot.GetConversation(conversationId);
            if (conversation == null || conversation.Messages.Count == 0)
            {
                MessageBox.Show("Kaydedilecek konuşma yok.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Text File|*.txt|JSON File|*.json";
                sfd.Title = "Konuşmayı Kaydet";
                sfd.FileName = string.Format("conversation_{0}.txt", DateTime.Now.ToString("yyyyMMdd_HHmmss"));
                
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine("=== CHATBOT KONUŞMASI ===");
                        sb.AppendLine("Kullanıcı: " + currentUser.Name);
                        sb.AppendLine("Tarih: " + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"));
                        sb.AppendLine("Mod: " + (useOnlineMode ? "Online (" + selectedModel + ")" : "Offline"));
                        sb.AppendLine("Mesaj Sayısı: " + conversation.Messages.Count);
                        sb.AppendLine(new string('=', 50));
                        sb.AppendLine();
                        
                        foreach (var msg in conversation.Messages)
                        {
                            sb.AppendLine(string.Format("[{0}] {1}:", 
                                msg.Timestamp.ToString("HH:mm:ss"),
                                msg.IsUserMessage ? currentUser.Name : "Bot"));
                            sb.AppendLine(msg.Content);
                            sb.AppendLine();
                        }
                        
                        File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                        MessageBox.Show("Konuşma başarıyla kaydedildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Kaydetme hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        
        private void btnImport_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Text File|*.txt|All Files|*.*";
                ofd.Title = "Konuşma Yükle";
                
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string content = File.ReadAllText(ofd.FileName, Encoding.UTF8);
                        
                        // Chat penceresini temizle ve içeriği göster
                        rtbChat.Clear();
                        
                        string[] lines = content.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
                        foreach (string line in lines)
                        {
                            if (line.Contains(currentUser.Name + ":") || line.Contains("Kullanıcı:"))
                            {
                                rtbChat.SelectionColor = Color.Blue;
                                rtbChat.SelectionFont = new Font(rtbChat.Font, FontStyle.Bold);
                            }
                            else if (line.Contains("Bot:"))
                            {
                                rtbChat.SelectionColor = Color.Green;
                                rtbChat.SelectionFont = new Font(rtbChat.Font, FontStyle.Bold);
                            }
                            else if (line.StartsWith("==="))
                            {
                                rtbChat.SelectionColor = Color.DarkGray;
                                rtbChat.SelectionFont = new Font(rtbChat.Font, FontStyle.Italic);
                            }
                            else
                            {
                                rtbChat.SelectionColor = Color.Black;
                                rtbChat.SelectionFont = new Font(rtbChat.Font, FontStyle.Regular);
                            }
                            
                            rtbChat.AppendText(line + "\r\n");
                        }
                        
                        rtbChat.ScrollToCaret();
                        MessageBox.Show("Konuşma yüklendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Yükleme hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}

