using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class QualityButton : Thing
    {
        public float targetSize;
        public SpriteMap _sprite;
        public bool selected = false;

        public bool targeted;

        public bool picked;
        public bool locked;

        public Vec2 defPos;

        public int screen;

        public bool add;

        public QualityButton(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/AlphaPack/buttonRect.png"), 16, 16, false);
            graphic = _sprite;
            _sprite.CenterOrigin();
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(16, 16);
            collisionOffset = new Vec2(-8, -8);
            depth = 0.4f;
            layer = Layer.Foreground;
            _sprite.depth = 0.41f;
            defPos = position;
        }

        public override void Update()
        {
            if(_sprite != null)
            {
                if (add)
                {
                    _sprite.frame = 2;
                }
                else
                {
                    _sprite.frame = 3;
                }
            }
            float pos = 0;
            if (Level.current is ShopLevel)
            {
                pos = (Level.current as ShopLevel).moving;
            }

            if (pos < -148)
            {
                pos = -148;
            }
            if (pos > 80)
            {
                pos = 80;
            }

            position.x = defPos.x + pos;

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
                targetSize = 1.2f;

            }
            else
            {
                targetSize = 1f;
                targeted = false;
            }

            if (position.x < 12)
            {
                targetSize = (position.x + 4) / 16;
                graphic.alpha = (position.x + 12) / 16;
                if (targetSize < 0)
                {
                    targetSize = 0;
                }
            }
            else if (position.x > 140)
            {
                targetSize = (140 - position.x) / 16;
                alpha = (140 - position.x) / 16;
                if (targetSize < 0)
                {
                    targetSize = 0;
                }
            }
            else
            {
                alpha = 1;
            }

            collisionSize = new Vec2(16, 16) * scale;
            collisionOffset = new Vec2(-8, -8) * scale;

            if (Mouse.left == InputState.Pressed && targeted)
            {
                if (Level.current is ShopLevel)
                {
                    if (!(Level.current as ShopLevel).unlockingItem)
                    {
                        if (add && (Level.current as ShopLevel).quality < 4)
                        {
                            (Level.current as ShopLevel).quality += 1;
                        }
                        else if(!add && (Level.current as ShopLevel).quality > 0)
                        {
                            (Level.current as ShopLevel).quality -= 1;
                        }
                    }
                }
            }


            base.Update();
        }

        public override void Draw()
        {
            base.Draw();

        }
    }
}
