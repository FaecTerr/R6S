using System;
using System.Collections.Generic;


namespace DuckGame.R6S
{
    public class NMPhoneCalled : NMEvent
    {
        public string team;

        public NMPhoneCalled()
        {
        }

        public NMPhoneCalled(string t)
        {
            team = t;
        }

        public override void Activate()
        {
            if (team != null)
            {
                foreach (Operators op in Level.current.things[typeof(Operators)])
                {
                    if (op.team != team)
                    {
                        op.effects.Add(new PhoneCalled());
                    }
                }
            }
        }
    }
}