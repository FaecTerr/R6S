using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Furniture")]
    public class Sofa : Thing
    {
        public SpriteMap _sprite;
        public EditorProperty<int> style;
        public Operators o;
        public int anim;
        public int stuck;

        public Sofa(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Decorations/Sofa.png"), 42, 21, false);
            base.graphic = _sprite;
            center = new Vec2(21f, 10f);
            _sprite.frame = 2;
            collisionOffset = new Vec2(-20f, -10f);
            collisionSize = new Vec2(40f, 20f);
            graphic = _sprite;
            hugWalls = WallHug.Floor;
            style = new EditorProperty<int>(0, this, 0, 5, 1);
            layer = Layer.Background;
            depth = 1f;
        }
        public override void Update()
        {
            base.Update();

            foreach (Operators op in Level.CheckRectAll<Operators>(topLeft, bottomRight))
            {
                if (op.local && Keyboard.Pressed(PlayerStats.keyBindings[4]) && o == null)
                {
                    o = op;
                    anim = 120;
                    op._sprite.SetAnimation("injured"); 
                    Level.Add(new SoundSource(position.x, position.y, 320, "SFX/Music/Delemma.wav", "J"));
                    Level.Add(new SoundSource(position.x, position.y, 320, "SFX/Music/Majima.wav", "J"));
                    stuck = 60 * 20;
                }
            }
            if(o != null)
            {
                if (o.mode != "injured")
                {
                    o.GoProne();
                    o.mode = "injured";
                    o._sprite.SetAnimation("injured");                                     
                }
                o.position = position + new Vec2(0, -12f); 
                o.gravMultiplier = 0;
                if(stuck <= 0)
                {
                    o = null;
                }
                else
                {
                    stuck--;
                }
            }
        }

        public override void Draw()
        {
            _sprite.flipH = flipHorizontal;
            _sprite.frame = style;
            if (anim > 0)
            {
                string text = "Man, I love this shit!";
                Graphics.DrawStringOutline(text, position + new Vec2(-text.Length * 1f, -24), Color.White, Color.Black, 1f, null, 0.25f);
                anim--;
            }
            base.Draw();
        }
    }
}
