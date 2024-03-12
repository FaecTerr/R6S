using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    public class CCEblast : ExPoD
    {
        public CCEblast(float xval, float yval) : base(xval, yval)
        {
            UsageCount = 99;

            Cooldown = 1;
            CooldownTime = 4f;
            CooldownTakeoffOnActivation = 0f;
            MinimalCooldownValue = 0;

            ShowCooldown = true;
        } 
    }
}
