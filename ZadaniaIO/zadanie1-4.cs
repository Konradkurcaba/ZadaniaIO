using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ZadaniaIO
{
    class Program
    {
        static void Main(string[] args)
        {

            TcpListener Server = new TcpListener(IPAddress.Any, 2048);
            Server.Start();
            ThreadPool.QueueUserWorkItem(client);
            ThreadPool.QueueUserWorkItem(client);
            ThreadPool.QueueUserWorkItem(client);
            while (true)
            {
                TcpClient client = Server.AcceptTcpClient();
                ThreadPool.QueueUserWorkItem(server, client);
            }


        }
        private static Object thisLock = new Object();
        static void writeConsoleMessage(string message, ConsoleColor color)
        {
            message = message.Replace("\0", "");
            lock (thisLock)
            {
                Console.ForegroundColor = color;
                Console.WriteLine(message);
                Console.ResetColor();
            }
            Thread.Sleep(200); // dodane tylko po to aby zdążyć zauważyć co sie dzieje w konsoli 
        }


        static void server(Object stateInfo)
        {
            TcpClient client = (TcpClient)stateInfo;
            while (true)
            {
                byte[] buffer = new byte[1024];
                client.GetStream().Read(buffer, 0, 1024);
                writeConsoleMessage("(Serwer)Otrzymalem wiadomosc: " + System.Text.Encoding.Default.GetString(buffer, 0, buffer.Length), ConsoleColor.Green);
                client.GetStream().Write(buffer, 0, buffer.Length);
            }
        }
        static void client(Object stateInfo)
        {
            TcpClient client = new TcpClient();
            client.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2048));

            while (true)
            {
                byte[] message = new ASCIIEncoding().GetBytes("Wiadomosc");
                client.GetStream().Write(message, 0, message.Length);
                byte[] buffer = new byte[1024];
                client.GetStream().Read(buffer, 0, buffer.Length);
                writeConsoleMessage("(Client)Otrzymalem wiadomosc: " + System.Text.Encoding.Default.GetString(buffer, 0, buffer.Length), ConsoleColor.Red);
            }
        }
    }
}