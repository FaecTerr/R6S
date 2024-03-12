using System;
using System.Collections.Generic;


namespace DuckGame.R6S
{
    public class NMInvisForDrones : NMEvent
    {
        public bool enable;

        public NMInvisForDrones()
        {

        }

        public NMInvisForDrones(bool e)
        {
            enable = e;
        }

        public override void Activate()
        {
            foreach (Operators op in Level.current.things[typeof(Operators)])
            {
                if (op.MainDevice is InvisibilityForDrones)
                {
                    (op.MainDevice as InvisibilityForDrones).enabled = enable;
                }
            }
        }
    }
    public class NMHEL : NMEvent
    {
        public bool enable;

        public NMHEL()
        {

        }

        public NMHEL(bool e)
        {
            enable = e;
        }

        public override void Activate()
        {
            foreach (Operators op in Level.current.things[typeof(Operators)])
            {
                if (op.MainDevice is HEL)
                {
                    (op.MainDevice as HEL).enabled = enable;
                }
            }
        }
    }
}