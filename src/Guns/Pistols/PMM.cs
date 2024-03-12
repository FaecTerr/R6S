using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|Pistols")]
    public class PMM : GunDev
    {
        public PMM(float xpos, float ypos) : base(xpos, ypos)
        {
            this._sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/PMM.png"), 32, 32, false);
            this.graphic = this._sprite;
            this._sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(14, 8);
            collisionOffset = new Vec2(-7f, -4f);
            barrel = new Vec2(10, -0.5f);

            ammo = 9;
            maxAmmo = 8;
            magazine = 80;
            timeToReload = 1.4f;
            timeToTacticalReload = 0.9f;
            accuracy = 0.6f;
            xScope = 1.25f;
            hRecoil = 4.8f;
            uRecoil = -6.5f;
            dRecoil = 7.6f;
            fireRate = 0.13333f;
            damage = 63;

            gunMobility = 1f;
            gunADSMobility = 0.7f;

            canBeTacticallyReloaded = true;
            name = "PMM";
            semiAuto = false;
            oneHand = true;

            yStability = 0.85f;
            xStability = 0.75f;
            fireSound = "SFX/guns/PMM.wav";

            weaponClass = "Pistol";
        }
    }
}
