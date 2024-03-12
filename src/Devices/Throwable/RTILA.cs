using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Devices|Throwable")]
    public class RTILA : Throwable
    { 
        public RTILA(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/RTILA.png"), 16, 16, false);
            graphic = this._sprite;
            _sprite.frame = 0;
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(16f, 16f);
            collisionOffset = new Vec2(-8f, -8f);
            setTime = 0.8f;
            index = 32;
            sticky = true;

            weightR = 7;

            setFramesLoad = 120;

            UsageCount = 2;
            placeSound = "SFX/Devices/RtilaStick.wav";
            pickupSound = "SFX/Devices/RtilaPick.wav"; 
            minimalTimeOfHolding = 0.75f;
            needsToBeGentle = false;
        }

        public override void SetRocky()
        {
            rock = new RTILAAP(position.x, position.y);
            base.SetRocky();
        }
    }

    public class RTILAAP : Rocky
    {
        float radius = 38;
        public RTILAAP(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/RTILA.png"), 16, 16, false);
            graphic = _sprite;
            _sprite.frame = 1;
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(16f, 16f);
            collisionOffset = new Vec2(-8f, -8f);

            sticky = true;

            scannable = true;
            breakable = true;
            electricible = true;
        }

        public override void Set()
        {
            base.Set();
            //SOUND
        }

        public override void Update()
        {
            base.Update(); 
            if (setFrames < 2 && !jammed)
            {
                if (ElectroFrames <= 0)
                {
                    Level.Add(new Electricity(x, y, 64f, alpha));
                    ElectroFrames = 10;

                    if(Rando.Float(1) > 0.8f)
                    Level.Add(new SoundSource(position.x, position.y, 80, "SFX/Devices/CEDIdleSound.wav", "J"));
                    DuckNetwork.SendToEveryone(new NMSoundSource(position, 80, "SFX/Devices/CEDIdleSound.wav", "J"));
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
                    if (d.electricible == true)
                    {
                        d.Electrice(); 
                        if (d.ElectroFrames <= 1)
                        {
                            d.ElectroFrames = 10;
                        }
                    }
                }
                foreach(SurfaceStationary s in Level.CheckCircleAll<SurfaceStationary>(position, radius))
                {
                    if(s.reinforced)
                    {
                        s.electrified = true;
                    }
                }
            }
        }

        public override void Draw()
        {
            base.Draw();
            if (setFrames > 0 && setted)
            {
                Graphics.DrawCircle(position, (120f - setFrames) * (radius / 120f), Color.White, 2f, 1f, 32);
                setFrames--;
            }
        }
    }
}
