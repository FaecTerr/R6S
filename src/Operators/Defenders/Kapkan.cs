using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators|Defense")]
    public class KapkanOPEQ : OPEQ
    {
        public KapkanOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            name = "KAPKAN";

            description =
                "Kapkan is equipped with a \n\n" +
                "Entry Denial Device. This \n\n" +
                "trap is a packed C4 charge \n\n" +
                "activated when motion is \n\n" +
                "detected. It can be placed \n\n" +
                "on door and window frames -\n\n" +
                "denying key entries for attackers.";

            oper = new Kapkan(this.position.x, this.position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 6;
            graphic = _sprite;
        }
    }
    public class Kapkan : Defender
    {
        public Kapkan(float xpos, float ypos) : base(xpos, ypos)
        {
            Primary = new List<GunDev>()
            {
                new VSN(position.x, position.y),
                new SASG11(position.x, position.y)

            };

            Secondary = new List<GunDev>()
            {
                new PMM(position.x, position.y),
                new GSH(position.x, position.y)
            };

            Devices = new List<Device>()
            {
                new Impact(position.x, position.y),
                new C4(position.x, position.y)
            };

            bodyLocation = "Sprites/Operators/spetsnaz.png";
            headLocation = "Sprites/Operators/kapkanHat.png";
            handLocation = "Sprites/Operators/SpetsnazGloves.png";

            injureScream = "SFX/Characters/ScreamKapkan.wav";

            Knife = new Knife(position.x, position.y);
            MainDevice = new EDD(position.x, position.y);
            Phone = new Phone(position.x, position.y);
            drone = new Drone(position.x, position.y) { UsageCount = 0 };
            Armor = 2;
            Speed = 2;
            team = "Def";


            SetSprites();
            operatorID = 6;
        }
    }
}
