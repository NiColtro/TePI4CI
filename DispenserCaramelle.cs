using System;
using System.Threading;

namespace Thread_Caramelle {

    class Dispenser {

        public int numeroCaramelle;
        public int[] contatoreCompagni;

        public Dispenser() {
            numeroCaramelle = 100;
            contatoreCompagni = new int[5]{ 0, 0, 0, 0, 0 };
        }

        public bool prelevaCaramella() {
            lock (this) {
                if (numeroCaramelle > 0) {
                    numeroCaramelle--;
                    // Console.WriteLine("[?] Mancano " + numeroCaramelle + " caramelle.");
                    return true;
                }

                return false;
            }
        }
    }

    class Compagno {

        private Dispenser dispenser;

        public Compagno(Dispenser dispenser) {
            this.dispenser = dispenser;
        }

        public void Azione() {
            while (dispenser.prelevaCaramella()) {
                
                Console.WriteLine("[-] Compagno " + Thread.CurrentThread.Name + " ha prelevato una caramella.");
                dispenser.contatoreCompagni[Int32.Parse(Thread.CurrentThread.Name) - 1]++;
            }
        }
    }

    class Program {
        static void Main(string[] args) {

            Dispenser dispenser = new Dispenser();

            Compagno c = new Compagno(dispenser);

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

            while (!th1.IsAlive && !th2.IsAlive && !th3.IsAlive && !th4.IsAlive && !th5.IsAlive) {}

            th1.Join();
            th2.Join();
            th3.Join();
            th4.Join();
            th5.Join();

            Console.WriteLine("\n\n[!] Le caramelle sono finite.");
            for (int i = 0; i < 5; i++)
                Console.WriteLine("[Compagno " + (i + 1) + "] Ho raccolto " + dispenser.contatoreCompagni[i] + " caramelle.");
        }
    }
}