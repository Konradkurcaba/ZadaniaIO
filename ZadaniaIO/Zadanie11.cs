using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ZadaniaIO
{
    class Program2
    {
        static BackgroundWorker worker = new BackgroundWorker();




        static void Main(string[] args)
        {
            TcpListener server = new TcpListener(IPAddress.Any, 2048);
            server.Start();




            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProcessChanged;
            worker.WorkerReportsProgress = true;
            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                worker.RunWorkerAsync(client);
            }
        }

        private static void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            TcpClient client = (TcpClient)e.Argument;
            int message_counter = 0;
            while (message_counter < 100)
            {

                byte[] buffer = new byte[1024];
                client.GetStream().Read(buffer, 0, 1024);
                client.GetStream().Write(buffer, 0, buffer.Length);

                message_counter += 1;
                worker.ReportProgress(message_counter);

            }
        }
        private static void Worker_ProcessChanged(object sender, ProgressChangedEventArgs e)
        {
            Console.WriteLine("% wiadomosci ?: " + e.ProgressPercentage, ConsoleColor.Green);
        }
    }
}
