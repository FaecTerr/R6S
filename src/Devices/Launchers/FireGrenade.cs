using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Grenades")]
    [BaggedProperty("isFatal", false)]
    public class FireGrenade : Throwable
    {
        public FireGrenade(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/FireGrenade.png"), 16, 16, false);
            graphic = _sprite;
            center = new Vec2(8f, 8f);
            collisionOffset = new Vec2(-4f, -4f);
            collisionSize = new Vec2(8f, 8f);

            Timer = 1f;
            bouncy = 0.4f;
            friction = 0.05f;
        }

            //QuickFlash();
            //Flash();
            /*for(int  i = 0; i < 16; i++)
            {
                LandFire f = new LandFire(this.position.x, this.position.y, 10f);
                f.vSpeed = -Math.Abs(4f - i * 0.5f);
                f.hSpeed = 4f - i*0.5f;
                Level.Add(f);
            }
            Level.Remove(this);
            HasExploded = true;*/
       
    }

    public class FireGrenadeAP : Rocky
    {
        public FireGrenadeAP(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/FireGrenade.png"), 16, 16, false);
            graphic = _sprite;
            center = new Vec2(8f, 8f);
            collisionOffset = new Vec2(-4f, -4f);
            collisionSize = new Vec2(8f, 8f);
            
            bouncy = 0.4f;
            friction = 0.05f;

            isGrenade = true;
        }

        public override void DetonateFull()
        {
            for (int i = 0; i < 16; i++)
            {
                LandFire f = new LandFire(this.position.x, this.position.y, 10f);
                f.vSpeed = -Math.Abs(4f - i * 0.5f);
                f.hSpeed = 4f - i * 0.5f;
                Level.Add(f);
            }
            Level.Remove(this);

            base.DetonateFull();
        }
    }
}