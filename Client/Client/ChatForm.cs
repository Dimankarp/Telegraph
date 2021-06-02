using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Security.Cryptography;
using System.Xml.Serialization;

namespace Client
{
    public partial class ChatForm : Form
    {
        bool RSAStatus = false;
        bool ECDHStatus = false;
        private delegate void ChatEvent(string content, string clr);
        private ChatEvent addMessage;
        private Socket ServerSocket;
        private Thread ListenThread;
        private Thread ConnectingThread;
        private string HostIP = "127.0.0.1";
        private int HostPort = 2222;
        public byte[] ECDHPublicKey;
        private byte[] ECDHPrivateKey;
        public RSAParameters PublicKey;
        private RSAParameters PrivateKey;
        private RSAParameters ServerPublicKey;

        public ChatForm()
        {
            InitializeComponent();
            RSA.Generate(out PublicKey, out PrivateKey);
            ECDH.ECDHProvider = new ECDiffieHellmanCng(256);
            ECDH.GeneratePublicKey(out ECDHPublicKey);
            addMessage = new ChatEvent(AddMessage);
            userMenu = new ContextMenuStrip();
            ToolStripMenuItem PrivateMessageItem = new ToolStripMenuItem();
            PrivateMessageItem.Text = "Личное сообщение";
            PrivateMessageItem.Click += delegate
            {
                if (userList.SelectedItems.Count > 0)
                {
                    messageData.Text = $"\"{userList.SelectedItem} ";
                }
            };
            userMenu.Items.Add(PrivateMessageItem);
            userList.ContextMenuStrip = userMenu;

        }

        private void AddMessage(string Content, string Color = "Black")
        {
            if (InvokeRequired)
            {
                Invoke(addMessage, Content, Color);
                return;
            }
            chatBox.SelectionStart = chatBox.TextLength;
            chatBox.SelectionLength = Content.Length;
            chatBox.SelectionColor = getColor(Color);
            chatBox.AppendText(Content + Environment.NewLine);
        }

        private Color getColor(string text)
        {
            if (Color.Red.Name.Contains(text))
                return Color.Red;
            else if (Color.Green.Name.Contains(text))
                return Color.Green;
            return Color.Black;

        }

        private void ChatForm_Load(object sender, EventArgs e)
        {
        }

