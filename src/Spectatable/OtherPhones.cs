using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class NHPhone : Phone
    {
        public NHPhone(float xpos, float ypos) : base(xpos, ypos)
        {
            phoneType = 1;
        }
    }
    public class WGPhone : Phone
    {
        public WGPhone(float xpos, float ypos) : base(xpos, ypos)
        {
            phoneType = 2;
        }
    }
    public class RHPhone : Phone
    {
        public RHPhone(float xpos, float ypos) : base(xpos, ypos)
        {
            phoneType = 5;
        }
    }
    public class VSPhone : Phone
    {
        public VSPhone(float xpos, float ypos) : base(xpos, ypos)
        {
            phoneType = 6;
        }
    }
    public class GEPhone : Phone
    {
        public GEPhone(float xpos, float ypos) : base(xpos, ypos)
        {
            phoneType = 7;
        }
    }
    public class Tablet : Phone
    {
        public Tablet(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/tablet.png"), 12, 12, false);
            graphic = _sprite;
            monitorSize = new Vec2(0.5f, 0.5f);
            phoneSize = new Vec2(0.91f, 0.88f);
            _sprite.frame = 3;
            phoneType = 3;
        }
    }
    public class HandPhone : Phone
    {
        public HandPhone(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Devices/NanoPhone.png"), 12, 12, false);
            graphic = _sprite;
            monitorSize = new Vec2(0.15f, 0.15f);
            phoneType = 4;
        }
    }
}
