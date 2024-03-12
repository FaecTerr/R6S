using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Devices|Deployable")]
    public class Beartrap : Placeable
    {
        public int soundFrames;

        public Beartrap(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/Kapkan.png"), 32, 16, false);
            graphic = _sprite;
            _sprite.frame = 0;
            setTime = 1.3f;
            center = new Vec2(16f, 8f);
            collisionSize = new Vec2(12f, 16f);
            collisionOffset = new Vec2(-6f, -8f);
            electricible = true;
            scannable = false;
            sticky = false;
            CheckRect = new Vec2(28, 2);
            secondary = true;
            UsageCount = 3;
            index = 40;

            DeviceCost = 15;
            descriptionPoints = "Welcome mat set";
            destroyedPoints = "Welcome mat destroyed";

            placeSound = "SFX/Devices/BarbedWireSet.wav";
        }
        public override void Set()
        {
            base.Set();
            //Level.Add(new SoundSource(position.x, position.y, 200, "SFX/Devices/BarbedWireSet.wav", "J"));
        }

        public override void SetAfterPlace()
        {
            afterPlace = new BeartrapAP(position.x, position.y);
            base.SetAfterPlace();
        }
    }

    public class BeartrapAP : Placeable
    {
        public Operators trapped;
        public bool deactive;

        public BeartrapAP(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/Kapkan.png"), 32, 16, false);
            graphic = _sprite;
            _sprite.frame = 1;
            setTime = 0.5f;
            center = new Vec2(16f, 14f);
            collisionSize = new Vec2(26f, 2f);
            collisionOffset = new Vec2(-13f, -1f);

            thickness = 0;

            electricible = false;
            scannable = false;
            sticky = false;
            afterPlace = null;
            zeroSpeed = true;

            DeviceCost = 10;

            breakable = true;
            destructingByHands = true;
            jammResistance = true;

            health = 67;

            destroyedPoints = "Beartrap destroyed";
        }
        public override void Set()
        {
            base.Set();
        }

        public override void Update()
        {
            base.Update();

            if(Level.CheckRect<Upstairs>(topLeft, bottomRight) == null && grounded)
            {
                deactive = false;
                _sprite.frame = 1;
            }
            else
            {
                deactive = true;
                _sprite.frame = 0;
            }

            if (!deactive)
            {
                if (trapped == null)
                {
                    foreach (Operators d in Level.CheckRectAll<Operators>(topLeft, bottomRight))
                    {
                        if(d.team != "Def")
                        {
                            d.Injure();
                        }
                    }
                }
                else
                {
                    trapped.unableToMove = 10;
                    if (!trapped.DBNO)
                    {
                        trapped = null;
                        Break();
                    }
                }
            }
        }
    }
}
