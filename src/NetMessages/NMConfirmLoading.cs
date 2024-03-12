using System;
using System.Collections.Generic;


namespace DuckGame.R6S
{
    public class NMConfirmLoading : NMEvent
    {
        public NMConfirmLoading()
        {
        }

        public override void Activate()
        {
            foreach (GamemodeScripter gm in Level.current.things[typeof(GamemodeScripter)])
            {
                gm.loaded += 1;
            }
        }
    }
}