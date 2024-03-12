using System;
using System.Collections.Generic;


namespace DuckGame.R6S
{
    public class NMUpdateShieldState : NMEvent
    {
        public HandShield shield;
        public bool op;

        public NMUpdateShieldState()
        {
        }

        public NMUpdateShieldState(HandShield o, bool opened)
        {
            shield = o;
            op = opened;
        }

        public override void Activate()
        {
            if (shield != null)
            {
                shield.opened = op;
            }
        }
    }
}