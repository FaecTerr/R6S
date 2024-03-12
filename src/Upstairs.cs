using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuckGame;

namespace DuckGame.R6S
{
    [EditorGroup("Faecterr's|Furniture")]
    public class Upstairs : Thing, IDrawToDifferentLayers
    {
        private SpriteMap _sprite;
        //public int style;
        public EditorProperty<int> style;
        public float cooldown;

        public Upstairs(float xval, float yval) : base(xval, yval)
        {
            center = new Vec2(11f, 19f);
            collisionOffset = new Vec2(-11f, -19f);
            collisionSize = new Vec2(22f, 38f);
            _sprite = new SpriteMap(GetPath("Sprites/Upstairs.png"), 22, 38, false);
            base.graphic = this._sprite;
            depth = -0.8f;
            hugWalls = WallHug.Floor;
            style = new EditorProperty<int>(0, this, 0f, 7f, 1f);
            _editorName = "Stairs";
        }

        public override void Update()
        {
            base.Update();
            if(cooldown > 0f)
            {
                cooldown -= 0.1f;
            }
            _sprite.frame = style;
            Vec2 dir = new Vec2(0f, -1f);
            if (style % 2 == 1)
                dir = new Vec2(0f, 1f);

            Upstairs up = Level.CheckLine<Upstairs>(position + dir * 6f, position + dir * 16000f, this);
            if (up != null)
            {

                foreach (DroneAP y in Level.CheckRectAll<DroneAP>(topLeft, bottomRight))
                {
                    Operators d = y.oper as Operators;
                    if (d != null)
                    {
                        if (up.cooldown <= 0f && y.oper != null)
                        {
                            if (d.controller && d.genericController != null)
                            {
                                if (d.duckOwner.inputProfile.genericController.MapPressed(268435456, false) && d.observing && d.local)
                                {
                                    y.position = new Vec2(up.position.x, up.position.y + 4f);
                                    up.cooldown = 4f;
                                    cooldown = 4f;
                                    y.sleeping = false;
                                }
                            }
                            else
                            {
                                if ((Keyboard.Pressed(PlayerStats.keyBindings[5]) || Keyboard.Pressed(PlayerStats.keyBindingsAlternate[5])) && y.oper.local && y.oper.observing)
                                {
                                    y.position = new Vec2(up.position.x, up.position.y + 4f);
                                    up.cooldown = 4f;
                                    cooldown = 4f;
                                    y.sleeping = false;
                                }
                            }
                        }
                    }
                }
                foreach (TwitchDroneAP y in Level.CheckRectAll<TwitchDroneAP>(topLeft, bottomRight))
                {
                    Operators d = y.oper as Operators;
                    if (d != null)
                    {
                        if (up.cooldown <= 0f && y.oper != null)
                        {
                            if (d.controller && d.genericController != null)
                            {
                                if (d.duckOwner.inputProfile.genericController.MapPressed(268435456, false) && d.observing && d.local)
                                {
                                    y.position = new Vec2(up.position.x, up.position.y + 4f);
                                    up.cooldown = 4f;
                                    cooldown = 4f;
                                    y.sleeping = false;
                                }
                            }
                            else
                            {
                                if ((Keyboard.Pressed(PlayerStats.keyBindings[5]) || Keyboard.Pressed(PlayerStats.keyBindingsAlternate[5])) && y.oper.local && y.oper.observing)
                                {
                                    y.position = new Vec2(up.position.x, up.position.y + 4f);
                                    up.cooldown = 4f;
                                    cooldown = 4f;
                                    y.sleeping = false;
                                }
                            }
                        }
                    }
                }
                foreach (YokaiAP y in Level.CheckRectAll<YokaiAP>(topLeft, bottomRight))
                {
                    Operators d = y.oper as Operators;
                    if (d != null)
                    {
                        if (up.cooldown <= 0f && y.oper != null)
                        {
                            if (d.controller && d.genericController != null)
                            {
                                if (d.duckOwner.inputProfile.genericController.MapPressed(268435456, false) && d.observing && d.local)
                                {
                                    y.position = new Vec2(up.position.x, up.position.y + 4f);
                                    up.cooldown = 4f;
                                    cooldown = 4f;
                                    y.sleeping = false;
                                }
                            }
                            else
                            {
                                if ((Keyboard.Pressed(PlayerStats.keyBindings[5]) || Keyboard.Pressed(PlayerStats.keyBindingsAlternate[5])) && y.oper.local && y.oper.observing)
                                {
                                    y.position = new Vec2(up.position.x, up.position.y + 4f);
                                    up.cooldown = 4f;
                                    cooldown = 4f;
                                    y.sleeping = false;
                                }
                            }
                        }
                    }
                }
                foreach (RateroDroneAP y in Level.CheckRectAll<RateroDroneAP>(topLeft, bottomRight))
                {
                    Operators d = y.oper as Operators;
                    if (d != null)
                    {
                        if (up.cooldown <= 0f && y.oper != null)
                        {
                            if (d.controller && d.genericController != null)
                            {
                                if (d.duckOwner.inputProfile.genericController.MapPressed(268435456, false) && d.observing && d.local)
                                {
                                    y.position = new Vec2(up.position.x, up.position.y + 4f);
                                    up.cooldown = 4f;
                                    cooldown = 4f;
                                    y.sleeping = false;
                                }
                            }
                            else
                            {
                                if ((Keyboard.Pressed(PlayerStats.keyBindings[5]) || Keyboard.Pressed(PlayerStats.keyBindingsAlternate[5])) && y.oper.local && y.oper.observing)
                                {
                                    y.position = new Vec2(up.position.x, up.position.y + 4f);
                                    up.cooldown = 4f;
                                    cooldown = 4f;
                                    y.sleeping = false;
                                }
                            }
                        }
                    }
                }
                foreach (Operators d in Level.CheckRectAll<Operators>(topLeft, bottomRight))
                {
                    if (up.cooldown <= 0f && d.duckOwner != null && d.priorityTaken < 0.5f)
                    {                        
                        if (d.controller && d.genericController != null)
                        {
                            if (d.duckOwner.inputProfile.genericController.MapPressed(268435456, false) && !d.observing && d.local)
                            {
                                d.position = new Vec2(up.position.x, up.position.y + 4f);
                                up.cooldown = 4f;
                                cooldown = 4f; 
                                
                                if (!d.HasEffect("SpawnArmor") && !d.HasEffect("ArmorCooldown"))
                                {
                                    d.effects.Add(new SpawnArmor(2) { team = d.team });
                                    if (d.team == "Att")
                                    {
                                        d.effects.Add(new StairsCooldown(10) { team = "Def" });
                                    }
                                    if (d.team == "Def")
                                    {
                                        d.effects.Add(new StairsCooldown(10) { team = "Att" });
                                    }
                                }
                            }
                        }
                        else
                        {
                            if ((Keyboard.Pressed(PlayerStats.keyBindings[4]) || Keyboard.Pressed(PlayerStats.keyBindingsAlternate[4])) && !d.observing && d.local)
                            {
                                d.position = new Vec2(up.position.x, up.position.y + 4f);
                                up.cooldown = 4f;
                                cooldown = 4f; 
                                
                                if (!d.HasEffect("SpawnArmor") && !d.HasEffect("ArmorCooldown"))
                                {
                                    d.effects.Add(new SpawnArmor(2) { team = d.team });
                                    if (d.team == "Att")
                                    {
                                        d.effects.Add(new StairsCooldown(10) { team = "Def" });
                                    }
                                    if (d.team == "Def")
                                    {
                                        d.effects.Add(new StairsCooldown(10) { team = "Att" });
                                    }
                                }
                            }
                        }
                        d.UpdateLayerID();
                    }
                }
            }

        }
        public override void Draw()
        {
            base.Draw();
            _sprite.frame = style;
        }

