using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DuckGame;

namespace DuckGame.R6S
{
    public class Drone : Throwable
    {
        public Drone(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/Drone.png"), 16, 16);
            graphic = _sprite;
            center = new Vec2(8f, 13f);
            collisionOffset = new Vec2(-8f, -3f);
            collisionSize = new Vec2(16f, 6f);
            depth = -0.5f;

            team = "Att";

            sticky = false;
            canPick = true;
            isSecondary = true;
            index = 7;

            UsageCount = 2;
            weightR = 7;

            DeviceCost = 10;
            descriptionPoints = "Drone active"; 
            minimalTimeOfHolding = 0.25f;
            needsToBeGentle = false;
        }

        public override void SetRocky()
        {
            rock = new DroneAP(position.x, position.y);
            if (oper != null)
            {
                rock.oper = oper;
            }
            base.SetRocky();
        }
    }

    public class DroneAP : ObservationThing
    {
        public bool inAir;

        public DroneAP(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/Drone.png"), 16, 16);
            graphic = _sprite;
            center = new Vec2(8f, 13f);
            collisionOffset = new Vec2(-8f, -2f);
            collisionSize = new Vec2(16f, 4f);

            team = "Att";

            _enablePhysics = true;
            controllable = true;

            UpdatePhones();
            Set();

            cameraOverlay = 2;

            breakable = true;
            destructingByElectricity = true;

            canPick = true;
            setted = true;
            sticky = false;

            DeviceCost = 10;
            descriptionPoints = "Drone active";
            destroyedPoints = "Drone destroyed";

            _editorName = "OBS drone";

            doLight = true;
            lightColor = Color.OrangeRed;
            sizeOfLight = 0.3f;
        }



        public override void Update()
        {
            base.Update();
            foreach (Door d in Level.CheckLineAll<Door>(position + new Vec2(-16f, 0f), position + new Vec2(16f, 0f)))
            {
                d._openForce = 2f;
                d._open = 2;
            }
            if (jammed || jammedFrames > 0)
            {
                jump = false;
                moveLeft = false;
                moveRight = false;
            }
            _enablePhysics = true;
            if (moveRight == true && moveLeft == false)
            {
                hSpeed += 0.5f;
            }
            if (moveRight == false && moveLeft == true)
            {
                hSpeed -= 0.5f;
            }
            if (Math.Abs(hSpeed) > 2.75f)
            {
                if (hSpeed > 0)
                {
                    hSpeed = 2.75f;
                }
                else
                {
                    hSpeed = -2.75f;
                }
            }
            
            if (moveRight == false && moveLeft == false)
            {
                float mod = 1f;
                if (grounded)
                {
                    mod = 0.8f;
                }
                hSpeed *= mod;
            }
            if (jump == true && grounded)
            {
                if (Math.Abs(hSpeed) < 0.5f)
                {
                    vSpeed = -5f;
                }
                else
                {
                    vSpeed = -3.85f;
                }

                Level.Add(new SoundSource(position.x, position.y, 160, "SFX/Devices/DroneJump.wav", "J"));
                DuckNetwork.SendToEveryone(new NMSoundSource(position, 160, "SFX/Devices/DroneJump.wav", "J"));
            }

            if (!grounded)
            {
                inAir = true;
            }
            else
            {
                if (inAir)
                {
                    Level.Add(new SoundSource(position.x, position.y, 160, "SFX/Devices/DroneLand.wav", "J"));
                    DuckNetwork.SendToEveryone(new NMSoundSource(position, 160, "SFX/Devices/DroneLand.wav", "J"));
                    inAir = false;
                }

            }

            angleDegrees = hSpeed*2;
        }
    }
}
