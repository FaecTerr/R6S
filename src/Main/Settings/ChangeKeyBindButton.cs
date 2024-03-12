using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class ChangeKeyBindButton : Thing
    {
        public float targetSize;
        public SpriteMap _sprite;
        public bool selected = false;

        public bool targeted;

        public bool picked;
        public bool alternate;
        public bool locked;
        
        public int keyID;
        public string bind;

        public ChangeKeyBindButton(float xpos, float ypos) : base(xpos, ypos)
        {
            depth = 0.4f;
            layer = Layer.Foreground;

            collisionSize = new Vec2(32, 16);
            collisionOffset = new Vec2(-16, -8);            
        }

        public override void Update()
        {
            if (Mouse.positionScreen.x > topLeft.x && Mouse.positionScreen.x < bottomRight.x && Mouse.positionScreen.y > topLeft.y && Mouse.positionScreen.y < bottomRight.y)
            {
                targeted = true;
                targetSize = 1.2f;

            }
            else
            {
                targetSize = 1f;
                targeted = false;
            }

            collisionSize = new Vec2(32, 16) * scale;
            collisionOffset = new Vec2(-16, -8) * scale;

            if (Mouse.left == InputState.Released)
            {
                if (targeted)
                {
                    if (Level.current is SettingsLevel && targetSize > 0.6f)
                    {
                        if (!(Level.current as SettingsLevel).changingKeybind)
                        {
                            picked = true;
                            (Level.current as SettingsLevel).changingKeybind = true;
                        }
                    }
                }
            }


            if (Level.current is SettingsLevel)
            {
                if (picked && PlayerStats.keyBindings.Count > keyID && PlayerStats.keyBindingsAlternate.Count > keyID)
                {
                    if (PlayerStats.LastPressedButton() > 0)
                    {
                        //Escape
                        if (PlayerStats.LastPressedButton() == 27)
                        {
                            picked = false;
                            (Level.current as SettingsLevel).changingKeybind = false;
                        }
                        else
                        {
                            if (alternate)
                            {
                                PlayerStats.keyBindingsAlternate[keyID] = (Keys)PlayerStats.LastPressedButton();
                                PlayerStats.ConvertKeysFrom();
                                PlayerStats.Save();
                                picked = false;
                                (Level.current as SettingsLevel).changingKeybind = false;
                            }
                            else
                            {
                                PlayerStats.keyBindings[keyID] = (Keys)PlayerStats.LastPressedButton();
                                PlayerStats.ConvertKeysFrom();
                                PlayerStats.Save();
                                picked = false;
                                (Level.current as SettingsLevel).changingKeybind = false;
                            }
                        }
                    }
                }
            }


            base.Update();
        }

        public override void Draw()
        {
            base.Draw();

            if (picked)
            {
                Graphics.DrawRect(Level.current.camera.position + new Vec2(0, 90), Level.current.camera.position + new Vec2(320, 120), Color.Black * 0.75f, 0.78f);
                Graphics.DrawStringOutline("Press any key to make new bind \n       (Escape to cancel)     ", Level.current.camera.position + new Vec2(40, 96), Color.White, Color.Black, 0.8f);
            }

            SpriteMap _widebutton = new SpriteMap(GetPath("Sprites/Keys.png"), 34, 17);
            _widebutton.CenterOrigin();
            _widebutton.scale = new Vec2(0.8f, 0.8f);

            SpriteMap _button = new SpriteMap(GetPath("Sprites/Keys.png"), 17, 17);
            _button.CenterOrigin();
            _button.scale = new Vec2(0.8f, 0.8f) * scale;

            //Primary bind
            if (!alternate)
            {
                if (PlayerStats.GetSizeOfButton(PlayerStats.keyBindings[keyID]))
                {
                    if (picked)
                    {
                        _widebutton.scale = new Vec2(1f, 1f);
                    }
                    _widebutton.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[keyID]);
                    Graphics.Draw(_widebutton, position.x, position.y, 0.6f);
                }
                else
                {
                    if (picked)
                    {
                        _button.scale = new Vec2(1f, 1f);
                    }
                    _button.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[keyID]);
                    Graphics.Draw(_button, position.x, position.y, 0.6f);
                }
            }
            else
            {


                //Secondary bind
                if (PlayerStats.GetSizeOfButton(PlayerStats.keyBindingsAlternate[keyID]))
                {
                    if (picked)
                    {
                        _widebutton.scale = new Vec2(1f, 1f);
                    }
                    _widebutton.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindingsAlternate[keyID]);
                    Graphics.Draw(_widebutton, position.x, position.y, 0.6f);
                }
                else
                {
                    if (picked)
                    {
                        _button.scale = new Vec2(1f, 1f);
                    }
                    _button.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindingsAlternate[keyID]);
                    Graphics.Draw(_button, position.x, position.y, 0.6f);
                }
            }
        }
    }
}
