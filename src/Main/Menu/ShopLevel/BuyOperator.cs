using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class BuyOperator : Thing
    {
        public float targetSize;
        public int operatorID;
        public SpriteMap _sprite;
        public bool selected = false;
        public OPEQ opeq;

        public bool targeted;

        public bool picked;
        public bool locked;

        public Vec2 defPos;


        public BuyOperator(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/OperatorIcons.png"), 24, 24, false);
            graphic = _sprite;
            _sprite.center = new Vec2(12, 12);
            center = new Vec2(12, 12);
            collisionSize = new Vec2(24, 24);
            collisionOffset = new Vec2(-12, -12);
            depth = 0.4f;
            layer = Layer.Foreground;
            _sprite.depth = 0.41f;
            defPos = position;
        }

        public override void Initialize()
        {
            opeq = PlayerStats.GetOpeqByID(operatorID, position);

            base.Initialize();
        }

        public override void Update()
        {
            if (_sprite != null)
            {
                _sprite.frame = operatorID;
            }

            float pos = 0;
            if(Level.current is ShopLevel)
            {
                pos = (Level.current as ShopLevel).moving;
            }

            if (pos < -480)
            {
                pos = -480;
            }
            if(pos > 600)
            {
                pos = 600;
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

            if(position.x < 20)
            {
                targetSize = (position.x - 20 + 16) / 16;
                graphic.alpha = (position.x - 20 + 16) / 16;
                if (targetSize < 0.3f)
                {
                    targetSize = 0;
                }
            }
            else if (position.x > 140)
            {
                targetSize = (140 - position.x - 16) / 16;
                alpha = (140 - position.x - 16) / 16;
                if (targetSize < 0.3f)
                {
                    targetSize = 0;
                }
            }
            else
            {
                alpha = 1;
            }

            if(targetSize <= 0)
            {
                alpha = 0f;
            }
            else
            {
                alpha = targetSize;
            }

            collisionSize = new Vec2(20, 20) * scale;
            collisionOffset = new Vec2(-10, -10) * scale;

            if (Mouse.left == InputState.Pressed && opeq != null && targeted)
            {
                foreach(Confirm c in Level.current.things[typeof(Confirm)])
                {
                    c.op = null;
                    c.op = this;
                }
            }

            foreach (Confirm c in Level.current.things[typeof(Confirm)])
            {
                if(c.op == this)
                {
                    selected = true;
                }
                else
                {
                    selected = false;
                }
            }

            base.Update();
        }

        public override void Draw()
        {
            base.Draw();

            if (!PlayerStats.openedOperators.Contains(operatorID))
            {
                SpriteMap _s = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
                _s.scale = _sprite.scale;
                _s.position = _sprite.position;
                _s.center = _sprite.center;

                _s.alpha = 0.8f * _sprite.alpha;
                _s.frame = 76;

                Graphics.Draw(_s, _s.position.x, _s.position.y, 0.42f);
            }

            if (opeq != null && selected)
            {
                Graphics.DrawStringOutline(opeq.name, Level.current.camera.position - new Vec2(120, 160) + new Vec2(320, 180), Color.White, Color.Black, 0.6f, null, 1.5f);
                Graphics.Draw(opeq._sprite, Level.current.camera.position.x - 146 + 320, Level.current.camera.position.y - 167 + 180, 0.6f);

                Graphics.DrawStringOutline(opeq.description, Level.current.camera.position - new Vec2(145, 135) + new Vec2(320, 180), Color.White, Color.Black, 0.6f, null, 0.52f);

                if (opeq != null)
                {
                    SpriteMap _ping = new SpriteMap(GetPath("Sprites/Cameras/ObservationMarker.png"), 24, 24);
                    _ping.CenterOrigin();

                    SpriteMap _dot = new SpriteMap(GetPath("Sprites/blackDot.png"), 1, 1);
                    _dot.scale = new Vec2(160f, 180f);
                    _dot.CenterOrigin();
                    _dot.alpha = 0.4f;

                    Graphics.Draw(_dot, Level.current.camera.position.x + 170, Level.current.camera.position.y, 0.55f);

                    Graphics.DrawStringOutline("Armor", Level.current.camera.position - new Vec2(130, 20) + new Vec2(320, 160), Color.White, Color.Black, 0.6f, null, 1f);

                    for (int i = 0; i < opeq.oper.Armor; i++)
                    {
                        _ping.frame = 0;
                        Graphics.Draw(_ping, Level.current.camera.position.x - 130 + 20 * i + 320, Level.current.camera.position.y + 160, 0.6f);
                    }
                    for (int i = 0; i < 3 - opeq.oper.Armor; i++)
                    {
                        _ping.frame = 4;
                        Graphics.Draw(_ping, Level.current.camera.position.x - 90 - 20 * i + 320, Level.current.camera.position.y + 160, 0.6f);
                    }

                    Graphics.DrawStringOutline("Speed", Level.current.camera.position - new Vec2(60, 20) + new Vec2(320, 160), Color.White, Color.Black, 0.6f, null, 1f);

                    for (int i = 0; i < opeq.oper.Speed; i++)
                    {
                        _ping.frame = 0;
                        Graphics.Draw(_ping, Level.current.camera.position.x - 60 + 20 * i + 320, Level.current.camera.position.y + 160, 0.6f);
                    }
                    for (int i = 0; i < 3 - opeq.oper.Speed; i++)
                    {
                        _ping.frame = 4;
                        Graphics.Draw(_ping, Level.current.camera.position.x - 20 - 20 * i + 320, Level.current.camera.position.y + 160, 0.6f);
                    }

                    if (!PlayerStats.openedOperators.Contains(operatorID))
                    {
                        int cost = 1000;
                        Graphics.DrawStringOutline("Unlock with " + cost + " renown", Level.current.camera.position + new Vec2(176, 124), Color.Yellow, Color.Black, 0.6f, null, 0.75f);
                    }
                }
            }
        }
    }
}
