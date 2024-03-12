using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    [EditorGroup("Faecterr's|Furniture")]
    public class Ventilation : Thing
    {
        public SpriteMap _sprite;

        public bool init;

        public Ventilation(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Decorations/Vents.png"), 16, 18, false);
            base.graphic = _sprite;
            center = new Vec2(8f, 8f);
            collisionOffset = new Vec2(-8f, -8f);
            collisionSize = new Vec2(16f, 16f);
            graphic = _sprite;
            hugWalls = WallHug.Floor;
            _sprite.AddAnimation("idle", 0.1f, true, new int[] { 0, 1, 2, 3});
            _sprite.SetAnimation("idle");
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
            else
            {
                if(_sprite.frame == 2)
                {
                    SmallSmoke s = SmallSmoke.New(position.x, position.y);
                    s.hSpeed = 0.2f;
                    s.vSpeed = 0.6f;
                    s.alpha = 0.2f;
                    s.alphaSub = 0.2f;
                    Level.Add(s);
                    
                }
            }
        }

        public override void Draw()
        {
            _sprite.flipH = flipHorizontal;
            base.Draw();
        }
    }
}
