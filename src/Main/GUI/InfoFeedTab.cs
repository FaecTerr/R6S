using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class InfoFeedTab : Thing, IDrawToDifferentLayers
    {
        public string who;
        public string[] args; //icons in order
        public int typed; //0 - System, 1 - Friendly, 2 - Enemy, 3 - FriendlyFire, 4 - Enemy
        
        public string message1;
        public string message2;

        public int order;
        public int timer = 240;
        public int baseTime;
        public float currentY;

        public InfoFeedTab(string m1, string m2)
        {
            message1 = m1;
            message2 = m2;
        }

        public override void Initialize()
        {
            foreach (InfoFeedTab r in Level.current.things[typeof(InfoFeedTab)])
            {
                if (r != this)
                {
                    order = r.order + 1;
                }
            }
            currentY = order;
            baseTime = timer;

            base.Initialize();
        }

        public override void Update()
        {
            if (timer > 0 && order < 6)
            {
                timer--;
            }
            if (timer <= 0)
            {
                Level.Remove(this);
            }
            base.Update();
            if (currentY > order)
            {
                currentY -= 0.05f;
            }
        }

        public void OnDrawLayer(Layer pLayer)
        {
            if(pLayer == Layer.Foreground)
            {
                if (Level.current.camera != null)
                {
                    Vec2 camPos = Level.current.camera.position;
                    Vec2 camSize = Level.current.camera.size;
                    Vec2 Unit = camSize / new Vec2(320, 180);

                    Vec2 pivot = camPos + camSize * new Vec2(1, 0);

                    Color c1 = Color.Black;
                    Color c2 = Color.Black;
                    if (typed == 1)
                    {
                        c1 = Color.BlueViolet;
                        c2 = Color.IndianRed;
                    }
                    if (typed == 2)
                    {
                        c1 = Color.IndianRed;
                        c2 = Color.BlueViolet;
                    }
                    if(typed == 3)
                    {
                        c1 = Color.BlueViolet;
                        c2 = Color.BlueViolet;
                    }
                    if(typed == 4)
                    {
                        c1 = Color.IndianRed;
                        c2 = Color.IndianRed;
                    }


                    if (order < 6)
                    {
                        float Scale = 0.7f;

                        string text1 = message1;
                        string text2 = message2;

                        float xMarge = 6;
                        float yMarge = 10;
                        float Height = 10;
                        float Width = message1.Length * 8 + message2.Length * 8 + 9 * args.Length + 3;
                        float WidthPart1 = message1.Length * 8 + 1;
                        float WidthPart2 = message2.Length * 8 + 1;
                        float SpacedY = Height + 4;

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

                        //xMarge -= (xOutAnimation) * 90;

                        SpriteMap _feed = new SpriteMap(GetPath("Sprites/InfoFeedIcons.png"), 9, 9);
                        _feed.CenterOrigin();

                        //Part 1
                        if (text1.Length > 0)
                        {
                            Graphics.DrawRect(pivot + new Vec2(-xMarge - Width, yMarge + currentY * SpacedY) * Unit * Scale,
                                pivot + new Vec2(-xMarge - Width + WidthPart1, yMarge + Height + currentY * SpacedY) * Unit * Scale, c1, 0.98f);
                            Graphics.DrawString(text1, pivot + new Vec2(-xMarge - Width + 1, yMarge + currentY * SpacedY + 1) * Unit * Scale, Color.White, 0.995f, null, Scale * Unit.x);

                            //Extra gaps at sides
                            Graphics.DrawRect(pivot + new Vec2(-xMarge - Width - 3, yMarge + currentY * SpacedY) * Unit * Scale,
                                pivot + new Vec2(-xMarge - Width + WidthPart1 + 3, yMarge + Height + currentY * SpacedY) * Unit * Scale, c1, 0.95f);
                        }

                        //Middle
                        if (args.Length > 0)
                        {
                            int i = 0;
                            foreach (string arg in args)
                            {
                                int fr = 0;
                                if (arg == "shot")
                                {
                                    fr = 0;
                                }
                                if (arg == "news")
                                {
                                    fr = 1;
                                }
                                if (arg == "help")
                                {
                                    fr = 2;
                                }
                                if(arg == "c4")
                                {
                                    fr = 3;
                                }
                                if(arg == "impact")
                                {
                                    fr = 4;
                                }
                                if(arg == "fnade")
                                {
                                    fr = 5;
                                }
                                if(arg == "charge")
                                {
                                    fr = 6;
                                }
                                if(arg == "GU")
                                {
                                    fr = 7;
                                }
                                if(arg == "meelee")
                                {
                                    fr = 8;
                                }
                                _feed.depth = 1f;
                                Graphics.Draw(_feed, fr, pivot.x + (-xMarge - Width + WidthPart1 + 4.5f + 9 * i) * Scale * Unit.x,
                                    pivot.y + (yMarge + currentY * SpacedY + 4.5f) * Scale * Unit.y, Scale * Unit.x, Scale * Unit.y, false);

                                i++;
                            }

                            Graphics.DrawRect(pivot + new Vec2(-xMarge - Width + WidthPart1, yMarge + currentY * SpacedY) * Unit * Scale,
                                    pivot + new Vec2(-xMarge - Width + WidthPart1 + args.Length * 9 + 1, yMarge + Height + currentY * SpacedY) * Unit * Scale, Color.Black, 0.98f);

                            //Extra gaps at sides
                            Graphics.DrawRect(pivot + new Vec2(-xMarge - Width + WidthPart1 - 3, yMarge + currentY * SpacedY) * Unit * Scale,
                                pivot + new Vec2(-xMarge - Width + WidthPart1 + args.Length * 9 + 1 + 3, yMarge + Height + currentY * SpacedY) * Unit * Scale, Color.Black, 0.95f);
                        }

                        //Part 2
                        if (text2.Length > 0)
                        {
                            Graphics.DrawRect(pivot + new Vec2(-xMarge - WidthPart2, yMarge + currentY * SpacedY) * Unit * Scale,
                                pivot + new Vec2(-xMarge, yMarge + Height + currentY * SpacedY) * Unit * Scale, c2, 0.98f);
                            Graphics.DrawString(text2, pivot + new Vec2(-xMarge - WidthPart2 + 1, yMarge + currentY * SpacedY + 1) * Unit * Scale, Color.White, 0.995f, null, Scale * Unit.x);

                            //Extra gaps at sides
                            Graphics.DrawRect(pivot + new Vec2(-xMarge - WidthPart2 - 3, yMarge + currentY * SpacedY) * Unit * Scale,
                                pivot + new Vec2(-xMarge + 3, yMarge + Height + currentY * SpacedY) * Unit * Scale, c2, 0.95f);
                        }
                    }
                    if (order > 0)
                    {
                        bool prevDeleted = true;
                        foreach (InfoFeedTab r in Level.current.things[typeof(InfoFeedTab)])
                        {
                            if (r != this)
                            {
                                if (r.order == order - 1)
                                {
                                    prevDeleted = false;
                                }
                                if (r.order == order)
                                {
                                    if (r.GetHashCode() > GetHashCode())
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
