using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|SMG")]
    public class T5SMG : GunDev
    {
        public T5SMG(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/T5SMG.png"), 32, 32, false);
            graphic = _sprite;
            _sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(30, 8);
            collisionOffset = new Vec2(-15f, -4f);
            barrel = new Vec2(12, -0.5f);

            ammo = 31;
            maxAmmo = 30;
            magazine = 150;
            timeToReload = 4f;
            timeToTacticalReload = 3.2f;
            accuracy = 0.65f;
            xScope = 1.5f;
            hRecoil = 3.4f;
            uRecoil = -6.5f;
            dRecoil = 5.4f;
            fireRate = 0.0666667f;
            damage = 30;

            grip = 1;
            muzzle = 2;

            penetration = 1;

            canBeTacticallyReloaded = true;
            name = "T-5 SMG";
            reticle = 7;

            yStability = 0.98f;
            xStability = 0.98f;
            fireSound = "SFX/guns/mp5SD_03.wav";

            horizontalPattern = new List<float>()
            {
            0, 1, -1, 1, -1, 1,
            1, 1, -1, 1, 1,
            0.8f, -0.2f, 0.8f, 1, 1,
            0.8f, 1, 0.9f, 1, 0.8f,
            0.6f, -0.2f, -1, -0.5f, 0.7f,
            1, 0.8f, 1, 0.7f, 1
            };

            verticalPattern = new List<float>()
            {
            0, 0.65f, 0.65f, 0.65f, 0.65f, 0.7f,
            0.7f, 0.7f, 0.7f, 0.75f, 0.75f,
            0.75f, 0.75f, 0.8f, 0.8f, 0.8f,
            0.8f, 0.8f, 0.8f, 0.8f, 0.85f,
            0.85f, 0.85f, 0.85f, 0.85f, 0.85f,
            0.85f, 0.85f, 0.95f, 0.9f, 0.9f
            };

            weaponClass = "SMG";
        }
    }
}
