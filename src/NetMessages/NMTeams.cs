using System;
using System.Collections.Generic;


namespace DuckGame.R6S
{
    public class NMTeam : NMEvent
    {
        public int team = 0;
        public Profile profile;
        

        public NMTeam()
        {

        }

        public NMTeam(int t, Profile p)
        {
            team = t;
            profile = p;
        }

        public override void Activate()
        {
            if (profile != null)
            {
                foreach (GamemodeScripter gm in Level.current.things[typeof(GamemodeScripter)])
                {
                    //gm.loaded += 1;
                    if (team == 0)
                    {
                        gm.defenders += 1;
                        R6S.upd.teamAqua.Add(profile);
                    }
                    if (team == 1)
                    {
                        gm.attackers += 1;
                        R6S.upd.teamMagma.Add(profile);
                    }
                }
            }
        }
    }
}