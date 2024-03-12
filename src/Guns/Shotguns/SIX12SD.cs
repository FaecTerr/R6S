using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|Shotguns")]
    public class SIX12SD : GunDev
    {
        public SIX12SD(float xpos, float ypos) : base(xpos, ypos)
        {
            this._sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/SIX12SD.png"), 32, 32, false);
            this.graphic = this._sprite;
            this._sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(30, 8);
            collisionOffset = new Vec2(-15f, -4f);
            barrel = new Vec2(15, -0.5f);

            ammo = 6;
            maxAmmo = 6;
            magazine = 30;
            timeToReload = 1.9f;
            timeToTacticalReload = 1.9f;
            accuracy = 0.75f;
            xScope = 1f;
            hRecoil = 1.8f;
            uRecoil = -40.5f;
            dRecoil = 45.6f;
            fireRate = 0.06f;
            damage = 18;

            muzzle = 1;

            //penetration = 1;

            gunMobility = 0.85f;
            gunADSMobility = 0.75f;

            isShotgun = true;
            semiAuto = false;
            bulletsPerShot = 8;
            canBeTacticallyReloaded = false;
            name = "SIX12 SD";

            yStability = 0.97f;
            xStability = 0.35f;
            fireSound = "SFX/guns/ksg_01.wav";


            weaponClass = "Shotgun";
            arc = 16;
        }
    }
}
