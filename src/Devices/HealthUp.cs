using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    public class HealthUp : Thing
    {
        public SpriteMap _sprite;
        public int lifeTime = 120;
        public HealthUp(float xval, float yval, float alp) : base(xval, yval)
        {
            alpha = alp;
            this.collisionSize = new Vec2(0f, 0f);
            this._sprite = new SpriteMap(GetPath("HealEffect"), 11, 11);
            _sprite.CenterOrigin();
            graphic = _sprite;
            _sprite.alpha = alp;
            layer = Layer.Foreground;
        }
        public override void Update()
        {
            base.Update();
            position = anchor.position - new Vec2(0f, (150f-lifeTime)/8f);
            _sprite.alpha -= 1 / 150;
            lifeTime--;
            if(lifeTime < 90)
            {
                _sprite.frame = 1;
                if (lifeTime < 60)
                {
                    _sprite.frame = 2;
                    if (lifeTime < 30)
                    {
                        _sprite.frame = 3;
                    }
                }
            }
            if(lifeTime <= 0)
            {
                Level.Remove(this);
            }
        }
    }
}
