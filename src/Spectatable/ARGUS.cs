using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Furniture")]
    public class ArgusLauncher : Launchers
    {
        public ArgusLauncher(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/Argus.png"), 22, 14, false); 
            graphic = _sprite;
            center = new Vec2(6f, 6f);
            collisionSize = new Vec2(8f, 8f);
            collisionOffset = new Vec2(-8f, -1f);
            team = "Att";
            _editorName = "A.R.G.U.S.";

            Missiles1 = 4;

            placeable = false;
            breakable = false;
            zeroSpeed = false;
            scannable = false;

            index = 51;
            isSecondary = false;
        }
        public override void SetMissile()
        {
            missile = new ArgusCamera(position.x, position.y);
            base.SetMissile();
        }
    }
    public class ArgusCamera : ObservationThing
    {
        public int degr;
        public bool usedDrill;
        public Vec2 drillPos;
        public Vec2 origPos;
        public bool alternateCamera;
        public float animation;
        public ArgusCamera(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/ArgusCam.png"), 16, 16, false); 
            graphic = _sprite;

            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(4f, 4f);
            collisionOffset = new Vec2(-2f, -2f);
            team = "Att";
            _editorName = "A.R.G.U.S.";

            gravMultiplier = 0.2f;
            weight = 0.9f;
            thickness = 0.1f;

            canPickUp = false;
            zeroSpeed = false;

            enablePhysics = true;

            destructingByElectricity = true;
            catchableByADS = true;

            bulletproof = false;
            destructingByHands = true;

            _editorName = "OBS ARGUS";

            index = 51;
            isSecondary = false;
        }

        public override void Update()
        {
            base.Update();
            if(observing && stickedTo != null)
            {
                if (jump)
                {
                    if (!usedDrill)
                    {
                        if (stickedTo is BreakableSurface)
                        {
                            usedDrill = true;
                            Drill();
                        }
                        else
                        {
                            drillPos = position;
                        }
                    }
                    else
                    {
                        if (degr % 180 == 0)
                        {
                            collisionOffset = collisionSize * -new Vec2(0.5f, 0.5f) + new Vec2(collisionSize.x * 0.5f * (alternateCamera ? 1 : -1));
                        }
                        else
                        {

                            collisionOffset = collisionSize * -new Vec2(0.5f, 0.5f) + new Vec2(0, collisionSize.y * 0.5f * (alternateCamera ? 1 : -1));
                        }
                    }
                }
            }
        }

        public void Drill()
        {
            float radians = Maths.DegToRad(degr);

            int lengths = 3;

            origPos = position;

            while(lengths < 18 && Level.CheckPoint<Block>(position.x + Dir.x * lengths, position.y + Dir.y * lengths) != null)
            {
                lengths++;
                if(lengths >= 18)
                {
                    return;
                }
            }

            drillPos = position + new Vec2(Dir.x * lengths, Dir.y * lengths);

            position += new Vec2(Dir.x * (lengths - 2), Dir.y * (lengths - 2));
            collisionSize = new Vec2(8f + Dir.x * (lengths + 4), 8f + Dir.y * (lengths + 4));
            collisionOffset = collisionSize * - new Vec2(0.5f, 0.5f);
        }

        public override void OnStick()
        {
            base.OnStick();
            drillPos = position;
        }


        public override void OnSolidImpact(MaterialThing with, ImpactedFrom from)
        {
            base.OnSolidImpact(with, from);
            if (with != null)
            {
                if ((with is Block || with is DeployableShieldAP) && setted == false)
                {
                    Level.Add(new SoundSource(position.x, position.y, 400, "SFX/Devices/ASHCharge.wav", "J"));
                    DuckNetwork.SendToEveryone(new NMSoundSource(position, 400, "SFX/Devices/ASHCharge.wav", "J"));

                    _sprite.SetAnimation("breach");
                    _enablePhysics = false;
                    grounded = true;
                    _hSpeed = 0f;
                    _vSpeed = 0f;
                    gravMultiplier = 0f;
                    setted = true;
                    frame = 1;
                    if (from == ImpactedFrom.Right)
                    {
                        angleDegrees = 0;
                        degr = 0;
                    }
                    if (from == ImpactedFrom.Left)
                    {
                        angleDegrees = 180;
                        degr = 180;
                    }
                    if (from == ImpactedFrom.Top)
                    {
                        angleDegrees = 270;
                        degr = 270;
                    }
                    if (from == ImpactedFrom.Bottom)
                    {
                        angleDegrees = 90;
                        degr = 90;
                    }
                }
            }
        }
        public override void Draw()
        {
            base.Draw();
            if (drillPos != position)
            {
                _sprite.angleDegrees += 180;
                Graphics.Draw(_sprite, drillPos.x, drillPos.y);
            }
        }
    }
}
