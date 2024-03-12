using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    [EditorGroup("Faecterr's|Furniture")]
    public class Furn : Thing
    {
        public SpriteMap _sprite;
        public EditorProperty<int> style;

        public bool init;

        public Furn(float xval, float yval) : base(xval, yval)
        {
            this._sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Decorations/Furniture.png"), 32, 32, false);
            base.graphic = _sprite;
            this.center = new Vec2(16f, 16f);
            this._sprite.frame = 2;
            this.collisionOffset = new Vec2(-15f, -15f);
            this.collisionSize = new Vec2(34f, 34f);
            this.graphic = this._sprite;
            this.hugWalls = WallHug.Floor;
            style = new EditorProperty<int>(0, this, 0, 5, 1);
            layer = Layer.Background;
            depth = 1f;
        }
        public override void Update()
        {
            base.Update();

            if(style == 4 && !init)
            {
                init = true;
                Level.Add(new SoundSource(position.x, position.y, 320, "SFX/Music/Consulate.wav", "J") { showTime = 153});
            }
        }

        public override void Draw()
        {
            _sprite.flipH = flipHorizontal;
            _sprite.frame = style;
            base.Draw();
        }
    }
}
