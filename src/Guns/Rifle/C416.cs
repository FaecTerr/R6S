﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Weapon|Rifles")]
    public class C416 : GunDev
    {
        public C416(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Guns/C416.png"), 32, 32, false);
            graphic = _sprite;
            _sprite.frame = 0;

            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(30, 8);
            collisionOffset = new Vec2(-15f, -4f);
            barrel = new Vec2(14, -0.5f);

            ammo = 31;
            maxAmmo = 30;
            magazine = 150;
            timeToReload = 2.1f;
            timeToTacticalReload = 1.3f;
            accuracy = 0.55f;
            xScope = 1.5f;
            hRecoil = -4.5f;
            uRecoil = -13.3f;
            dRecoil = 11.8f;
            fireRate = 0.08108f;
            damage = 34;
            penetration = 1;

            canBeTacticallyReloaded = true;
            name = "Carabine 416";

            yStability = 0.92f;
            xStability = 0.87f;
            fireSound = "SFX/guns/marksman_rifle_03.wav";


            weaponClass = "AR";
            arc = 26;
        }
    }
}
