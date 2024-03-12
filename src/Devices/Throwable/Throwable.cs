using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class Throwable : Device
    {
        public bool sticky;
        public int setFrames;
        public int setFramesLoad;

        public Rocky r;

        public Rocky rock;
        public float reload;
        public bool inPlace;
        public bool doubleActivation;
        public bool closeDeployment;
        public float weightR = 5;

        public float Timer;
        public bool isGrenade;

        public float throwingTime;

        public float delay;
        public float activationDelay;

        private float activationTimer;

        public bool drawTraectory;
        public bool useCustomHitMarker;

        public float minimalTimeOfHolding = 0;
        public float TimeOfHolding;
        public bool BackToWeapon;

        private Rocky _hover;

        public Throwable(float xpos, float ypos) : base(xpos, ypos)
        {
            thickness = 0f;
            depth = -0.5f;
            _canFlip = false;
            _canRaise = false;
            UsageCount = 5;

            ShowCounter = true;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update()
        {
            base.Update();

            if (reload > 0)
            {
                reload -= 0.01666666f;
            }

            activationTimer = activationDelay;

            if(user != null)
            {
                if(user.holdObject != this)
                {
                    BackToWeapon = false;
                    TimeOfHolding = 0;
                }
                else
                {
                    TimeOfHolding += 0.01666666f;
                    if(BackToWeapon && TimeOfHolding > minimalTimeOfHolding)
                    {
                        user.BackToWeapon(unableToTake);
                    }
                }
                if (UsageCount <= 0 && (!doubleActivation || (doubleActivation && !inPlace)))
                {
                    if (user.holdObject == this)
                    {
                        user.lockedTakeOut[takenSlot] = 20;
                    }
                }
            }
            if (oper != null)
            {
                if(rock == null)
                {
                    SetRocky();
                }


                if(user == null)
                {
                    user = oper;
                }
                if (oper.holdObject == this && doubleActivation && inPlace && reload <= 0f && !BackToWeapon)
                {
                    if(activationTimer > 0)
                    {
                        activationTimer -= 0.01666666f;
                    }
                    if (activationTimer <= 0)
                    {
                        Detonate();
                        DuckNetwork.SendToEveryone(new NMThrowable(this, "Detonate", oper.team));
                    }
                }


                if (oper.local && oper.inventory.Count > 6)
                {
                    if (input != null)
                    {
                        if ((oper.controller && oper.genericController != null) || !oper.controller)
                        {
                            if (oper.controller)
                            {
                                if (reload <= 0f && (oper.duckOwner.inputProfile.genericController.MapReleased(4194304) || (oper.duckOwner.inputProfile.genericController.MapDown(4194304, false) && closeDeployment)))
                                {
                                    Prepare();
                                }
                                else
                                {
                                    Cancel();
                                }
                            }
                            else
                            {
                                if (!closeDeployment)
                                {
                                    if (doubleActivation && inPlace && !BackToWeapon)
                                    {
                                        if (reload <= 0f)
                                        {
                                            Prepare();
                                        }
                                        else
                                        {
                                            Cancel();
                                        }
                                    }
                                    else
                                    {
                                        if (reload <= 0f && (Keyboard.Released(PlayerStats.keyBindings[takenSlot + 6]) ||
                                            Keyboard.Released(PlayerStats.keyBindingsAlternate[takenSlot + 6])))
                                        {
                                            Prepare();
                                        }
                                        else
                                        {
                                            Cancel();
                                        }
                                    }
                                }
                                else
                                {
                                    if ((Keyboard.Down(PlayerStats.keyBindings[13]) || Keyboard.Down(PlayerStats.keyBindingsAlternate[13])) && reload <= 0)
                                    {
                                        Prepare();
                                    }
                                    else
                                    {
                                        Cancel();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public virtual void Prepare()
        {
            if (r != null && inPlace && doubleActivation && !BackToWeapon)
            {
                Detonate();
                DuckNetwork.SendToEveryone(new NMThrowable(this, "Detonate", oper.team));
                inPlace = false;
                return;
            }
            if ((doubleActivation && !inPlace) || !doubleActivation)
            {
                bool possibleToUse = true;
                if (oper.mode == "normal" && cantStand)
                {
                    possibleToUse = false;
                }
                if (oper.mode == "crouch" && cantCrouch)
                {
                    possibleToUse = false;
                }
                if (oper.mode == "slide" && cantProne)
                {
                    possibleToUse = false;
                }
                if (closeDeployment)
                {
                    if (possibleToUse)
                    {
                        ATTracer tracer = new ATTracer();
                        tracer.range = 18f;
                        float a = angleDegrees;
                        Vec2 pos = Offset(default(Vec2));
                        tracer.penetration = 2f;
                        if (offDir < 0)
                        {
                            a = 180 - angleDegrees;
                        }
                        if (offDir > 0)
                        {
                            a = 360 - angleDegrees;
                        }

                        Bullet bul = new Bullet(pos.x, pos.y, tracer, a, owner, false, -1f, true, true);
                        Vec2 hitPos = new Vec2(bul.end.x, bul.end.y);

                        if (Level.CheckLine<Block>(position, bul.end) != null || Level.CheckLine<DeployableShieldAP>(position, bul.end) != null)
                        {
                            if (setting >= setTime)
                            {
                                Throw();
                            }
                            else
                            {
                                Deploying();
                            }
                        }
                        else
                        {
                            Cancel();
                        }
                    }
                }
                else
                {
                    //DevConsole.Log("Throwing rock", Color.White);
                    Throw();
                    return;
                }
            }
        }

        public virtual void Deploying()
        {
            if (setting == 0 && placeSound != "")
            {
                Level.Add(new SoundSource(position.x, position.y, 240, placeSound, "J"));
                DuckNetwork.SendToEveryone(new NMSoundSource(position, 240, placeSound, "J"));
            }
            setting += 0.01666666f;

            oper.unableToJump = 10;
            oper.unableToMove = 10;
        }

        public virtual void Cancel()
        {
            if (setting > 0)
            {
                foreach (SoundSource s in Level.CheckRectAll<SoundSource>(topLeft, bottomRight))
                {
                    s.Cancel();
                }
            }
            setting = 0;
        }

        public virtual void SetRocky()
        {
            //DevConsole.Log("ROCK CREATED", Color.White);
        }

        public virtual void Detonate()
        {
            if (r != null)
            {
                if (doubleActivation && !BackToWeapon)
                {
                    if (!r.jammed)
                    {
                        UsageCount--;
                        r.Detonate(delay);
                        r = null;
                        reload = 0.5f;
                        inPlace = false;

                        Level.Add(new SoundSource(position.x, position.y, 80, "SFX/Devices/ActivateDADevice.wav", "J"));
                        DuckNetwork.SendToEveryone(new NMSoundSource(position, 80, "SFX/Devices/ActivateDADevice.wav", "J"));
                    }
                }
                else
                {
                    r.Detonate(delay + (minimalTimeOfHolding > TimeOfHolding ? minimalTimeOfHolding - TimeOfHolding : 0));
                    r = null;
                    reload = 0.5f;
                    inPlace = false;
                }
            }
            if (oper != null)
            {
                BackToWeapon = true;
                //oper.BackToWeapon(unableToTake);
                //(oper.inventory[1] as GunDev).shotFrames = 0.25f;
            }
        }

        public virtual void Throw()
        {
            if (UsageCount > 0 && oper != null)
            {
                SetRocky();
                Set();
                if (!closeDeployment)
                {
                    oper.handAnimation = "Throw";
                    oper.handAnimFrames = 20;
                }

                float dir = 0;
                if (offDir > 0)
                    dir = oper.holdAngle;
                else
                    dir = (float)(Math.PI) + oper.holdAngle;

                if (rock != null)
                {
                    //DevConsole.Log("ROCK ALIVE", Color.White);
                    inPlace = true;
                    if (!doubleActivation)
                    {
                        UsageCount--;
                    }
                    r = rock;
                    r.oper = oper;
                    r.mainDevice = this;
                    r.vSpeed = weightR * (float)Math.Sin(dir);
                    r.hSpeed = weightR * (float)Math.Cos(dir) + oper.hSpeed / 2;
                    r.offDir = offDir;
                    r.angle = dir;
                    r.setFrames = setFramesLoad;
                    r.setFramesLoad = setFramesLoad;
                    r.team = team;
                    r.canPick = true;


                    if (mainDevice == null && oper.local && placings > 0)
                    {
                        placings--;
                        PlayerStats.renown += DeviceCost;
                        PlayerStats.Save();
                        Level.Add(new RenownGained() { description = descriptionPoints, amount = DeviceCost });
                    }


                    //r.sticky = sticky;
                    if (isGrenade)
                    {
                        r.time = Timer;
                        r.detonate = true;
                    }
                    reload = 0.5f;
                    if(oper.mode == "crouch")
                    {
                        r.position.y -= 3;
                    }
                    if(oper.mode == "slide")
                    {
                        r.position.y -= 6;
                    }
                    Level.Add(r);
                }

                BackToWeapon = true;
            }
            else if (oper != null)
            {
                BackToWeapon = true;
            }
        }

        public override void Draw()
        {
            base.Draw();
            if (oper != null && own != null)
            {
                if (text != "" && own.profile.localPlayer)
                {
                    Graphics.DrawString(text, new Vec2(oper.position.x - text.Length * 2, oper.position.y - 16f), Color.White, 1f, null, 0.5f);
                }
            }

            if (rock != null && drawTraectory && oper != null)
            {
                if (_hover == null && rock != null)
                {
                    _hover = rock as Rocky;
                }

                if (_hover != null && oper.local)
                {
                    float dir = 0;
                    if (offDir > 0)
                        dir = oper.holdAngle;
                    else
                        dir = (float)(Math.PI) + oper.holdAngle;
                    
                    _hover.stickedTo = null;

                    _hover.gravMultiplier = 1;

                    _hover.enablePhysics = true;
                    _hover.vSpeed = weightR * (float)Math.Sin(dir);
                    _hover.hSpeed = weightR * (float)Math.Cos(dir) + oper.hSpeed / 2;
                    _hover.sticky = rock.sticky;

                    _hover.position = position;
                    _hover.thickness = rock.thickness;
                    _hover.weight = rock.weight;
                    //_hover.impactPowerH = 0;
                    _hover.setted = false;
                    _hover.collisionSize = rock.collisionSize;
                    _hover.collisionOffset = rock.collisionOffset;

                    _hover.grounded = false;

                    _hover.frames = 0;

                    if (oper.mode == "crouch")
                    {
                        _hover.position.y -= 3;
                    }
                    if (oper.mode == "slide")
                    {
                        _hover.position.y -= 6;
                    }

                    _hover.bouncy = rock.bouncy;
                    _hover.friction = rock.friction;

                    _hover.Dir = new Vec2(0, 0);

                    SFX.enabled = false;
                    Vec2 lastPos = _hover.position;
                                        
                    for (int i = 0; i < 60; i++)
                    {
                        if (enablePhysics)
                        {
                            _hover.UpdatePhysics();
                        }
                        Graphics.DrawLine(lastPos, _hover.position, Color.White * 0.7f * ((60.0f - i) / 60.0f), 2f, 1f);
                        lastPos = _hover.position;

                        if (sticky)
                        {
                            if (_hover.stickedTo != null || _hover.grounded || _hover.Dir != new Vec2())
                            {                                
                                _hover.enablePhysics = false;
                                _hover.hSpeed = 0;
                                _hover.vSpeed = 0;
                                if (_hover.grounded)
                                {
                                    _hover.Dir = new Vec2(0, 1);
                                }
                            }
                        }
                    }


                    Sprite _fixate = new Sprite(GetPath("Sprites/ThrowingFixated.png"));

                    if (useCustomHitMarker)
                    {
                        if (rock._sprite != null)
                        {
                            _fixate = rock._sprite;
                        }

                        if (_hover.Dir.x == 1)
                        {
                            _fixate.angleDegrees = 0;
                        }
                        if (_hover.Dir.x == -1)
                        {
                            _fixate.angleDegrees = 180;
                        }
                        if (_hover.Dir.y == 1)
                        {
                            _fixate.angleDegrees = 90;
                        }
                        if (_hover.Dir.y == -1)
                        {
                            _fixate.angleDegrees = 270;
                        }

                        if (rock.offDir < 0)
                        {
                            _fixate.angleDegrees -= 180;
                        }
                    }

                    _fixate.alpha = 0.5f;
                    _fixate.CenterOrigin();

                    Graphics.Draw(_fixate, _hover.position.x, _hover.position.y);

                    SFX.enabled = true;
                }                
            }
        }
    }
}
