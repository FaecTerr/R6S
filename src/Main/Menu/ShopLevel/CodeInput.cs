using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class PromoCodeInput : Thing
    {
        public float targetSize;
        public SpriteMap _sprite;
        public bool selected = false;

        public bool targeted;

        public bool picked;
        public bool locked;

        public int screen;

        public string code;

        public int promoIsRight;

        public SinWave _pulse = 0.02f;

        public PromoCodeInput(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/AlphaPack/PCOde.png"), 24, 24, false);
            graphic = _sprite;
            _sprite.CenterOrigin();
            center = new Vec2(12f, 12f);
            collisionSize = new Vec2(24, 24);
            collisionOffset = new Vec2(-12, -12);
            depth = 0.4f;
            layer = Layer.Foreground;
            _sprite.depth = 0.41f;
        }

        public override void Update()
        {
            if (Mouse.positionScreen.x > topLeft.x && Mouse.positionScreen.x < bottomRight.x && Mouse.positionScreen.y > topLeft.y && Mouse.positionScreen.y < bottomRight.y && graphic.alpha > 0.5f)
            {
                targeted = true;
                if (Mouse.left == InputState.Pressed)
                {
                    picked = true;
                }
            }
            else
            {
                targeted = false;
                if (Mouse.left == InputState.Pressed)
                {
                    picked = false;
                }
            }

            if (picked && code.Length < 22)
            {
                code = Keyboard.keyString;
            }
            else
            {
                code = "";
                Keyboard.keyString = "";
            }


            if (promoIsRight > 0)
            {
                promoIsRight--;
            }


            if(picked && Keyboard.Pressed(Keys.Enter))
            {
                if (code == "GreenDash" && !PlayerStats.usedCodes.Contains(code))
                {
                    PlayerStats.renown += 2000;

                    PlayerStats.usedCodes.Add(code);

                    promoIsRight = 120;
                }

                if (code == "Emil1ano" && !PlayerStats.usedCodes.Contains(code))
                {
                    PlayerStats.renown += 2000;

                    PlayerStats.usedCodes.Add(code);

                    promoIsRight = 120;
                }

                if (code == "Gachi" && !PlayerStats.usedCodes.Contains(code))
                {
                    PlayerStats.renown += 2000;

                    PlayerStats.usedCodes.Add(code);

                    promoIsRight = 120;
                }

                if (code == "Jotaro" && !PlayerStats.usedCodes.Contains(code))
                {
                    PlayerStats.renown += 10000;
                    PlayerStats.materials += 500;

                    PlayerStats.usedCodes.Add(code);


                    promoIsRight = 120;
                }

                if (code == "Dio" && !PlayerStats.usedCodes.Contains(code))
                {
                    PlayerStats.renown += 5000;
                    PlayerStats.materials += 250;

                    PlayerStats.usedCodes.Add(code);


                    promoIsRight = 120;
                }

                if (code == "YaNeDolbaeeb" && !PlayerStats.usedCodes.Contains(code))
                {
                    PlayerStats.renown += 1000;

                    PlayerStats.usedCodes.Add(code);


                    promoIsRight = 120;
                }

                if (code == "FaecTerr.Dev" && !PlayerStats.usedCodes.Contains(code))
                {
                    PlayerStats.renown += 1000;

                    PlayerStats.usedCodes.Add(code);


                    promoIsRight = 120;
                }

                if (code == "/give renown 1000" && !PlayerStats.usedCodes.Contains(code))
                {
                    PlayerStats.renown += 1000;
                    PlayerStats.usedCodes.Add(code);

                    promoIsRight = 120;
                }

                if (code == "SCP SOSAT" && !PlayerStats.usedCodes.Contains(code))
                {
                    PlayerStats.materials += 100;
                    PlayerStats.usedCodes.Add(code);

                    promoIsRight = 120;
                }

                if (code == "Padlo Toxit" && !PlayerStats.usedCodes.Contains(code))
                {
                    PlayerStats.materials += 100;
                    PlayerStats.usedCodes.Add(code);

                    promoIsRight = 120;
                }

                if (code == "Troyan" && !PlayerStats.usedCodes.Contains(code))
                {
                    PlayerStats.materials += 100;
                    PlayerStats.usedCodes.Add(code);

                    promoIsRight = 120;
                }

                if (code == "Девочка с каре" && !PlayerStats.usedCodes.Contains(code))
                {
                    PlayerStats.materials += 100;
                    PlayerStats.usedCodes.Add(code);

                    promoIsRight = 120;
                }

                if (code == "Sweet Dreams" && !PlayerStats.usedCodes.Contains(code))
                {
                    PlayerStats.materials += 100;
                    PlayerStats.usedCodes.Add(code);

                    promoIsRight = 120;
                }

                if (code == "Wakaba Hiiro" && !PlayerStats.usedCodes.Contains(code))
                {
                    PlayerStats.renown += 5000;
                    PlayerStats.materials += 200;
                    PlayerStats.usedCodes.Add(code);

                    promoIsRight = 120;
                }

                if (code == "Welcome from R6S" && !PlayerStats.usedCodes.Contains(code))
                {
                    PlayerStats.renown += 5000;
                    PlayerStats.materials += 200;
                    PlayerStats.usedCodes.Add(code);

                    promoIsRight = 120;
                }

                if (code == "Ubishit" && !PlayerStats.usedCodes.Contains(code))
                {
                    PlayerStats.materials += 350;
                    PlayerStats.usedCodes.Add(code);

                    promoIsRight = 120;
                }

                if (code == "Игорь Летов" && !PlayerStats.usedCodes.Contains(code))
                {
                    PlayerStats.renown += 5000;
                    PlayerStats.materials += 200;
                    PlayerStats.usedCodes.Add(code);

                    promoIsRight = 120;
                }

                /*if (code == "05.08 Troyan" && !PlayerStats.usedCodes.Contains(code))
                {
                    PlayerStats.renown += 150000;
                    PlayerStats.materials += 200;
                    PlayerStats.usedCodes.Add(code);

                    promoIsRight = 120;
                }*/

                PlayerStats.Save();
                code = "";
                Keyboard.keyString = "";
            }


            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
            if (picked)
            {
                SpriteMap _coupon = new SpriteMap(GetPath("Sprites/AlphaPack/PCOde.png"), 24, 24);
                _coupon.frame = 1;
                _coupon.CenterOrigin();

                Graphics.DrawRect(position + new Vec2(20, -6), position + new Vec2(200, 12), Color.Black * (0.3f + 0.7f * (promoIsRight / 120)), 0.93f);
                Graphics.DrawString(code, position + new Vec2(24, 0), Color.White, 1f);

                if(promoIsRight > 0)
                {
                    _coupon.angle = _pulse * 0.15f;
                    Graphics.Draw(_coupon, 1, position.x + 230, position.y, 2f, 2f);
                    Graphics.DrawString("не забывай кто устроил этот подгон", position + new Vec2(48, 14), Color.White, 1f, null, 0.2f);
                }
            }
        }
    }
}
