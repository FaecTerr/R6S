using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class ChangePage : Thing, IDrawToDifferentLayers
    {
        public float targetSize;

        public SpriteMap _sprite;

        public bool selected = false;

        public int page;

        public ChangePage(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Sprites/ModMenu/PageCheck.png"), 9, 9, false);
            _sprite.CenterOrigin();
            center = new Vec2(4.5f, 4.5f);
            graphic = _sprite;
            layer = Layer.Foreground;
            depth = 0.6f;
            scale *= 0.6f;
            collisionSize = new Vec2(9, 9) * scale;
            collisionOffset = new Vec2(-4.5f, -4.5f) * scale;
        }

        public void OnDrawLayer(Layer pLayer)
        {
            if (pLayer == Layer.Foreground)
            {

            }
        }

        public override void Update()
        {
            if (Mouse.positionScreen.x > topLeft.x && Mouse.positionScreen.x < bottomRight.x && Mouse.positionScreen.y > topLeft.y && Mouse.positionScreen.y < bottomRight.y)
            {
                targetSize = 1.2f;
                selected = true;
            }
            else
            {
                targetSize = 1f;
                selected = false;
            }

            if (Level.current is MainMenu)
            {
                if (selected && Mouse.left == InputState.Pressed)
                {
                    (Level.current as MainMenu).page = page;
                    (Level.current as MainMenu).nextPage = 0;
                }
                if((Level.current as MainMenu).page == page)
                {
                    if (selected)
                    {
                        _sprite.frame = 1;
                    }
                    else
                    {
                        _sprite.frame = 3;
                    }
                }
                else
                {
                    if (selected)
                    {
                        _sprite.frame = 0;
                    }
                    else
                    {
                        _sprite.frame = 2;
                    }
                }
            }
            base.Update();
        }
    }
}


