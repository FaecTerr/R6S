using System;
using System.Collections.Generic;


namespace DuckGame.R6S
{
    public class NMReinforce : NMEvent
    {
        public Vec2 forceTL;
        public Vec2 forceBR;
        public float dir;
        public NMReinforce()
        {
        }

        public NMReinforce(Vec2 surfaceTL, Vec2 surfaceBR, float direction)
        {
            forceTL = surfaceTL;
            forceBR = surfaceBR;
            dir = direction;
        }

        public override void Activate()
        {
            if((forceTL-forceBR).length > 1)
            {
                foreach (SurfaceStationary b in Level.CheckRectAll<SurfaceStationary>(forceTL, forceBR))
                {
                    b.Reinforce(dir);
                }
            }
        }
    }
}