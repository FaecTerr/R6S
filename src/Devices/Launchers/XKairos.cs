using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Devices|Weapon")]
    public class XKairos : Launchers
    {
        public XKairos(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/XKAIROS.png"), 22, 14, false);
            graphic = _sprite;
            _sprite.frame = 0;
            center = new Vec2(11f, 7f);
            collisionSize = new Vec2(20f, 12f);
            collisionOffset = new Vec2(-10f, -6f);
            _canRaise = true;
            weight = 0.9f;
            thickness = 0.1f;
            placeable = false;
            zeroSpeed = false;
            scannable = false;

            index = 45;

            _editorName = "XKAIROS";

            Missiles1 = 6;
            //placeSound = "SFX/Devices/AirJabShot.wav";
            //pickupSound = "SFX/Devices/NomadClick.wav";
        }

        public override void SetMissile()
        {
            Vec2 pos = new Vec2(position.x + (float)(Math.Cos(angle) * 10 * offDir), position.y + (float)(Math.Sin(angle) * 2));
            missile = new XKPellet(pos.x, pos.y);
            base.SetMissile();
        }

        public override void Update()
        {
            base.Update();
            if (user != null)
            {
                if (user.holdObject == this && Missiles1 > 0)
                {
                    if (user.local && Keyboard.Pressed(Keys.MouseMiddle))
                    {
                        Missiles1--;
                        reload = 2;

                        foreach(XKPellet pellet in Level.current.things[typeof(XKPellet)])
                        {
                            pellet.Activation();
                        }

                        Level.Add(new SoundSource(position.x, position.y, 240, placeSound, "J"));
                        DuckNetwork.SendToEveryone(new NMSoundSource(position, 240, placeSound, "J"));
                    }
                }
            }
        }
    }

    public class XKPellet : Device
    {
        float radius = 8;
        float delay = 8 * 0.1f * 60;
        bool activated;

        int soundFrames;

        public XKPellet(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/Pellet.png"), 8, 12, false);
            graphic = _sprite;
            center = new Vec2(6f, 6f);
            collisionOffset = new Vec2(-2f, -2f);
            collisionSize = new Vec2(4f, 4f);
            gravMultiplier = 0f;
            weight = 0.9f;
            thickness = 0.1f;

            canPickUp = false;
            zeroSpeed = false;

            bulletproof = false;
            destructingByElectricity = true;
            catchableByADS = true;
            breakable = true;

            _sprite.AddAnimation("run", 0.1f, true, new int[] { 0, 1, 2, 3 });

            setTime = 1f;
        }

        public override void Update()
        {
            if (setted)
            {
                /*if (soundFrames <= 0)
                {
                    soundFrames = 90;
                    Level.Add(new SoundSource(position.x, position.y, radius * 2, "SFX/Devices/AirJabidle.wav", "J"));
                    DuckNetwork.SendToEveryone(new NMSoundSource(position, radius * 2, "SFX/Devices/AirJabidle.wav", "J"));
                }
                else
                {
                    soundFrames--;
                }*/

                if(activated)
                {
                    if(delay > 0)
                    {
                        delay -= 0.1f;
                    }
                    else
                    {
                        Explode();
                    }
                }
            }
            base.Update();
        }

        public override void Activation()
        {
            activated = true;
            _sprite.SetAnimation("run");
            base.Activation();
        }

        public override void Break()
        {
            base.Break();
            Level.Remove(this);
        }

        public virtual void Explode()
        {
            Level.Add(new Explosion(position.x, position.y, radius, 10, "H") { shootedBy = oper });
            Level.Add(new Explosion(position.x, position.y, radius * 1.25f, 10, "S") { shootedBy = oper });
            Level.Add(new Explosion(position.x, position.y, radius * 1.5f, 5, "N") { shootedBy = oper });

            DuckNetwork.SendToEveryone(new NMExplosion(position, radius, 10, "H", oper));
            DuckNetwork.SendToEveryone(new NMExplosion(position, radius * 1.25f, 10, "S", oper));
            DuckNetwork.SendToEveryone(new NMExplosion(position, radius * 1.5f, 5, "N", oper));
            
            foreach (SurfaceStationary sf in Level.CheckCircleAll<SurfaceStationary>(position, radius * 2))
            {
                sf.Breach(250000);
            }
            //Level.Add(new SoundSource(position.x, position.y, 600, "SFX/Devices/AirjabBlow.wav", "J"));
            //DuckNetwork.SendToEveryone(new NMSoundSource(position, 600, "SFX/Devices/AirjabBlow.wav", "J"));
            Break();
        }

        public override void OnSolidImpact(MaterialThing with, ImpactedFrom from)
        {
            if (with != null)
            {
                if ((with is Block || with is DeployableShieldAP) && setted == false)
                {
                    Level.Add(new SoundSource(position.x, position.y, 200, "SFX/Devices/AirJabStick.wav", "J"));
                    DuckNetwork.SendToEveryone(new NMSoundSource(position, 200, "SFX/Devices/AirJabStick.wav", "J"));

                    grounded = true;
                    _hSpeed = 0f;
                    _vSpeed = 0f;
                    gravMultiplier = 0f;
                    setted = true;
                    frame = 1;

                    if (from == ImpactedFrom.Right)
                    {
                        angleDegrees = 0;
                    }
                    if (from == ImpactedFrom.Left)
                    {
                        angleDegrees = 180;
                    }
                    if (from == ImpactedFrom.Top)
                    {
                        angleDegrees = 270;
                    }
                    if (from == ImpactedFrom.Bottom)
                    {
                        angleDegrees = 90;
                    }
                }
            }
            base.OnSolidImpact(with, from);
        }

        public override void Draw()
        {
            if (setted && setTime > 0)
            {
                Graphics.DrawCircle(position, radius, Color.White * setTime, 1f);
                Graphics.DrawCircle(position, radius * setTime, Color.White * setTime, 1f);

                setTime -= 0.01666666f;
            }
            base.Draw();
        }
    }
}