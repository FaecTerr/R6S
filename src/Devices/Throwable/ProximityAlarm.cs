using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Devices|Throwable")]
    public class ProximityAlarm : Throwable
    {
        public ProximityAlarm(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/ProximityAlarm.png"), 16, 16, false);
            graphic = _sprite;
            center = new Vec2(8f, 8f);
            collisionOffset = new Vec2(-4f, -4f);
            collisionSize = new Vec2(8f, 8f);
            weight = 0.9f;
            placeable = false;
            index = 5;
            UsageCount = 2;
            sticky = true;
            weightR = 6;

            setFramesLoad = 48;

            DeviceCost = 5;
            descriptionPoints = "Proximity set"; 
            drawTraectory = true; 
            minimalTimeOfHolding = 0.5f;
            needsToBeGentle = false;

            isSecondary = true;
        }

        public override void SetRocky()
        {
            rock = new ProximityAlarmAP(position.x, position.y);
            base.SetRocky();
        }     
    }

    public class ProximityAlarmAP : Rocky
    {
        public int soundFrames;

        public ProximityAlarmAP(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/ProximityAlarm.png"), 16, 16, false);
            graphic = _sprite;
            center = new Vec2(8f, 8f);
            collisionOffset = new Vec2(-4f, -4f);
            collisionSize = new Vec2(8f, 8f);

            weight = 0.9f;
            placeable = false;
            breakable = true;
            zeroSpeed = false;
            sticky = true;
            destructingByElectricity = true;
            scannable = true;

            setted = false;
        }

        public override void Update()
        {
            base.Update();
            if (setted == true)
            {
                foreach (Operators d in Level.CheckCircleAll<Operators>(position, 48))
                {
                    if (setted == true && Level.CheckLine<Block>(position, d.position) == null)
                    {
                        if (d.team != "Def" && d.undetectable <= 0)
                        {
                            bool exits = false;
                            foreach (Effect f in d.effects)
                            {
                                if (f.name == "Proximity alarm")
                                {
                                    exits = true;
                                    f.timer = 0.1f;
                                }
                            }
                            if (exits == false)
                            {
                                d.effects.Add(new ProximityAlarmed());
                            }

                            if (soundFrames < 30)
                            {
                                soundFrames = 300;
                                if (oper != null)
                                {
                                    if (oper.local)
                                    {
                                        PlayerStats.renown += 5;
                                        PlayerStats.Save();
                                        Level.Add(new RenownGained() { description = "Proximity alarm", amount = 5 });
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (soundFrames > 0)
            {
                soundFrames--;
            }

            if (soundFrames > 30 && soundFrames % 10 == 0)
            {
                DuckNetwork.SendToEveryone(new NMSoundSource(position, 320, "SFX/PeepSound.wav", "J"));
                Level.Add(new SoundSource(position.x, position.y, 320, "SFX/PeepSound.wav", "J"));
            }
        }

        public override void DetonateFull()
        {
            
        }

        public override void Draw()
        {
            base.Draw();
            if (setFrames > 0 && setted)
            {
                Graphics.DrawCircle(position, 48f - setFrames, Color.White, 2f, 1f, 32);
                setFrames--;
            }
        }
    }
}
