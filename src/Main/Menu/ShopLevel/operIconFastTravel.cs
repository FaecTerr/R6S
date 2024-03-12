using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class operIconFastTravel : Thing
    {
        public float targetSize;
        public int orderID;
        public SpriteMap _sprite;
        public bool selected = false;
        public OPEQ opeq;

        public bool targeted;

        public bool picked;
        public bool locked;

        public operIconFastTravel(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/OperatorIcons.png"), 24, 24, false);
            graphic = _sprite;
            _sprite.center = new Vec2(12, 12);
            center = new Vec2(12, 12);
            collisionSize = new Vec2(24, 24);
            collisionOffset = new Vec2(-12, -12);
            depth = 0.4f;
            layer = Layer.Foreground;
            _sprite.depth = 0.41f;
            scale = new Vec2(0.4f, 0.4f);
        }

        public override void Initialize()
        {
            /*OPEQ op = PlayerStats.GetOpeqByCallID(operatorID, position);
            if (op != null && op.oper != null)
            {
                opeq = op;
            }
            else
            {
                opeq = PlayerStats.GetOpeqByID(operatorID, position);
            }*/

            base.Initialize();
        }

        public override void Update()
        {
            if (_sprite != null && opeq != null && opeq.oper != null)
            {
                _sprite.frame = opeq.oper.operatorID;
            }
            /*
            int m = operatorID / 10;
            int l = operatorID % 10;
            */
            int m = 0, l = 0;

            m = orderID / 10;
            l = orderID % 10;
            

            position = new Vec2(0, Level.current.camera.position.y) + new Vec2(220 + 10 * l, 12 + 10 * m);

            if (!picked && !locked)
            {
                if (_sprite.scale.x > targetSize)
                {
                    _sprite.scale = new Vec2(_sprite.scale.x - 0.01f, _sprite.scale.y - 0.01f);
                }
                if (_sprite.scale.x < targetSize)
                {
                    _sprite.scale = new Vec2(_sprite.scale.x + 0.01f, _sprite.scale.y + 0.01f);
                }
                if (scale.x > targetSize)
                {
                    scale = new Vec2(scale.x - 0.04f, scale.y - 0.04f);
                }
                if (scale.x < targetSize)
                {
                    scale = new Vec2(scale.x + 0.04f, scale.y + 0.04f);
                }
            }

            if (Mouse.positionScreen.x > topLeft.x && Mouse.positionScreen.x < bottomRight.x && Mouse.positionScreen.y > topLeft.y && Mouse.positionScreen.y < bottomRight.y)
            {
                targeted = true;
                targetSize = 0.5f;

                foreach (OperatorSkins op in Level.current.things[typeof(OperatorSkins)])
                {
                    if (opeq != null && op.opeq != null)
                    {
                        if (opeq.name == op.opeq.name && Mouse.left == InputState.Pressed)
                        {
                            (Level.current as CustomizationLevel).moving = op.defPos.y - 20;
                        }
                    }
                }
            }
            else
            {
                targetSize = 0.4f;
                targeted = false;
            }

            collisionSize = new Vec2(20, 20) * scale;
            collisionOffset = new Vec2(-10, -10) * scale;

            base.Update();
        }
    }
}
