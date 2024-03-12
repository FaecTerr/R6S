using System;
using System.Collections.Generic;


namespace DuckGame.R6S
{
    public class NMName : NMEvent
    {
        public Operators oper;
        public string n;

        public NMName()
        {
        }

        public NMName(Operators o, string name)
        {
            oper = o;
            n = name;
        }

        public override void Activate()
        {
            if (oper != null)
            {
                oper.name = n;
            }
        }
    }
}