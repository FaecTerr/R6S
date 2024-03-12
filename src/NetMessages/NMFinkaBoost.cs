using System;
using System.Collections.Generic;


namespace DuckGame.R6S
{
    public class NMFinkaBoost : NMEvent
    {
        public string team;
        public float CDT;

        public NMFinkaBoost()
        {
        }

        public NMFinkaBoost(string t, float cooldownTime)
        {
            team = t;
            CDT = cooldownTime;
        }

        public override void Activate()
        {
            if (team != null)
            {
                foreach (Operators op in Level.current.things[typeof(Operators)])
                {
                    if (op.team == team)
                    {
                        if (op.DBNO)
                        {
                            op.resetFromDBNO();
                        }
                        if (op.flashImmuneFrames < 30)
                        {
                            op.flashImmuneFrames = 30;
                        }
                        if (!op.HasEffect("Overhealed"))
                        {
                            op.effects.Add(new Overheal() { timer = CDT, maxTimer = CDT, team = team });
                        }
                        else
                        {
                            op.GetEffect("Overhealed").timer += CDT;
                        }
                    }
                }
            }
        }
    }
}