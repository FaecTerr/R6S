using System;
using System.Collections.Generic;


namespace DuckGame.R6S
{
    public class NMExplosion : NMEvent
    {
        public float rad;
        public int damage;
        public string bType;
        public Vec2 pos;
        public Operators op;

        public NMExplosion()
        {
        }

        public NMExplosion(Vec2 p, float r, int d, string type, Operators oper = null)
        {
            pos = p;
            rad = r;
            damage = d;
            bType = type;
            op = oper;
        }

        public override void Activate()
        {
            if (bType != null && bType != "")
            {
                Level.Add(new Explosion(pos.x, pos.y, rad, damage, bType) { shootedBy = op});
            }
        }
    }
}