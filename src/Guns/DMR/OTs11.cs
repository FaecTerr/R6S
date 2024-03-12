using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|DMR")]
    public class OTs11 : GunDev
    {
        public OTs11(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/OTs11.png"), 48, 32, false);
            graphic = _sprite;
            _sprite.frame = 0;

            center = new Vec2(24f, 16f);
            collisionSize = new Vec2(30, 8);
            collisionOffset = new Vec2(-15f, -4f);
            barrel = new Vec2(22, -1.5f);

            ammo = 11;
            maxAmmo = 10;
            magazine = 60;
            timeToReload = 3f;
            timeToTacticalReload = 2f;
            accuracy = 0.85f;
            xScope = 1.5f;
            hRecoil = 1.8f;
            uRecoil = -18.3f;
            dRecoil = 20.7f;
            fireRate = 0.105f;
            damage = 71;

            muzzle = 2;

            gunMobility = 0.9f;
            gunADSMobility = 0.35f;

            semiAuto = false;
            canBeTacticallyReloaded = true;
            name = "OTs11";
            reticle = 8;
            highPowered = true;

            penetration = 1;

            yStability = 0.89f;
            xStability = 0.85f;
            fireSound = "SFX/guns/mcx300_01.wav";


            weaponClass = "DMR";
            arc = 20;
        }
    }
}
