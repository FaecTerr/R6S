using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Devices|Deployable")]
    public class Jammer : Placeable
    {
        public Jammer(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/Jammer.png"), 16, 10, false);
            graphic = this._sprite;
            center = new Vec2(8f, 5);
            collisionSize = new Vec2(16f, 10f);
            collisionOffset = new Vec2(-8f, -5f);
            setTime = 1f;
            CheckRect = new Vec2(9f, 0f);
            weight = 0.9f;
            thickness = 0.1f;
            placeable = true;
            breakable = true;
            electricible = false;

            index = 10;
            UsageCount = 4;

            DeviceCost = 15;

            destroyedPoints = "Jammer destroyed";

            cantProne = true;
        }

        public override void SetAfterPlace()
        {
            afterPlace = new JammerAP(position.x, position.y);
            base.SetAfterPlace();
        }

    }
    public class JammerAP : Device
    {
        public SpriteMap _jam;
        public Vec2 gPos;

        public float JamAlpha = 0.5f;

        public float radius = 64;

        public  List<Device> jammedDev = new List<Device>();

        public JammerAP(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/Jammer.png"), 16, 10, false);
            _jam = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/JammedLocation.png"), 32, 32, false);

            _jam.scale = new Vec2(4f, 4f);
            _jam.CenterOrigin();
            _jam.AddAnimation("idle", 0.1f, true, new int[] { 0, 1, 2 });
            _jam.SetAnimation("idle");

            graphic = _sprite;
            center = new Vec2(8f, 5);
            collisionSize = new Vec2(16f, 10f);
            collisionOffset = new Vec2(-8f, -5f);

            weight = 0.9f;
            thickness = 0.1f;
            placeable = true;
            breakable = true;
            electricible = false;
            scannable = true;

            DeviceCost = 15;

            destroyedPoints = "Jammer destroyed";

            placings = 10;
        }
        public override void Update()
        {
            if (!jammed)
            {
                if (Rando.Float(1f) > 0.9f)
                {
                    //Level.Add(new JammedParticle(position.x, position.y, 64, alpha));
                }

                foreach (Device d in Level.CheckCircleAll<Device>(position, radius))
                {
                    if ((d.team != team) || (d is DroneAP || d is TwitchDroneAP || d is BreachAP || d is ThermalBreachAP || d is ClusterChargeAP || d is FlashShield || d is ClaymoreAP || d is JackalTracker || d is HackingDevice))
                    {
                        if (!d.jammed && !d.jammResistance && !(d is GunDev))
                        {
                            if (oper != null)
                            {
                                if (oper.local && placings > 10 && !jammedDev.Contains(d))
                                {
                                    jammedDev.Add(d);
                                    PlayerStats.renown += 15;
                                    PlayerStats.Save();
                                    Level.Add(new RenownGained() { description = "Signal disruptor", amount = 15 });
                                }
                            }
                            d.jammed = true;
                            d.jammedFrames = 60;
                        }
                    }
                }
            }
            base.Update();
        }

        public override void OnDrawLayer(Layer layer)
        {
            if(layer == Layer.Foreground)
            {
                foreach (ObservationThing d in Level.CheckCircleAll<ObservationThing>(position, radius * 2))
                {
                    if (d.observing && d.team != team)
                    {
                        if (d.oper != null)
                        {
                            if (d.oper.local)
                            {
                                Vec2 Pos = Level.current.camera.position;
                                SpriteMap _scanner = new SpriteMap(GetPath("Sprites/Devices/JammedLocationRect.png"), 22, 22, false);
                                _scanner.scale = Level.current.camera.size / new Vec2(22, 22);
                                _scanner.CenterOrigin();
                                _scanner.scale *= new Vec2(0.25f, 0.25f);
                                _scanner.frame = Rando.Int(2);
                                _scanner.alpha = 0.4f;
                                Graphics.Draw(_scanner, Pos.x + Level.current.camera.width / 2, Pos.y + Level.current.camera.height / 2);
                            }
                        }
                    }
                }


                foreach (Operators d in Level.CheckCircleAll<Operators>(position, radius))
                {
                    bool exits = false;
                    foreach (Effect f in d.effects)
                    {
                        if (f.name == "Jammed")
                        {
                            exits = true;
                            f.timer = 0.2f;
                        }
                        if(f.name == "Phone called")
                        {
                            f.removeOnEnd = true;
                            f.TimerOut();
                        }
                    }
                    if (exits == false)
                    {
                        d.effects.Add(new JammedEffect());
                    }
                }
            }
            base.OnDrawLayer(layer);
        }

        public override void Draw()
        {

            if (JamAlpha > 0)
            {
                JamAlpha -= 0.002f;
            }
            _jam.alpha = JamAlpha * alpha;
            if (oper != null && oper.local)
            {
                Graphics.Draw(_jam, position.x, position.y);
            }

            if (DevConsole.showCollision)
            {
                Graphics.DrawCircle(position, 64, Color.MediumPurple, 1f, 1f, 32);
            }

            base.Draw();
        }
    }
}
