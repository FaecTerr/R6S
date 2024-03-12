using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    public class HitScan : Thing
    {
        protected SpriteMap _sprite;
        public float Timer = 0.5f;

        public bool device;
        public GamemodeScripter g;

        public HitScan(float xval, float yval, float stayTime = 0.1f) : base(xval, yval)
        {
            depth = 1f;
            layer = Layer.Foreground;          
            SetIsLocalDuckAffected();
            Timer = stayTime;
            _sprite = new SpriteMap(GetPath("Sprites/hitMarker.png"), 17, 17);
            _sprite.CenterOrigin();
        }

        public virtual void SetIsLocalDuckAffected()
        {
            List<Operators> ducks = new List<Operators>();
            foreach (Operators duck in Level.CheckCircleAll<Operators>(position, 9999))
            {
                if (!ducks.Contains(duck))
                {
                    ducks.Add(duck);
                }
            }
            foreach (Operators duck in ducks)
            {
                if (duck.local)
                {
                    if (Level.CheckLine<Block>(position, duck.position, duck) == null)
                    {
                        return;
                    }
                }
            }
            foreach(GamemodeScripter gm in Level.current.things[typeof(GamemodeScripter)])
            {
                g = gm;
            }
        }

        public override void Update()
        {
            base.Update();
            if (Timer > 0)
            {
                Timer -= 0.01f;
            }
            
            if (Timer <= 0)
            {
                Level.Remove(this);
            }
        }

        public override void Draw()
        {
            _sprite.frame = 0;
            if (Timer < 0.05f)
            {
                _sprite.frame = 2;
            }

            if (device)
            {
                _sprite.frame = 1;
                if(Timer < 0.05f)
                {
                    _sprite.frame = 3;
                }
            }

            Vec2 pos = Mouse.positionScreen;
            
            if(g != null)
            {
                if(g.selected != null)
                {
                    if(g.selected.oper != null)
                    {
                        pos = g.selected.oper.aim;
                    }
                }
            }

            Graphics.Draw(_sprite, pos.x, pos.y, 1f);    
            base.Draw();
        }
    }
}
