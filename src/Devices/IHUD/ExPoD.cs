using System;
using System.Collections.Generic;

namespace DuckGame.R6S
{
    public class ExPoD : Device
    {
        public bool enabled;
        public bool disabled;
        public float sinceactivation;

        public ExPoD(float xpos, float ypos) : base(xpos, ypos)
        {
            requiresTakeOut = false;
        }
        public override void PocketActivation()
        {
            base.PocketActivation();
            if (!enabled && Cooldown <= MinimalCooldownValue)
            {

            }
            else
            {
                enabled = !enabled;
                if (enabled)
                {
                    if (user != null && user.HasEffect("Jammed") && user.HasEffect("EMP'd"))
                    {
                        enabled = false;
                    }
                    else
                    {
                        Cooldown -= CooldownTakeoffOnActivation;
                    }
                }
            }
        }
        public override void Update()
        {
            UsageCount = 59;
            base.Update();


            if (Cooldown <= 0)
            {
                if (enabled)
                {
                    enabled = false;
                }
            }

            if (enabled)
            {
                ApplyEffect();
                Cooldown -= 0.01666666f / CooldownTime;
                if (sinceactivation < 1)
                {
                    sinceactivation += 0.02f;
                }
            }
            else
            {
                WearOffEffect();

                if (Cooldown < 1 && sinceactivation <= 0)
                {
                    Cooldown += 0.01666666f / CooldownTime * CooldownRestorationModifier;
                }

                if (sinceactivation > 0)
                {
                    sinceactivation -= 0.01f;
                }
            }

            if (user != null)
            {
                if (user.isDead)
                {
                    if (enabled)
                    {
                        enabled = false;
                    }
                }
                if (user.HasEffect("Jammed") || user.HasEffect("EMP'd"))
                {
                    enabled = false;
                }
            }
        }

        public virtual void ApplyEffect()
        {

        }
        public virtual void WearOffEffect()
        {

        }
    }
}
