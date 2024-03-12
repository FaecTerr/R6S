using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|SMG")]
    public class Scorpion : GunDev
    {
        public Scorpion(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/Scorpion.png"), 32, 32, false);
            graphic = _sprite;
            _sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(30, 8);
            collisionOffset = new Vec2(-15f, -4f);
            barrel = new Vec2(15, -0.5f);

            ammo = 41;
            maxAmmo = 40;
            magazine = 160;
            timeToReload = 2.8f;
            timeToTacticalReload = 2.1f;
            accuracy = 0.75f;
            xScope = 1.25f;
            hRecoil = 6.6f;
            uRecoil = -22.2f;
            dRecoil = 16.8f;
            fireRate = 0.05555f;
            damage = 23;

            canBeTacticallyReloaded = true;
            name = "SCORPION EVO3 A1";
            reticle = 6;

            penetration = 1;

            yStability = 0.65f;
            xStability = 0.85f;

            fireSound = "SFX/guns/mp5_03.wav";

            weaponClass = "SMG";

            horizontalPattern = new List<float>()
            {
            0, -0.6f, -0.6f, 0.6f, -0.6f, -0.6f,
            0.6f, 0.6f, -0.6f, -0.6f, 0.6f,
            -0.6f, -0.6f, 0.6f, -0.6f, -0.6f,
            0.6f, 0.6f, -0.6f, -0.6f, 0.6f,
            -0.5f, 0.5f, 0.5f, -0.4f, 0.4f,
            -0.3f, 0.3f, -0.3f, -0.2f, -0.2f,
            -0.2f, -0.2f, -0.2f, -0.2f, -0.2f,
            -0.2f, -0.2f, -0.2f, -0.2f, -0.2f,
            -0.2f
            };

            verticalPattern = new List<float>()
            {
            0.5f, 0.6f, 0.7f, 0.6f, 0.5f, 0.6f,
            0.6f, 0.7f, 0.6f, 0.5f, 0.6f,
            0.7f, 0.6f, 0.5f, 0.6f, 0.7f,
            0.6f, 0.5f, 0.6f, 0.7f, 0.7f,
            0.7f, 0.7f, 0.45f, 0.45f, 0.45f,
            0.45f, 0.45f, 0.45f, 0.45f, 0.45f,
            0.3f, 0.3f, 0.3f, 0.3f, 0.3f,
            0.3f, 0.3f, 0.3f, 0.3f, 0.3f,
            0.3f
            };

        }
    }
}
