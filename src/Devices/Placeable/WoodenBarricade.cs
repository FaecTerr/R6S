using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Devices|Deployable")]
    public class WoodenBarricade : Placeable
    {
        public WoodenBarricade(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/Barricade.png"), 10, 32, false);
            graphic = _sprite;
            center = new Vec2(5f, 16f);
            setTime = 2.2f;
            frame = 0;
            collisionOffset = new Vec2(-5f, -16f);
            collisionSize = new Vec2(10f, 32f);
            weight = 0.9f;
            thickness = 6f;
            placeable = true;
            zeroSpeed = false;

            doorPlacement = true;
            UsageCount = 999;

            index = 18;

            DeviceCost = 5;
            descriptionPoints = "Barricaded";

            cantProne = true;
            placeSound = "SFX/Devices/Barricading.wav";
        }

        public override void Update()
        {
            base.Update();
            setted = false;
        }

        public override void SetAfterPlace()
        {
            afterPlace = new WoodenBarricadeAP(position.x, position.y);
            base.SetAfterPlace();
        }
    }
    public class WoodenBarricadeAP : Device
    {
        public int load = 10;
        public bool init;

        public int selfexistance = 10;

        public WoodenBarricadeAP(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/Barricade.png"), 10, 32, false);
            graphic = _sprite;
            center = new Vec2(5f, 16f);
            _sprite.CenterOrigin();
            setTime = 1;
            frame = 0;
            collisionOffset = new Vec2(-5f, -16f);
            collisionSize = new Vec2(10f, 32f);
            weight = 0.9f;
            thickness = 0f;
            placeable = true;
            zeroSpeed = false;
            breakable = true;
            health = 300;
            scannable = false;

            jammResistance = true;

            DeviceCost = 0;
            descriptionPoints = "Destruction";
        }

        public virtual void Init()
        {
            Level.Add(new BreakableSurface(position.x - 2 * offDir, position.y, 1f, 36) { breakableMode = "E", vertical = true, minY = 8 });
            Level.Add(new BreakableSurface(position.x - 1 * offDir, position.y, 1f, 36) { breakableMode = "E", vertical = true, minY = 8 });
        }

        public override void Break()
        {
            DuckNetwork.SendToEveryone(new NMRemoveBarricade(this));
            foreach (BreakableSurface b in Level.CheckRectAll<BreakableSurface>(topLeft - new Vec2(4, 3), bottomRight + new Vec2(4, 3)))
            {
                if (b.breakableMode == "E")
                {
                    Level.Remove(b);
                }
            }
            Level.Add(new SoundSource(position.x, position.y, 196, "SFX/FullFrameBreak.wav", "J"));
            DuckNetwork.SendToEveryone(new NMSoundSource(position, 196, "SFX/FullFrameBreak.wav", "J"));
            base.Break();
        }

        public override void OnPickUp()
        {
            DuckNetwork.SendToEveryone(new NMRemoveBarricade(this));
            foreach (BreakableSurface b in Level.CheckRectAll<BreakableSurface>(topLeft - new Vec2(4, 3), bottomRight + new Vec2(4, 3)))
            {
                if (b.breakableMode == "E")
                {
                    Level.Remove(b);
                }
            }
            base.OnPickUp();
        }

        public override void Update()
        {
            base.Update();

            if (init)
            {
                selfexistance--;
                foreach (BreakableSurface b in Level.CheckRectAll<BreakableSurface>(topLeft - new Vec2(4, 2), bottomRight + new Vec2(4, 2)))
                {
                    if (b.breakableMode == "E")
                    {
                        selfexistance = 5;
                    }
                }
                if (selfexistance <= 0)
                {
                    Break();
                }
            }

            if (!init && load <= 0)
            {
                init = true;
                Init();
            }
            if (load > 0)
            {
                load--;
            }

            DoorFrame d = Level.CheckRect<DoorFrame>(topLeft, bottomRight);
            if (d != null)
            {
                position = d.position + new Vec2(0, 2);
            }
        }

        public override void Draw()
        {
            alpha = 1;
            _sprite.alpha = 1;
            base.Draw();
            _sprite.alpha = 1;
        }
    }
}