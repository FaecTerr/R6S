using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class EquipmentSkins : Thing
    {
        public float targetSize;
        public SpriteMap _sprite;
        public bool selected = false;

        public bool targeted;

        public bool picked;
        public bool locked;

        public int screen;
        
        public Operators oper;
        public int slot;
        public string name;

        public bool defaul = true;

        public int category;
        public int rarity;

        float epic;

        private int heigh = PlayerStats.totalOperators * 140;

        public EquipmentSkins(float xpos, float ypos, Operators o, SpriteMap _spr) : base(xpos, ypos)
        {
            _sprite = _spr;
            _sprite.CenterOrigin();
            graphic = _sprite;
            center = new Vec2(16, 16);
            collisionSize = new Vec2(32, 32);
            collisionOffset = new Vec2(-16, -16);
            depth = 0.4f;
            layer = Layer.Foreground;
            _sprite.depth = 0.41f;

            oper = o;            
        }

        public override void Update()
        {
            float pos = 0;
            if (Level.current is CustomizationLevel)
            {
                pos = (Level.current as CustomizationLevel).moving;
            }            

            if (oper != null && Mouse.positionScreen.x > topLeft.x && Mouse.positionScreen.x < bottomRight.x && Mouse.positionScreen.y > topLeft.y && Mouse.positionScreen.y < bottomRight.y && !locked)
            {
                targetSize = 1.2f;
                targeted = true;

                if (PlayerStats.operPreferences.Count > oper.operatorID * 20 + slot)
                {
                    if (PlayerStats.operPreferences[oper.operatorID * 20 + slot] == name)
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
            else
            {
                targeted = false;
                if (PlayerStats.operPreferences.Count > oper.operatorID * 20 + slot)
                {
                    if (PlayerStats.operPreferences[oper.operatorID * 20 + slot] == name)
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


            if (targeted && rarity == 4)
            {
                epic += 0.02f;
                if(epic > 1)
                {
                    epic = 1;
                }
            }
            else
            {
                epic -= 0.02f;
                if(epic < 0)
                {
                    epic = 0;
                }
            }

            collisionSize = new Vec2(18, 18) * scale;
            collisionOffset = new Vec2(-9, -9) * scale;

            if (Mouse.left == InputState.Pressed && targeted && oper != null && !locked)
            {
                if (Level.current is CustomizationLevel && PlayerStats.operPreferences.Count > oper.operatorID * 20 + slot)
                {
                    PlayerStats.operPreferences[oper.operatorID * 20 + slot] = name;
                    PlayerStats.Save();
                }
            }                       

            if(!(PlayerStats.openedCustoms.Contains(name)) && !defaul)
            {
                locked = true;
            }
            else
            {
                locked = false;
            }
            
            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
            SpriteMap _s = new SpriteMap(GetPath("Sprites/AlphaPack/skinRect.png"), 18, 18);
            _s.CenterOrigin();
            _s.scale = new Vec2(targetSize, targetSize);
            int r = 0;
            if (rarity == 1)
            {
                r = 1;
            }
            if (rarity == 2)
            {
                r = 2;
            }
            if (rarity == 3)
            {
                r = 3;
            }
            if(rarity == 4)
            {
                r = 4;
            }
            if (picked)
            {
                _s.frame = 1 + r * 2;
            }
            else
            {
                _s.frame = 0 + r * 2;
            }

            Graphics.Draw(_s, position.x, position.y, 0f);

            if (locked)
            {
                SpriteMap _sa = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
                _sa.scale = _sprite.scale;
                _sa.position = _sprite.position;
                _sa.center = _sprite.center;

                _sa.alpha = 0.8f * _sprite.alpha;
                _sa.frame = 76;

                Graphics.Draw(_sa, _sa.position.x, _sa.position.y, 0.42f);
            }

            SpriteMap _background = new SpriteMap(Mod.GetPath<R6S>("Sprites/GUI/BackgroundEpic.png"), 320, 240);
            _background.depth = -0.51f;
            _background.alpha = epic;

            Graphics.Draw(_background, 0, Level.current.camera.position.x, Level.current.camera.position.y);
        }
    }
}
