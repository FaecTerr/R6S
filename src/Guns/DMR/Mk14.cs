using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|DMR")]
    public class Mk14 : GunDev
    {
        public Mk14(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/Mk14.png"), 32, 32, false);
            graphic = _sprite;
            _sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(30, 8);
            collisionOffset = new Vec2(-15f, -4f);
            barrel = new Vec2(14, -0.5f);

            ammo = 16;
            maxAmmo = 15;
            magazine = 60;
            timeToReload = 3.2f;
            timeToTacticalReload = 2.4f;
            accuracy = 0.75f;
            xScope = 3f;
            hRecoil = 4.8f;
            uRecoil = -22.5f;
            dRecoil = 24.6f;
            fireRate = 0.095f;
            damage = 57;

            gunMobility = 0.9f;
            gunADSMobility = 0.35f;

            semiAuto = false;
            canBeTacticallyReloaded = true;
            name = "Mk14 EBR";
            reticle = 8;
            highPowered = true;
            penetration = 1;

            muzzle = 2;

            yStability = 0.95f;
            xStability = 0.45f;
            fireSound = "SFX/guns/Mk14EBR.wav";


            weaponClass = "DMR";
            arc = 24;
        }
    }
}
