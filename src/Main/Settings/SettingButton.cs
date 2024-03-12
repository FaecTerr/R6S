using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class ChangeSettingButton : Thing
    {
        public float targetSize;
        public SpriteMap _sprite;
        public bool selected = false;

        public bool targeted;

        public bool picked;
        public bool alternate;
        public bool locked;

        public string buttonFunction;
        public string buttonDescription;
        public string buttonType;

        public float value;
        public string stringValue;

        public float maxValue;
        public float minValue;
        public float valueScale;
        public int accuracy = 3;

        public List<string> labels = new List<string>();

        public ChangeSettingButton(float xpos, float ypos) : base(xpos, ypos)
        {
            depth = 0.4f;
            layer = Layer.Foreground;

            collisionSize = new Vec2(32, 16);
            collisionOffset = new Vec2(-16, -8);
        }

        public override void Initialize()
        {
            base.Initialize();
            if (buttonType == "slider")
            {
                collisionSize = new Vec2(100, 16) * scale;
                collisionOffset = new Vec2(-50, -8) * scale;
            }
            if (buttonType == "flag")
            {
                collisionSize = new Vec2(32, 16) * scale;
                collisionOffset = new Vec2(-16, -8) * scale;
            }
            if (buttonType == "presettedList")
            {
                collisionSize = new Vec2(100, 16) * scale;
                collisionOffset = new Vec2(-50, -8) * scale;

                if(stringValue != null)
                {
                    int i = 0;
                    string word = "";
                    while(stringValue.ToCharArray()[i] != '.')
                    {
                        if(stringValue.ToCharArray()[i] != ';')
                        {
                            word += stringValue.ToCharArray()[i];
                        }
                        else
                        {
                            labels.Add(word);
                            word = "";
                        }
                        i++;
                    }
                }
            }
        }

        public override void Update()
        {
            //DevConsole.Log(buttonFunction);
            if (buttonFunction == "screenshake")
            {
                PlayerStats.screenshake = value;
            }
            if (buttonFunction == "nicknames")
            {
                PlayerStats.shownickname = value > 0.5f ? true : false;
            }
            if (buttonFunction == "volume")
            {
                PlayerStats.volume = value;
            }
            if (buttonFunction == "Tab UI scale")
            {
                PlayerStats.TabScale = value;
            }
            if (buttonFunction == "aim assist")
            {
                PlayerStats.aimAssist = value > 0.5f ? true : false;
            }
            if (buttonFunction == "hide keybind icons")
            {
                PlayerStats.hideKeyBindIcons = value > 0.5f ? true : false;
            }
            if(buttonFunction == "Player speed")
            {
                PlayerStats.DebugSpeed = value > 0.5f ? true : false;
            }
            if (buttonFunction == "Cycle inside camera group")
            {
                PlayerStats.CycleCameras = value > 0.5f ? true : false;
            }

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

            if (targeted)
            {
                if(Mouse.left == InputState.Pressed)
                {
                    picked = true;
                    PlayerStats.Save();
                }
                if(buttonType == "flag" && Mouse.left == InputState.Pressed)
                {
                    value = 1 - value;
                }
            }
            if(picked && Mouse.left == InputState.Released)
            {
                picked = false;
                PlayerStats.Save();
            }

            if (buttonType == "slider" && Mouse.left == InputState.Down && (picked || targeted))
            {
                value = (Mouse.positionScreen.x - topLeft.x) / collisionSize.x;
                value = (float)Math.Round(value, accuracy);
                if(value > maxValue)
                {
                    value = maxValue;
                }
                else if(value < minValue)
                {
                    value = minValue;
                }
                PlayerStats.Save();
            }

            if(targeted && Mouse.left == InputState.Pressed && buttonType == "presettedList")
            {
                if(Mouse.position.x > center.x + 16)
                {
                    value++;
                }
                else if(Mouse.position.x < center.x - 16)
                {
                    value--;
                }

                if(value >= labels.Count)
                {
                    value = 0;
                }
                if(value < 0)
                {
                    value = labels.Count - 1;
                }
                PlayerStats.Save();
            }

            base.Update();
        }
        public override void Draw()
        {
            base.Draw();

            if(buttonType == "slider")
            {
                string text = Convert.ToString(value * valueScale);
                BitmapFont f = new BitmapFont("biosFont", 8, -1);
                //Graphics.DrawStringOutline(text, position + new Vec2(0, -4), Color.White, Color.Black, 0.5f, null, 1f);
                if (f != null && text != null)
                {
                    f.DrawOutline(text, position + new Vec2(-16, -4), Color.White, Color.Black, 0.55f);
                }
                Graphics.DrawLine(new Vec2(topLeft.x, position.y), new Vec2(topLeft.x + (bottomRight.x - topLeft.x) * value, position.y), Color.White * 0.8f, 8f, 0.5f);
                Graphics.DrawLine(new Vec2(topLeft.x, position.y), new Vec2(topLeft.x + (bottomRight.x - topLeft.x), position.y), Color.Black * 0.5f, 8f, 0.4f);
            }
            if(buttonType == "flag")
            {
                string text = "";
                BitmapFont f = new BitmapFont("biosFont", 8, -1);
                //Graphics.DrawStringOutline(text, position + new Vec2(0, -4), Color.White, Color.Black, 0.5f, null, 1f);
                if (f != null && text != null)
                {
                    if (value > 0.5f)
                    {
                        text = "ON";
                        f.DrawOutline(text, position + new Vec2(-8, -4), Color.White, Color.Black, 0.5f);
                    }
                    else
                    {
                        text = "OFF";
                        f.DrawOutline(text, position + new Vec2(-11, -4), Color.White, Color.Black, 0.5f);
                    }
                }
            }
            if(buttonType == "presettedList")
            {
                string text = "";
                BitmapFont f = new BitmapFont("biosFont", 8, -1);
                if (f != null && text != null)
                {
                    if (labels.Count > (int)value)
                    {
                        text = labels[(int)value];
                        f.DrawOutline(text, position + new Vec2(text.Length * -3, -4), Color.White, Color.Black, 0.5f);
                    }
                }
            }
        }
    }

    public class ButtonSetting : Thing
    {
        public float targetSize;
        public bool selected = false;

        public bool targeted;

        public bool picked;
        public bool alternate;
        public bool locked;
        
        public string buttonFunction;
        public string buttonDescription;
        public string buttonType;

        public float value = 1;
        public string stringValue;
        public float valueScale = 1;
        public float maxValue = 1;
        public float minValue = 0;
        public int accuracy = 3;

        public ButtonSetting(float xpos, float ypos) : base(xpos, ypos)
        {
            depth = 0.4f;
            layer = Layer.Foreground;
        }

        public override void Initialize()
        {
            if (buttonType == "flag")
            {
                Level.Add(new ChangeSettingButton(position.x + 200f, position.y) { buttonType = buttonType, buttonFunction = buttonFunction, maxValue = maxValue, stringValue = stringValue, value = value, accuracy = accuracy, minValue = minValue, valueScale = valueScale });
            }
            if(buttonType == "slider")
            {
                Level.Add(new ChangeSettingButton(position.x + 196, position.y) { buttonType = buttonType, buttonFunction = buttonFunction, maxValue = maxValue, stringValue = stringValue, value = value, accuracy = accuracy, minValue = minValue, valueScale = valueScale });
            }
            if (buttonType == "presettedList")
            {
                Level.Add(new ChangeSettingButton(position.x + 196, position.y) { buttonType = buttonType, buttonFunction = buttonFunction, maxValue = maxValue, stringValue = stringValue, value = value, accuracy = accuracy, minValue = minValue, valueScale = valueScale });
            }
            base.Initialize();
        }

        public override void Draw()
        {
            base.Draw();

            string text = buttonFunction;
            
            if (buttonType == "flag")
            {
                BitmapFont f = new BitmapFont("biosFont", 8, -1);
                //Graphics.DrawStringOutline(text, position + new Vec2(0, -4), Color.White, Color.Black, 0.5f, null, 1f);
                if (f != null && text != null)
                {
                    f.DrawOutline(text, position + new Vec2(0, -4), Color.White, Color.Black, 0.5f);
                }

                Graphics.DrawRect(position + new Vec2(-4, -8), position + new Vec2(178, 8), Color.Black * 0.3f, 0);
                Graphics.DrawRect(position + new Vec2(182, -8), position + new Vec2(218, 8), Color.Black * 0.3f, 0);
            }
            if (buttonType == "slider")
            {
                BitmapFont f = new BitmapFont("biosFont", 8, -1);
                //Graphics.DrawStringOutline(text, position + new Vec2(0, -4), Color.White, Color.Black, 0.5f, null, 1f);
                if (f != null && text != null)
                {
                    f.DrawOutline(text, position + new Vec2(0, -4), Color.White, Color.Black, 0.5f);
                }

                Graphics.DrawRect(position + new Vec2(-4, -8), position + new Vec2(138, 8), Color.Black * 0.3f, 0);
                Graphics.DrawRect(position + new Vec2(142, -8), position + new Vec2(252, 8), Color.Black * 0.3f, 0);
            }
            if (buttonType == "presettedList")
            {
                BitmapFont f = new BitmapFont("biosFont", 8, -1);
                //Graphics.DrawStringOutline(text, position + new Vec2(0, -4), Color.White, Color.Black, 0.5f, null, 1f);
                if (f != null && text != null)
                {
                    f.DrawOutline(text, position + new Vec2(0, -4), Color.White, Color.Black, 0.5f);
                }

                Graphics.DrawRect(position + new Vec2(-4, -8), position + new Vec2(178, 8), Color.Black * 0.3f, 0);
                Graphics.DrawRect(position + new Vec2(182, -8), position + new Vec2(218, 8), Color.Black * 0.3f, 0);
            }
        }
    }
}