        public void OnDrawLayer(Layer pLayer)
        {
            if(pLayer == Layer.Game)
            {
                if (Level.current is Editor) {
                    Vec2 dir = new Vec2(0f, -1f);
                    if (style % 2 == 1)
                        dir = new Vec2(0f, 1f);

                    Upstairs up = Level.CheckLine<Upstairs>(position + dir * 6f, position + dir * 16000f, this);

                    if (up != null)
                    {
                        Graphics.DrawLine(position, up.position, Color.Magenta * 0.5f, 0.99f);

                        float ypos = (position.y - up.position.y) * 0.001f * DateTime.Now.Millisecond;

                        Graphics.DrawRect(position  - new Vec2(-2, -2 + ypos), position - new Vec2(2, 2 + ypos), Color.DarkMagenta, 1f);
                    } 
                }

                foreach (Operators oper in Level.CheckRectAll<Operators>(topLeft, bottomRight))
                {
                    
                    if (oper.local)
                    {
                        if (oper.controller)
                        {
                            Vec2 pos = Level.current.camera.position;
                            Vec2 cameraSize = Level.current.camera.size;
                            Vec2 Unit = cameraSize / new Vec2(320, 180);

                            SpriteMap _button = new SpriteMap(GetPath("Sprites/Keys.png"), 17, 17);
                            _button.CenterOrigin();
                            _button.scale = new Vec2(0.8f * Unit.x, 0.8f * Unit.x);

                            _button.frame = PlayerStats.GetFrameOfButtonGP("LStickUP");
                            Graphics.Draw(_button, position.x, position.y - 16, 0.2f);

                        }
                        else
                        {
                            Vec2 pos = Level.current.camera.position;
                            Vec2 cameraSize = Level.current.camera.size;
                            Vec2 Unit = cameraSize / new Vec2(320, 180);

                            SpriteMap _widebutton = new SpriteMap(GetPath("Sprites/Keys.png"), 34, 17);
                            _widebutton.CenterOrigin();
                            _widebutton.scale = new Vec2(0.8f * Unit.x, 0.8f * Unit.x);

                            SpriteMap _button = new SpriteMap(GetPath("Sprites/Keys.png"), 17, 17);
                            _button.CenterOrigin();
                            _button.scale = new Vec2(0.8f * Unit.x, 0.8f * Unit.x);

                            if (PlayerStats.GetSizeOfButton(PlayerStats.keyBindings[4]))
                            {
                                _widebutton.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[4]);
                                Graphics.Draw(_widebutton, position.x, position.y - 16, 0.2f);
                            }
                            else
                            {
                                _button.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[4]);
                                Graphics.Draw(_button, position.x, position.y - 16, 0.2f);
                            }
                        }
                    }
                }

