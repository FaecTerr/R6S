using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class JackalTracker : Device
    {
        public bool enabled;

        public bool disabled;

        OperTrack track;

        public float scanning;
        public float scanTime = 4;

        public float level1 = 30;
        public float level2 = 55;
        public float level3 = 75;

        public JackalTracker(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/JackalTracker.png"), 32, 32, false);
            graphic = _sprite;
            
            _sprite.frame = 0;
            center = new Vec2(16f, 16f);

            index = 25;

            UsageCount = 3;

            team = "Att";

            ShowCounter = true;
        }

        public override void Update()
        {
            base.Update();
            if (oper != null)
            {
                enabled = !enabled;

                if (enabled)
                {
                    Level.Add(new SoundSource(position.x, position.y, 80, "SFX/Devices/TrackerActivate.wav", "J"));
                    DuckNetwork.SendToEveryone(new NMSoundSource(position, 80, "SFX/Devices/TrackerActivate.wav", "J"));
                }
                else
                {
                    Level.Add(new SoundSource(position.x, position.y, 80, "SFX/Devices/TrackerDeactivate.wav", "J"));
                    DuckNetwork.SendToEveryone(new NMSoundSource(position, 80, "SFX/Devices/TrackerDeactivate.wav", "J"));
                }

                if(user == null)
                {
                    user = oper;
                }
                oper.ChangeWeapon(10, 1);
                
            }

            if(UsageCount <= 0 && !disabled)
            {
                disabled = true;
                UsageCount = 1;
                ShowCounter = false;
            }

            if(user != null)
            {
                if (!user.isDead)
                {
                    if (enabled)
                    {
                        if(Level.CheckPoint<OperTrack>(user.aim) != null)
                        {
                            if(Level.CheckPoint<OperTrack>(user.aim) != track)
                            {
                                scanning = 0;
                            }
                            track = Level.CheckPoint<OperTrack>(user.aim);
                        }
                        else
                        {
                            scanning = 0;
                            track = null;
                        }

                        if(track != null && user.priorityTaken <= 0.5f && UsageCount > 0 && !disabled && !user.HasEffect("Jammed"))
                        {
                            if (Level.CheckLine<Block>(user.position, track.position) == null)
                            {
                                scanning += 0.01666666f;
                                if (scanning >= scanTime)
                                {
                                    scanning = 0;

                                    int lvl = 4;
                                    if (track.lifetime > level3)
                                    {
                                        lvl = 4;
                                    }
                                    else if (track.lifetime > level2)
                                    {
                                        lvl = 3;
                                    }
                                    else if (track.lifetime > level1)
                                    {
                                        lvl = 2;
                                    }
                                    else
                                    {
                                        lvl = 1;
                                    }
                                    Operators trackOwn = null;
                                    foreach (Operators op in Level.current.things[typeof(Operators)])
                                    {
                                        if(op.netIndex == track.netIndex)
                                        {
                                            trackOwn = op;
                                        }
                                    }
                                    if (trackOwn != null)
                                    {
                                        trackOwn.effects.Add(new SpottedEffect() { timer = 2.5f * lvl, maxTimer = 2.5f * lvl });
                                        DuckNetwork.SendToEveryone(new NMTrackedByJackal(track.own.netIndex, lvl));

                                        UsageCount--;

                                        foreach (OperTrack tr in Level.current.things[typeof(OperTrack)])
                                        {
                                            if (tr != track)
                                            {
                                                if (tr.own != track.own)
                                                {
                                                    Level.Remove(tr);
                                                }
                                            }
                                        }
                                    }
                                    Level.Remove(track);
                                }
                            }
                            else
                            {
                                scanning = 0;
                            }
                        }
                        else
                        {
                            scanning = 0;
                        }
                    }
                }
                else
                {
                    if (enabled)
                    {
                        enabled = false;
                    }
                }
            }

            foreach (Operators op in Level.current.things[typeof(Operators)])
            {
                if(op.runFrames == 1 && op.team != team && !op.silentStep && oper != null && oper.local)
                {
                    Level.Add(new OperTrack(op.position.x, op.position.y) { own = op, netIndex = op.netIndex });
                }
            }
        }

        public override void OnDrawLayer(Layer layer)
        {
            if(layer == Layer.Foreground)
            {
                if(enabled && user != null)
                {
                    if (!user.observing && user.local)
                    {
                        Vec2 Unit = Level.current.camera.size / new Vec2(320, 180);
                        Vec2 pos = Level.current.camera.position;
                        Vec2 cameraSize = Level.current.camera.size;

                        SpriteMap _status = new SpriteMap(GetPath("Sprites/TrackVision.png"), 64, 36, false);

                        _status.CenterOrigin();
                        _status.scale = new Vec2(Level.current.camera.size.x / 63, Level.current.camera.size.y / 35);
                        _status.alpha = 0.75f;

                        Graphics.Draw(_status, pos.x + cameraSize.x / 2, pos.y + cameraSize.y / 2);

                        foreach (OperTrack tr in Level.current.things[typeof(OperTrack)])
                        {
                            Color c = Color.PaleVioletRed;
                            if(tr.lifetime > level3)
                            {
                                c = Color.PaleVioletRed;
                            }
                            else if(tr.lifetime > level2)
                            {
                                c = Color.Yellow;
                            }
                            else if (tr.lifetime > level1)
                            {
                                c = Color.DarkSeaGreen;
                            }
                            else
                            {
                                c = Color.LightBlue;
                            }

                            if (tr.grounded && Level.CheckLine<Block>(user.position, tr.position) == null)
                            {
                                Graphics.DrawRect(tr.position + new Vec2(-3, -1), tr.position + new Vec2(3, 1), c, 0.1f, true);
                            }
                        }

                        if(track != null)
                        {
                            SpriteMap _cd = new SpriteMap(GetPath("Sprites/whiteDot.png"), 1, 1);
                            _cd.scale = new Vec2(0.6f, 2.6f) * Unit;
                            _cd.CenterOrigin();

                            if (scanning > 0)
                            {
                                for (int k = 0; k < (scanning / scanTime) * 100; k++)
                                {
                                    float num = 3.6f * k - 90;
                                    float ang = Maths.DegToRad(num);
                                    _cd.angleDegrees = num;
                                    _cd.color = Color.White;
                                    if (track.lifetime > level3)
                                    {
                                        _cd.color = Color.PaleVioletRed;
                                    }
                                    else if (track.lifetime > level2)
                                    {
                                        _cd.color = Color.Yellow;
                                    }
                                    else if (track.lifetime > level1)
                                    {
                                        _cd.color = Color.DarkSeaGreen;
                                    }
                                    else
                                    {
                                        _cd.color = Color.LightBlue;
                                    }
                                    Graphics.Draw(_cd, track.position.x + 8f * (float)Math.Cos(ang) * Unit.x, track.position.y + 8f * (float)Math.Sin(ang) * Unit.x, 0.5f);
                                }
                            }
                        }

                        /*if(scanning > 0)
                        {
                            Graphics.DrawLine(pos + new Vec2(cameraSize.x / 320 * 60, cameraSize.y / 180 * 120), pos + new Vec2(cameraSize.x / 320 * (60 + 200 * (scanning / scanTime)), cameraSize.y / 180 * 120), Color.White, 1f, 1f);
                            Graphics.DrawLine(pos + new Vec2(cameraSize.x / 320 * 60, cameraSize.y / 180 * 120), pos + new Vec2(cameraSize.x / 320 * (60 + 200), cameraSize.y / 180 * 120), Color.Gray, 2f, 0.98f);
                        }*/

                        if (user.HasEffect("Jammed"))
                        {
                            Vec2 Pos = Level.current.camera.position;
                            SpriteMap _scanner = new SpriteMap(GetPath("Sprites/Devices/JammedLocation.png"), 32, 32, false);
                            _scanner.scale = Level.current.camera.size / new Vec2(22, 22);
                            _scanner.CenterOrigin();
                            _scanner.frame = Rando.Int(2);
                            _scanner.alpha = 1f;
                            Graphics.Draw(_scanner, Pos.x + Level.current.camera.width / 2, Pos.y + Level.current.camera.height / 2);
                        }
                    }
                }
            }
            base.OnDrawLayer(layer);
        }
    }
}
