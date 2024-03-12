using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class DeadBody : PhysicsObject
    {
        public OperatorHead head;
        public SpriteMap _sprite;
        public SpriteMap _hand;
        public SpriteMap _head;

        public string team;

        public DeadBody(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Operators/jager.png"), 32, 32, false);
            _hand = new SpriteMap(GetPath("Sprites/Operators/jagerArms.png"), 16, 16, false);
            _head = new SpriteMap(GetPath("Sprites/Operators/jagerHat.png"), 32, 32, false);
            
            _sprite.frame = 17;

            _head.center = new Vec2(16, 16);
            _hand.CenterOrigin();
            _sprite.CenterOrigin();

            _sprite.depth = 0.4f;
            _head.depth = 0.45f;
            weight = 0f;
            flammable = 0f;
            thickness = 0;
            center = new Vec2(16, 16);
            collisionSize = new Vec2(14f, 11f);
            collisionOffset = new Vec2(-7f, 4f);
        }


        public override void Draw()
        {
            _head.center = new Vec2(16, 16);
            _hand.CenterOrigin();
            _sprite.frame = 17;
            _head.alpha = alpha;
            _sprite.alpha = alpha;
            _sprite.flipH = offDir != 1;
            Graphics.Draw(_sprite, position.x, position.y);

            _head.flipV = true;
            _head.angle = -2.3f * offDir;
            _head.flipH = offDir != 1;
            if (offDir > 0)
            {
                _head.scale = new Vec2(1, -1);
            }
            if (offDir < 0)
            {
                _head.scale = new Vec2(1, 1);
            }

            Graphics.Draw(_head, position.x - 10f * offDir, position.y + 9f);
            base.Draw();
        }
    }
}
