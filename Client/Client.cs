using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Client
{
    class Client
    {
        TcpClient client;
        NetworkStream stream;
        StreamReader sr;
        StreamWriter sw;

        public Client()
        {
            client = new TcpClient("127.0.0.1", 3333);
            stream = client.GetStream();
            sr = new StreamReader(stream);
            sw = new StreamWriter(stream);
        }
        public void Run()
        {
            new Thread(() => WriteResponses()).Start();

            while (true)
            {
                //Console.Write(Environment.UserName + "@Windows");
                SendToServer(Console.ReadLine());
            }
        }

        public void WriteResponses()
        {
            string data = sr.ReadLine();
            while (data != null)
            {
                Console.WriteLine(data);
                data = sr.ReadLine();
            }
        }

        public void SendToServer(string data)
        {
            sw.WriteLine(data);
            sw.Flush();
        }
    }
}
