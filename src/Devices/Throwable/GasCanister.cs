using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Devices|Throwable")]
    public class GasCanister : Throwable
    {
        public GasCanister(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/GasCanister.png"), 16, 16, false);
            graphic = _sprite;
            center = new Vec2(8f, 8f);
            collisionOffset = new Vec2(-4f, -6f);
            collisionSize = new Vec2(8f, 12f);
            weight = 0.9f;
            thickness = 0.1f;
            placeable = false;
            breakable = true;
            sticky = true;
            UsageCount = 3;
            delay = 0;
            weightR = 7;
            index = 8;
            doubleActivation = true;

            drawTraectory = true;
            useCustomHitMarker = true;

            DeviceCost = 15;
            descriptionPoints = "Gas canister"; 
            minimalTimeOfHolding = 0.25f;
            needsToBeGentle = false;
        }

        public override void SetRocky()
        {
            rock = new GasCanisterAP(position.x, position.y);
            base.SetRocky();
        }   
    }
    public class GasCanisterAP : Rocky
    {
        public GasCanisterAP(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/GasCanister.png"), 16, 16, false);
            graphic = _sprite;
            center = new Vec2(8f, 8f);
            collisionOffset = new Vec2(-5f, -5f);
            collisionSize = new Vec2(10f, 10f);
            weight = 0.9f;
            thickness = 0.1f;
            placeable = false;
            breakable = true;
            zeroSpeed = false;
            destructingByElectricity = true;
                        
            sticky = true;
        }

        public override void DetonateFull()
        {
            Level.Add(new SoundSource(position.x, position.y, 480, "SFX/Devices/Smoke.wav", "J"));
            DuckNetwork.SendToEveryone(new NMSoundSource(position, 480, "SFX/Devices/Smoke.wav", "J"));

            Level.Add(new GasSmoke(position.x - Dir.x * 16, position.y - Dir.y * 12, 8f) { team = team, oper = oper});

            DuckNetwork.SendToEveryone(new NMSmokeCanister(position, 6f));
            base.DetonateFull();
        }
    }
}
