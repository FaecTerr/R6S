using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators|Defense")]
    public class VigilOPEQ : OPEQ
    {
        public VigilOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            description =
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
" \n\n" +
"       ";
            name = "VIGIL";
            oper = new Vigil(position.x, position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 28;
            graphic = _sprite;
        }
    }
    public class Vigil : Defender
    {
        public Vigil(float xpos, float ypos) : base(xpos, ypos)
        {
            Primary = new List<GunDev>()
            {
                new K1A(position.x, position.y),
                new Vector45ACP(position.x, position.y)
            };

            Secondary = new List<GunDev>()
            {
                new SMG12(position.x, position.y),
                new CZ75(position.x, position.y)
            };

            Devices = new List<Device>()
            {
                new Impact(position.x, position.y),
                new BulletProofCamera(position.x, position.y)
            };

            bodyLocation = "Sprites/Operators/707th.png";
            headLocation = "Sprites/Operators/vigilHead.png";
            handLocation = "Sprites/Operators/FBIGloves.png";

            injureScream = "SFX/Characters/ScreamSmoke.wav";

            Knife = new Knife(position.x, position.y);
            MainDevice = new InvisibilityForDrones(position.x, position.y);
            Phone = new Phone(position.x, position.y);
            drone = new Drone(position.x, position.y) { UsageCount = 0 };

            Armor = 1;
            Speed = 3;
            team = "Def";


            SetSprites();

            operatorID = 28;
        }
    }
}
