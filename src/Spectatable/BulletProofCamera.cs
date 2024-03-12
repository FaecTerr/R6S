using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DuckGame;

namespace DuckGame.R6S
{
    public class BulletProofCamera : Throwable
    {
        public BulletProofCamera(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Cameras/Bulletproof.png"), 16, 16);
            graphic = _sprite;
            _sprite.frame = 0;
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(16f, 16f);
            collisionOffset = new Vec2(-8f, -8f);
            setTime = 0.8f;
            weight = 0.9f;
            thickness = 0.1f;
            placeable = false;
            breakable = false;
            zeroSpeed = false;
            team = "Def";
            _enablePhysics = true;

            setTime = 0.5f;

            sticky = true;
            closeDeployment = true;
            index = 4;
            UsageCount = 2;
            isSecondary = true;

            placeSound = "SFX/Devices/BulletproofCam.wav";
        }

        public override void SetRocky()
        {
            rock = new BulletProofCameraAP(position.x, position.y);
            if (oper != null)
            {
                rock.oper = oper;
            }
            base.SetRocky();
        }
    }

    public class BulletProofCameraAP : ObservationThing
    {
        public BulletProofCameraAP(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Cameras/Bulletproof.png"), 16, 16);
            graphic = _sprite;
            _sprite.frame = 0;
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(16f, 16f);
            collisionOffset = new Vec2(-8f, -8f);
            setTime = 0.8f;
            weight = 0.9f;
            thickness = 0.4f;
            placeable = false;
            setted = false;
            breakable = false;
            sticky = true;
            team = "Def";
            bulletproof = true;

            cameraOverlay = 1;
            _editorName = "OBS bproof";

            UpdatePhones();

            observableOutside = false;

            lightColor = Color.Green;
            doLight = true;
            sizeOfLight = 0.5f;
        }
    }
}
