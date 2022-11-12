using System;
using System.Threading;

namespace ConsoleApp_Elabora001
{
    internal class Program
    {
        static int var;
        static System.Diagnostics.Stopwatch sw;

        // Oggetto generico da usare come "testimone"
        static object _lock0 = new object();

        static void Elabora01()
        {
            Console.WriteLine($"\nStarting {Thread.CurrentThread.Name} with ID {Thread.CurrentThread.ManagedThreadId}");
            int v;
            
            for (int i = 1; i <= 100; i++)
            {
                lock (_lock0)
                { // Prende il testimone: Lock
                    v = var;
                    Thread.Sleep(10);
                    var = v + 1;
                } // Lascia il testimone: Unlock
            }
        }

        static void Elabora02()
        {
            Console.WriteLine($"\nStarting {Thread.CurrentThread.Name} with ID {Thread.CurrentThread.ManagedThreadId}");

            int v;
            for (int i = 1; i <= 100; i++)
            {
                lock (_lock0)
                {
                    v = var;
                    Thread.Sleep(5);
                    var = v + 1;
                }
                Thread.Sleep(1);
            }
        }
        
        static void Main(string[] args)
        {
            
            var = 0;
            //Elabora01();
            //Elabora02();

            #region In questo caso non cambia nulla vista la presenza dei join
            //Thread th1 = new Thread(Elabora01);
            //th1.Start();
            //th1.Join();

            //Thread th2 = new Thread(Elabora02);
            //th2.Start();
            //th2.Join();

            #endregion

            Thread th1 = new Thread(Elabora01);
            th1.Name = "T1";

            Thread th2 = new Thread(Elabora02);
            th2.Name = "T2";

            th1.Start();
            th2.Start();

            // In questo caso avviando i Thread allo stesso tempo si contendono la variabile var
            // che viene assegnata ogni volta, quindi tornerà indietro ogni tanto e il risultato
            // non sarà quello desiderato
            th1.Join();
            th2.Join();

            Console.WriteLine($"\n var = {var}");

            Console.WriteLine("\t\nProgramma terminato, premere un tasto per chiudere");
            Console.ReadKey(true);
        }
    }
}
