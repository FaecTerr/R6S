using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Devices|Weapon")]
    public class AirjabLauncher : Launchers
    {
        public AirjabLauncher(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/GrenadeCannon.png"), 22, 14, false);
            graphic = _sprite;
            _sprite.frame = 2;
            center = new Vec2(11f, 7f);
            collisionSize = new Vec2(20f, 12f);
            collisionOffset = new Vec2(-10f, -6f);
            _canRaise = true;
            weight = 0.9f;
            thickness = 0.1f;
            placeable = false;
            zeroSpeed = false;
            scannable = false;

            index = 33;

            _editorName = "AirJab Cannon";

            Missiles1 = 3;
            placeSound = "SFX/Devices/AirJabShot.wav";
            pickupSound = "SFX/Devices/NomadClick.wav";
        }

        public override void SetMissile()
        {
            Vec2 pos = new Vec2(position.x + (float)(Math.Cos(angle) * 10 * offDir), position.y + (float)(Math.Sin(angle) * 2));
            missile = new Airjab(pos.x, pos.y);
            base.SetMissile();
        }
    }

    public class Airjab : Device
    {
        float radius = 48;
        float stunLength = 2.5f;
        float power = 4;

        int soundFrames;

        public Airjab(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/Airjab.png"), 16, 16, false);
            graphic = _sprite;
            center = new Vec2(8f, 8f);
            collisionOffset = new Vec2(-3f, -3f);
            collisionSize = new Vec2(6f, 6f);
            gravMultiplier = 0f;
            weight = 0.9f;
            thickness = 0.1f;

            canPickUp = false;
            zeroSpeed = false;
            
            bulletproof = false;
            destructingByElectricity = true;
            catchableByADS = true;
            breakable = true;

            setTime = 1f;
        }

        public override void Update()
        {
            if (setted)
            {
                if(soundFrames <= 0)
                {
                    soundFrames = 90;
                    Level.Add(new SoundSource(position.x, position.y, radius * 2, "SFX/Devices/AirJabidle.wav", "J"));
                    DuckNetwork.SendToEveryone(new NMSoundSource(position, radius * 2, "SFX/Devices/AirJabidle.wav", "J"));
                }
                else
                {
                    soundFrames--;
                }


                foreach (Operators op in Level.CheckCircleAll<Operators>(position, radius))
                {
                    if(op.team != "Att" && !jammed && Level.CheckLine<Block>(op.position, position) == null && setTime <= 0)
                    {
                        Explode();
                    }
                }
            }
            base.Update();
        }

        public override void Break()
        {            
            base.Break();
            Level.Remove(this);
        }

        public virtual void Explode()
        {
            foreach (Operators op in Level.CheckCircleAll<Operators>(position, radius))
            {
                if (Level.CheckLine<Block>(op.position, position) == null)
                {
                    op.Jump(0.4f);
                    op.grounded = false;
                    op.unableToStand = (int)(stunLength * 60);
                    op.unableToCrouch = (int)(stunLength * 60);
                    op.unableToMove = (int)(stunLength * 60);
                    op.unableToChangeState = (int)(stunLength * 60);
                    op.lockWeaponChange = (int)(stunLength * 60);
                    op.deafenFrames = (int)(stunLength * 60);
                    op.concussionFrames = (int)(stunLength * 60);
                    op.hSpeed += (op.position.x > position.x ? 1 : -1) * power;
                    op.ADS = 0;
                    op.inADS = 0;
                    op.isADS = false;
                    op.slideFrames = (int)(stunLength * 30);
                }
            }
            Level.Add(new SoundSource(position.x, position.y, 600, "SFX/Devices/AirjabBlow.wav", "J"));
            DuckNetwork.SendToEveryone(new NMSoundSource(position, 600, "SFX/Devices/AirjabBlow.wav", "J"));
            Break();
        }

        public override void OnSolidImpact(MaterialThing with, ImpactedFrom from)
        {
            if (with != null)
            {
                if ((with is Block || with is DeployableShieldAP) && setted == false)
                {
                    Level.Add(new SoundSource(position.x, position.y, 200, "SFX/Devices/AirJabStick.wav", "J"));
                    DuckNetwork.SendToEveryone(new NMSoundSource(position, 200, "SFX/Devices/AirJabStick.wav", "J"));

                    grounded = true;
                    _hSpeed = 0f;
                    _vSpeed = 0f;
                    gravMultiplier = 0f;
                    setted = true;
                    frame = 1;

                    if (from == ImpactedFrom.Right)
                    {
                        angleDegrees = 0;
                    }
                    if (from == ImpactedFrom.Left)
                    {
                        angleDegrees = 180;
                    }
                    if (from == ImpactedFrom.Top)
                    {
                        angleDegrees = 270;
                    }
                    if (from == ImpactedFrom.Bottom)
                    {
                        angleDegrees = 90;
                    }
                }
            }
            base.OnSolidImpact(with, from);
        }

        public override void Draw()
        {
            if (setted && setTime > 0)
            {
                Graphics.DrawCircle(position, radius, Color.White * setTime, 1f);
                Graphics.DrawCircle(position, radius * setTime, Color.White * setTime, 1f);

                setTime -= 0.01666666f;
            }
            base.Draw();
        }
    }
}