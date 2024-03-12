using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    //[EditorGroup("Faecterr's|Devices|Throwable")]
    public class MagNet : Throwable
    {
        public MagNet(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/MagNet.png"), 16, 16, false);
            graphic = _sprite;
            _sprite.frame = 0;
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(14f, 14f);
            collisionOffset = new Vec2(-7f, -7f);
            setTime = 0.8f;

            sticky = true;

            setFramesLoad = 160;

            weightR = 7;
            UsageCount = 5; 
            minimalTimeOfHolding = 0.5f;
            needsToBeGentle = false;
        }

        public override void SetRocky()
        {
            rock = new MagNetAP(position.x, position.y);
            base.SetRocky();
        }
      
    }
    public class MagNetAP : Rocky
    {
        protected Sprite _sightHit;
        public int destroyFrames = 30;
        public Vec2 gPos;

        public Rocky cat;

        public MagNetAP(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(Mod.GetPath<R6S>("Sprites/Devices/MagNet.png"), 16, 16, false);
            graphic = this._sprite;
            _sprite.frame = 0;
            center = new Vec2(8f, 8f);
            collisionSize = new Vec2(14f, 14f);
            collisionOffset = new Vec2(-7f, -7f);
            setTime = 0.8f;
            sticky = true;

            scannable = true;

            placeable = false;
            breakable = true;
        }

        public override void DetonateFull()
        {
            
        }

        public override void Update()
        {
            /*List<Rocky> exception = new List<Rocky>();
            exception.Add(this);

            foreach(Rocky r in Level.CheckCircleAll<Rocky>(position, 320f))
            {
                if(r is ObservationThing)
                {
                    exception.Add(r);
                }
            }*/

            Rocky g = Level.Nearest<Rocky>(this.x, this.y, this);
            foreach (Rocky gr in Level.CheckCircleAll<Rocky>(position, 160f))
            {
                if (Level.CheckLine<Block>(gr.position, position) == null && !gr.catched && !(gr is ObservationThing) && g != null && gr != this)
                {
                    if (gr.isGrenade && gr.team != team)
                    {
                        g = gr;
                    }
                }
            }
            if (catched == true)
            {
                if (destroyFrames > 0)
                {
                    destroyFrames--;
                }
                else
                {
                    Level.Remove(this);
                }
            }

            if (setFrames < 10)
            {
                if (g != null)
                {
                    if (Level.CheckLine<Block>(g.position, position) == null && !g.catched && g.isGrenade && !(g is ObservationThing) && g.team != team && g != this && (position - g.position).length < 160f)
                    {
                        g.position += (position - g.position) / 8;
                        if (catched == false)
                        {
                            cat = g;
                            g.hSpeed = 0;
                            g.vSpeed = 0;
                            g.gravMultiplier = 0;
                            catched = true;
                            destroyFrames = 65;
                            g.time = 1f;
                            g.catched = true;
                        }
                    }
                }
            }

            if(cat != null)
            {
                g.position += (position - g.position) / 8;
                g.hSpeed = 0;
                g.vSpeed = 0;
                g.gravMultiplier = 0;
            }

            base.Update();
        }

        public override void Draw()
        {
            base.Draw();
            if (setFrames > 0 && setted)
            {
                //text = "Projectile redirection is now active";
                Graphics.DrawString(text, new Vec2(position.x - this.text.Length * 2, position.y), Color.White, 1f, null, 0.5f);
                Graphics.DrawCircle(this.position, 160f - setFrames, Color.White, 2f, 1f, 32);
                setFrames -= 4;
            }
        }
    }
}
