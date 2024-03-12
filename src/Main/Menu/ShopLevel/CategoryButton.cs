using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class CategoryButton : Thing
    {
        public float targetSize;
        public float size = 1f;
        public SpriteMap _sprite;
        public bool selected = false;

        public bool targeted;

        public float scal = 0.5f;

        public bool picked;
        public bool locked;

        public Vec2 defPos;

        public int screen;


        public CategoryButton(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/GUI/ShopIcons.png"), 64, 32, false);
            graphic = _sprite;
            _sprite.CenterOrigin();
            center = new Vec2(32, 16);
            collisionSize = new Vec2(64, 32);
            collisionOffset = new Vec2(-32, -16);
            depth = 0.4f;
            layer = Layer.Foreground;
            _sprite.depth = 0.41f;
            defPos = position;

            alpha = 0;
        }

        public override void Update()
        {
            if (_sprite != null)
            {
                _sprite.frame = screen - 1;
            }


            if (Level.current is ShopLevel)
            {
                float pos = (Level.current as ShopLevel).moving;
                if (pos < -48)
                {
                    pos = -48;
                }
                if (pos > 80)
                {
                    pos = 80;
                }
                position.x = defPos.x + pos;

            }


            if (size < targetSize)
            {
                size += 0.02f;
            }
            if (size > targetSize)
            {
                size -= 0.02f;
            }

            if (!picked && !locked && Level.current is ShopLevel)
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

            if (Mouse.positionScreen.x > topLeft.x && Mouse.positionScreen.x < bottomRight.x && Mouse.positionScreen.y > topLeft.y && Mouse.positionScreen.y < bottomRight.y && ((graphic.alpha > 0.5f && Level.current is ShopLevel) || Level.current is MainMenu))
            {
                targeted = true;
                targetSize = 1.2f;
            }
            else
            {
                targetSize = 1f;
                targeted = false;
            }
            if(targetSize < 1)
            {
                targetSize = 1;
            }

            if (Level.current is ShopLevel)
            {
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
            }

            if (Level.current is MainMenu)
            {
                if (screen == 1)
                {
                    collisionSize = new Vec2(48, 16) * scale * scal;
                    collisionOffset = new Vec2(-24, -8) * scale * scal;
                }

                if (screen == 2)
                {
                    collisionSize = new Vec2(44, 16) * scale * scal;
                    collisionOffset = new Vec2(-22, -8) * scale * scal;
                }

                if (screen == 3)
                {
                    collisionSize = new Vec2(58, 16) * scale * scal;
                    collisionOffset = new Vec2(-29, -8) * scale * scal;
                }
            }
            else
            {
                collisionSize = new Vec2(64, 32) * scale * scal;
                collisionOffset = new Vec2(-32, -16) * scale * scal;
            }

            if (Mouse.left == InputState.Pressed && targeted)
            {
                if(Level.current is ShopLevel)
                {
                    (Level.current as ShopLevel).screen = screen;
                }
                if (Level.current is MainMenu)
                {
                    if (screen != 3)
                    {
                        Level.current = new ShopLevel() { screen = screen };
                    }
                    else
                    {
                        Level.current = new CustomizationLevel();
                    }
                }
            }


            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
            if (Level.current is MainMenu)
            {
                _sprite.alpha = 0;
                alpha = 0;

                string text = "";
                if(screen == 1)
                {
                    text = "Alpha pack";
                }
                if (screen == 2)
                {
                    text = "Operators";
                }
                if (screen == 3)
                {
                    text = "Customization";
                }

                Graphics.DrawStringOutline(text, position + new Vec2(text.Length * (-2), -6) * scal, Color.White * (size - 0.4f), Color.Black * (size - 0.7f) * 2, 0.3f, null, 0.5f * scal);
                Graphics.DrawLine(position + new Vec2((text.Length * (-2) - 4) * (size - 1f) * 5 * scal, 2), position + new Vec2((text.Length * 2 + 4) * (size - 1f) * 5 * scal, 2), Color.White * (size - 0.7f) * 2, 0.6f, 0.3f);
            }
        }
    }
}
