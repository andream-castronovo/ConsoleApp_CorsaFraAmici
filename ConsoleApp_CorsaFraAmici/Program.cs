using System;
using System.Threading;

namespace ConsoleApp_CorsaFraAmici
{
    class Program
    {
        static int posAndrea = 0;
        static int posBaldo = 0;
        static int posCarlo = 0;

        static int classifica = 0;

        static object _lock = new object();

        // TODO Stampare lo stato dei thread in tempo reale affianco al nome del corridore
        // TODO Stampare lo stato di vita del thread da qualche parte

        // TODO Fare metodo per scrivere che incorpora lock set cursor e write
        // TODO Fare metodo per aggiornare la classifica all'arrivo


        static void Andrea()
        {
            do
            {
                posAndrea++;
                //  @"  A",
                //  @" /║\",
                //  @"  ╙",

                Thread.Sleep(50);

                lock (_lock)
                {
                    Console.SetCursorPosition(posAndrea, 5);
                    Console.Write(@"  ╙");

                }

                Thread.Sleep(50);

                lock (_lock)
                {

                    Console.SetCursorPosition(posAndrea, 4);
                    Console.Write(@" /║\");
                }

                Thread.Sleep(50);
                lock (_lock)
                {

                    Console.SetCursorPosition(posAndrea, 3);
                    Console.Write(@"  A");
                }
                


            } while (posAndrea < 115);

            lock (_lock)
            {
                classifica++;
                Console.SetCursorPosition(115, 2);
                Console.Write(classifica);
            }
        }
        static void Baldo()
        {
            do
            {
                posBaldo++;
                // @"  B",
                // @" └║┘",
                // @"  ╨"
                Thread.Sleep(50);
                lock (_lock)
                {
                    Console.SetCursorPosition(posBaldo, 9);
                    Console.Write(@"  ╨");
                }
                Thread.Sleep(50);
                lock (_lock)
                {
                    Console.SetCursorPosition(posBaldo, 8);
                    Console.Write(@" └║┘");
                }

                Thread.Sleep(50);

                lock (_lock)
                {
                    Console.SetCursorPosition(posBaldo, 7);
                    Console.Write(@"  B");
                }

            } while (posBaldo < 115);

            lock (_lock)
            {
                classifica++;
                Console.SetCursorPosition(115, 6);
                Console.Write(classifica);
            }

        }

        static void Carlo()
        {
            do
            {
                posCarlo++;
   
                Thread.Sleep(50);

                lock (_lock)
                {
                    Console.SetCursorPosition(posCarlo, 13);
                    Console.Write(@" / \");
                }

                Thread.Sleep(50);

                lock (_lock)
                {
                    Console.SetCursorPosition(posCarlo, 12);
                    Console.Write(@" /|\");
                }

                Thread.Sleep(50);

                lock (_lock)
                {
                    Console.SetCursorPosition(posCarlo, 11);
                    Console.Write(@"  C");
                }

            } while (posCarlo < 115);

            lock (_lock)
            {
                classifica++;
                Console.SetCursorPosition(115, 10);
                Console.Write(classifica);
            }

        }

        static void Pronti()
        {
            string[] stringhe =
            {
                "Andrea",
                @"  A",
                @" /║\",
                @"  ╙",
                @"Baldo",
                @"  B",
                @" └║┘",
                @"  ╨",
                @"Carlo",
                @"  C",
                @" /|\",
                @" / \"
            };

            for (int i = 2; i < 14; i++)
            {
                int pos = 0;
                if (i < 6)
                    pos = posAndrea;
                else if (i < 10)
                    pos = posBaldo;
                else if (i < 14)
                    pos = posCarlo;

                Console.SetCursorPosition(pos, i);
                Console.Write(stringhe[i - 2]);
            }
        }

        static void Main(string[] args)
        {
            Console.CursorVisible = false;


            Pronti();



            // Essendo la Console una risorsa condivisa, non possono scrivere tutti allo stesso tempo,
            // c'è bisogno di un semaforo che "guidi" la scrittura.
            Thread tAndrea = new Thread(Andrea);

            Thread tBaldo = new Thread(Baldo);

            Thread tCarlo = new Thread(Carlo);
            
            
            tAndrea.Start(); // Esegue il codice nel metodo che gli abbiamo passato
            tBaldo.Start();
            tCarlo.Start();


            tAndrea.Join(); //  Aspetta che il Thread sia finito. È bloccante 
            tBaldo.Join();
            tCarlo.Join();

            // Baldo();
            // Andrea();
            // Carlo();


            // Console.WriteLine("\t\nProgramma finito, premi un tasto per terminare");
            Console.ReadKey(true);
        }
    }
}
