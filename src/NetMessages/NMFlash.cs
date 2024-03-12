using System;
using System.Collections.Generic;


namespace DuckGame.R6S
{
    public class NMFlash : NMEvent
    {
        public Vec2 pos;

        public NMFlash()
        {
        }

        public NMFlash(Vec2 p)
        {
            pos = p;
        }

        public override void Activate()
        {
            if (pos != null)
            {
                Level.Add(new Flashlight(pos.x, pos.y));
            }
        }
    }
}