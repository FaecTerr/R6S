using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|Shotguns")]
    public class M870 : GunDev
    {
        public M870(float xpos, float ypos) : base(xpos, ypos)
        {
            this._sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/M870.png"), 32, 32, false);
            this.graphic = this._sprite;
            this._sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(30, 8);
            collisionOffset = new Vec2(-15f, -4f);
            barrel = new Vec2(15, -0.5f);

            ammo = 7;
            maxAmmo = 7;
            magazine = 28;
            timeToReload = 0.6f;
            timeToTacticalReload = 0.3f;
            accuracy = 0.8f;
            xScope = 1f;
            hRecoil = 1.8f;
            uRecoil = -52.5f;
            dRecoil = 55.6f;
            fireRate = 0.7f;
            damage = 30;

            //penetration = 1;

            gunMobility = 0.85f;
            gunADSMobility = 0.75f;

            overwriteDamageDrop = true;
            damageDropDistance = 0.15f;
            maxDropDistance = 0.45f;
            minDamageDrop = 0.25f;

            isShotgun = true;
            semiAuto = false;
            bulletsPerShot = 10;
            canBeTacticallyReloaded = false;
            name = "M870";
            manuallyReload = true;

            yStability = 0.95f;
            xStability = 0.35f;

            fireSound = "SFX/guns/remington_870_01_pump.wav";
            reloadSound = "SFX/guns/reload_shotgun_shell.wav";
            tacticalReloadSound = "SFX/guns/reload_shotgun_shell.wav";


            weaponClass = "Shotgun";
            arc = 24;
        }
    }
}
