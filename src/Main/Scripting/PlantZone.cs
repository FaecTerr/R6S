using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckGame;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Scripting")]
    public class PlantZone : Thing
    {
        private SpriteMap _sprite;
        public PlantZone(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/BlueSquare.png"), 16, 16, false);
            base.graphic = new SpriteMap(Mod.GetPath<R6S>("Sprites/BlueSquare.png"), 16, 16, false);
            //center = new Vec2(8f, 8f);
            _sprite.frame = 2;
            collisionOffset = new Vec2(0f, 0f);
            collisionSize = new Vec2(16f, 16f);
            graphic = _sprite;
            _visibleInGame = false;
            alpha = 0;
            layer = Layer.Foreground;
        }

        public override void Draw()
        {
            
            //Graphics.DrawRect(topLeft, bottomRight, Color.Yellow * 0.3f, 1f);
            base.Draw();
        }
    }
}
