using System;
using System.Collections.Generic;


namespace DuckGame.R6S
{
    public class NMGamemodeEvent : NMEvent
    {
        public GamemodeScripter gm;
        public string e = "";
        public float t;


        public NMGamemodeEvent()
        {
        }

        public NMGamemodeEvent(GamemodeScripter scripter, string Event, float time)
        {
            gm = scripter;
            e = Event;
            t = time;
        }

        public override void Activate()
        {
            if (gm != null)
            {
                if(e == "plant")
                {
                    if (!gm.planted && gm.currentPhase == 4)
                    {
                        gm.planted = true;
                        gm.time = t;
                    }
                }
                if(e == "defuse")
                {
                    gm.defused = true;
                }
            }
        }
    }
}