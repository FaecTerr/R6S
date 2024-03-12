using System;
using System.Collections.Generic;


namespace DuckGame.R6S
{
    public class NMSonicBurst : NMEvent
    {
        public Vec2 pos;

        public NMSonicBurst()
        {
        }

        public NMSonicBurst(Vec2 p)
        {
            pos = p;
        }

        public override void Activate()
        {
            foreach (YokaiAP y in Level.current.things[typeof(YokaiAP)])
            {
                y.SonicBurst(pos);
            }
        }
    }
}