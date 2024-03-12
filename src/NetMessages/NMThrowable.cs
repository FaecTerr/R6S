using System;
using System.Collections.Generic;


namespace DuckGame.R6S
{
    public class NMThrowable : NMEvent
    {
        public Throwable r;
        public string s;
        public string t;

        public NMThrowable()
        {
        }

        public NMThrowable(Throwable rock, string state, string team)
        {
            r = rock;
            s = state;
            t = team;
        }

        public override void Activate()
        {
            if(r != null && s != null)
            {
                if (s == "Detonate")
                {
                    r.team = t;
                    r.Detonate();
                }
                if(s == "Throw")
                {
                    //r.Throw();
                }
            }
        }
    }
}