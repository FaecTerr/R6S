using System;
using System.Collections.Generic;


namespace DuckGame.R6S
{
    public class NMConfirmSelection : NMEvent
    {
        public NMConfirmSelection()
        {
        }

        public override void Activate()
        {
            foreach (GamemodeScripter gm in Level.current.things[typeof(GamemodeScripter)])
            {
                gm.confirmed += 1;
            }
        }
    }
}