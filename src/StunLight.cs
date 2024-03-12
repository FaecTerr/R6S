using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    public class Stunlight : Thing
    {
        public StateBinding _positionStateBinding = new CompressedVec2Binding("position");
        protected SpriteMap _sprite;
        private float radius;
        private int outFrame = 0;

        private SinWave _pulse1 = Rando.Float(1f, 2f);
        private SinWave _pulse2 = Rando.Float(0.5f, 4f);

        public bool IsLocalAffected;
        public float Timer;

        public Stunlight(float xval, float yval, float stayTime = 2f, float radius = 160f, float alp = 1f) : base(xval, yval)
        {
            Timer = stayTime;
            
            depth = 1f;
            layer = Layer.Foreground;

            this.radius = radius;
            SetIsLocalDuckAffected();
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/StunLight.png"), 32, 32);

            _sprite.alpha = alp;
        }

        public virtual void SetIsLocalDuckAffected()
        {
            List<Operators> oper = new List<Operators>();
            foreach (Operators op in Level.CheckCircleAll<Operators>(position, radius))
            {
                if (!oper.Contains(op))
                {
                    oper.Add(op);
                }
            }

            foreach (Operators op in oper)
            {
                if (op.local)
                {
                    if (Level.CheckLine<Block>(position, op.position, op) == null)
                    {
                        if(op.operatorID == 27 || op.operatorID == 26)
                        {
                            Timer *= 0.5f;
                        }

                        if(position.x > op.position.x && op.offDir < 0)
                        {
                            Timer *= 0.85f;
                        }
                        if (position.x < op.position.x && op.offDir > 0)
                        {
                            Timer *= 0.85f;
                        }
                                                
                        if((position - op.position).length > radius / 2)
                        {
                            float rad = radius / 2;
                            Timer *= rad / (position - op.position).length;
                        }

                        op.deafenFrames = (int)(Timer * 90);
                        op.unableToSprint = 120;
                        IsLocalAffected = true;

                        SFX.Play(GetPath("SFX/Devices/ConcussionEffect"));

                        return;
                    }
                }
            }
            IsLocalAffected = false;
        }

        public override void Update()
        {
            base.Update();
            _sprite.xscale = Level.current.camera.width/32;
            _sprite.yscale = Level.current.camera.height/32;
            _sprite.angleDegrees = 0f + _pulse2 * 0.1f;

            if (Timer > 0)
            {
                Timer -= 0.01f;
            }
            else
            {
                _sprite.alpha -= 0.011f;
                outFrame++;
            }
            if(outFrame > 90)
            {
                Level.Remove(this);
            }
        }

        public override void Draw()
        {
            if (IsLocalAffected)
            {
                Graphics.Draw(_sprite, Level.current.camera.position.x, Level.current.camera.position.y, 1f);
            }
            base.Draw();
        }
    }
}
