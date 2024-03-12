using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    public class Flashlight : Thing
    {
        public StateBinding _positionStateBinding = new CompressedVec2Binding("position");
        protected SpriteMap _sprite;
        private float radius;
        private int outFrame = 0;
        private SinWave _pulse2 = Rando.Float(0.5f, 4f);
        public bool IsLocalDuckAffected;

        public float Timer
        {
            get;
            set;
        }

        public Flashlight(float xval, float yval, float stayTime = 2f, float radius = 128f, float alp = 1f) : base(xval, yval)
        {
            Timer = stayTime;
            depth = 1f;
            layer = Layer.Foreground;
            this.radius = radius;
            SetIsLocalDuckAffected();
            if (IsLocalDuckAffected)
            {
                SFX.Play(GetPath("SFX/Devices/flashBeep.wav"), 1f, 0.0f, 0.0f, false);
            }
            _sprite = new SpriteMap(GetPath("Sprites/FlashbangLight.png"), 32, 32);
            _sprite.alpha = alp;
        }
  
        public virtual void SetIsLocalDuckAffected()
        {
            List<Operators> operators = new List<Operators>();
            foreach (Operators oper in Level.CheckCircleAll<Operators>(position, radius))
            {
                if (!operators.Contains(oper))
                {
                    operators.Add(oper);
                }
            }           
            foreach (Operators oper in operators)
            {
                if (oper.local)
                {
                    if (Level.CheckLine<Block>(position, oper.position) == null && Level.CheckLine<SmokeGR>(position, oper.position) == null && oper.flashImmuneFrames <= 0)
                    {
                        IsLocalDuckAffected = true;

                        if((position - oper.position).length > (radius / 2))
                        {
                            Timer *= (radius - (position - oper.position).length / 2) / (radius);
                        }

                        if(position.x > oper.position.x && oper.offDir == -1)
                        {
                            Timer *= 0.75f;
                        }
                        if (position.x < oper.position.x && oper.offDir == 1)
                        {
                            Timer *= 0.75f;
                        }

                        return;
                    }
                }
            }
            IsLocalDuckAffected = false;
        }

        public override void Update()
        {
            base.Update();
            _sprite.xscale = Level.current.camera.width/32;
            _sprite.yscale = Level.current.camera.height/32;
            _sprite.angleDegrees = 0f + _pulse2*0.1f;
            if (Timer > 0)
            {
                Timer -= 0.01f;
            }
            else
            {
                _sprite.alpha -= 0.011f;
                outFrame++;
            }
            //SoundEffect.SpeedOfSound = 1f - this.alpha;
            if (outFrame > 90)
            {
                Level.Remove(this);
            }
        }

        public override void Draw()
        {
            if (IsLocalDuckAffected)
            {
                //updater.ShaderController();
                Graphics.Draw(_sprite, Level.current.camera.position.x, Level.current.camera.position.y, 1f);
            }
            base.Draw();
        }
    }
}
