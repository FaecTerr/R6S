using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|Shotguns")]
    public class ITA12S : GunDev
    {
        public ITA12S(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/ITA12S.png"), 32, 32, false);
            graphic = this._sprite;
            _sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(30, 8);
            collisionOffset = new Vec2(-15f, -4f);
            barrel = new Vec2(12, -0.5f);

            ammo = 5;
            maxAmmo = 5;
            magazine = 40;
            timeToReload = 0.6f;
            timeToTacticalReload = 0.35f;
            accuracy = 0.725f;
            xScope = 1.05f;
            hRecoil = 7.4f;
            uRecoil = -20.3f;
            dRecoil = 22.3f;
            fireRate = 0.7f;
            damage = 35;

            gunMobility = 0.85f;
            gunADSMobility = 0.75f;

            isShotgun = true;
            semiAuto = false;
            bulletsPerShot = 8;
            canBeTacticallyReloaded = false;
            name = "ITA12S";
            manuallyReload = true;

            overwriteDamageDrop = true;
            damageDropDistance = 0.05f;
            maxDropDistance = 0.3f;
            minDamageDrop = 0.08f;

            yStability = 0.9f;
            xStability = 0.35f;

            weaponClass = "Shotgun";
            arc = 30;
        }
    }
}
