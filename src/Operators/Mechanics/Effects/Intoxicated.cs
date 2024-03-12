using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class Intoxicated : Effect
    {
        public float damageTime = 2f;

        public Intoxicated()
        {
            team = "Def";
            type = 3;
            name = "Intoxicated";
            timer = damageTime;
            maxTimer = damageTime;

            removeOnEnd = false;
            charge = 1f;
        }

        public override void TimerOut()
        {
            base.TimerOut();
            if(owner != null)
            {
                owner.GetDamage(6);
                timer = damageTime;
            }
        }

        public override void Update()
        {
            if(owner != null)
            {
                owner.unableToSprint = 30;
                owner.priorityTaken = 4;
            }
            if(charge <= 0)
            {
                timer = 0;
                removeOnEnd = true;
            }
            base.Update();
        }
    }
}

