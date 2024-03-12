using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|Pistols")]
    public class Bailiff : GunDev
    {
        public Bailiff(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/Bailiff.png"), 32, 32, false);
            graphic = _sprite;
            _sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(14, 8);
            collisionOffset = new Vec2(-7f, -4f);
            barrel = new Vec2(10, -0.5f);

            ammo = 5;
            maxAmmo = 5;
            magazine = 30;
            timeToReload = 3.5f;
            accuracy = 0.9f;
            xScope = 1.25f;
            hRecoil = 6.8f;
            uRecoil = -55.2f;
            dRecoil = 56.8f;
            fireRate = 0.13333f;
            damage = 30;

            gunMobility = 1f;
            gunADSMobility = 0.7f;

            //isShotgun = true;
            bulletsPerShot = 4;
            canBeTacticallyReloaded = false;
            name = "Bailiff 410";
            semiAuto = false;
            oneHand = true;

            highPowered = true;
            penetration = 0.8f;

            overwriteDamageDrop = true;
            damageDropDistance = 0.1f;
            maxDropDistance = 0.25f;
            minDamageDrop = 0.40f;


            yStability = 0.97f;
            xStability = 0.97f;
            fireSound = "SFX/guns/magnum_357_02.wav";


            weaponClass = "Pistol";
            arc = 24;
        }
    }
}
