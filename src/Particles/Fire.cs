using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    /*public class LandFire : PhysicsObject, IDrawToDifferentLayers
    {
        public SpriteMap _sprite;
        public float lifeTime = 15f;
        public Operators oper;

        public float alphaMod = 1;
        private int time;

        public float firePound = 8;

        public LandFire(float xpos, float ypos, float Timer) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Sprites/LandFire"), 16, 16);
            _sprite.CenterOrigin();
            lifeTime = Timer;
            collisionSize = new Vec2(8f, 8f);
            collisionOffset = new Vec2(-4f, -4f);
            center = new Vec2(8f, 12f);
            //graphic = _sprite;
            weight = 0f;
        }

        public void OnDrawLayer(Layer pLayer)
        {
            if (pLayer == Layer.Game)
            {
                if (firePound > collisionSize.x)
                {
                    float openToTheRight = 0;
                    float openToTheLeft = 0;

                    if (Level.CheckRay<Block>(new Vec2(position.x, bottom - 1), new Vec2(position.x, bottom - 1) + new Vec2(firePound)) != null)
                    {
                        float bposx = Level.CheckRay<Block>(new Vec2(position.x, bottom - 2), new Vec2(position.x, bottom - 2) + new Vec2(firePound - 4)).position.x;
                        openToTheRight = Math.Abs(bposx - position.x);
                        //Graphics.DrawLine(new Vec2(position.x, bottom - 2), new Vec2(position.x + openToTheRight, bottom - 2), Color.Blue * 0.5f);
                        //Graphics.DrawRect(new Vec2(bposx - 1, bottom - 3), new Vec2(bposx + 1, bottom -1), Color.Red * 0.5f);
                    }
                    else
                    {
                        openToTheRight = firePound;

                        //Graphics.DrawLine(new Vec2(position.x, bottom - 2), new Vec2(position.x + openToTheRight, bottom - 2), Color.Blue * 0.5f);
                        //Graphics.DrawRect(new Vec2(position.x + openToTheRight - 1, bottom - 3), new Vec2(position.x + openToTheRight + 1, bottom - 1), Color.Red * 0.5f);
                    }

                    if (Level.CheckRay<Block>(new Vec2(position.x, bottom - 1), new Vec2(position.x, bottom - 1) - new Vec2(firePound)) != null)
                    {
                        float bposx = Level.CheckRay<Block>(new Vec2(position.x, bottom - 2), new Vec2(position.x, bottom - 2) - new Vec2(firePound - 4)).position.x;
                        openToTheLeft = Math.Abs(bposx - position.x);

                        //Graphics.DrawLine(new Vec2(position.x, bottom - 2), new Vec2(position.x - openToTheLeft, bottom - 2), Color.Blue * 0.5f);
                        //Graphics.DrawRect(new Vec2(bposx - 1, bottom - 3), new Vec2(bposx + 1, bottom - 1), Color.Red * 0.5f);
                    }
                    else
                    {
                        openToTheLeft = firePound;
                        //Graphics.DrawLine(new Vec2(position.x, bottom - 2), new Vec2(position.x - openToTheLeft, bottom - 2), Color.Blue * 0.5f);
                        //Graphics.DrawRect(new Vec2(position.x - openToTheLeft - 1, bottom - 3), new Vec2(position.x - openToTheLeft + 1, bottom - 1), Color.Red * 0.5f);
                    }

                    if (openToTheLeft + openToTheRight > firePound)
                    {
                        float norm = (openToTheLeft + openToTheRight) / firePound;
                        openToTheRight /= norm;
                        openToTheLeft /= norm;
                    }
                    if (openToTheLeft + openToTheRight < 16)
                    {
                        openToTheLeft = 8;
                        openToTheRight = 8;
                    }

                    float totalOffset = (openToTheRight - openToTheLeft) * 0.5f;
                    collisionSize = new Vec2(openToTheRight + openToTheLeft, 8);
                    collisionOffset = new Vec2(-(openToTheRight + openToTheLeft) * 0.5f, -4);
                    position.x += totalOffset;
                }
                else
                {
                    collisionSize = new Vec2(firePound, 8);
                    collisionOffset = new Vec2(-firePound * 0.5f, -4);
                }
                if (grounded)
                {
                    Material m = Graphics.material;
                    _sprite.CenterOrigin();
                    Graphics.material = new MaterialWiggle(_sprite);
                    _sprite.alpha = alpha;

                    for (int i = 0; i < (int)(collisionSize.x / 8); i++)
                    {
                        _sprite.frame = Rando.Int(3);
                        Graphics.Draw(_sprite, topLeft.x + 8 + 8 * i, position.y - 4);
                    }
                    //Graphics.Draw(_sprite, position.x, position.y - 4);
                    Graphics.material = m;
                }
                //Graphics.DrawLine(new Vec2(position.x - firePound, bottom - 2), new Vec2(position.x + firePound, bottom - 2), Color.Green * 0.5f, 0.99f);
            }
        }

        public override void Update()
        {
            base.Update();
            if (lifeTime > 0f)
            {
                if (!grounded && Level.CheckRect<Block>(topLeft, bottomRight) != null)
                {
                    UpdatePhysics();
                    grounded = true;
                }

                lifeTime -= 0.01666666f;
                if (lifeTime < 1)
                {
                    alpha = alpha * alphaMod;
                    _sprite.alpha = _sprite.alpha * alphaMod;
                    alphaMod -= 0.016f;
                    if (alphaMod < 0)
                    {
                        alphaMod = 0;
                    }
                }

                if (Level.CheckRect<Block>(topLeft, bottomRight) != null)
                {
                    FixClipping();
                    UpdatePhysics();
                }

                if (time == 0)
                {
                    time = Rando.Int(8, 14) * 4;
                    Level.Add(new SoundSource(position.x, position.y, 120, "SFX/Devices/FireSound.wav", "J"));
                    DuckNetwork.SendToEveryone(new NMSoundSource(position, 120, "SFX/Devices/FireSound.wav", "J"));
                }
                else
                {
                    time--;
                }

                for (int i = 0; i < (int)(collisionSize.x / 4); i++)
                {
                    if (Rando.Float(1) > 0.98f)
                    {
                        Level.Add(new FireSparkle(left + i * 4, position.y));
                    }
                }

            }
            else
            {
                Level.Remove(this);
            }
        }
    }*/
    public class LandFire : PhysicsObject, IDrawToDifferentLayers
    {
        public SpriteMap _sprite;
        public float lifeTime = 15f;
        public Operators oper;

        public float alphaMod = 1;
        private int time;

        public float firePound = 8;

        //public bool doMakeSound = Rando.Float(1) > 0.5f ? true : false;
        public bool doMakeSound;
        public LandFire(float xpos, float ypos, float Timer) : base(xpos, ypos)
        {
            lifeTime = Timer;
            collisionSize = new Vec2(8f, 8f);
            collisionOffset = new Vec2(-4f, -4f);
            center = new Vec2(8f, 12f);
            _sprite = new SpriteMap(GetPath("Sprites/LandFire.png"), 16, 16);
            //graphic = _sprite;
            _sprite.AddAnimation("idle", 0.2f, true, new int[] {
                0,
                1,
                2,
                3
            });
            _sprite.AddAnimation("none", 1f, true, new int[] {
                4
            });
            weight = 0f;
            _sprite.SetAnimation("idle");
            _sprite.frame = Rando.Int(3);
            material = new MaterialWiggle(_sprite);
            graphic = _sprite;
        }

        public void OnDrawLayer(Layer pLayer)
        {
            if (pLayer == Layer.Game)
            {
                //Material m = Graphics.material;
                //OldEnergyScimi old = new OldEnergyScimi(position.x, position.y);
                //Graphics.material = new MaterialEnergyBlade(old);
                //_sprite.CenterOrigin();
                //Graphics.material = new MaterialWiggle(_sprite);
                //_sprite.alpha = alpha;
                //Graphics.Draw(_sprite, position.x, position.y - 4);
                //Graphics.material = m;
            }
        }

        public override void Update()
        {
            base.Update();
            if (lifeTime > 0f)
            {
                lifeTime -= 0.01666666f;
                if (lifeTime < 1)
                {
                    alpha = alpha * alphaMod;
                    _sprite.alpha = _sprite.alpha * alphaMod;
                    alphaMod -= 0.016f;
                    if (alphaMod < 0)
                    {
                        alphaMod = 0;
                    }
                }

                if (doMakeSound)
                {
                    if (time == 0)
                    {
                        time = Rando.Int(6, 10) * 6;
                        Level.Add(new SoundSource(position.x, position.y, 120, "SFX/Devices/FireSound.wav", "J"));
                        DuckNetwork.SendToEveryone(new NMSoundSource(position, 120, "SFX/Devices/FireSound.wav", "J"));
                    }
                    else
                    {
                        time--;
                    }
                }
                /*for (int i = 0; i < (int)(collisionSize.x / 4); i++)
                {
                    if (Rando.Float(1) > 0.98f)
                    {
                        Level.Add(new FireSparkle(left + i * 4, position.y));
                    }
                }*/
            }
            else
            {
                Level.Remove(this);
            }

            if (grounded == true && _sprite.currentAnimation != "idle")
            {
                _sprite.SetAnimation("idle");
            }
            if (grounded == false)
            {

            }
        }
    }
    public class FireSparkle : Thing, IDrawToDifferentLayers
    {
        public SpriteMap _sprite;
        public float lifeTime = 0.6f * Rando.Float(0.9f, 1.1f);
        public float alphaMod = 1;
        Vec2 vel = new Vec2(Rando.Float(-0.5f, 0.5f), -Rando.Float(0.5f, 1f));
        SinWave velX = 0.2f;

        public SinWave _scaleWave = Rando.Float(0.06f, 0.8f);

        public FireSparkle(float xpos, float ypos) : base(xpos, ypos)
        {
        }

        public void OnDrawLayer(Layer pLayer)
        {
            if (pLayer == Layer.Game)
            {
                Sprite _p = new Sprite(GetPath("Sprites/PointLight.png"));
                _p.CenterOrigin();
                _p.scale = new Vec2(_scaleWave * Rando.Float(0.1f, 0.12f), _scaleWave * Rando.Float(0.1f, 0.12f));
                _p.depth = 0.9f;
                _p.color = Color.OrangeRed;
                Graphics.Draw(_p, position.x, position.y);
                _p.depth = 0.91f;
                _p.color = Color.Orange;
                _p.scale *= new Vec2(0.8f, 0.8f);
                Graphics.Draw(_p, position.x, position.y);
            }
        }

        public override void Update()
        {
            base.Update();
            if (lifeTime > 0f)
            {
                position += new Vec2(velX, 1) * vel;

                lifeTime -= 0.01f;
                if (lifeTime < 0.2f)
                {
                    alpha = alpha * alphaMod;
                    alphaMod -= 0.05f;
                }
            }
            else
            {
                Level.Remove(this);
            }
            if (alphaMod < 0)
            {
                Level.Remove(this);
            }
        }
    }
}
