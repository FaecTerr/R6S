using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Devices|IHUD")]
    public class PulseScanner : Device, IDrawToDifferentLayers
    {
        public List<Operators> devices = new List<Operators>();
        public float radius1;
        public float radius2 = 6f;

        public List<Operators> scannedOpers = new List<Operators>();

        public int usings = 4;

        public GamemodeScripter gm;

        public PulseScanner(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/PulseScanner.png"), 16, 16, false);
            graphic = _sprite;
            center = new Vec2(8f, 8f);
            collisionOffset = new Vec2(-8f, -8f);
            collisionSize = new Vec2(16f, 16f);
            weight = 0.9f;
            placeable = false;
            breakable = false;
            zeroSpeed = false;
            scannable = true;

            index = 16;
        }

        public override void Update()
        {
            base.Update();

            if (gm == null)
            {
                if (Level.current is Editor)
                {
                    foreach (GamemodeScripter g in Level.current.things[typeof(GamemodeScripter)])
                    {
                        gm = g;
                    }
                }
            }

            if (owner == null)
            {
                frame = 0;
            }
            if (oper != null)
            {
                if (oper.holdObject == this && !jammed)
                {
                    if (!oper.controller)
                    {
                        frame = 1;
                        Vec2 mousePos = Mouse.positionScreen;
                        foreach (Operators d in Level.CheckRectAll<Operators>(mousePos - new Vec2(30, 8), mousePos + new Vec2(30, 10)))
                        {
                            if (mainDevice == null && oper.local && oper.team != team && usings > 0 && !scannedOpers.Contains(d))
                            {
                                scannedOpers.Add(d);
                                usings--;
                                PlayerStats.renown += 25;
                                PlayerStats.Save();
                                Level.Add(new RenownGained() { description = "Cardiac sensor", amount = 25 });
                            }
                            devices.Add(d);
                        }
                    }
                    else
                    {
                        if (gm != null)
                        {
                            frame = 1;
                            foreach (Operators d in Level.CheckRectAll<Operators>(gm.padMousePositionScreen - new Vec2(30, 8), gm.padMousePositionScreen + new Vec2(30, 10)))
                            {
                                if (mainDevice == null && oper.local && oper.team != team && usings > 0 && !scannedOpers.Contains(d))
                                {
                                    scannedOpers.Add(d);
                                    usings--;
                                    PlayerStats.renown += 25;
                                    PlayerStats.Save();
                                    Level.Add(new RenownGained() { description = "Cardiac sensor", amount = 25 });
                                }
                                devices.Add(d);
                            }
                        }
                    }

                    if(scannedOpers.Count > 0)
                    {
                        if (radius1 == 0)
                        {
                            Level.Add(new SoundSource(position.x, position.y, 80, "SFX/Devices/CardiacSensor.wav", "J"));
                            DuckNetwork.SendToEveryone(new NMSoundSource(position, 80, "SFX/Devices/CardiacSensor.wav", "J"));
                        }
                    }
                }
                if(oper.holdObject != this || jammed)
                {
                    devices.Clear();
                }
            }
        }
        public override void OnDrawLayer(Layer layer)
        {
            if (layer == Layer.Foreground)
            {
                if (oper != null && oper.local)
                {
                    if (!oper.controller)
                    {
                        SpriteMap _scanner = new SpriteMap(GetPath("Sprites/Devices/PulseD.png"), 48, 32, false);
                        Vec2 mousePos = Mouse.positionScreen;
                        _scanner.scale = new Vec2(2, 2);
                        _scanner.CenterOrigin();
                        Graphics.Draw(_scanner, mousePos.x, mousePos.y);
                    }
                    else
                    {
                        if (gm != null)
                        {
                            SpriteMap _scanner = new SpriteMap(GetPath("Sprites/Devices/PulseD.png"), 48, 32, false);
                            Vec2 mousePos = gm.padMousePositionScreen;
                            _scanner.scale = new Vec2(2, 2);
                            _scanner.CenterOrigin();
                            Graphics.Draw(_scanner, mousePos.x, mousePos.y);
                        }
                    }
                }
                base.OnDrawLayer(Layer.Foreground);
            }
            if(layer == Layer.Game)
            {
                if (oper != null && oper.local)
                {
                    foreach (Operators d in devices)
                    {
                        if (d.team != team && !jammed)
                        {
                            Graphics.DrawCircle(d.position, radius1, Color.Red, 2f, 0.5f, 32);
                            Graphics.DrawCircle(d.position, radius2, Color.OrangeRed, 2f, 0.5f, 32);
                        }
                    }
                    radius1 += 0.25f;
                    radius2 += 0.25f;
                    if (radius1 >= 12f)
                    {
                        radius1 = 0f;
                    }
                    if (radius2 >= 12f)
                    {
                        radius2 = 0f;
                    }
                }
                base.OnDrawLayer(Layer.Game);
            }
        }
    }
}
