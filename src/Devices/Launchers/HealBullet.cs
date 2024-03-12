using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    public class HealBullet : Device
    {
        public HealBullet(float xpos, float ypos) : base(xpos, ypos)
        {
            gravMultiplier = 0;
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/Stimulator.png"), 12, 8, false);
            graphic = this._sprite;
            center = new Vec2(6f, 4f);
            collisionSize = new Vec2(12f, 8f);
            collisionOffset = new Vec2(-6f, -4f);
            weight = 0f;
            thickness = 0f;

            breakable = false;

            jammResistance = true;

            placeSound = "SFX/Devices/DocHeal.wav";
        }
        public override void Update()
        {
            base.Update();
            if(Level.CheckPoint<Block>(position) != null || hSpeed == 0)
            {
                Level.Remove(this);
            }

            if(Level.CheckRect<Operators>(topLeft, bottomRight) != null)
            {
                Operators f = Level.CheckRect<Operators>(topLeft, bottomRight);

                if (f.Health > 60)
                {
                    if (!f.HasEffect("Overhealed"))
                    {
                        f.effects.Add(new Overheal() { timer = 3 + (f.Health - 60) / 2, maxTimer = 3 + (f.Health - 60) / 2, team = f.team});
                    }
                    else
                    {
                        f.GetEffect("Overhealed").timer = 23;
                        f.GetEffect("Overhealed").maxTimer = 23;
                    }
                }
                f.Health += 40;
                if (f.DBNO)
                {
                    f.resetFromDBNO();
                    f.Health += 55;
                }
                

                if (mainDevice != null)
                {
                    if (mainDevice.oper != null)
                    {
                        if (mainDevice.oper.team == f.team && mainDevice.oper.local)
                        {
                            PlayerStats.renown += 50;
                            PlayerStats.Save();
                            Level.Add(new RenownGained() { description = "Teammate recovered", amount = 50 });
                        }
                    }
                }


                DuckNetwork.SendToEveryone(new NMUpdateHealth(f.netIndex, f.Health));
                Level.Remove(this);
            }
            if (oper != null)
            {
                if (mainDevice == null && oper.local)
                {
                    PlayerStats.renown += 50;
                    PlayerStats.Save();
                    Level.Add(new RenownGained() { description = "Healing", amount = 50 });
                }
            }
        }
    }
}
