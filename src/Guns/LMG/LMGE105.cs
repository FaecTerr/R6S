using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|LMG")]
    public class LMGE105 : GunDev
    {
        public LMGE105(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/LMGE105.png"), 32, 32, false);
            graphic = _sprite;
            _sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(30, 8);
            collisionOffset = new Vec2(-15f, -4f);
            barrel = new Vec2(22, -0.5f);

            ammo = 150;
            maxAmmo = 150;
            magazine = 150;
            timeToReload = 6.8f;
            timeToTacticalReload = 6.3f;
            accuracy = 0.65f;
            xScope = 1.25f;
            hRecoil = 12.3f;
            uRecoil = -9.4f;
            dRecoil = 12f;
            fireRate = 0.12f;
            damage = 30;
            penetration = 1;

            gunMobility = 0.8f;
            gunADSMobility = 0.3f;

            canBeTacticallyReloaded = false;
            name = "LMG-E";
            reticle = 14;


            yStability = 0.75f;
            xStability = 0.85f;
            fireSound = "SFX/guns/M249_01.wav";


            weaponClass = "LMG";
            arc = 40;
        }
    }
}
