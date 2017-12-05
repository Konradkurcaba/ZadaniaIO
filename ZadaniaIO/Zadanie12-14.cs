using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ZadaniaIO
{
    class Program4
    {
        public struct TResultDataStructure
        {
            private int a, b;


            public int A { get => a; set => a = value; }
            public int B { get => b; set => b = value; }
        }
        public struct TResultDataStructure2
        {
            private string result;

            public string Result { get => result; set => result = value; }
        }


        public static async void Zadanie2()
        {
            //ZADANIE 2. ODKOMENTUJ I POPRAW  
            bool Z2 = false;
            await Task.Run(
                  () =>
                  {
                      Z2 = true;
                  });

        }

        static async Task<TResultDataStructure2> download_data(String url)
        {
            WebClient client = new WebClient();
            String result;
            result = await client.DownloadStringTaskAsync(url);
            TResultDataStructure2 nowa = new TResultDataStructure2();
            nowa.Result = result;
            return nowa;


        }
        static void Main(string[] args)
        {
            Task<TResultDataStructure2> download = download_data("http://www.feedforall.com/sample.xml");
            download.Wait();
            String result = download.Result.Result;
            Console.Write(result);
            Thread.Sleep(2000);
        }
    }
}
