using System;
using System.Collections.Generic;


namespace DuckGame.R6S
{
    public class NMHoldAngle : NMEvent
    {
        public float direction;
        public sbyte _offDir;
        public Operators oper;
        public Holdable h;
        public Holdable h2;
        
        public NMHoldAngle()
        {
        }

        public NMHoldAngle(float dir, sbyte offDir, Operators op, Holdable hold, Holdable hold2 = null)
        {
            direction = dir;
            _offDir = offDir;
            oper = op;
            h = hold;
            h2 = hold2;
        }

        public override void Activate()
        {
            if (oper != null && h != null)
            {
                oper.holdAngle = direction;
                oper.offDir = _offDir;

                h.position = oper.position + oper.handPosition;
                h.offDir = oper.offDir;
                h._offDir = _offDir;
                h.flipHorizontal = oper.offDir == -1;
                h.angle = oper.holdAngle;

                if (h is BallisticShield)
                {
                    if ((h as BallisticShield).opened)
                    {                        
                        h.angle = 3.14f - 1.57f * _offDir;
                    }
                }
                else
                {
                    h.angle = oper.holdAngle;
                }


                if (h2 != null)
                {
                    h2.position = oper.position + oper.hand2Position;
                    h2.offDir = oper.offDir;
                    h2._offDir = _offDir;
                    h2.flipHorizontal = oper.offDir == -1;
                    h2.angle = oper.holdAngle;
                }
                
            }
        }
    }
}