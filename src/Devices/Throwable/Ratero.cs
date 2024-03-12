using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class RateroDrone : Drone
    {
        public RateroDrone(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/Ratero.png"), 24, 8);
            graphic = _sprite;
            center = new Vec2(12f, 4f);
            collisionOffset = new Vec2(-11f, -3f);
            collisionSize = new Vec2(22f, 6f);
            depth = -0.5f;
            weight = 0.9f;
            physicsMaterial = PhysicsMaterial.Metal;
            thickness = 0.4f;
            team = "Att";
            _enablePhysics = true;

            sticky = false;

            UsageCount = 4;
            weightR = 4;

            DeviceCost = 20;
            descriptionPoints = "Shock drone active";

            closeDeployment = false;

            index = 55;
            minimalTimeOfHolding = 0.25f;
            needsToBeGentle = false;
        }

        public override void SetRocky()
        {
            rock = new RateroDroneAP(position.x, position.y);
            if (oper != null)
            {
                rock.oper = oper;
            }
            //base.SetRocky();
        }
        public override void Throw()
        {
            base.Throw();
            if(oper != null)
            {
                BackToWeapon = false;
                oper.ChangeWeapon(30, 5);
            }
        }
    }

    public class RateroDroneAP : ObservationThing
    {
        public float runTime = 10;
        public bool initiate;
        public float detonation = 3f;

        public RateroDroneAP(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/Ratero.png"), 24, 8);
            graphic = _sprite;
            center = new Vec2(12f, 4f);
            collisionOffset = new Vec2(-7f, -3f);
            collisionSize = new Vec2(14f, 6f);
            depth = -0.5f;
            weight = 0.9f;
            physicsMaterial = PhysicsMaterial.Metal;

            bulletproof = false;
            breakable = true;

            UsageCount = 1;

            health = 3;

            team = "Att";
            _enablePhysics = true;
            controllable = true;

            _editorName = "OBS Ratero";

            sticky = false;
            destructingByElectricity = true;

            DeviceCost = 20;
            destroyedPoints = "RCE Ratero destroyed";

            canPick = true;
        }

        public override void Update()
        {
            base.Update();
            health = 3;
            if (!initiate)
            {
                runTime -= 0.01666666f; 
                if (runTime <= 0)
                {
                    Fire();
                }
            }
            else
            {
                detonation -= 0.01666666f;
                if (detonation <= 0)
                {
                    Explode();
                }
            }

            if (jammed)
            {
                jump = false; moveLeft = false; moveRight = false;
                initiate = true;
                bulletproof = true;
                sticky = true;
                runTime = 0;
            }

            hSpeed = offDir * 2.75f;
            if (oper != null)
            {
                if (oper.observing)
                {
                    foreach (Holdable h in Level.CheckCircleAll<Holdable>(position, 32))
                    {
                        h.alpha = 1;
                    }
                    foreach (Jammer j in Level.CheckCircleAll<Jammer>(position, 96))
                    {
                        if (j.setted)
                        {
                            jammed = true;
                        }
                    }
                    foreach (Door d in Level.CheckLineAll<Door>(position + new Vec2(-16f, 0f), position + new Vec2(16f, 0f)))
                    {
                        d._openForce = 2f;
                        d._open = 2;
                    }

                    if (jammed)
                    {
                        jump = false; moveLeft = false; moveRight = false;
                        initiate = true;
                        bulletproof = true;
                        sticky = true;
                        runTime = 0;
                    }
                    else
                    {
                        if (moveRight == true && moveLeft == false)
                        {
                            offDir = 1;
                        }
                        if (moveRight == false && moveLeft == true)
                        {
                            offDir = -1;
                        }
                        if (!initiate)
                        {
                            if (jump == true && grounded)
                            {
                                vSpeed = -5f;
                            }
                            if (fire && UsageCount > 0)
                            {
                                Fire();
                            }
                        }
                    }
                    angleDegrees = hSpeed;
                }
            }
        }

        public virtual void Fire()
        {
            if (oper != null)
            {
                initiate = true;
                bulletproof = true;
                runTime = 0;
                sticky = true;
            }
        }
        public void Explode()
        {
            Level.Add(new Explosion(position.x, position.y, 102, 165, "N") { shootedBy = oper });
            Level.Add(new Explosion(position.x, position.y, 28, 65, "E") { shootedBy = oper });
            Level.Add(new Explosion(position.x, position.y, 56, 105, "S") { shootedBy = oper });

            DuckNetwork.SendToEveryone(new NMExplosion(position, 102, 165, "N", oper));
            DuckNetwork.SendToEveryone(new NMExplosion(position, 28, 65, "E", oper));
            DuckNetwork.SendToEveryone(new NMExplosion(position, 56, 105, "S", oper));

            Level.Add(new SoundSource(position.x, position.y, 320, "SFX/explo/big_bomb.wav", "J"));
            DuckNetwork.SendToEveryone(new NMSoundSource(position, 320, "SFX/explo/big_bomb.wav", "J"));

            Level.Remove(this);
        }
        public override void OnDrawLayer(Layer layer)
        {
            if (layer == Layer.Foreground)
            {
                if (oper != null)
                {
                    if (oper.local && oper.observing && observing)
                    {
                        string text = Math.Round(detonation, 2) + " / " + Convert.ToString(3);
                        string name = "Blast: ";
                        Color c = Color.White;

                        name += Math.Round(runTime, 2) + " / " + "10";
                        c = Color.OrangeRed;
                        
                        /*if (game == null)
                        {
                            BitmapFont _font = new BitmapFont("smallFont", 8, -1);
                        }*/

                        Graphics.DrawStringOutline(name, Level.current.camera.position + Level.current.camera.size - new Vec2(oper._aim.scale.x * 80, oper._aim.scale.y * 15), c, Color.Black, 1f, null, Level.current.camera.height / 320);
                        Graphics.DrawStringOutline(text, Level.current.camera.position + Level.current.camera.size - new Vec2(oper._aim.scale.x * 80, oper._aim.scale.y * 10), Color.White, Color.Black, 1f, null, Level.current.camera.height / 320);
                    }
                }
            }
        }
        public override void Draw()
        {
            base.Draw();
        }
    }
}