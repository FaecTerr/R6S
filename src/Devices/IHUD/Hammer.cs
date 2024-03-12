using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    public class Hammer : Knife
    {
        bool canStab;

        public Hammer(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/Hammer.png"), 32, 32, false);
            graphic = this._sprite;
            center = new Vec2(16f, 16f);
            collisionOffset = new Vec2(-16f, -16f);
            collisionSize = new Vec2(30f, 30f);
            weight = 0.9f;

            range = 20;

            cantProne = true;

            damage = 2500;

            index = 11;
            UsageCount = 25;
            ShowCounter = true;
            Cooldown = 1;
            CooldownTime = 1;

            defaultKnife = false;
        }

        public override void Update()
        {
            if(oper != null)
            {
                if(oper.holdObject == this && UsageCount > 0)
                {
                    if (canStab)
                    {
                        UsageCount -= 1;
                        Cooldown -= 0.04f;
                        DoKnifeStab();
                        stabTime = 63;
                        canStab = false;

                        if (mainDevice == null && oper.local)
                        {
                            PlayerStats.renown += 5;
                            PlayerStats.Save();
                            Level.Add(new RenownGained() { description = "Sledge hammer", amount = 5 });
                        }
                    }
                }
                else
                {
                    stabTime = 80;
                    canStab = true;
                }
                if(oper.holdObject == this && UsageCount <= 0)
                {
                    oper.BackToWeapon(30);
                }
            }
            else
            {
                stabTime = 80;
                canStab = true;
            }
            base.Update();
        }

        public override void SummonOnHitpos(Vec2 pos)
        {
            DuckNetwork.SendToEveryone(new NMExplosion(pos, 18, 0, "S"));
            DuckNetwork.SendToEveryone(new NMExplosion(pos, 18, 0, "E"));
            Level.Add(new Explosion(pos.x, pos.y, 18, 0, "S"));
            Level.Add(new Explosion(pos.x, pos.y, 18, 0, "E"));

            base.SummonOnHitpos(pos);
        }
    }
}

