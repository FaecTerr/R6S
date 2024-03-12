using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DuckGame.R6S
{
    public class SettingsLevel : Level
    {
        public int xpos;
        public int ypos;

        public Color c = Color.White;

        public Profile p;

        public int quality = 0;

        public float screen = 4;
        public int targetScreen = 0;

        public float moving = -10;

        public SpriteMap _background;
        public SpriteMap _exit;

        public float pos;

        public double h;
        public double s = 1;
        public double v = 1;

        public bool changingKeybind;

        // 320 x 180
               
        public override void Initialize()
        {
            //General
            int step = 0;
            float moveX = 0;
            float moveY = 0;

            Add(new ButtonSetting(20 + moveX, 50 + step * 20 + moveY) { buttonType = "flag", buttonFunction = "Player speed", value = PlayerStats.DebugSpeed ? 1 : 0}); step++;
            Add(new ButtonSetting(20 + moveX, 50 + step * 20 + moveY) { buttonType = "flag", buttonFunction = "Cycle inside camera group", value = PlayerStats.CycleCameras ? 1 : 0 }); step++;
            //Add(new ButtonSetting(20 + moveX, 50 + step * 20 + moveY) { buttonType = "presetedList", buttonFunction = "Drone after Prep", stringValue = "Auto-exit;Semi-Auto;Manual;." }); step++;
            Add(new ButtonSetting(20 + moveX, 50 + step * 20 + moveY) { buttonType = "slider", buttonFunction = "Tab UI scale", valueScale = 100, minValue = 0.25f, accuracy = 2, value = PlayerStats.TabScale}); step++;

            //Display
            step = 0;
            moveX = 320 * 1;
            moveY = 0;

            Add(new ButtonSetting(20 + moveX, 50 + step * 20 + moveY) { buttonType = "slider", buttonFunction = "screenshake", valueScale = 100, accuracy = 2, value = PlayerStats.screenshake }); step++;
            Add(new ButtonSetting(20 + moveX, 50 + step * 20 + moveY) { buttonType = "flag", buttonFunction = "nicknames", value = PlayerStats.shownickname ? 1 : 0 }); step++;
            Add(new ButtonSetting(20 + moveX, 50 + step * 20 + moveY) { buttonType = "flag", buttonFunction = "hide keybind icons", value = PlayerStats.hideKeyBindIcons ? 1 : 0 }); step++;
            
            //Sound
            step = 0;
            moveX = 320 * 2;
            moveY = 0;

            Add(new ButtonSetting(20 + moveX, 50 + step * 20 + moveY) { buttonType = "slider", buttonFunction = "volume", valueScale = 100, accuracy = 2, value = PlayerStats.volume }); step++;

            //Accesibility
            step = 0;
            moveX = 320 * 3;
            moveY = 0;

            Add(new ButtonSetting(20 + moveX, 50 + step * 20 + moveY) { buttonType = "flag", buttonFunction = "aim assist", value = PlayerStats.aimAssist ? 1 : 0 }); step++;

            //Controls
            step = 0;
            moveX = 320 * 4;
            moveY = 0;

            Add(new BindCategory(20 + moveX, 50 + step * 20 + moveY) { bind = "Movement" });
            step += 2;
            //Movement
            Add(new KeyBindButton(20 + moveX, 20 + step * 20 + moveY, 0)); step++;
            Add(new KeyBindButton(20 + moveX, 20 + step * 20 + moveY, 1)); step++;
            Add(new KeyBindButton(20 + moveX, 20 + step * 20 + moveY, 2)); step++;
            Add(new KeyBindButton(20 + moveX, 20 + step * 20 + moveY, 3)); step++;
            Add(new KeyBindButton(20 + moveX, 20 + step * 20 + moveY, 6)); step++;
            Add(new KeyBindButton(20 + moveX, 20 + step * 20 + moveY, 19)); step++;
            
            Add(new BindCategory(20 + moveX, 50 + step * 20 + moveY) { bind = "Inventory" }); step += 2;

            //Inventory
            Add(new KeyBindButton(20 + moveX, 20 + step * 20 + moveY, 7)); step++;
            Add(new KeyBindButton(20 + moveX, 20 + step * 20 + moveY, 8)); step++;
            Add(new KeyBindButton(20 + moveX, 20 + step * 20 + moveY, 9)); step++;
            Add(new KeyBindButton(20 + moveX, 20 + step * 20 + moveY, 10)); step++;
            Add(new KeyBindButton(20 + moveX, 20 + step * 20 + moveY, 11)); step++;
            Add(new KeyBindButton(20 + moveX, 20 + step * 20 + moveY, 12)); step++;
            Add(new KeyBindButton(20 + moveX, 20 + step * 20 + moveY, 22)); step++;
            
            Add(new BindCategory(20 + moveX, 50 + step * 20 + moveY) { bind = "Interactions" }); step += 2;
            //Interactions
            Add(new KeyBindButton(20 + moveX, 20 + step * 20 + moveY, 4)); step++;
            Add(new KeyBindButton(20 + moveX, 20 + step * 20 + moveY, 5)); step++;
            Add(new KeyBindButton(20 + moveX, 20 + step * 20 + moveY, 13)); step++;
            Add(new KeyBindButton(20 + moveX, 20 + step * 20 + moveY, 14)); step++;
            Add(new KeyBindButton(20 + moveX, 20 + step * 20 + moveY, 20)); step++;
            Add(new KeyBindButton(20 + moveX, 20 + step * 20 + moveY, 21)); step++;
            Add(new KeyBindButton(20 + moveX, 20 + step * 20 + moveY, 23)); step++;
            Add(new KeyBindButton(20 + moveX, 20 + step * 20 + moveY, 24)); step++;

            Add(new BindCategory(20 + moveX, 50 + step * 20 + moveY) { bind = "Cameras control" }); step += 2;
            //Cameras controll
            Add(new KeyBindButton(20 + moveX, 20 + step * 20 + moveY, 15)); step++;
            Add(new KeyBindButton(20 + moveX, 20 + step * 20 + moveY, 16)); step++;
            Add(new KeyBindButton(20 + moveX, 20 + step * 20 + moveY, 17)); step++;
            Add(new KeyBindButton(20 + moveX, 20 + step * 20 + moveY, 18)); step++;
            
            base.Initialize();
        }

        public SettingsLevel() : base()
        {
            Layer.Game.fade = 0f;
            Layer.Foreground.fade = 0f;

            _background = new SpriteMap(Mod.GetPath<R6S>("Sprites/GUI/Background.png"), 320, 240);
            _background.depth = 0.5f;

            _exit = new SpriteMap(Mod.GetPath<R6S>("Sprites/GUI/ExitButton.png"), 20, 20);
        }
        public override void Update()
        {
            base.Update();

            //Maths.LerpTowards(screen, targetScreen, Math.Abs(screen - targetScreen) / 3);
            if (Keyboard.Pressed(PlayerStats.keyBindings[2]) || Keyboard.Pressed(PlayerStats.keyBindingsAlternate[2]))
            {
                targetScreen--;
                moving = -10;
            }
            if (Keyboard.Pressed(PlayerStats.keyBindings[3]) || Keyboard.Pressed(PlayerStats.keyBindingsAlternate[3]))
            {
                targetScreen++;
                moving = -10;
            }

            if(targetScreen < 0)
            {
                targetScreen = 0;
            }
            if(targetScreen > 4)
            {
                targetScreen = 4;
            }

            if (Math.Abs(screen - targetScreen) < 0.01f)
            {
                screen = targetScreen;
            }
            else
            {
                screen += (targetScreen - screen) * 0.05f;
            }

            moving += Mouse.scroll * 0.2f;

            if (screen >= 3.5f && screen < 4.5f)
            {
                if (moving > 500)
                {
                    moving = 500;
                }
                if (moving < -10)
                {
                    moving = -10;
                }
                if (!changingKeybind)
                {
                    if (Keyboard.Down(Keys.R) && Keyboard.Down(Keys.LeftAlt))
                    {
                        PlayerStats.ResetKeybindings();
                    }
                }
            }
            else
            {
                changingKeybind = false;
            }

            if(screen >= 2.5f && screen < 3.5)
            {
                if (moving < -10)
                {
                    moving = -10;
                }
                if(moving > -10)
                {
                    moving = -10;
                }
            }

            if (screen >= 1.5f && screen < 2.5)
            {
                if (moving < -10)
                {
                    moving = -10;
                }
                if (moving > -10)
                {
                    moving = -10;
                }
            }

            if (screen >= 0.5f && screen < 1.5)
            {
                if (moving < -10)
                {
                    moving = -10;
                }
                if (moving > -10)
                {
                    moving = -10;
                }
            }

            if (screen >= -0.5f && screen < 0.5)
            {
                if (moving < -10)
                {
                    moving = -10;
                }
                if (moving > -10)
                {
                    moving = -10;
                }
            }

            camera.position = new Vec2(320 * screen, moving);
            
            if (Math.Abs(moving) < 0.01f)
            {
                moving = 0;
            }

            Profile p = Profiles.DefaultPlayer1;

            if (Keyboard.Pressed(Keys.Escape) && !changingKeybind)
            {
                if (screen == 0)
                {
                    current = new TitleScreen();
                }
                else
                {
                    PlayerStats.Save();
                    ReturnToMainMenu();
                }
            }
        }

        public virtual void ReturnToMainMenu()
        {
            if (R6S.upd.mainMenu == null)
            {
                R6S.upd.mainMenu = new MainMenu();
                current = R6S.upd.mainMenu;
            }
            else
            {
                current = R6S.upd.mainMenu;
                (R6S.upd.mainMenu as MainMenu).Load();
            }
        }
       

        public override void PostDrawLayer(Layer layer)
        {
            if (layer == Layer.Foreground)
            {
                Vec2 camSize = new Vec2(current.camera.width, current.camera.width);
                Vec2 realCamSize = new Vec2(current.camera.width, current.camera.height);
                Vec2 camPos = new Vec2(current.camera.position.x, current.camera.position.y);

                SpriteMap _key = new SpriteMap(Mod.GetPath<R6S>("Sprites/keys.png"), 17, 17);
                _key.CenterOrigin();
                _key.frame = PlayerStats.GetFrameOfButton(Keys.Escape);

                Graphics.Draw(_key, 16 + camPos.x, 12 + camPos.y, 1f);
                Graphics.DrawStringOutline("EXIT", camPos + new Vec2(30, 9), Color.White, Color.Black, 1f, null, 1f);

                SpriteMap _cursor = new SpriteMap(Mod.GetPath<R6S>("Sprites/Aim.png"), 17, 17);
                _cursor.CenterOrigin();

                _cursor.position = Mouse.positionScreen;
                _cursor.frame = 31;
                Graphics.Draw(_cursor, _cursor.position.x, _cursor.position.y, 1);
                                                
                if (screen == 4)
                {
                    Graphics.DrawStringOutline("Alt + R to reset to default", camPos + new Vec2(5, 175), Color.White, Color.Black, 0.3f, null, 0.5f);
                }

            }

            if (layer == Layer.Game)
            {
                string text = "";

                text = "General";
                Graphics.DrawStringOutline(text, new Vec2(320 * 0 + 160 - text.Length * 4, -7), Color.White, Color.Black, 0.3f, null);

                text = "Display";
                Graphics.DrawStringOutline(text, new Vec2(320 * 1 + 160 - text.Length * 4, -7), Color.White, Color.Black, 0.3f, null);

                text = "Audio";
                Graphics.DrawStringOutline(text, new Vec2(320 * 2 + 160 - text.Length * 4, -7), Color.White, Color.Black, 0.3f, null);

                text = "Accesibility";
                Graphics.DrawStringOutline(text, new Vec2(320 * 3 + 160 - text.Length * 4, -7), Color.White, Color.Black, 0.3f, null);

                text = "Controls";
                Graphics.DrawStringOutline(text, new Vec2(320 * 4 + 160 - text.Length * 4, -7), Color.White, Color.Black, 0.3f, null);
            }

            if (layer == Layer.Background)
            {
                Graphics.Draw(_background, 0, current.camera.position.x, current.camera.position.y);
            }
            base.PostDrawLayer(layer);

        }
    }
}
