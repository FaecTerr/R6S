using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class StairsCooldown : Effect
    {
        public StairsCooldown(float time = 15)
        {
            type = 1;
            name = "ArmorCooldown";
            timer = time;
            maxTimer = time;
        }

        public override void Update()
        {
            base.Update();
        }
    }
}