                foreach (DroneAP d in Level.CheckRectAll<DroneAP>(topLeft, bottomRight))
                {
                    if (d.oper != null)
                    {
                        if (d.oper.local && d.oper.observing)
                        {
                            if (d.oper.controller)
                            {
                                Vec2 pos = Level.current.camera.position;
                                Vec2 cameraSize = Level.current.camera.size;
                                Vec2 Unit = cameraSize / new Vec2(320, 180);

                                SpriteMap _button = new SpriteMap(GetPath("Sprites/Keys.png"), 17, 17);
                                _button.CenterOrigin();
                                _button.scale = new Vec2(0.8f * Unit.x, 0.8f * Unit.x);

                                _button.frame = PlayerStats.GetFrameOfButtonGP("LStickUP");
                                Graphics.Draw(_button, position.x, position.y - 16, 0.2f);

                            }
                            else
                            {
                                Vec2 pos = Level.current.camera.position;
                                Vec2 cameraSize = Level.current.camera.size;
                                Vec2 Unit = cameraSize / new Vec2(320, 180);

                                SpriteMap _widebutton = new SpriteMap(GetPath("Sprites/Keys.png"), 34, 17);
                                _widebutton.CenterOrigin();
                                _widebutton.scale = new Vec2(0.8f * Unit.x, 0.8f * Unit.x);

                                SpriteMap _button = new SpriteMap(GetPath("Sprites/Keys.png"), 17, 17);
                                _button.CenterOrigin();
                                _button.scale = new Vec2(0.8f * Unit.x, 0.8f * Unit.x);

                                if (PlayerStats.GetSizeOfButton(PlayerStats.keyBindings[5]))
                                {
                                    _widebutton.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[5]);
                                    Graphics.Draw(_widebutton, position.x, position.y - 16, 0.2f);
                                }
                                else
                                {
                                    _button.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[5]);
                                    Graphics.Draw(_button, position.x, position.y - 16, 0.2f);
                                }
                            }
                        }
                    }
                }

                foreach (YokaiAP d in Level.CheckRectAll<YokaiAP>(topLeft, bottomRight))
                {
                    if (d.oper != null)
                    {
                        if (d.oper.local && d.oper.observing)
                        {
                            if (d.oper.controller)
                            {
                                Vec2 pos = Level.current.camera.position;
                                Vec2 cameraSize = Level.current.camera.size;
                                Vec2 Unit = cameraSize / new Vec2(320, 180);

                                SpriteMap _button = new SpriteMap(GetPath("Sprites/Keys.png"), 17, 17);
                                _button.CenterOrigin();
                                _button.scale = new Vec2(0.8f * Unit.x, 0.8f * Unit.x);

                                _button.frame = PlayerStats.GetFrameOfButtonGP("LStickUP");
                                Graphics.Draw(_button, position.x, position.y - 16, 0.2f);

                            }
                            else
                            {
                                Vec2 pos = Level.current.camera.position;
                                Vec2 cameraSize = Level.current.camera.size;
                                Vec2 Unit = cameraSize / new Vec2(320, 180);

                                SpriteMap _widebutton = new SpriteMap(GetPath("Sprites/Keys.png"), 34, 17);
                                _widebutton.CenterOrigin();
                                _widebutton.scale = new Vec2(0.8f * Unit.x, 0.8f * Unit.x);

                                SpriteMap _button = new SpriteMap(GetPath("Sprites/Keys.png"), 17, 17);
                                _button.CenterOrigin();
                                _button.scale = new Vec2(0.8f * Unit.x, 0.8f * Unit.x);

                                if (PlayerStats.GetSizeOfButton(PlayerStats.keyBindings[5]))
                                {
                                    _widebutton.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[5]);
                                    Graphics.Draw(_widebutton, position.x, position.y - 16, 0.2f);
                                }
                                else
                                {
                                    _button.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[5]);
                                    Graphics.Draw(_button, position.x, position.y - 16, 0.2f);
                                }
                            }
                        }
                    }
                }

                foreach (TwitchDroneAP d in Level.CheckRectAll<TwitchDroneAP>(topLeft, bottomRight))
                {
                    if (d.oper != null)
                    {
                        if (d.oper.local && d.oper.observing)
                        {
                            if (d.oper.controller)
                            {
                                Vec2 pos = Level.current.camera.position;
                                Vec2 cameraSize = Level.current.camera.size;
                                Vec2 Unit = cameraSize / new Vec2(320, 180);

                                SpriteMap _button = new SpriteMap(GetPath("Sprites/Keys.png"), 17, 17);
                                _button.CenterOrigin();
                                _button.scale = new Vec2(0.8f * Unit.x, 0.8f * Unit.x);

                                _button.frame = PlayerStats.GetFrameOfButtonGP("LStickUP");
                                Graphics.Draw(_button, position.x, position.y - 16, 0.2f);

                            }
                            else
                            {
                                Vec2 pos = Level.current.camera.position;
                                Vec2 cameraSize = Level.current.camera.size;
                                Vec2 Unit = cameraSize / new Vec2(320, 180);

                                SpriteMap _widebutton = new SpriteMap(GetPath("Sprites/Keys.png"), 34, 17);
                                _widebutton.CenterOrigin();
                                _widebutton.scale = new Vec2(0.8f * Unit.x, 0.8f * Unit.x);

                                SpriteMap _button = new SpriteMap(GetPath("Sprites/Keys.png"), 17, 17);
                                _button.CenterOrigin();
                                _button.scale = new Vec2(0.8f * Unit.x, 0.8f * Unit.x);

                                if (PlayerStats.GetSizeOfButton(PlayerStats.keyBindings[5]))
                                {
                                    _widebutton.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[5]);
                                    Graphics.Draw(_widebutton, position.x, position.y - 16, 0.2f);
                                }
                                else
                                {
                                    _button.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[5]);
                                    Graphics.Draw(_button, position.x, position.y - 16, 0.2f);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
