using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;

namespace Server
{
    public static class Server
    {

        public static int CountUsers = 0;
        public static RSAParameters RSAPublicKey;
        internal static RSAParameters RSAPrivateKey;
        public static byte[] ECDHPublicKey;
        public delegate void UserEvent(string Name);
        public static event UserEvent UserConnected = (Username) =>
        {
            Console.WriteLine($"User {Username} connected.");
            CountUsers++;
            SendGlobalMessage($"Пользователь {Username} подключился к чату.", "Black");
            SendUserList();
        };
        public static event UserEvent UserDisconnected = (Username) =>
        {
            Console.WriteLine($"User {Username} disconnected.");
            CountUsers--;
            SendGlobalMessage($"Пользователь {Username} отключился от чата.","Black");
            SendUserList();
        };
        public static List<User> UserList = new List<User>();
        public static Socket ServerSocket;
        public const string Host = "127.0.0.1";
        public const int Port = 2222;
        public static bool Work = true;

      
        public static void NewUser(User usr)
        {
            if (UserList.Contains(usr))
                return;
            UserList.Add(usr);
            UserConnected(usr.Username);
        }
        public static void EndUser(User usr)
        {
            if (!UserList.Contains(usr))
                return;
            UserList.Remove(usr);
            usr.End();
            UserDisconnected(usr.Username);

        }

        public static User GetUser(string Name)
        {
            for(int i = 0;i < CountUsers;i++)
            {
                if (UserList[i].Username == Name)
                    return UserList[i];
            }
            return null;
        }
        public static void SendUserList()
        {
            string userList = "#userlist|";

            for(int i = 0;i < CountUsers;i++)
            {
                userList += UserList[i].Username + ",";
            }

            SendAllUsers(userList);
        }
        public static void SendGlobalMessage(string content,string clr)
        {
            for(int i = 0;i < CountUsers;i++)
            {
                UserList[i].SendMessage(content, clr);
            }
        }
        public static void SendGlobalRSAEncryptedMessage(string content, string clr)
        {
            for (int i = 0; i < CountUsers; i++)
            {
                UserList[i].SendRSAEncryptedMessage(content, clr);
            }
        }
        public static void SendGlobalECDHEncryptedMessage(string content, string clr)
        {
            for (int i = 0; i < CountUsers; i++)
            {
                UserList[i].SendECDHEncryptedMessage(content, clr);
            }
        }
        public static void SendAllUsers(byte[] data)
        {
            for(int i = 0;i < CountUsers;i++)
            {
                UserList[i].Send(data);
            }
        }
        public static void SendAllUsers(string data)
        {
            for (int i = 0; i < CountUsers; i++)
            {
                UserList[i].Send(data);
            }
        }
    }
}
