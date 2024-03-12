using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Devices|Weapon")]
    public class ParasyteLauncher : Launchers
    {
        public ParasyteLauncher(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/ParasyteLauncher.png"), 22, 14, false);
            graphic = _sprite;
            center = new Vec2(11f, 7f);
            collisionSize = new Vec2(20f, 12f);
            collisionOffset = new Vec2(-10f, -6f);
            _canRaise = true;
            weight = 0.9f;
            thickness = 0.1f;
            placeable = false;
            zeroSpeed = false;
            scannable = false;

            index = 34;

            _editorName = "Parasyte Launcher";

            Missiles1 = 3;
            placeSound = "SFX/Devices/PestPlace.wav";
            //pickupSound = "SFX/Devices/NomadClick.wav";
        }

        public override void SetMissile()
        {
            Vec2 pos = new Vec2(position.x + (float)(Math.Cos(angle) * 10 * offDir), position.y + (float)(Math.Sin(angle) * 2));
            missile = new Parasyte(pos.x, pos.y);
            base.SetMissile();
        }
    }

    public class Parasyte : Device
    {
        public float radius = 48;
        //float stunLength = 2.5f;
        //float power = 4;

        int soundFrames;

        public Parasyte(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/Parasyte.png"), 16, 16, false);
            graphic = _sprite;
            center = new Vec2(8f, 8f);
            collisionOffset = new Vec2(-2f, -2f);
            collisionSize = new Vec2(4f, 4f);
            gravMultiplier = 0f;
            weight = 0.9f;
            thickness = 0.1f;

            canPickUp = true;
            canPick = true;
            zeroSpeed = false;

            bulletproof = false;
            destructingByElectricity = true;
            catchableByADS = false;
            breakable = true;

            setTime = 1f;
        }

        public override void Update()
        {
            if (setted)
            {
                canPick = true;
                if (soundFrames <= 0)
                {
                    soundFrames = 180;
                    Level.Add(new SoundSource(position.x, position.y, radius * 2, "SFX/Devices/PestIdle.wav", "J"));
                    DuckNetwork.SendToEveryone(new NMSoundSource(position, radius * 2, "SFX/Devices/PestIdle.wav", "J"));
                }
                else
                {
                    soundFrames--;
                }


                foreach (DroneAP op in Level.CheckCircleAll<DroneAP>(position, radius))
                {
                    if (op.team != "Def" && op.oper != null && op.owner == null && Level.CheckLine<Block>(op.position, position) == null && setTime <= 0)
                    {
                        op.team = team;
                        op.oper = oper;
                        op.own = own;
                        position = op.position;
                        DuckNetwork.SendToEveryone(new NMMozzieSteal(op, oper));
                        op.UpdatePhones();
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
            Break();
        }

        public override void OnSolidImpact(MaterialThing with, ImpactedFrom from)
        {
            if (with != null)
            {
                if ((with is Block || with is DeployableShieldAP) && setted == false)
                {
                    //Level.Add(new SoundSource(position.x, position.y, 200, "SFX/Devices/PestPlace.wav", "J"));
                    //DuckNetwork.SendToEveryone(new NMSoundSource(position, 200, "SFX/Devices/PestPlace.wav", "J"));

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