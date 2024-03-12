using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    [EditorGroup("Faecterr's|Scripting")]
    public class DoorFrame : Thing
    {
        public SpriteMap _sprite;
        public EditorProperty<bool> barricaded;
        public DoorFrame(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/DoorFrame.png"), 12, 40, false);
            base.graphic = _sprite;
            center = new Vec2(6f, 22f);
            _sprite.frame = 2;
            collisionOffset = new Vec2(-6f, -18f);
            collisionSize = new Vec2(12f, 36f);
            graphic = _sprite;
            hugWalls = WallHug.Floor;
            barricaded = new EditorProperty<bool>(false);
            
        }

        public override void Initialize()
        {
            if (barricaded && !(Level.current is Editor))
            {
                WoodenBarricadeAP w = new WoodenBarricadeAP(position.x, position.y);
                Level.Add(w);
                w.setted = true;
                w.team = "Def";
                w.canPickUp = true;
                w.offDir = (sbyte)(flipHorizontal ? 1 : -1);
                w.layer = Layer.Game;
            }
            base.Initialize();
        }
        public override void Update()
        {
            base.Update();
        }
    }
}
