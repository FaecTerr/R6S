using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class NewBullet : Thing
    {
        public List<MaterialThing> hit = new List<MaterialThing>();
        public bool init;
        public Vec2 _startPos;
        public Vec2 _endPos;
        public float life = 1.1f;
        public Vec2 hitP;
        public float damage = 10;
        public Operators shootedBy;
        public float maxDistance = 1;

        public float maxLife = 0;

        public List<MaterialThing> ignore = new List<MaterialThing>();

        public float thickness = 1f;
        public bool tracer = true;

        public int rate;
        public float headshotModifier = 1.5f;

        public float damageDropDistance;
        public float maxDropDistance;
        public float minDamageDrop;

        public int trackType = 0;

        public NewBullet(float xpos, float ypos, Vec2 startPos, Vec2 endPos, Vec2 hitPos, int fireRate, float lifetime = 0.1f, bool trace = true, float hs = 1.5f) : base(xpos, ypos)
        {
            _startPos = startPos;
            //_endPos = endPos;
            //_startPos = bullet.start;
            _endPos = endPos;
            life = lifetime;
            hitP = hitPos;
            tracer = trace;
            //layer = Layer.Blocks;
            rate = fireRate;
            headshotModifier = hs;
        }

        public float DistanceToPoint(Vec2 p, Vec2 p1, Vec2 p2)
        {
            float a = Math.Abs((p2.x - p1.x) * (p1.y - p.y) - (p1.x - p.x) * (p2.y - p1.y));
            float b = (float)Math.Sqrt((p2.x - p1.x) * (p2.x - p1.x) + (p2.y - p1.y) * (p2.y - p1.y));
            return a/b;
        }
        public float DamageDrop(float traveled)
        {
            float value = 1;

            value = minDamageDrop + (1 - minDamageDrop) * (1 - (traveled - damageDropDistance) / (maxDropDistance - damageDropDistance));

            return value;
        }
        public virtual void Init()
        {
            if(maxLife == 0)
            {
                maxLife = life;
            }
            init = true;
            Operators oper = new Operators(position.x, position.y); //Ignored oper a.k.a. shootedBy
            foreach(MaterialThing o in ignore)
            {
                if(o is Operators)
                {
                    Operators op = o as Operators;
                    oper = op;
                }
            }

            foreach (MaterialThing p in Level.CheckLineAll<MaterialThing>(_startPos, _endPos))
            {
                if(p is Operators)
                {
                    hit.Add(p);
                }
                if (p is OperatorHead)
                {
                    hit.Add(p);
                }
                if(p is Device)
                {
                    hit.Add(p);
                }
                if (p is Terrorist)
                {
                    hit.Add(p);
                }
                if(p is TerroristHead)
                {
                    hit.Add(p); 
                }
            }
            

            foreach (MaterialThing p in hit)
            {
                if (p is Operators)
                {
                    //DevConsole.Log("Start to processing O-Damage");
                    Operators op = p as Operators;
                    float traveled = (op.position - _startPos).length / maxDistance;

                    float localDamage = damage;

                    if (traveled > damageDropDistance)
                    {
                        if (traveled >= maxDropDistance)
                        {
                            localDamage *= minDamageDrop;
                        }
                        else
                        {
                            localDamage *= DamageDrop(traveled);
                        }
                    }

                    if (ignore.Contains(op) || hit.Contains(op.head))
                    {

                    }
                    else
                    {
                        if (shootedBy != null && op.bulletImmuneFrames <= 0)
                        {
                            if (shootedBy.local)
                            {
                                SFX.Play(GetPath("SFX/Hit3.wav"));
                                if(op.team == shootedBy.team)
                                {
                                    Level.Add(new RenownGained() { description = "Friendly fire", amount = -5 });
                                }
                            }
                            op.lastDamageFrom = shootedBy;
                            if (op.wasDowned < op.maxDowns && op.Health - (int)(localDamage * (0.9f - 0.1f * op.Armor)) <= 0)
                            {
                                if (shootedBy.local)
                                {
                                    if (op.team != shootedBy.team)
                                    {
                                        PlayerStats.renown += 60;
                                        PlayerStats.Save();
                                        Level.Add(new RenownGained() { description = "Enemy injured", amount = 60 });
                                    }
                                    else
                                    {
                                        Level.Add(new RenownGained() { description = "Teammate injured", amount = -50 });
                                    }
                                }
                            }
                            if (op.wasDowned >= op.maxDowns && op.Health - (int)(localDamage * (0.9f - 0.1f * op.Armor)) <= 0)
                            {
                                if (op.DBNO)
                                {
                                    if (op.team != shootedBy.team)
                                    {
                                        Level.Add(new InfoFeedTab(shootedBy.name, op.name) { typed = 1, args = new string[1] { "shot" } });
                                    }
                                    else
                                    {
                                        Level.Add(new InfoFeedTab(shootedBy.name, op.name) { typed = 3, args = new string[1] { "shot" } });
                                    }
                                    if (shootedBy.local)
                                    {
                                        if (op.team != shootedBy.team)
                                        {
                                            PlayerStats.renown += 60;
                                            PlayerStats.Save();
                                            Level.Add(new RenownGained() { description = "Enemy finished", amount = 60 });
                                        }
                                        else
                                        {
                                            Level.Add(new RenownGained() { description = "Teammate finished", amount = -50 });
                                        }
                                    }
                                }
                                else
                                {
                                    if (op.team != shootedBy.team)
                                    {
                                        Level.Add(new InfoFeedTab(shootedBy.name, op.name) { typed = 1, args = new string[1] { "shot" } });
                                    }
                                    else
                                    {
                                        Level.Add(new InfoFeedTab(shootedBy.name, op.name) { typed = 3, args = new string[1] { "shot" } });
                                    }
                                    if (shootedBy.local)
                                    {
                                        if (op.team != shootedBy.team)
                                        {
                                            PlayerStats.renown += 75;
                                            PlayerStats.Save();
                                            Level.Add(new RenownGained() { description = "Enemy killed", amount = 75 });
                                        }
                                        else
                                        {
                                            Level.Add(new RenownGained() { description = "Teammate killed", amount = -75 });
                                        }
                                    }
                                }
                            }
                        }
                            //op.RecievedDamage(oper);
                        
                        op.GetDamageBullet((int)localDamage, oper.netIndex);
                        if (op.bulletImmuneFrames <= 0)
                        {
                            op.bulletImmuneFrames = rate;
                        }
                    }
                    //DevConsole.Log("End of processing O-Damage");
                }
                if (p is Device)
                {
                    //DevConsole.Log("Start to processing D-Damage");
                    Device op = p as Device;
                    if (ignore.Contains(op))
                    {

                    }
                    else
                    {
                        if (op.health > 0)
                        {
                            op.GetDamage((int)damage);

                            op.HittedFrom(shootedBy);

                            if (op.health <= 0)
                            {
                                if (shootedBy != null)
                                {
                                    if (shootedBy.local)
                                    {
                                        SFX.Play(GetPath("SFX/Hit2.wav"));
                                        Level.Add(new HitScan(position.x, position.y) { device = true });
                                    }
                                    if (shootedBy.local)
                                    {
                                        if (op.team != shootedBy.team)
                                        {
                                            PlayerStats.renown += op.DeviceCost;
                                            PlayerStats.Save();
                                            Level.Add(new RenownGained() { description = op.destroyedPoints, amount = op.DeviceCost });
                                        }
                                        else
                                        {

                                            Level.Add(new RenownGained() { description = op.destroyedPoints, amount = op.DeviceCost * -1 });
                                        }
                                    }
                                }
                            }
                        }
                    }
                    //DevConsole.Log("End of processing D-Damage");
                }
                if (p is OperatorHead)
                {
                    //DevConsole.Log("Start to processing OH-Damage");
                    OperatorHead op = p as OperatorHead;

                    float traveled = (op.position - _startPos).length / maxDistance;

                    float localDamage = damage * headshotModifier;
                    
                    if(op.oper != null)
                    {
                        localDamage *= (1 - op.oper.HeadshotDamageResist);
                    }

                    if (traveled > damageDropDistance)
                    {
                        if (traveled >= maxDropDistance)
                        {
                            localDamage *= minDamageDrop;
                        }
                        else
                        {
                            localDamage *= DamageDrop(traveled);
                        }
                    }

                    if ((ignore.Contains(op)))
                    {

                    }
                    else
                    {
                        if (op.oper != null && op.oper.Health > 0 && op.oper.bulletImmuneFrames <= 0)
                        {
                            if (shootedBy != null)
                            {
                                if (shootedBy.local)
                                {
                                    SFX.Play(GetPath("SFX/Hit3.wav"));
                                }
                                op.oper.lastDamageFrom = shootedBy;


                                if (shootedBy.local)
                                {
                                    SFX.Play(GetPath("SFX/Hit1.wav"));
                                }
                                op.oper.lastDamageFrom = shootedBy;
                                if (op.oper.wasDowned < op.oper.maxDowns && op.oper.Health - (int)(damage * (0.9f - 0.1f * op.oper.Armor)) <= 0)
                                {
                                    if (shootedBy.local && op.oper.team != shootedBy.team)
                                    {
                                        PlayerStats.renown += 70;
                                        PlayerStats.Save();
                                        Level.Add(new RenownGained() { description = "Enemy injured", amount = 70, additional = "+10: Headshot" });
                                    }
                                }
                                if (op.oper.wasDowned >= op.oper.maxDowns && op.oper.Health - (int)(damage * (0.9f - 0.1f * op.oper.Armor)) <= 0)
                                {
                                    if (op.oper.DBNO)
                                    {
                                        if (op.oper.team != shootedBy.team)
                                        {
                                            Level.Add(new InfoFeedTab(shootedBy.name, op.oper.name) { typed = 1, args = new string[1] { "shot" } });
                                        }
                                        else
                                        {
                                            Level.Add(new InfoFeedTab(shootedBy.name, op.oper.name) { typed = 3, args = new string[1] { "shot" } });
                                        }
                                        if (shootedBy.local)
                                        {
                                            if (op.oper.team != shootedBy.team)
                                            {
                                                PlayerStats.renown += 70;
                                                PlayerStats.Save();
                                                Level.Add(new RenownGained() { description = "Enemy finished", amount = 70, additional = "+10: Headshot" });
                                            }
                                            else
                                            {
                                                Level.Add(new RenownGained() { description = "Teammate finished", amount = -50 });
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (op.oper.team != shootedBy.team)
                                        {
                                            Level.Add(new InfoFeedTab(shootedBy.name, op.oper.name) { typed = 1, args = new string[1] { "shot" } });
                                        }
                                        else
                                        {
                                            Level.Add(new InfoFeedTab(shootedBy.name, op.oper.name) { typed = 3, args = new string[1] { "shot" } });
                                        }
                                        if (shootedBy.local)
                                        {
                                            if (op.oper.team != shootedBy.team)
                                            {
                                                PlayerStats.renown += 85;
                                                PlayerStats.Save();
                                                Level.Add(new RenownGained() { description = "Enemy killed", amount = 85, additional = "+10: Headshot" });
                                            }
                                            else
                                            {
                                                Level.Add(new RenownGained() { description = "Teammate killed", amount = -100 });
                                            }
                                        }
                                    }
                                }
                            }
                            op.oper.GetDamageBullet((int)localDamage);
                            op.oper.bulletImmuneFrames = rate;
                        }
                    }
                    DevConsole.Log("Start to processing OH-Damage");
                }

                if (p is Terrorist && shootedBy != null)
                {
                    //DevConsole.Log("Start to processing T-Damage");
                    Terrorist op = p as Terrorist;

                    float traveled = (op.position - _startPos).length / maxDistance;

                    float localDamage = damage;

                    if (traveled > damageDropDistance)
                    {
                        if (traveled >= maxDropDistance)
                        {
                            localDamage *= minDamageDrop;
                        }
                        else
                        {
                            localDamage *= DamageDrop(traveled);
                        }
                    }

                    if (ignore.Contains(op) || hit.Contains(op.head))
                    {

                    }
                    else
                    {
                        op.awared = true;
                        op.awaredFrames = 1200;
                        op.GetDamageBullet((int)localDamage);
                        op.lastHitIsHeadshot = false;
                        DevConsole.Log("Hit bodyshot: " + Convert.ToString(Math.Round(localDamage)));

                        if (oper != null)
                        {
                            if (shootedBy != null)
                            {
                                op.lastDamageFrom = shootedBy;
                                if (shootedBy.local)
                                {
                                    Level.Add(new HitScan(position.x, position.y));
                                    SFX.Play(GetPath("SFX/Hit3.wav"));
                                }
                                if (op.Health <= 0)
                                {
                                    if (shootedBy.local && op.team != shootedBy.team)
                                    {
                                        if (!op.Dummy)
                                        {
                                            PlayerStats.renown += 25;
                                            PlayerStats.Save();
                                        }
                                        Level.Add(new RenownGained() { description = "Target killed", amount = 25 });
                                    }
                                }
                            }
                        }
                    }
                    //DevConsole.Log("End of processing T-Damage");
                }

                if (p is TerroristHead)
                {
                    //DevConsole.Log("Start to processing TH-Damage");
                    TerroristHead op = p as TerroristHead;

                    float traveled = (op.position - _startPos).length / maxDistance;
                    float localDamage = damage * headshotModifier;

                    if (traveled > damageDropDistance)
                    {
                        if (traveled >= maxDropDistance)
                        {
                            localDamage *= minDamageDrop;
                        }
                        else
                        {
                            localDamage *= DamageDrop(traveled);
                        }
                    }

                    if (ignore.Contains(op))
                    {

                    }
                    else
                    {
                        if (op.oper != null && op.oper.Health > 0)
                        {
                            if (shootedBy != null)
                            {
                                op.oper.lastDamageFrom = shootedBy;
                                if (shootedBy.local)
                                {
                                    SFX.Play(GetPath("SFX/Hit3.wav"));
                                    SFX.Play(GetPath("SFX/Hit1.wav"));
                                }
                            }
                            op.oper.lastHitIsHeadshot = true;
                            op.oper.GetDamageBullet((int)localDamage);
                            DevConsole.Log("Hit headshot: " + Convert.ToString((int)localDamage));
                            if (op.oper.Health <= 0)
                            {
                                if (shootedBy != null && shootedBy.local && op.oper.team != shootedBy.team)
                                {
                                    if (!op.oper.Dummy)
                                    {
                                        PlayerStats.renown += 35;
                                        PlayerStats.Save();
                                    }
                                    Level.Add(new RenownGained() { description = "Target killed", amount = 35, additional = "+10: Headshot" });
                                }
                            }
                        }
                    }
                    //DevConsole.Log("End of processing TH-Damage");
                }
            }
            foreach (OperatorHead op in Level.current.things[typeof(OperatorHead)])
            {
                float dist = DistanceToPoint(op.position, _startPos, _endPos);
                float pan = 0;
                float range = 16;
                if (op.oper != null && op.oper != shootedBy && dist < range && op.oper.local && !hit.Contains(op) && !hit.Contains(op.oper))
                {
                    pan = (op.position.x - _startPos.x) / range;
                    if (pan > 0.6f)
                    {
                        pan = 0.6f;
                    }
                    if (pan < -0.6f)
                    {
                        pan = -0.6f;
                    }
                    SFX.Play(GetPath("SFX/ShotMiss.wav"), dist / range + 0.2f, 0, pan);
                }
            }
        }

        public override void Update()
        {
            base.Update();
            if (!init)
            {
                Init();
            }
            if (life > 0)
            {
                life -= 0.01666666f;
                thickness *= 0.95f;
            }
            else
            {
                Level.Remove(this);
            }
        }

        public override void Draw()
        {
            base.Draw();
            if (tracer)
            {
                string path = "Sprites/Guns/";

                if(trackType == 0)
                {
                    path += "lineStraight.png";
                }
                if (trackType == 1)
                {
                    path += "lineSpiral.png";
                }
                if (trackType == 2)
                {
                    path += "lineFired.png";
                }
                if (trackType == 3)
                {
                    path += "lineFreezed.png";
                }
                if (trackType == 4)
                {
                    path += "linePlasm.png";
                }
                if(trackType == 5)
                {
                    path += "lineBlood";
                }
                if(trackType == 6)
                {
                    path += "lineBluePlasm";
                }

                Sprite line = new Sprite(GetPath(path));
                line.CenterOrigin();
                //Graphics.DrawLine(_startPos, _endPos, Color.White * (life/maxLife), thickness, 1f);
                float len = (_startPos - _endPos).length / 8;
                                
                for (int i = 0; i < len; i++)
                {
                    Graphics.DrawTexturedLine(line.texture, _startPos + (_endPos - _startPos) * (i / len), _startPos + (_endPos - _startPos) * ((i+1) / len), Color.White * (life / maxLife), thickness, 1f);
                }
                
                //Graphics.DrawTexturedLine(line.texture, _startPos, _endPos, Color.White * (life / maxLife), thickness, 1f);
            }
        }
    }
}
