using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace UDPChat
{
    public partial class ChatForm : Form
    {
        private UdpClient udpClient;
        private IPEndPoint remoteEndPoint;
        private string logFilePath = "chatlog.txt";

        public ChatForm()
        {
            InitializeComponent();
            udpClient = new UdpClient();
            remoteEndPoint = new IPEndPoint(IPAddress.Loopback, 0);
        }

        private void ChatForm_Load(object sender, EventArgs e)
        {
            LoadSettings();
        }

        private void LoadSettings()
        {
            try
            {
                if (System.IO.File.Exists("settings.txt"))
                {
                    string[] lines = System.IO.File.ReadAllLines("settings.txt");
                    foreach (string line in lines)
                    {
                        string[] parts = line.Split(':');
                        if (parts[0] == "IPAddress")
                            remoteEndPoint.Address = IPAddress.Parse(parts[1]);
                        else if (parts[0] == "Port")
                            remoteEndPoint.Port = int.Parse(parts[1]);
                        else if (parts[0] == "Font")
                        {
                            string[] fontParts = parts[1].Split(',');
                            chatBox.Font = new System.Drawing.Font(fontParts[0], float.Parse(fontParts[1]), (System.Drawing.FontStyle)Enum.Parse(typeof(System.Drawing.FontStyle), fontParts[2]));
                        }
                        else if (parts[0] == "Color")
                        {
                            string[] colorParts = parts[1].Split(',');
                            chatBox.ForeColor = System.Drawing.Color.FromArgb(int.Parse(colorParts[0]), int.Parse(colorParts[1]), int.Parse(colorParts[2]));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading settings: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            if (remoteEndPoint == null || remoteEndPoint.Address == null || remoteEndPoint.Port == 0)
            {
                MessageBox.Show("Please set the IP address and port in settings before sending messages.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string message = messageTextBox.Text;
            byte[] data = Encoding.UTF8.GetBytes(message);
            udpClient.Send(data, data.Length, remoteEndPoint);
            chatBox.AppendText("Me: " + message + Environment.NewLine);
            System.IO.File.AppendAllText(logFilePath, "Me: " + message + Environment.NewLine);
            messageTextBox.Clear();
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            using (ChatSettingsForm settingsForm = new ChatSettingsForm())
            {
                if (settingsForm.ShowDialog() == DialogResult.OK)
                {
                    remoteEndPoint = new IPEndPoint(IPAddress.Parse(settingsForm.IpAddress), settingsForm.Port);
                    chatBox.Font = settingsForm.ChatFont;
                    chatBox.ForeColor = settingsForm.ChatColor;
                }
            }
        }
    }
}
