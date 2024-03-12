using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators|Defense")]
    public class DocOPEQ : OPEQ
    {
        public DocOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            description =
"Doc equipped with stim \n\n" +
"pistol, which can cure \n\n" +
"you and your teammates. \n\n" +
"Can get on feet injured \n\n" +
"players.   \n\n" +
"   \n\n" +
"       ";
            name = "DOC";
            oper = new Doc(this.position.x, this.position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 14;
            graphic = _sprite;
        }
    }
    public class Doc : Operators
    {
        public Doc(float xpos, float ypos) : base(xpos, ypos)
        {
            Primary = new List<GunDev>()
            {
                new MP5(position.x, position.y),
                new P90(position.x, position.y)
                
            };

            Secondary = new List<GunDev>()
            {
                new P9(position.x, position.y),
                new LFP(position.x, position.y)
            };

            Devices = new List<Device>()
            {
                new BarbedWire(position.x, position.y),
                new ProximityAlarm(position.x, position.y)//,
                //new C4(position.x, position.y)
            };

            bodyLocation = "Sprites/Operators/gign.png";
            headLocation = "Sprites/Operators/docHat.png";
            handLocation = "Sprites/Operators/GIGNDocHand.png";

            injureScream = "SFX/Characters/ScreamDoc.wav";

            Knife = new Knife(position.x, position.y);
            MainDevice = new Stimulator(position.x, position.y);
            Phone = new WGPhone(position.x, position.y);
            drone = new Drone(position.x, position.y) { UsageCount = 0 };

            Armor = 3;
            Speed = 1;
            team = "Def";


            SetSprites();

            operatorID = 14;
        }
    }
}
