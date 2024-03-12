using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class SettingsButton : Thing
    {
        public float targetSize;
        public SpriteMap _sprite;
        public bool selected = false;

        public bool targeted;

        public bool picked;
        public bool locked;

        public int screen;


        public SettingsButton(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/AlphaPack/Settings.png"), 24, 24, false);
            graphic = _sprite;
            _sprite.CenterOrigin();
            center = new Vec2(12, 12);

            collisionSize = new Vec2(24, 24);
            collisionOffset = new Vec2(-12, -12);
            depth = 0.4f;

            layer = Layer.Foreground;

            _sprite.depth = 0.41f;
        }

        public override void Update()
        {

            if (!picked && !locked)
            {
                if (_sprite.scale.x > targetSize)
                {
                    _sprite.scale = new Vec2(_sprite.scale.x - 0.01f, _sprite.scale.y - 0.01f);
                }
                if (_sprite.scale.x < targetSize)
                {
                    _sprite.scale = new Vec2(_sprite.scale.x + 0.01f, _sprite.scale.y + 0.01f);
                }
                if (scale.x > targetSize)
                {
                    scale = new Vec2(scale.x - 0.04f, scale.y - 0.04f);
                }
                if (scale.x < targetSize)
                {
                    scale = new Vec2(scale.x + 0.04f, scale.y + 0.04f);
                }
            }

            if (Mouse.positionScreen.x > topLeft.x && Mouse.positionScreen.x < bottomRight.x && Mouse.positionScreen.y > topLeft.y && Mouse.positionScreen.y < bottomRight.y && graphic.alpha > 0.5f)
            {
                targeted = true;
                targetSize = 0.6f;
            }
            else
            {
                targetSize = 0.5f;
                targeted = false;
            }


            collisionSize = new Vec2(24, 24) * scale;
            collisionOffset = new Vec2(-12, -12) * scale;

            if (Mouse.left == InputState.Pressed && targeted)
            {
                Level.current = new SettingsLevel();

                /*if (Level.current is ShopLevel)
                {
                    (Level.current as ShopLevel).screen = 4;
                }
                if(Level.current is MainMenu)
                {
                    Level.current = new ShopLevel() { screen = 4 };
                }*/
            }


            base.Update();
        }
    }
}
