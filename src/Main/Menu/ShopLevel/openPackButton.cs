using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class OpenPackButton : Thing
    {
        public float targetSize;
        public SpriteMap _sprite;
        public bool selected = false;

        public bool targeted;

        public bool picked;
        public bool locked;

        public Vec2 defPos;

        public int screen;
        public int quality;
        public int cost = 1000;

        public OpenPackButton(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/AlphaPack/openPack.png"), 53, 26, false);
            graphic = _sprite;
            _sprite.CenterOrigin();
            center = new Vec2(26.5f, 13f);
            collisionSize = new Vec2(53, 26);
            collisionOffset = new Vec2(-26.5f, -13);
            depth = 0.4f;
            layer = Layer.Foreground;
            _sprite.depth = 0.41f;
            defPos = position;
        }

        public override void Update()
        {
            if (_sprite != null)
            {
                
            }
            float pos = 0;
            if (Level.current is ShopLevel)
            {
                pos = (Level.current as ShopLevel).moving * 2;
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

            collisionSize = new Vec2(53, 26) * scale;
            collisionOffset = new Vec2(-26.5f, -13) * scale;
            if (Level.current is ShopLevel)
            {
                quality = (Level.current as ShopLevel).quality;
                cost = 1000;
                if (quality == 1)
                {
                    cost = 100;
                }
                if (quality == 2)
                {
                    cost = 175;
                }
                if (quality == 3)
                {
                    cost = 250;
                }
                if (quality == 4)
                {
                    cost = 350;
                }
                if (quality == 5)
                {
                    cost = 450;
                }
            }
            if (Mouse.left == InputState.Pressed && targeted && Level.current is ShopLevel)
            {
                if (((PlayerStats.renown >= cost && quality == 0) || (PlayerStats.materials >= cost && quality > 0)) && !(Level.current as ShopLevel).unlockingItem && (Level.current as ShopLevel).screen == 1)
                {

                    (Level.current as ShopLevel).unlockingItem = true;


                    bool m = false;
                    if (quality > 0)
                    {
                        m = true;
                    }


                    (Level.current as ShopLevel).unlockTimer = 420;
                    (Level.current as ShopLevel).OpenPack();
                    if (!m)
                    {
                        if (PlayerStats.packs > 0)
                        {
                            PlayerStats.packs--;
                        }
                        else
                        {
                            PlayerStats.renown -= cost;
                            PlayerStats.exp += 25;
                        }
                    }
                    else
                    {
                        PlayerStats.materials -= cost;
                        PlayerStats.exp += 25;
                    }

                    PlayerStats.Save();
                }
            }


            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
            if(PlayerStats.packs > 0 && quality == 0)
            {
                string text = "Free (" + Convert.ToString(PlayerStats.packs) + ")";
                Graphics.DrawString(text, position + new Vec2(-2 * text.Length, 6), Color.White, 1f, null, scale.x * 0.5f);
            }
            else
            {
                string text = Convert.ToString(cost);
                Graphics.DrawString(text, position + new Vec2(-2 * text.Length, 6), Color.White, 1f, null, scale.x* 0.5f);
            }
        }
    }
}
