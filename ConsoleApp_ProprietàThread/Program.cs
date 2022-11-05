using System;

using System.Threading;

namespace ConsoleApp_ProprietàThread
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Documentazione 

            #region Proprietà
            // thread.IsAlive: se è true il thread non è terminato, altrimenti false è terminato
            //                 un Thread termina quando muore di morte naturale (quando finisce il codice da eseguire)
            //                 o con thread.Abort();

            // thread.CurrentThread: ci consente di mettere in una variabile di tipo Thread il thread attuale 
            //                      Ci da il thread che sta eseguendo questo codice;

            // thread.ManagedThreadId: ci da un ID univoco di quel thread;

            // thread.ThreadState: ci dice in che stato è il thread;
            //                     Stati un cui può essere un Thread:
            //                     http://www.diranieh.com/NETThreading/Figures/ThreadStates.jpg

            // thread.Priority: serve allo scheduler,
            //                  chi ha priorità alta ha più tempo per essere eseguito,
            //                  se ha meno priorità avrà meno tempo,
            //                  ciò significa che se ha più priorità sarà più veloce;

            // th.IsBackground ci dice se il thread è eseguito in background o no;

            // th.CurrentCulture, di che cultura è il thread (Per permettere formattazione in base alla nazione, credo);
            #endregion

            #region Metodi
            // Start() -> Avvia un thread

            #region Metodi che mandano in stato: WaitSleepJoin

            // Wait() -> 
            // Sleep(ms) -> aspetta ms millisecondi e poi continua
            // Join() -> Aspetta la morte del Thread

            #endregion

            #region Metodi che riguardano Suspended
            // Suspend() 

            // Resume() torna in Running
            #endregion


            #region Metodi che mandano in Stopped il thread (morte)
            // Terminazione naturale (nessun metodo, il codice da eseguire è finito)

            // Abort() -> uccisione forzata, da usare con parsimonia
            //            Non consente al Thread di terminare ciò che sta facendo
            #endregion

            #endregion

            #endregion

            Thread th = Thread.CurrentThread;

            Console.WriteLine($"Thread.IsAlive = {th.IsAlive}");
            Console.WriteLine($"Thread Id = {th.ManagedThreadId}");
            Console.WriteLine($"Thread.ThreadState = {th.ThreadState}");
            Console.WriteLine($"Thread.Priority = {th.Priority}");
            Console.WriteLine($"Thread.IsBackground = {th.IsBackground}");
            Console.WriteLine($"Thread Culture = {th.CurrentCulture}");



            Console.ReadKey(false);




        }
    }
}
