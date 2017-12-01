using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class ClientHandler
    {
        Socket socket;
        NetworkStream stream;
        StreamReader sr;
        StreamWriter sw;

        string currentPath;

        public ClientHandler(Socket socket)
        {
            this.socket = socket;
            stream = new NetworkStream(socket);
            sr = new StreamReader(stream);
            sw = new StreamWriter(stream);
            currentPath = "C:/";
        }

        public void Start()
        {
            while (stream.CanRead)
            {
                ExecuteCommand(sr.ReadLine());
            }
        }

        public void SendToClient(string data)
        {
            sw.WriteLine(data);
            sw.Flush();
        }

        public void ExecuteCommand(string command)
        {
            string[] commandSplit = command.Split(new char[] { ' ' }, 2);
            switch (commandSplit[0])
            {
                case "ls":
                    Service.ListDirectories(currentPath).ForEach(x => SendToClient(x));
                    break;

                case "cd":
                    currentPath = Service.ChangeDirectory(currentPath, commandSplit[1]);
                    break;

                case "mkdir":
                    Service.MakeDirectory(currentPath + commandSplit[1]);
                    break;

                case "rm":
                    SendToClient(Service.RemoveDirectory(currentPath + commandSplit[1]));
                    break;

                default:
                    SendToClient("Invalid command!");
                    break;
            }
        }

    }
}
