using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ZadaniaIO
{
    class Program5
    {
        static void Main(string[] args)
        {   // wartosci początkowe #################
            int wielkosc_tablicy = 100020;
            int wielkosc_segmentu = 10000;
            // #####################################
            int[] tab = new int[wielkosc_tablicy];  //stworzenie tablicy o podanej wielkosc
            object locker = new object();   // stworzenie obiektu do locka
            value result = new value();        //stworzenie struktury przechowujacej wyniki watkow
            Random rnd = new Random();
            for (int i = 0; i < wielkosc_tablicy; i++) // wypielnienie tablicy
            {

                tab[i] = rnd.Next(1, 1);
            }
            int ilosc_watkow = wielkosc_tablicy / wielkosc_segmentu; // policzenie ile watkow jest potrzebnych 
            if (wielkosc_tablicy % wielkosc_segmentu != 0) ilosc_watkow++; // jesli byla reszta w dzieleniu to potrzea jednego wiecej
            WaitHandle[] waitHandles = new WaitHandle[ilosc_watkow]; // tablica dla auto resetEvent ???
            for (int i = 0; i < ilosc_watkow; i++)
            {
                waitHandles[i] = new AutoResetEvent(false); // inicjalizacja
            }
            for (int i = 0; i < ilosc_watkow; i++) // rozdzielanie zadan - obliczenie kto co dodaje 
            {
                Info inf = new Info(i * wielkosc_segmentu, ((i + 1) * wielkosc_segmentu) - 1, tab, locker, result, waitHandles[i]);
                if (((i + 1) * wielkosc_segmentu) > wielkosc_tablicy) inf.koniec = wielkosc_tablicy - 1;
                ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadProc), inf); // dodanie zadania i przekazanie mu parametrów
            }
            WaitHandle.WaitAll(waitHandles); // czekanie az kazdy watek sobie pododaje 
            Console.Write("Otrzymana suma to:" + result.result);
            Thread.Sleep(6000);


        }
        static void ThreadProc(Object stateInfo)
        {
            Info inf = (Info)stateInfo;
            long result = 0;
            for (int i = inf.poczatek; i <= inf.koniec; i++)
            {
                result += inf.tab[i];
            }
            lock (inf.locker)
            {
                inf.result.result += result; // dodanie wyniku dzialania jednego watku do klasy trzymajacej wynik
            }
            AutoResetEvent are = (AutoResetEvent)inf.handle;
            are.Set(); // powiadomienie ze watek skonczyl swoja robote


        }

    }
    class Info // klasa trzymajaca informacje dla watku
    {
        public int poczatek;
        public int koniec;
        public int[] tab;
        public object locker;
        public value result;
        public WaitHandle handle;
        public Info(int poczatek, int koniec, int[] tab, object locker, value result, WaitHandle handle)
        {
            this.poczatek = poczatek;
            this.koniec = koniec;
            this.tab = tab;
            this.locker = locker;
            this.result = result;
            this.handle = handle;
        }


    }
    class value // klasa trzymajaca wartosc wszystkich operacji, zrobiona tylko dlatego ze zmienna przekazywana jest przez wartosc
    {
        public long result = 0;


    }
}

