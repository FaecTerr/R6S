using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class Overheal : Effect
    {
        int power = 25;
        public Overheal()
        {
            effectType = EffectType.Temporary;
            name = "Overhealed";
            timer = 2;
            maxTimer = 2;
        }
        public Overheal(int heal)
        {
            power = heal;
        }

        public override void Update()
        {
            if (owner != null)
            {
                if(owner.Health < 100)
                {
                    timer = 0;
                    owner.Health += power;
                }
            }
            base.Update();
        }
    }
}

