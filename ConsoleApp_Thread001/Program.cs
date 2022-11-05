using System;
using System.Threading;


namespace ConsoleApp_Thread001
{
    internal class Program
    {
        const int TEMPO_THREAD = 700;
        const int TEMPO_MAIN = 200;
        
        static void PrintNumbers()
        {
            Console.WriteLine($"\nStarting thread {Thread.CurrentThread.ManagedThreadId}...\n");
            for (int i = 1; i <= 10; i++)
            {
                Thread.Sleep(TEMPO_THREAD); // 0,5 secondi == 500 millisecondi
                Console.WriteLine($" {Thread.CurrentThread.Name}   {i}");
            }

        }

        [Obsolete]
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Main\n MethodName(ThreadId) ");

            // Lo stesso metodo può essere associato a più Thread
            // Sono Thread indipendenti ma hanno lo stesso codice
            Thread th = new Thread(PrintNumbers);
            th.Name = $"PrintNumbers1";
            th.Start();

            th.Join(); // Join fa aspettare il Main finché "th" non è terminato.

            Thread th2 = new Thread(PrintNumbers);
            th2.Name = $"PrintNumbers2";
            th2.Start();

            Thread.Sleep(2200);

            th2.Suspend();
            Console.WriteLine($"{th2.Name} sospeso");

            for (int i = 1; i <= 10; i++)
            {
                Thread.Sleep(TEMPO_MAIN);
                Console.WriteLine($" Main({Thread.CurrentThread.ManagedThreadId})   {i}");
            }

            Console.WriteLine("Main terminato.");

            th2.Resume();
            Console.WriteLine($"{th2.Name} ripreso");

            for (int i = 11; i <= 15; i++)
            {
                Thread.Sleep(700);
                Console.WriteLine($" Main({Thread.CurrentThread.ManagedThreadId})   {i}");
            }

            th2.Join(); // Aspetta che il thread abbia finito.
                        // si dice BLOCCANTE!
                        // È molto importante non lasciare thread aperti alla fine del programma
                        // Quindi aspettiamo che il thread aperto finisca.

            Console.WriteLine("\n\tProgramma terminato. Premi un tasto per uscire");
            Console.ReadKey(false);
        }
    }
}
