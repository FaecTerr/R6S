using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|Pistols")]
    public class Mag44 : GunDev
    {
        public Mag44(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/44Mag.png"), 32, 32, false);
            graphic = _sprite;
            _sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(14, 8);
            collisionOffset = new Vec2(-7f, -4f);
            barrel = new Vec2(10, -0.5f);

            ammo = 8;
            maxAmmo = 7;
            magazine = 70;
            timeToReload = 2.3f;
            timeToTacticalReload = 1.8f;
            accuracy = 0.65f;
            xScope = 2.75f;
            hRecoil = 3.4f;
            uRecoil = -4.1f;
            dRecoil = 4.1f;
            fireRate = 0.1333f;
            damage = 54;
            penetration = 1.5f;

            minDamageDrop = 0.78f;

            gunMobility = 1f;
            gunADSMobility = 0.6f;

            grip = 2;

            canBeTacticallyReloaded = true;
            name = ".44 Mag";
            semiAuto = false;
            oneHand = true;

            yStability = 0.95f;
            xStability = 0.92f;
            fireSound = "SFX/guns/p226_01.wav";


            weaponClass = "Pistol";
            arc = 20;
        }
    }
}
