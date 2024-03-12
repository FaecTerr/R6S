using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|Shotguns")]
    public class SGCQB : GunDev
    {
        public SGCQB(float xpos, float ypos) : base(xpos, ypos)
        {
            this._sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/SGCQB.png"), 32, 32, false);
            this.graphic = this._sprite;
            this._sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(30, 8);
            collisionOffset = new Vec2(-15f, -4f);
            barrel = new Vec2(14, -0.5f);

            ammo = 7;
            maxAmmo = 7;
            magazine = 28;
            timeToReload = 0.3f;
            timeToTacticalReload = 0.5f;
            accuracy = 0.8f;
            xScope = 1f;
            hRecoil = 1.8f;
            uRecoil = -32.5f;
            dRecoil = 35.6f;
            fireRate = 0.914f;
            damage = 27;

            //penetration = 1;

            gunMobility = 0.85f;
            gunADSMobility = 0.75f;

            overwriteDamageDrop = true;
            damageDropDistance = 0.25f;
            maxDropDistance = 0.6f;
            minDamageDrop = 0.5f;

            isShotgun = true;
            semiAuto = false;
            bulletsPerShot = 7;
            name = "SGCQB";
            manuallyReload = true;
            canBeTacticallyReloaded = false;

            yStability = 0.99f;
            xStability = 0.35f;
            fireSound = "SFX/guns/mossberg_590a_01_pump.wav";


            weaponClass = "Shotgun";
            arc = 18;
        }
    }
}
