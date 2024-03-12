using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|Pistols")]
    public class P12 : GunDev
    {
        public P12(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/P12.png"), 32, 32, false);
            graphic = _sprite;
            _sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(14, 8);
            collisionOffset = new Vec2(-7f, -4f);
            barrel = new Vec2(10, -0.5f);

            ammo = 16;
            maxAmmo = 15;
            magazine = 80;
            timeToReload = 2.7f;
            timeToTacticalReload = 1.9f;
            accuracy = 0.65f;
            xScope = 1.25f;
            hRecoil = 6.8f;
            uRecoil = -11.5f;
            dRecoil = 17.6f;
            fireRate = 0.13333f;
            damage = 44;

            gunMobility = 1f;
            gunADSMobility = 0.7f;

            underGrip = 1;
            grip = 2;

            canBeTacticallyReloaded = true;
            name = "P12";
            semiAuto = false;
            oneHand = true;

            depth = 0.55f;

            yStability = 0.75f;
            xStability = 0.85f;
            fireSound = "SFX/guns/p226_01.wav";

            weaponClass = "Pistol";
        }
    }
}
