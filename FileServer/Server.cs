using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
    class Server
    {
        public void Run()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 3333);
            listener.Start();

            while (true)
            {
                Console.WriteLine("Waiting for client.");
                Socket clientSocket = listener.AcceptSocket();
                ClientHandler ch = new ClientHandler(clientSocket);
                new Thread(() => ch.Start()).Start();
            }
        }
    }
}
