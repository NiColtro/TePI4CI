using System;

namespace ThreadDueRaceCondition
{
    class OggettoCondiviso
    {
        private int d;

        public OggettoCondiviso(int d)
        {
            this.d = d;
        }

        public int Valore
        {
            get { return d; }
        }

        public int Dec()
        {
            --d;
            return d;
        }
    }
}