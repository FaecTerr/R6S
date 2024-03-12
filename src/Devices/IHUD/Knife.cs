using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class Knife : Device
    {
        public float stabTime;
        public bool autoReturn = true;
        public int damage = 100;
        public float range = 16;
        public float size = 3;

        public bool defaultKnife = true;

        public Vec2 end;
        public Vec2 start;

        public Knife(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/Knife.png"), 16, 16, false);
            graphic = _sprite;
            _sprite.frame = 0;
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(16f, 16f);
            collisionOffset = new Vec2(-8f, -8f);
            setTime = 0.8f;
            weight = 0.9f;
            thickness = 0.4f;
            placeable = false;
            setted = false;
            scannable = false;

            dontRotate = true;
            bulletproof = true;
            breakable = false;
        }

        public override void Update()
        {
            if(stabTime > 0)
            {
                stabTime--;
            }
            if (oper != null)
            {
                if (oper.isDead)
                {
                    position = new Vec2(9999999f, -99999999f);
                }
                if (stabTime > 59 && stabTime < 61 && !oper.DBNO)
                {
                    DoKnifeStab();
                }
            }
            else
            {
                angle = 0;
                position = new Vec2(9999999f, -99999999f);
            }

            if(stabTime > 40 && stabTime < 80)
            {
                angle = Math.Abs(angle) + (stabTime - 20) * 0.3f * 0.0174f;
                angle *= offDir;
                _holdOffset = new Vec2((float)Math.Cos(angle) * (range - Math.Abs(stabTime - 60) * (range / 20)) + range * 0.25f, (float)Math.Sin(angle) * (range - Math.Abs(stabTime - 60) * (range / 20)) * offDir);
            }

            if(stabTime > 10 && stabTime < 20)
            {
                if(oper != null)
                {
                    oper.BackToWeapon(30);
                }
            }
            base.Update();
        }

        public override void Draw()
        {
            //Graphics.DrawLine(start, end, Color.Red, 1f);
            base.Draw();
        }

        public virtual void DoKnifeStab()
        {
            Vec2 sPos = position;
            if (oper != null)
            {
                sPos = oper.position + oper.headPosition + new Vec2(3 * offDir, 3f);
                if (oper.local)
                {
                    Level.Add(new SoundSource(oper.position.x, oper.position.y, 160, "SFX/knife_stab.wav", "J"));
                    DuckNetwork.SendToEveryone(new NMSoundSource(oper.position, 160, "SFX/knife_stab.wav", "J"));
                    DuckNetwork.SendToEveryone(new NMKnifeStab(oper));
                }

                start = sPos;

                ATTracer tracer = new ATTracer();
                tracer.range = range;
                tracer.penetration = 0.4f;
                Vec2 pos = start;
                float a = Maths.PointDirection(pos, oper.aim);

                Bullet b = new Bullet(pos.x, pos.y, tracer, a, owner, false, -1f, true, true);

                Vec2 hitPos = b.end;
                end = hitPos;

                foreach (Operators op in Level.CheckLineAll<Operators>(start, end))
                {
                    if (op.team != team && Level.CheckLine<Block>(start, end) == null)
                    {
                        op.GetDamage(damage);
                        if (oper != null)
                        {
                            op.lastDamageFrom = oper;
                        }
                        if (op.Health <= 0)
                        {
                            if (oper.local)
                            {
                                PlayerStats.renown += 70;
                                PlayerStats.Save();
                                Level.Add(new RenownGained() { description = "Enemy knifed", amount = 70, additional = "+10: Melee" });
                            }
                        }
                    }
                }

                foreach (Terrorist op in Level.CheckLineAll<Terrorist>(start, end))
                {
                    if (op.team != team && Level.CheckLine<Block>(start, end) == null)
                    {
                        op.GetDamage(damage);
                        if (op.Health <= 0)
                        {
                            if (oper.local)
                            {
                                PlayerStats.renown += 35;
                                PlayerStats.Save();
                                Level.Add(new RenownGained() { description = "Target killed", amount = 35, additional = "+10: Melee" });
                            }
                        }
                    }
                }

                foreach (BreakableDoor bd in Level.CheckLineAll<BreakableDoor>(start, end))
                {
                    bd.Damaged();
                }

                foreach (Device d in Level.CheckLineAll<Device>(start, end))
                {
                    if (d.destructingByHands)
                    {
                        d.GetDamage(damage, true);
                    }
                }

                
                foreach (BreakableSurface bs in Level.CheckLineAll<BreakableSurface>(start, end))
                {
                    bs.Explosion(end, size, "E");
                    bs.Explosion(end, size * 0.5f, "S");
                }

                SummonOnHitpos(end);
            }
        }

        public virtual void SummonOnHitpos(Vec2 pos)
        {

        }

    }
}
