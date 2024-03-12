using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class NewsPageButton : Thing, IDrawToDifferentLayers
    {
        public float targetSize;

        public bool selected = false;

        public NewsPageButton(float xpos, float ypos) : base(xpos, ypos)
        {
            center = new Vec2(64f, 36f);
            layer = Layer.Foreground;
            depth = 0.6f;
            collisionSize = new Vec2(128, 72) * scale;
            collisionOffset = new Vec2(-64f, -36f) * scale;
        }

        public void OnDrawLayer(Layer pLayer)
        {
            if (pLayer == Layer.Foreground)
            {

            }
        }

        public override void Update()
        {
            if (Mouse.positionScreen.x > topLeft.x && Mouse.positionScreen.x < bottomRight.x && Mouse.positionScreen.y > topLeft.y && Mouse.positionScreen.y < bottomRight.y)
            {
                targetSize = 1.2f;
                selected = true;
            }
            else
            {
                targetSize = 1f;
                selected = false;
            }

            if (Level.current != null)
            {
                if (Level.current is MainMenu)
                {
                    if (selected && Mouse.left == InputState.Pressed)
                    {
                        Level.Add(new SoundSource(position.x, position.y, 320, "SFX/UI/UIClick.wav", "J"));
                        if (Level.current is MainMenu)
                        {
                            if ((Level.current as MainMenu).page == 0)
                            {
                                Level.current = new ShopLevel() { screen = 3 };
                            }
                        }
                        if (Level.current is MainMenu)
                        {
                            if ((Level.current as MainMenu).page == 1)
                            {
                                Level.current = new ShopLevel() { screen = 1 };
                            }
                        }
                        if (Level.current is MainMenu)
                        {
                            if ((Level.current as MainMenu).page == 2)
                            {
                                Level.current = new ShopLevel() { screen = 2 };
                            }
                        }
                        if (Level.current is MainMenu)
                        {
                            if ((Level.current as MainMenu).page == 3)
                            {
                                Level.current = new ShopLevel() { screen = 2 };
                            }
                        }
                        if (Level.current is MainMenu)
                        {
                            if ((Level.current as MainMenu).page == 4)
                            {
                                System.Diagnostics.Process.Start("https://www.ubisoft.com/en-gb/game/rainbow-six/siege");
                            }
                        }
                    }
                }
            }
            base.Update();
        }
    }
}


