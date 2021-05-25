using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Security.Cryptography;
using System.IO;
using System.Xml.Serialization;

namespace Server
{
    public class User
    {
        private Thread UserThread;
        private string UserName;
        private bool AuthSuccess = false;
        public RSAParameters PublicKey;
        internal byte[] ECDHPrivateKey;
        public string Username
        {
            get { return UserName; }
        }
        private Socket UserHandle;
        public User(Socket handle)
        {
            UserHandle = handle;
            UserThread = new Thread(Listener);
            UserThread.IsBackground = true;
            UserThread.Start();
        }
        private void Listener()
        {
            try
            {
                while (UserHandle.Connected)
                {
                    byte[] buffer = new byte[4048];
                    int bytesReceive = UserHandle.Receive(buffer);
                    HandleCommand(Encoding.Unicode.GetString(buffer, 0, bytesReceive));
                }
            }
            catch { Server.EndUser(this); }
        }
        private bool SetName(string Name, string RSAPublicKeyString, string ECDHPublicKeyString)
        {
            UserName = Name;
            Server.NewUser(this);
            AuthSuccess = true;
            using(StringReader StringReader = new StringReader(RSAPublicKeyString))
            {
                XmlSerializer XMLSerializer = new XmlSerializer(typeof(RSAParameters));
                PublicKey = (RSAParameters)XMLSerializer.Deserialize(StringReader);
            }

            ECDH.GeneratePrivateKey(Server.ECDHPublicKey, Convert.FromBase64String(ECDHPublicKeyString), out ECDHPrivateKey);

            return true;
        }
        private void HandleCommand(string cmd)
        {
            try
            {
                string[] commands = cmd.Split('#');
                int countCommands = commands.Length;
                for (int i = 0; i < countCommands; i++)
                {
                    string currentCommand = commands[i];
                    if (string.IsNullOrEmpty(currentCommand))
                        continue;
                    if (!AuthSuccess)
                    {
                        if (currentCommand.Contains("setname"))
                        {
                            if (SetName(currentCommand.Split('|')[1], currentCommand.Split('|')[2], currentCommand.Split('|')[3]))
                            {
                                using (StringWriter Writer = new StringWriter())
                                {
                                    XmlSerializer XMLSerializer = new XmlSerializer(typeof(RSAParameters));
                                    XMLSerializer.Serialize(Writer, Server.RSAPublicKey);
                                    Send($"#setnamesuccess|{Writer.ToString()}|{Convert.ToBase64String(Server.ECDHPublicKey)}");
                                }
                            }
                            else
                                Send("#setnamefailed");
                        }
                        continue;
                    }
                    if (currentCommand.Contains("message"))
                    {
                        string[] Arguments = currentCommand.Split('|');
                        Server.SendGlobalMessage($"[{UserName}]: {Arguments[1]}", "Black");

                        continue;
                    }
                    if (currentCommand.Contains("rsaenc")) //Encrypted using RSA
                    {
                        string[] Arguments = currentCommand.Split('|');
                        Server.SendGlobalRSAEncryptedMessage($"[{UserName}]: {RSA.Decryption(Convert.FromBase64String(Arguments[1]), Server.RSAPrivateKey)}", "Black");

                        continue;
                    }
                    if (currentCommand.Contains("ecdhenc")) //Encrypted using ECDH
                    {
                        string[] Arguments = currentCommand.Split('|');
                        Server.SendGlobalECDHEncryptedMessage($"[{UserName}]: {ECDH.Decryption(Convert.FromBase64String(Arguments[1]), ECDHPrivateKey, Convert.FromBase64String(Arguments[2]))}", "Black");

                        continue;
                    }
                    if (currentCommand.Contains("endsession"))
                    {
                        Server.EndUser(this);
                        return;
                    }

                    if (currentCommand.Contains("private"))
                    {
                        string[] Arguments = currentCommand.Split('|');
                        string TargetName = Arguments[1];
                        string Content = Arguments[2];
                        User TargetUser = Server.GetUser(TargetName);
                        if (TargetUser == null)
                        {
                            SendMessage($"Пользователь {TargetName} не найден!", "Red");
                            continue;
                        }
                        SendMessage($"-[Отправлено][{TargetName}]: {Content}", "Black");
                        TargetUser.SendMessage($"-[Получено][{Username}]: {Content}", "Black");
                        continue;
                    }

                }

            }
            catch (Exception exp) { Console.WriteLine("Error with handleCommand: " + exp.Message); }
        }

        public void SendMessage(string content, string clr)
        {
            Send($"#msg|{content}|{clr}");
        }
        public void SendRSAEncryptedMessage(string content, string clr)
        {
            Send($"#rsaenc|{Convert.ToBase64String(RSA.Encryption(content, PublicKey))}|{clr}");
        }
        public void SendECDHEncryptedMessage(string content, string clr)
        {
            byte[] IV;
            Send($"#ecdhenc|{Convert.ToBase64String(ECDH.Encryption(content, ECDHPrivateKey, out IV))}|{Convert.ToBase64String(IV)}|{clr}");
        }
        public void Send(byte[] buffer)
        {
            try
            {
                UserHandle.Send(buffer);
            }
            catch { }
        }
        public void Send(string Buffer)
        {
            try
            {
                UserHandle.Send(Encoding.Unicode.GetBytes(Buffer));
            }
            catch { }
        }
        public void End()
        {
            try
            {
                UserHandle.Close();
            }
            catch { }

        }
    }
}
