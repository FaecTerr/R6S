using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class OperatorSkins : Thing
    {
        public float targetSize;
        public SpriteMap _sprite;
        public bool selected = false;

        public bool targeted;

        public bool picked;
        public bool locked;

        public Vec2 defPos;

        public int screen;

        public int operatorID;
        public OPEQ opeq;

        public OperatorSkins(float xpos, float ypos, int id) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/OperatorIcons.png"), 24, 24, false);
            graphic = _sprite;
            _sprite.CenterOrigin();
            center = new Vec2(12, 12);
            collisionSize = new Vec2(24, 24);
            collisionOffset = new Vec2(-12, -12);
            depth = 0.4f;
            layer = Layer.Foreground;
            _sprite.depth = 0.41f;
            defPos = position;

            operatorID = id;
            Init();
        }

        public void Init()
        {
            opeq = PlayerStats.GetOpeqByID(operatorID, position);
        }

        public override void Update()
        {
            if (_sprite != null)
            {
                _sprite.frame = operatorID;
            }

            float pos = 0;
            if (Level.current is CustomizationLevel)
            {
                pos = (Level.current as CustomizationLevel).moving;
            }

            position.y = (defPos.y);

            if (opeq == null)
            {
                targeted = true;
                _sprite.frame = operatorID;
            }
            else
            {
                targetSize = 1f;
                targeted = false;
            }

            collisionSize = new Vec2(24, 24) * scale;
            collisionOffset = new Vec2(-12, -12) * scale;

            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
            if (opeq != null)
            {
                Graphics.DrawStringOutline(opeq.name, position + new Vec2(-110, 0), Color.White, Color.Black, 0.1f, null, 1f);
                Graphics.DrawRect(position + new Vec2(-120, -12), position + new Vec2(20, 12), Color.Black * 0.4f, 0f, true, 1f);
            }
        }
    }
}