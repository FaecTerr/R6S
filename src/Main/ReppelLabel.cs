using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators")]
    public class ReppelLabel : Thing
    {
        public SpriteMap _sprite;
        public float length;

        public ReppelLabel(float xpos, float ypos) : base(xpos, ypos)
        {          
            _sprite = new SpriteMap(GetPath("Sprites/Reppel.png"), 32, 3, false);
            graphic = _sprite;
            center = new Vec2(16, 1.5f);
            collisionSize = new Vec2(30f, 1f);
            collisionOffset = new Vec2(-15f, -0.5f);
        }

        public override void Update()
        {
            base.Update();
            Block b = Level.CheckRay<Block>(position + new Vec2(-9*offDir, 0), position + new Vec2(-9*offDir, 480));
            if(b != null)
            {
                //DevConsole.Log(Convert.ToString(length), Color.White);
                length = ((position + new Vec2(-9f * offDir)) - b.position).length;
            }
            
        }

        public override void Draw()
        {
            base.Draw();
            _sprite.flipH = offDir == -1;
        }
    }
}
