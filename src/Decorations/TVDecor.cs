using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Furniture")]
    public class TVDecor : Thing
    {
        public SpriteMap _sprite;
        public int anim;
        public int fram;
        public bool init;
        public bool used;

        public TVDecor(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Decorations/TV.png"), 48, 48, false);
            base.graphic = _sprite;
            center = new Vec2(24f, 24f);
            collisionOffset = new Vec2(-24f, -24f);
            collisionSize = new Vec2(48f, 48f);
            graphic = _sprite;
            hugWalls = WallHug.Floor;

            depth = -0.6f;
        }
        public override void Update()
        {
            base.Update();
            if (!init && used)
            {
                Level.Add(new SoundSource(position.x, position.y, 320, "SFX/Music/delemma.wav", "J") { showTime = 150 });
                init = true;
            }
            foreach (Operators op in Level.CheckRectAll<Operators>(topLeft, bottomRight))
            {
                if (op.local && Keyboard.Pressed(PlayerStats.keyBindings[4]))
                {
                    used = !used;
                }
            }
        }

        public override void Draw()
        {
            _sprite.flipH = flipHorizontal;
            SpriteMap _video = new SpriteMap(Mod.GetPath<R6S>("Sprites/Decorations/TVContainment.png"), 64, 40, false);
            _video.scale = new Vec2(0.5f, 0.5f);
            _video.CenterOrigin();
            if(anim <= 0)
            {
                fram++;
                if(fram > 5)
                {
                    fram = 0;
                }
                anim = 9;
            }
            else
            {
                anim--;
            }
            _video.frame = fram;
            Graphics.Draw(_video, position.x, position.y - 10);
            base.Draw();
        }
    }
}
