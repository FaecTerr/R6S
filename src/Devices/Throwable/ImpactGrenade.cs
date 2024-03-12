using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.R6S
{
    public class Impact : Throwable
    {
        /// <summary>
        /// Impact grenade, which explodes after a contact with a solid object. Inventory variant
        /// </summary>
        /// <param name="xpos"></param>
        /// <param name="ypos"></param>
        public Impact(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/Impact.png"), 16, 16, false);
            graphic = _sprite;
            _sprite.frame = 0;
            setTime = 0.5f;
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(10f, 10f);
            collisionOffset = new Vec2(-5f, -5f);
            scannable = false;
            jammResistance = true;

            UsageCount = 2;
            index = 1;
            delay = 0.5f;

            weightR = 7;
            DeviceCost = 10;
            descriptionPoints = "Impact grenade"; 
            minimalTimeOfHolding = 0.5f;
            needsToBeGentle = false;

            isSecondary = true;

        }

        public override void SetRocky()
        {
            rock = new ImpactRock(position.x, position.y);
            base.SetRocky();
        }
    }
    /// <summary>
    /// Impact grenade, which explodes after a contact with a solid object. Used and now flying towards you... Active variant in other words
    /// </summary>
    /// <param name="xpos"></param>
    /// <param name="ypos"></param>
    public class ImpactRock : Rocky
    {
        public bool used;
        public ImpactRock(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/Impact.png"), 16, 16, false);
            graphic = _sprite;
            setTime = 0.5f;
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(14f, 14f);
            collisionOffset = new Vec2(-7f, -7f);
            scannable = true;
            sticky = true;

            grounded = false;

            gravMultiplier = 0.8f;
            jammResistance = true;
        }


        public override void Update()
        {
            base.Update();
            if (grounded && !used)
            {
                used = true;
                Level.Add(new Explosion(position.x, position.y, 44, 60, "N") { shootedBy = oper });
                Level.Add(new Explosion(position.x, position.y, 26, 0, "S") { shootedBy = oper });

                DuckNetwork.SendToEveryone(new NMExplosion(position, 44, 60, "N", oper));
                DuckNetwork.SendToEveryone(new NMExplosion(position, 26, 0, "S", oper));

                Level.Add(new SoundSource(position.x, position.y, 320, "SFX/explo/impact_blast.wav", "J"));
                DuckNetwork.SendToEveryone(new NMSoundSource(position, 320, "SFX/explo/explosion_barrel.wav", "J"));

                Level.Remove(this);
            }
        }

        public override void Impact(MaterialThing with, ImpactedFrom from)
        {
            if (with != null)
            {
                if (with is Block && !used)
                {
                    used = true;
                    Level.Add(new Explosion(position.x, position.y, 44, 60, "N") { shootedBy = oper });
                    Level.Add(new Explosion(position.x, position.y, 26, 0, "S") { shootedBy = oper });

                    DuckNetwork.SendToEveryone(new NMExplosion(position, 44, 60, "N", oper));
                    DuckNetwork.SendToEveryone(new NMExplosion(position, 26, 0, "S", oper));

                    Level.Add(new SoundSource(position.x, position.y, 320, "SFX/explo/explosion_barrel.wav", "J"));
                    DuckNetwork.SendToEveryone(new NMSoundSource(position, 320, "SFX/explo/explosion_barrel.wav", "J"));

                    base.Impact(with, from);

                    Level.Remove(this);
                }
            }
        }
    }
}
