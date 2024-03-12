using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Devices|Deployable")]
    public class CED : Placeable
    {
        public CED(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/CED.png"), 18, 12, false);
            graphic = this._sprite;
            _sprite.frame = 0;
            center = new Vec2(9f, 6);
            collisionSize = new Vec2(12f, 12f);
            collisionOffset = new Vec2(-6f, -6f);
            setTime = 1f;
            //setTime = 0.0001f;
            weight = 0.9f;
            thickness = 0.1f;
            placeable = true;
            breakable = true;
            electricible = true;
            CheckRect = new Vec2(6.5f, 0);

            cantProne = true;

            UsageCount = 3;

            DeviceCost = 5;

            destroyedPoints = "Generator destroyed";

            index = 2;

            placeSound = "SFX/Devices/CEDPlace.wav";
        }

        public override void SetAfterPlace()
        {
            afterPlace = new CEDAP(position.x, position.y);
            base.SetAfterPlace();
        }

        public override void Draw()
        {
            base.Draw();
            float radius = 16f;
            if(afterPlace != null && afterPlace is CEDAP)
            {
                radius = (afterPlace as CEDAP).radius;
            }
            foreach (Device d in Level.CheckCircleAll<Device>(position, 16))
            {
                if (d.electricible == true && d.setted == true)
                {
                    Graphics.DrawRect(d.topLeft, d.bottomRight, Color.Aqua, 0.9f, false, 2f);
                }
            }
            foreach (SurfaceStationary b in Level.CheckRectAll<SurfaceStationary>(topLeft - new Vec2(radius * 0.5f, 1f), bottomRight + new Vec2(radius * 0.5f, 1f)))
            {
                if (b.reinforced)
                {
                    Graphics.DrawRect(b.topLeft, b.bottomRight, Color.Aqua, 0.9f, false, 2f);
                }
            }
        }
    }

    public class CEDAP : Device
    {
        public float radius = 16;
        public CEDAP(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/CED.png"), 18, 12, false);
            graphic = _sprite;
            center = new Vec2(9f, 6);
            collisionSize = new Vec2(10f, 10f);
            collisionOffset = new Vec2(-5f, -5f);
            setTime = 1.5f;

            placeable = true;
            breakable = true;
            electricible = true;

            setted = true;

            DeviceCost = 10;

            destroyedPoints = "Shockwire destroyed";
        }

        public override void Set()
        {
            base.Set();
            foreach (Operators d in Level.current.things[typeof(Operators)])
            {
                //DinamicSFX.Play(d.position, this.position, 1f, 1f, "Claymore.wav");
            }
        }


        public override void Update()
        {
            base.Update();
            if (!jammed)
            {
                if (ElectroFrames <= 0)
                {
                    Level.Add(new Electricity(x, y, 64f, alpha));
                    ElectroFrames = 10;

                    if (Rando.Float(1) > 0.80f)
                    {
                        Level.Add(new SoundSource(position.x, position.y, 80, "SFX/Devices/CEDIdleSound.wav", "J"));
                        DuckNetwork.SendToEveryone(new NMSoundSource(position, 80, "SFX/Devices/CEDIdleSound.wav", "J"));
                    }
                }
                if (ElectroFrames > 0)
                {
                    ElectroFrames--;
                }

                foreach (Operators d in Level.CheckRectAll<Operators>(topLeft, bottomRight))
                {
                    if (d.team != team && d.elecFrames <= 0)
                    {
                        d.elecFrames = 35;
                    }
                }

                foreach (Device d in Level.CheckCircleAll<Device>(position, radius))
                {
                    if (d.electricible && d != this)
                    {
                        d.Electrice();
                        if (d.ElectroFrames <= 1)
                        {
                            d.ElectroFrames = 10;
                        }
                    }
                }

                foreach (SurfaceStationary s in Level.CheckCircleAll<SurfaceStationary>(position, radius))
                {
                    if (s.reinforced)
                    {
                        s.electrified = true;
                    }
                }
            }
        }


        public override void Draw()
        {
            base.Draw();

            if (DevConsole.showCollision)
            {
                Graphics.DrawCircle(position, radius, Color.Aqua, 1f, 1f, 32);
            }
        }
    }
}
