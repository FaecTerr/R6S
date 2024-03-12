using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class Rocky : Device
    {
        public bool sticky;
        public int setFrames;
        public int setFramesLoad;

        public bool catched;

        public Vec2 Dir;

        public List<Bullet> firedBullets = new List<Bullet>();
        public bool detonate;
        public float time = 1f;

        public int frames = 10;

        public bool isGrenade;

        public MaterialThing stickedTo;
        public string StickedSound = "";
        public bool sensitiveToSurface = false;

        public Rocky(float xpos, float ypos) : base(xpos, ypos)
        {
            setted = false;
            weight = 1f;
            grounded = false;
        }

        public virtual void DetonateFull()
        {
            Level.Remove(this);
        }

        public virtual void Detonate(float delay = 0)
        {
            detonate = true;
            time = delay;
        }

        public virtual void NotDetonated()
        {

        }

        public override void Update()
        {
            if (frames > 0)
            {
                frames--;
            }
            if (detonate)
            {
                if (time > 0)
                {
                    time -= 0.01666666f;
                    NotDetonated();
                }
                else
                {
                    DetonateFull();
                }
            }

            if(vSpeed > 4f)
            {
                _skipPlatforms = true;
            }
            else
            {
                _skipPlatforms = false;
            }

            if (grounded && frames <= 0 && sticky && !setted && stickedTo == null)
            {
                angleDegrees = 90 * offDir;
                if (Level.CheckLine<MaterialThing>(position, position + new Vec2(0, 4)) != null)
                {
                    Impact(Level.CheckLine<MaterialThing>(position, position + new Vec2(0, 4)), ImpactedFrom.Bottom);
                }

                _enablePhysics = false;
                grounded = true;
                _hSpeed = 0f;
                _vSpeed = 0f;
                gravMultiplier = 0f;
                setted = true;
                Set();
                canPickUp = false;
                frame = 1;
                setFrames = setFramesLoad;
                Dir = new Vec2(0, 1);

                if (StickedSound != "")
                {
                    Level.Add(new SoundSource(position.x, position.y, 240, StickedSound, "J"));
                    DuckNetwork.SendToEveryone(new NMSoundSource(position, 240, StickedSound, "J"));
                }
            }
            base.Update();

            if(stickedTo != null && _enablePhysics == false && setted && sticky && !grounded && stickedTo.removeFromLevel)
            {
                Dir = new Vec2(0, 0);
                _enablePhysics = true;
                gravMultiplier = 1;
                setted = false;
                frame = 0;
                stickedTo = null;
            }
        }

        public virtual void Impact(MaterialThing with, ImpactedFrom from)
        {

        }

        public virtual void OnStick()
        {

        }

        public override void OnSoftImpact(MaterialThing with, ImpactedFrom from)
        {
            base.OnSoftImpact(with, from);
            if (sticky)
            {
                Impact(with, from);
                if (with != null && setted == false)
                {
                    if ((with is Block || with is DeployableShieldAP) && !(with is Door) && setted == false && stickedTo == null)
                    {
                        if (from == ImpactedFrom.Right)
                        {
                            angleDegrees = 0;
                            Dir = new Vec2(1f, 0f);
                        }
                        if (from == ImpactedFrom.Left)
                        {
                            angleDegrees = 0;
                            Dir = new Vec2(-1f, 0f);
                        }
                        if (from == ImpactedFrom.Top)
                        {
                            angleDegrees = -90 * offDir;
                            Dir = new Vec2(0f, -1f);
                        }
                        if (from == ImpactedFrom.Bottom)
                        {
                            angleDegrees = 90 * offDir;
                            Dir = new Vec2(0f, 1f);
                        }

                        _enablePhysics = false;
                        _hSpeed = 0f;
                        _vSpeed = 0f;
                        gravMultiplier = 0f;
                        setted = true;
                        canPickUp = false;
                        frame = 1;
                        setFrames = setFramesLoad;
                        stickedTo = with;

                        if (StickedSound != "")
                        {
                            Level.Add(new SoundSource(position.x, position.y, 240, StickedSound, "J"));
                            DuckNetwork.SendToEveryone(new NMSoundSource(position, 240, StickedSound, "J"));
                        }
                        OnStick();
                    }
                }
            }
        }
    }
}
