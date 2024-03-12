using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators|Defense")]
    public class RookOPEQ : OPEQ
    {
        public RookOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            description =
"Rook is capable of protecting \n\n" +
"himself and his team mates \n\n" +
"through his unique ability: \n\n" +
"R1N 'RHINO' Armor. \n\n" +
" \n\n" +
" \n\n" +
"        ";
            name = "ROOK";
            oper = new Rook(this.position.x, this.position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 12;
            graphic = _sprite;
        }
    }
    public class Rook : Operators
    {
        public Rook(float xpos, float ypos) : base(xpos, ypos)
        {
            Primary = new List<GunDev>()
            {
                new MP5(this.position.x, this.position.y),
                new SGCQB(this.position.x, this.position.y)
            };

            Secondary = new List<GunDev>()
            {
                new P9(this.position.x, this.position.y),
                new LFP(this.position.x, this.position.y)
            };

            Devices = new List<Device>()
            {
                new Impact(this.position.x, this.position.y),
                new BarbedWire(this.position.x, this.position.y)
            };

            _sprite = new SpriteMap(GetPath("Sprites/Operators/gign.png"), 32, 32, false);
            _head = new SpriteMap(GetPath("Sprites/Operators/rookHat.png"), 32, 32, false);
            _hand = new SpriteMap(GetPath("Sprites/Operators/GIGNGloves.png"), 8, 8, false);

            bodyLocation = "Sprites/Operators/gign.png";
            headLocation = "Sprites/Operators/rookHat.png";
            handLocation = "Sprites/Operators/GIGNGloves.png";

            injureScream = "SFX/Characters/ScreamRook.wav";

            Knife = new Knife(this.position.x, this.position.y);
            MainDevice = new RookArmor(this.position.x, this.position.y);
            Phone = new Phone(this.position.x, this.position.y);
            drone = new Drone(position.x, position.y) { UsageCount = 0 };

            this.Armor = 3;
            this.Speed = 1;
            this.team = "Def";

            SetSprites();

            operatorID = 12;
        }
    }
}
