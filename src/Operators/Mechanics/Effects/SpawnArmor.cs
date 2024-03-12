using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class SpawnArmor : Effect
    {
        public SpawnArmor(float time = 15)
        {
            type = 1;
            name = "SpawnArmor";
            timer = time;
            maxTimer = time;
        }

        public override void Update()
        {
            base.Update();
        }
    }
}

