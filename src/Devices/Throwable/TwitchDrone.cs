using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class TwitchDrone : Drone
    {
        public TwitchDrone(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/Twitch.png"), 16, 16);
            graphic = _sprite;
            center = new Vec2(8f, 13f);
            collisionOffset = new Vec2(-8f, -3f);
            collisionSize = new Vec2(16f, 6f);
            depth = -0.5f;
            weight = 0.9f;
            physicsMaterial = PhysicsMaterial.Metal;
            thickness = 0.4f;
            team = "Att";
            _enablePhysics = true;

            sticky = false;

            UsageCount = 2;
            weightR = 4;

            DeviceCost = 20;
            descriptionPoints = "Shock drone active";

            index = 15; 
            minimalTimeOfHolding = 0.25f;
            needsToBeGentle = false;
        }

        public override void SetRocky()
        {
            rock = new TwitchDroneAP(position.x, position.y);
            if (oper != null)
            {
                rock.oper = oper;
            }
            //base.SetRocky();
        }
    }

    public class TwitchDroneAP : ObservationThing
    { 
        public float reload = 10;
        public StateBinding _UsageCount = new StateBinding("UsageCount", -1, false, false);
        public StateBinding _reload = new StateBinding("reload", -1, false, false);
        public int cooldown;

        public TwitchDroneAP(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/Twitch.png"), 16, 16);
            graphic = this._sprite;
            center = new Vec2(8f, 12f);
            collisionOffset = new Vec2(-8f, -3f);
            collisionSize = new Vec2(16f, 6f);
            depth = -0.5f;
            weight = 0.9f;
            physicsMaterial = PhysicsMaterial.Metal;

            bulletproof = false;
            breakable = true;

            UsageCount = 3;

            health = 15;

            team = "Att";
            _enablePhysics = true;
            controllable = true;

            _editorName = "OBS shock";

            sticky = false;
            destructingByElectricity = true;

            DeviceCost = 20;
            destroyedPoints = "Shock drone destroyed";

            canPick = true;
        }

        public override void Update()
        {
            if (oper != null)
            {
                if (oper.observing && UsageCount > 0 && observing)
                {
                    oper._aim.frame = 23;
                    oper.aim = position + Mouse.position - Level.current.camera.size * new Vec2(0.5f, 0.5f);
                }
            }
            base.Update();
            health = 15;
            if (oper != null)
            {
                if (oper.observing)
                {
                    if (position.x + Mouse.position.x - Level.current.camera.size.x * 0.5f > position.x)
                    {
                        offDir = 1;
                    }
                    if (position.x + Mouse.position.x - Level.current.camera.size.x * 0.5f < position.x)
                    {
                        offDir = -1;
                    }
                    foreach (Holdable h in Level.CheckCircleAll<Holdable>(position, 32))
                    {
                        h.alpha = 1;
                    }
                    if (UsageCount < 3)
                    {
                        if (reload > 0)
                        {
                            reload -= 0.01666666f;
                        }
                        else
                        {
                            UsageCount++;
                            reload = 10f;
                        }
                    }
                    foreach (Jammer j in Level.CheckCircleAll<Jammer>(position, 96))
                    {
                        if (j.setted)
                        {
                            jammed = true;
                        }
                    }
                    foreach (Door d in Level.CheckLineAll<Door>(this.position + new Vec2(-16f, 0f), this.position + new Vec2(16f, 0f)))
                    {
                        d._openForce = 2f;
                        d._open = 2;
                    }
                    if (oper != null)
                    {
                        if (oper.observing && UsageCount > 0 && observing)
                        {
                            oper._aim.frame = 23;
                            oper.aim = position + Mouse.position - Level.current.camera.size * new Vec2(0.5f, 0.5f);
                        }
                    }

                    if(cooldown > 0)
                    {
                        cooldown--;
                    }

                    if (jammed)
                    {
                        jump = false; moveLeft = false; moveRight = false;
                    }
                    else
                    {
                        if (moveRight == true && moveLeft == false)
                        {
                            hSpeed += 0.5f;
                        }
                        if (moveRight == false && moveLeft == true)
                        {
                            hSpeed -= 0.5f;
                        }
                        if (Math.Abs(hSpeed) > 2.75f)
                        {
                            if (hSpeed > 0)
                            {
                                hSpeed = 2.75f;
                            }
                            else
                            {
                                hSpeed = -2.75f;
                            }
                        }
                        if (moveRight == false && moveLeft == false)
                        {
                            hSpeed *= 0.8f;
                        }
                        if (jump == true && grounded)
                        {
                            vSpeed = -4f;
                        }
                        if (fire && UsageCount > 0 && (Mouse.position - Level.current.camera.size * new Vec2(0.5f, 0.5f)).length > 0 && cooldown <= 0)
                        {
                            Fire();
                        }
                    }
                    angleDegrees = hSpeed;
                }
            }
        }

        public virtual void Fire()
        {
            if (oper != null)
            {
                cooldown = 180;
                Vec2 vec2 = Mouse.position - Level.current.camera.size * new Vec2(0.5f, 0.5f);
                Vec2 vec3 = new Vec2(vec2.x, vec2.y * -1f);
                float num2 = (float)Math.Atan((vec3.y / vec3.x));

                if (num2 > 3f)
                {
                    num2 = 3f;
                }
                if (num2 < -3f)
                {
                    num2 = -3f;
                }
                this.angle = 6.28318548f - num2;
                float dir = 0f;
                if (offDir > 0)
                {
                    dir = 360 - angleDegrees;
                }
                else
                {
                    dir = 180f - angleDegrees;
                }
                UsageCount--;
                List<Bullet> firedBullets = new List<Bullet>();
                ATSniper shrap = new ATSniper();
                shrap.accuracy = 1;

                SFX.Play("plasmaFire", 1f, 1f, 0f, false);

                shrap.range = 80;
                shrap.bulletSpeed = 40f;
                shrap.bulletThickness = 2;
                dir += Rando.Float(-(1 - shrap.accuracy) * 30, (1 - shrap.accuracy) * 30);
                Bullet bullet = new Bullet(position.x + (float)(Math.Cos(Maths.DegToRad(dir)) * 6.0), position.y - (float)(Math.Sin(Maths.DegToRad(dir)) * 6.0), shrap, dir, null, false, -1f, false, true);

                bullet.traced = true;
                bullet.owner = oper;
                bullet.ammo.penetration = 2;
                Level.Add(bullet);
                firedBullets.Add(bullet);

                NewBullet nb = new NewBullet(position.x, position.y, bullet.start, bullet.end, bullet.end, 1, 1.0f) { damage = 5, thickness = thickness };

                DuckNetwork.SendToEveryone(new NMFireNewBullet(position.x, position.y, bullet.start, bullet.end, 1, 1f, oper, 5, thickness));
                nb.ignore.Add(this);
                Level.Add(nb);


                if (Network.isActive)
                {
                    NMFireGun gunEvent = new NMFireGun(null, firedBullets, 20, false, 4, false);
                    Send.Message(gunEvent, NetMessagePriority.ReliableOrdered);
                    firedBullets.Clear();
                }
            }
        }
        public override void OnDrawLayer(Layer layer)
        {
            if (layer == Layer.Foreground)
            {
                if (oper != null)
                {
                    if (oper.local && oper.observing && observing)
                    {
                        string text = Convert.ToString(UsageCount) + " / " + Convert.ToString(3);
                        string name = "Shock charge";
                        Color c = Color.White;
                        if(cooldown > 0)
                        {
                            name = "RELOADING";
                            c = Color.OrangeRed;
                        }
                        /*if (game == null)
                        {
                            BitmapFont _font = new BitmapFont("smallFont", 8, -1);
                        }*/

                        Graphics.DrawStringOutline(name, Level.current.camera.position + Level.current.camera.size - new Vec2(oper._aim.scale.x * 80, oper._aim.scale.y * 15), c, Color.Black, 1f, null, Level.current.camera.height / 320);
                        Graphics.DrawStringOutline(text, Level.current.camera.position + Level.current.camera.size - new Vec2(oper._aim.scale.x * 80, oper._aim.scale.y * 10), Color.White, Color.Black, 1f, null, Level.current.camera.height / 320);
                    }
                }
            }
            if(layer == Layer.Game)
            {
                if(oper != null)
                {
                    if(observing && oper.observing)
                    {
                        Vec2 aimScale = oper._aim.scale;
                        Vec2 pos = position + Mouse.position - Level.current.camera.size * new Vec2(0.5f, 0.5f);
                        oper._aim.scale = new Vec2(3, 3);
                        Graphics.Draw(oper._aim, pos.x, pos.y, 1);
                        oper._aim.scale = aimScale;
                    }
                }
            }
        }
        public override void Draw()
        {
            base.Draw();
        }
    }
}