using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    public class EMP : Thing
    {
        public StateBinding _positionStateBinding = new CompressedVec2Binding("position");
        protected SpriteMap _sprite;
        protected SpriteMap _sprite1;
        private float radius = 80f;
        public bool IsLocalDuckAffected;
        public float Timer;
        public float radius1;
        public float radius2;
        public float radius3;
        public float radius4;
        public float radius5;

        public bool init;

        public string team;

        //public List<Device> d = new List<Device>();
        public List<ObservationThing> obs = new List<ObservationThing>();

        public EMP(float xval, float yval, float stayTime = 0.6f, float radius = 160f) : base(xval, yval)
        {
            Timer = stayTime;
            layer = Layer.Foreground;
            this.radius = radius;
            center = new Vec2(8f, 8f);
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/empLabel.png"), 16, 16);
            _sprite1 = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/empLabel.png"), 16, 16);
            _sprite.center = new Vec2(8f, 8f);
            _sprite1.center = new Vec2(8f, 8f);
        }

        public virtual void Init()
        {
            foreach (Device d in Level.CheckCircleAll<Device>(position, radius))
            {
                if (d.team == "Def" || d is OBSCamera || d is EDDAP || d is CEDAP || d is ADSAP || d is BulletProofCameraAP || d is JammerAP || d is PulseScanner || d is C4thing || 
                    d is ProximityAlarmAP || d is GuMineAP || d is YokaiAP || d is BlackEyeAP)
                {
                    d.jammedFrames = 900;
                    d.jammed = true;
                }
            }

            foreach (LightningSun s in Level.CheckCircleAll<LightningSun>(position, radius))
            {
                s.TurnOff();
            }

            foreach(Operators o in Level.CheckCircleAll<Operators>(position, radius))
            {
                if (o.HasEffect("EMP'd"))
                {
                    o.GetEffect("EMP'd").timer = 15f;
                }
                else
                {
                    o.effects.Add(new EMPdEffect());
                }               
            }
        }

        public override void Update()
        {
            base.Update();
            if (!init)
            {
                init = true;
                Init();
            }
            foreach(ObservationThing o in obs)
            {
                o.jammed = true;
            }
            radius1 += 1f;
            if (radius1 > 6f)
            {
                radius2 += 1f;
                if (radius2 > 6f)
                {
                    radius3 += 1f;
                    if (radius3 > 6f)
                    {
                        radius4 += 1f;
                        if (radius4 > 6f)
                        {
                            radius5 += 1f;
                        }
                    }
                }
            }
            _sprite.xscale = _sprite.yscale += 2f;
            _sprite.alpha -= 0.1f;
            if (Timer > 0)
            {
                Timer -= 0.01f;
            }
            else
            {
                Level.Remove(this);
            }

        }
        public override void Draw()
        {
            base.Draw();
            Graphics.Draw(_sprite, position.x, position.y, 1f);

            if (DevConsole.showCollision)
            {
                Graphics.DrawCircle(position, radius, Color.BlueViolet, 1f, 1f, 32);
            }
        }
    }
}

