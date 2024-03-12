using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Devices|IHUD")]
    public class EEONED : Device
    {
        public float reload;

        public EEONED(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/FinkaBoost.png"), 16, 16, false);
            graphic = _sprite;
            center = new Vec2(8f, 8f);
            collisionOffset = new Vec2(-8f, -8f);
            collisionSize = new Vec2(16f, 16f);
            weight = 0.9f;

            UsageCount = 3;

            ShowCooldown = true;
            ShowCounter = true;

            CooldownTime = 20;
            Cooldown = 0;

            index = 31;
        }

        public override void Update()
        {
            base.Update();

            if (Cooldown > 0)
            {
                Cooldown -= 0.01666666f / CooldownTime;
            }

            if (oper != null)
            {
                if (Cooldown <= 0 && UsageCount > 0)
                {
                    if (oper.local)
                    {
                        PlayerStats.renown += 10;
                        PlayerStats.Save();
                        Level.Add(new RenownGained() { description = "", amount = 10 });
                    }

                    UsageCount--;
                    Cooldown = 1;
                    foreach (Operators op in Level.current.things[typeof(Operators)])
                    {
                        if (op.team == team)
                        {

                        }
                    }
                    oper.BackToWeapon(30);
                }
                else
                {
                    oper.BackToWeapon(30);
                }
            }
        }
    }
}
