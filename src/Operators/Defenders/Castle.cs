using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators|Defense")]
    public class CastleOPEQ : OPEQ
    {
        public CastleOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            description =
"Castle's unique ability is \n\n" +
"to create reinforced \n\n" +
"barricades using the: \n\n" +
"UTP1-Universal Tactical Panel \n\n" +
"   \n\n" +
"   \n\n" +
"       ";
            name = "CASTLE";
            oper = new Castle(this.position.x, this.position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 18;
            graphic = _sprite;
        }
    }
    public class Castle : Defender
    {
        public Castle(float xpos, float ypos) : base(xpos, ypos)
        {
            Primary = new List<GunDev>()
            {
                new UMP45(this.position.x, this.position.y),
                new M1014(this.position.x, this.position.y)
            };

            Secondary = new List<GunDev>()
            {
                new FiveSeven(this.position.x, this.position.y),
                new Meusoc(this.position.x, this.position.y)
                //new FiveSeven(this.position.x, this.position.y),
            };

            Devices = new List<Device>()
            {
                new Impact(this.position.x, this.position.y),
                new ProximityAlarm(this.position.x, this.position.y)
            };

            bodyLocation = "Sprites/Operators/fbi.png";
            headLocation = "Sprites/Operators/castleHat.png";
            handLocation = "Sprites/Operators/FBICastleHand.png";

            injureScream = "SFX/Characters/ScreamCastle.wav";

            Knife = new Knife(this.position.x, this.position.y);
            MainDevice = new CastleBarricade(this.position.x, this.position.y);
            Phone = new Phone(this.position.x, this.position.y);
            drone = new Drone(position.x, position.y) { UsageCount = 0 };

            this.Armor = 2;
            this.Speed = 2;
            this.team = "Def";

            SetSprites();

            operatorID = 18;
        }
    }
}
