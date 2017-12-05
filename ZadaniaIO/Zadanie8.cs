using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ZadaniaIO
{

    class Program1
    {
        delegate long delegat(int n);
        static delegat test_recursion;
        static delegat test;
        static delegat test_recursion2;
        static delegat test2;
        static long factorial_recursion(int n)
        {
            if (n == 0) return 1;
            else return n * factorial_recursion(n - 1);
        }

        static long fibonacci_recursion(int n)
        {
            if (n == 0) return 0;
            else if (n == 1) return 1;
            else return fibonacci_recursion(n - 1) + fibonacci_recursion(n - 2);
        }
        static long factorial(int n)
        {
            long result = 1;
            for (int i = 1; i <= n; i++)
            {
                result *= i;
            }
            return result;
        }
        static long fibonacci(int n)
        {
            if (n == 0) return 0;

            int last_result = 1;
            int result = 1;
            int last_last_result = 0;
            for (int i = 2; i <= n; i++)
            {

                result = last_last_result + last_result;
                last_last_result = last_result;
                last_result = result;

            }
            return result;
        }
        static void callback(IAsyncResult state)
        {
            if ((int)((object[])state.AsyncState)[1] == 1) Console.Write("skonzyla rekurencja - silnia\n");
            if ((int)((object[])state.AsyncState)[1] == 2) Console.Write("skonzyla iteracja - silnia\n");
            if ((int)((object[])state.AsyncState)[1] == 3) Console.Write("skonzyla rekurencja - fibbonaci\n");
            if ((int)((object[])state.AsyncState)[1] == 3) Console.Write("skonzyla iteracyjna - fibbonaci\n");
        }

        static void Main(string[] args)
        {

            test = new delegat(factorial);
            test_recursion = new delegat(factorial_recursion);
            IAsyncResult ar = test_recursion.BeginInvoke(40, callback, new object[] { 35, 1 });
            IAsyncResult arr = test.BeginInvoke(40, callback, new object[] { 35, 2 });

            long result_recursion = test_recursion.EndInvoke(ar);
            long result = test.EndInvoke(arr);




            test2 = new delegat(fibonacci);
            test_recursion2 = new delegat(fibonacci_recursion);
            IAsyncResult arrr = test_recursion.BeginInvoke(40, callback, new object[] { 90, 3 });
            IAsyncResult arrrr = test.BeginInvoke(40, callback, new object[] { 90, 4 });

            result_recursion = test_recursion.EndInvoke(arrr);
            result = test.EndInvoke(arrrr);

            Thread.Sleep(4000);



        }
    }
}
