using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Devices|Deployable")]
    public class CastleBarricade : Placeable
    {
        public CastleBarricade(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/Barricade.png"), 10, 32, false);
            graphic = _sprite;
            center = new Vec2(5f, 16f);
            setTime = 3f;
            frame = 1;
            collisionOffset = new Vec2(-5f, -16f);
            collisionSize = new Vec2(10f, 32f);
            weight = 0.9f;
            thickness = 6f;
            placeable = true;
            zeroSpeed = false;

            doorPlacement = true;
            UsageCount = 3;

            DeviceCost = 15;
            descriptionPoints = "Barricaded";

            index = 18;

            cantProne = true;
            placeSound = "SFX/Devices/castlebarricade.wav";
        }

        public override void Update()
        {
            base.Update();
            setted = false;
        }

        public override void Set()
        {
            base.Set();
        }

        public override void SetAfterPlace()
        {
            afterPlace = new CastleBarricadeAP(position.x, position.y);
            base.SetAfterPlace();
        }
    }
    public class CastleBarricadeAP : Device
    {
        public int load = 10;
        public bool init;
        public CastleBarricadeAP(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/Barricade.png"), 10, 32, false);
            graphic = _sprite;
            center = new Vec2(5f, 16f);
            _sprite.CenterOrigin();
            setTime = 1;
            frame = 1;
            collisionOffset = new Vec2(-5f, -16f);
            collisionSize = new Vec2(10f, 32f);
            weight = 0.9f;
            thickness = 6f;
            placeable = true;
            zeroSpeed = false;
            breakable = true;
            bulletproof = true;
            health = 1200;
            scannable = false;

            jammResistance = true;

            DeviceCost = 10;
            destroyedPoints = "Barricade destroyed";

            pickupSound = "SFX/Devices/BreakCastleBarricade.wav";

            pickUpTime = 1.8f;
        }

        public virtual void Init()
        {
            Level.Add(new BreakableSurface(position.x - 3, position.y, 2f, 36) { breakableMode = "S", vertical = true, minY = 6 });
            Level.Add(new BreakableSurface(position.x + 3, position.y, 2f, 36) { breakableMode = "S", vertical = true, minY = 6 });
        }

        public override void Break()
        {
            DuckNetwork.SendToEveryone(new NMRemoveCastleBarricade(this));
            foreach (BreakableSurface b in Level.CheckRectAll<BreakableSurface>(topLeft, bottomRight))
            {
                if (b.breakableMode == "S")
                {
                    Level.Remove(b);
                }
            }
            base.Break();
        }

        public override void OnPickUp()
        {

            DuckNetwork.SendToEveryone(new NMRemoveCastleBarricade(this));
            foreach (BreakableSurface b in Level.CheckRectAll<BreakableSurface>(topLeft, bottomRight))
            {
                if(b.breakableMode == "S")
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
                bool remove = true;
                foreach (BreakableSurface b in Level.CheckRectAll<BreakableSurface>(topLeft, bottomRight))
                {
                    if (b.breakableMode == "S")
                    {
                        remove = false;
                    }
                }
                if (remove)
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
                position = d.position;
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