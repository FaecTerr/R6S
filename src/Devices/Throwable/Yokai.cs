using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DuckGame;

namespace DuckGame.R6S
{
    public class Yokai : Drone
    {
        public SpriteMap _circle;

        //public List<Duck> ducks;
        public Yokai(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/Yokai.png"), 16, 16);
            graphic = _sprite;
            center = new Vec2(8f, 13f);
            collisionOffset = new Vec2(-8f, -3f);
            collisionSize = new Vec2(16f, 6f);
            depth = -0.5f;
            weight = 0.9f;
            thickness = 0.1f;
            physicsMaterial = PhysicsMaterial.Metal;

            team = "Def";
            _enablePhysics = true;
            weightR = 4;

            sticky = false;

            DeviceCost = 20;
            descriptionPoints = "Yokai drone active";

            UsageCount = 2;
            index = 22; 
            minimalTimeOfHolding = 0.25f;
            needsToBeGentle = false;
        }

        public override void SetRocky()
        {
            rock = new YokaiAP(position.x, position.y);
            if (oper != null)
            {
                rock.oper = oper;
            }
            //base.SetRocky();
        }
    }

    public class YokaiAP : ObservationThing
    {
        public SpriteMap _circle;

        public float reloadTime = 15;
        public float reload;

        public int makeSound;

        public YokaiAP(float xpos, float ypos) : base(xpos, ypos)
        {
            _circle = new SpriteMap(GetPath("Sprites/Devices/RadiusCircle.png"), 32, 32);
            _circle.CenterOrigin();
            _circle.scale = new Vec2(3f, 3f);

            _sprite = new SpriteMap(GetPath("Sprites/Devices/Yokai.png"), 16, 16);
            graphic = _sprite;
            center = new Vec2(8f, 13f);
            collisionOffset = new Vec2(-8f, -3f);
            collisionSize = new Vec2(16f, 6f);
            depth = -0.5f;
            weight = 0.9f;
            thickness = 0.1f;
            physicsMaterial = PhysicsMaterial.Metal;

            team = "Def";
            _enablePhysics = true;
            
            bulletproof = false;
            breakable = true;
            
            controllable = true;

            _editorName = "OBS yokai";

            scannable = true;
            destructingByElectricity = true;
            destroyedPoints = "Shock drone destroyed";
            canPick = true;
            sticky = false;

            DeviceCost = 20;
            descriptionPoints = "Yokai drone active";

            UsageCount = 2;

            observableOutside = false;
            lightColor = Color.MediumPurple;
        }


        public override void OnSolidImpact(MaterialThing with, ImpactedFrom from)
        {
            base.OnSolidImpact(with, from);

            if (with != null && setted == false)
            {
                if (with is Block)
                {
                    if (from == ImpactedFrom.Top)
                    {
                        _enablePhysics = false;
                        grounded = true;
                        _hSpeed = 0f;
                        _vSpeed = 0f;
                        
                        setted = true;
                    }
                }
            }
        }
        public override void Update()
        {
            if (UsageCount < 2)
            {
                reload -= 0.01666666f;
                if (reload <= 0f)
                {
                    UsageCount++;
                    reload = reloadTime;
                }
            }

            if (makeSound <= 0)
            {
                Level.Add(new SoundSource(position.x, position.y, 48, "SFX/Devices/EchoDroneIdle.wav", "J"));
                makeSound = 25;
            }
            else
            {
                makeSound--;
            }
            //DuckNetwork.SendToEveryone(new NMSoundSource(position, 48, "SFX/Devices/EchoDroneIdle.wav", "J"));

            if (oper != null)
            {
                if (oper.observing)
                {
                    if (Mouse.positionScreen.x > position.x)
                    {
                        offDir = 1;
                    }
                    if (Mouse.positionScreen.x < position.x)
                    {
                        offDir = -1;
                    }
                    foreach (Holdable h in Level.CheckCircleAll<Holdable>(position, 32))
                    {
                        h.alpha = 1;
                    }
                    if (UsageCount < 3)
                    {
                        if (reload > 0)
                        {
                            reload -= 0.01666666f;
                        }
                        else
                        {
                            UsageCount++;
                            reload = 10f;
                        }
                    }

                    if (oper != null)
                    {
                        if (oper.observing && UsageCount > 0 && observing)
                        {
                            oper._aim.frame = 35;
                            
                        }
                    }
                   
                    if (jammed)
                    {
                        jump = false; moveLeft = false; moveRight = false;
                    }
                    else
                    {
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
                            hSpeed *= 0.8f;
                        }
                        if (jump == true && grounded && enablePhysics)
                        {
                            vSpeed = -7f;
                        }
                        if(jump && !enablePhysics)
                        {
                            enablePhysics = true;
                            setted = false;
                        }
                        if (fire && UsageCount > 0 && !_enablePhysics)
                        {
                            Fire();
                            fire = !fire;
                        }
                    }
                    angleDegrees = hSpeed;
                }
            }

            base.Update();
        }


        public virtual void Fire()
        {
            UsageCount--;
            if (oper != null)
            {
                if (oper.controller && oper.genericController != null)
                {
                    Vec2 pos = position + oper.duckOwner.inputProfile.rightStick * 128f * new Vec2(1, -1);

                    DuckNetwork.SendToEveryone(new NMSonicBurst(pos));
                    SonicBurst(pos);
                }
                else
                {
                    Vec2 pos = position + Mouse.position - Level.current.camera.size * new Vec2(0.5f, 0.5f);

                    DuckNetwork.SendToEveryone(new NMSonicBurst(pos));
                    SonicBurst(pos);
                }
            }
        }

        public virtual void SonicBurst(Vec2 pos)
        {
            foreach (Operators op in Level.CheckCircleAll<Operators>(pos, 60))
            {
                if (op.holdObject is Phone && op.GetPhone().ConnectedCameras() > (op.inventory[5] as Phone).camIndex)
                {
                    op.GetPhone().GetCurrentObservable().Disconnect();
                    op.immobilized = false;
                }

                op.unableToSprint = 60;
                op.BackToWeapon(60);
                op.concussionFrames = 600;
                op.deafenFrames = 660;
            }
            Level.Add(new SoundSource(position.x, position.y, 320, "SFX/Devices/EchoDroneShot.wav", "J"));
            DuckNetwork.SendToEveryone(new NMSoundSource(position, 320, "SFX/Devices/EchoDroneShot.wav", "J"));
        }

        public override void Draw()
        {
            if (observing && oper != null)
            {
                if (oper.local)
                {
                    Vec2 pos = position + Mouse.position - Level.current.camera.size * new Vec2(0.5f, 0.5f);

                    if (oper.controller && oper.duckOwner != null)
                    {
                        pos = position + oper.duckOwner.inputProfile.rightStick * 128f * new Vec2(1, -1);

                    }

                    Graphics.DrawCircle(pos, (reload / reloadTime) * 60, Color.White, 2f, 1f, 32);
                    if (!enablePhysics)
                    {
                        Graphics.Draw(_circle, pos.x, pos.y, 1f);
                    }

                    if (DevConsole.showCollision)
                    {
                        Graphics.DrawCircle(pos, 60, Color.Gray, 1f, 1f, 32);
                    }
                }
            }
            base.Draw();
        }
    }

}