using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class GrowthShieldKnife : Knife
    {
        public GrowthShieldKnife(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/HandShield.png"), 32, 32, false);
            graphic = _sprite;
            _sprite.frame = 7;
            center = new Vec2(16f, 16f);
            _sprite.center = new Vec2(16f, 16f);

            
            setTime = 0.8f;
            weight = 0.9f;
            thickness = 0.4f;
            placeable = false;
            setted = false;
            scannable = false;

            size = 4;

            defaultKnife = false;
        }

        public override void Update()
        {
            base.Update();
        }
    }
    public class FlashShieldKnife : Knife
    {
        public FlashShieldKnife(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/FlashShield.png"), 32, 32, false);
            _sprite.CenterOrigin();
            graphic = _sprite;
            center = new Vec2(16f, 16f);


            setTime = 0.8f;
            weight = 0.9f;
            thickness = 0.4f;
            placeable = false;
            setted = false;
            scannable = false;

            size = 4;

            defaultKnife = false;
        }

        public override void Update()
        {
            base.Update();
        }
    }

    public class BallisticShieldKnife : Knife
    {
        public BallisticShieldKnife(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/BallisticShield.png"), 32, 32, false);
            _sprite.CenterOrigin();
            graphic = _sprite;
            center = new Vec2(16f, 16f);

            setTime = 0.8f;
            weight = 0.9f;
            thickness = 0.4f;
            placeable = false;
            setted = false;
            scannable = false;

            size = 4;

            defaultKnife = false;
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
