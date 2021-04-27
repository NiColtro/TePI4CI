using System;
using System.Collections.Generic;
using System.Threading;

namespace Lotto {
    class Urna {
        public double montepremi;
        public bool resetFlag;

        private List<int> numeriEstratti;
        public List<int> numeriVincenti;
        private Random rnd = new Random();

        public Urna() {
            numeriEstratti = new List<int>();
            numeriVincenti = new List<int>();

            resetFlag = false;
            montepremi = 1000;
        }

        public List<int> estrai() {
            lock (this) {
                int estrazione;
                List<int> returnList = new List<int>();

                while (returnList.Count < 3) {
                    // Popola la lista con 3 items
                    estrazione = rnd.Next(1, 90); // Genera numero random

                    while (numeriEstratti.Contains(estrazione)) // Controlla se il numero è stato già estratto
                        estrazione = rnd.Next(1, 90);

                    returnList.Add(estrazione); // Aggiunge estrazione al return
                    numeriEstratti.Add(estrazione); // Aggiunge estrazione al pool globale
                }

                montepremi *= 1.20;
                return returnList;
            }
        }

        public void reset() {
            lock (this) {
                montepremi = 1000;
                numeriEstratti.Clear();
            }
        }
    }

    class Compagno {
        private Urna urna;

        public Compagno(Urna urna) {
            this.urna = urna;
        }

        public void Azione() {
            lock (this) {
                if (urna.resetFlag)
                    return;
                
                List<int> estrazione = urna.estrai();

                Console.Write("\n\n[!] Biglietto " + Thread.CurrentThread.Name + "\nNumeri estratti: ");
                estrazione.ForEach(x => Console.Write(x + " ")); // Printa i 3 numeri

                // Controlla se la giocata è vincente (= tutti e 3 i numeri nella lista vincente)
                bool vincente = estrazione.FindAll(x => urna.numeriVincenti.Contains(x)).Count >= 1;

                Console.WriteLine("\nGiocata vincente? " + (vincente ? "Si" : "No"));

                if (vincente) {
                    Console.WriteLine(">>> Hai vinto " + urna.montepremi + " euro <<<\n\n");
                    urna.resetFlag = true;
                    urna.reset();
                }
                else
                    Console.WriteLine("Hai perso. Il montepremi sale a " + urna.montepremi + " euro.\n\n");
            }
        }
    }

    class Program {
        static void Main(string[] args) {

            do {
                // Inizializza oggetti
                Urna urna = new Urna();
                Compagno c = new Compagno(urna);


                // Richiede i numeri vincenti e li aggiunge in lista
                Console.WriteLine("Inserisci i 3 numeri vincenti:");

                Console.Write("1) ");
                urna.numeriVincenti.Add(Int32.Parse(Console.ReadLine()));
                Console.Write("2) ");
                urna.numeriVincenti.Add(Int32.Parse(Console.ReadLine()));
                Console.Write("3) ");
                urna.numeriVincenti.Add(Int32.Parse(Console.ReadLine()));


                do {
                    // Creazione e gestione dei threads
                    Thread th1 = new Thread(c.Azione);
                    th1.Name = "1";

                    Thread th2 = new Thread(c.Azione);
                    th2.Name = "2";

                    Thread th3 = new Thread(c.Azione);
                    th3.Name = "3";

                    Thread th4 = new Thread(c.Azione);
                    th4.Name = "4";

                    Thread th5 = new Thread(c.Azione);
                    th5.Name = "5";

                    th1.Start();
                    th2.Start();
                    th3.Start();
                    th4.Start();
                    th5.Start();

                    while (!th1.IsAlive && !th2.IsAlive && !th3.IsAlive && !th4.IsAlive && !th5.IsAlive) {
                    }

                    th1.Join();
                    th2.Join();
                    th3.Join();
                    th4.Join();
                    th5.Join();
                    
                } while (!urna.resetFlag);

                urna.resetFlag = false;
            } while (1 == 1);
        }
    }
}