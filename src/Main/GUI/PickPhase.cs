using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class PickPhase : Thing
    {
        public SpriteMap attackers;
        public SpriteMap defenders;
        public int team;
        public PickPhase(int t) : base()
        {
            team = t;
        }
        public override void Draw()
        {
            base.Draw();
            if (team == 1)
            {
                Graphics.Draw(attackers, 0, 0);
            }
            else
            {
                Graphics.Draw(defenders, 0, 0);
            }
        }
    }
}
