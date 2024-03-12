using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Devices|Weapon")]
    public class MVG : Device
    {
        public SpriteMap _gas;
        public List<Bullet> firedBullets = new List<Bullet>();

        public float reload;
        
        public SpriteMap _aim;
        public int shotFrames;

        public float act;
        public SinWave pulse = 0.2f;

        public bool reloading;

        public MVG(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/MVG.png"), 16, 16, false);
            _gas = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/MVG_gas.png"), 15, 5, false);
            _gas.center = new Vec2(0f, 2.5f);
            graphic = this._sprite;
            _sprite.frame = 0;
            _aim = new SpriteMap(Mod.GetPath<R6S>("Sprites/Aim.png"), 17, 17);
            _aim.center = new Vec2(8.5f, 8.5f);
            //this._aim.frame = 2;
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(16, 16f);
            collisionOffset = new Vec2(-8f, -8f);

            setTime = 1f;

            _canRaise = true;
            placeable = false;
            scannable = false;
            zeroSpeed = false;

            index = 21;

            UsageCount = 4;
            
            CooldownTime = 1;
            Cooldown = CooldownTime;

            ShowCounter = true;
        }

        public override void Update()
        {
            base.Update();
            
            if(Cooldown < 0)
            {
                Cooldown = 0;
            }

            if (UsageCount > 0)
            {
                if (Cooldown <= 0 && reloading == false)
                {
                    reload = 3.5f;
                    reloading = true;
                    UsageCount--;
                }
                if (reloading == true)
                {
                    reload -= 0.01666666f;
                    Cooldown += 0.006f;
                    if (reload <= 0f)
                    {
                        Cooldown = CooldownTime;
                        reloading = false;
                    }
                }
            }
            else
            {
                Cooldown = 0;
            }
            _gas.xscale = act + pulse*0.15f;
            _gas.yscale = 0.4f;
            if(act < 0.4)
            {
                act = 0.4f;
            }
            if (act > 0.4 && shotFrames <= 0)
            {
                act -= 0.1f;
            }
            if (oper != null)
            {
                if (oper.duckOwner != null)
                {
                    if (oper.local && !oper.isDead)
                    {
                        shotFrames--;
                    }
                    if (Cooldown > 0 && shotFrames <= 0 && UsageCount > 0 && !reloading)
                    {
                        if ((Keyboard.Down(PlayerStats.keyBindings[13]) || Keyboard.Down(PlayerStats.keyBindingsAlternate[13])) && !oper.controller)
                        {
                            Use();
                        }
                        else if(oper.controller && oper.genericController != null)
                        {
                            if (oper.genericController.MapDown(4194304))
                            {
                                Use();
                            }
                        }
                    }
                }
            }
        }

        public virtual void Use()
        {
            if (oper != null)
            {
                Cooldown -= 0.05f;
                shotFrames = 3;
                act += 0.4f;
                if (act > 2)
                {
                    act = 2;
                }
                List<Bullet> firedBullets = new List<Bullet>();
                ATOldPistol shrap = new ATOldPistol();
                shrap.bulletSpeed = 1;
                shrap.accuracy = 1f;
                shrap.range = 16f;
                shrap.penetration = 0;
                float dir = 360 - angleDegrees;
                if (offDir == -1)
                {
                    dir = 180 - angleDegrees;
                }

                float X = 5.5f * (float)Math.Sin(angle) + 6.5f * (float)Math.Cos(angle) * offDir;
                float Y = 5.5f * (float)Math.Cos(angle) - 6.5f * (float)Math.Sin(angle) * offDir;

                Bullet bullet = new Bullet(position.x + X, position.y - Y, shrap, dir, null, false, 10, true, true);
                bullet.owner = own;
                bullet.ammo.penetration = 0;
                bullet.ammo.bulletSpeed = 1;
                bullet.traced = false;
                bullet.ammo.range = 16f;
                bullet.ammo.rangeVariation = 0f;
                Level.Add(bullet);

                Level.Add(new Explosion(bullet.end.x, bullet.end.y, 3.5f, 2, "H") { shootedBy = oper, ignoreDamage = true });
                Level.Add(new Explosion(bullet.end.x, bullet.end.y, 3.5f, 2, "S") { shootedBy = oper, ignoreDamage = true });

                DuckNetwork.SendToEveryone(new NMExplosion(bullet.end, 3.5f, 2, "S", oper));
                DuckNetwork.SendToEveryone(new NMExplosion(bullet.end, 3.5f, 2, "H", oper));

                foreach (SurfaceStationary sf in Level.CheckCircleAll<SurfaceStationary>(bullet.end, 4))
                {
                    sf.Breach(50);
                }

                SFX.Play("pistolFire", 1f, 1f, 0f, false);

                firedBullets.Add(bullet);
                if (Network.isActive)
                {
                    NMFireGun gunEvent = new NMFireGun(null, firedBullets, 20, false, 4, false);
                    Send.Message(gunEvent, NetMessagePriority.ReliableOrdered);
                    firedBullets.Clear();
                }
            }
        }

        public override void Draw()
        {
            base.Draw();
            if (oper != null)
            {
                float gna = angleDegrees;
                _gas.angle = Maths.DegToRad(gna);

                _gas.flipH = offDir == -1;

                float X =  5.5f * (float)Math.Sin(angle) + 6.5f * (float)Math.Cos(angle) * offDir;
                float Y = 5.5f * (float)Math.Cos(angle) - 6.5f * (float)Math.Sin(angle) * offDir;


                Graphics.Draw(_gas, _sprite.position.x + X, _sprite.position.y - Y, 1f);

                //Graphics.Draw(_aim, Mouse.positionScreen.x, Mouse.positionScreen.y);
            }
        }
    }
}
