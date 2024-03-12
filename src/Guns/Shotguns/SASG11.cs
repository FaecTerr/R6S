using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|Shotguns")]
    public class SASG11 : GunDev
    {
        public SASG11(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/SASG11.png"), 32, 32, false);
            graphic = _sprite;
            _sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(30, 8);
            collisionOffset = new Vec2(-15f, -4f);
            barrel = new Vec2(15, -0.5f);

            ammo = 8;
            maxAmmo = 7;
            magazine = 35;
            timeToReload = 3.5f;
            timeToTacticalReload = 2.4f;
            accuracy = 0.825f;
            xScope = 1.05f;
            hRecoil = 1.8f;
            uRecoil = -49.5f;
            dRecoil = 51.6f;
            fireRate = 0.06f;
            damage = 25;

            //penetration = 1;

            gunMobility = 0.85f;
            gunADSMobility = 0.75f;

            isShotgun = true;
            semiAuto = false;
            bulletsPerShot = 8;
            canBeTacticallyReloaded = true;
            name = "SASG 11";

            overwriteDamageDrop = true;
            damageDropDistance = 0.1f;
            maxDropDistance = 0.5f;
            minDamageDrop = 0.25f;

            yStability = 0.95f;
            xStability = 0.35f;
            fireSound = "SFX/guns/ksg_01.wav";


            weaponClass = "Shotgun";
            arc = 25;
        }
    }
}
