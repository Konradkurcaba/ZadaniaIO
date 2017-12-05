using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ZadaniaIO
{
    class Program
    {
        static void Main(string[] args)
        {

            FileStream stream = new FileStream("plik.txt", FileMode.Open);
            byte[] buffer = new byte[1024];
            IAsyncResult result = stream.BeginRead(buffer, 0, buffer.Length, null, new object[] { stream, buffer });
            // tutaj mozna wykonywac operacje asynchronicznie
            stream.EndRead(result);
            string message = System.Text.Encoding.UTF8.GetString((byte[])((object[])result.AsyncState)[1]);
            stream.Close();
            Console.Write(message);

            Thread.Sleep(10000);


        }
    }
}
