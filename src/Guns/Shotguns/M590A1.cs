using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|Shotguns")]
    public class M590A1 : GunDev
    {
        public M590A1(float xpos, float ypos) : base(xpos, ypos)
        {
            this._sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/M590A1.png"), 32, 32, false);
            this.graphic = this._sprite;
            this._sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(30, 8);
            collisionOffset = new Vec2(-15f, -4f);
            barrel = new Vec2(13, -0.5f);

            ammo = 7;
            maxAmmo = 7;
            magazine = 24;
            timeToReload = 0.6f;
            timeToTacticalReload = 0.5f;
            accuracy = 0.775f;
            xScope = 1f;
            hRecoil = 12f;
            uRecoil = -34f;
            dRecoil = 36f;
            fireRate = 0.48f;
            damage = 26;
            
            penetration = 1;

            gunMobility = 0.85f;
            gunADSMobility = 0.85f;

            isShotgun = true;
            semiAuto = false;
            bulletsPerShot = 7;
            name = "M590A1";
            manuallyReload = true;
            canBeTacticallyReloaded = false;

            yStability = 0.9f;
            xStability = 0.95f;
            fireSound = "SFX/guns/mossberg_590a_01_pump.wav";


            weaponClass = "Shotgun";
            arc = 20;
        }
    }
}
