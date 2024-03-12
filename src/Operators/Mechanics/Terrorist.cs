using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.R6S
{
    [EditorGroup("Faecterr's|Scripting")]
    public class Terrorist : PhysicsObject, IDrawToDifferentLayers
    {
        public int FOV = 60;

        //health part
        public int stepFrames = 20;
        public int Health = 100;
        public int extraHealth = 0;
        public int prevHealth = 100;

        public int lastDamage;
        public bool lastHitIsHeadshot;
        public int hitFrames;
        public bool isDead;

        //Body
        public TerroristHead head;
        public SpriteMap _sprite;
        public SpriteMap _hand;
        public int Armor = 2;
        public int Speed = 2;
        public SpriteMap _head;

        //movement
        public bool moveLeft;
        public bool moveRight;
        public bool moveUp;
        public bool moveDown;
        public bool jump;
        public bool crouching;
        public float _idleSpeed;

        /// <summary>
        /// Team name: Def or Att for defenders and attackers
        /// </summary>

        public string team = "Ter";
        public string mode = "normal";
        public float _maxSpeed;
        public bool silentStep;

        //state part
        public int fireFrames;
        public int burnFrames;
        public int elecFrames;
        public int poisonFrames;

        public Vec2 headPosition;
        public Vec2 handPosition;
        public float holdAngle;

        public bool lonewolf;
        public bool scouting;
        public bool awared;

        public int awaredFrames;

        public Operators nearestOper;
        public Operators lastDamageFrom;

        public Vec2 targetPos;
        public Vec2 recoil;

        public float ADS;
        public bool inADS;

        public int UpdatedHealth;


        public int flashedFrames;
        public int stunnedFrames;
        public int smokedFrames;

        public Vec2 destination;

        public EditorProperty<bool> Dummy;

        public Terrorist(float xpos, float ypos) : base(xpos, ypos)
        {
            _sprite = new SpriteMap(GetPath("Sprites/Operators/mute.png"), 32, 32, false);
            _hand = new SpriteMap(GetPath("Sprites/Operators/GSGGloves.png"), 8, 8, false);
            _head = new SpriteMap(GetPath("Sprites/Operators/TerroristHat.png"), 32, 32, false);
            _head.CenterOrigin();
            _hand.CenterOrigin();
            _sprite.depth = 0.4f;
            _head.depth = 0.45f;
            weight = 0f;
            flammable = 0f;
            center = new Vec2(16, 16);
            collisionSize = new Vec2(20, 14f);
            collisionOffset = new Vec2(-10f, -7f);
            SetSprites();
            offDir = Rando.Int(1) == 1 ? (sbyte)1 : (sbyte)-1;
            _sprite.color = new Vec3(255, 150, 150).ToColor();
        }

        public virtual void SetSprites()
        {
            _head.CenterOrigin();
            _hand.CenterOrigin();
            _sprite.AddAnimation("run", 0.2f, true, new int[] {
                1,
                2,
                3,
                4,
                5,
                6
            });
            _sprite.AddAnimation("idle", 1f, true, new int[] {
                0
            });
            _sprite.AddAnimation("jump", 1f, false, new int[] {
                7, 8
            });
            _sprite.AddAnimation("crouch", 1f, true, new int[] {
                11
            });
            _sprite.AddAnimation("slide", 1f, true, new int[] {
                12
            });
            _sprite.AddAnimation("injured", 0.04f, true, new int[] {
                17,
                18
            });
            _sprite.AddAnimation("planting", 0.2f, true, new int[] {
                13,
                14
            });
            _sprite.SetAnimation("idle");
            graphic = _sprite;
            base.graphic = _sprite;

            Dummy = new EditorProperty<bool>(false);
        }

        public virtual void GetDamageBullet(int Damage)
        {
            float mod = 1f;

            Health -= (int)(Damage * mod * (1 - 0.1f * Armor - 0.1f));
        }

        public virtual void GetDamage(float Damage)
        {
            Health -= (int)(Damage);          
        }

        public virtual void UpdateMode()
        {
            if (mode == "normal")
            {
                collisionSize = new Vec2(8f, 22f);
                collisionOffset = new Vec2(-4f, -7f);
                handPosition = new Vec2(3f * offDir, 4f);
                headPosition = new Vec2(0f, -1f);
            }
            if (mode == "slide")
            {
                collisionSize = new Vec2(14f, 11f);
                collisionOffset = new Vec2(-7f, 4f);
                handPosition = new Vec2(5f * offDir, 10f);
                headPosition = new Vec2(2f * offDir, 3f);
            }
            if (mode == "crouch")
            {
                collisionSize = new Vec2(10f, 16f);
                collisionOffset = new Vec2(-5f, -1f);
                handPosition = new Vec2(3f * offDir, 4f);
                headPosition = new Vec2(0f, 1f);
            }
            else
            {
                _head.flipV = false;
                _sprite.flipV = false;
                _hand.flipV = false;
            }

            if (grounded && _sprite.currentAnimation == "jump" || mode == "normal")
            {
                if (Math.Abs(hSpeed) > 0.1f)
                {
                    
                    if (_sprite.currentAnimation != "run")
                    {
                        _sprite.SetAnimation("run");
                    }
                   
                }
                else if (Math.Abs(hSpeed) <= 0.1f)
                {

                    if (_sprite.currentAnimation != "idle")
                    {
                        _sprite.SetAnimation("idle");
                    }
                }
            }
            
            if (mode == "normal" && Math.Abs(vSpeed) > 0.1f)
            {
                if (_sprite.currentAnimation != "jump")
                {
                    _sprite.SetAnimation("jump");
                }
            }
            if (mode == "crouch")
            {
                if (_sprite.currentAnimation != "crouch")
                {
                    _sprite.SetAnimation("crouch");
                }
            }
            if (mode == "slide")
            {
                if (_sprite.currentAnimation != "slide")
                {
                    _sprite.SetAnimation("slide");
                }
            }
            if (mode == "injured")
            {
                if (_sprite.currentAnimation != "injured")
                {
                    _sprite.SetAnimation("injured");
                }
            }
        }


        public virtual void UpdateMovement()
        {                                  
            if (!moveLeft && !moveRight)
            {
                float mod = 0.999999f;
                if (mode == "normal")
                {
                    mod = 0.7f;
                }
                else if (mode == "crouch")
                {
                    mod = 0.999999f;
                }
                else if (mode == "slide")
                {
                    mod = 0.999999f;
                }
                if (!grounded)
                {
                    mod = 1f;
                }
                if (hSpeed < -0.01f)
                {
                    hSpeed *= mod;
                }
                else if (hSpeed > 0.01f)
                {
                    hSpeed *= mod;
                }
            }

            float modi = 1f;

            if (mode == "normal")
            {
                _sprite.speed = 0.45f;
                modi = 0.9f;
                if (!grounded)
                {
                    modi = 0.7f;
                }
            }
            if (mode == "crouch")
            {
                _sprite.speed = 0.45f;
                modi = 0.6f;
                if (!grounded)
                {
                    modi = 0.4f;
                }
            }
            if (mode == "slide")
            {
                _sprite.speed = 0.45f;
                modi = 0.3f;
                if (!grounded)
                {
                    modi = 0.2f;
                }
            }
            if (mode == "injured")
            {
                _sprite.speed = 0.2f + 0.7f * ((Math.Abs(hSpeed) + 1) / 2);
                modi = 0.4f;
                if (!grounded)
                {
                    modi = 0.3f;
                }
            }

            if (holdObject is GunDev)
            {
                if (ADS > 0.8f)
                {
                    modi *= ((holdObject as GunDev).gunADSMobility);
                }
                else
                {
                    modi *= ((holdObject as GunDev).gunMobility);
                }
                float minSpeed = 0.13f;
                if (modi * (0.3f + 0.1f * Speed) < minSpeed)
                {
                    modi = minSpeed / (0.3f + 0.1f * Speed);
                }
            }
            
            if (moveLeft)
            {
                hSpeed -= (0.3f + 0.1f * Speed) * modi;
                if (hSpeed < -_maxSpeed * modi)
                {
                    hSpeed = -_maxSpeed * modi;
                }
                _idleSpeed += 1f;
            }
            if (moveRight)
            {
                hSpeed += (0.3f + 0.1f * Speed) * modi;
                if (hSpeed > _maxSpeed * modi)
                {
                    hSpeed = _maxSpeed * modi;
                }
                _idleSpeed += 1f;
            }
        } 

        public virtual void UpdateStatusEffects()
        {
            //Counters
            if (stepFrames > 0)
            {
                stepFrames--;
            }

            //Poisoned
            if (poisonFrames > 0)
            {
                poisonFrames--;
                if (poisonFrames % 30 == 0)
                {
                    Health -= 10;
                }
            }

            //Fired
            if (burnFrames > 0)
            {
                burnFrames -= 1;
            }
            if (burnFrames <= 0)
            {
                if (Level.CheckRect<LandFire>(topLeft, bottomRight) != null)
                {
                    burnFrames = 25;
                    Health -= 12;
                }
            }

            //Electrified
            if (elecFrames > 0)
            {
                elecFrames--;
            }
            if (elecFrames % 30 == 0 && elecFrames > 10)
            {
                Health -= 4;
            }

            if(flashedFrames > 0)
            {
                flashedFrames--;
            }
        }

        public virtual void UpdateHealth()
        {
            if (Level.CheckRect<SmallFire>(topLeft - new Vec2(12, 0), bottomRight + new Vec2(12, 6)) != null)
            {
                if (fireFrames <= 0)
                {
                    fireFrames = 20;
                }
            }

            //Extra health
            if (Health > 100)
            {
                extraHealth += Health - 100;
                Health = 100;
            }
            if (extraHealth > 0 && Health < 100)
            {
                extraHealth -= 100 - Health;
                Health = 100;
            }
            if (extraHealth > 0 && Health == 100)
            {
                extraHealth -= 1;
            }
            if (extraHealth > 100)
            {
                extraHealth = 100;
            }

            if(prevHealth != Health)
            {
                lastDamage = prevHealth - Health;
                prevHealth = Health;
                UpdatedHealth = 20;
                hitFrames = 30;
            }
            if (UpdatedHealth > 0)
            {
                UpdatedHealth--;
            }

            //Death check
            if (Health <= 0)
            {
                string nameofkiller = "N/A";
                if(lastDamageFrom != null)
                {
                    nameofkiller = lastDamageFrom.name;
                }
                if (!Dummy)
                {                    
                    if (holdObject != null)
                    {
                        Level.Remove(holdObject);
                    }
                    if (head != null)
                    {
                        Level.Remove(head);
                    }
                    _sprite.frame = 17;
                    mode = "injured";
                    Level.Add(new DeadBody(position.x, position.y) { _head = _head, _sprite = _sprite, _hand = _hand, offDir = offDir });
                    Level.Add(new InfoFeedTab(nameofkiller, "Terrorist") { typed = 1, args = new string[1] { "shot" } });
                    Level.Remove(this);
                }
                else
                {
                    Health = 100;
                    prevHealth = 100;
                    DevConsole.Log("Died");
                    Level.Add(new InfoFeedTab(nameofkiller, "Terrorist") { typed = 1, args = new string[1] { "shot" } });
                }
            }

            if (Health < 0)
            {
                Health = 0;
            }            
        }

        public virtual void UpdateHoldable()
        {
            Vec2 vec2 = targetPos - position;

            Vec2 vec3 = new Vec2(vec2.x, vec2.y * -1f);
            float num2 = (float)Math.Atan((vec3.y / vec3.x));


            float ang = (float)Math.PI * 2 - num2;

            holdAngle = ang;

            if (targetPos.x > position.x)
            {
                offDir = 1;
            }
            if (targetPos.x < position.x)
            {
                offDir = -1;
            }

            if (holdObject != null)
            {
                holdObject.offDir = offDir;
                holdObject.position = position + handPosition;
                holdObject.angle = holdAngle;

                holdObject.enablePhysics = false;
                holdObject.velocity = this.velocity;
            }


            if (holdObject == null)
            {
                L85A2 gun = new L85A2(position.x, position.y);
                holdObject = gun;
                Level.Add(gun);
            }
        }

        public override void Update()
        {
            UpdateMovement();
            UpdateStatusEffects();
            UpdateHealth();
            UpdateHoldable();

            if(head == null)
            {
                head = new TerroristHead(position.x, position.y);
                head.oper = this;
                Level.Add(head);
            }

            if(awared && awaredFrames > 0)
            {
                awaredFrames--;
                if(awaredFrames <= 0)
                {
                    awared = false;
                }
            }

            base.Update();
            UpdateMode();

            nearestOper = Level.current.NearestThing<Operators>(position);
            
            foreach(SoundSource s in Level.current.things[typeof(SoundSource)])
            {
                if (s.sound != "SFX/guns/mcx300_01.wav")
                {
                    if ((s.position - position).length < s.range * 0.45f)
                    {
                        awared = true;
                        awaredFrames = 300;

                        targetPos = s.position;
                    }
                }
            }

            if (flashedFrames <= 0)
            {
                if (!lonewolf)
                {
                    if (!awared)
                    {
                        targetPos = position + new Vec2(60 * offDir, 0);
                        if (!scouting)
                        {
                            if (nearestOper != null)
                            {
                                if ((nearestOper.x > position.x && offDir > 0) || (nearestOper.x < position.x && offDir < 0))
                                {
                                    if (Level.CheckLine<Block>(nearestOper.position, position) == null)
                                    {
                                        awared = true;
                                        targetPos = nearestOper.position + recoil;
                                        awaredFrames = 1500;
                                    }
                                }

                                if ((nearestOper.position - position).length < 240)
                                {

                                }
                            }
                            crouching = true;
                            mode = "crouch";
                        }

                        else
                        {
                            crouching = false;
                            mode = "normal";
                            if (nearestOper != null)
                            {
                                float operAngle = Maths.PointDirection(position, nearestOper.position);
                                float peekingAngle = Maths.PointDirection(position, targetPos);
                                if (awaredFrames % 60 == 1)
                                {
                                    DevConsole.Log(Convert.ToString(operAngle));
                                }

                                if (Math.Abs(Maths.RadToDeg(operAngle) - Maths.RadToDeg(peekingAngle))/2 < FOV)
                                {
                                    if (Level.CheckLine<Block>(nearestOper.position, position) == null)
                                    {
                                        awared = true;
                                        targetPos = nearestOper.position + recoil;
                                        awaredFrames = 1500;
                                    }
                                }                                
                            }
                        }
                    }
                    else
                    {
                        if (holdObject != null)
                        {
                            if (holdObject is L85A2)
                            {
                                L85A2 gun = holdObject as L85A2;
                                ADS += 0.02f;
                                targetPos = targetPos + recoil;

                                if (gun.shotFrames > 0)
                                {
                                    gun.shotFrames -= 0.01666666f;
                                }

                                recoil *= new Vec2(gun.xStability, gun.yStability);

                                if (Level.CheckLine<Block>(nearestOper.position, position) == null && gun.shotFrames <= 0 && gun.ammo > 0)
                                {
                                    if (!Dummy)
                                    {
                                        Fire();
                                    }
                                }

                                if (UpdatedHealth > 0 && gun.shotFrames <= 0 && gun.ammo > 0)
                                {
                                    if (!Dummy)
                                    {
                                        Fire();
                                    }
                                }

                                if (gun.ammo <= 0 && gun.magazine > 0)
                                {
                                    gun.Reload(0, false);
                                }

                                if (nearestOper.holdObject is GunDev)
                                {
                                    if ((nearestOper.holdObject as GunDev).reloading)
                                    {

                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public virtual void Fire()
        {
            if (holdObject != null)
            {
                if (holdObject is L85A2)
                {
                    L85A2 gun = holdObject as L85A2;

                    gun.shotFrames = gun.fireRate;

                    gun.ammo--;

                    float dir = 0f;

                    if (offDir > 0)
                    {
                        dir = 360 - Maths.RadToDeg(holdAngle);
                    }
                    else
                    {
                        dir = 180f - Maths.RadToDeg(holdAngle);
                    }

                    Level.Add(new SoundSource(position.x, position.y, 800, gun.fireSound, "J"));
                    DuckNetwork.SendToEveryone(new NMSoundSource(position, 800, gun.fireSound, "J"));
                    List<Bullet> firedBullets = new List<Bullet>();

                    //SFX.Play("plasmaFire", 1f, 1f, 0f, false);
                    for (int i = 0; i < gun.bulletsPerShot; i++)
                    {
                        AmmoType shrap = new ATSniper();

                        shrap.accuracy = 1;
                        float acc = gun.accuracy;

                        float damageMod = 1;

                        if (ADS > 0.8f)
                        {
                            shrap.accuracy = 1f;
                            acc = 0.6f;
                            damageMod *= 0.75f;
                        }
                        else
                        {
                            shrap.accuracy = 1;
                            acc = gun.accuracy * 0.6f;
                        }

                        Vec2 pos = new Vec2(gun.barrelOffsetPos.x, gun.barrelOffsetPos.y);

                        if (Level.CheckLine<Block>(position + handPosition, pos) != null || Level.CheckLine<DeployableShieldAP>(position + handPosition, pos) != null)
                        {
                            //return;
                            pos = position + handPosition;
                        }

                        shrap.range = gun.range * gun.xScope;
                        shrap.bulletSpeed = 120f;
                        shrap.bulletThickness = gun.penetration;
                        shrap.penetration = gun.penetration;
                        dir += Rando.Float(-(1 - acc) * 30, (1 - acc) * 30);

                        Bullet bullet = new Bullet(pos.x, pos.y, shrap, dir, null, false, -1f, false, true);


                        //TRACER
                        ATTracer tracer = new ATTracer();
                        tracer.range = gun.range * gun.xScope;
                        float a = angleDegrees;
                        tracer.penetration = gun.penetration;
                        tracer.accuracy = 1;
                        tracer.speedVariation = 0;

                        if (offDir < 0)
                        {
                            a = 180 - angleDegrees;
                        }
                        if (offDir > 0)
                        {
                            a = 360 - angleDegrees;
                        }
                        a += Rando.Float(-(1 - acc) * 30, (1 - acc) * 30);

                        Bullet bul = new Bullet(pos.x, pos.y, tracer, dir, owner, false, -1f, true, true);

                        gun.framesSinceShot = 0;
                        //Bullet
                        bullet.traced = true;
                        bullet.ammo.penetration = gun.penetration;

                        float dmg = 1;

                        dmg *= damageMod;


                        Level.Add(bullet);
                        firedBullets.Add(bullet);
                        bool trace = true;

                        NewBullet nb = new NewBullet(pos.x, pos.y, bul.start, bul.end, bul.end, (int)(gun.fireRate / 0.01666666 + 2), 0.2f, trace)
                        { damage = (int)(gun.damage * dmg * 0.4f), damageDropDistance = gun.damageDropDistance, minDamageDrop = gun.minDamageDrop,
                            maxDropDistance = gun.maxDropDistance, thickness = bullet.ammo.penetration / 2, maxDistance = gun.range };
                        nb.ignore.Add(this);

                        if(head != null)
                        {
                            nb.ignore.Add(head);
                        }

                        Level.Add(nb);
                    }
                    

                    recoil += new Vec2(Rando.Float(-gun.hRecoil, gun.hRecoil), gun.dRecoil);
                }
            }
        }

        public virtual void DrawBody()
        {
            _head.angle = holdAngle / 2;
            _head.alpha = alpha;
            if (offDir > 0)
            {
                _head.scale = new Vec2(1, 1);
                _head.angle -= (float)Math.PI;
            }
            if (offDir < 0)
            {
                _head.scale = new Vec2(1, -1);
            }

            _hand.alpha = alpha;

            if(flashedFrames > 0)
            {
                _hand.position = position + _head.position + new Vec2(6 * offDir, -1);
                _hand.frame = 6;
                _hand.angle = -1.1f * offDir;
                //_hand.flipH = true;
            }
            else
            {
                _hand.frame = 0;
            }

            Graphics.Draw(_head, position.x + headPosition.x, position.y + headPosition.y, 0.45f);
            Graphics.Draw(_hand, position.x + handPosition.x + 2 * offDir, position.y + handPosition.y + 2, 0.45f);
        }

        public virtual void LookingForEnemy()
        {

        }


        public void OnDrawLayer(Layer layer)
        {
            if(layer == Layer.Foreground)
            {
                if (lastDamage > 0 && hitFrames > 0 && Dummy)
                {
                    Graphics.DrawStringOutline(Convert.ToString(lastDamage), position + new Vec2(-4, -12), Color.Wheat, Color.Black, 1f, null, 0.6f);
                    hitFrames--;
                }
                if (Dummy && Health < 100)
                {
                    Graphics.DrawStringOutline(Convert.ToString(100 - Health), position + new Vec2(-4, 12), Color.Wheat, Color.Black, 1f, null, 0.6f);
                }
            }
        }

        public override void Draw()
        {
            _head.alpha = alpha;
            base.Draw(); 
            graphic.flipH = flipHorizontal;
            DrawBody();
        }
    }
}
