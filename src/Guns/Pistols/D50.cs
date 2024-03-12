using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|Pistols")]
    public class D50 : GunDev
    {
        public D50(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/D50.png"), 32, 32, false);
            graphic = _sprite;
            _sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(14, 8);
            collisionOffset = new Vec2(-7f, -4f);
            barrel = new Vec2(10, -0.5f);

            ammo = 8;
            maxAmmo = 7;
            magazine = 35;
            timeToReload = 2.5f;
            timeToTacticalReload = 2f;
            accuracy = 0.45f;
            xScope = 1.25f;
            hRecoil = 14.8f;
            uRecoil = -35.2f;
            dRecoil = 30.8f;
            fireRate = 0.13333f;
            damage = 71;

            gunMobility = 1f;
            gunADSMobility = 0.7f;

            canBeTacticallyReloaded = true;
            name = "D-50";
            semiAuto = false;
            oneHand = true;

            highPowered = true;

            yStability = 0.97f;
            xStability = 0.97f;
            fireSound = "SFX/guns/magnum_357_02.wav";


            weaponClass = "Pistol";
        }
    }
}
