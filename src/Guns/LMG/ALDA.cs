using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|LMG")]
    public class ALDA : GunDev
    {
        public ALDA(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/ALDA.png"), 48, 32, false);
            graphic = _sprite;
            _sprite.frame = 0;

            center = new Vec2(24f, 16f);
            collisionSize = new Vec2(46, 8);
            collisionOffset = new Vec2(-23f, -4f);
            barrel = new Vec2(22, -0.5f);

            ammo = 81;
            maxAmmo = 80;
            magazine = 160;
            timeToReload = 5.5f;
            timeToTacticalReload = 4.4f;
            accuracy = 0.57f;
            xScope = 1.25f;
            hRecoil = 5f;
            uRecoil = -10f;
            dRecoil = 12f;
            fireRate = 0.10501f;
            damage = 31;

            gunMobility = 0.8f;
            gunADSMobility = 0.3f;

            highPowered = true;
            penetration = 1;

            canBeTacticallyReloaded = false;
            name = "ALDA 5.56";

            yStability = 0.92f;
            xStability = 0.85f;
            fireSound = "SFX/guns/slug_01.wav";


            weaponClass = "LMG";
            arc = 28;
        }
    }
}
