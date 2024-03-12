using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|Pistols")]
    public class P226 : GunDev
    {
        public P226(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/P226.png"), 32, 32, false);
            graphic = _sprite;
            _sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(14, 8);
            collisionOffset = new Vec2(-7f, -4f);
            barrel = new Vec2(10, -0.5f);

            ammo = 13;
            maxAmmo = 12;
            magazine = 60;
            timeToReload = 2.7f;
            timeToTacticalReload = 2.2f;
            accuracy = 0.55f;
            xScope = 1.25f;
            hRecoil = 9.4f;
            uRecoil = -10.5f;
            dRecoil = 14.6f;
            fireRate = 0.13333f;
            damage = 50;

            gunMobility = 1f;
            gunADSMobility = 0.7f;

            canBeTacticallyReloaded = true;
            name = "P226";
            semiAuto = false;
            oneHand = true;

            yStability = 0.95f;
            xStability = 0.85f;
            fireSound = "SFX/guns/p226_01.wav";

            weaponClass = "Pistol";
            arc = 20;
        }
    }
}
