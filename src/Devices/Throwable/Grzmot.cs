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
    public class Grzmot : Throwable
    {
        public Grzmot(float xval, float yval) : base(xval, yval)
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
            index = 26;

            drawTraectory = true; 
            minimalTimeOfHolding = 0.5f;
            needsToBeGentle = false;
        }

        public override void SetRocky()
        {
            rock = new GrzmotAP(position.x, position.y);
            base.SetRocky();
        }
    }


    public class GrzmotAP : Rocky
    {
        public float radius = 80;
        public GrzmotAP(float xval, float yval) : base(xval, yval)
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
                if(op.team != "Def" && setted && Level.CheckLine<Block>(position, op.position) == null && op.undetectable <= 0)
                {
                    Flash();
                }
            }
        }

        public virtual void Flash()
        {
            for (int i = 0; i < 3; i++)
            {
                Level.Add(new Stunlight(position.x, position.y, 1.5f, radius + 32f, 0.8f));
                Level.Add(new Stunlight(position.x, position.y, 0.2f, radius + 32f));
            }
            
            Level.Add(new SoundSource(position.x, position.y, 360, "SFX/Devices/ConcussionActivate.wav", "J"));
            DuckNetwork.SendToEveryone(new NMSoundSource(position, 360, "SFX/Devices/ConcussionActivate.wav", "J"));

            Break();
        }

        public override void OnDrawLayer(Layer layer)
        {
            base.OnDrawLayer(layer);
            if(layer == Layer.Foreground)
            {
                if(setFrames > 0 && setted)
                {
                    Graphics.DrawCircle(position, setFrames, Color.White * alpha, 2f, 0.1f);
                    setFrames--;
                }
            }
        }
    }
}