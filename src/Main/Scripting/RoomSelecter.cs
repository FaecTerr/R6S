using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    [EditorGroup("Faecterr's|Gamemode")]
    public class RoomRect : Thing, IDrawToDifferentLayers
    {
        public EditorProperty<int> sizex;
        public EditorProperty<int> sizey;
        public EditorProperty<string> name;
        public EditorProperty<bool> outside; 
        public EditorProperty<string> tile;

        bool init;
        float localIsOutside;
        SinWave _pulse = 0.34f;
        bool localINSIDE;
        SpriteMap _tile;
        bool baseboarded = false;
        public RoomRect(float xpos, float ypos) : base(xpos, ypos)
        {
            sizex = new EditorProperty<int>(1, this, 1, 40, 1);
            sizey = new EditorProperty<int>(1, this, 1, 40, 1);
            name = new EditorProperty<string>("1F - Main hall");
            layer = Layer.Foreground;
            outside = new EditorProperty<bool>(false);
            tile = new EditorProperty<string>("");
        }

        public override void Initialize()
        {
            base.Initialize();
            string path = "Sprites/Decorations/";
            baseboarded = false;

            if (tile == "RedRoom")
            {
                path += "Room1";
            }
            if (tile == "OrangeRoom")
            {
                path += "Room2";
            }
            if (tile == "GreenRoom")
            {
                path += "Room3";
                baseboarded = true;
            }
            if (tile == "BlueRoom")
            {
                path += "Room4";
            }
            if (tile == "LowRoom")
            {
                path += "Room5";
            }
            if (tile == "OrangeOrnamentRoom")
            {
                path += "Room6";
            }
            if (tile == "GreenOrnamentRoom")
            {
                path += "Room7";
                baseboarded = true;
            }
            if (tile == "RedStripsRoom")
            {
                path += "Room8";
            }
            if (tile == "WhiteRoom")
            {
                path += "Room9";
            }
            if (tile == "BlueOrnamentRoom")
            {
                path += "Room10";
            }
            if (tile == "RedOrnamentRoom")
            {
                path += "Room11";
            }
            if (tile == "BlueStripsRoom")
            {
                path += "Room12";
            }
            if (tile == "WoodRoom")
            {
                path += "Room13";
            }
            if (tile == "PlainRoom")
            {
                path += "Room14";
            }
            if (tile == "TiledRoom")
            {
                path += "Room15";
            }
            if (tile == "StoneRoom")
            {
                path += "Room16";
            }
            if (tile == "OldTileRoom")
            {
                path += "Room17";
            }
            if (tile == "OldRoom")
            {
                path += "Room18";
            }

            path += ".png";

            _tile = new SpriteMap(GetPath(path), 16, 16);
        }
        public override void Update()
        {
            base.Update();

            if(!(Level.current is Editor))
            {
                localINSIDE = false;
                foreach(Operators op in Level.CheckRectAll<Operators>(position + new Vec2(8, 8), position + new Vec2(16f * sizex + 8, 16f * sizey + 8)))
                {
                    op.lastLocation = name;
                    if (op.team == "Def" && outside)
                    {
                        if (!op.HasEffect("Exposed"))
                        {
                            op.effects.Add(new SpottedEffect() { timer = 1.5f, maxTimer = 1.5f });
                        }
                        else
                        {
                            op.GetEffect("Exposed").timer = 1.5f;
                        }
                    }

                    if(op.local)
                    {
                        localINSIDE = true;
                        if (outside)
                        {
                            Vec2 camPos = Level.current.camera.position;
                            Vec2 Unitt = Level.current.camera.size;

                            //Level.Add(new Snowflake(Unitt.x * 1.05f, Rando.Float(-0.95f, 0.95f) * Unitt.y));

                            if (localIsOutside < 1)
                            {
                                localIsOutside += 0.02f;
                            }
                        }
                        else
                        {

                            if (localIsOutside > 0)
                            {
                                localIsOutside -= 0.02f;
                            }
                        }
                    }                    
                }

                if (!localINSIDE && localIsOutside > 0)
                {
                    localIsOutside -= 0.02f;
                }

                if (outside && !init)
                {
                    init = true;
                    Level.Add(new LocalOutsideBlock(position.x + 8, position.y + 8) { collisionSize = new Vec2(16 * sizex, 16 * sizey)});
                }

                foreach (ObservationThing obs in Level.CheckRectAll<ObservationThing>(position + new Vec2(8, 8), position + new Vec2(16f * sizex + 8, 16f * sizey + 8)))
                {
                    if(obs.team == "Def" && !obs.observableOutside && outside)
                    {
                        //obs.jammed = true;
                        obs.framesBeforeReconnect = 5;
                    }
                }
            }
        }

        public override void Draw()
        {
            base.Draw();
            if(Level.current is Editor)
            {
                Graphics.DrawRect(position + new Vec2(8, 8), position + new Vec2(16f * sizex + 8, 16f * sizey + 8), Color.White * 0.2f, 1f, false, 2f);
                Graphics.DrawDottedRect(position - new Vec2(8f, 8f), position + new Vec2(8f, 8f), Color.White * 0.5f, 1f, 0.5f, 4f);
                if (!outside)
                {
                    Graphics.DrawStringOutline(name, position + new Vec2(name.value.Length * (-4) + sizex * 8, -6f + sizey * 8), Color.White * 0.5f, Color.Black * 0.5f, 1f, null, 1f);
                }
            }
        }

        public void OnDrawLayer(Layer pLayer)
        {
            if(pLayer == Layer.Foreground)
            {
                if (localIsOutside > 0)
                {
                    SpriteMap _snow = new SpriteMap(GetPath("Sprites/ScreenBounds.png"), 64, 36);
                    _snow.color = Color.Aquamarine * (0.6f - (1f - localIsOutside) * 0.6f);
                    _snow.CenterOrigin();

                    Vec2 camPos = Level.current.camera.position;
                    Vec2 Unitt = Level.current.camera.size;
                    _snow.scale = Level.current.camera.size / new Vec2(63 - (_pulse + 1) * 0.5f, 35 - (_pulse + 1) * 0.5f);

                    //Graphics.Draw(_snow, camPos.x + Unitt.x * 0.5f, camPos.y + Unitt.y * 0.5f, 0.7f);
                }
            }
            if (pLayer == Layer.Background)
            {
                if (tile != "" && sizex >= 1 && sizey > 1)
                {
                    _tile.CenterOrigin();
                    int spriteX = 5;
                    int spriteY = 4;
                    int fr = 0;
                    for (int i = 0; i < sizex; i++)
                    {
                        for (int j = 0; j < sizey; j++)
                        {
                            if (j == 0)
                            {
                                if (i == 0)
                                {
                                    fr = 0;
                                }
                                else if (i == sizex - 1)
                                {
                                    fr = spriteX - 2;
                                }
                                else
                                {
                                    fr = i % 2 + 1;
                                }
                            }
                            else if (j == sizey - 1)
                            {
                                if (i == 0)
                                {
                                    fr = (spriteY - 1) * 5;
                                }
                                else if (i == sizex - 1)
                                {
                                    fr = (spriteY - 1) * 5 + spriteX - 2;
                                }
                                else
                                {
                                    fr = (spriteY - 1) * 5 + i % 2 + 1;
                                }
                            }
                            else
                            {
                                if (baseboarded)
                                {
                                    if (j == sizey - 2)
                                    {
                                        if (i == 0)
                                        {
                                            fr = 10;
                                        }
                                        else if (i == sizex - 1)
                                        {
                                            fr = 10 + spriteX - 2;
                                        }
                                        else
                                        {
                                            fr = 10 + i % 2 + 1;
                                        }
                                    }
                                    else
                                    {
                                        if (i == 0)
                                        {
                                            fr = 5;
                                        }
                                        else if (i == sizex - 1)
                                        {
                                            fr = 5 + spriteX - 2;
                                        }
                                        else
                                        {
                                            fr = 5 + i % 2 + 1;
                                        }
                                    }
                                }
                                else
                                {
                                    if (i == 0)
                                    {
                                        fr = 5 * ((j + 1) % 2 + 1);
                                    }
                                    else if (i == sizex - 1)
                                    {
                                        fr = 5 * ((j + 1) % 2 + 1) + spriteX - 2;
                                    }
                                    else
                                    {
                                        fr = 5 * ((j + 1) % 2 + 1) + i % 2 + 1;
                                    }
                                }
                            }

                            _tile.depth = -0.6f;
                            _tile.frame = fr;
                            Graphics.Draw(_tile, position.x + 16 + 16 * i, position.y + 16 + 16 * j);
                        }
                    }
                }
            }
        }
    }
}
