using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|Rifles")]
    public class F2 : GunDev
    {
        public F2(float xpos, float ypos) : base(xpos, ypos)
        {
            this._sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/F2.png"), 32, 32, false);
            this.graphic = this._sprite;
            this._sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(30, 8);
            collisionOffset = new Vec2(-15f, -4f);
            barrel = new Vec2(14, -0.5f);

            ammo = 26;
            maxAmmo = 25;
            magazine = 125;
            timeToReload = 3.3f;
            timeToTacticalReload = 3f;
            accuracy = 0.5f;
            xScope = 2.5f;
            hRecoil = 6f;
            uRecoil = -10.1f;
            dRecoil = 14.8f;
            fireRate = 0.06122f;
            damage = 37;
            penetration = 1;

            horizontalPattern = new List<float>()
            {
            0, 0.1f, 0.1f, 0.5f, 0.05f, 0.05f,
            0.1f, 0.4f, 0.6f, 0.4f, 0.2f,
            0.05f, 0.05f, 0.05f, 0.1f, 0.05f,
            0.05f, 0.05f, 0.05f, 0.1f, 0.05f,
            0.05f, 0.05f, 0.05f, 0.1f, 0.3f,
            0.5f, 0.6f, 0.6f, 0.6f, 0.6f
            };

            verticalPattern = new List<float>()
            {
            0, 0.75f, 0.75f, 0.8f, 0.85f, 0.8f,
            0.85f, 0.85f, 0.85f, 0.85f, 0.85f,
            0.9f, 0.9f, 0.85f, 0.85f, 0.9f,
            0.85f, 0.9f, 0.9f, 0.85f, 0.85f,
            0.9f, 0.85f, 0.85f, 0.9f, 0.9f,
            0.95f, 0.9f, 0.9f, 0.9f, 0.9f
            };

            canBeTacticallyReloaded = true;
            name = "F2";

            yStability = 0.65f;
            xStability = 0.3f;
            fireSound = "SFX/guns/mp9SD_03.wav";


            weaponClass = "AR";
            arc = 26;
        }
    }
}
