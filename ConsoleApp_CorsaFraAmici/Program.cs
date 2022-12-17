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

        static int[] coordinateMenu = { 0, 17 };
        
        // Sviluppare un menù che una volta selezionato un corridore abiliti alle seguenti opzioni:
        
        // Abort per uccidere il corridore
        // Suspend per fermare temporaneamente il corridore
        // Resume per far riprendere il corridore
        // Join per far aspettare un altro corridore a quello selezionato.

        static void StampaMenuPrincipale()
        {
            int[] coordinatePrecedenti = { Console.CursorLeft, Console.CursorTop };

            Console.SetCursorPosition(coordinateMenu[0], coordinateMenu[1]);
            
            Console.WriteLine(" MENU'");

            Console.SetCursorPosition(coordinateMenu[0]+1, Console.CursorTop);
            Console.WriteLine(" |- A) Andrea");

            Console.SetCursorPosition(coordinateMenu[0]+1, Console.CursorTop);
            Console.WriteLine(" |- B) Baldo");

            Console.SetCursorPosition(coordinateMenu[0]+1, Console.CursorTop);
            Console.WriteLine(" |- C) Carlo");

            Console.SetCursorPosition(coordinatePrecedenti[0], coordinatePrecedenti[1]);
        }

        static void StampaMenuCorridore(Thread corridore)
        {

            
            int[] coordinatePrecedenti = { Console.CursorLeft, Console.CursorTop };

            int[] coordinateMenuCorridore = { coordinateMenu[0]+25, coordinateMenu[1] };

            string nomeCorridore = "";

            Console.SetCursorPosition(coordinateMenuCorridore[0], coordinateMenuCorridore[1]);
            Console.WriteLine("Seleziona azione da compiere su " + nomeCorridore);
            
            Console.SetCursorPosition(coordinateMenuCorridore[0], Console.CursorTop);
            Console.WriteLine("S) Suspend");
            
            Console.SetCursorPosition(coordinateMenuCorridore[0], Console.CursorTop);
            Console.WriteLine("K) Abort");

            Console.SetCursorPosition(coordinateMenuCorridore[0], Console.CursorTop);
            Console.WriteLine("R) Resume");
            
            Console.SetCursorPosition(coordinateMenuCorridore[0], Console.CursorTop);
            Console.WriteLine("J) Join");

            Console.SetCursorPosition(coordinatePrecedenti[0], coordinatePrecedenti[1]);
        }
        
        static void EseguiAzione(char azione, Thread tCorridore)
        {

            // TODO: Completare le azioni
            switch (azione)
            {
                case 'J':
                    break;
                case 'K':
                    break;
                case 'S':
                    break;
                case 'R':
                    break;
            }

        }

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Thread tAndrea = new Thread(Andrea);

            Thread tBaldo = new Thread(Baldo);


            Thread tCarlo = new Thread(Carlo);

            Pronti();

            StampaMenuPrincipale();

            while (true)
            {
                // TODO: Sistemare un po' il codice
                char c;
                do
                {
                    c = Console.ReadKey(true).KeyChar;
                    if (c >= 97 && c <= 99)
                        c = (char)(c - 32);
                } while (c < 65 || c > 67);

                Thread corridoreScelto = null;
                switch (c)
                {
                    case 'A':
                        corridoreScelto = tAndrea;
                        break;
                    case 'B':
                        corridoreScelto = tBaldo;
                        break;
                    case 'C':
                        corridoreScelto = tCarlo;
                        break;
                }

                StampaMenuCorridore(corridoreScelto);

                char a;
                do
                {
                    // Sommando 32 si ottiene la minuscola
                    // J = 74
                    // K = 75
                    // S = 83
                    // R = 82

                    a = Console.ReadKey(true).KeyChar;
                    if (a >= 97)
                        a = (char)(a - 32);
                } while (a != 74 && a != 75 && a != 82 && a != 83);


                
                
                
                
            }


            //// Stampa iniziale
            //StampaStatiThread(tAndrea, tBaldo, tCarlo);

            //Console.SetCursorPosition(0, 14);
            //Console.WriteLine("\n\nClicca un tasto per far partire i corridori");
            //Console.ReadKey(true);
            //Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
            //Console.WriteLine("                                                                 ");

            //// Essendo la Console una risorsa condivisa, non possono scrivere tutti allo stesso tempo,
            //// c'è bisogno di un semaforo che "guidi" la scrittura.


            //tAndrea.Start(); // Esegue il codice nel metodo che gli abbiamo passato
            //tBaldo.Start();
            //tCarlo.Start();

            //// Stampa in tempo reale
            //while (tAndrea.IsAlive || tBaldo.IsAlive || tCarlo.IsAlive)
            //{
            //    StampaStatiThread(tAndrea, tBaldo, tCarlo);
            //}

            //// Stampa finale
            //StampaStatiThread(tAndrea, tBaldo, tCarlo);

            Console.SetCursorPosition(0, coordinateMenu[1]+6);
            Console.WriteLine("\t\nProgramma finito, premi invio per terminare");
            Console.ReadLine();
        }


        // Il codice era ridondante e quindi l'ho inserito in un metodo
        static void StampaStatiThread(Thread tAndrea, Thread tBaldo, Thread tCarlo)
        {
            StampaStatoThreadEIsAlive(tAndrea, 10, 2, _lock);
            StampaStatoThreadEIsAlive(tBaldo, 10, 2 + 4, _lock);
            StampaStatoThreadEIsAlive(tCarlo, 10, 2 + 8, _lock);
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
