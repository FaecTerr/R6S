using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|Pistols")]
    public class P229 : GunDev
    {
        public P229(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/P229.png"), 32, 32, false);
            graphic = _sprite;
            _sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(14, 8);
            collisionOffset = new Vec2(-7f, -4f);
            barrel = new Vec2(10, -0.5f);

            ammo = 13;
            maxAmmo = 12;
            magazine = 60;
            timeToReload = 2.8f;
            timeToTacticalReload = 2.3f;
            accuracy = 0.67f;
            xScope = 1.25f;
            hRecoil = 8f;
            uRecoil = -9.5f;
            dRecoil = 14.6f;
            fireRate = 0.13333f;
            damage = 44;

            gunMobility = 1f;
            gunADSMobility = 0.7f;

            canBeTacticallyReloaded = true;
            name = "P229";
            semiAuto = false;
            oneHand = true;

            yStability = 0.96f;
            xStability = 0.85f;
            fireSound = "SFX/guns/p226_02.wav";


            weaponClass = "Pistol";
            arc = 16;
        }
    }
}
