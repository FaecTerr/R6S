using System;
using System.Collections.Generic;


namespace DuckGame.R6S
{
    public class NMChangeOper : NMEvent
    {
        public Duck d;
        public NMChangeOper()
        {

        }

        public NMChangeOper(Duck duck)
        {
            d = duck;
        }

        public override void Activate()
        {
            if (d != null)
            {
                OPEQ opeq = null;
                foreach (OPEQ op in Level.current.things[typeof(OPEQ)])
                {
                    if (d.HasEquipment(op))
                    {
                        opeq = op;
                    }
                }
                foreach (GamemodeScripter gm in Level.current.things[typeof(GamemodeScripter)])
                {
                    if (opeq != null && gm.selectedOperators.Contains(opeq))
                    {
                        gm.selectedOperators.Remove(opeq);
                    }
                }
                if (opeq != null)
                {
                    d.Unequip(opeq);
                    Level.Remove(opeq);
                }
            }
        }
    }
}