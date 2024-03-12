using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class PingedByAlibi : Effect
    {
        public float pingTime = 1f;

        public PingedByAlibi()
        {
            team = "Def";
            type = 3;
            name = "PingedByAlibi";
            timer = 0;
            maxTimer = pingTime;

            removeOnEnd = false;
            charge = 5f;
        }

        public override void TimerOut()
        {
            base.TimerOut();
            charge--;
            if (owner != null)
            {
                timer = pingTime;
                float t = pingTime;
                if(charge == 0)
                {
                    t = pingTime * 3;
                }
                Level.Add(new Ping(owner.position.x, owner.position.y) { lifetime = t, fram = 1 });
            }
        }

        public override void Update()
        {
            if (owner != null)
            {
                if (charge <= 0)
                {
                    timer = 0;
                    removeOnEnd = true;                                    
                    
                }
            }
            base.Update();
        }
    }
}

