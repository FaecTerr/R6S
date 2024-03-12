using System;
using System.Collections.Generic;


namespace DuckGame.R6S
{
    public class NMLaunchers : NMEvent
    {
        public Launchers l;
        public string s;

        public NMLaunchers()
        {
        }

        public NMLaunchers(Launchers lau, string state)
        {
            l = lau;
            s = state;
        }

        public override void Activate()
        {
            if (l != null && s != null)
            {
                if (s == "M1")
                {
                    l.LaunchMissile();
                }
                if (s == "M2")
                {
                    l.LaunchMissile1();
                }
            }
        }
    }
}