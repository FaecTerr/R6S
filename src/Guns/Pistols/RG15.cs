using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|Pistols")]
    public class RG15 : GunDev
    {
        public RG15(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/RG15.png"), 32, 32, false);
            graphic = _sprite;
            _sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(14, 8);
            collisionOffset = new Vec2(-7f, -4f);
            barrel = new Vec2(10, -0.5f);

            ammo = 16;
            maxAmmo = 15;
            magazine = 90;
            timeToReload = 2.1f;
            timeToTacticalReload = 1.4f;
            accuracy = 0.55f;
            xScope = 1.8f;
            hRecoil = 3.4f;
            uRecoil = -5.1f;
            dRecoil = 8.1f;
            fireRate = 0.13333f;
            damage = 38;

            gunMobility = 1f;
            gunADSMobility = 0.7f;

            grip = 2;
            underGrip = 1;

            canBeTacticallyReloaded = true;
            name = "RG 15";
            semiAuto = false;
            oneHand = true;

            yStability = 0.92f;
            xStability = 0.92f;
            fireSound = "SFX/guns/p226_01.wav";


            weaponClass = "Pistol";
        }
    }
}
