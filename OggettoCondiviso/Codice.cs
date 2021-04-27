using System;
using System.Threading;

namespace ThreadDueRaceCondition
{
    class Codice
    {
        private OggettoCondiviso obj;

        public Codice(OggettoCondiviso obj)
        {
            this.obj = obj;
        }

        public void Attività()
        {
            Console.WriteLine(Thread.CurrentThread.Name + " è stato creato");
            int valore;
            do
            {
                valore = obj.Valore;
                if (valore > 0)
                {
                    Console.WriteLine("mancano " + valore + " cicli");
                    obj.Dec();
                }
            } while (valore > 0);

            Console.WriteLine(Thread.CurrentThread.Name + " termina");
        }
    }
}