        private void ConnectToServer()
        {
            IPAddress temp = IPAddress.Parse(HostIP);
            ServerSocket = new Socket(temp.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            System.Diagnostics.Stopwatch Watch = new System.Diagnostics.Stopwatch();
            Watch.Start();
            while (Watch.ElapsedMilliseconds <= 4000)
            {
                try
                {
                    ServerSocket.Connect(new IPEndPoint(temp, HostPort));
                    break;
                }
                catch
                {
                    continue;
                }
            }

            if (ServerSocket.Connected)
            {
                Invoke((MethodInvoker)delegate
                {
                    enterChat.Enabled = true;
                    nicknameData.Enabled = true;
                    ServerStatusLabel.Text = "Доступен";
                    ServerStatusLabel.ForeColor = Color.Green;
                    AddMessage("Связь с сервером установлена.");
                    ListenThread = new Thread(Listener);
                    ListenThread.IsBackground = true;
                    ListenThread.Start();
                });
            }
            else
                Invoke((MethodInvoker)delegate
                {
                    IPEnteringBox.Enabled = true;
                    ServerStatusLabel.Text = "Недоступно";
                    ServerStatusLabel.ForeColor = Color.Crimson;

                });


        }

        public void Send(byte[] Buffer)
        {
            try
            {
                ServerSocket.Send(Buffer);
            }
            catch { }
        }
        public void Send(string Buffer)
        {
            try
            {
                ServerSocket.Send(Encoding.Unicode.GetBytes(Buffer));
            }
            catch { }
        }

        public void handleCommand(string cmd)
        {

            string[] commands = cmd.Split('#');
            int countCommands = commands.Length;
            for (int i = 0; i < countCommands; i++)
            {
                try
                {
                    string currentCommand = commands[i];
                    if (string.IsNullOrEmpty(currentCommand))
                        continue;
                    if (currentCommand.Contains("setnamesuccess"))
                    {
                        string[] Arguments = currentCommand.Split('|');
                        using (StringReader StringReader = new StringReader(Arguments[1]))
                        {
                            XmlSerializer XMLSerializer = new XmlSerializer(typeof(RSAParameters));
                            ServerPublicKey = (RSAParameters)XMLSerializer.Deserialize(StringReader);
                        }
                        ECDH.GeneratePrivateKey(ECDHPublicKey, Convert.FromBase64String(Arguments[2]), out ECDHPrivateKey);
                        Invoke((MethodInvoker)delegate
                        {
                            AddMessage($"Добро пожаловать, {nicknameData.Text}");
                            nameData.Text = nicknameData.Text;
                            chatBox.Enabled = true;
                            messageData.Enabled = true;
                            userList.Enabled = true;
                            nicknameData.Enabled = false;
                            enterChat.Enabled = false;
                            rsachecker.Enabled = true;
                            ecdhchecker.Enabled = true;
                        });
                        continue;
                    }
                    if (currentCommand.Contains("setnamefailed"))
                    {
                        AddMessage("Неверный ник!");
                        continue;
                    }
                    if (currentCommand.Contains("rsaenc")) //Encrypted using RSA
                    {
                        string[] Arguments = currentCommand.Split('|');

                        AddMessage($"Расшифровано с RSA: {RSA.Decryption(Convert.FromBase64String(Arguments[1]), PrivateKey)}", Arguments[2]);
                        continue;
                    }
                    if (currentCommand.Contains("ecdhenc")) //Encrypted using ECDH
                    {
                        string[] Arguments = currentCommand.Split('|');

                        AddMessage($"Расшифровано с ECDH: {ECDH.Decryption(Convert.FromBase64String(Arguments[1]), ECDHPrivateKey, Convert.FromBase64String(Arguments[2])) }", Arguments[3]);
                        continue;
                    }
                    if (currentCommand.Contains("msg"))
                    {
                        string[] Arguments = currentCommand.Split('|');
                        AddMessage(Arguments[1], Arguments[2]);
                        continue;
                    }

                    if (currentCommand.Contains("userlist"))
                    {
                        string[] Users = currentCommand.Split('|')[1].Split(',');
                        int countUsers = Users.Length;
                        userList.Invoke((MethodInvoker)delegate { userList.Items.Clear(); });
                        for (int j = 0; j < countUsers; j++)
                        {
                            userList.Invoke((MethodInvoker)delegate { userList.Items.Add(Users[j]); });
                        }
                        continue;

                    }

                }
                catch (Exception exp) { Console.WriteLine("Error with handleCommand: " + exp.Message); }

            }


        }
        public void Listener()
        {
            try
            {
                while (ServerSocket.Connected)
                {
                    byte[] buffer = new byte[2048];
                    int bytesReceive = ServerSocket.Receive(buffer);
                    handleCommand(Encoding.Unicode.GetString(buffer, 0, bytesReceive));
                }
            }
            catch
            {
                MessageBox.Show("Связь с сервером прервана");
                Invoke((MethodInvoker)delegate { ServerConnectionLost(); });
                ConnectingThread = new Thread(ConnectToServer);
                ConnectingThread.IsBackground = true;
                ConnectingThread.Start();
            }
        }

        private void ServerConnectionLost()
        {
            ServerStatusLabel.Text = "Недоступен";
            ServerStatusLabel.ForeColor = Color.Crimson;
            nameData.Text = string.Empty;
            nicknameData.Text = string.Empty;
            userList.Items.Clear();
            chatBox.Enabled = false;
            messageData.Enabled = false;
            userList.Enabled = false;
            nicknameData.Enabled = false;
            enterChat.Enabled = false;
            rsachecker.Enabled = false;
            ecdhchecker.Enabled = false;
            IPEnteringBox.Enabled = true;
        }

        private void enterChat_Click(object sender, EventArgs e)
        {
            string nickName = nicknameData.Text;
            if (string.IsNullOrEmpty(nickName))
                return;
            using (StringWriter Writer = new StringWriter())
            {
                XmlSerializer XMLSerializer = new XmlSerializer(typeof(RSAParameters));
                XMLSerializer.Serialize(Writer, PublicKey);
                Send($"#setname|{nickName}|{Writer.ToString()}|{Convert.ToBase64String(ECDHPublicKey)}");
            }
        }

        private void ChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ServerSocket.Connected)
                Send("#endsession");
        }

