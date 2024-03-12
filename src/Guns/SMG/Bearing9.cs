using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|Mini SMG")]
    public class Bearing9 : GunDev
    {
        public Bearing9(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/Bearing9.png"), 32, 32, false);
            graphic = _sprite;
            _sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(24, 8);
            collisionOffset = new Vec2(-12f, -4f);
            barrel = new Vec2(12, -0.5f);

            ammo = 26;
            maxAmmo = 25;
            magazine = 100;
            timeToReload = 3.6f;
            timeToTacticalReload = 3f;
            accuracy = 0.42f;
            xScope = 1.25f;
            hRecoil = 8.2f;
            uRecoil = -10.3f;
            dRecoil = 14.3f;
            fireRate = 0.0545f;
            damage = 33;

            gunMobility = 0.95f;
            gunADSMobility = 0.55f;

            canBeTacticallyReloaded = true;
            name = "Bearing 9";
            oneHand = true;
            penetration = 1;

            yStability = 0.68f;
            xStability = 0.85f;
            fireSound = "SFX/guns/mac10_02.wav";

            weaponClass = "SMG";
        }
    }
}
