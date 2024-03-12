using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class CCEstun : Effect
    {
        public CCEstun()
        {
            team = "Def";
            type = 3;
            name = "CCEstun";
            timer = 0.0f;
            maxTimer = 2.0f;

            removeOnEnd = true;
            charge = 0f;
        }

        public override void Update()
        {
            if(timer > maxTimer)
            {
                timer = maxTimer;
            }
            if(charge > 0)
            {
                charge--;
            }
            else
            {
                charge = 30;
            }
            if(charge == 1)
            {
                if (owner != null)
                {
                    owner.unableToSprint = 15;
                    owner.hSpeed *= 0.9f;
                }
            }
            base.Update();
        }
    }
}

