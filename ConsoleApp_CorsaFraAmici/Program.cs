using System;
using System.Threading;

namespace ConsoleApp_CorsaFraAmici
{
    class Program
    {
        static int posAndrea = 0;
        static int posBaldo = 0;
        static int posCarlo = 0;

        static void Andrea()
        {
            do
            {
                posAndrea++;
                //  @"  A",
                //  @" /║\",
                //  @"  ╙",
                Thread.Sleep(50);
                Console.SetCursorPosition(posAndrea, 5);
                Console.Write(@"  ╙");
                Console.SetCursorPosition(posAndrea, 4);
                Console.Write(@" /║\");
                Console.SetCursorPosition(posAndrea, 3);
                Console.Write(@"  A");


            } while (posAndrea < 115);
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
                Console.SetCursorPosition(posBaldo, 9);
                Console.Write(@"  ╨");
                Console.SetCursorPosition(posBaldo, 8);
                Console.Write(@" └║┘");
                Console.SetCursorPosition(posBaldo, 7);
                Console.Write(@"  B");


            } while (posBaldo < 115);
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
                Console.Write(stringhe[i-2]);
            }
        }

        static void Main(string[] args)
        {
            Console.CursorVisible = false;


            Pronti();



            // Essendo la Console una risorsa condivisa, non possono scrivere tutti allo stesso tempo,
            // c'è bisogno di un semaforo che "guidi" la scrittura.
            Thread tAndrea = new Thread(Andrea);
            tAndrea.Start(); // Esegue il codice nel metodo che gli abbiamo passato
            tAndrea.Join(); //  Aspetta che il Thread sia finito. È bloccante 

            Thread tBaldo = new Thread(Baldo);
            tBaldo.Start();
            tBaldo.Join();

            // Baldo();
            // Andrea();
            // Carlo();

            // Console.WriteLine("\t\nProgramma finito, premi un tasto per terminare");
            Console.ReadKey(true);
        }
    }
}
