using System;
using System.Collections.Generic;


namespace DuckGame.R6S
{
    public class NMBlitzFlash : NMEvent
    {
        public ulong netIndex;
        
        public NMBlitzFlash()
        {
        }

        public NMBlitzFlash(ulong net = 255)
        {
            netIndex = net;
        }

        public override void Activate()
        {
            if(netIndex < 255)
            {
                foreach (Operators op in Level.current.things[typeof(Operators)])
                {
                    if(op.netIndex == netIndex && !op.local && op.holdObject is FlashShield)
                    {
                        (op.holdObject as FlashShield).Flash();
                    }
                }
            }
        }
    }
}