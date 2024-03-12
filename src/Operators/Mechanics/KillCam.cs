using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class KillCam : Thing
    {
        SinWave _pulse = 0.05f;
        public Operators oper;
        public float Unit;
        public string killedBy = "[DELETED]";
        public int killerHealth = 100;
        public int killerOperID;

        public float timer = 6f;

        public KillCam(float xpos, float ypos) : base(xpos, ypos)
        {
            layer = Layer.Foreground;
        }

        public override void Update()
        {
            Unit = Level.current.camera.size.x / 320;
            if (Keyboard.Pressed(Keys.Space) || timer <= 0f)
            {
                Level.Remove(this);
            }
            if(timer > 0)
            {
                timer -= 0.01666666f;
            }
            //Level.current.camera.size = new Vec2(320, 320);
            base.Update();
        }

        public override void Draw()
        {
            if (oper != null && killedBy != null)
            {
                SpriteMap _icon = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
                if (oper.opeq != null)
                {
                    _icon.frame = oper.opeq._sprite.frame;
                }
                _icon.CenterOrigin();
                offDir = 1;
                _icon.scale = new Vec2(0.8f, 0.8f) * Unit;

               
                Vec2 camPos = Level.current.camera.position;
                Graphics.DrawRect(camPos - new Vec2(1f, 1f), camPos + Level.current.camera.size + new Vec2(1f, 1f), Color.Black * 0.8f, 0.945f, true, 1);
                Graphics.DrawRect(camPos + new Vec2(15 * Unit, 135 * Unit), camPos + new Vec2(115 * Unit, 170 * Unit), Color.Black * 0.6f, 0.95f);
                Graphics.DrawRect(camPos + new Vec2(15 * Unit, 135 * Unit), camPos + new Vec2(115 * Unit, 140 * Unit), Color.Black * 0.6f, 0.95f);
                Graphics.DrawStringOutline(killedBy, camPos + new Vec2(20 * Unit, 145 * Unit), Color.White, Color.Black, 0.97f, null, Unit * 0.8f);

                Graphics.DrawStringOutline("Press 'SPACE' to skip", camPos + new Vec2(15 * Unit, 120 * Unit), Color.White, Color.Black, 0.97f, null, Unit * 0.8f);

                Graphics.Draw(_icon, camPos.x + 100 * Unit, camPos.y + 150 * Unit, 0.97f);
                
                Graphics.DrawLine(camPos + new Vec2(20 * Unit, 165 * Unit), camPos + new Vec2(20 * Unit + 90 * (killerHealth / 100f) * Unit, 165 * Unit), Color.White, 4f, 0.97f);
            }
        }
    }
}
