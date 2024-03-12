using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|Rifles")]
    public class AUGa2: GunDev
    {
        public AUGa2(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/AUGa2.png"), 32, 32, false);
            graphic = _sprite;
            _sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(30, 8);
            collisionOffset = new Vec2(-15f, -4f);
            barrel = new Vec2(14, -0.5f);

            ammo = 31;
            maxAmmo = 30;
            magazine = 150;
            timeToReload = 2.5f;
            timeToTacticalReload = 2f;
            accuracy = 0.6f;
            xScope = 2f;
            hRecoil = 9.5f;
            uRecoil = -15.5f;
            dRecoil = 19.5f;
            fireRate = 0.0834f;
            damage = 42;
            penetration = 1;

            canBeTacticallyReloaded = true;
            name = "AUG A2";

            yStability = 0.95f;
            xStability = 0.9f;
            fireSound = "SFX/guns/marksman_rifle_03.wav";


            weaponClass = "AR";
        }
    }
}
