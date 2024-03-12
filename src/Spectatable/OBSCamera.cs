using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    [EditorGroup("Faecterr's|Furniture")]
    public class OBSCamera : ObservationThing
    {
        public OBSCamera(float xpos, float ypos) : base(xpos, ypos)
        {
            center = new Vec2(6f, 6f);
            collisionSize = new Vec2(12f, 12f);
            collisionOffset = new Vec2(-6f, -6f);
            team = "Def";
            _editorName = "OBS cam";

            DeviceCost = 10;
            descriptionPoints = "Camera set";
            destroyedPoints = "Camera destroyed";

            setted = true;
            placeable = false;
            jammResistance = false;

            isSecondary = true;
            index = 6;

            doLight = true;
            mainDevice = this;
        }
    }
}
