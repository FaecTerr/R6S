using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|Rifles")]
    public class R4C : GunDev
    {
        public R4C(float xpos, float ypos) : base(xpos, ypos)
        {
            this._sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/R4C.png"), 32, 32, false);
            this.graphic = this._sprite;
            this._sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(30, 8);
            collisionOffset = new Vec2(-15f, -4f);
            barrel = new Vec2(15, -0.5f);

            ammo = 31;
            maxAmmo = 30;
            magazine = 150;
            timeToReload = 3.6f;
            timeToTacticalReload = 2.9f;
            accuracy = 0.4f;
            xScope = 1.25f;
            hRecoil = 9.3f;
            uRecoil = -17.6f;
            dRecoil = 19.7f;
            fireRate = 0.06977f;
            damage = 39;

            penetration = 1f;
            
            canBeTacticallyReloaded = true;
            name = "R4-C";
            reticle = 11;

            horizontalPattern = new List<float>()
            {
            0, 0.6f, -0.1f, -0.5f, 0.05f, -0.05f,
            -0.1f, -0.4f, -0.3f, -0.4f, -0.2f,
            -0.05f, 0.05f, -0.05f, 0.1f, 0.05f,
            0.05f, 0.05f, -0.05f, 0.1f, 0.05f,
            0.05f, -0.05f, 0.05f, 0.1f, 0.3f,
            0.5f, 0.6f, 0.7f, 0.7f, 0.7f
            };

            verticalPattern = new List<float>()
            {
            0, 0.6f, 0.6f, 0.6f, 0.6f, 0.6f,
            0.75f, 0.75f, 0.75f, 0.75f, 0.8f,
            0.8f, 0.8f, 0.95f, 0.85f, 0.9f,
            0.85f, 0.9f, 0.9f, 0.85f, 0.85f,
            0.9f, 0.95f, 0.95f, 0.9f, 0.9f,
            1.05f, 1f, 1.1f, 1.1f, 1.1f
            };

            yStability = 0.78f;
            xStability = 0.92f;
            fireSound = "SFX/guns/ak47_01.wav";


            weaponClass = "AR";
            arc = 26;
        }
    }
}
