using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class ThermalScope : Device
    {
        public bool inScope;
        int usings = 1;
        public ThermalScope(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/ADS.png"), 16, 16, false);
            graphic = _sprite;
            _sprite.frame = 0;
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(16f, 16f);
            collisionOffset = new Vec2(-8f, -8f);
            setTime = 0.8f;
            weight = 0.9f;
            thickness = 0.1f;
            placeable = false;
            breakable = true;
            zeroSpeed = false;
            index = 5;
        }

        public override void Update()
        {
            base.Update();
            if(oper != null)
            {
                if (oper.MainGun != null)
                {
                    inScope = !inScope;
                    oper.ChangeWeapon(30, 1);
                    if (inScope)
                    {
                        oper.MainGun.xScope = 3.5f;
                        if(oper.MainGun.ADS > 1)
                        {
                            foreach (SmokeGR g in Level.current.things[typeof(SmokeGR)])
                            {
                                g.alpha -= 0.01f;
                            }
                        }
                        if (mainDevice == null && oper.local && usings > 0)
                        {
                            usings--;
                            PlayerStats.renown += 50;
                            PlayerStats.Save();
                            Level.Add(new RenownGained() { description = "Thermal scope", amount = 50 });
                        }
                    }
                    else
                    {
                        oper.MainGun.xScope = 1.5f;
                    }

                }
            }
            if(user != null)
            {
                if (inScope)
                {
                    user.seeTSmokeFrames = 15;
                }
            }
        }
    }
}
