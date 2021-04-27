using System;
using System.Threading;

namespace ThreadDueRaceCondition
{
    class Program
    {
        static void Main(string[] args)
        {
            OggettoCondiviso obj = new OggettoCondiviso(100);
            Codice c1 = new Codice(obj);
            Codice c2 = new Codice(obj);
            Thread th1 = new Thread(new ThreadStart(c2.Attività));
            Thread th2 = new Thread(new ThreadStart(c1.Attività));
            th1.Name = "Primo";
            th2.Name = "Secondo";
            th1.Start(); //il main thread chiede al S.O. di creare th
            th2.Start();
            
            while (!th1.IsAlive && !th2.IsAlive) ;
            Thread.Sleep(10);
            th1.Join();
            th2.Join();
            Console.WriteLine("main thread è terminato così come th1 e th2");
        }
    }
}