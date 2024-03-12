using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Furniture")]
    public class EvilEye : Throwable
    {
        public EvilEye(float xpos, float ypos) : base(xpos, ypos)
        {
            center = new Vec2(6f, 6f);
            collisionSize = new Vec2(12f, 12f);
            collisionOffset = new Vec2(-6f, -6f);
            team = "Def";
            _editorName = "OBS evileye";

            bulletproof = true;
            sticky = true;
            closeDeployment = true;
            breakable = true;

            UsageCount = 2;
            index = 46;

            needsToBeGentle = true;
        }
        public override void SetRocky()
        {
            rock = new EvilEyeAP(position.x, position.y);
            base.SetRocky();
        }
    }
    public class EvilEyeAP : ObservationThing
    {
        public EvilEyeAP(float xpos, float ypos) : base(xpos, ypos)
        {
            center = new Vec2(6f, 6f);
            collisionSize = new Vec2(12f, 12f);
            collisionOffset = new Vec2(-6f, -6f);
            team = "Def";
            _editorName = "OBS evileye"; 
            
            Set();
            UpdatePhones();
        }
    }
}
