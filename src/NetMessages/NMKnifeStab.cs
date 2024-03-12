using System;
using System.Collections.Generic;


namespace DuckGame.R6S
{
    public class NMKnifeStab : NMEvent
    {
        public Operators oper;
        //public Vec2 targetPos;

        public NMKnifeStab()
        {

        }

        public NMKnifeStab(Operators o/*, Vec2 target*/)
        {
            oper = o;
            //targetPos = target;
        }

        public override void Activate()
        {
            if (oper != null)
            {
                oper.KnifeStab();
            }
        }
    }
}