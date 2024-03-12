using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class Explosion : Thing, IDrawToDifferentLayers
    {
        float rad;
        float damage;
        string bType;
        public Operators shootedBy;

        int baseDamage;
        public bool ignoreDamage = false;

        public bool doEffect;
        public Vec2 direction;
        public int explosionType = 1;
        public int animationTime = 10;
        public int actionFrames;

        public Explosion(float xpos, float ypos, float radius, int Damage, string Type = "S") : base(xpos, ypos)
        {
            rad = radius;
            damage = Damage;
            bType = Type;
            baseDamage = Damage;

            CalculateDamage();           
        }

        public virtual void CalculateDamage()
        {
            foreach(Operators op in Level.CheckCircleAll<Operators>(position, rad))
            {
                float distance = Math.Abs((op.position - position).length / rad);
                if (distance < 0)
                {
                    distance = 0;
                }
                if (distance > 1)
                {
                    distance = 1;
                }
                float dam = damage * (1 - distance);

                float m = 0;

                foreach (Block block in Level.CheckLineAll<Block>(op.position, position))
                {
                    if (block is BreakableSurface)
                    {
                        BreakableSurface b = block as BreakableSurface;

                        if (b.breakableMode != bType)
                        {
                            if (b.breakableMode == "E")
                            {
                                m += 1;
                            }
                            if (b.breakableMode == "S")
                            {
                                m += 0.6f;
                            }
                            if(b.breakableMode == "H")
                            {
                                m += 2;
                            }
                            if (b.breakableMode == "U")
                            {
                                m += 1;
                            }
                        }
                    }
                    else
                    {
                        m += 7;
                    }
                }

                if(op.holdObject != null)
                {
                    if(op.holdObject is HandShield)
                    {
                        if((op.holdObject as HandShield).opened)
                        {
                            m += 7;
                        }
                        else
                        {
                            m += 4;
                        }
                    }
                    if(op.holdObject is FlashShield)
                    {
                        m += 4;
                    }
                    if(op.holdObject is BallisticShield)
                    {
                        m += 4;
                    }
                }

                if(Level.CheckLine<DeployableShieldAP>(op.position, position) != null)
                {
                    m += 7;
                }

                if(m > 9.5f)
                {
                    m = 9.5f;
                }

                dam *= 1 - m * 0.1f;

                if(dam < 0)
                {
                    dam = 0;
                }
                if(dam > baseDamage)
                {
                    dam = baseDamage;
                }

                if (shootedBy != null)
                {
                    op.lastDamageFrom = shootedBy;
                }

                if (op == shootedBy && ignoreDamage)
                {
                    
                }
                else
                {
                    op.GetDamage(dam);
                    DevConsole.Log(Convert.ToString(dam) + " (" + Convert.ToString(baseDamage) + ")");
                }

                if (op.Health <= 0)
                {
                    if (shootedBy != null)
                    {
                        if (shootedBy.local)
                        {
                            SFX.Play(GetPath("SFX/Hit1.wav"));
                            Level.Add(new HitScan(position.x, position.y));
                            if (op.team != shootedBy.team)
                            {
                                PlayerStats.renown += 70;
                                PlayerStats.Save();
                                Level.Add(new RenownGained() { description = "Enemy exploded", amount = 70, additional = "+10: Explosion" });
                            }
                        }
                    }
                }
            }

            foreach (BreakableSurface b in Level.CheckCircleAll<BreakableSurface>(position, rad))
            {
                b.Explosion(position, rad, bType);
            }

            foreach (Terrorist d in Level.CheckCircleAll<Terrorist>(position, rad))
            {
                float distance = Math.Abs((d.position - position).length / rad);
                if(distance < 0)
                {
                    distance = 0;
                }
                if(distance > 1)
                {
                    distance = 1;
                }
                float dam = damage * (1 - distance * distance);
                if (dam > baseDamage)
                {
                    dam = baseDamage;
                }
                if(dam < 0)
                {
                    dam = 0;
                }

                float m = 0;
                foreach (BreakableSurface b in Level.CheckLineAll<BreakableSurface>(d.position, position))
                {
                    if (b.breakableMode != "H" || b.breakableMode != "U")
                    {
                        if (b.breakableMode == "E")
                        {
                            m += 1;
                        }
                        if (b.breakableMode == "S")
                        {
                            m += 2;
                        }
                    }
                }

                
                if (Level.CheckLine<DeployableShieldAP>(d.position, position) != null)
                {
                    m += 7;
                }

                if (m > 9.5f)
                {
                    m = 9.5f;
                }

                dam *= 1 - m * 0.1f;

                if (dam < 0)
                {
                    dam = 0;
                }
                if (dam > baseDamage)
                {
                    dam = baseDamage;
                }

                if (ignoreDamage)
                {

                }
                else
                {
                    d.GetDamage(dam);
                    d.lastHitIsHeadshot = false;
                    if (d.Health <= 0)
                    {
                        if (shootedBy != null)
                        {                            
                            if (shootedBy.local)
                            {
                                SFX.Play(GetPath("SFX/Hit1.wav"));
                                Level.Add(new HitScan(position.x, position.y));
                                if (d.team != shootedBy.team)
                                {
                                    PlayerStats.renown += 35;
                                    PlayerStats.Save();
                                    Level.Add(new RenownGained() { description = "Target killed", amount = 35, additional = "+10: Explosion"});
                                }
                            }
                        }
                    }
                }
            }

            foreach (BreakableDoor b in Level.CheckCircleAll<BreakableDoor>(position, rad))
            {
                b.Exploded();
            }

            foreach(Device d in Level.CheckCircleAll<Device>(position, rad * 0.5f))
            {
                if (d.breakable && Level.CheckLine<Block>(position, d.position) == null)
                {
                    d.Break();
                    if(shootedBy != null)
                    {
                        if (shootedBy.local && d.mainDevice != null)
                        {
                            if (d.team != shootedBy.team)
                            {
                                PlayerStats.renown += d.DeviceCost;
                                PlayerStats.Save();
                                Level.Add(new RenownGained() { description = d.destroyedPoints, amount = d.DeviceCost });
                            }
                            else
                            {
                                Level.Add(new RenownGained() { description = d.destroyedPoints, amount = d.DeviceCost * -1 });
                            }
                        }
                    }
                }
            }
            //SFX.Play("explode", 1f, 0f, 0f, false);
        }

        public override void Draw()
        {
            if (DevConsole.showCollision)
            {
                Color c = Color.Red;
                if(bType == "H")
                {
                    c = Color.Gray;
                }
                Graphics.DrawCircle(position, rad, c, 1, 1f, 32);
            }
            base.Draw();
        }

        public void OnDrawLayer(Layer pLayer)
        {
            if(pLayer == Layer.Game)
            {
                if (doEffect && actionFrames < animationTime)
                {
                    float size = rad / 12;

                    string path = "Sprites/Explosions/";
                    Vec2 updPos = position;
                    
                    if (explosionType == 1)
                    {
                        path += "smallDirected.png";
                        updPos = position + direction * new Vec2(-11f, -14f);
                    }

                    SpriteMap _sprite = new SpriteMap(GetPath(path), 22, 28);

                    _sprite.position = updPos;
                    

                    _sprite.frame = actionFrames / animationTime;
                    actionFrames++;
                    _sprite.alpha = alpha;

                    if(direction == new Vec2(1, 0))
                    {
                        _sprite.angle = 0;
                    }
                    if (direction == new Vec2(-1, 0))
                    {
                        _sprite.angle = 180;
                    }
                    if (direction == new Vec2(0, 1))
                    {
                        _sprite.angle = 90;
                    }
                    if (direction == new Vec2(0, -1))
                    {
                        _sprite.angle = 270;
                    }

                    Graphics.Draw(_sprite, _sprite.position.x, _sprite.position.y);
                }
            }
        }
    }
}
