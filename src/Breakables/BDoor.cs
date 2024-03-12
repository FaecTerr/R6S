using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Furniture")]
    public class BreakableDoor : Block
    {
        public SpriteMap _sprite;
        public int Health = 10;
        public bool untouched = true;
        public int typ;

        public BreakableDoor(float xval, float yval) : base(xval, yval)
        {
            _enablePhysics = false;
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Decorations/BDoor.png"), 10, 32, false);
            base.graphic = _sprite;
            center = new Vec2(5f, 16f);
            _sprite.frame = 2;
            collisionOffset = new Vec2(-5f, -16f);
            collisionSize = new Vec2(10f, 32f);
            graphic = _sprite;
            hugWalls = WallHug.Floor;
        }

        public override void Update()
        {
            if(Health <= 0)
            {
                Level.Remove(this);
            }
            if (untouched)
            {
                if(Level.CheckLine<Furniture>(position - new Vec2(8f, -4f), position + new Vec2(8f, 4f)) != null)
                {
                    untouched = false;
                    Health = 100;
                }

            }
            base.Update();
        }

        public override bool Hit(Bullet bullet, Vec2 hitPos)
        {
            Health -= 1;
            return base.Hit(bullet, hitPos);
        }

        public virtual void Damaged()
        {
            untouched = false;
            Health -= 40;
            if (typ == 0)
            {
                SFX.Play(Mod.GetPath<R6S>("SFX/player_hit_door_a.wav"), 1f);
            }
            if (typ == 1)
            {
                SFX.Play(Mod.GetPath<R6S>("SFX/player_hit_door_b.wav"), 1f);
            }
            if (typ == 2)
            {
                SFX.Play(Mod.GetPath<R6S>("SFX/player_hit_door_c.wav"), 1f);
            }
            typ++;
            if(typ == 3)
            {
                typ = 0;
            }
        }


        public virtual void Exploded()
        {
            untouched = false;
            Health -= 200;
            SFX.Play(Mod.GetPath<R6S>("SFXplayer_hit_door_a.wav"), 1f);
        }
    }
}
