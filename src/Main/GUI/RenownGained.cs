using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class RenownGained : Thing, IDrawToDifferentLayers
    {
        public int amount;
        public string description = " ";
        public string additional = " ";
        public int order = 0;

        public int timer = 240;
        public int baseTime;
        public float currentY;

        public RenownGained() : base()
        {

        }

        public override void Initialize()
        {
            foreach (RenownGained r in Level.current.things[typeof(RenownGained)])
            {
                if (r != this)
                {
                    order = r.order + 1;
                    PlayerStats.exp += (int)(amount * 0.25f);
                }
            }
            foreach (RenownGained r in Level.current.things[typeof(RenownGained)])
            {
                if (r.order == order - 1)
                {
                    currentY = order + r.currentY;
                }
            }
            baseTime = timer;
            base.Initialize();
        }

        public override void Update()
        {
            if (timer > 0 && order < 3)
            {
                timer--;
            }
            if (timer <= 0)
            {
                Level.Remove(this);
            }
            base.Update();
            if(currentY > order)
            {
                currentY -= 0.05f;
            }
        }


        public void OnDrawLayer(Layer pLayer)
        {
            if (pLayer == Layer.Foreground)
            {
                if (Level.current.camera != null)
                {
                    Color c = Color.Yellow;
                    if(amount == 0)
                    {
                        c = Color.White;
                    }
                    if(amount < 0)
                    {
                        c = Color.Red;
                    }

                    if (order < 3)
                    {
                        float Scale = 0.75f;

                        float xMarge = 0;
                        float yMarge = 60;
                        float Height = 16;
                        float Width = 80;
                        float SpacedY = Height + 14;

                        Vec2 camPos = Level.current.camera.position;
                        Vec2 Unit = Level.current.camera.size / new Vec2(320, 180);

                        float xOutAnimation = 1f;
                        int animationLenght = 15;

                        if (timer > baseTime - animationLenght)
                        {
                            xOutAnimation = 1f - ((float)timer - (baseTime - animationLenght)) / animationLenght;
                        }
                        else if (timer < animationLenght)
                        {
                            xOutAnimation = 1f - (animationLenght - (float)timer) / animationLenght;
                        }
                        //Frame
                        Graphics.DrawRect(camPos + new Vec2(xMarge, yMarge + (SpacedY * currentY) * Scale) * Unit,
                            camPos + new Vec2(xMarge + Width * xOutAnimation * Scale, yMarge + (Height + SpacedY * currentY) * Scale) * Unit, Color.Black * 0.8f, 0.45f, true);
                        //Tip
                        Graphics.DrawRect(camPos + new Vec2(xMarge + Width * xOutAnimation * Scale - 1, yMarge + (SpacedY * currentY) * Scale) * Unit,
                            camPos + new Vec2(xMarge + Width * xOutAnimation * Scale, yMarge + (Height + SpacedY * currentY) * Scale) * Unit, c * 0.8f, 0.46f, true);

                        SpriteMap _renown = new SpriteMap(GetPath("Sprites/GUI/Renown.png"), 32, 32);
                        _renown.CenterOrigin();
                        _renown.scale = new Vec2(0.5f, 0.5f) * Scale;

                        if (xOutAnimation > 0.5f)
                        {
                            //Renown picture
                            _renown.alpha *= xOutAnimation;
                            Graphics.Draw(_renown, camPos.x + (xMarge + (8 * Scale)) * Unit.x, camPos.y + (yMarge + (2 + 30 * currentY) * Scale) * Unit.y, 0.56f);
                            //Renown amount
                            Graphics.DrawStringOutline(Convert.ToString(amount), camPos + new Vec2(xMarge + 24 * Scale, yMarge + (SpacedY * 0.067f + SpacedY * currentY) * Scale) * Unit,
                                c * xOutAnimation, Color.Black * xOutAnimation, 0.56f, null, 1f * Unit.x * Scale);

                            //Description
                            Graphics.DrawStringOutline(description, camPos + new Vec2(xMarge + 4 * Scale, yMarge + (SpacedY * 0.367f + SpacedY * currentY) * Scale) * Unit,
                                Color.White * xOutAnimation, Color.Black * xOutAnimation, 0.56f, null, 0.5f * Unit.x * Scale);
                            //Additional
                            Graphics.DrawStringOutline(additional, camPos + new Vec2(xMarge + 4 * Scale, yMarge + (SpacedY * 0.5f + SpacedY * currentY) * Scale) * Unit,
                                Color.White * xOutAnimation, Color.Black * xOutAnimation, 0.56f, null, 0.25f * Unit.x * Scale);
                        }
                    }
                    if (order > 0)
                    {
                        bool prevDeleted = true;
                        foreach (RenownGained r in Level.current.things[typeof(RenownGained)])
                        {
                            if (r != this)
                            {
                                if (r.order == order - 1)
                                {
                                    prevDeleted = false;
                                }
                                if(r.order == order)
                                {
                                    if(r.GetHashCode() > GetHashCode())
                                    {
                                        order++;
                                    }
                                }
                            }
                        }
                        if (prevDeleted)
                        {
                            order--;
                        }
                    }
                }
            }
        }
    }
}
