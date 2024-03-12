using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    public class Stair : MaterialThing, IPlatform
    {
        public SpriteMap _sprite;
        public Stair(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Blank.png"), 16, 16, false);
            graphic = _sprite;
            center = new Vec2(8f, 8f);
            collisionOffset = new Vec2(-1.5f, -1f);
            collisionSize = new Vec2(3f, 2f);
            depth = -0.5f;
        }
    }
}
