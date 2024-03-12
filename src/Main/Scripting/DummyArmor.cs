using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    [EditorGroup("Faecterr's|Scripting")]
    public class DummyArmor : Thing
    {
        public SpriteMap _sprite;
        public int armor = 2;

        public DummyArmor(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Script.png"), 16, 16, false);
            base.graphic = _sprite;
            center = new Vec2(8f, 8f);

            collisionOffset = new Vec2(-8f, -8f);
            collisionSize = new Vec2(16f, 16f);

            layer = Layer.Background;
            depth = -1f;
        }
        public override void Update()
        {
            base.Update();

            foreach (Operators op in Level.CheckRectAll<Operators>(topLeft, bottomRight))
            {
                if (op.local && Keyboard.Pressed(PlayerStats.keyBindings[4]))
                {
                    armor++;
                    if(armor >= 4)
                    {
                        armor = 1;
                    }
                    foreach(Terrorist t in Level.current.things[typeof(Terrorist)])
                    {
                        t.Armor = armor;
                    }
                }
            }
            if(!(Level.current is Editor))
            {
                _sprite.alpha = 0;
                alpha = 0;
            }
        }

        public override void Draw()
        {
            foreach (Operators op in Level.CheckRectAll<Operators>(topLeft, bottomRight))
            {
                string text = "Press [INTERACT] to\nchange dummy armor level";
                Graphics.DrawStringOutline(text, position + new Vec2(-text.Length * 1, -8f), Color.White, Color.Black, -0.6f, null, 0.5f);
            }

            Graphics.DrawRect(topLeft, bottomRight, Color.Black * 0.5f, -0.7f);
            Graphics.DrawString(Convert.ToString(armor), position, Color.White, -0.68f);
            base.Draw();
        }
    }

    [EditorGroup("Faecterr's|Scripting")]
    public class AutoJump : Thing
    {
        public SpriteMap _sprite;
        public Vec2 pos;
        public Operators oper;
        public int result;

        public AutoJump(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Script.png"), 16, 16, false);
            base.graphic = _sprite;
            center = new Vec2(8f, 8f);

            collisionOffset = new Vec2(-8f, -8f);
            collisionSize = new Vec2(16f, 16f);

            layer = Layer.Background;
            depth = -1f;
        }
        public override void Update()
        {
            base.Update();
            if(oper != null && oper.grounded)
            {
                result = (int)(new Vec2(oper.position.x, oper.bottom) - pos).length;
                oper = null;
            }

            foreach (Operators op in Level.CheckRectAll<Operators>(topLeft, bottomRight))
            {
                if (op.local)
                {
                    op.jump = true;
                    oper = op;
                    pos = new Vec2(op.position.x, op.bottom);
                }
            }
            if (!(Level.current is Editor))
            {
                _sprite.alpha = 0;
                alpha = 0;
            }
        }

        public override void Draw()
        {
            Graphics.DrawStringOutline(Convert.ToString(result), position, Color.White, Color.Black, 1f);
            Graphics.DrawRect(topLeft, bottomRight, Color.Black * 0.5f, -0.7f);
            base.Draw();
        }
    }
}
