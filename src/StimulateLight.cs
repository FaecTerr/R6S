using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    public class Stimulatelight : Thing
    {
        public StateBinding _positionStateBinding = new CompressedVec2Binding("position");
        protected SpriteMap _sprite;
        private float radius;
        private int outFrame = 0;
        private SinWave _pulse1 = 0.2f;
        private SinWave _pulse2 = 0.2f;
        public bool IsLocalDuckAffected
        {
            get;
            set;
        }
        public float Timer
        {
            get;
            set;
        }

        public Stimulatelight(float xval, float yval, Color color, float stayTime = 2f, float radius = 160f, float alp = 1f) : base(xval, yval)
        {
            Timer = stayTime;
            this.depth = 1f;
            this.layer = Layer.Foreground;
            this.radius = radius;
            SetIsLocalDuckAffected();
            _sprite = new SpriteMap(Graphics.Recolor(Mod.GetPath<R6S>("Sprites/Buff.png"), color.ToVector3()), 32, 32);     
            this._sprite.CenterOrigin();
            this._sprite.alpha = alp;
        }

        public virtual void SetIsLocalDuckAffected()
        {
            List<Duck> ducks = new List<Duck>();
            foreach (Duck duck in Level.CheckCircleAll<Duck>(position, radius))
            {
                if (!ducks.Contains(duck))
                {
                    ducks.Add(duck);
                }
            }
            foreach (Ragdoll ragdoll in Level.CheckCircleAll<Ragdoll>(position, radius))
            {
                if (!ducks.Contains(ragdoll._duck))
                {
                    ducks.Add(ragdoll._duck);
                }
            }
            foreach (Duck duck in ducks)
            {
                if (duck.profile.localPlayer)
                {
                    if (Level.CheckLine<Block>(position, duck.position, duck) == null)
                    {
                        IsLocalDuckAffected = true;
                        return;
                    }
                }
            }
            IsLocalDuckAffected = false;
        }

        public override void Update()
        {
            base.Update();
            _sprite.xscale = Level.current.camera.width / (31 - _pulse1*0.2f);
            _sprite.yscale = Level.current.camera.height / (31 - _pulse1*0.2f);
            if(Timer > 0f)
            _sprite.alpha = 0.9f + _pulse2*0.2f;
            if (Timer > 0)
            {
                Timer -= 0.01666666f;
            }
            else
            {
                _sprite.alpha -= 0.011f;
                outFrame++;
            }
            if (this.outFrame > 90)
            {
                Level.Remove(this);
            }
        }

        public override void Draw()
        {
            if (IsLocalDuckAffected)
            {
                //updater.ShaderController();
                Graphics.Draw(_sprite, Level.current.camera.center.x, Level.current.camera.center.y );
            }
            base.Draw();
        }
    }
}
