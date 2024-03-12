using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|Rifles")]
    public class G36 : GunDev
    {
        public G36(float xpos, float ypos) : base(xpos, ypos)
        {
            this._sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/G36.png"), 32, 32, false);
            this.graphic = this._sprite;
            this._sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(30, 8);
            collisionOffset = new Vec2(-15f, -4f);
            barrel = new Vec2(15, -0.5f);

            ammo = 31;
            maxAmmo = 30;
            magazine = 150;
            timeToReload = 2.8f;
            timeToTacticalReload = 2.1f;
            accuracy = 0.5f;
            xScope = 1.6f;
            hRecoil = 4.8f;
            uRecoil = -8.8f;
            dRecoil = 10.2f;
            fireRate = 0.07692f;
            damage = 38;
            penetration = 1;

            grip = 2;

            horizontalPattern = new List<float>()
            {
            0, 0.6f, -0.1f, -0.5f, 0.05f, -0.05f,
            0.1f, 0.4f, 0.3f, 0.4f, -0.2f,
            -0.05f, -0.05f, -0.05f, -0.1f, 0.05f,
            0.05f, -0.05f, -0.05f, -0.1f, 0.05f,
            0.05f, -0.05f, 0.05f, -0.1f, -0.3f,
            -0.5f, -0.6f, -0.7f, -0.7f, -0.7f
            };

            verticalPattern = new List<float>()
            {
            0, 0.75f, 0.75f, 0.75f, 0.75f, 0.75f,
            0.75f, 0.75f, 0.8f, 0.8f, 0.8f,
            0.8f, 0.8f, 0.95f, 0.85f, 0.9f,
            0.85f, 0.9f, 0.9f, 0.9f, 0.85f,
            0.9f, 0.95f, 0.95f, 0.95f, 0.95f,
            1.05f, 1f, 1.05f, 1.05f, 1.05f
            };

            canBeTacticallyReloaded = true;
            name = "G36";
            reticle = 11;

            yStability = 0.75f;
            xStability = 0.96f;
            fireSound = "SFX/guns/magnum_44_03.wav";


            weaponClass = "AR";
            arc = 26;
        }
    }
}
