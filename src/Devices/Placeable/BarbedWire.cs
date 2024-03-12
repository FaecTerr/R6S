using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Devices|Deployable")]
    public class BarbedWire : Placeable
    {
        public int soundFrames;

        public BarbedWire(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/BarbedWire.png"), 16, 16, false);
            graphic = _sprite;
            _sprite.frame = 2;
            setTime = 0.5f;
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(12f, 16f);
            collisionOffset = new Vec2(-6f, -8f);
            electricible = true;
            scannable = false;
            sticky = false;
            CheckRect = new Vec2(24, 2);
            secondary = true;
            UsageCount = 2;
            index = 3;

            isSecondary = true;

            DeviceCost = 15;
            descriptionPoints = "Barbed wire set";
            destroyedPoints = "Barbed wire destroyed";

            placeSound = "SFX/Devices/BarbedWireSet.wav";
        }
        public override void Set()
        {
            base.Set();
            //Level.Add(new SoundSource(position.x, position.y, 200, "SFX/Devices/BarbedWireSet.wav", "J"));
        }

        public override void SetAfterPlace()
        {
            afterPlace = new BarbedWireAP(position.x, position.y);
            base.SetAfterPlace();
        }
    }

    public class BarbedWireAP : Placeable
    {
        public int soundFrames;

        public bool broken;

        public BarbedWireAP(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/BarbedWire.png"), 32, 16, false);
            graphic = _sprite;
            _sprite.frame = 1;
            setTime = 0.5f;
            center = new Vec2(16f, 8f);
            collisionSize = new Vec2(12f, 16f);
            collisionOffset = new Vec2(-6f, -8f);

            thickness = 0;
            health = 200;

            electricible = true;
            scannable = false;
            sticky = false;
            afterPlace = null;
            zeroSpeed = true;

            DeviceCost = 10;

            breakable = true;
            bulletproof = true;
            destructingByHands = true;
            jammResistance = true;

            health = 200;

            DeviceCost = 10;
            destroyedPoints = "Barbed wire destroyed";
        }
        public override void Set()
        {
            base.Set();
        }

        public override void Update()
        {
            base.Update();
            collisionSize = new Vec2(48f, 16f);
            collisionOffset = new Vec2(-24f, -8f);
            _sprite.frame = 0;
            xscale = 1.5f;
            if (soundFrames > 0)
            {
                soundFrames--;
            }

            if(health > 50 && health < 150)
            {
                _sprite.frame = 2;
            }
            if(health <= 50)
            {
                _sprite.frame = 3;
            }
            if(health<= 0 && !broken)
            {
                Break();
            }

            foreach (Operators d in Level.CheckRectAll<Operators>(topLeft, bottomRight))
            {
                if (!broken)
                {
                    if (d.team != team)
                    {
                        d.hSpeed *= 0.3f;
                        d.unableToSprint = 10;
                    }
                    if (soundFrames <= 0 && d.hSpeed != 0)
                    {
                        Level.Add(new SoundSource(position.x, position.y, 200, "SFX/Devices/BBactive.wav", "J"));
                        soundFrames = 30;
                    }
                }
            } 
        }

        public override void Break()
        {
            broken = true;
            canPick = false;
            breakable = false;
            electricible = false;
        }
    }
}
