using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|LMG")]
    public class G8A1 : GunDev
    {
        public G8A1(float xpos, float ypos) : base(xpos, ypos)
        {
            this._sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/G8A1.png"), 48, 32, false);
            this.graphic = this._sprite;
            this._sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(30, 8);
            collisionOffset = new Vec2(-15f, -4f);
            barrel = new Vec2(22, -0.5f);

            ammo = 51;
            maxAmmo = 50;
            magazine = 100;
            timeToReload = 4.6f;
            timeToTacticalReload = 4f;
            accuracy = 0.47f;
            xScope = 1.5f;
            hRecoil = 9f;
            uRecoil = -16f;
            dRecoil = 20f;
            fireRate = 0.0706f;
            damage = 37;
            penetration = 1;

            gunMobility = 0.9f;
            gunADSMobility = 0.35f;

            name = "G8A1";

            yStability = 0.9f;
            xStability = 0.85f;
            fireSound = "SFX/guns/marksman_rifle_03.wav";


            weaponClass = "LMG";
            arc = 40;
        }
    }
}
