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

        // TODO FATTO Stampare lo stato dei thread in tempo reale affianco al nome del corridore
        // TODO FATTO Stampare lo stato di vita del thread da qualche parte
        // TODO FATTO Fare metodo per scrivere che incorpora lock set cursor e write
        
        // TODO Fare metodo per aggiornare la classifica all'arrivo

        static void ScriviCondiviso(object lck, int posX, int posY, string text)
        {
            lock (lck)
            {
                Console.SetCursorPosition(posX, posY);
                Console.Write(text);
            }
        }


        static void Andrea()
        {
            do
            {
                posAndrea++;
                //  @"  A",
                //  @" /║\",
                //  @"  ╙",
                int testaAndrea = 3;


                Thread.Sleep(50);
                ScriviCondiviso(_lock, posAndrea, testaAndrea + 2, @"  ╙");

                Thread.Sleep(50);
                ScriviCondiviso(_lock, posAndrea, testaAndrea + 1, @" /║\");

                Thread.Sleep(50);
                ScriviCondiviso(_lock, posAndrea, testaAndrea, @"  A");


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

                int testaBaldo = 7;

                Thread.Sleep(50);
                ScriviCondiviso(_lock, posBaldo, testaBaldo + 2, @"  ╨");

                Thread.Sleep(50);
                ScriviCondiviso(_lock, posBaldo, testaBaldo + 1, @" └║┘");

                Thread.Sleep(50);
                ScriviCondiviso(_lock, posBaldo, testaBaldo, @"  B");


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



                int testaCarlo = 11;

                Thread.Sleep(50);
                ScriviCondiviso(_lock, posCarlo, testaCarlo + 2, @" / \");

                Thread.Sleep(50);
                ScriviCondiviso(_lock, posCarlo, testaCarlo + 1, @" /|\");

                Thread.Sleep(50);
                ScriviCondiviso(_lock, posCarlo, testaCarlo, @"  C");

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
            Thread tAndrea = new Thread(Andrea);

            Thread tBaldo = new Thread(Baldo);

            Thread tCarlo = new Thread(Carlo);

            Pronti();
            StampaStatoThreadEIsAlive(tAndrea, 10, 2, _lock);
            StampaStatoThreadEIsAlive(tBaldo, 10, 2 + 4, _lock);
            StampaStatoThreadEIsAlive(tCarlo, 10, 2 + 8, _lock);


            Console.SetCursorPosition(0, 14);
            Console.WriteLine("\n\nClicca un tasto per far partire i corridori");
            Console.ReadKey(true);
            Console.SetCursorPosition(Console.CursorLeft,Console.CursorTop - 1);
            Console.WriteLine("                                                                 ");

            // Essendo la Console una risorsa condivisa, non possono scrivere tutti allo stesso tempo,
            // c'è bisogno di un semaforo che "guidi" la scrittura.


            tAndrea.Start(); // Esegue il codice nel metodo che gli abbiamo passato
            tBaldo.Start();
            tCarlo.Start();

            while (tAndrea.IsAlive || tBaldo.IsAlive || tCarlo.IsAlive)
            {
                StampaStatoThreadEIsAlive(tAndrea, 10, 2, _lock);
                StampaStatoThreadEIsAlive(tBaldo, 10, 2 + 4, _lock);
                StampaStatoThreadEIsAlive(tCarlo, 10, 2 + 8, _lock);
            }

            //tAndrea.Join(); //  Aspetta che il Thread sia finito. È bloccante 
            //tBaldo.Join();
            //tCarlo.Join();

            // Baldo();
            // Andrea();
            // Carlo();


            // Console.WriteLine("\t\nProgramma finito, premi un tasto per terminare");
            Console.ReadKey(true);
        }

        static void StampaStatoThreadEIsAlive(Thread th, int posX, int posY, object lck)
        {
            lock (lck)
            {
                Console.SetCursorPosition(posX, posY);
                Console.Write($"Stato thread: {th.ThreadState}      ");

            }    
            lock (lck)
            {
                Console.SetCursorPosition(posX + 40, posY);
                Console.Write($"IsAlive: {th.IsAlive}      ");
            }
        }

    }
}