        private void messageData_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                string msgData = messageData.Text;
                string[] Words = msgData.Split(' ');
                if (string.IsNullOrEmpty(msgData))
                    return;
                if (Words[0] == "#clear")
                {
                    chatBox.Clear();
                    messageData.Text = string.Empty;
                    return;
                }
                if (msgData[0] == '"')
                {
                    string temp = msgData.Split(' ')[0];
                    string content = msgData.Substring(temp.Length + 1);
                    temp = temp.Replace("\"", string.Empty);
                    Send($"#private|{temp}|{content}");
                    messageData.Text = string.Empty;
                    return;
                }
                if (RSAStatus)
                {
                    string EncryptedMessageBits = BitConverter.ToString(RSA.Encryption(messageData.Text, ServerPublicKey));
                    string EncryptedMessageBase64 = Convert.ToBase64String(RSA.Encryption(messageData.Text, ServerPublicKey));
                    AddMessage($"Зашифрованное сообщение отправлено: {EncryptedMessageBits}", "Green");
                    Send($"#rsaenc|{EncryptedMessageBase64}");
                    messageData.Text = String.Empty;
                    return;
                }
                if (ECDHStatus)
                {
                    byte[] IV;
                    byte[] EncyptedMessage = ECDH.Encryption(messageData.Text, ECDHPrivateKey, out IV);
                    string EncryptedMessageBits = BitConverter.ToString(EncyptedMessage);
                    string EncryptedMessageBase64 = Convert.ToBase64String(EncyptedMessage);
                    AddMessage($"Зашифрованное сообщение отправлено: {EncryptedMessageBits}", "Green");
                    Send($"#ecdhenc|{EncryptedMessageBase64}|{Convert.ToBase64String(IV)}");
                    messageData.Text = String.Empty;
                    return;
                }
                else
                    Send($"#message|{msgData}");
                messageData.Text = string.Empty;
            }
        }

        private void RSAStatusChanged(object sender, EventArgs e)
        {
            RSAStatus = rsachecker.Checked;
            if (rsachecker.Checked)
            {
                ecdhchecker.Checked = false;
                AddMessage("Шифрование RSA - включено", "Green");
            }
            else AddMessage("Шифрование RSA - выключено", "Red");

        }

        private void IPAdressBox_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Enter)
            {
                ServerStatusLabel.Text = "Подключение...";
                ServerStatusLabel.ForeColor = Color.DarkBlue;
                string[] IPAdress;
                if (!IPValidation(IPEnteringBox.Text, out IPAdress)) return;
                IPEnteringBox.Enabled = false;
                HostIP = IPAdress[0];
                HostPort = Int32.Parse(IPAdress[1]);
                ConnectingThread = new Thread(ConnectToServer);
                ConnectingThread.IsBackground = true;
                ConnectingThread.Start();


            }
        }

        private void ECDHStatusChanged(object sender, EventArgs e)
        {
            ECDHStatus = ecdhchecker.Checked;
            if (ecdhchecker.Checked)
            {
                rsachecker.Checked = false;
                AddMessage("Шифрование ECDH - включено", "Green");
            }
            else AddMessage("Шифрование ECDH - выключено", "Red");
        }

        private bool IPValidation(string Adress, out string[] Data)
        {
            if (!Adress.Contains(':'))
            {
                ServerStatusLabel.Text = "Неверный IP!";
                ServerStatusLabel.ForeColor = Color.Crimson;
                Data = null;
                return false;
            }
            Adress.Trim();
            Data = Adress.Split(':');

            if (!Data[0].Contains('.'))
            {
                ServerStatusLabel.Text = "Неверный IP!";
                ServerStatusLabel.ForeColor = Color.Crimson;
                return false;
            }

            string[] IpNumbers = Data[0].Split('.');

            if (IpNumbers.Length != 4)
            {
                ServerStatusLabel.Text = "Неверный IP!";
                ServerStatusLabel.ForeColor = Color.Crimson;
                return false;
            }

            foreach (string item in IpNumbers)
            {
                int num;
                if (!int.TryParse(item, out num) || num < 0 || num > 255)
                {
                    ServerStatusLabel.Text = "Неверный IP!";
                    ServerStatusLabel.ForeColor = Color.Crimson;
                    return false;
                }
            }
            int port;
            if (!int.TryParse(Data[1], out port) || port < 1 || port > 65535)
            {
                ServerStatusLabel.Text = "Неверный IP!";
                ServerStatusLabel.ForeColor = Color.Crimson;
                return false;
            }
            return true;

        }

    }
}

