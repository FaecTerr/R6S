using System;
using System.Collections.Generic;


namespace DuckGame.R6S
{
    public class NMTrackedByJackal : NMEvent
    {
        public ulong netIndex = 255;
        public int level;

        public NMTrackedByJackal()
        {

        }

        public NMTrackedByJackal(ulong index, int lvl)
        {
            netIndex = index;
            level = lvl;
        }

        public override void Activate()
        {
            if (netIndex < 255)
            {
                foreach (Operators o in Level.current.things[typeof(Operators)])
                {
                    if (o.netIndex == netIndex)
                    {
                        o.effects.Add(new SpottedEffect() { timer = 2.5f * level, maxTimer = 2.5f * level });
                    }
                }
            }
        }
    }
}