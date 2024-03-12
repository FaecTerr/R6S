using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators|Defense")]
    public class JagerOPEQ : OPEQ
    {
        public JagerOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            description =
"He is capable of destroying \n\n" +
"incoming projectiles due to \n\n" +
"his unique deployable gadget: \n\n" +
"the Active Defense System \n\n" +
"   \n\n" +
"   \n\n" +
"       ";
            name = "JAGER";
            oper = new Jager(this.position.x, this.position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 0;
            graphic = _sprite;
        }
    }
    public class Jager : Defender
    {
        public Jager(float xpos, float ypos) : base(xpos, ypos)
        {
            Primary = new List<GunDev>()
            {
                new C416(position.x, position.y),
                new M870(position.x, position.y)
            };

            Secondary = new List<GunDev>()
            {
                new P12(position.x, position.y)
            };

            Devices = new List<Device>()
            {
                new BulletProofCamera(position.x, position.y),
                new BarbedWire(position.x, position.y)
            };

            bodyLocation = "Sprites/Operators/jager.png";
            headLocation = "Sprites/Operators/jagerHat.png";
            handLocation = "Sprites/Operators/GSGJagerGloves.png";

            injureScream = "SFX/Characters/ScreamJager.wav";

            Knife = new Knife(position.x, position.y);
            MainDevice = new ADS(position.x, position.y);
            Phone = new Phone(position.x, position.y);
            drone = new Drone(position.x, position.y) { UsageCount = 0 };

            Armor = 1;
            Speed = 3;
            team = "Def";

            SetSprites();

            operatorID = 0;
        }
    }
}
