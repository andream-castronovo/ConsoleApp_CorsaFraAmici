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
        static object _lockThread = new object();

        // FATTO Stampare lo stato dei thread in tempo reale affianco al nome del corridore
        // FATTO Stampare lo stato di vita del thread da qualche parte
        // FATTO Fare metodo per scrivere che incorpora lock set cursor e write
        
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

                DisegnaCorpo(posAndrea, testaAndrea, @"  A", @" /║\", @"  ╙");

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
                DisegnaCorpo(posBaldo, testaBaldo, @"  B", @" └║┘", @"  ╨");

            } while (posBaldo < 115);

            lock (_lock)
            {
                classifica++;
                Console.SetCursorPosition(115, 6);
                Console.Write(classifica);
            }

        }

        private static void DisegnaCorpo(int x, int y, string testa, string corpo, string gambe)
        {
            Thread.Sleep(50);
            ScriviCondiviso(_lock, x, y + 2, gambe);

            Thread.Sleep(50);
            ScriviCondiviso(_lock, x, y + 1, corpo);

            Thread.Sleep(50);
            ScriviCondiviso(_lock, x, y, testa);
        }

        static void Carlo()
        {
            do
            {
                posCarlo++;
                int testaCarlo = 11;
                
                DisegnaCorpo(posCarlo, testaCarlo, @"  C", @" /|\", @" / \");

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
        
        // Abort per uccidere il corridore (K)
        // Suspend per fermare temporaneamente il corridore (S)
        // Resume per far riprendere il corridore (R)
        // Join per far aspettare un altro corridore a quello selezionato. (J)

        static void StampaMenuPrincipale()
        {
            int[] coordinatePrecedenti = { Console.CursorLeft, Console.CursorTop };

            ScriviCondiviso(_lock, coordinateMenu[0], coordinateMenu[1], " MENU'\n");
            ScriviCondiviso(_lock, coordinateMenu[0] + 1, Console.CursorTop, " |- A) Andrea\n");
            ScriviCondiviso(_lock, coordinateMenu[0] + 1, Console.CursorTop, " |- B) Baldo\n");
            ScriviCondiviso(_lock, coordinateMenu[0] + 1, Console.CursorTop, " |- C) Carlo\n");

            Console.SetCursorPosition(coordinatePrecedenti[0], coordinatePrecedenti[1]);
        }

        static void StampaMenuCorridore(char c)
        {
            int[] coordinatePrecedenti = { Console.CursorLeft, Console.CursorTop };

            int[] coordinateMenuCorridore = { coordinateMenu[0]+25, coordinateMenu[1] };

            string nomeCorridore = "";

            switch (c)
            {
                case 'A':
                    nomeCorridore = "Andrea";
                    break;
                case 'B':
                    nomeCorridore = "Baldo";
                    break;
                case 'C':
                    nomeCorridore = "Carlo";
                    break;
                default:
                    throw new Exception("Corridore non valido");
            }

            ScriviCondiviso(_lock, coordinateMenuCorridore[0], coordinateMenuCorridore[1], "Seleziona azione da compiere su " + nomeCorridore + "   \n");

            ScriviCondiviso(_lock, coordinateMenuCorridore[0], Console.CursorTop, "S) Suspend\n");
            ScriviCondiviso(_lock, coordinateMenuCorridore[0], Console.CursorTop, "K) Abort\n");
            ScriviCondiviso(_lock, coordinateMenuCorridore[0], Console.CursorTop, "R) Resume\n");
            ScriviCondiviso(_lock, coordinateMenuCorridore[0], Console.CursorTop, "J) Join\n");

            Console.SetCursorPosition(coordinatePrecedenti[0], coordinatePrecedenti[1]);
        }

        [Obsolete]
        static void EseguiAzione(char azione, Thread tCorridore)
        {
            if (!tCorridore.IsAlive)
                return;
            
            // TODO: Completare le azioni
            switch (azione)
            {
                // J Join ...

                // K Abort ✔
                // S Suspend ✔
                // R Resume ✔
                case 'J':
                    corridorePerJoin = true;
                    break;
                case 'K':
                    if (tCorridore.ThreadState == ThreadState.Suspended)
                        tCorridore.Resume();
                    tCorridore.Abort();
                    break;
                case 'S':
                    if (tCorridore.ThreadState == ThreadState.Suspended || !tCorridore.IsAlive)
                        return;
                    tCorridore.Suspend();
                    break;
                case 'R':
                    if (tCorridore.ThreadState != ThreadState.Suspended || !tCorridore.IsAlive) 
                        return;
                    tCorridore.Resume();
                    break;
                    
            }

        }
        [Obsolete]
        static void AzioneJoin(Thread tCorridoreCheAspetta, Thread tCorridoreDaAspettare)
        {
            if (!tCorridoreCheAspetta.IsAlive)
                return;

            tCorridoreCheAspetta.Suspend();

            while (tCorridoreDaAspettare.IsAlive)
            {

            }
               
            if (tCorridoreCheAspetta.ThreadState == ThreadState.Suspended || tCorridoreCheAspetta.ThreadState == ThreadState.SuspendRequested)
                tCorridoreCheAspetta.Resume();
            
            
            Thread.CurrentThread.Abort();
        }

        static Thread tAndrea = new Thread(Andrea) { Name = "Andrea" };
        static Thread tBaldo = new Thread(Baldo) { Name = "Baldo" };
        static Thread tCarlo = new Thread(Carlo) { Name = "Carlo" };

        [Obsolete]
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            

            Thread tMenu = new Thread(GestisciMenu);
            Pronti();

            tMenu.Start();


            #region Comment
            // Stampa iniziale
            StampaStatiThread(tAndrea, tBaldo, tCarlo);


            ScriviCondiviso(_lock, 0, 16, "Clicca un tasto per far partire i corridori\n");
            Console.ReadKey(true);
            ScriviCondiviso(_lock, 0, 16, "                                                                         ");

            // Essendo la Console una risorsa condivisa, non possono scrivere tutti allo stesso tempo,
            // c'è bisogno di un semaforo che "guidi" la scrittura.


            tAndrea.Start(); // Esegue il codice nel metodo che gli abbiamo passato
            tBaldo.Start();
            tCarlo.Start();

            // Stampa in tempo reale
            while (tAndrea.IsAlive || tBaldo.IsAlive || tCarlo.IsAlive)
            {
                StampaStatiThread(tAndrea, tBaldo, tCarlo);
            }

            // Stampa finale
            StampaStatiThread(tAndrea, tBaldo, tCarlo);
            #endregion

            tMenu.Abort();

            Console.SetCursorPosition(0, coordinateMenu[1]+6);
            Console.WriteLine("\t\nProgramma finito, premi invio per terminare");
            Console.ReadLine();
        }

        static Thread tCorridoreSelezionato;
        static Thread tCorridoreSelezionatoPerJoin;

        static bool corridorePerJoin = false;
        [Obsolete]
        static void GestisciMenu()
        {
            StampaMenuPrincipale();
            bool corridoreScelto = false;
            do
            {
                char c = Console.ReadKey(true).KeyChar;

                if (c >= 97)
                    c = (char)(c - 32);

                if (c >= 65 && c < 68)
                {
                    if (corridorePerJoin)
                    {
                        switch (c)
                        {
                            case 'A':
                                tCorridoreSelezionatoPerJoin = tAndrea;
                                break;
                            case 'B':
                                tCorridoreSelezionatoPerJoin = tBaldo;
                                break;
                            case 'C':
                                tCorridoreSelezionatoPerJoin = tCarlo;
                                break;
                        }

                        new Thread ( () => AzioneJoin(tCorridoreSelezionato, tCorridoreSelezionatoPerJoin)).Start();
                        corridorePerJoin = false;
                    }
                    else
                    {
                        Thread tSelezionatoVecchio = tCorridoreSelezionato;
                        switch (c)
                        {
                            case 'A':
                                tCorridoreSelezionato = tAndrea;
                                break;
                            case 'B':
                                tCorridoreSelezionato = tBaldo;
                                break;
                            case 'C':
                                tCorridoreSelezionato = tCarlo;
                                break;
                        }
                        if (tCorridoreSelezionato.IsAlive)
                        {
                            StampaMenuCorridore(c);
                            corridoreScelto = true;
                        }
                        else
                        {
                            tCorridoreSelezionato = tSelezionatoVecchio;
                        }
                    }
                }
                else if (corridoreScelto && (c == 'J' || c == 'K' || c == 'S' || c == 'R'))
                    EseguiAzione(c, tCorridoreSelezionato);
              
            } while (true);
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
                Console.Write($"Stato thread: {th.ThreadState}                               ");

            }    
            lock (lck)
            {
                Console.SetCursorPosition(posX + 60, posY);
                Console.Write($"IsAlive: {th.IsAlive}      ");
            }
        }

    }
}
