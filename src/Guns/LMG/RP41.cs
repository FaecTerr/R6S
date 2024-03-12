using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|LMG")]
    public class RP41 : GunDev
    {
        public RP41(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/RP41.png"), 48, 32, false);
            graphic = _sprite;
            _sprite.frame = 0;

            center = new Vec2(24f, 16f);
            collisionSize = new Vec2(46, 8);
            collisionOffset = new Vec2(-23f, -4f);
            barrel = new Vec2(20, -0.5f);

            ammo = 100;
            maxAmmo = 100;
            magazine = 100;
            timeToReload = 5.3f;
            timeToTacticalReload = 4.6f;
            accuracy = 0.57f;
            xScope = 2f;
            hRecoil = 7f;
            uRecoil = -11f;
            dRecoil = 9f;
            fireRate = 0.105f;
            damage = 46;
            penetration = 1;

            gunMobility = 0.8f;
            gunADSMobility = 0.3f;

            canBeTacticallyReloaded = false;
            name = "RP41";

            yStability = 0.96f;
            xStability = 0.95f;
            fireSound = "SFX/guns/M249_01.wav";


            weaponClass = "LMG";
            arc = 40;
        }
    }
}
