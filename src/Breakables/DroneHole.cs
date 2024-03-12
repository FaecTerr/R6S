using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    [EditorGroup("Faecterr's|Furniture")]
    public class DroneHole : Thing
    {
        public SpriteMap _sprite;

        public DroneHole()
        {
            collisionSize = new Vec2(16, 8);
            collisionOffset = new Vec2(-8, -4);
            _sprite = new SpriteMap(GetPath("Sprites/Decorations/DroneHole.png"), 18, 11);
            //_sprite.center = new Vec2(9, 7);
            center = new Vec2(9, 7);
            graphic = _sprite;
            hugWalls = WallHug.Floor;
            depth = -0.6f;
        }
    }
}
