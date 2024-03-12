using System;
using System.Collections.Generic;


namespace DuckGame.R6S
{
    public class NMHitScan : NMEvent
    {
        public ulong index = 255;

        public NMHitScan()
        {
        }

        public NMHitScan(ulong netIndex)
        {
            index = netIndex;
        }

        public override void Activate()
        {
            if (index < 200)
            {
                foreach (Operators oper in Level.current.things[typeof(Operators)])
                {
                    if (oper.netIndex == index)
                    {
                        oper.HitScan();
                    }
                }
            }
        }
    }
}