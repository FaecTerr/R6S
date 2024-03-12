using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class KeyBindButton : Thing
    {
        public float targetSize;
        public bool selected = false;

        public bool targeted;

        public bool picked;
        public bool alternate;
        public bool locked;
        
        public int keyID;
        public string bind;

        public KeyBindButton(float xpos, float ypos, int k) : base(xpos, ypos)
        {
            depth = 0.4f;
            keyID = k;
            layer = Layer.Foreground;
        }

        public override void Initialize()
        {
            Level.Add(new ChangeKeyBindButton(position.x + 160, position.y) { keyID = keyID });
            Level.Add(new ChangeKeyBindButton(position.x + 200, position.y) { keyID = keyID, alternate = true });
            base.Initialize();
        }

        public override void Update()
        {          
            if (keyID == 0)
            {
                bind = "Jump";
            }
            if (keyID == 1)
            {
                bind = "Crouch";
            }
            if (keyID == 2)
            {
                bind = "Left";
            }
            if (keyID == 3)
            {
                bind = "Right";
            }
            if (keyID == 4)
            {
                bind = "Interact";
            }
            if (keyID == 5)
            {
                bind = "Special";
            }
            if (keyID == 6)
            {
                bind = "Prone";
            }
            if (keyID == 7)
            {
                bind = "Primary weapon";
            }
            if (keyID == 8)
            {
                bind = "Secondary weapon";
            }
            if (keyID == 9)
            {
                bind = "Primary device";
            }
            if (keyID == 10)
            {
                bind = "Secondary device";
            }
            if (keyID == 11)
            {
                bind = "Phone";
            }
            if (keyID == 12)
            {
                bind = "Drone";
            }
            if (keyID == 13)
            {
                bind = "Fire";
            }
            if (keyID == 14)
            {
                bind = "Aim down sight";
            }
            if (keyID == 15)
            {
                bind = "Previous camera";
            }
            if (keyID == 16)
            {
                bind = "Next camera";
            }
            if (keyID == 17)
            {
                bind = "Previous category";
            }
            if (keyID == 18)
            {
                bind = "Next category";
            }
            if (keyID == 19)
            {
                bind = "Sprint";
            }
            if (keyID == 20)
            {
                bind = "Reload";
            }
            if (keyID == 21)
            {
                bind = "Melee";
            }
            if (keyID == 22)
            {
                bind = "Change gun mode";
            }
            if(keyID == 23)
            {
                bind = "Drop defuser";
            }
            if (keyID == 24)
            {
                bind = "Match info";
            }

            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
            string text = bind;

            BitmapFont f = new BitmapFont("biosFont", 8, -1);
            //Graphics.DrawStringOutline(text, position + new Vec2(0, -4), Color.White, Color.Black, 0.5f, null, 1f);
            if (f != null && text != null)
            {
                f.DrawOutline(text, position + new Vec2(0, -4), Color.White,Color.Black, 0.5f);
            }

            Graphics.DrawRect(position + new Vec2(-4, -8), position + new Vec2(138, 8), Color.Black * 0.3f, 0);
            Graphics.DrawRect(position + new Vec2(142, -8), position + new Vec2(178, 8), Color.Black * 0.3f, 0);
            Graphics.DrawRect(position + new Vec2(182, -8), position + new Vec2(218, 8), Color.Black * 0.3f, 0);            
        }
    }
}
