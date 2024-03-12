using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|DMR")]
    public class AR1550 : GunDev
    {
        public AR1550(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/AR1550.png"), 32, 32, false);
            graphic = _sprite;
            _sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(30, 8);
            collisionOffset = new Vec2(-15f, -4f);
            barrel = new Vec2(15, -0.5f);

            ammo = 11;
            maxAmmo = 10;
            magazine = 60;
            timeToReload = 2.3f;
            timeToTacticalReload = 1.7f;
            accuracy = 0.65f;
            xScope = 2.4f;
            hRecoil = 7.8f;
            uRecoil = -8f;
            dRecoil = 12f;
            fireRate = 0.1333f;
            damage = 62;
            penetration = 1;

            grip = 2;

            semiAuto = false;
            canBeTacticallyReloaded = true;
            name = "AR-15.50";
            reticle = 8;
            highPowered = true;

            yStability = 0.89f;
            xStability = 0.96f;
            fireSound = "SFX/guns/s12k_01.wav";


            weaponClass = "DMR";
            arc = 20;
        }
    }
}
