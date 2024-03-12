using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class CustomizeThing : Thing
    {
        public float targetSize;
        //public SpriteMap _sprite;
        public string location;
        public bool selected = false;

        public bool targeted;

        public bool picked;
        public bool locked;

        public Vec2 defPos;

        public int category;
        public string name = "";

        public CustomizeThing(float xpos, float ypos) : base(xpos, ypos)
        {
            center = new Vec2(12, 12);
            collisionSize = new Vec2(24, 24);
            collisionOffset = new Vec2(-12, -12);
            depth = 0.4f;
            layer = Layer.Foreground;
            //_sprite.depth = 0.41f;
            defPos = position;
            
            Init();
        }

        public void Init()
        {

        }

        public override void Update()
        {                     
            targetSize = 1f;
            targeted = false;

            collisionSize = new Vec2(24, 24) * scale;
            collisionOffset = new Vec2(-12, -12) * scale;

            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
            if (name != "")
            {
                Graphics.DrawStringOutline(name, position + new Vec2(-110, 0), Color.White, Color.Black, 0.1f, null, 1f);
                Graphics.DrawRect(position + new Vec2(-120, -12), position + new Vec2(20, 12), Color.Black * 0.4f, 0f, true, 1f);
            }
            if(category == 2)
            {
                SpriteMap _sprite = new SpriteMap(GetPath("Sprites/GUI/" + location), 67, 67);
                _sprite.CenterOrigin();
                _sprite.scale = new Vec2(0.4f, 0.4f);

                Graphics.Draw(_sprite, position.x, position.y);
            }

        }
    }
}