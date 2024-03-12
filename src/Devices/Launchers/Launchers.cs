using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class Launchers : Device
    {
        public Device missile;
        public Device missile1;

        public float reload;
        public int Missiles1 = 0;
        public int Missiles2 = 0;
        public int mode;
        public int maxMode = 0;

        public float speed = 1;
        public float vlSpeed = 1;
        public float reloadTime;

        public bool doubleActivation;

        public string place2Sound = "";

        public Launchers(float xpos, float ypos) : base(xpos, ypos)
        {
            UsageCount = Missiles1 + Missiles2;

            ShowCounter = true;

            placeSound = "";
            lockSprint = false;
        }

        public override void Update()
        {
            base.Update();
            UsageCount = Missiles1 + Missiles2;
            if(oper != null && UsageCount <= 0 && oper.inventory[1] != null)
            {
                oper.BackToWeapon(unableToTake);
            }
            if (reload > 0)
            {
                reload -= 0.01666666f;

                if(reload > 4.98 && reload < 5)
                {
                    if (setting == 0)
                    {
                        Level.Add(new SoundSource(position.x, position.y, 240, "SFX/ReloadDevice.wav", "J"));
                        DuckNetwork.SendToEveryone(new NMSoundSource(position, 240, "SFX/ReloadDevice.wav", "J"));
                    }
                }
            }
            if (oper != null)
            {
                if (((oper.controller && oper.genericController != null) || !oper.controller) && input != null)
                {
                    if (oper.controller)
                    {
                        if (reload <= 0f && oper.duckOwner.inputProfile.genericController.MapPressed(4194304, false))
                        {
                            Fire();
                        }

                        if (oper.duckOwner.inputProfile.genericController.MapPressed(8))
                        {
                            if (mode < maxMode)
                            {
                                mode++;
                            }
                            else
                            {
                                mode = 0;
                            }
                        }
                    }
                    else
                    {
                        if (reload <= 0f && (input.Pressed("SHOOT") || Mouse.left == InputState.Pressed))
                        {
                            Fire();
                        }

                        if (Keyboard.Pressed(PlayerStats.keyBindings[22]) || Keyboard.Pressed(PlayerStats.keyBindingsAlternate[22]))
                        {
                            if (mode < maxMode)
                            {
                                mode++;
                            }
                            else
                            {
                                mode = 0;
                            }
                        }

                        if(Keyboard.Pressed(PlayerStats.keyBindings[9]) || Keyboard.Pressed(PlayerStats.keyBindingsAlternate[9]))
                        {
                            LitUp();
                        }
                    }
                }
            }
        }

        public virtual void LitUp()
        {
            foreach(Device d in Level.current.things[typeof(Device)])
            {
                if(d.oper == oper && d.mainDevice == this && !d.jammed)
                {
                    d.Activation();
                }
            }
        }

        public virtual void SetMissile()
        {

        }

        public virtual void LaunchMissile()
        {
            if (oper != null)
            {
                if (Missiles1 > 0 && mode == 0)
                {
                    Missiles1--;
                    SetMissile();

                    if (mainDevice == null && oper.local )
                    {
                        PlayerStats.renown += DeviceCost;
                        PlayerStats.Save();
                        Level.Add(new RenownGained() { description = descriptionPoints, amount = DeviceCost });
                    }

                    float dir = 0;
                    if (offDir > 0)
                        dir = oper.holdAngle;
                    else
                        dir = (float)(Math.PI) + oper.holdAngle;

                    if (missile != null)
                    {
                        missile.vSpeed = 30 * (float)Math.Sin(dir) * speed * vlSpeed;
                        missile.hSpeed = 30 * (float)Math.Cos(dir) * speed;
                        //missile.offDir = offDir;
                        missile.flipVertical = offDir < 0;
                        missile.angle = dir;
                        missile.mainDevice = this;
                        reload = 2f;

                        Level.Add(new SoundSource(position.x, position.y, 280, placeSound, "J"));
                        DuckNetwork.SendToEveryone(new NMSoundSource(position, 280, placeSound, "J"));

                        if (isServerForObject)
                            Level.Add(missile);
                    }
                }
            }
        }

        public virtual void LaunchMissile1()
        {
            if (oper != null)
            {
                Missiles2--;
                SetMissile();

                if (mainDevice == null && oper.local)
                {
                    PlayerStats.renown += DeviceCost;
                    PlayerStats.Save();
                    Level.Add(new RenownGained() { description = descriptionPoints, amount = DeviceCost });
                }

                float dir = 0;
                if (offDir > 0)
                    dir = oper.holdAngle;
                else
                    dir = (float)(Math.PI) + oper.holdAngle;

                if (missile1 != null)
                {
                    missile1.vSpeed = 30 * (float)Math.Sin(dir) * speed * vlSpeed;
                    missile1.hSpeed = 30 * (float)Math.Cos(dir) * speed;
                    //missile.offDir = offDir;
                    missile1.flipVertical = offDir < 0;
                    missile1.angle = dir;
                    missile1.mainDevice = this;
                    reload = 2f;

                    Level.Add(new SoundSource(position.x, position.y, 280, place2Sound, "J"));
                    DuckNetwork.SendToEveryone(new NMSoundSource(position, 280, place2Sound, "J"));

                    if (isServerForObject)
                        Level.Add(missile1);
                }
            }
        }

        public virtual void Fire()
        {
            if (own != null && oper != null && owner != null && input != null)
            {
                if ((oper.controller && oper.genericController != null) || !oper.controller)
                {
                    if (oper.controller)
                    {
                        if (reload <= 0f && oper.duckOwner.inputProfile.genericController.MapPressed(4194304, false))
                        {
                            if (Missiles1 > 0 && mode == 0)
                            {
                                LaunchMissile();
                                DuckNetwork.SendToEveryone(new NMLaunchers(this, "M1"));
                            }
                            else if (Missiles2 > 0 && mode == 1)
                            {
                                LaunchMissile1();
                                DuckNetwork.SendToEveryone(new NMLaunchers(this, "M2"));
                            }
                            else
                            {
                                SFX.Play("click", 1f);
                                for (int i = 0; i < 2; i++)
                                {
                                    SmallSmoke s = SmallSmoke.New(this.position.x + 3f * offDir, this.position.y - 2f);
                                    s.scale = new Vec2(0.3f, 0.3f);
                                    s.hSpeed = Rando.Float(-0.1f, 0.1f);
                                    s.vSpeed = -Rando.Float(0.05f, 0.2f);
                                    s.alpha = 0.6f;
                                    Level.Add(s);
                                    reload = 0.5f;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (reload <= 0f && (input.Pressed("SHOOT") || Mouse.left == InputState.Pressed))
                        {
                            if (Missiles1 > 0 && mode == 0)
                            {
                                LaunchMissile();
                                DuckNetwork.SendToEveryone(new NMLaunchers(this, "M1"));
                            }
                            else if (Missiles2 > 0 && mode == 1)
                            {
                                LaunchMissile1();
                                DuckNetwork.SendToEveryone(new NMLaunchers(this, "M2"));
                            }
                            else
                            {
                                SFX.Play("click", 1f);
                                for (int i = 0; i < 2; i++)
                                {
                                    SmallSmoke s = SmallSmoke.New(position.x + 3f * offDir, position.y - 2f);
                                    s.scale = new Vec2(0.3f, 0.3f);
                                    s.hSpeed = Rando.Float(-0.1f, 0.1f);
                                    s.vSpeed = -Rando.Float(0.05f, 0.2f);
                                    s.alpha = 0.6f;
                                    Level.Add(s);
                                    reload = 0.5f;
                                }
                            }
                        }
                    }
                }
            }
        }

        public override void OnDrawLayer(Layer layer)
        {
            if(layer == Layer.Foreground)
            {
                if(oper != null)
                {
                    if(oper.holdObject == this && oper.local)
                    {
                        if(maxMode > 0)
                        {
                            Vec2 pos = Level.current.camera.position;
                            Vec2 cameraSize = Level.current.camera.size;
                            Vec2 Unit = cameraSize / new Vec2(320, 180);

                            SpriteMap _widebutton = new SpriteMap(GetPath("Sprites/Keys.png"), 34, 17);
                            _widebutton.CenterOrigin();
                            _widebutton.scale = new Vec2(0.8f * Unit.x, 0.8f * Unit.x) * 0.5f;

                            SpriteMap _button = new SpriteMap(GetPath("Sprites/Keys.png"), 17, 17);
                            _button.CenterOrigin();
                            _button.scale = new Vec2(0.8f * Unit.x, 0.8f * Unit.x) * 0.5f;

                            if (PlayerStats.GetSizeOfButton(PlayerStats.keyBindings[22]))
                            {
                                _widebutton.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[22]);
                                //Graphics.Draw(_widebutton, pos.x + 292 * Unit.x, pos.y + 157 * Unit.x, 1);
                            }
                            else
                            {
                                _button.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[22]);
                                //Graphics.Draw(_button, pos.x + 292 * Unit.x, pos.y + 157 * Unit.x, 1);
                            }
                        }
                    }
                }
            }
            base.OnDrawLayer(layer);
        }
    }
}
