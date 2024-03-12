using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class LocalOperUI : Thing, IDrawToDifferentLayers
    {
        SinWave _pulse = 0.05f;
        public Operators oper;
        public float Unit;
        public Vec2 pos;
        public Vec2 cameraSize; 
        SpriteMap _item = new SpriteMap(Mod.GetPath<R6S>("Sprites/DevicesIcons.png"), 24, 24);
        SpriteMap _sitem = new SpriteMap(Mod.GetPath<R6S>("Sprites/SDevicesIcons.png"), 24, 24);
        SpriteMap _mode = new SpriteMap(Mod.GetPath<R6S>("Sprites/ModeSwitch.png"), 32, 12);
        SpriteMap _dtip = new SpriteMap(Mod.GetPath<R6S>("Sprites/DefuserUI.png"), 17, 17);
        SpriteMap _cd = new SpriteMap(Mod.GetPath<R6S>("Sprites/whiteDot.png"), 1, 1);
        SpriteMap _count = new SpriteMap(Mod.GetPath<R6S>("Sprites/Counter.png"), 17, 17);

        SpriteMap _button = new SpriteMap(Mod.GetPath<R6S>("Sprites/Keys.png"), 17, 17);
        SpriteMap _widebutton = new SpriteMap(Mod.GetPath<R6S>("Sprites/Keys.png"), 34, 17);
        SpriteMap _gui = new SpriteMap(Mod.GetPath<R6S>("Sprites/Cameras/ObservationGUI.png"), 160, 48);
        SpriteMap _marker = new SpriteMap(Mod.GetPath<R6S>("Sprites/Cameras/ObservationMarker.png"), 24, 24);
        SpriteMap _gu = new SpriteMap(Mod.GetPath<R6S>("Sprites/Cameras/GUIFilters.png"), 64, 36, false);
        SpriteMap _scanner = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/JammedLocation.png"), 32, 32, false);
        SpriteMap _dot = new SpriteMap(Mod.GetPath<R6S>("Sprites/blackDot.png"), 1, 1);
        SpriteMap _cross = new SpriteMap(Mod.GetPath<R6S>("Sprites/DBNO.png"), 32, 32);

        SpriteMap _blood = new SpriteMap(Mod.GetPath<R6S>("Sprites/Bloodlust.png"), 32, 32);
        SpriteMap _guiEnter = new SpriteMap(Mod.GetPath<R6S>("Sprites/Cameras/GUIEnterCam.png"), 64, 36, false);
        SpriteMap _state = new SpriteMap(Mod.GetPath<R6S>("Sprites/State.png"), 48, 48);
        SpriteMap _toxic= new SpriteMap(Mod.GetPath<R6S>("Sprites/Toxic.png"), 32, 32);

        public LocalOperUI(float xpos, float ypos) : base(xpos, ypos)
        {
            layer = Layer.Foreground;
        }

        public override void Update()
        {
            pos = Level.current.camera.position;
            Unit = Level.current.camera.size.x / 320;
            cameraSize = Level.current.camera.size;
            
            base.Update();
        }

        public void DrawKeyboardInventory()
        {
            _item.center = new Vec2();
            _sitem.center = new Vec2();

            _item.scale = new Vec2(0.4f, 0.4f) * Unit;
            _sitem.scale = new Vec2(0.4f, 0.4f) * Unit;

            for (int i = 0; i < 4; i++)
            {
                _sitem.alpha = 1;
                _item.frame = 71;
                _item.alpha = 0.7f;
                Graphics.Draw(_item, Level.current.camera.position.x + 137 * Unit + i * 15 * Unit, Level.current.camera.position.y + 160 * Unit, 0.2f);
                if (i == 0)
                {
                    _sitem.alpha = 1;
                    _sitem.frame = oper.SecondDevice.index;
                    if (oper.SecondDevice is Throwable)
                    {
                        if ((oper.SecondDevice as Throwable).inPlace && (oper.SecondDevice as Throwable).doubleActivation)
                        {
                            _sitem.alpha = 0.7f + _pulse * 0.3f;
                        }
                        else if (oper.SecondDevice.UsageCount <= 0)
                        {
                            _sitem.alpha = 0.5f;
                        }
                    }
                    if (oper.SecondDevice is Placeable)
                    {
                        if (oper.SecondDevice.UsageCount <= 0)
                        {
                            _sitem.alpha = 0.5f;
                        }
                    }
                    Graphics.Draw(_sitem, Level.current.camera.position.x + 137 * Unit + i * 15 * Unit, Level.current.camera.position.y + 160 * Unit, 0.4f);
                    _sitem.alpha = 1;

                    if (oper.SecondDevice.UsageCount > 0 && oper.SecondDevice.ShowCounter)
                    {
                        _count.frame = oper.SecondDevice.UsageCount;
                        _count.scale = new Vec2(0.6f, 0.6f) * Unit;
                        Graphics.Draw(_count, Level.current.camera.position.x + 136.5f * Unit + i * 15 * Unit, Level.current.camera.position.y + 152 * Unit, 0.4f);
                    }
                }
                if (i == 1)
                {
                    _sitem.alpha = 1f;
                    if ((oper.inventory[5] as Phone).ConnectedCameras() <= 0 && !oper.HasEffect("Phone called"))
                    {
                        //_sitem.alpha = 0.5f;
                    }
                    _sitem.frame = 21;
                    Graphics.Draw(_sitem, Level.current.camera.position.x + 137 * Unit + i * 15 * Unit, Level.current.camera.position.y + 160 * Unit, 0.4f);
                    _sitem.alpha = 1f;

                }
                if (i == 2)
                {

                    if (oper.drone.UsageCount > 0 && (oper.drone is Throwable) && oper.team == "Att")
                    {
                        _count.frame = oper.drone.UsageCount;
                        _count.scale = new Vec2(0.6f, 0.6f) * Unit;
                        Graphics.Draw(_count, Level.current.camera.position.x + 136.5f * Unit + i * 15 * Unit, Level.current.camera.position.y + 152 * Unit, 0.4f);
                    }
                    else
                    {
                        _sitem.alpha = 0.5f;
                    }
                    _sitem.frame = 7;
                    Graphics.Draw(_sitem, Level.current.camera.position.x + 137 * Unit + i * 15 * Unit, Level.current.camera.position.y + 160 * Unit, 0.4f);


                }
                if (i == 3)
                {
                    if (oper.operatorID >= 0)
                    {
                        _item.alpha = 1;
                        _item.frame = oper.MainDevice.index;
                        if (oper.MainDevice is Throwable)
                        {
                            if ((oper.MainDevice as Throwable).inPlace && (oper.MainDevice as Throwable).doubleActivation)
                            {
                                _item.alpha = 0.7f + _pulse * 0.3f;
                            }
                            else if (oper.MainDevice.UsageCount <= 0)
                            {
                                _item.alpha = 0.5f;
                            }
                        }
                        if (oper.MainDevice is Placeable)
                        {
                            if (oper.MainDevice.UsageCount <= 0)
                            {
                                _item.alpha = 0.5f;
                            }
                        }

                        Graphics.Draw(_item, Level.current.camera.position.x + 137 * Unit + i * 15 * Unit, Level.current.camera.position.y + 160 * Unit, 0.4f);
                        _item.alpha = 1;

                        if (oper.MainDevice.UsageCount > 0 || oper.MainDevice.Cooldown > 0)
                        {
                            bool show = false;
                            if (oper.MainDevice.ShowCounter)
                            {
                                _count.frame = oper.MainDevice.UsageCount;
                                show = true;
                            }
                            if(!oper.MainDevice.ShowCounter && oper.MainDevice.ShowCooldown)
                            {
                                _count.frame = 0;
                            }
                            if (show)
                            {
                                _count.scale = new Vec2(0.6f, 0.6f) * Unit;
                                Graphics.Draw(_count, Level.current.camera.position.x + 136.5f * Unit + i * 15 * Unit, Level.current.camera.position.y + 152 * Unit, 0.4f);
                            }

                            show = false;
                            if (oper.MainDevice.ShowCooldown)
                            {
                                show = true;
                            }
                            if (show)
                            {
                                _cd.scale = new Vec2(0.6f, 0.6f) * Unit;
                                _cd.CenterOrigin();

                                for (int k = 0; k < oper.MainDevice.Cooldown * 100; k++)
                                {
                                    float num = 3.6f * k - 90;
                                    float ang = Maths.DegToRad(num);
                                    _cd.angleDegrees = num;
                                    _cd.color = Color.White;
                                    Graphics.Draw(_cd, Level.current.camera.position.x + (136.5f + 5f + 4f * (float)Math.Cos(ang)) * Unit + i * 15 * Unit,
                                        Level.current.camera.position.y + (152 + 5f + 4f * (float)Math.Sin(ang)) * Unit, 0.5f);
                                }
                            }
                        }
                    }
                    else
                    {
                        _sitem.alpha = 1;
                        _sitem.frame = oper.MainDevice.index;
                        if (oper.MainDevice is Throwable)
                        {
                            if ((oper.MainDevice as Throwable).inPlace && (oper.MainDevice as Throwable).doubleActivation)
                            {
                                _sitem.alpha = 0.7f + _pulse * 0.3f;
                            }
                            else if (oper.MainDevice.UsageCount <= 0)
                            {
                                _sitem.alpha = 0.5f;
                            }
                        }
                        if (oper.MainDevice is Placeable)
                        {
                            if (oper.MainDevice.UsageCount <= 0)
                            {
                                _sitem.alpha = 0.5f;
                            }
                        }
                        Graphics.Draw(_sitem, Level.current.camera.position.x + 137 * Unit + i * 15 * Unit, Level.current.camera.position.y + 160 * Unit, 0.4f);
                        _sitem.alpha = 1;

                        if ((oper.MainDevice.UsageCount > 0 || oper.MainDevice.Cooldown > 0) && oper.MainDevice.ShowCounter)
                        {
                            _count.frame = oper.MainDevice.UsageCount;
                            _count.scale = new Vec2(0.6f, 0.6f) * Unit;
                            Graphics.Draw(_count, Level.current.camera.position.x + 136.5f * Unit + i * 15 * Unit, Level.current.camera.position.y + 152 * Unit, 0.4f);

                            _cd.scale = new Vec2(0.6f, 0.6f) * Unit;
                            _cd.CenterOrigin();

                            for (int k = 0; k < oper.MainDevice.Cooldown * 100; k++)
                            {
                                float num = 3.6f * k - 90;
                                float ang = Maths.DegToRad(num);
                                _cd.angleDegrees = num;
                                _cd.color = Color.White;
                                Graphics.Draw(_cd, Level.current.camera.position.x + (136.5f + 5f + 4f * (float)Math.Cos(ang)) * Unit + i * 15 * Unit,
                                    Level.current.camera.position.y + (152 + 5f + 4f * (float)Math.Sin(ang)) * Unit, 0.5f);
                            }
                        }
                    }
                }
            }
        }

        public void DrawControllerInventory()
        {
            SpriteMap _item = new SpriteMap(GetPath("Sprites/DevicesIcons.png"), 24, 24);
            SpriteMap _sitem = new SpriteMap(GetPath("Sprites/SDevicesIcons.png"), 24, 24);

            _item.center = new Vec2();
            _sitem.center = new Vec2();

            if (oper.openInventory && oper.lockWeaponChange <= 0)
            {
                SpriteMap _circle = new SpriteMap(GetPath("Sprites/InventoryCircle.png"), 64, 64);
                _circle.CenterOrigin();
                _circle.scale = new Vec2(Unit, Unit);
                Graphics.Draw(_circle, Level.current.camera.position.x + 160 * Unit, Level.current.camera.position.y + 90 * Unit, 0.2f);

                _item.CenterOrigin();
                _sitem.CenterOrigin();
                _item.scale = new Vec2(0.6f, 0.6f) * Unit;
                _sitem.scale = new Vec2(0.6f, 0.6f) * Unit;

                for (int i = 0; i < 4; i++)
                {
                    //Second Device
                    if (i == 0)
                    {
                        _sitem.frame = oper.SecondDevice.index;

                        if (oper.SecondDevice is Throwable)
                        {
                            if ((oper.SecondDevice as Throwable).inPlace && (oper.SecondDevice as Throwable).doubleActivation)
                            {
                                _sitem.alpha = 0.7f + _pulse * 0.3f;
                            }
                            else if (oper.SecondDevice.UsageCount <= 0)
                            {
                                _sitem.alpha = 0.5f;
                            }
                        }
                        if (oper.SecondDevice is Placeable)
                        {
                            if (oper.SecondDevice.UsageCount <= 0)
                            {
                                _sitem.alpha = 0.5f;
                            }
                        }

                        Graphics.Draw(_sitem, Level.current.camera.position.x + 160 * Unit, Level.current.camera.position.y + 90 * Unit + 25 * Unit, 0.4f);
                        _sitem.alpha = 1;

                        if (oper.SecondDevice.UsageCount > 0 && oper.SecondDevice.ShowCounter)
                        {
                            SpriteMap _count = new SpriteMap(GetPath("Sprites/Counter.png"), 17, 17);
                            _count.frame = oper.SecondDevice.UsageCount;
                            _count.CenterOrigin();
                            _count.scale = new Vec2(0.6f, 0.6f) * Unit;
                            Graphics.Draw(_count, Level.current.camera.position.x + 160f * Unit, Level.current.camera.position.y + (90 + 25 + 12) * Unit, 0.4f);
                        }
                    }
                    //Main Gun
                    if (i == 1)
                    {
                        _sitem.frame = 17;
                        Graphics.Draw(_sitem, Level.current.camera.position.x + 160 * Unit, Level.current.camera.position.y + 90 * Unit - 25 * Unit, 0.4f);
                    }

                    //SecondGun
                    if (i == 2)
                    {
                        _sitem.frame = 16;
                        Graphics.Draw(_sitem, Level.current.camera.position.x + 160 * Unit - 25 * Unit, Level.current.camera.position.y + 90 * Unit, 0.4f);
                    }

                    //Main device
                    if (i == 3)
                    {
                        if (oper.operatorID >= 0)
                        {
                            _item.alpha = 1;
                            _item.frame = oper.MainDevice.index;
                            if (oper.MainDevice is Throwable)
                            {
                                if ((oper.MainDevice as Throwable).inPlace && (oper.MainDevice as Throwable).doubleActivation)
                                {
                                    _item.alpha = 0.7f + _pulse * 0.3f;
                                }
                                else if (oper.MainDevice.UsageCount <= 0)
                                {
                                    _item.alpha = 0.5f;
                                }
                            }
                            if (oper.MainDevice is Placeable)
                            {
                                if (oper.MainDevice.UsageCount <= 0)
                                {
                                    _item.alpha = 0.5f;
                                }
                            }
                            Graphics.Draw(_item, Level.current.camera.position.x + 160 * Unit + 25 * Unit, Level.current.camera.position.y + 90 * Unit, 0.4f);
                            _item.alpha = 1;

                            if (oper.MainDevice.UsageCount > 0 && oper.MainDevice.ShowCounter)
                            {
                                SpriteMap _count = new SpriteMap(GetPath("Sprites/Counter.png"), 17, 17);
                                _count.frame = oper.MainDevice.UsageCount;
                                _count.CenterOrigin();
                                _count.scale = new Vec2(0.6f, 0.6f) * Unit;
                                Graphics.Draw(_count, Level.current.camera.position.x + (160f + 25 + 12) * Unit, Level.current.camera.position.y + 90 * Unit, 0.4f);
                            }
                        }
                        else
                        {
                            _sitem.alpha = 1;
                            _sitem.frame = oper.MainDevice.index;
                            if (oper.MainDevice is Throwable)
                            {
                                if ((oper.MainDevice as Throwable).inPlace && (oper.MainDevice as Throwable).doubleActivation)
                                {
                                    _sitem.alpha = 0.7f + _pulse * 0.3f;
                                }
                                else if (oper.MainDevice.UsageCount <= 0)
                                {
                                    _sitem.alpha = 0.5f;
                                }
                            }
                            if (oper.MainDevice is Placeable)
                            {
                                if (oper.MainDevice.UsageCount <= 0)
                                {
                                    _sitem.alpha = 0.5f;
                                }
                            }
                            Graphics.Draw(_sitem, Level.current.camera.position.x + 160 * Unit + 25 * Unit, Level.current.camera.position.y + 90 * Unit, 0.4f);
                            _sitem.alpha = 1;

                            if (oper.MainDevice.UsageCount > 0 && oper.MainDevice.ShowCounter)
                            {
                                SpriteMap _count = new SpriteMap(GetPath("Sprites/Counter.png"), 17, 17);
                                _count.frame = oper.MainDevice.UsageCount;
                                _count.CenterOrigin();
                                _count.scale = new Vec2(0.6f, 0.6f) * Unit;
                                Graphics.Draw(_count, Level.current.camera.position.x + (160f + 25 + 12) * Unit, Level.current.camera.position.y + 90 * Unit, 0.4f);
                            }
                        }

                    }
                }
            }
            else
            {
                SpriteMap _button = new SpriteMap(GetPath("Sprites/Keys.png"), 17, 17);
                _button.scale = new Vec2(0.4f, 0.4f) * Unit;

                _button.frame = PlayerStats.GetFrameOfButtonGP("LStickUP");

                _button.scale = new Vec2(0.6f, 0.6f) * Unit;


                for (int i = 0; i < 4; i++)
                {
                    _sitem.alpha = 1;
                    _sitem.frame = 71;
                    _item.alpha = 0.7f;
                    //Graphics.Draw(_item, Level.current.camera.position.x + 137 * Unit + i * 15 * Unit, Level.current.camera.position.y + 160 * Unit, 0.2f);

                    //Inventory
                    if (i == 0)
                    {
                        _sitem.alpha = 1;
                        _sitem.frame = 20;
                        Graphics.Draw(_sitem, Level.current.camera.position.x + 137 * Unit + i * 15 * Unit, Level.current.camera.position.y + 160 * Unit, 0.4f);

                        _sitem.alpha = 1;
                    }

                    //Phone
                    if (i == 1)
                    {
                        _sitem.alpha = 1f;
                        if ((oper.inventory[5] as Phone).ConnectedCameras() <= 0)
                        {
                            _sitem.alpha = 0.5f;
                        }
                        _sitem.frame = 21;
                        Graphics.Draw(_sitem, Level.current.camera.position.x + 137 * Unit + i * 15 * Unit, Level.current.camera.position.y + 160 * Unit, 0.4f);
                        _sitem.alpha = 1f;

                    }

                    //Drone
                    if (i == 2)
                    {
                        if (oper.drone.UsageCount > 0 && (oper.drone is Throwable) && oper.team == "Att")
                        {
                            SpriteMap _count = new SpriteMap(GetPath("Sprites/Counter.png"), 17, 17);
                            _count.frame = oper.drone.UsageCount;
                            _count.scale = new Vec2(0.6f, 0.6f) * Unit;
                            Graphics.Draw(_count, Level.current.camera.position.x + 136.5f * Unit + i * 15 * Unit, Level.current.camera.position.y + 152 * Unit, 0.4f);
                        }
                        else
                        {
                            _sitem.alpha = 0.5f;
                        }
                        _sitem.frame = 7;
                        Graphics.Draw(_sitem, Level.current.camera.position.x + 137 * Unit + i * 15 * Unit, Level.current.camera.position.y + 160 * Unit, 0.4f);
                    }

                    //ChangeMode;
                    if (i == 3)
                    {

                        _sitem.alpha = 1;
                        _sitem.frame = 15;
                        Graphics.Draw(_sitem, Level.current.camera.position.x + 137 * Unit + i * 15 * Unit, Level.current.camera.position.y + 160 * Unit, 0.4f);
                        _sitem.alpha = 1;

                    }
                }
                _button.scale = new Vec2(0.4f, 0.4f) * Unit;

                //Prone
                _button.frame = PlayerStats.GetFrameOfButtonGP("LT");
                Graphics.Draw(_button, Level.current.camera.position.x + 30 * Unit, Level.current.camera.position.y + 136 * Unit, 0.5f);

                //Melee
                _button.frame = PlayerStats.GetFrameOfButtonGP("RButton");
                Graphics.Draw(_button, Level.current.camera.position.x + 260 * Unit, Level.current.camera.position.y + 156 * Unit);



                if (oper.holdObject is GunDev)
                {
                    //Sprint                                    
                    _button.frame = PlayerStats.GetFrameOfButtonGP("RT");
                    Graphics.Draw(_button, Level.current.camera.position.x + 30 * Unit, Level.current.camera.position.y + 150 * Unit, 0.5f);

                    //Reload
                    _button.frame = PlayerStats.GetFrameOfButtonGP("X");
                    Graphics.Draw(_button, Level.current.camera.position.x + 244 * Unit, Level.current.camera.position.y + 156 * Unit, 0.5f);
                }

                //Inventory
                _button.frame = PlayerStats.GetFrameOfButtonGP("CrossUP");
                Graphics.Draw(_button, Level.current.camera.position.x + 138 * Unit, Level.current.camera.position.y + 168 * Unit, 0.5f);
                //Cameras
                _button.frame = PlayerStats.GetFrameOfButtonGP("B");
                Graphics.Draw(_button, Level.current.camera.position.x + 153 * Unit, Level.current.camera.position.y + 168 * Unit, 0.5f);
                //Drone
                _button.frame = PlayerStats.GetFrameOfButtonGP("Y");
                Graphics.Draw(_button, Level.current.camera.position.x + 168 * Unit, Level.current.camera.position.y + 168 * Unit, 0.5f);
                //ChangeMode
                _button.frame = PlayerStats.GetFrameOfButtonGP("CrossRIGHT");
                Graphics.Draw(_button, Level.current.camera.position.x + 183 * Unit, Level.current.camera.position.y + 168 * Unit, 0.5f);




                if (oper.holdObject is Launchers)
                {
                    if ((oper.holdObject as Launchers).maxMode > 0)
                    {

                        SpriteMap _mode = new SpriteMap(GetPath("Sprites/ModeSwitch.png"), 32, 12);
                        _mode.scale = new Vec2(0.8f, 0.8f) * Unit;
                        _mode.frame = (oper.holdObject as Launchers).mode;
                        Graphics.Draw(_mode, Level.current.camera.position.x + 280 * Unit, Level.current.camera.position.y + 156 * Unit);



                        SpriteMap _count = new SpriteMap(GetPath("Sprites/Counter.png"), 17, 17);
                        _count.frame = (oper.MainDevice as Launchers).Missiles1;
                        _count.scale = new Vec2(0.4f, 0.4f) * Unit;
                        Graphics.Draw(_count, Level.current.camera.position.x + (280f + 12 - 8) * Unit, Level.current.camera.position.y + 162 * Unit, 0.4f);


                        _count.frame = (oper.MainDevice as Launchers).Missiles2;
                        _count.scale = new Vec2(0.4f, 0.4f) * Unit;
                        Graphics.Draw(_count, Level.current.camera.position.x + (280f + 12 + 4) * Unit, Level.current.camera.position.y + 162 * Unit, 0.4f);


                        Vec2 Pos = Level.current.camera.position;
                        Vec2 cameraSize = Level.current.camera.size;


                        _button.frame = PlayerStats.GetFrameOfButtonGP("CrossRIGHT");
                        Graphics.Draw(_button, Pos.x + 272 * Unit, Pos.y + 156 * Unit);

                    }
                }
            }
        }

        public void DrawKeys()
        {
            _widebutton.scale = new Vec2(0.6f, 0.6f) * Unit;
            _button.scale = new Vec2(0.4f, 0.4f) * Unit;

            //Prone
            if (!PlayerStats.hideKeyBindIcons)
            {
                if (PlayerStats.GetSizeOfButton(PlayerStats.keyBindings[6]))
                {
                    _widebutton.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[6]);
                    _widebutton.scale = new Vec2(0.6f, 0.6f) * Unit;
                    Graphics.Draw(_widebutton, Level.current.camera.position.x + 36 * Unit, Level.current.camera.position.y + 134 * Unit);
                    _widebutton.scale = new Vec2(0.4f, 0.4f) * Unit;
                }
                else
                {
                    _button.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[6]);
                    _button.scale = new Vec2(0.6f, 0.6f) * Unit;
                    Graphics.Draw(_button, Level.current.camera.position.x + 36 * Unit, Level.current.camera.position.y + 134 * Unit);
                    _button.scale = new Vec2(0.4f, 0.4f) * Unit;
                }
            }

            if (!PlayerStats.hideKeyBindIcons)
            {
                if (oper.holdObject is Device && !(oper.holdObject as Device).lockSprint)
                {
                    //Sprint
                    if (PlayerStats.GetSizeOfButton(PlayerStats.keyBindings[19]))
                    {
                        _widebutton.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[19]);
                        Graphics.Draw(_widebutton, Level.current.camera.position.x + 30 * Unit, Level.current.camera.position.y + 150 * Unit, 0.5f);
                        _widebutton.scale = new Vec2(0.4f, 0.4f) * Unit;
                    }
                    else
                    {
                        _button.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[19]);
                        Graphics.Draw(_button, Level.current.camera.position.x + 30 * Unit, Level.current.camera.position.y + 150 * Unit, 0.5f);
                        _button.scale = new Vec2(0.4f, 0.4f) * Unit;
                    }

                    //Reload
                    if (PlayerStats.GetSizeOfButton(PlayerStats.keyBindings[20]))
                    {
                        _widebutton.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[20]);
                        Graphics.Draw(_widebutton, Level.current.camera.position.x + 246 * Unit, Level.current.camera.position.y + 156 * Unit);
                        _widebutton.scale = new Vec2(0.4f, 0.4f) * Unit;
                    }
                    else
                    {
                        _button.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[20]);
                        Graphics.Draw(_button, Level.current.camera.position.x + 246 * Unit, Level.current.camera.position.y + 156 * Unit);
                        _button.scale = new Vec2(0.4f, 0.4f) * Unit;
                    }
                }
            }

            if (oper.holdObject is Launchers)
            {
                if ((oper.holdObject as Launchers).maxMode > 0)
                {
                    _mode.scale = new Vec2(0.8f, 0.8f) * Unit;
                    _mode.frame = (oper.holdObject as Launchers).mode;
                    Graphics.Draw(_mode, Level.current.camera.position.x + 280 * Unit, Level.current.camera.position.y + 156 * Unit);

                    _count.frame = (oper.MainDevice as Launchers).Missiles1;
                    _count.scale = new Vec2(0.4f, 0.4f) * Unit;
                    Graphics.Draw(_count, Level.current.camera.position.x + (280f + 12 - 8) * Unit, Level.current.camera.position.y + 162 * Unit, 0.4f);


                    _count.frame = (oper.MainDevice as Launchers).Missiles2;
                    _count.scale = new Vec2(0.4f, 0.4f) * Unit;
                    Graphics.Draw(_count, Level.current.camera.position.x + (280f + 12 + 4) * Unit, Level.current.camera.position.y + 162 * Unit, 0.4f);


                    Vec2 Pos = Level.current.camera.position;
                    Vec2 cameraSize = Level.current.camera.size;

                    if (!PlayerStats.hideKeyBindIcons)
                    {
                        if (PlayerStats.GetSizeOfButton(PlayerStats.keyBindings[22]))
                        {
                            _widebutton.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[22]);
                            Graphics.Draw(_widebutton, Pos.x + 272 * Unit, Pos.y + 156 * Unit);
                        }
                        else
                        {
                            _button.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[22]);
                            Graphics.Draw(_button, Pos.x + 272 * Unit, Pos.y + 156 * Unit);
                        }
                    }
                }
            }


            //Secondary device
            if (!PlayerStats.hideKeyBindIcons)
            {
                if (PlayerStats.GetSizeOfButton(PlayerStats.keyBindings[10]))
                {
                    _widebutton.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[10]);
                    Graphics.Draw(_widebutton, Level.current.camera.position.x + 138 * Unit, Level.current.camera.position.y + 168 * Unit);
                    _widebutton.scale = new Vec2(0.4f, 0.4f) * Unit;
                }
                else
                {
                    _button.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[10]);
                    Graphics.Draw(_button, Level.current.camera.position.x + 138 * Unit, Level.current.camera.position.y + 168 * Unit);
                    _button.scale = new Vec2(0.4f, 0.4f) * Unit;
                }
            }

            //_button.frame = 42;
            //Graphics.Draw(_button, Level.current.camera.position.x + 153 * Unit, Level.current.camera.position.y + 168 * Unit);

            //Open cameras
            if (!PlayerStats.hideKeyBindIcons)
            {
                if (PlayerStats.GetSizeOfButton(PlayerStats.keyBindings[11]))
                {
                    _widebutton.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[11]);
                    Graphics.Draw(_widebutton, Level.current.camera.position.x + 153 * Unit, Level.current.camera.position.y + 168 * Unit);
                    _widebutton.scale = new Vec2(0.4f, 0.4f) * Unit;
                }
                else
                {
                    _button.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[11]);
                    Graphics.Draw(_button, Level.current.camera.position.x + 153 * Unit, Level.current.camera.position.y + 168 * Unit);
                    _button.scale = new Vec2(0.4f, 0.4f) * Unit;
                }
            }


            //Drones
            if (!PlayerStats.hideKeyBindIcons)
            {
                if (PlayerStats.GetSizeOfButton(PlayerStats.keyBindings[12]))
                {
                    _widebutton.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[12]);
                    Graphics.Draw(_widebutton, Level.current.camera.position.x + 168 * Unit, Level.current.camera.position.y + 168 * Unit);
                    _widebutton.scale = new Vec2(0.4f, 0.4f) * Unit;
                }
                else
                {
                    _button.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[12]);
                    Graphics.Draw(_button, Level.current.camera.position.x + 168 * Unit, Level.current.camera.position.y + 168 * Unit);
                    _button.scale = new Vec2(0.4f, 0.4f) * Unit;
                }
            }


            //Main device
            if (!PlayerStats.hideKeyBindIcons)
            {
                if (PlayerStats.GetSizeOfButton(PlayerStats.keyBindings[9]))
                {
                    _widebutton.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[9]);
                    Graphics.Draw(_widebutton, Level.current.camera.position.x + 183 * Unit, Level.current.camera.position.y + 168 * Unit);
                    _widebutton.scale = new Vec2(0.4f, 0.4f) * Unit;
                }
                else
                {
                    _button.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[9]);
                    Graphics.Draw(_button, Level.current.camera.position.x + 183 * Unit, Level.current.camera.position.y + 168 * Unit);
                    _button.scale = new Vec2(0.4f, 0.4f) * Unit;
                }
            }

            //Melee
            if (!PlayerStats.hideKeyBindIcons)
            {
                if (PlayerStats.GetSizeOfButton(PlayerStats.keyBindings[21]))
                {
                    _widebutton.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[21]);
                    Graphics.Draw(_widebutton, Level.current.camera.position.x + 260 * Unit, Level.current.camera.position.y + 156 * Unit);
                    _widebutton.scale = new Vec2(0.4f, 0.4f) * Unit;
                }
                else
                {
                    _button.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[21]);
                    Graphics.Draw(_button, Level.current.camera.position.x + 260 * Unit, Level.current.camera.position.y + 156 * Unit);
                    _button.scale = new Vec2(0.4f, 0.4f) * Unit;
                }
            }

            //Defuser plant
            _dtip.CenterOrigin();
            _dtip.scale = new Vec2(Unit);
            //_dtip.depth = 0.1f;
            if (oper.canPlant || oper.canDefuse)
            {
                if (!PlayerStats.hideKeyBindIcons)
                {
                    if (PlayerStats.GetSizeOfButton(PlayerStats.keyBindings[5]))
                    {
                        _widebutton.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[5]);
                        Graphics.Draw(_widebutton, Level.current.camera.position.x + 297.5f * Unit, Level.current.camera.position.y + 156 * Unit);
                        _widebutton.scale = new Vec2(0.4f, 0.4f) * Unit;
                    }
                    else
                    {
                        _button.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[5]);
                        Graphics.Draw(_button, Level.current.camera.position.x + 297.5f * Unit, Level.current.camera.position.y + 156 * Unit);
                        _button.scale = new Vec2(0.4f, 0.4f) * Unit;
                    }
                }

                if (oper.canPlant)
                {
                    _dtip.frame = 2;
                }
                else
                {
                    _dtip.frame = 3;
                }
                Graphics.Draw(_dtip, Level.current.camera.position.x + 300 * Unit, Level.current.camera.position.y + 140 * Unit);
            }
            if(oper.canDropDefuser || oper.canPickUpDefuser)
            {
                if (!PlayerStats.hideKeyBindIcons)
                {
                    if (PlayerStats.GetSizeOfButton(PlayerStats.keyBindings[23]))
                    {
                        _widebutton.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[23]);
                        Graphics.Draw(_widebutton, Level.current.camera.position.x + 277.5f * Unit, Level.current.camera.position.y + 156 * Unit);
                        _widebutton.scale = new Vec2(0.4f, 0.4f) * Unit;
                    }
                    else
                    {
                        _button.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[23]);
                        Graphics.Draw(_button, Level.current.camera.position.x + 277.5f * Unit, Level.current.camera.position.y + 156 * Unit);
                        _button.scale = new Vec2(0.4f, 0.4f) * Unit;
                    }
                }

                if (oper.canDropDefuser)
                {
                    _dtip.frame = 0;
                }
                else
                {
                    _dtip.frame = 1;
                }
                Graphics.Draw(_dtip, Level.current.camera.position.x + 280 * Unit, Level.current.camera.position.y + 140 * Unit);
            }
        }

        public void DrawCamerasInterface()
        {
            if(oper.inventory[5] is Phone)
            {
                Phone phone = oper.inventory[5] as Phone;

                if (oper.spectate && phone.ConnectedCameras() > phone.camIndex && !phone.locked)
                {
                    Vec2 camPos = Level.current.camera.position;
                    Vec2 camSize = Level.current.camera.size;
                    _gui.scale = new Vec2(Level.current.camera.width / 480, Level.current.camera.width / 480);
                    _marker.scale = new Vec2(Level.current.camera.width / 480, Level.current.camera.width / 480);
                    Graphics.Draw(_gui, camPos.x + _gui.scale.x * 6, camPos.y + camSize.y - _gui.scale.y * 60, 0.89f);

                    _gu.frame = 1;
                    _gu.scale = new Vec2(Level.current.camera.size.x / 63.9f, Level.current.camera.size.y / 35.9f);

                    Graphics.Draw(_gu, Level.current.camera.position.x - 1, Level.current.camera.position.y - 1, 0.1f);

                    if (oper.observing && (phone.GetCurrentObservable().jammed || phone.GetCurrentObservable().broken))
                    {
                        Vec2 Pos = Level.current.camera.position;
                        _scanner.scale = Level.current.camera.size / new Vec2(22, 22);
                        _scanner.CenterOrigin();
                        _scanner.frame = Rando.Int(2);
                        _scanner.alpha = 1f;
                        Graphics.Draw(_scanner, Pos.x + Level.current.camera.width / 2, Pos.y + Level.current.camera.height / 2, 0.89f);
                    }

                    for (int i = 0; i < (oper.inventory[5] as Phone).ConnectedCameras(); i++)
                    {
                        _marker.frame = 0;
                        if ((oper.inventory[5] as Phone).GetObservable(i).jammed)
                        {
                            _marker.frame = 1;
                        }
                        if (i == (oper.inventory[5] as Phone).camIndex)
                        {
                            _marker.frame = 3;
                        }
                        if ((oper.inventory[5] as Phone).GetObservable(i).broken)
                        {
                            _marker.frame = 2;
                        }
                        Graphics.Draw(_marker, camPos.x + _gui.scale.x * (12 + 18 * i), camPos.y + camSize.y - _gui.scale.y * 36, 0.89f);
                    }
                }
            }
        }

        public void DrawDBNOInterface()
        {
            _dot.scale = Level.current.camera.size + new Vec2(2f, 2f);
            _dot.alpha = 0.7f;
            if (oper.timeUntilDeath <= 15f)
            {
                _dot.alpha = 1f - (oper.timeUntilDeath / 15f) * 0.3f;
            }
            Graphics.Draw(_dot, pos.x - 1, pos.y - 1, 0.1f);

            _cross.scale = cameraSize / new Vec2(160, 90) * Unit + new Vec2(_pulse * 0.05f, _pulse * 0.05f);
            _cross.CenterOrigin();
            _cross.frame = 3;
            Graphics.Draw(_cross, pos.x + cameraSize.x / 2, pos.y + cameraSize.y * 0.4f, 0.5f);


            Graphics.DrawLine(pos + new Vec2(cameraSize.x / 320 * 160 - 100 * (oper.timeUntilDeath / 30) * Unit, cameraSize.y / 180 * 120), pos + new Vec2(cameraSize.x / 320 * 160 + 100 * (oper.timeUntilDeath / 30) * Unit, cameraSize.y / 180 * 120), Color.White * 0.6f, 1f, 0.5f);
            Graphics.DrawLine(pos + new Vec2(cameraSize.x / 320 * 60, cameraSize.y / 180 * 120), pos + new Vec2(cameraSize.x / 320 * 60 + 200 * Unit, cameraSize.y / 180 * 120), Color.Gray, 1f, 0.3f);
            Graphics.DrawLine(pos + new Vec2(cameraSize.x / 320 * 160 - 100 * ((3 - oper.timeToRevive) / 3) * Unit, cameraSize.y / 180 * 120), pos + new Vec2(cameraSize.x / 320 * 160 + 100 * ((3 - oper.timeToRevive) / 3) * Unit, cameraSize.y / 180 * 120), Color.Green, 1f, 0.4f);

            string text = "Try not to move to reduce blood loss";
            if (oper.inventory[3] is Stimulator)
            {
                if ((oper.inventory[3] as Stimulator).UsageCount > 0 && oper.timeUntilDeath < oper.DeathTime * 0.9f)
                {
                    text = "Press   to revive yourself";

                    _widebutton.CenterOrigin();
                    _widebutton.scale = new Vec2(0.8f * Unit, 0.8f * Unit);

                    _button.CenterOrigin();
                    _button.scale = new Vec2(0.8f * Unit, 0.8f * Unit);
                    if (!PlayerStats.hideKeyBindIcons)
                    {
                        if (PlayerStats.GetSizeOfButton(PlayerStats.keyBindings[9]))
                        {
                            _widebutton.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[9]);
                            Graphics.Draw(_widebutton, pos.x + (160 - text.Length / 2 * 9 + 64) * Unit, pos.y + 144 * Unit, 1);
                        }
                        else
                        {
                            _button.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[9]);
                            Graphics.Draw(_button, pos.x + (160 - text.Length / 2 * 9 + 64) * Unit, pos.y + 144 * Unit, 1);
                        }
                    }
                }
            }
            if (oper.inventory[3] is BuffFinka)
            {
                if ((oper.inventory[3] as BuffFinka).UsageCount > 0 && oper.timeUntilDeath < oper.DeathTime * 0.9f)
                {
                    text = "Press   to revive yourself";

                    SpriteMap _widebutton = new SpriteMap(GetPath("Sprites/Keys.png"), 34, 17);
                    _widebutton.CenterOrigin();
                    _widebutton.scale = new Vec2(0.8f * Unit, 0.8f * Unit);

                    SpriteMap _button = new SpriteMap(GetPath("Sprites/Keys.png"), 17, 17);
                    _button.CenterOrigin();
                    _button.scale = new Vec2(0.8f * Unit, 0.8f * Unit);
                    if (!PlayerStats.hideKeyBindIcons)
                    {
                        if (PlayerStats.GetSizeOfButton(PlayerStats.keyBindings[9]))
                        {
                            _widebutton.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[9]);
                            Graphics.Draw(_widebutton, pos.x + (160 - text.Length / 2 * 9 + 64) * Unit, pos.y + 144 * Unit, 1);
                        }
                        else
                        {
                            _button.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[9]);
                            Graphics.Draw(_button, pos.x + (160 - text.Length / 2 * 9 + 64) * Unit, pos.y + 144 * Unit, 1);
                        }
                    }
                }
            }

            Graphics.DrawStringOutline(text, Level.current.camera.position + new Vec2(160 - 4 * text.Length * Unit, 140) * Unit, Color.White, Color.Black, 0.6f, null, 1f);
        }

        public override void Draw()
        {
            if (oper != null)
            {                               
                if(oper.showFrames > 0 && !oper.Healed)
                {
                    _blood.alpha = oper.showFrames*0.02f;
                    _blood.scale = Level.current.camera.size / new Vec2(31, 31);

                    oper.showFrames--;

                    Vec2 pos = Level.current.camera.position;
                    Graphics.Draw(_blood, pos.x, pos.y, 1f);
                }

                if (oper.poisonFrames > 0)
                {
                    _toxic.alpha = oper.poisonFrames * 0.04f;
                    _toxic.scale = Level.current.camera.size / new Vec2(31, 31);

                    Vec2 pos = Level.current.camera.position;
                    Graphics.Draw(_blood, pos.x, pos.y, 1f);
                }

                if (!oper.isDead)
                {
                    if (oper.DBNO)
                    {
                        DrawDBNOInterface();
                    }
                    else
                    {
                        //oper._aim.center = new Vec2(8.5f, 8.5f);
                        Graphics.Draw(oper._aim, oper.aim.x, oper.aim.y);
                        if (!oper.observing)
                        {
                            _state.scale = new Vec2(0.4f, 0.4f) * Unit;
                            if (oper.mode == "normal")
                            {
                                _state.frame = 0;
                            }
                            if (oper.mode == "crouch")
                            {
                                _state.frame = 1;
                            }
                            if (oper.mode == "slide")
                            {
                                _state.frame = 2;
                            }

                            Graphics.Draw(_state, Level.current.camera.position.x + 10 * Unit, Level.current.camera.position.y + 152 * Unit, 0.2f);
                            
                            //Health
                            Graphics.DrawLine(Level.current.camera.position + new Vec2(36 * Unit, 175 * Unit), 
                                Level.current.camera.position + new Vec2(36 * Unit + 50 * ((float)oper.Health / 100) * Unit, 175 * Unit), Color.White * 0.8f, 2 * Unit, 0.5f);
                            Graphics.DrawLine(Level.current.camera.position + new Vec2(36 * Unit, 175 * Unit), 
                                Level.current.camera.position + new Vec2(36 * Unit + 50 * Unit, 175 * Unit), Color.Gray * 0.8f, 2 * Unit, 0.4f);

                            Vec2 posi = new Vec2(Level.current.camera.position.x + Level.current.camera.width / 2, Level.current.camera.position.y + Level.current.camera.height * 59 / 64);
                            float radius = Level.current.camera.width / 30;
                            oper._font.scale = new Vec2(Level.current.camera.size.x / 256 * 0.5625f, Level.current.camera.size.y / 256);
                            //Graphics.DrawString(Convert.ToString(holdIndex), pos - new Vec2(radius / 2, 0f), Color.White, 1f);
                            //Graphics.DrawCircle(pos + new Vec2(radius / 2, 0f), radius, Color.White, Level.current.camera.width / 320, 1f, 32);

                            string text = " + " + Convert.ToString(oper.Health);
                            oper._font.DrawOutline(text, new Vec2(posi.x - 120 * Unit - oper._font.GetWidth(text, false, null) / 4f, posi.y), Color.White, Color.Black, 0.9f);

                            _item.center = new Vec2();
                            _sitem.center = new Vec2();

                            //_item.frame = SecondDevice.index;
                            _item.scale = new Vec2(0.4f, 0.4f) * Unit;
                            _sitem.scale = new Vec2(0.4f, 0.4f) * Unit;
                            //Items showed in inventory - second device, drones, shoottype, maindevice
                            if (oper.controller)
                            {
                                DrawControllerInventory();
                            }
                            else
                            {
                                DrawKeyboardInventory();
                                DrawKeys();                                
                            }
                        }

                        if (oper.observFrames > 0)
                        {
                            oper.observFrames--;
                            _guiEnter.frame = Rando.Int(0, 3);
                            _guiEnter.alpha = oper.observFrames / 10;
                            _guiEnter.scale = new Vec2(Level.current.camera.size.x / 64, Level.current.camera.size.y / 36);

                            Graphics.Draw(_guiEnter, Level.current.camera.position.x, Level.current.camera.position.y, 0.1f);
                        }

                        Vec2 camPos = Level.current.camera.position;
                        float LocalUnit = cameraSize.y / 192;
                        if (oper.holdObject is GunDev)
                        {
                            GunDev gun = oper.holdObject as GunDev;                            
                            if (gun.canFire)
                            {
                                string txt = Convert.ToString(gun.ammo) + " / " + Convert.ToString(gun.magazine);
                                if (gun.game == null)
                                {
                                    BitmapFont _font = new BitmapFont("smallFont", 8, -1);
                                }
                                if (!gun.reloading)
                                {
                                    Graphics.DrawStringOutline(gun.name, camPos + Level.current.camera.size - new Vec2(LocalUnit * 80, LocalUnit * 15), Color.White, Color.Black, 1f, null, Level.current.camera.height / 320);
                                }
                                else
                                {
                                    float reloadTime = gun.timeToReload;
                                    float saveTime = gun.timeToTacticalReload;
                                    float reload = gun.reload;

                                    Vec2 startPos = new Vec2(255, 187);

                                    float reloadMove = (reloadTime - reload) / reloadTime;
                                    float savereload = saveTime / reloadTime;

                                    float length = 58;
                                    float height = 3;

                                    Graphics.DrawRect(camPos + LocalUnit * startPos,
                                        camPos + LocalUnit * (startPos + new Vec2((1 - savereload) * length, height)), Color.Yellow * 0.6f, 1.01f); //Main reload line

                                    Graphics.DrawRect(camPos + LocalUnit * (startPos + new Vec2((1 - savereload) * length, 0)), 
                                        camPos + LocalUnit * (startPos + new Vec2(1 * length, height)), Color.OrangeRed * 0.3f, 1.01f); //tactical reload line

                                    Graphics.DrawRect(camPos + LocalUnit * startPos, 
                                        camPos + LocalUnit * (startPos + new Vec2(reloadMove * length, height)), Color.Blue * 0.4f, 1.02f); //reload line

                                    Graphics.DrawRect(camPos + LocalUnit * (startPos + new Vec2(-1, -1)),
                                        camPos + LocalUnit * (startPos + new Vec2(length + 1, height + 1)), Color.Black * 0.3f, 1.005f); //Main reload line


                                    Graphics.DrawStringOutline("Reloading", camPos + Level.current.camera.size - new Vec2(LocalUnit * 80, LocalUnit * 15), 
                                        Color.OrangeRed, Color.Black, 1f, null, Level.current.camera.height / 320);                                    
                                }
                                Graphics.DrawStringOutline(txt, camPos + Level.current.camera.size - new Vec2(LocalUnit * 80, LocalUnit * 10), 
                                    Color.White, Color.Black, 1f, null, Level.current.camera.height / 320);
                            }
                        }
                        if (oper.holdObject2 != null)
                        {
                            if (oper.holdObject2 is GunDev)
                            {
                                GunDev gun = oper.holdObject2 as GunDev;
                                if (gun.canFire)
                                {
                                    string txt = Convert.ToString(gun.ammo) + " / " + Convert.ToString(gun.magazine);
                                    if (gun.game == null)
                                    {
                                        BitmapFont _font = new BitmapFont("smallFont", 8, -1);
                                    }
                                    if (!gun.reloading)
                                    {
                                        Graphics.DrawStringOutline(gun.name, Level.current.camera.position + Level.current.camera.size - new Vec2(LocalUnit * 80, LocalUnit * 15), Color.White, Color.Black, 1f, null, Level.current.camera.height / 320);
                                    }
                                    else
                                    {
                                        float reloadTime = gun.timeToReload;
                                        float saveTime = gun.timeToTacticalReload;
                                        float reload = gun.reload;

                                        Vec2 startPos = new Vec2(255, 187);

                                        float reloadMove = (reloadTime - reload) / reloadTime;
                                        float savereload = saveTime / reloadTime;

                                        float length = 58;
                                        float height = 3;

                                        Graphics.DrawRect(camPos + LocalUnit * startPos,
                                            camPos + LocalUnit * (startPos + new Vec2((1 - savereload) * length, height)), Color.Yellow * 0.6f, 1.01f); //Main reload line

                                        Graphics.DrawRect(camPos + LocalUnit * (startPos + new Vec2((1 - savereload) * length, 0)),
                                            camPos + LocalUnit * (startPos + new Vec2(1 * length, height)), Color.OrangeRed * 0.3f, 1.01f); //tactical reload line

                                        Graphics.DrawRect(camPos + LocalUnit * startPos,
                                            camPos + LocalUnit * (startPos + new Vec2(reloadMove * length, height)), Color.Blue * 0.4f, 1.02f); //reload line

                                        Graphics.DrawRect(camPos + LocalUnit * (startPos + new Vec2(-1, -1)),
                                            camPos + LocalUnit * (startPos + new Vec2(length + 1, height + 1)), Color.Black * 0.3f, 1.005f); //Main reload line


                                        Graphics.DrawStringOutline("Reloading", camPos + Level.current.camera.size - new Vec2(LocalUnit * 80, LocalUnit * 15),
                                            Color.OrangeRed, Color.Black, 1f, null, Level.current.camera.height / 320);
                                    }
                                    Graphics.DrawStringOutline(txt, Level.current.camera.position + Level.current.camera.size - new Vec2(LocalUnit * 80, LocalUnit * 10), Color.White, Color.Black, 1f, null, Level.current.camera.height / 320);
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (oper != null)
                    {
                        if (oper.inventory[5] is Phone)
                        {
                            DrawCamerasInterface();
                        }
                    }
                }
            }
            base.Draw();
        }

        public void OnDrawLayer(Layer pLayer)
        {
            //Graphics.material = new MaterialFrozen(this);
        }
    }
}


