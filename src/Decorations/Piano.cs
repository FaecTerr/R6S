using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    [EditorGroup("Faecterr's|Furniture")]
    public class Piano : Thing
    {
        public SpriteMap _sprite;

        public bool init;

        public Piano(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Decorations/Piano.png"), 40, 32, false);
            base.graphic = _sprite;
            center = new Vec2(20f, 16f);
            _sprite.frame = 2;
            collisionOffset = new Vec2(-15f, -15f);
            collisionSize = new Vec2(34f, 34f);
            graphic = _sprite;
            hugWalls = WallHug.Floor;

            depth = -0.6f;

        }
        public override void Update()
        {
            base.Update();

            if (!init)
            {
                init = true;
                //Level.Add(new SoundSource(position.x, position.y, 320, "SFX/Music/Consulate.wav", "J") { showTime = 153 });
            }
        }

        public override void Draw()
        {
            _sprite.flipH = flipHorizontal;
            base.Draw();
        }
    }
}
