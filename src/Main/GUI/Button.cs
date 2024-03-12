using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class Button : Thing
    {
        public float targetSize;
        public SpriteMap _sprite;
        public SpriteMap _select;
        public bool selected = false;
        public GamemodeScripter gm;

        public bool picked;
        public bool locked;
        public bool hovered;

        public Button(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/DevicesIcons.png"), 24, 24, false);


            center = new Vec2(24, 13.5f);

            layer = Layer.Foreground;

            depth = 0.6f;
        }

        public virtual void OnActivation()
        {
            SFX.Play(GetPath("SFX/UI/UIClick.wav"));
        }

        public virtual void OnHoldActivation()
        {

        }

        public virtual void OnReleaseActivation()
        {

        }

        public virtual void OnHover()
        {
            targetSize = 1.2f;
            selected = true;
        }

        public virtual void NotHovered()
        {
            targetSize = 1f;
            selected = false;
        }

        public override void Update()
        {

            if (gm != null)
            {
                if (gm.localDuck != null)
                {
                    if (gm.controller)
                    {
                        if (gm.padMousePositionScreen.x > topLeft.x && gm.padMousePositionScreen.x < bottomRight.x && gm.padMousePositionScreen.y > topLeft.y && gm.padMousePositionScreen.y < bottomRight.y)
                        {
                            OnHover();
                        }
                        else
                        {
                            NotHovered();
                        }
                    }
                    else
                    {
                        if(Mouse.positionScreen.x > topLeft.x && Mouse.positionScreen.x < bottomRight.x && Mouse.positionScreen.y > topLeft.y && Mouse.positionScreen.y < bottomRight.y)
                        {
                            OnHover();
                            hovered = true;
                        }
                        else
                        {
                            NotHovered();
                            hovered = false;
                        }
                    }
                }
            }
            else
            {
                if (Mouse.positionScreen.x > topLeft.x && Mouse.positionScreen.x < bottomRight.x && Mouse.positionScreen.y > topLeft.y && Mouse.positionScreen.y < bottomRight.y)
                {
                    OnHover();
                    hovered = true;
                }
                else
                {
                    NotHovered();
                    hovered = false;
                }
            }

            foreach (GamemodeScripter g in Level.current.things[typeof(GamemodeScripter)])
            {
                if (gm == null)
                {
                    gm = g;
                }
            }
            if (gm != null)
            {
                if (gm.localDuck != null)
                {
                    if (selected && ((Mouse.left == InputState.Pressed && !gm.controller) || (gm.localDuck.inputProfile.genericController.MapPressed(4096) && gm.controller)))
                    {
                        OnActivation();
                    }
                    if (selected && ((Mouse.left == InputState.Down && !gm.controller) || (gm.localDuck.inputProfile.genericController.MapDown(4096) && gm.controller)))
                    {
                        OnHoldActivation();
                    }
                    if (selected && ((Mouse.left == InputState.Released && !gm.controller) || (gm.localDuck.inputProfile.genericController.MapReleased(4096) && gm.controller)))
                    {
                        OnReleaseActivation();
                    }
                }
            }
            else
            {
                if (selected && Mouse.left == InputState.Pressed)
                {
                    OnActivation();
                }
                if (selected && Mouse.left == InputState.Down)
                {
                    OnHoldActivation();
                }
                if (selected && Mouse.left == InputState.Released)
                {
                    OnReleaseActivation();
                }
            }
            base.Update();
        }
    }
}


