using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|Pistols")]
    public class Meusoc : GunDev
    {
        public Meusoc(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/Meusoc.png"), 32, 32, false);
            graphic = _sprite;
            _sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(14, 8);
            collisionOffset = new Vec2(-7f, -4f);
            barrel = new Vec2(10, -0.5f);

            ammo = 8;
            maxAmmo = 7;
            magazine = 49;
            timeToReload = 2.4f;
            timeToTacticalReload = 1.9f;
            accuracy = 0.5f;
            xScope = 1.25f;
            hRecoil = 4f;
            uRecoil = -8.8f;
            dRecoil = 15.5f;
            fireRate = 0.13333f;
            damage = 58;

            gunMobility = 1f;
            gunADSMobility = 0.7f;

            grip = 2;

            canBeTacticallyReloaded = true;
            name = "M45 MEU SOC";
            semiAuto = false;
            oneHand = true;

            yStability = 0.75f;
            xStability = 0.85f;
            fireSound = "SFX/guns/glock_17_01.wav";


            weaponClass = "Pistol";
            arc = 40;
        }
    }
}
