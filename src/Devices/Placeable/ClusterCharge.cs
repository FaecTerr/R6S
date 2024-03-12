using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Guns|Grenades")]
    [BaggedProperty("isFatal", false)]
    public class ClusterCharge : Throwable
    {
        public ClusterCharge(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/ClasterCharge.png"), 12, 12, false);
            graphic = _sprite;
            center = new Vec2(6f, 6f);
            collisionOffset = new Vec2(-5f, -5f);
            collisionSize = new Vec2(10f, 10f);
            weight = 0.9f;
            thickness = 0.1f;
            placeable = false;
            scannable = false;

            sticky = true;
            UsageCount = 3;
            index = 7;
            closeDeployment = true;
            doubleActivation = true;

            setTime = 0.5f;

            descriptionPoints = "Cluster charge";
            DeviceCost = 10;

            placeSound = "SFX/Devices/FuzeChargePlace.wav";
        }

        public override void SetRocky()
        {
            rock = new ClusterChargeAP(position.x, position.y);
            base.SetRocky();
        }
    }
    public class ClusterChargeAP : Rocky
    {
        public bool HasExploded;
        public bool hasPin;
        public int dropFrames = 30;
        

        public ClusterChargeAP(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/ClasterCharge.png"), 12, 12, false);
            graphic = _sprite;
            center = new Vec2(6f, 6f);
            collisionOffset = new Vec2(-5f, -5f);
            collisionSize = new Vec2(10f, 10f);

            weight = 0.9f;
            thickness = 0.1f;
            placeable = false;
            breakable = true;
            scannable = false;
            zeroSpeed = false;
            sticky = true;
            destructingByElectricity = true;

            UsageCount = 5;
            
            DeviceCost = 20;
            destroyedPoints = "Cluster destroyed";
        }

        public override void Detonate(float delay = 0)
        {
            Level.Add(new SoundSource(position.x, position.y, 320, "SFX/Devices/FuzeChargeDetonate.wav", "J"));
            DuckNetwork.SendToEveryone(new NMSoundSource(position, 320, "SFX/Devices/FuzeChargeDetonate.wav", "J"));
            base.Detonate(delay);
        }

        public override void DetonateFull()
        {
            //base.DetonateFull();
        }

        public override void Update()
        {
            if (detonate)
            {
                canPick = false;
                if (jammed == true)
                {
                    dropFrames = 30;
                }
                if (dropFrames <= 0 && UsageCount > 0)
                {
                    UsageCount--;
                    dropFrames = 30;
                    Level.Add(new ClusterBomb(position.x + Dir.x * 28, position.y + Dir.y * 28) { hSpeed = (2 - UsageCount) * 2 * Dir.y + (2 - UsageCount) * 2 * Dir.x, oper = oper });
                    Level.Add(new SoundSource(position.x, position.y, 200, "SFX/Devices/FuzeChargeThrow.wav", "J"));
                    DuckNetwork.SendToEveryone(new NMSoundSource(position, 200, "SFX/Devices/FuzeChargeThrow.wav", "J"));
                }
                else
                {
                    dropFrames--;
                }
                if (UsageCount <= 0)
                {
                    Level.Remove(this);
                }
            }
            base.Update();
        }
    }
}