using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace UDPChat
{
    public partial class ChatSettingsForm : Form
    {
        public string IpAddress => ipAddressTextBox.Text;
        public int Port => int.TryParse(portTextBox.Text, out int port) ? port : 0;
        public Font ChatFont { get; private set; }
        public Color ChatColor { get; private set; }

        public ChatSettingsForm()
        {
            InitializeComponent();
        }

        private void ChatSettingsForm_Load(object sender, EventArgs e)
        {
            LoadCurrentSettings();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (ValidateSettings())
            {
                SaveSettings();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void fontButton_Click(object sender, EventArgs e)
        {
            if (chatFontDialog.ShowDialog() == DialogResult.OK)
            {
                chatBox.Font = chatFontDialog.Font;
                ChatFont = chatFontDialog.Font;
            }
        }

        private void colorButton_Click(object sender, EventArgs e)
        {
            if (chatColorDialog.ShowDialog() == DialogResult.OK)
            {
                chatBox.ForeColor = chatColorDialog.Color;
                ChatColor = chatColorDialog.Color;
            }
        }

        private bool ValidateSettings()
        {
            if (!IPAddress.TryParse(IpAddress, out _))
            {
                MessageBox.Show("Введено некоректну IP-адресу.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (Port <= 0 || Port > 65535)
            {
                MessageBox.Show("Введено некоректний порт.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void SaveSettings()
        {
            try
            {
                string settingsFilePath = "settings.txt";
                using (StreamWriter writer = new StreamWriter(settingsFilePath, false))
                {
                    writer.WriteLine("IPAddress:" + IpAddress);
                    writer.WriteLine("Port:" + Port);
                    writer.WriteLine("Font:" + chatBox.Font.Name + "," + chatBox.Font.Size + "," + chatBox.Font.Style);
                    writer.WriteLine("Color:" + chatBox.ForeColor.R + "," + chatBox.ForeColor.G + "," + chatBox.ForeColor.B);
                }
                MessageBox.Show("Налаштування успішно збережено.", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при збереженні налаштувань: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCurrentSettings()
        {
            try
            {
                if (File.Exists("settings.txt"))
                {
                    string[] lines = File.ReadAllLines("settings.txt");
                    foreach (string line in lines)
                    {
                        string[] parts = line.Split(':');
                        if (parts[0] == "IPAddress")
                            ipAddressTextBox.Text = parts[1];
                        else if (parts[0] == "Port")
                            portTextBox.Text = parts[1];
                        else if (parts[0] == "Font")
                        {
                            string[] fontParts = parts[1].Split(',');
                            ChatFont = new Font(fontParts[0], float.Parse(fontParts[1]), (FontStyle)Enum.Parse(typeof(FontStyle), fontParts[2]));
                            chatBox.Font = ChatFont;
                        }
                        else if (parts[0] == "Color")
                        {
                            string[] colorParts = parts[1].Split(',');
                            ChatColor = Color.FromArgb(int.Parse(colorParts[0]), int.Parse(colorParts[1]), int.Parse(colorParts[2]));
                            chatBox.ForeColor = ChatColor;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при завантаженні налаштувань: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}