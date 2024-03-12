using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|Pistols")]
    public class FiveSeven : GunDev
    {
        public FiveSeven(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/FiveSeven.png"), 32, 32, false);
            graphic = _sprite;
            _sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(14, 8);
            collisionOffset = new Vec2(-7f, -4f);
            barrel = new Vec2(9, -0.5f);

            ammo = 21;
            maxAmmo = 20;
            magazine = 80;
            timeToReload = 2.7f;
            timeToTacticalReload = 2f;
            accuracy = 0.6f;
            xScope = 1.25f;
            hRecoil = 9.8f;
            uRecoil = -19f;
            dRecoil = 20f;
            fireRate = 0.13333f;
            damage = 42;

            gunMobility = 1f;
            gunADSMobility = 0.7f;

            _editorName = "FiveSeven";

            canBeTacticallyReloaded = true;
            name = "5.7 USG";
            semiAuto = false;
            oneHand = true;

            yStability = 0.75f;
            xStability = 0.95f;


            weaponClass = "Pistol";
            arc = 20;
        }
    }
}
