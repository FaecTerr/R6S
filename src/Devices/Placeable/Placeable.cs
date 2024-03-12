using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class Placeable : Device
    {
        public Vec2 CheckRect = new Vec2(0f, 0f);
        public bool isVertical;
        public Vec2 _wallPoint = default(Vec2);
        public Vec2 _grappleTravel = default(Vec2);
        public int _lagFrames;
        public Vec2 _lastHit = Vec2.Zero;

        public Device afterPlace;

        public bool sticky;
        public bool doorPlacement;
        public bool secondary;

        public Placeable(float xpos, float ypos) : base(xpos, ypos)
        {
            thickness = 0f;
            depth = -0.5f;
            placeable = true;
            canPlace = true;

            ShowCounter = true;
        }

        public virtual void SetAfterPlace()
        {

        }

        public override void Set()
        {
            SetAfterPlace();
            if (afterPlace != null)
            {
                afterPlace.offDir = offDir;
                afterPlace.position = position;
                afterPlace.setted = true;
                afterPlace.team = oper.team;

                afterPlace.oper = oper;

                if (doorPlacement)
                {
                    afterPlace.enablePhysics = false;
                    afterPlace.hSpeed = 0;
                    afterPlace.vSpeed = 0;
                }

                afterPlace.canPick = true;
                afterPlace.mainDevice = this;

                Level.Add(afterPlace);
            }
            base.Set();
            if (oper != null)
            {
                oper.BackToWeapon(unableToTake);

                (oper.inventory[1] as GunDev).shotFrames = 0.35f;
            }
        }

        public virtual void SetOnPlace(Vec2 dir)
        {
            SetAfterPlace();
            if (afterPlace != null)
            {
                afterPlace.mainDevice = this;
                afterPlace.position = position;
                UsageCount--;
                afterPlace.setted = true;
                afterPlace.team = oper.team;
                afterPlace.hSpeed = 50*dir.x;
                afterPlace.vSpeed = 50*dir.y;
                afterPlace.offDir = offDir;
                afterPlace.canPick = true;
                afterPlace.oper = oper;

                Level.Add(afterPlace);
            }
            if (oper != null)
            {
                oper.BackToWeapon(unableToTake);

                (oper.inventory[1] as GunDev).shotFrames = 0.1f;
            }
        }

        public override void Update()
        {
            if (oper != null && own != null && input != null)
            {
                if(UsageCount <= 0)
                {
                    oper.BackToWeapon(unableToTake);

                    (oper.inventory[1] as GunDev).shotFrames = 0.1f;
                }
                text = "hold LMB to place device";

                Block b = Level.CheckLine<Block>(position - CheckRect, position + CheckRect);
                if (doorPlacement)
                {
                    DoorFrame d = Level.CheckRect<DoorFrame>(topLeft, bottomRight);
                    DoorFrame cd = Level.CheckRect<DoorFrame>(oper.topLeft, oper.bottomRight);
                    if (d != null && cd == null)
                    {
                        if (Level.CheckRect<Block>(d.topLeft + new Vec2(0, 8f), d.bottomRight + new Vec2(0, -4f)) == null)
                        {
                            if (setted == false)
                            {
                                canPlace = true;
                            }
                            else
                            {
                                alpha = 0.8f;
                            }

                            if (oper.local)
                            {
                                if (oper.controller && oper.genericController != null)
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

                                    if (possibleToUse)
                                    {
                                        if (canPlace)
                                        {
                                            if (oper.duckOwner.inputProfile.genericController.MapDown(4194304, false))
                                            {
                                                DoorPlacement(d);
                                            }
                                            else
                                            {
                                                Cancel();
                                            }
                                        }
                                    }
                                }
                                else
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

                                    if (possibleToUse)
                                    {
                                        if (canPlace)
                                        {
                                            if (Keyboard.Down(PlayerStats.keyBindings[13]) || Keyboard.Down(PlayerStats.keyBindingsAlternate[13]))
                                            {
                                                DoorPlacement(d);
                                            }
                                            if(Keyboard.Released(PlayerStats.keyBindings[13]) || Keyboard.Released(PlayerStats.keyBindingsAlternate[13]))
                                            {
                                                Cancel();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (setted == false)
                        {
                            Cancel();
                        }
                    }
                    else if (setted == false)
                    {
                        Cancel();
                    }
                }
                else if (sticky)
                {
                    if (UsageCount > 0)
                    {
                        float dir = 0f;
                        if (offDir > 0)
                            dir = oper.holdAngle;
                        else
                            dir = (float)(Math.PI) + oper.holdAngle;

                        
                        Vec2 direction = new Vec2(new Vec2(16f * (float)Math.Cos(dir), 16f * (float)Math.Sin(dir)));
                        Block bl = Level.CheckLine<Block>(oper.position, oper.position + direction);

                        if (bl != null)
                        {
                            if (oper.local)
                            {
                                if (oper.controller && oper.genericController != null)
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

                                    if (oper.duckOwner.inputProfile.genericController.MapDown(4194304, false) && possibleToUse)
                                    {
                                        setting += 0.01666666f;
                                        oper.hSpeed = 0;
                                        oper.unableToMove = 10;
                                        oper.unableToJump = 10;
                                        opened = true;
                                        if (setting > setTime)
                                        {
                                            setting = 0;
                                            Set();
                                            ATTracer tracer = new ATTracer();
                                            tracer.range = 22f;
                                            float a = angleDegrees;
                                            Vec2 pos = this.Offset(default(Vec2));
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


                                            SetOnPlace(direction);
                                            Vec2 hitPos = new Vec2(bul.end.x, bul.end.y);
                                            //SetAfterPlace();
                                            /*if (afterPlace != null)
                                            {
                                                afterPlace.position = bul.end;
                                                UsageCount--;
                                                afterPlace.setted = true;
                                                afterPlace.team = oper.team;
                                                afterPlace.offDir = offDir;
                                                Level.Add(afterPlace);
                                            }*/
                                        }
                                    }
                                    else
                                    {
                                        Cancel();
                                    }
                                }
                                else
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

                                    if (possibleToUse)
                                    {
                                        if (Keyboard.Down(PlayerStats.keyBindings[13]) || Keyboard.Down(PlayerStats.keyBindingsAlternate[13]))
                                        {
                                            if(setting == 0)
                                            {
                                                Level.Add(new SoundSource(position.x, position.y, 240, placeSound, "J"));
                                                DuckNetwork.SendToEveryone(new NMSoundSource(position, 240, placeSound, "J"));
                                            }
                                            setting += 0.01666666f;
                                            oper.hSpeed = 0;
                                            oper.unableToMove = 10;
                                            oper.unableToJump = 10;
                                            opened = true;
                                            if (setting > setTime)
                                            {
                                                setting = 0;
                                                Set();
                                                ATTracer tracer = new ATTracer();
                                                tracer.range = 22f;
                                                float a = angleDegrees;
                                                Vec2 pos = this.Offset(default(Vec2));
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


                                                SetOnPlace(direction);
                                                Vec2 hitPos = new Vec2(bul.end.x, bul.end.y);
                                                //SetAfterPlace();
                                                /*if (afterPlace != null)
                                                {
                                                    afterPlace.position = bul.end;
                                                    UsageCount--;
                                                    afterPlace.setted = true;
                                                    afterPlace.team = oper.team;
                                                    afterPlace.offDir = offDir;
                                                    Level.Add(afterPlace);
                                                }*/
                                            }
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
                else
                {
                    //canPlace = (Level.CheckRect<Block>(position - CheckRect, position + CheckRect) == null);
                    if (UsageCount > 0 && oper.local && placeable)
                    {
                        if (oper.controller && oper.genericController != null)
                        {
                            if (oper.duckOwner.inputProfile.genericController.MapDown(4194304, false) && canPlace == true && oper.holdObject == this && oper.grounded && b == null)
                            {
                                Deploying();
                            }
                            else if (oper.duckOwner.inputProfile.genericController.MapDown(4194304, false) && canPlace == true && oper.holdObject == this && oper.grounded && wallSet && b != null)
                            {
                                Deploying();
                            }
                            else
                            {
                                Cancel();
                            }


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
                            if (!possibleToUse)
                            {
                                setting = 0f;
                                opened = false;
                            }

                            if (oper.duckOwner.inputProfile.genericController.MapDown(4194304, false) && wallSet == false && oper.grounded && b != null)
                            {
                                text = "not enough place to deploy";
                            }
                        }
                        else
                        {

                            if ((input.Down("SHOOT") || (Mouse.left == InputState.Down && own.profile.localPlayer) || (Keyboard.Down(Keys.F) && own.profile.localPlayer && this is Defuser)) && canPlace == true && oper.holdObject == this && oper.grounded && b == null)
                            {
                                Deploying();
                            }
                            else if ((input.Down("SHOOT") || (Mouse.left == InputState.Down && own.profile.localPlayer) || (Keyboard.Down(Keys.F) && own.profile.localPlayer && this is Defuser)) && canPlace == true && oper.holdObject == this && oper.grounded && wallSet && b != null)
                            {
                                Deploying();
                            }
                            else
                            {
                                Cancel();
                            }

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
                            if (!possibleToUse)
                            {
                                setting = 0f;
                                opened = false;
                            }

                            if ((input.Down("SHOOT") || (Mouse.left == InputState.Down && own.profile.localPlayer)) && wallSet == false && oper.grounded && b != null)
                            {
                                text = "not enough place to deploy";
                            }
                        }
                    }
                    if (setting > setTime)
                    {
                        UsageCount--;
                        Set();
                        setting = 0;


                        /*if (Mouse.positionScreen.x > afterPlace.topLeft.x && Mouse.positionScreen.x < afterPlace.bottomRight.x &&
                            Mouse.positionScreen.y > afterPlace.topLeft.y && Mouse.positionScreen.y < afterPlace.bottomRight.y)
                        {
                            Mouse.position += new Vec2(0, -2f);
                        }*/
                    }
                }
            }
            base.Update();
        }

        public virtual void DoorPlacement(DoorFrame d)
        {
            if (d != null)
            {
                if (setting == 0)
                {
                    Level.Add(new SoundSource(position.x, position.y, 240, placeSound, "J"));
                    DuckNetwork.SendToEveryone(new NMSoundSource(position, 240, placeSound, "J"));
                }
                setting += 0.01666666f;

                oper.unableToMove = 10;
                oper.unableToJump = 10;

                if (setting > setTime)
                {
                    setted = true;
                    setting = 0;
                    UsageCount--;
                    Set();
                    if (position.y > d.position.y + 8f)
                    {
                        position.y = d.position.y + 8f;
                    }
                    if (position.y < d.position.y - 8f)
                    {
                        position.y = d.position.y - 8f;
                    }
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
            oper.hSpeed *= 0.3f;
            oper.unableToMove = 10;
            oper.unableToJump = 10;
            opened = true;
            setting += 0.01666666f;
        }
        public virtual void Cancel()
        {
            if (setting > 0)
            {
                foreach (SoundSource s in Level.CheckRectAll<SoundSource>(topLeft, bottomRight))
                {
                    if (s.sound == placeSound)
                    {
                        s.Cancel();
                    }
                }
            }
            setting = 0f;
            opened = false;

        }
    }
}
