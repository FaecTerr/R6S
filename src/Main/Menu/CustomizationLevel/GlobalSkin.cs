using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class GlobalSkins : Thing
    {
        public float targetSize;
        public SpriteMap _sprite;
        public bool selected = false;

        public bool targeted;

        public bool picked;
        public bool locked;

        public int screen;

        public int slot;
        public string name;

        public bool defaul = true;

        public int category;
        public int rarity;

        public GlobalSkins(float xpos, float ypos, SpriteMap _spr) : base(xpos, ypos)
        {
            _sprite = _spr;
            _sprite.CenterOrigin();
            graphic = _sprite;
            center = new Vec2(28, 28);
            collisionSize = new Vec2(56, 56);
            collisionOffset = new Vec2(-28, -28);
            depth = 0.4f;
            layer = Layer.Foreground;
            _sprite.depth = 0.41f;
            _sprite.frame = 1;
        }

        public override void Update()
        {
            float pos = 0;
            if (Level.current is CustomizationLevel)
            {
                pos = (Level.current as CustomizationLevel).moving;
            }

            if (Mouse.positionScreen.x > topLeft.x && Mouse.positionScreen.x < bottomRight.x && Mouse.positionScreen.y > topLeft.y && Mouse.positionScreen.y < bottomRight.y && !locked)
            {
                targetSize = 1.2f;
                targeted = true;

                if (PlayerStats.globalPreferences.Count > slot)
                {
                    if (PlayerStats.globalPreferences[slot] == name)
                    {
                        picked = true;
                    }
                    else
                    {
                        picked = false;
                    }
                }
            }
            else
            {
                targetSize = 1;
                targeted = false;
                if (PlayerStats.globalPreferences.Count > slot)
                {
                    if (PlayerStats.globalPreferences[slot] == name)
                    {
                        targetSize = 1.2f;
                        picked = true;
                    }
                    else
                    {
                        targetSize = 1f;
                        picked = false;
                    }
                }
            }

            _sprite.scale = scale;
            collisionSize = new Vec2(56, 56) * scale;
            collisionOffset = new Vec2(-28, -28) * scale;
            Maths.LerpTowards(scale.x, targetSize, 0.1f);
            Maths.LerpTowards(scale.y, targetSize, 0.1f);

            if (Mouse.left == InputState.Pressed && targeted && !locked)
            {
                if (Level.current is CustomizationLevel && PlayerStats.globalPreferences.Count > slot)
                {
                    PlayerStats.globalPreferences[slot] = name;
                    PlayerStats.Save();
                }
            }

            if (!defaul)
            {
                picked = PlayerStats.globalPreferences[slot] == name ? true : false;
            }
            else
            {
                picked = (PlayerStats.globalPreferences[slot] == "" || PlayerStats.globalPreferences[slot] == null) ? true : false;
            }

            if (!(PlayerStats.openedCustoms.Contains(name)) && !defaul)
            {
                locked = true;
            }
            else
            {
                locked = false;
            }

            if (!picked)
            {
                _sprite.alpha = 0.65f;
                alpha = 0.65f;
            }
            else
            {
                _sprite.alpha = 1f;
                alpha = 1f;
            }

            if (locked)
            {
                _sprite.color = Color.DarkGray * 0.35f;
            }

            base.Update();
        }

        public override void Draw()
        {
            if (locked)
            {
                SpriteMap _s = new SpriteMap(GetPath("Sprites/GUI/Locked.png"), 17, 17);
                _s.CenterOrigin();
                _s.scale = new Vec2(targetSize, targetSize);
                _sprite.scale = new Vec2(targetSize, targetSize);
                Graphics.Draw(_s, position.x - collisionSize.x * 0.4f, position.y - collisionSize.y * 0.4f, 0f);
            }
            base.Draw();
        }
    }
}
