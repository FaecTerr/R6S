using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Devices|Throwable")]
    public class ADS : Throwable
    {
        public ADS(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/ADS.png"), 16, 16, false);
            graphic = this._sprite;
            _sprite.frame = 0;
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(16f, 16f);
            collisionOffset = new Vec2(-8f, -8f);
            setTime = 0.8f;
            weight = 0.9f;
            thickness = 0.1f;
            placeable = true;
            breakable = true;
            zeroSpeed = false;
            sticky = true;
            weightR = 6;
            UsageCount = 3;

            setFramesLoad = 80;
            closeDeployment = true;

            DeviceCost = 15;
            descriptionPoints = "Active defense set";
            placeSound = "SFX/Devices/ADSPlace.wav";
        }

        public override void SetRocky()
        {
            rock = new ADSAP(position.x, position.y);
            base.SetRocky();
        }
    }
    public class ADSAP : Rocky
    {
        public int destroyFrames;
        public Vec2 gPos;

        public float reload;
        float radius = 80;

        public ADSAP(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/ADS.png"), 16, 16, false);
            graphic =_sprite;
            _sprite.frame = 0;
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(16f, 16f);
            collisionOffset = new Vec2(-8f, -8f);
            thickness = 0.4f;
            placeable = false;
            setted = false;
            breakable = true;
            sticky = true;
            destructingByElectricity = true;
            UsageCount = 1;

            setted = false;
            setFrames = 80;

            DeviceCost = 25;
            descriptionPoints = "Active defense destroyed";
        }

        public override void Set()
        {
            base.Set();
        }

        public override void DetonateFull()
        {

        }

        public override void Update()
        {
            base.Update();
            if (destroyFrames > 0)
            {
                destroyFrames--;
            }
            if(UsageCount <= 0)
            {
                if(reload < 15)
                {
                    reload += 0.01666666f;
                }
                else
                {
                    UsageCount += 1;
                }
            }

            if (setFrames == 2)
            {
                Level.Add(new SoundSource(position.x, position.y, 240, "SFX/Devices/ADSActive.wav", "J"));
                DuckNetwork.SendToEveryone(new NMSoundSource(position, 240, "SFX/Devices/ADSActive.wav", "J"));
            }

            if (setted == true && !jammed)
            {
                if (UsageCount > 0)
                {            
                    foreach(Device bg in Level.CheckCircleAll<Device>(position, radius))
                    {
                        if (Level.CheckLine<Block>(bg.position, position) == null && bg.catchableByADS && UsageCount > 0 && bg.team != team && bg.mainDevice != null)
                        {
                            Level.Remove(bg);
                            UsageCount--;
                            gPos = bg.position;
                            destroyFrames = 20;
                        }
                    }

                    if (oper != null)
                    {
                        if (mainDevice == null && oper.local)
                        {
                            PlayerStats.renown += 20;
                            PlayerStats.Save();
                            Level.Add(new RenownGained() { description = "Disabled grenade", amount = 20 });
                        }
                    }
                }
                else
                {
                    canPick = false;
                }
            }
        }

        public override void Draw()
        {
            base.Draw();
            if (setted == true && destroyFrames > 0)
            {
                Graphics.DrawDottedLine(position, gPos, Color.White, 0.9f, 8f);
            }
            if (setFrames > 0 && setted && oper != null && oper.local)
            {
                //text = "Grenade defense is now active";
                
                Graphics.DrawCircle(position, 80f - setFrames, Color.White, 2f, 1f, 32);

                setFrames -= 2;
            }

            if (DevConsole.showCollision)
            {
                Graphics.DrawCircle(position, 80, Color.Beige, 1f, 1f, 32);
            }
        }
    }
}
