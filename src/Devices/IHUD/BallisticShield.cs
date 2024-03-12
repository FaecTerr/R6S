using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Devices|IHUD")]
    public class BallisticShield : GunDev
    {
        public Color color;
        private SinWave _pulse = 0.1f;
        public int reqFrame = 0;

        public Knife shieldKnife;

        public BallisticShield(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/BallisticShield.png"), 32, 32, false);
            graphic = _sprite;
            center = new Vec2(16f, 16f);
            collisionSize = new Vec2(12f, 18f);
            collisionOffset = new Vec2(-6f, -9f);
            thickness = 10f;

            _holdOffset = new Vec2(6f, -4f);

            placeable = false;
            scannable = false;

            opened = false;
            
            scannable = false;
            electricible = false;

            setted = true;
            _sprite.AddAnimation("idle", 1f, true, new int[0]);

            gunADSMobility = 0.5f;
            gunMobility = 0.85f;

            jammResistance = true;
            mainHand = true;
            oneHand = true;
            canFire = false;
            shield = true;
            opened = false;

            holdback = false;

            cantProne = true;

            damage = 100;
            accuracy = 1;
            fireRate = 1;
            magazine = 1;
            ammo = 1;

            shieldKnife = new BallisticShieldKnife(position.x, position.y);
            _editorName = "BallisticShield";

            weaponClass = "Sheild";
        }

        public override void Update()
        {
            base.Update();
            if (!opened)
            {
                if(user != null && user.holdObject == this)
                {
                    if (user.sprinting && !holdback)
                    {
                        collisionSize = new Vec2(12f, 12f + 3f);
                        collisionOffset = new Vec2(-6f, -6f - 1.5f);
                    }
                    if (user.isADS)
                    {
                        collisionSize = new Vec2(12f, 12f + 6f * (1 - user.inADS));
                        collisionOffset = new Vec2(-6f, -6f - 3f * (1 - user.inADS));
                    }
                }
            }
        }
    }
}

