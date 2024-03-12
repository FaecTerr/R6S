using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Guns|Devices|IHUD")]
    public class ElectronicsDetector : Device
    {
        public List<Device> devices = new List<Device>();
        public List<ObservationThing> obs = new List<ObservationThing>();
        public float radius1;
        public float radius2 = 12f;

        public int scans = 999;

        public List<Device> scannedDevices = new List<Device>();

        public ElectronicsDetector(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/DED.png"), 16, 16, false);
            graphic = _sprite;
            center = new Vec2(8f, 8f);
            collisionOffset = new Vec2(-8f, -8f);
            collisionSize = new Vec2(16f, 16f);
            weight = 0.9f;
            placeable = false;
            breakable = false;
            zeroSpeed = false;
            scannable = true;

            depth = 0.6f;

            oneHand = true;
            mainHand = true;

            jammResistance = true;

            index = 3;

            ShowCounter = false;
            UsageCount = 3;
        }

        public override void Update()
        {
            base.Update();

            if (user != null && user.holdObject != null)
            {
                if (user.holdObject == this && jammed == false)
                {
                    frame = 1;

                    Vec2 camPos = Level.current.camera.position;
                    Vec2 camSize = Level.current.camera.size;

                    foreach (Device d in Level.CheckRectAll<Device>(camPos, camPos + camSize))
                    {
                        if (!(d is GunDev))
                        {
                            if (radius1 == 0)
                            {
                                Level.Add(new SoundSource(position.x, position.y, 80, "SFX/Devices/EDSensorSee.wav", "J"));
                                DuckNetwork.SendToEveryone(new NMSoundSource(position, 80, "SFX/Devices/EDSensorSee.wav", "J"));
                            }
                            d.alpha = 1;
                            if (d.team != user.team && !devices.Contains(d))
                            {
                                devices.Add(d);
                            }
                            if (user.local && scans > 0 && !scannedDevices.Contains(d))
                            {
                                scannedDevices.Add(d);
                                scans--;
                                PlayerStats.renown += DeviceCost;
                                PlayerStats.Save();
                                Level.Add(new RenownGained() { description = "Device scanned", amount = 2 });
                            }
                        }
                    }
                }
                if(user.holdObject != this || jammed)
                {
                    devices.Clear();
                }
            }
        }
        public override void Draw()
        {
            if (user != null && user.holdObject != null)
            {
                if (user.holdObject == this && user.local)
                {
                    foreach (Device d in devices)
                    {
                        if (d.scannable && d.team != team)
                        {
                            Graphics.DrawCircle(d.position, radius1, Color.Purple, 2f, 0.5f, 32);
                            Graphics.DrawCircle(d.position, radius2, Color.Blue, 2f, 0.5f, 32);
                        }
                    }

                    radius1 += 0.5f;
                    radius2 += 0.5f;
                    if (radius1 >= 24f)
                    {
                        radius1 = 0f;
                    }
                    if (radius2 >= 24f)
                    {
                        radius2 = 0f;
                    }
                }
            }
            base.Draw();
        }
    }
}
