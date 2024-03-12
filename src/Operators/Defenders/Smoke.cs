using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Operators|Defense")]
    public class SmokeOPEQ : OPEQ
    {
        public SmokeOPEQ(float xpos, float ypos) : base(xpos, ypos)
        {
            description =
"Smoke is able to restrain \n\n" +
"enemies on key passes, as \n\n" +
"well as force them to leave \n\n" +
"their positions and change \n\n" +
"the attack vector. \n\n" +
" \n\n" +
"        ";

            name = "SMOKE";
            oper = new Smoke(this.position.x, this.position.y);
            _sprite = new SpriteMap(GetPath("Sprites/OperatorIcons.png"), 24, 24);
            _sprite.frame = 8;
            graphic = _sprite;
        }
    }
    public class Smoke : Operators
    {
        public Smoke(float xpos, float ypos) : base(xpos, ypos)
        {
            Primary = new List<GunDev>()
            {
                new FMG(position.x, position.y),
                new M590A1(position.x, position.y)
            };

            Secondary = new List<GunDev>()
            {
                new SMG11(position.x, position.y),
                new P226(position.x, position.y)
            };

            Devices = new List<Device>()
            {
                new DeployableShield(position.x, position.y),
                new BarbedWire(position.x, position.y)
            };

            bodyLocation = "Sprites/Operators/mute.png";
            headLocation = "Sprites/Operators/smokeHat.png";
            handLocation = "Sprites/Operators/SASSmokeGloves.png";

            injureScream = "SFX/Characters/ScreamSmoke.wav";
            roundStartPhrase = "SFX/Characters/smoke_firstin.wav";

            Knife = new Knife(position.x, position.y);
            MainDevice = new GasCanister(position.x, position.y);
            Phone = new NHPhone(position.x, position.y);
            drone = new Drone(position.x, position.y) { UsageCount = 0 };

            Armor = 2;
            Speed = 2;
            team = "Def";

            SetSprites();

            operatorID = 8;
        }
    }
}
