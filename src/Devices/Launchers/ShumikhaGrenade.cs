using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class ShumikhaGrenade : Device
    {
        public float timer = 0.8f;
        public bool contact = false;
        public ShumikhaGrenade(float xpos, float ypos) : base(xpos, ypos)
        {
            gravMultiplier = 0.9f;
            _enablePhysics = true;
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/LifeLineGrenade.png"), 10, 10, false);
            graphic = this._sprite;
            center = new Vec2(5f, 5f);
            collisionSize = new Vec2(10f, 10f);
            collisionOffset = new Vec2(-5f, -5f);
            weight = 0.6f;
            thickness = 0.1f;
            bouncy = 0.6f;
           

            jammResistance = true;
        }
        public override void Update()
        {
            if(grounded && !contact)
            {
                contact = true;
            }
            if (contact)
            {
                timer -= 0.016666666f;
            }
            if(timer <= 0f)
            {
                Explode();
            }
            base.Update();
        }
        public virtual void Explode()
        {
            for (int i = 0; i < 9; i++)
            {
                LandFire f = new LandFire(position.x, position.y, 7f) { oper = oper, doMakeSound = i % 4 == 0 };
                f.vSpeed = -Math.Abs(4f - i * 1f);
                f.hSpeed = 4f - i * 1f;
                if (isServerForObject)
                {
                    Level.Add(f);
                }
            }

            /*LandFire f = new LandFire(position.x, position.y, 7f) { oper = oper, firePound = 96f };
            f.vSpeed = -2;
            f.hSpeed = 0;
            if (isServerForObject)
            {
                Level.Add(f);
            }*/

            Level.Add(new SoundSource(position.x, position.y, 240, "SFX/Devices/FireOpen.wav", "J"));
            DuckNetwork.SendToEveryone(new NMSoundSource(position, 240, "SFX/Devices/FireOpen.wav", "J"));

            Level.Remove(this);
        }
        public override void OnSoftImpact(MaterialThing with, ImpactedFrom from)
        {
            if (with != null)
            {
                if (!(with is Operators))
                {
                    contact = true;
                }
            }
            base.OnSoftImpact(with, from);
        }
    }
}
