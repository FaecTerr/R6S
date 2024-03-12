using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|Rifles")]
    public class ARX200 : GunDev
    {
        public ARX200(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/ARX200.png"), 32, 32, false);
            graphic = _sprite;
            _sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(30, 8);
            collisionOffset = new Vec2(-15f, -4f);
            barrel = new Vec2(15, -0.5f);

            ammo = 21;
            maxAmmo = 20;
            magazine = 120;
            timeToReload = 2.8f;
            timeToTacticalReload = 2.2f;
            accuracy = 0.65f;
            xScope = 1.75f;
            hRecoil = 4.2f;
            uRecoil = -5.2f;
            dRecoil = 8.5f;
            fireRate = 0.08571f;
            damage = 47;
            penetration = 1;

            canBeTacticallyReloaded = true;
            name = "ARX200";
            reticle = 10;

            grip = 1;

            yStability = 0.76f;
            xStability = 0.96f;
            fireSound = "SFX/guns/mcx300_01.wav";

            weaponClass = "AR"; 
            arc = 26;
        }
    }
}
