using System;
using System.Security.Cryptography;
using System.Threading;

namespace ConsoleApp_CorsaFraAmici
{
    class Program
    {
        static int posAndrea = 0;
        static int posBaldo = 0;
        static int posCarlo = 0;

        static int classifica = 0;

        static object _lockConsole = new object();
        static object _lockJoin = new object();

        static Thread[] _persone = new Thread[2];

        static int[] coordinateMenu = { 0, 17 };
        
        static Thread tAndrea = new Thread(Andrea) { Name = "Andrea" };
        static Thread tBaldo = new Thread(Baldo) { Name = "Baldo" };
        static Thread tCarlo = new Thread(Carlo) { Name = "Carlo" };

        static bool corridorePerJoin = false;
        static Thread tCorridoreSelezionato = null;


        static void ScriviCondiviso(object lck, int posX, int posY, string text)
        {
            lock (lck)
            {
                Console.SetCursorPosition(posX, posY);
                Console.Write(text);
            }
        }

        #region Corridori
        static void Andrea()
        {
            int testaAndrea = 3;
            do
            {
                ControlloJoin();

                posAndrea++;
                // @"  A",
                // @" /║\",
                // @"  ╙",

                StampaCorpo(posAndrea, testaAndrea, @"  A", @" /║\", @"  ╙",10);
            
            } while (posAndrea < 115);

            StampaClassifica(115, testaAndrea);
        }
        static void Baldo()
        {
            int testaBaldo = 7;
            do
            {
                ControlloJoin();

                posBaldo++;
                // @"  B",
                // @" └║┘",
                // @"  ╨"

                StampaCorpo(posBaldo, testaBaldo, @"  B", @" └║┘", @"  ╨", 10);

            } while (posBaldo < 115);
            
            StampaClassifica(115, testaBaldo);

        }

        static void Carlo()
        {
            int testaCarlo = 11;
            do
            {
                ControlloJoin();
                // @"  C"
                // @" /|\"
                // @" / \"
                posCarlo++;

                StampaCorpo(posCarlo, testaCarlo, @"  C", @" /|\", @" / \",10);

            } while (posCarlo < 115);

            StampaClassifica(115, testaCarlo);

        }

        #endregion

        
        private static void ControlloJoin()
        {
            // _persone[0] = Chi deve aspettare
            // _persone[1] = Chi deve essere aspettato
            
            if (_persone[0] == _persone[1])
            {
                lock (_lockJoin) // lock perché _persone è una variabile condivisa tra più Thread
                {
                    _persone[0] = null;
                    _persone[1] = null;
                }

                return; // Se entrambe le posizioni hanno lo stesso Thread esci dal metodo.
            }
            
            // Se il thread che chiama questo metodo è al primo posto nell'array
            // e il secondo posto non è null, allora aspetta il secondo thread.
            if (_persone[0] == Thread.CurrentThread && _persone[1] != null)
            {
                Thread temp = _persone[1];
                
                lock (_lockJoin)
                {
                    _persone[0] = null;
                    _persone[1] = null;
                }   

                temp.Join(); 
            }
            
            
        }

        static void Pronti()
        {
            // Metto ciò che voglio stampare in un array, per poi stampare tutto con un singolo ciclo
            
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

            // Stampo le stringhe
            for (int i = 2; i < 14; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(stringhe[i - 2]);
            }
        }

        

        #region Metodi stampa

        #region Stampa Corridori
        /// <summary>
        /// Stampa la classifica
        /// </summary>
        /// <param name="coordTraguardo">Coordinata X del traguardo</param>
        /// <param name="testaCorridore">Coordinata Y del corridore</param>
        static void StampaClassifica(int coordTraguardo, int testaCorridore)
        {
            lock (_lockConsole)
            {
                classifica++;
            }

            ScriviCondiviso(_lockConsole, coordTraguardo, testaCorridore - 1, classifica.ToString());
        }

        /// <summary>
        /// Stampa un corridore
        /// </summary>
        /// <param name="x">Distanza dal bordo sinistro</param>
        /// <param name="y">Distanza dal bordo alto</param>
        /// <param name="testa">Testa del corridore da stampare</param>
        /// <param name="corpo">Corpo del corridore da stampare</param>
        /// <param name="gambe">Gambe del corridore da stampare</param>
        /// <param name="delayPerOgniParte">Tempo di attesa per la stampa di ogni parte del corpo (millisecondi)</param>
        static void StampaCorpo(int x, int y, string testa, string corpo, string gambe, int delayPerOgniParte)
        {
            Thread.Sleep(delayPerOgniParte);
            ScriviCondiviso(_lockConsole, x, y + 2, gambe);

            Thread.Sleep(delayPerOgniParte);
            ScriviCondiviso(_lockConsole, x, y + 1, corpo);

            Thread.Sleep(delayPerOgniParte);
            ScriviCondiviso(_lockConsole, x, y, testa);
        }
        #endregion

        #region Stampa Menu
        static void StampaMenuPrincipale()
        {
            ScriviCondiviso(_lockConsole, coordinateMenu[0], coordinateMenu[1], " MENU'                                                                                                      \n");
            ScriviCondiviso(_lockConsole, coordinateMenu[0] + 1, coordinateMenu[1] + 1, " |- A) Andrea                                                                                                   \n");
            ScriviCondiviso(_lockConsole, coordinateMenu[0] + 1, coordinateMenu[1] + 2, " |- B) Baldo                                                                                                    \n");
            ScriviCondiviso(_lockConsole, coordinateMenu[0] + 1, coordinateMenu[1] + 3, " |- C) Carlo                                                                                                    \n                                                                                                    ");
        }

        
        static void StampaMenuCorridore(string nome)
        {
            int[] coordinateMenuCorridore = { coordinateMenu[0]+25, coordinateMenu[1] };

            ScriviCondiviso(_lockConsole, coordinateMenuCorridore[0], coordinateMenuCorridore[1], "Seleziona azione da compiere su " + nome + "     ");

            ScriviCondiviso(_lockConsole, coordinateMenuCorridore[0], coordinateMenuCorridore[1]+1, "S) Suspend");
            ScriviCondiviso(_lockConsole, coordinateMenuCorridore[0], coordinateMenuCorridore[1]+2, "K) Abort");
            ScriviCondiviso(_lockConsole, coordinateMenuCorridore[0], coordinateMenuCorridore[1]+3, "R) Resume");
            ScriviCondiviso(_lockConsole, coordinateMenuCorridore[0], coordinateMenuCorridore[1]+4, "J) Join");

        }

        static void StampaMenuJoin()
        {
            int[] coordinateMenuJoin = { coordinateMenu[0] + 73, coordinateMenu[1] };
            
            ScriviCondiviso(_lockConsole, coordinateMenuJoin[0], coordinateMenuJoin[1], $"Chi vuoi che {_persone[0].Name} aspetti?");
            ScriviCondiviso(_lockConsole, coordinateMenuJoin[0], coordinateMenuJoin[1]+1, "A) Andrea");
            ScriviCondiviso(_lockConsole, coordinateMenuJoin[0], coordinateMenuJoin[1]+2, "B) Baldo");
            ScriviCondiviso(_lockConsole, coordinateMenuJoin[0], coordinateMenuJoin[1]+3, "C) Carlo");
        }
        #endregion

        #region Stampa Stati Thread
        // Il codice era ridondante e quindi l'ho inserito in un metodo
        
        static void StampaStatiThread(Thread tAndrea, Thread tBaldo, Thread tCarlo)
        {
            StampaStatoThreadEIsAlive(tAndrea, 10, 2, _lockConsole);
            StampaStatoThreadEIsAlive(tBaldo, 10, 2 + 4, _lockConsole);
            StampaStatoThreadEIsAlive(tCarlo, 10, 2 + 8, _lockConsole);
        }

        static void StampaStatoThreadEIsAlive(Thread th, int posX, int posY, object lck)
        {
            ScriviCondiviso(lck, posX, posY, $"Stato thread: {th.ThreadState}                               ");
            ScriviCondiviso(lck, posX + 60, posY, $"IsAlive: {th.IsAlive}      ");
        }
        #endregion

        #endregion



        [Obsolete]
        static void EseguiAzione(char azione, Thread tCorridore)
        {
            if (!tCorridore.IsAlive)
                return;
            
            // TODO: Completare le azioni
            switch (azione)
            {
                case 'J':
                    corridorePerJoin = true;
                    StampaMenuJoin();
                    break;
                case 'K':
                    if (tCorridore.ThreadState == ThreadState.Suspended)
                        tCorridore.Resume();
                    
                    lock (_lockConsole)
                    {
                        tCorridore.Abort();
                    }
                    break;
                case 'S':
                    if (tCorridore.ThreadState == ThreadState.Suspended || !tCorridore.IsAlive)
                        return;
                    lock (_lockConsole)
                    {
                        tCorridore.Suspend();
                    }
                    break;
                case 'R':
                    if (tCorridore.ThreadState != ThreadState.Suspended || !tCorridore.IsAlive) 
                        return;
                        tCorridore.Resume();
                    break;
                    
            }


        }

        

        


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

                if (c == 'A' || c == 'B' || c == 'C')
                {
                    string nomeCorridore = "";
                    switch (c)
                    {
                        case 'A':
                            tCorridoreSelezionato = tAndrea;
                            nomeCorridore = "Andrea";
                            break;
                        case 'B':
                            tCorridoreSelezionato = tBaldo;
                            nomeCorridore = "Baldo";
                            break;
                        case 'C':
                            tCorridoreSelezionato = tCarlo;
                            nomeCorridore = "Carlo";
                            break;
                    }

                    if (corridorePerJoin)
                    {
                        _persone[1] = tCorridoreSelezionato;
                        corridorePerJoin = false;

                        StampaMenuPrincipale();

                    }
                    else
                    {
                        Thread tSelezionatoVecchio = tCorridoreSelezionato;
                        
                        if (tCorridoreSelezionato.IsAlive)
                        {
                            StampaMenuCorridore(nomeCorridore);
                            corridoreScelto = true;
                        }
                        else
                        {
                            tCorridoreSelezionato = tSelezionatoVecchio;
                        }

                        _persone[0] = tCorridoreSelezionato;
                    }
                }
                else if (corridoreScelto && (c == 'J' || c == 'K' || c == 'S' || c == 'R'))
                {
                    EseguiAzione(c, tCorridoreSelezionato);
                    if (!corridorePerJoin)
                        StampaMenuPrincipale();
                    corridoreScelto = false;
                }
              
            } while (true);
        }

        [Obsolete] // Obsolete è per evitare di avere la segnalazione che t.Suspend() e t.Resume() sono obsoleti
        static void Main(string[] args)
        {
            Console.CursorVisible = false;


            Thread tMenu = new Thread(GestisciMenu);
            Pronti();

            tMenu.Start();



            // Stampa iniziale
            StampaStatiThread(tAndrea, tBaldo, tCarlo);


            ScriviCondiviso(_lockConsole, 0, 16, "Clicca un tasto per far partire i corridori\n");
            Console.ReadKey(true);
            ScriviCondiviso(_lockConsole, 0, 16, "                                                                         ");

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


            tMenu.Abort(); // Per evitare rimanga in esecuzione anche dopo il termine della corsa

            Console.SetCursorPosition(0, coordinateMenu[1] + 6);
            Console.WriteLine("\t\nProgramma finito, premi invio per terminare");
            Console.ReadLine();
        }

    }
}
