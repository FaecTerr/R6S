using System;
using System.Collections.Generic;


namespace DuckGame.R6S
{
    public class NMSmokeCanister : NMEvent
    {
        public Vec2 pos;
        public float time;

        public NMSmokeCanister()
        {
        }

        public NMSmokeCanister(Vec2 p, float t)
        {
            pos = p;
            time = t;
        }

        public override void Activate()
        {
            if (time > 0)
            {
                Level.Add(new GasSmoke(pos.x, pos.y, time) { team = "Def" });
            }
        }
    }
}