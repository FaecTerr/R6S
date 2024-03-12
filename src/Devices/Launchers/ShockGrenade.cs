using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    public class ShockGrenade : Device
    {
        public float time = 3;
        public float radius = 104;

        public ShockGrenade(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/LifeLineGrenade.png"), 10, 10, false);
            graphic = _sprite;
            _sprite.frame = 2;

            center = new Vec2(5f, 5f);
            collisionOffset = new Vec2(-3f, -3f);
            collisionSize = new Vec2(6f, 6f);
            bouncy = 0.4f;
            weight = 0.4f;

            canPickUp = false;
            gravMultiplier = 0.9f;
            
            breakable = false;
            catchableByADS = true;
            jammResistance = true;

            team = "Att";
        }

        public override void Update()
        {
            foreach (Operators op in Level.CheckCircleAll<Operators>(position, radius))
            {
                if (Level.CheckLine<Block>(position, op.position) == null && op.team != team)
                {
                    Flash();
                    time = 0;
                }
            }

            if (Math.Abs(hSpeed) + Math.Abs(vSpeed) > 0.3f)
            {
                Level.Add(SmallSmoke.New(position.x, position.y, 0.3f, 0.4f));
            }

            base.Update();

            if (time > 0)
            {
                time -= 0.01666666f;
            }
            else
            {
                Flash();
            }
        }

        public virtual void Flash()
        {
            for (int i = 0; i < 3; i++)
            {
                Level.Add(new Stunlight(position.x, position.y, 1.5f, radius + 32, 0.8f));
                Level.Add(new Stunlight(position.x, position.y, 0.2f, radius + 32));
            }

            Level.Add(new SoundSource(position.x, position.y, 360, "SFX/Devices/ConcussionActivate.wav", "J"));
            DuckNetwork.SendToEveryone(new NMSoundSource(position, 360, "SFX/Devices/ConcussionActivate.wav", "J"));

            Break();

            Level.Remove(this);
        }
    }
}
