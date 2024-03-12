using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Grenades")]
    [BaggedProperty("isFatal", false)]
    public class Razorbloom : Throwable
    {
        public Razorbloom(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/Grzmot.png"), 16, 16, false);
            graphic = _sprite;
            center = new Vec2(8f, 8f);
            collisionOffset = new Vec2(-4f, -6f);
            collisionSize = new Vec2(8f, 12f);
            thickness = 0.1f;

            sticky = true;
            weightR = 6;

            scannable = true;

            UsageCount = 3;
            index = 62;

            drawTraectory = true;
            minimalTimeOfHolding = 0.5f;
            needsToBeGentle = false;
        }

        public override void SetRocky()
        {
            rock = new RazorbloomAP(position.x, position.y);
            base.SetRocky();
        }
    }


    public class RazorbloomAP : Rocky
    {
        public float radius = 80;
        public RazorbloomAP(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/Grzmot.png"), 16, 16, false);
            graphic = _sprite;
            center = new Vec2(8f, 8f);
            collisionOffset = new Vec2(-4f, -6f);
            collisionSize = new Vec2(8f, 12f);

            thickness = 0.1f;

            setFrames = (int)radius - 1;

            breakable = true;
            bulletproof = false;

            sticky = true;
        }


        public override void Update()
        {
            base.Update();

            foreach (Operators op in Level.CheckCircleAll<Operators>(position, radius))
            {
                if (op.team != "Def" && setted && Level.CheckLine<Block>(position, op.position) == null && op.undetectable <= 0)
                {

                }
            }
        }

        public virtual void Flash()
        {

            Break();
        }

        public override void OnDrawLayer(Layer layer)
        {
            base.OnDrawLayer(layer);
            if (layer == Layer.Foreground)
            {
                if (setFrames > 0 && setted)
                {
                    Graphics.DrawCircle(position, setFrames, Color.White * alpha, 2f, 0.1f);
                    setFrames -= 2;
                }
            }
        }
    }
}