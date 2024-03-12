using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Devices|Throwable")]
    public class BlackEye : Throwable
    {
        public BlackEye(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/BlackEye.png"), 16, 16, false);
            graphic = _sprite;
            center = new Vec2(8f, 8f);
            collisionOffset = new Vec2(-2f, -2f);
            collisionSize = new Vec2(4f, 4f);
            weight = 0.9f;
            thickness = 0.1f;

            placeable = false;
            breakable = true;
            zeroSpeed = false;
            sticky = true;
            UsageCount = 4;
            delay = 0;

            weightR = 7;
            index = 20;

            DeviceCost = 10;
            descriptionPoints = "Black eye";

            needsToBeGentle = false;
            minimalTimeOfHolding = 0.5f;
            //drawTraectory = true;
        }

        public override void SetRocky()
        {
            rock = new BlackEyeAP(position.x, position.y);
            base.SetRocky();
        }
    }
    public class BlackEyeAP : ObservationThing
    {
        public BlackEyeAP(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/BlackEye.png"), 16, 16, false);
            graphic = _sprite;
            center = new Vec2(8f, 8f);
            collisionOffset = new Vec2(-2f, -2f);
            collisionSize = new Vec2(4f, 4f);
            weight = 0.9f;
            thickness = 0.1f;

            breakable = true;
            enablePhysics = true;

            bulletproof = false;

            destructingByElectricity = true;

            sticky = true;

            team = "Def";
            _editorName = "OBS blackeye";

            _sprite.frame = 0;

            DeviceCost = 20;
            destroyedPoints = "Black eye destroyed";

            cameraOverlay = 3;

            Set();
            UpdatePhones();

            StickedSound = "SFX/Devices/ValkCam.wav";

            doLight = true;
            sizeOfLight = 0.1f;
            lightColor = Color.Turquoise;            
        }

        public override void Set()
        {
            base.Set();
        }

        public override void Update()
        {
            if (broken)
            {
                alpha = 0;
                _sprite.alpha = 0;

                Level.Remove(this);
            }
            base.Update();
        }
    }
}
