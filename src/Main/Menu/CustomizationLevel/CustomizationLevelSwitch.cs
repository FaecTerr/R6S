using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class CustomizationSwitch : Thing, IDrawToDifferentLayers
    {
        public float targetSize;

        public bool selected = false;

        public CustomizationSwitch(float xpos, float ypos) : base(xpos, ypos)
        {
            center = new Vec2(16f, 6f);
            layer = Layer.Foreground;
            depth = 0.6f;
            collisionSize = new Vec2(32, 32) * scale;
            collisionOffset = new Vec2(-16f, -26f) * scale;
        }

        public void OnDrawLayer(Layer pLayer)
        {
            if (pLayer == Layer.Foreground)
            {
                SpriteMap _button = new SpriteMap(GetPath("Sprites/ModeSwitch.png"), 32, 12);

                if (Level.current is CustomizationLevel)
                {
                    if((Level.current as CustomizationLevel).screen == 1)
                    {
                        _button.frame = 1;
                    }
                }
                _button.CenterOrigin();

                Graphics.Draw(_button, position.x, position.y);
            }
        }

        public override void Update()
        {
            if (Mouse.positionScreen.x > topLeft.x && Mouse.positionScreen.x < bottomRight.x && Mouse.positionScreen.y > topLeft.y && Mouse.positionScreen.y < bottomRight.y)
            {
                targetSize = 0.6f;
                selected = true;
            }
            else
            {
                targetSize = 0.5f;
                selected = false;
            }

            if (Level.current != null)
            {
                if (Level.current is CustomizationLevel)
                {
                    if (selected && Mouse.left == InputState.Pressed)
                    {
                        Level.Add(new SoundSource(position.x, position.y, 960, "SFX/UI/UIClick.wav", "J"));
                        if((Level.current as CustomizationLevel).screen == 0)
                        {
                            (Level.current as CustomizationLevel).screen = 1;
                        }
                        else if ((Level.current as CustomizationLevel).screen == 1)
                        {
                            (Level.current as CustomizationLevel).screen = 0;
                        }
                    }
                }
            }

            position.y = Level.current.camera.position.y + 166f;
            base.Update();
        }

        public override void Draw()
        {
            Graphics.Draw(new Sprite(GetPath("Sprites/GUI/CustomState.png")), position.x - 9, position.y - 26);
            base.Draw();
        }
    }
}


