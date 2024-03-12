using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Scripting")]
    public class DialogueEditor : Thing, IDrawToDifferentLayers
    {
        public EditorProperty<int> order;
        public EditorProperty<string> text;

        public EditorProperty<bool> trigger;
        public EditorProperty<int> Scale;

        public EditorProperty<float> ShowTime;
        public EditorProperty<bool> repeatable;

        public EditorProperty<int> Speaker;

        public bool isPlayed;


        private int currentOrder;
        private float time = -1;
        private bool triggered;

        public DialogueEditor()
        {
            collisionSize = new Vec2(8, 8);
            collisionOffset = new Vec2(-4, -4);

            order = new EditorProperty<int>(0, this, 0, 99, 1);
            text = new EditorProperty<string>("");

            trigger = new EditorProperty<bool>(false);
            Scale = new EditorProperty<int>(2, this, 4, 80, 2);

            ShowTime = new EditorProperty<float>(2, this, 0f, 10, 0.1f);
            repeatable = new EditorProperty<bool>(false);

            Speaker = new EditorProperty<int>(0, null, 0, 1, 1);
        }



        public override void Update()
        {
            if (time == -1 && !(Level.current is Editor) && triggered)
            {
                time = ShowTime;
            }

            foreach (DialogueEditor d in Level.current.things[typeof(DialogueEditor)])
            {
                if(d.order > currentOrder && d.isPlayed && d != this)
                {
                    currentOrder = d.order;
                }
            }

            if (repeatable)
            {
                if (ShowTime <= 0)
                {
                    triggered = false;
                    isPlayed = false;
                }
                else
                {
                    if (isPlayed)
                    {
                        triggered = false;
                        isPlayed = false;
                    }
                }
            }

            if ((order == currentOrder && !isPlayed && !trigger) || (trigger && Level.CheckRect<Operators>(topLeft + new Vec2(-8, -8) * Scale, bottomRight + new Vec2(8, 8) * Scale) != null))
            {
                if (!triggered)
                {
                    triggered = true;
                }
            }


            base.Update();
        }

        public void OnDrawLayer(Layer pLayer)
        {
            if (pLayer == Layer.Foreground)
            {
                if (Level.current is Editor)
                {
                    if (trigger)
                    {
                        Graphics.DrawRect(topLeft + new Vec2(-8, -8) * Scale, bottomRight + new Vec2(8, 8) * Scale, Color.Aqua, 1, false);
                    }
                }
                else
                {
                    if (!triggered)
                    {

                    }
                    else
                    {
                        Vec2 camPos = Level.current.camera.position;
                        Vec2 Unit = Level.current.camera.size / new Vec2(320, 180);

                        //DevConsole.Log(text);

                        string txt = text.value;
                                                
                        float textSize = Unit.x * 0.5f;

                        if (!isPlayed)
                        {
                            Vec2 defPos = new Vec2(80, 130) * Unit;
                            Vec2 size = new Vec2(200, 40) * Unit;

                            SpriteMap _portrait = new SpriteMap(GetPath("Sprites/Tutorial/Portrait.png"), 24, 24);
                            SpriteMap _portraitFrame = new SpriteMap(GetPath("Sprites/Tutorial/DFrame.png"), 32, 32);
                            _portrait.CenterOrigin();
                            _portrait.frame = Speaker;
                            _portraitFrame.CenterOrigin();
                            _portrait.scale = Unit;
                            _portraitFrame.scale = Unit;
                            Graphics.Draw(_portrait, camPos.x + defPos.x + size.y * 0.5f, camPos.y + defPos.y + size.y * 0.5f, 0.56f);
                            Graphics.Draw(_portraitFrame, camPos.x + defPos.x + size.y * 0.5f, camPos.y + defPos.y + size.y * 0.5f, 0.57f);

                            int mLen = 99;
                            for (int i = 0; i < txt.Length; i++)
                            {
                                if (txt[i] == ' ' && mLen > 80)
                                {
                                    if (i > 24)
                                    {
                                        mLen = i;
                                    }
                                }
                                if (i == 40 && mLen > 80)
                                {
                                    mLen = 40;
                                }
                            }
                            mLen = 32;
                            int k = 0;
                            string separatedText = SplitInLines(txt, mLen, out k);
                            string showText = "";
                            //int k = (int)(txt.Length / mLen);
                            int currentLineIndex = 0;
                            int currentCharIndex = 0;

                            bool open = false;
                            int keyID = 0;

                            for (int i = 0; i < separatedText.Length; i++)
                            {
                                char symbol = separatedText[i];

                                switch (symbol)
                                {
                                    case '[':
                                        open = true;
                                        showText += " ";
                                        currentCharIndex++;
                                        break;
                                    case ']':
                                        open = false;
                                        DrawKey(keyID, new Vec2(currentCharIndex * 8, currentLineIndex * 4), ref currentCharIndex, ref showText);
                                        keyID = 0;
                                        break;
                                    case '\n':
                                        currentLineIndex++;
                                        showText += symbol;
                                        currentCharIndex = 0;
                                        break;
                                    default:
                                        if (!open)
                                        {
                                            showText += symbol;
                                            currentCharIndex++;
                                        }
                                        break;
                                }

                                if(open && symbol >= '0' && symbol <= '9')
                                {
                                    keyID *= 10;
                                    keyID += symbol - '0';
                                }
                                  

                                if (separatedText[i] == '\n')
                                {
                                    currentLineIndex++;
                                }
                            }

                            Graphics.DrawStringOutline
                                (
                                showText, 
                                    new Vec2(
                                        camPos.x + defPos.x + size.x * 0.2f,                                    
                                        camPos.y + defPos.y + (6) * Unit.y
                                    ), 
                                    Color.White, Color.Black, 1f, null, textSize
                                );

                            Graphics.DrawRect(camPos + defPos, camPos + defPos + size, Color.Black * 0.6f, 0.5f);
                            Graphics.DrawRect(camPos + defPos, camPos + defPos + new Vec2(size.x, 1), Color.White, 0.55f);
                           
                            time -= 0.01666666f;
                        }
                        
                    }

                    if (time <= 0 && time > -0.9f)
                    {
                        isPlayed = true;
                    }
                }
            }
        }
        void DrawKey(int keyID, Vec2 location, ref int offset, ref string text)
        {
            Vec2 camPos = Level.current.camera.position;
            Vec2 Unit = Level.current.camera.size / new Vec2(320, 180);

            Vec2 defPos = new Vec2(80, 130) * Unit;
            Vec2 size = new Vec2(200, 40) * Unit;

            Unit *= 0.5f;

            SpriteMap _widebutton = new SpriteMap(GetPath("Sprites/Keys.png"), 34, 17);
            _widebutton.scale = new Vec2(0.6f, 0.6f) * Unit;
            SpriteMap _button = new SpriteMap(GetPath("Sprites/Keys.png"), 17, 17);
            _button.scale = new Vec2(0.4f, 0.4f) * Unit;

            if (PlayerStats.GetSizeOfButton(PlayerStats.keyBindings[keyID]))
            {
                _widebutton.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[keyID]);
                _widebutton.scale = new Vec2(0.6f, 0.6f) * Unit;
                Graphics.Draw(_widebutton, 
                    camPos.x + defPos.x + (location.x - 9) * Unit.x + size.x * 0.2f,                    
                    camPos.y + defPos.y + (location.y + 9) * Unit.y, 0.6f);
                _widebutton.scale = new Vec2(0.4f, 0.4f) * Unit;
                offset += 1;
                text += ' ';
            }
            else
            {
                _button.frame = PlayerStats.GetFrameOfButton(PlayerStats.keyBindings[keyID]);
                _button.scale = new Vec2(0.6f, 0.6f) * Unit;
                Graphics.Draw(_button, 
                    camPos.x + defPos.x + (location.x - 9) * Unit.x + size.x * 0.2f,
                    camPos.y + defPos.y + (location.y + 9) * Unit.y, 0.6f);
                _button.scale = new Vec2(0.4f, 0.4f) * Unit;
            }
        }
        public static string SplitInLines(string text, int lineSize, out int lines)
        {
            lines = 0;
            string updatedText = "";
            string[] words = text.Split(' ');
            int charCount = 0;

            for (int i = 0; i < words.Length; i++)
            {
                charCount += words[i].Length;
                if (charCount > lineSize)
                {
                    lines++;
                    updatedText += '\n';
                    charCount = 0;
                }
                updatedText += words[i];
                updatedText += ' ';
            }


            return updatedText;
        }
    }
}
