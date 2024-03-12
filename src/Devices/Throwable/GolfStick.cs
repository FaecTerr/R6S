using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Grenades")]

    public class GolfStick : Throwable
    {
        public GolfStick(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/GolfStick.png"), 16, 16, false);
            graphic = _sprite;
            center = new Vec2(6f, 3f);
            collisionOffset = new Vec2(-4f, -5f);
            collisionSize = new Vec2(8f, 12f);
            bouncy = 0.4f;
            friction = 0.05f;
            UsageCount = 1;
            index = 9;

            weightR = 8.5f;

            drawTraectory = true;
        }

        public override void SetRocky()
        {
            rock = new GolfBallAP(position.x, position.y);
            base.SetRocky();
        }
    }

    public class GolfBallAP : Rocky
    {
        public GolfBallAP(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/Ball.png"), 16, 16, false);
            graphic = _sprite;
            center = new Vec2(8f, 8f);
            collisionOffset = new Vec2(-1.5f, -1.5f);
            collisionSize = new Vec2(3f, 3f);
            bouncy = 0.7f;
            
            breakable = false;

            pickUpTime = 0.2f;
        }

        public override void DetonateFull()
        {
            base.DetonateFull();
        }


        public override void Update()
        {
            base.Update();
            canPickUp = true;
            angle += hSpeed;
            canPick = true;
            setted = true;
        }
    }
}